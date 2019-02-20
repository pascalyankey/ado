using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoGemeenschap
{
    public class Brouwer
    {
        public Brouwer(Int32 brouwernr, string naam, string adres, Int16 postcode, string gemeente, Int32? omzet)
        {
            brouwersNrValue = brouwernr;
            Naam = naam;
            Adres = adres;
            Postcode = postcode;
            Gemeente = gemeente;
            Omzet = omzet;
        }
        private Int32 brouwersNrValue;
        public Int32 BrouwerNr
        {
            get { return brouwersNrValue; }

        }
        public string Naam { get; set; }
        public string Adres { get; set; }
        private Int16 postcodeValue;
        public Int16 Postcode
        {
            get { return postcodeValue; }
            set
            {
                if (value < 1000 || value > 9999)
                {
                    throw new Exception("Postcode moet tussen 1000 en 9999 liggen");
                }
                else
                {
                    postcodeValue = value;
                }
            }
        }
        public string Gemeente { get; set; }
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
                }
            }
        }

    }
}
