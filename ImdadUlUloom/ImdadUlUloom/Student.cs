using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdadUlUloom
{
    class Student
    {
        private StudentDakhlaNumber studentDakhlaNumber;
        private StudentRegistrationNumber studentRegistrationNumber;
        private StudentBasicInfo studentBasicInfo;
        private StudentFormDate studentFormDate;
        private StudentGuardianInfo studentGuardianInfo;
        private StudentKarachiAddress studentKarachiAddress;
        private StudentPermanentAddress studentPermanentAddress;
        private StudentQawaif studentQawaif;
        private StudentRihaish studentRihaish;
        
        public Student() {
            StudentBasicInfo = new StudentBasicInfo();
            StudentFormDate = new StudentFormDate();
            StudentGuardianInfo = new StudentGuardianInfo();
            StudentKarachiAddress = new StudentKarachiAddress();
            StudentPermanentAddress = new StudentPermanentAddress();
            StudentQawaif = new StudentQawaif();
            StudentRihaish = new StudentRihaish();
            StudentDakhlaNumber = new StudentDakhlaNumber();
            StudentRegistrationNumber = new StudentRegistrationNumber();
        }

        public Student(StudentBasicInfo studentBasicInfo, StudentFormDate studentFormDate, StudentGuardianInfo studentGuardianInfo, StudentKarachiAddress studentKarachiAddress, StudentPermanentAddress studentPermanentAddress, StudentQawaif studentQawaif, StudentRihaish studentRihaish, StudentDakhlaNumber studentDakhlaNumber)
        {
            this.StudentBasicInfo = studentBasicInfo;
            this.StudentFormDate = studentFormDate;
            this.StudentGuardianInfo = studentGuardianInfo;
            this.StudentKarachiAddress = studentKarachiAddress;
            this.StudentPermanentAddress = studentPermanentAddress;
            this.StudentQawaif = studentQawaif;
            this.StudentRihaish = studentRihaish;
            this.StudentDakhlaNumber = studentDakhlaNumber;
        }

        internal StudentBasicInfo StudentBasicInfo
        {
            get
            {
                return studentBasicInfo;
            }

            set
            {
                studentBasicInfo = value;
            }
        }

        internal StudentFormDate StudentFormDate
        {
            get
            {
                return studentFormDate;
            }

            set
            {
                studentFormDate = value;
            }
        }

        internal StudentGuardianInfo StudentGuardianInfo
        {
            get
            {
                return studentGuardianInfo;
            }

            set
            {
                studentGuardianInfo = value;
            }
        }

        internal StudentKarachiAddress StudentKarachiAddress
        {
            get
            {
                return studentKarachiAddress;
            }

            set
            {
                studentKarachiAddress = value;
            }
        }

        internal StudentPermanentAddress StudentPermanentAddress
        {
            get
            {
                return studentPermanentAddress;
            }

            set
            {
                studentPermanentAddress = value;
            }
        }

        internal StudentQawaif StudentQawaif
        {
            get
            {
                return studentQawaif;
            }

            set
            {
                studentQawaif = value;
            }
        }

        internal StudentRihaish StudentRihaish
        {
            get
            {
                return studentRihaish;
            }

            set
            {
                studentRihaish = value;
            }
        }

        internal StudentDakhlaNumber StudentDakhlaNumber
        {
            get
            {
                return studentDakhlaNumber;
            }

            set
            {
                studentDakhlaNumber = value;
            }
        }

        internal StudentRegistrationNumber StudentRegistrationNumber
        {
            get
            {
                return studentRegistrationNumber;
            }

            set
            {
                studentRegistrationNumber = value;
            }
        }
    }
}
