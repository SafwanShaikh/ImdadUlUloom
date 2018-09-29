using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdadUlUloom
{
    class StudentPermanentAddress
    {
        private String zila;
        private String tehseel;
        private String dakKhana;
        private String village;

        public StudentPermanentAddress() { }

        public string Zila
        {
            get
            {
                return zila;
            }

            set
            {
                zila = value;
            }
        }

        public string Tehseel
        {
            get
            {
                return tehseel;
            }

            set
            {
                tehseel = value;
            }
        }

        public string DakKhana
        {
            get
            {
                return dakKhana;
            }

            set
            {
                dakKhana = value;
            }
        }

        public string Village
        {
            get
            {
                return village;
            }

            set
            {
                village = value;
            }
        }

        public StudentPermanentAddress(string zila, string tehseel, string dakKhana, string village)
        {
            this.Zila = zila;
            this.Tehseel = tehseel;
            this.DakKhana = dakKhana;
            this.Village = village;
        }
    }
}
