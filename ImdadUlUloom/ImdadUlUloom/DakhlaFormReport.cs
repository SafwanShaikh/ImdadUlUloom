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
    public partial class DakhlaFormReport : Form
    {
        private Student student;

        public DakhlaFormReport()
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

        private void ReportForm_Load(object sender, EventArgs e)
        {
            DakhlaForm rpt = new DakhlaForm();
            TextObject text;
            
            text = (TextObject)rpt.ReportDefinition.Sections["Section2"].ReportObjects["regNumBox"];
            text.Text = student.StudentRegistrationNumber.RegistrationNumber.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section2"].ReportObjects["formNumBox"];
            text.Text = student.StudentDakhlaNumber.FormNumber.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["nameBox"];
            text.Text = student.StudentBasicInfo.NameStudent.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["fnameBox"];
            text.Text = student.StudentBasicInfo.FatherNameStudent.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["dobBox"];
            text.Text = student.StudentBasicInfo.DobStudent.Day.ToString() +"-"+ student.StudentBasicInfo.DobStudent.Month.ToString() + "-" + student.StudentBasicInfo.DobStudent.Year.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["contactSBox"];
            text.Text = student.StudentBasicInfo.ContactStudent.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["cnicSBox"];
            text.Text = student.StudentBasicInfo.CnicStudent.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["requiredDarjaSBox"];
            text.Text = student.StudentBasicInfo.RequiredDarjaStudent.ToString();

            //rpt.SetParameterValue("imgUrl", student.StudentBasicInfo.PictureStudent);
            
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["zilaBox"];
            text.Text = student.StudentPermanentAddress.Zila.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["tehseelBox"];
            text.Text = student.StudentPermanentAddress.Tehseel.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["dakKhanaBox"];
            text.Text = student.StudentPermanentAddress.DakKhana.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["villageBox"];
            text.Text = student.StudentPermanentAddress.Village.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["houseBox"];
            text.Text = student.StudentKarachiAddress.HouseNumber.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["sectorBox"];
            text.Text = student.StudentKarachiAddress.SectorNumber.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["galiBox"];
            text.Text = student.StudentKarachiAddress.BlockNumber.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["areaBox"];
            text.Text = student.StudentKarachiAddress.Area.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["nameGBox"];
            text.Text = student.StudentGuardianInfo.NameGuardian.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["contactGBox"];
            text.Text = student.StudentGuardianInfo.ContactGuardian.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["cnicGBox"];
            text.Text = student.StudentGuardianInfo.CnicGuardian.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["relationGBox"];
            text.Text = student.StudentGuardianInfo.RelationGuardian.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["hifzBox"];
            text.Text = student.StudentQawaif.Hifz == true ? "ہاں" : "نہیں";
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["naziraBox"];
            text.Text = student.StudentQawaif.Nazira == true ? "ہاں" : "نہیں";
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["nizamiBox"];
            text.Text = student.StudentQawaif.DarseNizami.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["idaraBox"];
            text.Text = student.StudentQawaif.LastIdara.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["educationBox"];
            text.Text = student.StudentQawaif.AsriTaleem.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["imdadiBox"];
            text.Text = student.StudentRihaish.Imdadi == true ? "ہاں" : "نہیں";
            text = (TextObject)rpt.ReportDefinition.Sections["Section3"].ReportObjects["rihaishiBox"];
            text.Text = student.StudentRihaish.Rihaish == true ? "ہاں" : "نہیں";
            text = (TextObject)rpt.ReportDefinition.Sections["Section5"].ReportObjects["ikhrajBox"];
            text.Text = student.StudentFormDate.IkhrajDate.ToString();
            text = (TextObject)rpt.ReportDefinition.Sections["Section5"].ReportObjects["takmeelBox"];
            text.Text = student.StudentFormDate.TakmeelDakhlaDate.ToString();
            crystalReportViewer1.ReportSource = rpt;
        }
    }
}