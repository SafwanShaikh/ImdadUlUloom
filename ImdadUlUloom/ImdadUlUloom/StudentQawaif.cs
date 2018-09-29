using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdadUlUloom
{
    class StudentQawaif
    {
        private Boolean hifz;
        private Boolean nazira;
        private String darseNizami;
        private String lastIdara;
        private String asriTaleem;

        public StudentQawaif() { }

        public bool Hifz
        {
            get
            {
                return hifz;
            }

            set
            {
                hifz = value;
            }
        }

        public bool Nazira
        {
            get
            {
                return nazira;
            }

            set
            {
                nazira = value;
            }
        }

        public string DarseNizami
        {
            get
            {
                return darseNizami;
            }

            set
            {
                darseNizami = value;
            }
        }

        public string LastIdara
        {
            get
            {
                return lastIdara;
            }

            set
            {
                lastIdara = value;
            }
        }

        public string AsriTaleem
        {
            get
            {
                return asriTaleem;
            }

            set
            {
                asriTaleem = value;
            }
        }

        public StudentQawaif(bool hifz, bool nazira, string darseNizami, string lastIdara, string asriTaleem)
        {
            this.Hifz = hifz;
            this.Nazira = nazira;
            this.DarseNizami = darseNizami;
            this.LastIdara = lastIdara;
            this.AsriTaleem = asriTaleem;
        }
    }
}
