using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectieDLL
{
    public class Plant
    {
        public Plant(Int32 plantnr, string naam, Int32 soortnr, Int32 levnr, string kleur, Decimal verkoopprijs)
        {
            PlantNr = plantnr;
            Naam = naam;
            SoortNr = soortnr;
            Levnr = levnr;
            Kleur = kleur;
            VerkoopPrijs = verkoopprijs;
        }
        public Int32 PlantNr { get; }
        public string Naam { get; set; }
        public Int32 SoortNr { get; set; }
        public Int32 Levnr { get; set; }
        public string Kleur { get; set; }
        public Decimal VerkoopPrijs { get; set; }
    }
}
