using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Transactions;
using System.Collections.ObjectModel;

namespace ConnectieDLL
{
    public class LeveranciersManager
    {
        public Int32 Toevoegen(string Naam, string Adres, string PostNr, string Woonplaats)
        {
            var dbManger = new TuincentrumDbManager();
            using (var conTuincentrum = dbManger.GetConnection())
            {
                using (var comToevoegen = conTuincentrum.CreateCommand())
                {
                    comToevoegen.CommandType = CommandType.StoredProcedure;
                    comToevoegen.CommandText = "Toevoegen";

                    DbParameter parNaam = comToevoegen.CreateParameter();
                    parNaam.ParameterName = "@Naam";
                    parNaam.Value = Naam;
                    comToevoegen.Parameters.Add(parNaam);

                    DbParameter parAdres = comToevoegen.CreateParameter();
                    parAdres.ParameterName = "@Adres";
                    parAdres.Value = Adres;
                    comToevoegen.Parameters.Add(parAdres);

                    DbParameter parPostNr = comToevoegen.CreateParameter();
                    parPostNr.ParameterName = "@PostNr";
                    parPostNr.Value = PostNr;
                    comToevoegen.Parameters.Add(parPostNr);

                    DbParameter parWoonplaats = comToevoegen.CreateParameter();
                    parWoonplaats.ParameterName = "@Woonplaats";
                    parWoonplaats.Value = Woonplaats;
                    comToevoegen.Parameters.Add(parWoonplaats);

                    conTuincentrum.Open();
                    Int32 levNr = Convert.ToInt32(comToevoegen.ExecuteScalar());
                    return levNr;
                }
            }
        }

        public void VervangLeverancier(int oudLevnr, int nieuwLevnr)
        {
            var dbManger = new TuincentrumDbManager();

            var opties = new TransactionOptions();
            opties.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (var traVervangLeverancier = new TransactionScope(TransactionScopeOption.Required, opties))
            {
                using (var conTuincentrum = dbManger.GetConnection())
                {
                    using (var comVervangLevPlanten = conTuincentrum.CreateCommand())
                    {
                        comVervangLevPlanten.CommandType = CommandType.StoredProcedure;
                        comVervangLevPlanten.CommandText = "VervangLeverancier";

                        var parOudLevnr = comVervangLevPlanten.CreateParameter();
                        parOudLevnr.ParameterName = "@oudLevnr";
                        parOudLevnr.Value = oudLevnr;
                        comVervangLevPlanten.Parameters.Add(parOudLevnr);

                        var parNieuwLevnr = comVervangLevPlanten.CreateParameter();
                        parNieuwLevnr.ParameterName = "@nieuwLevnr";
                        parNieuwLevnr.Value = nieuwLevnr;
                        comVervangLevPlanten.Parameters.Add(parNieuwLevnr);

                        conTuincentrum.Open();
                        if (comVervangLevPlanten.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Oud leveranciersnummer bestaat niet");
                        }
                    }
                }
                using (var conTuincentrum = dbManger.GetConnection())
                {
                    using (var comVerwijderLev = conTuincentrum.CreateCommand())
                    {
                        comVerwijderLev.CommandType = CommandType.StoredProcedure;
                        comVerwijderLev.CommandText = "VerwijderLeverancier";

                        var parOudLevnr = comVerwijderLev.CreateParameter();
                        parOudLevnr.ParameterName = "@oudLevnr";
                        parOudLevnr.Value = oudLevnr;
                        comVerwijderLev.Parameters.Add(parOudLevnr);

                        conTuincentrum.Open();
                        if (comVerwijderLev.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Oud leveranciersnummer bestaat niet");
                        }
                        traVervangLeverancier.Complete();
                    }
                }
            }
        }

