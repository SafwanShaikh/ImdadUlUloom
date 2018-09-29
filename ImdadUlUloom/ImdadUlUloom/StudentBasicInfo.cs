using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdadUlUloom
{
    class StudentBasicInfo
    {
        private String nameStudent;
        private String contactStudent;
        private String pictureStudent;
        private String fatherNameStudent;
        private DateTime dobStudent;
        private String cnicStudent;
        private String requiredDarjaStudent;
        private Byte[] imageStudent;
        
        public string NameStudent
        {
            get
            {
                return nameStudent;
            }

            set
            {
                nameStudent = value;
            }
        }

        public string ContactStudent
        {
            get
            {
                return contactStudent;
            }

            set
            {
                contactStudent = value;
            }
        }

        public string FatherNameStudent
        {
            get
            {
                return fatherNameStudent;
            }

            set
            {
                fatherNameStudent = value;
            }
        }

        public DateTime DobStudent
        {
            get
            {
                return dobStudent;
            }

            set
            {
                dobStudent = value;
            }
        }

        public string CnicStudent
        {
            get
            {
                return cnicStudent;
            }

            set
            {
                cnicStudent = value;
            }
        }

        public string RequiredDarjaStudent
        {
            get
            {
                return requiredDarjaStudent;
            }

            set
            {
                requiredDarjaStudent = value;
            }
        }
        
        public Byte[] ImageStudent
        {
            get
            {
                return imageStudent;
            }

            set
            {
                imageStudent = value;
            }
        }

        public StudentBasicInfo() { }

        public StudentBasicInfo(string nameStudent, string contactStudent, string fatherNameStudent, DateTime dobStudent, string cnicStudent, string requiredDarjaStudent, Boolean activeIndex, Byte[] imageStudent)
        {
            this.NameStudent = nameStudent;
            this.ContactStudent = contactStudent;
            this.FatherNameStudent = fatherNameStudent;
            this.DobStudent = dobStudent;
            this.CnicStudent = cnicStudent;
            this.RequiredDarjaStudent = requiredDarjaStudent;
            this.ImageStudent = imageStudent;
        }

    }
}
