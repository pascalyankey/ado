using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Collections.ObjectModel;

namespace ConnectieDLL
{
    public class SoortenManager
    {
        public ObservableCollection<Soort> GetSoorten()
        {
            var soorten = new ObservableCollection<Soort>();
            var manager = new TuincentrumDbManager();

            using (var conTuincentrum = manager.GetConnection())
            {
                using (var comSoorten = conTuincentrum.CreateCommand())
                {
                    comSoorten.CommandType = CommandType.Text;
                    comSoorten.CommandText = "select * from Soorten order by Soort ASC";

                    conTuincentrum.Open();
                    using (var rdrSoorten = comSoorten.ExecuteReader())
                    {
                        Int32 soortNrPos = rdrSoorten.GetOrdinal("SoortNr");
                        Int32 naamPos = rdrSoorten.GetOrdinal("Soort");
                        Int32 magazijnNrPos = rdrSoorten.GetOrdinal("MagazijnNr");
                        while (rdrSoorten.Read())
                        {
                            soorten.Add(new Soort(rdrSoorten.GetInt32(soortNrPos), 
                                                rdrSoorten.GetString(naamPos), 
                                                rdrSoorten.GetByte(magazijnNrPos)));
                        }
                    }
                }
            }
            return soorten;
        }

        public ObservableCollection<Plant> GetPlanten(Int32 soortNr)
        {
            var planten = new ObservableCollection<Plant>();
            var manager = new TuincentrumDbManager();

            using (var conTuincentrum = manager.GetConnection())
            {
                using (var comPlanten = conTuincentrum.CreateCommand())
                {
                    comPlanten.CommandType = CommandType.Text;
                    if (soortNr != 0)
                    {
                        comPlanten.CommandText = "select * from Planten where SoortNr = @soortNr order by Naam ASC";
                        var parSoortNr = comPlanten.CreateParameter();
                        parSoortNr.ParameterName = "@soortNr";
                        parSoortNr.Value = soortNr;
                        comPlanten.Parameters.Add(parSoortNr);
                    }
                    else
                        comPlanten.CommandText = "select * from Planten";
                    conTuincentrum.Open();
                    using (var rdrPlanten = comPlanten.ExecuteReader())
                    {
                        Int32 plantNrPos = rdrPlanten.GetOrdinal("PlantNr");
                        Int32 naamPos = rdrPlanten.GetOrdinal("Naam");
                        Int32 soortNrPos = rdrPlanten.GetOrdinal("SoortNr");
                        Int32 levNrPos = rdrPlanten.GetOrdinal("Levnr");
                        Int32 kleurPos = rdrPlanten.GetOrdinal("Kleur");
                        Int32 verkoopPrijsPos = rdrPlanten.GetOrdinal("VerkoopPrijs");
                        while (rdrPlanten.Read())
                        {
                            planten.Add(new Plant(rdrPlanten.GetInt32(plantNrPos),
                                                rdrPlanten.GetString(naamPos),
                                                rdrPlanten.GetInt32(soortNrPos),
                                                rdrPlanten.GetInt32(levNrPos),
                                                rdrPlanten.GetString(kleurPos),
                                                rdrPlanten.GetDecimal(verkoopPrijsPos)
                                                ));

                        }
                    }
                }
            }
            return planten;
        }

        public List<Plant> SchrijfWijzigingen(List<Plant> planten)
        {
            var nietDoorgevoerdePlanten = new List<Plant>();
            var manager = new TuincentrumDbManager();
            using (var conTuincentrum = manager.GetConnection())
            {
                using (var comUpdate = conTuincentrum.CreateCommand())
                {
                    comUpdate.CommandType = CommandType.Text;
                    comUpdate.CommandText = "update Planten set Kleur=@kleur, VerkoopPrijs=@verkoopprijs where SoortNr=@soortnr and Naam=@naam";

                    var parKleur = comUpdate.CreateParameter();
                    parKleur.ParameterName = "@kleur";
                    comUpdate.Parameters.Add(parKleur);

                    var parVerkoopPrijs = comUpdate.CreateParameter();
                    parVerkoopPrijs.ParameterName = "@verkoopprijs";
                    comUpdate.Parameters.Add(parVerkoopPrijs);

                    var parSoortNr = comUpdate.CreateParameter();
                    parSoortNr.ParameterName = "@soortnr";
                    comUpdate.Parameters.Add(parSoortNr);

                    var parNaam = comUpdate.CreateParameter();
                    parNaam.ParameterName = "@naam";
                    comUpdate.Parameters.Add(parNaam);

                    conTuincentrum.Open();
                    foreach (Plant eenPlant in planten)
                    {
                        try
                        {
                            parKleur.Value = eenPlant.Kleur;
                            parVerkoopPrijs.Value = eenPlant.VerkoopPrijs;
                            parSoortNr.Value = eenPlant.SoortNr;
                            parNaam.Value = eenPlant.Naam;
                            if (comUpdate.ExecuteNonQuery() == 0)
                                nietDoorgevoerdePlanten.Add(eenPlant);
                        }
                        catch (Exception)
                        {
                            nietDoorgevoerdePlanten.Add(eenPlant);
                        }
                    }
                }
            }
            return nietDoorgevoerdePlanten;
        }
    }
}
