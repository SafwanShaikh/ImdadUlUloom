using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdadUlUloom
{
    class StudentFormDate
    {
        private String takmeelDakhlaDate;
        private String ikhrajDate;

        public StudentFormDate()
        {

        }

        public String TakmeelDakhlaDate
        {
            get
            {
                return takmeelDakhlaDate;
            }

            set
            {
                takmeelDakhlaDate = value;
            }
        }

        public String IkhrajDate
        {
            get
            {
                return ikhrajDate;
            }

            set
            {
                ikhrajDate = value;
            }
        }
        
        public StudentFormDate(String takmeelDakhlaDate, String ikhrajDate)
        {
            this.TakmeelDakhlaDate = takmeelDakhlaDate;
            this.IkhrajDate = ikhrajDate;
        }
    }
}
