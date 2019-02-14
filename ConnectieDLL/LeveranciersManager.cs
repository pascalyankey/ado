using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Transactions;

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
                    return comToevoegen.ExecuteNonQuery();
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
    }
}
