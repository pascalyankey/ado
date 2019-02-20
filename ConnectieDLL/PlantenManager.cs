using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace ConnectieDLL
{
    public class PlantenManager
    {
        public Int32 Eindejaarskorting(Decimal korting)
        {
            var dbManager = new TuincentrumDbManager();
            using (var conTuincentrum = dbManager.GetConnection())
            {
                using (var comEindejaarskorting = conTuincentrum.CreateCommand())
                {
                    comEindejaarskorting.CommandType = CommandType.StoredProcedure;
                    comEindejaarskorting.CommandText = "Eindejaarskorting";

                    DbParameter parKorting = comEindejaarskorting.CreateParameter();
                    parKorting.ParameterName = "@korting";
                    parKorting.Value = korting;
                    comEindejaarskorting.Parameters.Add(parKorting);

                    conTuincentrum.Open();
                    return comEindejaarskorting.ExecuteNonQuery();
                }
            }
        }

        public Decimal BerekenGemiddeldeKostprijs(string soort)
        {
            var dbManager = new TuincentrumDbManager();
            using (var conTuincentrum = dbManager.GetConnection())
            {
                using (var comGemPrijs = conTuincentrum.CreateCommand())
                {
                    comGemPrijs.CommandType = CommandType.StoredProcedure;
                    comGemPrijs.CommandText = "BerekenGemiddeldeKostprijs";

                    var parSoort = comGemPrijs.CreateParameter();
                    parSoort.ParameterName = "@soort";
                    parSoort.Value = soort;
                    comGemPrijs.Parameters.Add(parSoort);

                    conTuincentrum.Open();
                    Object resultaat = comGemPrijs.ExecuteScalar();
                    if (resultaat == DBNull.Value)
                    {
                        throw new Exception("Soort bestaat niet");
                    }
                    else
                    {
                        return (Decimal)resultaat;
                    }
                }
            }
        }

        public PlantInfo PlantInfoRaadplegen (int plantNr)
        {
            var dbManager = new TuincentrumDbManager();
            using (var conTuincentrum = dbManager.GetConnection())
            {
                using (var comInfo = conTuincentrum.CreateCommand())
                {
                    comInfo.CommandType = CommandType.StoredProcedure;
                    comInfo.CommandText = "PlantInfoRaadplegen";

                    var parPlantNr = comInfo.CreateParameter();
                    parPlantNr.ParameterName = "@PlantNr";
                    parPlantNr.Value = plantNr;
                    comInfo.Parameters.Add(parPlantNr);

                    var parPlantNaam = comInfo.CreateParameter();
                    parPlantNaam.ParameterName = "@Plantnaam";
                    parPlantNaam.DbType = DbType.String;
                    parPlantNaam.Size = 30;
                    parPlantNaam.Direction = ParameterDirection.Output;
                    comInfo.Parameters.Add(parPlantNaam);

                    var parSoort = comInfo.CreateParameter();
                    parSoort.ParameterName = "@Soort";
                    parSoort.DbType = DbType.String;
                    parSoort.Size = 10;
                    parSoort.Direction = ParameterDirection.Output;
                    comInfo.Parameters.Add(parSoort);

                    var parLevNaam = comInfo.CreateParameter();
                    parLevNaam.ParameterName = "@Leveranciersnaam";
                    parLevNaam.DbType = DbType.String;
                    parLevNaam.Size = 30;
                    parLevNaam.Direction = ParameterDirection.Output;
                    comInfo.Parameters.Add(parLevNaam);

                    var parKleur = comInfo.CreateParameter();
                    parKleur.ParameterName = "@Kleur";
                    parKleur.DbType = DbType.String;
                    parKleur.Size = 10;
                    parKleur.Direction = ParameterDirection.Output;
                    comInfo.Parameters.Add(parKleur);

                    var parKostprijs = comInfo.CreateParameter();
                    parKostprijs.ParameterName = "@Kostprijs";
                    parKostprijs.DbType = DbType.Currency;
                    parKostprijs.Direction = ParameterDirection.Output;
                    comInfo.Parameters.Add(parKostprijs);

                    conTuincentrum.Open();
                    comInfo.ExecuteNonQuery();
                    if (parPlantNaam.Value.Equals(DBNull.Value))
                    {
                        throw new Exception("Plant bestaat niet");
                    }
                    else
                    {
                        return new PlantInfo((string)parPlantNaam.Value, (string)parSoort.Value, (string)parLevNaam.Value, (string)parKleur.Value, (Decimal)parKostprijs.Value);
                    }
                }
            }
        }
    }
}
