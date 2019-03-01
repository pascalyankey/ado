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
            Changed = false;
        }
        public bool Changed { get; set; }
        public Int32 PlantNr { get; }
        public string Naam { get; set; }
        public Int32 SoortNr { get; set; }
        public Int32 Levnr { get; }
        private string kleurValue;
        public string Kleur
        {
            get
            {
                return kleurValue;
            }
            set
            {
                kleurValue = value;
                Changed = true;
            }
        }
        private Decimal verkoopprijsValue;
        public Decimal VerkoopPrijs
        {
            get
            {
                return verkoopprijsValue;
            }
            set
            {
                verkoopprijsValue = value;
                Changed = true;
            }
        }
    }
}
