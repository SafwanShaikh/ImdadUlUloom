using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdadUlUloom
{
    class StudentGuardianInfo
    {
        private String nameGuardian;
        private String contactGuardian;
        private String cnicGuardian;
        private String relationGuardian;

        public string NameGuardian
        {
            get
            {
                return nameGuardian;
            }

            set
            {
                nameGuardian = value;
            }
        }

        public string ContactGuardian
        {
            get
            {
                return contactGuardian;
            }

            set
            {
                contactGuardian = value;
            }
        }

        public string CnicGuardian
        {
            get
            {
                return cnicGuardian;
            }

            set
            {
                cnicGuardian = value;
            }
        }

        public string RelationGuardian
        {
            get
            {
                return relationGuardian;
            }

            set
            {
                relationGuardian = value;
            }
        }

        public StudentGuardianInfo() { }

        public StudentGuardianInfo(string nameGuardian, string contactGuardian, string cnicGuardian, string relationGuardian)
        {
            this.NameGuardian = nameGuardian;
            this.ContactGuardian = contactGuardian;
            this.CnicGuardian = cnicGuardian;
            this.RelationGuardian = relationGuardian;
        }
    }
}
