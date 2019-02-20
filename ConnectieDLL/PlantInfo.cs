using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectieDLL
{
    public class PlantInfo
    {
        public PlantInfo(string plantnaam, string soort, string leverancier, string kleur, Decimal kostprijs)
        {
            PlantNaam = plantnaam;
            Soort = soort;
            Leverancier = leverancier;
            Kleur = kleur;
            Kostprijs = kostprijs;
        }
        public string PlantNaam { get; set; }
        public string Soort { get; set; }
        public string Leverancier { get; set; }
        public string Kleur { get; set; }
        public Decimal Kostprijs { get; set; }
    }
}
