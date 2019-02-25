using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectieDLL
{
    public class Soort
    {
        public Soort(Int32 soortnr, string naam, Byte magazijnnr)
        {
            SoortNr = soortnr;
            Naam = naam;
            MagazijnNr = magazijnnr;
        }
        public Int32 SoortNr { get; }
        public string Naam { get; set; }
        public Byte MagazijnNr { get; set; }
    }
}
