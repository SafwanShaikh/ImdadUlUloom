using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImdadUlUloom
{
    public partial class DakhlaCardReport : Form
    {
        private Student student;

        public DakhlaCardReport()
        {
            InitializeComponent();
        }

        internal Student Student
        {
            get
            {
                return student;
            }

            set
            {
                student = value;
            }
        }

        private void DakhlaCardReport_Load(object sender, EventArgs e)
        {
            DakhlaCard rpt = new DakhlaCard();
            TextObject text;

            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["nameStudent"];
            text.Text = student.StudentBasicInfo.NameStudent.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["fnameStudent"];
            text.Text = student.StudentBasicInfo.FatherNameStudent.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["requiredDarja"];
            text.Text = student.StudentBasicInfo.RequiredDarjaStudent.ToString();

            //rpt.SetParameterValue("imageUrl", student.StudentBasicInfo.PictureStudent);
            
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["dakhlaDate"];
            text.Text = student.StudentFormDate.TakmeelDakhlaDate.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["dakhlaNumber"];
            text.Text = student.StudentDakhlaNumber.DakhlaNumber.ToString();

            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["registrationBox"];
            text.Text = student.StudentRegistrationNumber.RegistrationNumber.ToString();

            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["zilaBox"];
            text.Text = student.StudentPermanentAddress.Zila.Equals("") ? "-" : student.StudentPermanentAddress.Zila;
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["tehseelBox"];
            text.Text = student.StudentPermanentAddress.Tehseel.Equals("") ? "-" : student.StudentPermanentAddress.Tehseel;
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["dakKhanaBox"];
            text.Text = student.StudentPermanentAddress.DakKhana.Equals("") ? "-" : student.StudentPermanentAddress.DakKhana;
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["villageBox"];
            text.Text = student.StudentPermanentAddress.Village.Equals("") ? "-" : student.StudentPermanentAddress.Village;

            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["houseBox"];
            text.Text = student.StudentKarachiAddress.HouseNumber.Equals("") ? "-" : student.StudentKarachiAddress.HouseNumber;
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["blockBox"];
            text.Text = student.StudentKarachiAddress.BlockNumber.Equals("") ? "-" : student.StudentKarachiAddress.BlockNumber;
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["sectorBox"];
            text.Text = student.StudentKarachiAddress.SectorNumber.Equals("") ? "-" : student.StudentKarachiAddress.SectorNumber;
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["areaBox"];
            text.Text = student.StudentKarachiAddress.Area.Equals("") ? "-" : student.StudentKarachiAddress.Area;

            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["ijraDate"];
            text.Text = student.StudentFormDate.TakmeelDakhlaDate;
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["mansookhDate"];
            //text.Text = 
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["rihaish"];
            text.Text = (student.StudentRihaish.Rihaish == true) ? "رہائشی" : "غیر رہائشی";

            crystalReportViewer1.ReportSource = rpt;
        }
    }
}
