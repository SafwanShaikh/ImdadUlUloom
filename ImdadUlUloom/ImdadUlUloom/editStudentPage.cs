using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImdadUlUloom
{
    public partial class editStudentPage : Form
    {
        private Student updateStudent;

        public editStudentPage()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        internal Student UpdateStudent
        {
            get
            {
                return updateStudent;
            }

            set
            {
                updateStudent = value;
            }
        }

        private void editStudentPage_Load(object sender, EventArgs e)
        {
            try
            {
                dakhlaNumberLabel.Text = UpdateStudent.StudentDakhlaNumber.DakhlaNumber.ToString();
                registrationNumberLabel.Text = UpdateStudent.StudentRegistrationNumber.RegistrationNumber.ToString();
                formNumberLabel.Text = UpdateStudent.StudentDakhlaNumber.FormNumber.ToString();
                nameStudentTextbox.Text = UpdateStudent.StudentBasicInfo.NameStudent;
                fatherNameStudentTextbox.Text = UpdateStudent.StudentBasicInfo.FatherNameStudent;
                dobStudentDatetimepicker.Value = UpdateStudent.StudentBasicInfo.DobStudent;
                contactStudentTextbox.Text = UpdateStudent.StudentBasicInfo.ContactStudent;
                imageLocationLabel.Text = "Location";
                try
                {
                    if(UpdateStudent.StudentBasicInfo.ImageStudent != null)
                    {
                        MemoryStream ms = new MemoryStream(UpdateStudent.StudentBasicInfo.ImageStudent);
                        pictureStudentPicturebox.Image = Image.FromStream(ms);
                        imageLocationLabel.Text = "Yes";
                    }
                    else
                    {
                        MessageBox.Show("تصویر موجود نہیں ہے۔");
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                cnicStudentTextbox.Text = UpdateStudent.StudentBasicInfo.CnicStudent;
                requiredDarjaStudentCombobox.Text = UpdateStudent.StudentBasicInfo.RequiredDarjaStudent;

                if(!UpdateStudent.StudentFormDate.TakmeelDakhlaDate.Equals(""))
                {
                    String[] takmeelDate = UpdateStudent.StudentFormDate.TakmeelDakhlaDate.Split('-');
                    takmeelDateCombobox.Text = takmeelDate[2];
                    takmeelMonthCombobox.Text = takmeelDate[1];
                    takmeelYearCombobox.Text = takmeelDate[0];
                }
                else
                {
                    takmeelDateCombobox.Text = "";
                    takmeelMonthCombobox.Text = "";
                    takmeelYearCombobox.Text = "";
                }

                if(!UpdateStudent.StudentFormDate.IkhrajDate.Equals(""))
                {
                    String[] ikhrajDate = UpdateStudent.StudentFormDate.IkhrajDate.Split('-');
                    ikhrajDateCombobox.Text = ikhrajDate[2];
                    ikhrajMonthCombobox.Text = ikhrajDate[1];
                    ikhrajYearCombobox.Text = ikhrajDate[0];
                }
                else
                {
                    ikhrajDateCombobox.Text = "";
                    ikhrajMonthCombobox.Text = "";
                    ikhrajYearCombobox.Text = "";
                }
                
                zilaCombobox.Text = UpdateStudent.StudentPermanentAddress.Zila;
                tehseelCombobox.Text = UpdateStudent.StudentPermanentAddress.Tehseel;
                dakKhanaCombobox.Text = UpdateStudent.StudentPermanentAddress.DakKhana;
                villageCombobox.Text = UpdateStudent.StudentPermanentAddress.Village;

                houseNumberTextbox.Text = UpdateStudent.StudentKarachiAddress.HouseNumber.ToString();
                blockNumberTextbox.Text = UpdateStudent.StudentKarachiAddress.BlockNumber.ToString();
                sectorNumberCombobox.Text = UpdateStudent.StudentKarachiAddress.SectorNumber;
                areaCombobox.Text = UpdateStudent.StudentKarachiAddress.Area;

                nameGuardianTextbox.Text = UpdateStudent.StudentGuardianInfo.NameGuardian;
                contactGuardianTextbox.Text = UpdateStudent.StudentGuardianInfo.ContactGuardian;
                relationGuardianTextbox.Text = UpdateStudent.StudentGuardianInfo.RelationGuardian;
                cnicGuardianTextbox.Text = UpdateStudent.StudentGuardianInfo.CnicGuardian;

                imdadiCheckbox.Checked = UpdateStudent.StudentRihaish.Imdadi;
                rihaishiCheckbox.Checked = UpdateStudent.StudentRihaish.Rihaish;

                hifzCheckbox.Checked = UpdateStudent.StudentQawaif.Hifz;
                naziraCheckbox.Checked = UpdateStudent.StudentQawaif.Nazira;
                darseNizamiCombobox.Text = UpdateStudent.StudentQawaif.DarseNizami;
                lastIdaraTextbox.Text = UpdateStudent.StudentQawaif.LastIdara;
                asriTaleemCombobox.Text = UpdateStudent.StudentQawaif.AsriTaleem;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            LandingPage newPage = new LandingPage();
            newPage.Show();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(nameStudentTextbox.Text) || String.IsNullOrEmpty(fatherNameStudentTextbox.Text) ||
                String.IsNullOrEmpty(requiredDarjaStudentCombobox.Text) || String.IsNullOrEmpty(takmeelDateCombobox.Text) ||
                String.IsNullOrEmpty(takmeelMonthCombobox.Text) || String.IsNullOrEmpty(takmeelYearCombobox.Text))
            {
                if (String.IsNullOrEmpty(nameStudentTextbox.Text))
                {
                    errorProvider.SetError(nameStudentTextbox, "طالبعلم کا نام ضروری ہے۔");
                }

                if (String.IsNullOrEmpty(fatherNameStudentTextbox.Text))
                {
                    errorProvider.SetError(fatherNameStudentTextbox, "والد کا نام ضروری ہے۔");
                }

                if (String.IsNullOrEmpty(requiredDarjaStudentCombobox.Text))
                {
                    errorProvider.SetError(requiredDarjaStudentCombobox, "درجہ ضروری ہے۔");
                }

                if (String.IsNullOrEmpty(takmeelDateCombobox.Text) || String.IsNullOrEmpty(takmeelMonthCombobox.Text) ||
                    String.IsNullOrEmpty(takmeelYearCombobox.Text))
                {
                    errorProvider.SetError(takmeelDateCombobox, "تاریخ تکمیل ضروری ہے۔");
                }
            }
            else
            {
                try
                {
                    Student newStudent = getStudentFromForm();
                    DataManipulation.editStudent(newStudent);
                    LandingPage newPage = new LandingPage();
                    newPage.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            
            
        }

        private Student getStudentFromForm()
        {
            Student newStudent = new Student();
            try
            {
                newStudent.StudentRegistrationNumber.RegistrationNumber = Convert.ToInt32(registrationNumberLabel.Text);

                newStudent.StudentDakhlaNumber.DakhlaNumber = Convert.ToInt32(dakhlaNumberLabel.Text);
                newStudent.StudentDakhlaNumber.FormNumber = Convert.ToInt32(formNumberLabel.Text);
                newStudent.StudentDakhlaNumber.ActiveIndex = true;
                newStudent.StudentDakhlaNumber.Darja = requiredDarjaStudentCombobox.Text;
                newStudent.StudentDakhlaNumber.DarjaYear = takmeelYearCombobox.Text;

                newStudent.StudentBasicInfo.NameStudent = nameStudentTextbox.Text;
                newStudent.StudentBasicInfo.ContactStudent = contactStudentTextbox.Text;
                newStudent.StudentBasicInfo.FatherNameStudent = fatherNameStudentTextbox.Text;
                newStudent.StudentBasicInfo.DobStudent = dobStudentDatetimepicker.Value;
                newStudent.StudentBasicInfo.CnicStudent = cnicStudentTextbox.Text;
                newStudent.StudentBasicInfo.RequiredDarjaStudent = requiredDarjaStudentCombobox.Text;
                /*if (!imageLocationLabel.Text.Equals("Location") || !imageLocationLabel.Text.Equals("Yes"))
                {
                    FileStream fs = new FileStream(imageLocationLabel.Text, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    newStudent.StudentBasicInfo.ImageStudent = br.ReadBytes((int)fs.Length);
                }
                else
                {
                    newStudent.StudentBasicInfo.ImageStudent = UpdateStudent.StudentBasicInfo.ImageStudent;
                }*/

                if(pictureStudentPicturebox.Image != null)
                {
                    Image imageIn = pictureStudentPicturebox.Image;
                    MemoryStream ms = new MemoryStream();
                    imageIn.Save(ms, imageIn.RawFormat);
                    newStudent.StudentBasicInfo.ImageStudent = ms.ToArray();
                }
                else
                {
                    newStudent.StudentBasicInfo.ImageStudent =null;
                }
                
                String takmeelDate = takmeelYearCombobox.Text + "-" + takmeelMonthCombobox.Text + "-" + takmeelDateCombobox.Text;
                String ikhrajDate = ikhrajYearCombobox.Text + "-" + ikhrajMonthCombobox.Text + "-" + ikhrajDateCombobox.Text;

                newStudent.StudentFormDate.IkhrajDate = ikhrajDate;
                newStudent.StudentFormDate.TakmeelDakhlaDate = takmeelDate;

                newStudent.StudentGuardianInfo.NameGuardian = nameGuardianTextbox.Text;
                newStudent.StudentGuardianInfo.RelationGuardian = relationGuardianTextbox.Text;
                newStudent.StudentGuardianInfo.CnicGuardian = cnicGuardianTextbox.Text;
                newStudent.StudentGuardianInfo.ContactGuardian = contactGuardianTextbox.Text;

                newStudent.StudentPermanentAddress.Zila = zilaCombobox.Text;
                newStudent.StudentPermanentAddress.Tehseel = tehseelCombobox.Text;
                newStudent.StudentPermanentAddress.DakKhana = dakKhanaCombobox.Text;
                newStudent.StudentPermanentAddress.Village = villageCombobox.Text;

                newStudent.StudentKarachiAddress.HouseNumber = houseNumberTextbox.Text;
                newStudent.StudentKarachiAddress.SectorNumber = sectorNumberCombobox.Text;
                newStudent.StudentKarachiAddress.BlockNumber = blockNumberTextbox.Text;
                newStudent.StudentKarachiAddress.Area = areaCombobox.Text;

                newStudent.StudentRihaish.Imdadi = imdadiCheckbox.Checked;
                newStudent.StudentRihaish.Rihaish = rihaishiCheckbox.Checked;

                newStudent.StudentQawaif.Hifz = hifzCheckbox.Checked;
                newStudent.StudentQawaif.Nazira = naziraCheckbox.Checked;
                newStudent.StudentQawaif.LastIdara = lastIdaraTextbox.Text;
                newStudent.StudentQawaif.AsriTaleem = asriTaleemCombobox.GetItemText(asriTaleemCombobox.SelectedItem);
                newStudent.StudentQawaif.DarseNizami = darseNizamiCombobox.GetItemText(darseNizamiCombobox.SelectedItem);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return newStudent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();

            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "JPG Files (*.jpg)| *.jpg";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                imageLocationLabel.Text = fileOpen.FileName.ToString();
                pictureStudentPicturebox.ImageLocation = fileOpen.FileName.ToString();
            }
            
            fileOpen.Dispose();
        }

        private void formExportoPDF_Click(object sender, EventArgs e)
        {
            Student student = getStudentFromForm();
            DakhlaFormReport f2 = new DakhlaFormReport();
            f2.Student = student;
            f2.Show();
            
        }

        private void contactStudentTextbox_TextChanged(object sender, EventArgs e)
        {
            if (contactStudentTextbox.Text.Length > 11)
            {
                MessageBox.Show("موبائل نمبر 11 ڈیجیٹ کا فل کیجئے۔");
                contactStudentTextbox.Text = "";
                contactStudentTextbox.Focus();
            }
        }

        private void cnicStudentTextbox_TextChanged(object sender, EventArgs e)
        {
            if (cnicStudentTextbox.Text.Length > 13)
            {
                MessageBox.Show("شناختی کارڈ نمبر 13 ڈیجیٹ کا فل کیجئے۔");
                cnicStudentTextbox.Text = "";
                cnicStudentTextbox.Focus();
            }
        }

        private void contactGuardianTextbox_TextChanged(object sender, EventArgs e)
        {
            if (contactGuardianTextbox.Text.Length > 11)
            {
                MessageBox.Show("موبائل نمبر 11 ڈیجیٹ کا فل کیجئے۔");
                contactGuardianTextbox.Text = "";
                contactGuardianTextbox.Focus();
            }
        }

        private void cnicGuardianTextbox_TextChanged(object sender, EventArgs e)
        {
            if (cnicGuardianTextbox.Text.Length > 13)
            {
                MessageBox.Show("شناختی کارڈ نمبر 13 ڈیجیٹ کا فل کیجئے۔");
                cnicGuardianTextbox.Text = "";
                cnicGuardianTextbox.Focus();
            }
        }

        private void nameStudentTextbox_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
        }

        private void fatherNameStudentTextbox_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
        }

        private void requiredDarjaStudentCombobox_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
        }

        private void takmeelDateCombobox_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
        }

        private void takmeelMonthCombobox_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
        }

        private void takmeelYearCombobox_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
        }
    }
}