        public ObservableCollection<Leverancier> GetLeveranciers (string postNr)
        {
            var leveranciers = new ObservableCollection<Leverancier>();
            var manager = new TuincentrumDbManager();

            using (var conTuincentrum = manager.GetConnection())
            {
                using (var comLeveranciers = conTuincentrum.CreateCommand())
                {
                    comLeveranciers.CommandType = CommandType.Text;
                    if (postNr != string.Empty)
                    {
                        comLeveranciers.CommandText = "select * from Leveranciers where PostNr = @postnr order by PostNr";

                        var parPostNr = comLeveranciers.CreateParameter();
                        parPostNr.ParameterName = "@postnr";
                        parPostNr.Value = postNr;
                        comLeveranciers.Parameters.Add(parPostNr);
                    }
                    else
                        comLeveranciers.CommandText = "select * from Leveranciers";
                    conTuincentrum.Open();
                    using (var rdrLeveranciers = comLeveranciers.ExecuteReader())
                    {
                        Int32 levNrPos = rdrLeveranciers.GetOrdinal("LevNr");
                        Int32 naamPos = rdrLeveranciers.GetOrdinal("Naam");
                        Int32 adresPos = rdrLeveranciers.GetOrdinal("Adres");
                        Int32 postNrPos = rdrLeveranciers.GetOrdinal("PostNr");
                        Int32 woonplaatsPos = rdrLeveranciers.GetOrdinal("Woonplaats");
                        Int32 VersiePos = rdrLeveranciers.GetOrdinal("Versie");
                        while (rdrLeveranciers.Read())
                        {
                            leveranciers.Add(new Leverancier(rdrLeveranciers.GetInt32(levNrPos),
                                                            rdrLeveranciers.GetString(naamPos),
                                                            rdrLeveranciers.GetString(adresPos),
                                                            rdrLeveranciers.GetString(postNrPos),
                                                            rdrLeveranciers.GetString(woonplaatsPos),
                                                            rdrLeveranciers.GetValue(VersiePos)));
                        }
                    }
                }
            }
            return leveranciers;
        }

        public List<Leverancier> SchrijfVerwijderingen(List<Leverancier> leveranciers)
        {
            var nietVerwijderdeLeveranciers = new List<Leverancier>();
            var manager = new TuincentrumDbManager();
            using (var conTuincentrum = manager.GetConnection())
            {
                using (var comDelete = conTuincentrum.CreateCommand())
                {
                    comDelete.CommandType = CommandType.Text;
                    comDelete.CommandText = "delete from Leveranciers where LevNr=@levnr";

                    var parLevNr = comDelete.CreateParameter();
                    parLevNr.ParameterName = "@levnr";
                    comDelete.Parameters.Add(parLevNr);

                    conTuincentrum.Open();
                    foreach (Leverancier eenLeverancier in leveranciers)
                    {
                        try
                        {
                            parLevNr.Value = eenLeverancier.LevNr;
                            if (comDelete.ExecuteNonQuery() == 0)
                                nietVerwijderdeLeveranciers.Add(eenLeverancier);
                        }
                        catch (Exception)
                        {
                            nietVerwijderdeLeveranciers.Add(eenLeverancier);
                        }
                    }
                }
            }
            return nietVerwijderdeLeveranciers;
        }

        public List<Leverancier> SchrijfToevoegingen(List<Leverancier> leveranciers)
        {
            var nietToegevoegdeLeveranciers = new List<Leverancier>();
            var manager = new TuincentrumDbManager();
            using (var conTuincentrum = manager.GetConnection())
            {
                using (var comInsert = conTuincentrum.CreateCommand())
                {
                    comInsert.CommandType = CommandType.Text;
                    comInsert.CommandText = "Insert into Leveranciers (Naam, Adres, PostNr, Woonplaats) values(@naam, @adres, @postnr, @woonplaats)";

                    var parNaam = comInsert.CreateParameter();
                    parNaam.ParameterName = "@naam";
                    comInsert.Parameters.Add(parNaam);

                    var parAdres = comInsert.CreateParameter();
                    parAdres.ParameterName = "@adres";
                    comInsert.Parameters.Add(parAdres);

                    var parPostNr = comInsert.CreateParameter();
                    parPostNr.ParameterName = "@postnr";
                    comInsert.Parameters.Add(parPostNr);

                    var parWoonplaats = comInsert.CreateParameter();
                    parWoonplaats.ParameterName = "@woonplaats";
                    comInsert.Parameters.Add(parWoonplaats);

                    conTuincentrum.Open();
                    foreach (Leverancier eenLeverancier in leveranciers)
                    {
                        try
                        {
                            parNaam.Value = eenLeverancier.Naam;
                            parAdres.Value = eenLeverancier.Adres;
                            parPostNr.Value = eenLeverancier.PostNr;
                            parWoonplaats.Value = eenLeverancier.Woonplaats;
                            if (comInsert.ExecuteNonQuery() == 0)
                                nietToegevoegdeLeveranciers.Add(eenLeverancier);
                        }
                        catch (Exception)
                        {
                            nietToegevoegdeLeveranciers.Add(eenLeverancier);
                        }
                    }
                }
            }
            return nietToegevoegdeLeveranciers;
        }

