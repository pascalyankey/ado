using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectieDLL
{
    public class Leverancier
    {
        public Leverancier() { }
        public Leverancier(Int32 levnr, string naam, string adres, string postnr, string woonplaats, Object versie)
        {
            LevNr = levnr;
            Naam = naam;
            Adres = adres;
            PostNr = postnr;
            Woonplaats = woonplaats;
            Changed = false;
            Versie = versie;
        }
        public bool Changed { get; set; }
        public Int32 LevNr { get; }
        private string naamValue;
        public string Naam
        {
            get { return naamValue; }
            set
            {
                naamValue = value;
                Changed = true;
            }
        }
        private string adresValue;
        public string Adres
        {
            get { return adresValue; }
            set
            {
                adresValue = value;
                Changed = true;
            }
        }
        private string postnrValue;
        public string PostNr
        {
            get { return postnrValue; }
            set
            {
                postnrValue = value;
                Changed = true;
            }
        }
        private string woonplaatsValue;
        public string Woonplaats
        {
            get { return woonplaatsValue; }
            set
            {
                woonplaatsValue = value;
                Changed = true;
            }
        }
        public Object Versie { get; set; }
    }
}
