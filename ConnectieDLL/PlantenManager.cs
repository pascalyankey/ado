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
    }
}