        public List<Leverancier> SchrijfWijzigingen(List<Leverancier> leveranciers)
        {
            var nietDoorgevoerdeLeveranciers = new List<Leverancier>();
            var manager = new TuincentrumDbManager();
            using (var conTuincentrum = manager.GetConnection())
            {
                using (var comUpdate = conTuincentrum.CreateCommand())
                {
                    comUpdate.CommandType = CommandType.Text;
                    comUpdate.CommandText = "update Leveranciers set Naam=@naam, Adres=@adres, PostNr=@postnr, Woonplaats=@woonplaats where LevNr=@levnr and Versie=@versie";

                    var parNaam = comUpdate.CreateParameter();
                    parNaam.ParameterName = "@naam";
                    comUpdate.Parameters.Add(parNaam);

                    var parAdres = comUpdate.CreateParameter();
                    parAdres.ParameterName = "@adres";
                    comUpdate.Parameters.Add(parAdres);

                    var parPostNr = comUpdate.CreateParameter();
                    parPostNr.ParameterName = "@postnr";
                    comUpdate.Parameters.Add(parPostNr);

                    var parWoonplaats = comUpdate.CreateParameter();
                    parWoonplaats.ParameterName = "@woonplaats";
                    comUpdate.Parameters.Add(parWoonplaats);

                    var parLevNr = comUpdate.CreateParameter();
                    parLevNr.ParameterName = "@levnr";
                    comUpdate.Parameters.Add(parLevNr);

                    var parVersie = comUpdate.CreateParameter();
                    parVersie.ParameterName = "@versie";
                    comUpdate.Parameters.Add(parVersie);

                    conTuincentrum.Open();
                    foreach (Leverancier eenLeverancier in leveranciers)
                    {
                        try
                        {
                            parNaam.Value = eenLeverancier.Naam;
                            parAdres.Value = eenLeverancier.Adres;
                            parPostNr.Value = eenLeverancier.PostNr;
                            parWoonplaats.Value = eenLeverancier.Woonplaats;
                            parLevNr.Value = eenLeverancier.LevNr;
                            parVersie.Value = eenLeverancier.Versie;
                            if (comUpdate.ExecuteNonQuery() == 0)
                            {
                                nietDoorgevoerdeLeveranciers.Add(eenLeverancier);
                            } 
                        }
                        catch (Exception)
                        {
                            nietDoorgevoerdeLeveranciers.Add(eenLeverancier);
                        }
                    }
                }
            }
            return nietDoorgevoerdeLeveranciers;
        }

        public void SchrijfWijzigingenMultiUser(List<Leverancier> leveranciers)
        {
            var manager = new TuincentrumDbManager();
            using (var conTuincentrum = manager.GetConnection())
            {
                using (var comUpdate = conTuincentrum.CreateCommand())
                {
                    comUpdate.CommandType = CommandType.Text;
                    comUpdate.CommandText = "update Leveranciers set Naam=@naam, Adres=@adres, PostNr=@postnr, Woonplaats=@woonplaats where LevNr=@levnr and Versie=@versie";

                    var parNaam = comUpdate.CreateParameter();
                    parNaam.ParameterName = "@naam";
                    comUpdate.Parameters.Add(parNaam);

                    var parAdres = comUpdate.CreateParameter();
                    parAdres.ParameterName = "@adres";
                    comUpdate.Parameters.Add(parAdres);

                    var parPostNr = comUpdate.CreateParameter();
                    parPostNr.ParameterName = "@postnr";
                    comUpdate.Parameters.Add(parPostNr);

                    var parWoonplaats = comUpdate.CreateParameter();
                    parWoonplaats.ParameterName = "@woonplaats";
                    comUpdate.Parameters.Add(parWoonplaats);

                    var parLevNr = comUpdate.CreateParameter();
                    parLevNr.ParameterName = "@levnr";
                    comUpdate.Parameters.Add(parLevNr);

                    var parVersie = comUpdate.CreateParameter();
                    parVersie.ParameterName = "@versie";
                    comUpdate.Parameters.Add(parVersie);

                    conTuincentrum.Open();
                    foreach (Leverancier eenLeverancier in leveranciers)
                    {
                        parNaam.Value = eenLeverancier.Naam;
                        parAdres.Value = eenLeverancier.Adres;
                        parPostNr.Value = eenLeverancier.PostNr;
                        parWoonplaats.Value = eenLeverancier.Woonplaats;
                        parLevNr.Value = eenLeverancier.LevNr;
                        parVersie.Value = eenLeverancier.Versie;
                        if (comUpdate.ExecuteNonQuery() == 0)
                            throw new Exception("Iemand was je voor");
                    }
                }
            }
        }
    }
}
