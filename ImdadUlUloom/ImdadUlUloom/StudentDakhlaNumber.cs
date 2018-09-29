using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdadUlUloom
{
    class StudentDakhlaNumber
    {
        private int dakhlaNumber;
        private int formNumber;
        private String darja;
        private String darjaYear;
        private Boolean activeIndex;

        public int FormNumber
        {
            get
            {
                return formNumber;
            }

            set
            {
                formNumber = value;
            }
        }

        public string Darja
        {
            get
            {
                return darja;
            }

            set
            {
                darja = value;
            }
        }

        public string DarjaYear
        {
            get
            {
                return darjaYear;
            }

            set
            {
                darjaYear = value;
            }
        }

        public bool ActiveIndex
        {
            get
            {
                return activeIndex;
            }

            set
            {
                activeIndex = value;
            }
        }
        
        public int DakhlaNumber
        {
            get
            {
                return dakhlaNumber;
            }

            set
            {
                dakhlaNumber = value;
            }
        }

        public StudentDakhlaNumber()
        {

        }

        public StudentDakhlaNumber(int dakhlaNumber, int formNumber, string darja, string darjaYear, bool activeIndex)
        {
            this.DakhlaNumber = dakhlaNumber;
            this.FormNumber = formNumber;
            this.Darja = darja;
            this.DarjaYear = darjaYear;
            this.ActiveIndex = activeIndex;
        }
    }
}
