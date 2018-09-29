using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdadUlUloom
{
    class StudentKarachiAddress
    {
        private String houseNumber;
        private String sectorNumber;
        private String blockNumber;
        private String area;

        public StudentKarachiAddress() { }

        public String HouseNumber
        {
            get
            {
                return houseNumber;
            }

            set
            {
                houseNumber = value;
            }
        }

        public string SectorNumber
        {
            get
            {
                return sectorNumber;
            }

            set
            {
                sectorNumber = value;
            }
        }

        public String BlockNumber
        {
            get
            {
                return blockNumber;
            }

            set
            {
                blockNumber = value;
            }
        }

        public string Area
        {
            get
            {
                return area;
            }

            set
            {
                area = value;
            }
        }

        public StudentKarachiAddress(string houseNumber, string sectorNumber, string blockNumber, string area)
        {
            this.HouseNumber = houseNumber;
            this.SectorNumber = sectorNumber;
            this.BlockNumber = blockNumber;
            this.Area = area;
        }
    }
}
