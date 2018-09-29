using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdadUlUloom
{
    class StudentRihaish
    {
        private Boolean rihaish;
        private Boolean imdadi;

        public StudentRihaish() { }

        public bool Rihaish
        {
            get
            {
                return rihaish;
            }

            set
            {
                rihaish = value;
            }
        }

        public bool Imdadi
        {
            get
            {
                return imdadi;
            }

            set
            {
                imdadi = value;
            }
        }

        public StudentRihaish(bool rihaish, bool imdadi)
        {
            this.Rihaish = rihaish;
            this.Imdadi = imdadi;
        }
    }
}
