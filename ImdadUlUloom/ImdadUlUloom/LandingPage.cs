using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace ImdadUlUloom
{
    public partial class LandingPage : Form
    {
        private Student[] activeStudentList;
        private Student[] inActiveStudentList;
        private Dictionary<int, Student> sameRegistrationNumberStudentsList = new Dictionary<int, Student>();
        private String fileName;

        public LandingPage()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            activeStudentList = DataManipulation.getAllStudents(true).ToArray();
            inActiveStudentList = DataManipulation.getAllStudents(false).ToArray();
            fileName = null;
            editStudentButton.Enabled = false;
            deleteStudentButton.Enabled = false;
            printCardButton.Enabled = false;
            exportGridView.Enabled = false;
            Cursor.Current = Cursors.Default;
        }

        private void mainTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(mainTab.SelectedTab == mainTab.TabPages["addNewStudentTab"])
            {
                Cursor.Current = Cursors.WaitCursor;
                fillZilaComboBox();
                fillTehseelComboBox();
                fillDakKhanaComboBox();
                fillVillageComboBox();
                fillAreaComboBox();
                fillSectorComboBox();
                DataManipulation.getNewRegistration_FormAndDakhlaNumber();
                formNumberLabel.Text = StudentRegAndDakhlaNumber.FormNumber.ToString();
                registrationNumberLabel.Text = StudentRegAndDakhlaNumber.RegistrationNumber.ToString();
                dakhlaNumberLabel.Text = StudentRegAndDakhlaNumber.DakhlaNumber.ToString();
                Cursor.Current = Cursors.Default;
            }
            else if(mainTab.SelectedTab == mainTab.TabPages["addPreviousStudentTab"])
            {
                Cursor.Current = Cursors.WaitCursor;
                DataManipulation.getNewDakhlaNumber();
                DataManipulation.getNewFormNumber();
                newFormNumberLabel.Text = StudentRegAndDakhlaNumber.FormNumber.ToString();
                newDakhlaNumberLabel.Text = StudentRegAndDakhlaNumber.DakhlaNumber.ToString();
                Cursor.Current = Cursors.Default;
            }
        }

        private void LandingPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        //      Save Student Tab
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(nameStudentTextbox.Text) || String.IsNullOrEmpty(fatherNameStudentTextbox.Text) ||
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
                    Cursor.Current = Cursors.WaitCursor;
                    Student newStudent = getStudentFromForm();
                    DataManipulation.addStudent(newStudent, false);

                    DakhlaCardReport rpt = new DakhlaCardReport();
                    rpt.Student = newStudent;

                    LandingPage newPage = new LandingPage();
                    this.Hide();
                    newPage.Show();
                    rpt.Show();
                    Cursor.Current = Cursors.Default;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    Cursor.Current = Cursors.Default;
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
                if(!imageLocationLabel.Text.Equals("Location"))
                {
                    FileStream fs = new FileStream(imageLocationLabel.Text, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    newStudent.StudentBasicInfo.ImageStudent = br.ReadBytes((int)fs.Length);
                }
                else
                {
                    newStudent.StudentBasicInfo.ImageStudent = null;
                }

                String takmeelDate = takmeelYearCombobox.Text+"-"+takmeelMonthCombobox.Text+"-"+takmeelDateCombobox.Text;
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
                newStudent.StudentRihaish.Rihaish = rihaishCheckbox.Checked;

                newStudent.StudentQawaif.Hifz = hifzCheckbox.Checked;
                newStudent.StudentQawaif.Nazira = naziraCheckbox.Checked;
                newStudent.StudentQawaif.LastIdara = lastIdaraTextbox.Text;
                newStudent.StudentQawaif.AsriTaleem = asriTaleemCombobox.GetItemText(asriTaleemCombobox.SelectedItem);
                newStudent.StudentQawaif.DarseNizami = darseNizamiCombobox.GetItemText(darseNizamiCombobox.SelectedItem);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return newStudent;
        }

        private void uploadButton_Click(object sender, EventArgs e)
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
        
        private void fillZilaComboBox()
        {
            List<string> zilaAll = new List<string>();
            for (int i = 0; i < activeStudentList.Length; i++)
            {
                zilaAll.Add(activeStudentList[i].StudentPermanentAddress.Zila);
            }
            for (int i = 0; i < inActiveStudentList.Length; i++)
            {
                zilaAll.Add(inActiveStudentList[i].StudentPermanentAddress.Zila);
            }
            var zilaDistinct = zilaAll.Distinct().ToArray();
            foreach (var item in zilaDistinct)
            {
                zilaCombobox.Items.Add(item);
            }
        }

        private void fillTehseelComboBox()
        {
            List<string> TehseelAll = new List<string>();
            for (int i = 0; i < activeStudentList.Length; i++)
            {
                TehseelAll.Add(activeStudentList[i].StudentPermanentAddress.Tehseel);
            }
            for (int i = 0; i < inActiveStudentList.Length; i++)
            {
                TehseelAll.Add(inActiveStudentList[i].StudentPermanentAddress.Tehseel);
            }
            var TehseelDistinct = TehseelAll.Distinct().ToArray();
            foreach (var item in TehseelDistinct)
            {
                tehseelCombobox.Items.Add(item);
            }
        }

        private void fillDakKhanaComboBox()
        {
            List<string> dakKhanaAll = new List<string>();
            for (int i = 0; i < activeStudentList.Length; i++)
            {
                dakKhanaAll.Add(activeStudentList[i].StudentPermanentAddress.DakKhana);
            }
            for (int i = 0; i < inActiveStudentList.Length; i++)
            {
                dakKhanaAll.Add(inActiveStudentList[i].StudentPermanentAddress.DakKhana);
            }
            var dakKhanaDistinct = dakKhanaAll.Distinct().ToArray();
            foreach (var item in dakKhanaDistinct)
            {
                dakKhanaCombobox.Items.Add(item);
            }
        }

        private void fillVillageComboBox()
        {
            List<string> villageAll = new List<string>();
            for (int i = 0; i < activeStudentList.Length; i++)
            {
                villageAll.Add(activeStudentList[i].StudentPermanentAddress.Village);
            }
            for (int i = 0; i < inActiveStudentList.Length; i++)
            {
                villageAll.Add(inActiveStudentList[i].StudentPermanentAddress.Village);
            }
            var villageDistinct = villageAll.Distinct().ToArray();
            foreach (var item in villageDistinct)
            {
                villageCombobox.Items.Add(item);
            }
        }

        private void fillSectorComboBox()
        {
            List<string> sectorAll = new List<string>();
            for (int i = 0; i < activeStudentList.Length; i++)
            {
                sectorAll.Add(activeStudentList[i].StudentKarachiAddress.SectorNumber);
            }
            for (int i = 0; i < inActiveStudentList.Length; i++)
            {
                sectorAll.Add(inActiveStudentList[i].StudentKarachiAddress.SectorNumber);
            }
            var sectorDistinct = sectorAll.Distinct().ToArray();
            foreach (var item in sectorDistinct)
            {
                sectorNumberCombobox.Items.Add(item);
            }
        }

        private void fillAreaComboBox()
        {
            List<string> areaAll = new List<string>();
            for (int i = 0; i < activeStudentList.Length; i++)
            {
                areaAll.Add(activeStudentList[i].StudentKarachiAddress.Area);
            }
            for (int i = 0; i < inActiveStudentList.Length; i++)
            {
                areaAll.Add(inActiveStudentList[i].StudentKarachiAddress.Area);
            }
            var areaDistinct = areaAll.Distinct().ToArray();
            foreach (var item in areaDistinct)
            {
                areaCombobox.Items.Add(item);
            }
        }

        //      Search Student Tab

        private void searchStudentButton_Click(object sender, EventArgs e)
        {
            editStudentButton.Enabled = true;
            deleteStudentButton.Enabled = true;
            printCardButton.Enabled = true;
            exportGridView.Enabled = true;
            try
            {
                string search = searchTextbox.Text;
                List<int> searchDakhlaNumbers = new List<int>();
                studentGridView.Rows.Clear();
                String classOfStudent = getClassOfStudent();

                if (searchCombobox.Text.Equals("داخلہ نمبر"))
                {
                    searchDakhlaNumbers.Add(Convert.ToInt32(search));
                    if(!previousStudent.Checked)
                    {
                        fillGridView(searchDakhlaNumbers, true);
                    }
                    else
                    {
                        fillGridView(searchDakhlaNumbers, false);
                    }
                    
                }
                else if (searchCombobox.Text.Equals("نام"))
                {
                    searchDakhlaNumbers = getDakhlaNumbersOfName(search, classOfStudent);
                    if(!previousStudent.Checked)
                    {
                        fillGridView(searchDakhlaNumbers, true);
                    }
                    else
                    {
                        fillGridView(searchDakhlaNumbers, false);
                    }
                    
                }
                else if (searchCombobox.Text.Equals("شناختی کارڈ نمبر"))
                {
                    searchDakhlaNumbers = getDakhlaNumbersOfCNIC(search);
                    if (!previousStudent.Checked)
                    {
                        fillGridView(searchDakhlaNumbers, true);
                    }
                    else
                    {
                        fillGridView(searchDakhlaNumbers, false);
                    }
                }
                else if (searchCombobox.Text.Equals("رہائشی") || searchCombobox.Text.Equals("غیر رہائشی"))
                {
                    if (searchCombobox.Text.Equals("رہائشی"))
                    {
                        searchDakhlaNumbers = getDakhlaNumbersOfRihaishi(true, classOfStudent);
                        fileName = "رہائشی.pdf";
                    }
                    else if(searchCombobox.Text.Equals("غیر رہائشی"))
                    {
                        searchDakhlaNumbers = getDakhlaNumbersOfRihaishi(false, classOfStudent);
                        fileName = "غیر رہائشی.pdf";
                    }

                    if (!previousStudent.Checked)
                    {
                        fillGridView(searchDakhlaNumbers, true);
                    }
                    else
                    {
                        fillGridView(searchDakhlaNumbers, false);
                    }
                }
                else if (searchCombobox.Text.Equals("سال"))
                {
                    searchDakhlaNumbers = getDakhlaNumbersOfYear(search);
                    fileName =  "سال.pdf";
                    if (!previousStudent.Checked)
                    {
                        fillGridView(searchDakhlaNumbers, true);
                    }
                    else
                    {
                        fillGridView(searchDakhlaNumbers, false);
                    }
                }
                else if (searchCombobox.SelectedItem == null && String.IsNullOrEmpty(classOfStudent))
                {
                    searchDakhlaNumbers = getDakhlaNumbersOfAllStudents();
                    if (!previousStudent.Checked)
                    {
                        fillGridView(searchDakhlaNumbers, true);
                    }
                    else
                    {
                        fillGridView(searchDakhlaNumbers, false);
                    }
                    fileName = "تمام طلبہ.pdf";
                }
                else if(!String.IsNullOrEmpty(classOfStudent))
                {
                    searchDakhlaNumbers = getDakhlaNumbersOfStudentsByClass(classOfStudent);
                    if (!previousStudent.Checked)
                    {
                        fillGridView(searchDakhlaNumbers, true);
                    }
                    else
                    {
                        fillGridView(searchDakhlaNumbers, false);
                    }
                    fileName = classOfStudent + ".pdf";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("درست ڈیٹا درج کیجئے۔");
            }
        }

        private void fillGridView(List<int> dakhlaNumber, Boolean active)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                List<Student> tempStudent = new List<Student>();
                if(active)
                {
                    for (int i = 0; i < dakhlaNumber.Count; i++)
                    {
                        for (int j = 0; j < activeStudentList.Length; j++)
                        {
                            if (dakhlaNumber[i] == activeStudentList[j].StudentDakhlaNumber.DakhlaNumber)
                            {
                                tempStudent.Add(activeStudentList[j]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dakhlaNumber.Count; i++)
                    {
                        for (int j = 0; j < inActiveStudentList.Length; j++)
                        {
                            if (dakhlaNumber[i] == inActiveStudentList[j].StudentDakhlaNumber.DakhlaNumber)
                            {
                                tempStudent.Add(inActiveStudentList[j]);
                            }
                        }
                    }
                }
                

                studentGridView.Rows.Add(tempStudent.Count);

                for (int row = 0; row < tempStudent.Count; row++)
                {
                    Student fillStudent = tempStudent[row];
                    studentGridView.Rows[row].Cells[1].Value = fillStudent.StudentDakhlaNumber.DakhlaNumber;
                    studentGridView.Rows[row].Cells[2].Value = fillStudent.StudentRegistrationNumber.RegistrationNumber.ToString();
                    studentGridView.Rows[row].Cells[3].Value = fillStudent.StudentBasicInfo.NameStudent.ToString();
                    studentGridView.Rows[row].Cells[4].Value = fillStudent.StudentBasicInfo.FatherNameStudent.ToString();
                    studentGridView.Rows[row].Cells[5].Value = fillStudent.StudentBasicInfo.CnicStudent.ToString();
                    studentGridView.Rows[row].Cells[6].Value = fillStudent.StudentBasicInfo.RequiredDarjaStudent.ToString();
                    studentGridView.Rows[row].Cells[7].Value = fillStudent.StudentFormDate.TakmeelDakhlaDate.ToString();
                    studentGridView.Rows[row].Cells[8].Value = fillStudent.StudentFormDate.IkhrajDate.ToString();
                    studentGridView.Rows[row].Cells[9].Value = fillStudent.StudentPermanentAddress.Village.ToString();
                    studentGridView.Rows[row].Cells[10].Value = fillStudent.StudentKarachiAddress.Area.ToString();
                    studentGridView.Rows[row].Cells[11].Value = fillStudent.StudentRihaish.Imdadi.ToString();
                    studentGridView.Rows[row].Cells[12].Value = fillStudent.StudentRihaish.Rihaish.ToString();
                }
                studentGridView.Sort(studentGridView.Columns["dakhlaNumber"], ListSortDirection.Ascending);

                for (int row = 0; row < studentGridView.RowCount; row++)
                {

                    studentGridView.Rows[row].Cells[0].Value = row + 1;
                }
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                MessageBox.Show("ڈیٹا موجود نہیں۔");
                searchTextbox.Clear();
                Cursor.Current = Cursors.Default;
            }
            
        }

        private String getClassOfStudent()
        {
            string studentClass = "";

            if (class1Radio.Checked == true)
            {
                studentClass = "اعدادیہ اول";
            }
            else if (class2Radio.Checked == true)
            {
                studentClass = "اعدادیہ دوم";
            }
            else if (class3Radio.Checked == true)
            {
                studentClass = "اعدادیہ سوم-متوسطہ";
            }
            else if (class4Radio.Checked == true)
            {
                studentClass = "نھم";
            }
            else if (class5Radio.Checked == true)
            {
                studentClass = "دھم";
            }
            else if (class6Radio.Checked == true)
            {
                studentClass = "اولیٰ";
            }
            else if (class7Radio.Checked == true)
            {
                studentClass = "ثانیہ";
            }
            else if (class8Radio.Checked == true)
            {
                studentClass = "ثالثہ";
            }
            else if (class9Radio.Checked == true)
            {
                studentClass = "رابعہ";
            }
            else if (class10Radio.Checked == true)
            {
                studentClass = "خامسہ";
            }
            else if (class11Radio.Checked == true)
            {
                studentClass = "سادسہ";
            }
            else if (class12Radio.Checked == true)
            {
                studentClass = "سابعہ";
            }
            else if (class13Radio.Checked == true)
            {
                studentClass = "ثامنہ";
            }

            return studentClass;
        }

        private List<int> getDakhlaNumbersOfName(string name, string classOfStudent)
        {
            List<int> dakhlaNumbers = new List<int>();
            if (!previousStudent.Checked)
            {
                for (int i = 0; i < activeStudentList.Length; i++)
                {
                    if (activeStudentList[i].StudentBasicInfo.NameStudent.Contains(name) && activeStudentList[i].StudentBasicInfo.RequiredDarjaStudent.Contains(classOfStudent))
                    {
                        dakhlaNumbers.Add(activeStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                    }
                }
            }
            else
            {
                for (int i = 0; i < inActiveStudentList.Length; i++)
                {
                    if (inActiveStudentList[i].StudentBasicInfo.NameStudent.Contains(name) && inActiveStudentList[i].StudentBasicInfo.RequiredDarjaStudent.Contains(classOfStudent))
                    {
                        dakhlaNumbers.Add(inActiveStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                    }
                }
            }
            
            return dakhlaNumbers;
        }

        private List<int> getDakhlaNumbersOfCNIC(string cnic)
        {
            List<int> dakhlaNumbers = new List<int>();
            if(!previousStudent.Checked)
            {
                for (int i = 0; i < activeStudentList.Length; i++)
                {
                    if (activeStudentList[i].StudentBasicInfo.CnicStudent.Equals(cnic))
                    {
                        dakhlaNumbers.Add(activeStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                    }
                }
            }
            else
            {
                for (int i = 0; i < inActiveStudentList.Length; i++)
                {
                    if (inActiveStudentList[i].StudentBasicInfo.CnicStudent.Equals(cnic))
                    {
                        dakhlaNumbers.Add(inActiveStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                    }
                }
            }
            
            return dakhlaNumbers;
        }

        private List<int> getDakhlaNumbersOfYear(string year)
        {
            List<int> dakhlaNumbers = new List<int>();
            if (!previousStudent.Checked)
            {
                for (int i = 0; i < activeStudentList.Length; i++)
                {
                    string[] date = activeStudentList[i].StudentFormDate.TakmeelDakhlaDate.Split('-');
                    if (date[0].Equals(year))
                    {
                        dakhlaNumbers.Add(activeStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                    }
                }
            }
            else
            {
                for (int i = 0; i < inActiveStudentList.Length; i++)
                {
                    string[] date = activeStudentList[i].StudentFormDate.TakmeelDakhlaDate.Split('-');
                    if (date[2].Equals(year))
                    {
                        dakhlaNumbers.Add(inActiveStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                    }
                }
            }
            return dakhlaNumbers;
        }

        private List<int> getDakhlaNumbersOfRihaishi(Boolean rihaishi, string classOfStudent)
        {
            List<int> regNumbers = new List<int>();
            if(rihaishi == true)
            {
                if(!previousStudent.Checked)
                {
                    for (int i = 0; i < activeStudentList.Length; i++)
                    {
                        if (activeStudentList[i].StudentRihaish.Rihaish == true && activeStudentList[i].StudentBasicInfo.RequiredDarjaStudent.Contains(classOfStudent))
                        {
                            regNumbers.Add(activeStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < inActiveStudentList.Length; i++)
                    {
                        if (inActiveStudentList[i].StudentRihaish.Rihaish == true && inActiveStudentList[i].StudentBasicInfo.RequiredDarjaStudent.Contains(classOfStudent))
                        {
                            regNumbers.Add(inActiveStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                        }
                    }
                }
                
            }
            else if(rihaishi == false)
            {
                if(!previousStudent.Checked)
                {
                    for (int i = 0; i < activeStudentList.Length; i++)
                    {
                        if (activeStudentList[i].StudentRihaish.Rihaish == false && activeStudentList[i].StudentBasicInfo.RequiredDarjaStudent.Contains(classOfStudent))
                        {
                            regNumbers.Add(activeStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < inActiveStudentList.Length; i++)
                    {
                        if (inActiveStudentList[i].StudentRihaish.Rihaish == false && inActiveStudentList[i].StudentBasicInfo.RequiredDarjaStudent.Contains(classOfStudent))
                        {
                            regNumbers.Add(inActiveStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                        }
                    }
                }
                
            }
            return regNumbers;
        }

        private List<int> getDakhlaNumbersOfAllStudents()
        {
            List<int> dakhlaNumbers = new List<int>();
            if(!previousStudent.Checked)
            {
                for (int i = 0; i < activeStudentList.Length; i++)
                {
                    dakhlaNumbers.Add(activeStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                }
            }
            else
            {
                for (int i = 0; i < inActiveStudentList.Length; i++)
                {
                    dakhlaNumbers.Add(inActiveStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                }
            }
            
            return dakhlaNumbers;
        }

        private List<int> getDakhlaNumbersOfStudentsByClass(string classOfStudent)
        {
            List<int> dakhlaNumbers = new List<int>();
            if(!previousStudent.Checked)
            {
                for (int i = 0; i < activeStudentList.Length; i++)
                {
                    if (activeStudentList[i].StudentBasicInfo.RequiredDarjaStudent.Contains(classOfStudent))
                    {
                        dakhlaNumbers.Add(activeStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                    }
                }
            }
            else
            {
                for (int i = 0; i < inActiveStudentList.Length; i++)
                {
                    if (inActiveStudentList[i].StudentBasicInfo.RequiredDarjaStudent.Contains(classOfStudent))
                    {
                        dakhlaNumbers.Add(inActiveStudentList[i].StudentDakhlaNumber.DakhlaNumber);
                    }
                }
            }
            
            return dakhlaNumbers;
        }

        private void editStudentButton_Click(object sender, EventArgs e)
        {
            if(studentGridView.SelectedRows.Count == 1)
            {
                Cursor.Current = Cursors.WaitCursor;
                int dakhlaNumber = Convert.ToInt32(studentGridView.SelectedRows[0].Cells[1].Value);
                foreach (var item in activeStudentList)
                {
                    if(item.StudentDakhlaNumber.DakhlaNumber == dakhlaNumber)
                    {
                        editStudentPage newPage = new editStudentPage();
                        newPage.UpdateStudent = item;
                        this.Hide();
                        this.Dispose();
                        newPage.Show();
                        
                        break;
                    }
                }
                Cursor.Current = Cursors.Default;
                
            }
            else
            {
                MessageBox.Show("ایک طالبعلم کا ڈیٹا سلیکٹ کیجئے۔");
            }
        }

        private void deleteStudentButton_Click(object sender, EventArgs e)
        {
            List<string> dakhlaNumbers = new List<string>();
            foreach(DataGridViewRow row in studentGridView.SelectedRows)
            {
                dakhlaNumbers.Add(row.Cells[1].Value.ToString());
            }
            
            DialogResult confirm = MessageBox.Show("Are you sure, You want to delete students?", "Delete Student", MessageBoxButtons.YesNo);
            if(confirm == DialogResult.Yes)
            {
                DataManipulation.deleteStudent(dakhlaNumbers);
                LandingPage newPage = new LandingPage();
                this.Hide();
                newPage.Show();
                newPage.mainTab.SelectedIndex = 2;
                //searchStudentButton.PerformClick();
            }
                
        }
        
        private void searchCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(searchCombobox.Text.Equals("داخلہ نمبر"))
            {
                searchLabel.Text = "داخلہ نمبر";
                searchLabel.Enabled = true;
                searchTextbox.Enabled = true;
            }
            else if(searchCombobox.Text.Equals("نام"))
            {
                searchLabel.Text = "نام";
                searchLabel.Enabled = true;
                searchTextbox.Enabled = true;
            }
            else if (searchCombobox.Text.Equals("سال"))
            {
                searchLabel.Text = "سال";
                searchLabel.Enabled = true;
                searchTextbox.Enabled = true;
            }
            else if(searchCombobox.Text.Equals("شناختی کارڈ نمبر"))
            {
                searchLabel.Text = "شناختی کارڈ نمبر";
                searchLabel.Enabled = true;
                searchTextbox.Enabled = true;
            }
            else if (searchCombobox.Text.Equals("رہائشی"))
            {
                searchLabel.Enabled = false;
                searchTextbox.Enabled = false;
            }
            else if (searchCombobox.Text.Equals("غیر رہائشی"))
            {
                searchLabel.Enabled = false;
                searchTextbox.Enabled = false;
            }


        }
        
        private void resetCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            class1Radio.Checked = false;
            class2Radio.Checked = false;
            class3Radio.Checked = false;
            class6Radio.Checked = false;
            class7Radio.Checked = false;
            class8Radio.Checked = false;
            class9Radio.Checked = false;
            class10Radio.Checked = false;
            class11Radio.Checked = false;
            class12Radio.Checked = false;
            class13Radio.Checked = false;

        }

        private void exportGridView_Click (object sender, EventArgs e)
        {
            
            string path = @"D:\"+fileName;

            //Original path
            /*
            string path = @"E:\" + fileName;
            */

            /*
            string pathImage = @"D:\Uni\Xtra\Program\DotNet\Imdad-ul-Uloom\logo\logo.png";

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(pathImage);
            logo.ScaleAbsolute(200, 100);
            logo.Alignment = Element.ALIGN_CENTER;
            */
            Document document = new Document(PageSize.A4);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
                
                BaseFont basefont = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", BaseFont.IDENTITY_H, true);

                iTextSharp.text.Font urduFont = new iTextSharp.text.Font(basefont, 8, iTextSharp.text.Font.DEFAULTSIZE);

                var el = new Chunk();
                iTextSharp.text.Font f2 = new iTextSharp.text.Font(basefont, 8);
                el.Font = f2;

                PdfPTable table = new PdfPTable(studentGridView.Columns.Count);
                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

                foreach (DataGridViewColumn item in studentGridView.Columns)
                {
                    var str = studentGridView.Columns[item.Index].HeaderText.ToString();
                    PdfPCell cell = new PdfPCell(new Phrase(10, str, el.Font));
                    cell.NoWrap = false;
                    table.AddCell(cell);
                }

                for (int row = 0; row < studentGridView.Rows.Count; row++)
                {
                    for (int column = 0; column < studentGridView.Columns.Count; column++)
                    {
                        var str = studentGridView.Rows[row].Cells[column].Value.ToString();
                        PdfPCell cell = new PdfPCell(new Phrase(10, str, el.Font));
                        table.AddCell(cell);
                    }
                }
                document.Open();
                //document.Add(logo);
                document.Add(table);
                document.Close();

            }
            catch (DocumentException de)
            {
                //              this.Message = de.Message;
            }
            catch (IOException ioe)
            {
                //                this.Message = ioe.Message;
            }

            // step 5: we close the document
            document.Close();

            MessageBox.Show("Print completed at Location : "+path);
        }

        private void formExportToPDButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Student student = getStudentFromForm();
            DakhlaFormReport f2 = new DakhlaFormReport();
            f2.Student = student;
            f2.Show();
            Cursor.Current = Cursors.Default;
        }

        private void searchDakhlaNumbers_Click(object sender, EventArgs e)
        {
            dakhlaNumbersList.Items.Clear();
            clearLabels();
            sameRegistrationNumberStudentsList.Clear();
            try
            {
                List<String> dakhlaNumbers = DataManipulation.getDakhlaNumbers(regNumTextbox.Text);
                if(dakhlaNumbers.Any())
                {
                    foreach (var item in dakhlaNumbers)
                    {
                        dakhlaNumbersList.Items.Add(item);
                    }

                    foreach (String item in dakhlaNumbers)
                    {
                        foreach (Student student in inActiveStudentList)
                        {
                            if (Convert.ToInt32(item) == student.StudentDakhlaNumber.DakhlaNumber)
                            {
                                sameRegistrationNumberStudentsList.Add(student.StudentDakhlaNumber.DakhlaNumber, student);
                            }
                        }
                    }

                    StudentRegAndDakhlaNumber.RegistrationNumber = Convert.ToInt32(regNumTextbox.Text);
                    newRegistratioNumberLabel.Text = StudentRegAndDakhlaNumber.RegistrationNumber.ToString();


                }
                else
                {
                    MessageBox.Show("سابق طالبعلم نہیں ہے۔");
                }
                
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void dakhlaNumbersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dakhlaNumber = Convert.ToInt32(dakhlaNumbersList.SelectedItem);
            Student item = sameRegistrationNumberStudentsList[dakhlaNumber];
            
            registrationNumberlbl.Text = item.StudentRegistrationNumber.RegistrationNumber.ToString();
            formNumberlbl.Text = item.StudentDakhlaNumber.FormNumber.ToString();
            dakhlaNumberlbl.Text = item.StudentDakhlaNumber.DakhlaNumber.ToString();
            namelbl.Text = item.StudentBasicInfo.NameStudent;
            fNamelbl.Text = item.StudentBasicInfo.FatherNameStudent;
            string dob = item.StudentBasicInfo.DobStudent.Day + "-" + item.StudentBasicInfo.DobStudent.Month + "-" + item.StudentBasicInfo.DobStudent.Year;
            doblbl.Text = dob;
            contactlbl.Text = item.StudentBasicInfo.ContactStudent;
            cniclbl.Text = item.StudentBasicInfo.CnicStudent;
            darjalbl.Text = item.StudentBasicInfo.RequiredDarjaStudent;
            zilalbl.Text = item.StudentPermanentAddress.Zila;
            tehseellbl.Text = item.StudentPermanentAddress.Tehseel;
            dakKhanalbl.Text = item.StudentPermanentAddress.DakKhana;
            villagelbl.Text = item.StudentPermanentAddress.Village;
            houselbl.Text = item.StudentKarachiAddress.HouseNumber;
            blocklbl.Text = item.StudentKarachiAddress.BlockNumber;
            sectorlbl.Text = item.StudentKarachiAddress.SectorNumber;
            ilaqalbl.Text = item.StudentKarachiAddress.Area;
            guardianNamelbl.Text = item.StudentGuardianInfo.NameGuardian;
            guardianContactlbl.Text = item.StudentGuardianInfo.ContactGuardian;
            guardiancniclbl.Text = item.StudentGuardianInfo.CnicGuardian;
            guardianRelatioblbl.Text = item.StudentGuardianInfo.RelationGuardian;
            darseNizamilbl.Text = item.StudentQawaif.DarseNizami;
            asriTaleemlbl.Text = item.StudentQawaif.AsriTaleem;
            lastIdaralbl.Text = item.StudentQawaif.LastIdara;
            hifzChckBox.Checked = item.StudentQawaif.Hifz;
            naziraChkbox.Checked = item.StudentQawaif.Nazira;
            imdadiChckBox.Checked = item.StudentRihaish.Imdadi;
            rihaishChckBox.Checked = item.StudentRihaish.Rihaish;
            takmeelDatelbl.Text = item.StudentFormDate.TakmeelDakhlaDate;
            ikhrajDatelbl.Text = item.StudentFormDate.IkhrajDate;
            try
            {
                if (item.StudentBasicInfo.ImageStudent != null)
                {
                    MemoryStream ms = new MemoryStream(item.StudentBasicInfo.ImageStudent);
                    picBox.Image = System.Drawing.Image.FromStream(ms);
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

        }

        private void clearLabels()
        {
            registrationNumberlbl.Text = "";
            formNumberlbl.Text = "";
            dakhlaNumberlbl.Text = "";
            namelbl.Text = "";
            fNamelbl.Text = "";
            doblbl.Text = "";
            contactlbl.Text = "";
            cniclbl.Text = "";
            darjalbl.Text = "";
            zilalbl.Text = "";
            tehseellbl.Text = "";
            dakKhanalbl.Text = "";
            villagelbl.Text = "";
            houselbl.Text = "";
            blocklbl.Text = "";
            sectorlbl.Text = "";
            ilaqalbl.Text = "";
            guardianNamelbl.Text = "";
            guardianContactlbl.Text = "";
            guardiancniclbl.Text = "";
            guardianRelatioblbl.Text = "";
            darseNizamilbl.Text = "";
            asriTaleemlbl.Text = "";
            lastIdaralbl.Text = "";
            hifzChckBox.Checked = false;
            naziraChkbox.Checked = false;
            imdadiChckBox.Checked = false;
            rihaishChckBox.Checked = false;
            takmeelDatelbl.Text = "";
            ikhrajDatelbl.Text = "";
            picBox.Image = null;

            newRegistratioNumberLabel.Text = "";
            
        }

        private void savePreiousStudentButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(dakhlaNumbersList.SelectedItems.Count == 1)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Student student = sameRegistrationNumberStudentsList[Convert.ToInt32(dakhlaNumbersList.SelectedItem)];
                    student.StudentDakhlaNumber.DakhlaNumber = Convert.ToInt32(newDakhlaNumberLabel.Text);
                    student.StudentDakhlaNumber.FormNumber = Convert.ToInt32(newFormNumberLabel.Text);
                    student.StudentDakhlaNumber.ActiveIndex = true;
                    DataManipulation.addStudent(student, true);

                    DakhlaCardReport rpt = new DakhlaCardReport();
                    rpt.Student = student;
                    
                    LandingPage newPage = new LandingPage();
                    this.Hide();
                    newPage.Show();
                    rpt.Show();
                    Cursor.Current = Cursors.Default;

                }
                else
                {
                    MessageBox.Show("داخلہ نمبر سلیکت کیجئے۔");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void printCardButton_Click(object sender, EventArgs e)
        {
            if (studentGridView.SelectedRows.Count == 1)
            {
                Cursor.Current = Cursors.WaitCursor;
                int dakhlaNumber = Convert.ToInt32(studentGridView.SelectedRows[0].Cells[1].Value);
                foreach (var item in activeStudentList)
                {
                    if (item.StudentDakhlaNumber.DakhlaNumber == dakhlaNumber)
                    {
                        DakhlaCardReport rpt = new DakhlaCardReport();
                        rpt.Student = item;
                        rpt.Show();
                        break;
                    }
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainTab.SelectedIndex = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainTab.SelectedIndex = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainTab.SelectedIndex = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataManipulation.backUpDatabase();
        }

        private void cnicStudentTextbox_TextChanged(object sender, EventArgs e)
        {
            if(cnicStudentTextbox.Text.Length > 13)
            {
                MessageBox.Show("شناختی کارڈ نمبر 13 ڈیجیٹ کا فل کیجئے۔");
                cnicStudentTextbox.Text = "";
                cnicStudentTextbox.Focus();
            }
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

        private void cnicStudentTextbox_Leave(object sender, EventArgs e)
        {
            if(cnicStudentTextbox.Text.Length == 13)
            {
                String cnicCurrent = cnicStudentTextbox.Text;
                Boolean contain = false;
                foreach(Student student in activeStudentList)
                {
                    if (cnicCurrent.Equals(student.StudentBasicInfo.CnicStudent))
                    {
                        contain = true;
                    }
                }
                foreach(Student student in inActiveStudentList)
                {
                    if (cnicCurrent.Equals(student.StudentBasicInfo.CnicStudent))
                    {
                        contain = true;
                    }
                }
                if (contain)
                {
                    DialogResult result = MessageBox.Show("طالبعلم پہلے سے ڈیٹا بیس میں موجود ہے۔ دوبارہ داخلے کیلئے قدیم طالبعلم ٹیب استعمال کیجئے۔", "Duplicate Student", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        cnicStudentTextbox.Text = "";
                        mainTab.SelectedIndex = 3;
                    }
                    else
                    {
                        cnicStudentTextbox.Text = "";
                    }
                }
            }
        }
    }
}