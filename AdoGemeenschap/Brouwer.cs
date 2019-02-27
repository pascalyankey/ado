using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoGemeenschap
{
    public class Brouwer
    {
        public Brouwer() { }
        public Brouwer(Int32 brouwernr, string naam, string adres, Int16 postcode, string gemeente, Int32? omzet)
        {
            BrouwerNr = brouwernr;
            Naam = naam;
            Adres = adres;
            Postcode = postcode;
            Gemeente = gemeente;
            Omzet = omzet;
            Changed = false;
        }
        public Int32 BrouwerNr { get; }
        public bool Changed { get; set; }
        private String naamValue;
        public string Naam
        {
            get
            {
                return naamValue;
            }
            set
            {
                naamValue = value;
                Changed = true;
            }
        }
        private String adresValue;
        public string Adres
        {
            get
            {
                return adresValue;
            }
            set
            {
                adresValue = value;
                Changed = true;
            }
        }
        private Int16 postcodeValue;
        public Int16 Postcode
        {
            get
            {
                return postcodeValue;
            }
            set
            {
                postcodeValue = value;
                Changed = true;
            }
        }
        private String gemeenteValue;
        public string Gemeente
        {
            get
            {
                return gemeenteValue;
            }
            set
            {
                gemeenteValue = value;
                Changed = true;
            }
        }
        private Int32? omzetValue;
        public Int32? Omzet
        {
            get { return omzetValue; }
            set
            {
                if (value.HasValue && Convert.ToInt32(value) < 0)
                {
                    throw new Exception("Omzet moet positief zijn");
                }
                else
                {
                    omzetValue = value;
                    Changed = true;
                }
            }
        }

    }
}
