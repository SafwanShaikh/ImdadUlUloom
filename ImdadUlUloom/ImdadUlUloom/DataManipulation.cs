using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImdadUlUloom
{
    class DataManipulation
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        public static SqlConnection sqlConnection;
        public static SqlTransaction sqlTransaction;
        
        public static void getNewRegistration_FormAndDakhlaNumber()
        {
            try
            {
                getNewRegistrationNumber();
                getNewDakhlaNumber();
                getNewFormNumber();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        public static void getNewRegistrationNumber()
        {
            //int regNumber = -1;
            using (sqlConnection = new SqlConnection(connectionString))
            {
                string queryGetRegNumber = "select max(registrationNumber) from StudentRegistrationNumber";
                try
                {
                    sqlConnection.Open();
                    SqlCommand getRegNumberCommand = new SqlCommand(queryGetRegNumber, sqlConnection);
                    SqlDataReader reader1 = getRegNumberCommand.ExecuteReader();
                    while (reader1.Read())
                    {
                        StudentRegAndDakhlaNumber.RegistrationNumber = Convert.ToInt32(reader1[0]) + 1;
                    }
                    reader1.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void getNewFormNumber()
        {
            //int formNumber = -1;
            using (sqlConnection = new SqlConnection(connectionString))
            {
                string queryGetFormNumber = "select max(formNumber) from StudentDakhlaNumber";
                try
                {
                    sqlConnection.Open();
                    SqlCommand getFormNumberCommand = new SqlCommand(queryGetFormNumber, sqlConnection);
                    SqlDataReader reader2 = getFormNumberCommand.ExecuteReader();
                    while (reader2.Read())
                    {
                        StudentRegAndDakhlaNumber.FormNumber = Convert.ToInt32(reader2[0]) + 1;
                    }
                    reader2.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void getNewDakhlaNumber()
        {
            //int dakhlaNumber = -1;
            using (sqlConnection = new SqlConnection(connectionString))
            {
                string queryGetDakhlaNumber = "select max(dakhlaNumber) from StudentDakhlaNumber";
                try
                {
                    sqlConnection.Open();
                    SqlCommand getDakhlaNumberCommand = new SqlCommand(queryGetDakhlaNumber, sqlConnection);
                    SqlDataReader reader2 = getDakhlaNumberCommand.ExecuteReader();
                    
                    while (reader2.Read())
                    {
                        StudentRegAndDakhlaNumber.DakhlaNumber = Convert.ToInt32(reader2[0]) + 1;
                    }
                    reader2.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            //return dakhlaNumber;
        }

        public static void addStudent(Student newStudent, Boolean existing)
        {
            using(sqlConnection = new SqlConnection(connectionString))
            {
                string queryRegNumber = "insert into StudentRegistrationNumber(registrationNumber) values(@registrationNumber)";
                string querydakhlaNumber = "insert into StudentDakhlaNumber(dakhlaNumber, registrationNumber, formNumber, darja, darjaYear, activeIndex) values(@dakhlaNumber, @registrationNumber, @formNumber, @darja, @darjaYear, @activeIndex)";
                string queryBasicInfo = "insert into StudentBasicInfo(dakhlaNumber, nameStudent, contactStudent, fatherNameStudent, dobStudent, cnicStudent, requiredDarjaStudent, imageStudent) values(@dakhlaNumber, @nameStudent, @contactStudent, @fatherNameStudent, @dobStudent, @cnicStudent, @requiredDarjaStudent, @imageStudent)";
                string queryFormDate = "insert into StudentFormDate(dakhlaNumber, takmeelDakhlaDate, ikhrajDate) values(@dakhlaNumber, @takmeelDakhlaDate, @ikhrajDate)";
                string queryGuardianInfo = "insert into StudentGuardianInfo(dakhlaNumber, nameGuardian, contactGuardian, relationGuardian, cnicGuardian) values(@dakhlaNumber, @nameGuardian, @contactGuardian, @relationGuardian, @cnicGuardian)";
                string queryPermanentAddress = "insert into StudentPermanentAddress(dakhlaNumber, zila, tehseel, dakKhana, village) values (@dakhlaNumber, @zila, @tehseel, @dakKhana, @village)";
                string queryKarachiAddress = "insert into StudentKarachiAddress(dakhlaNumber, houseNumber, blockNumber, sectorNumber, area) values(@dakhlaNumber, @houseNumber, @blockNumber, @sectorNumber, @area)";
                string queryQawaif = "insert into StudentQawaif(dakhlaNumber, hifz, nazira, darseNizami, lastIdara, asriTaleem) values(@dakhlaNumber, @hifz, @nazira, @darseNizami, @lastIdara, @asriTaleem)";
                string queryRihaish = "insert into StudentRihaish(dakhlaNumber, imdadi, rihaish) values(@dakhlaNumber, @imdadi, @rihaish)";
                try
                {
                    sqlConnection.Open();
                    sqlTransaction = sqlConnection.BeginTransaction();

                    if (!existing)
                    {
                        SqlCommand insertRegNumber = new SqlCommand(queryRegNumber, sqlConnection, sqlTransaction);
                        insertRegNumber.Parameters.AddWithValue("@registrationNumber", newStudent.StudentRegistrationNumber.RegistrationNumber);
                        insertRegNumber.ExecuteNonQuery();
                    }
                    
                    SqlCommand insertDakhlaNumberCommand = new SqlCommand(querydakhlaNumber, sqlConnection, sqlTransaction);
                    insertDakhlaNumberCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertDakhlaNumberCommand.Parameters.AddWithValue("@registrationNumber", newStudent.StudentRegistrationNumber.RegistrationNumber);
                    insertDakhlaNumberCommand.Parameters.AddWithValue("@formNumber", newStudent.StudentDakhlaNumber.FormNumber);
                    insertDakhlaNumberCommand.Parameters.AddWithValue("@darja", newStudent.StudentDakhlaNumber.Darja);
                    insertDakhlaNumberCommand.Parameters.AddWithValue("@darjaYear", newStudent.StudentDakhlaNumber.DarjaYear);
                    insertDakhlaNumberCommand.Parameters.AddWithValue("@activeIndex", newStudent.StudentDakhlaNumber.ActiveIndex);
                    insertDakhlaNumberCommand.ExecuteNonQuery();
                    
                    SqlCommand insertBasicInfoCommand = new SqlCommand(queryBasicInfo, sqlConnection, sqlTransaction);
                    insertBasicInfoCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertBasicInfoCommand.Parameters.AddWithValue("@nameStudent", newStudent.StudentBasicInfo.NameStudent);
                    insertBasicInfoCommand.Parameters.AddWithValue("@contactStudent", newStudent.StudentBasicInfo.ContactStudent);
                    insertBasicInfoCommand.Parameters.AddWithValue("@fatherNameStudent", newStudent.StudentBasicInfo.FatherNameStudent);
                    insertBasicInfoCommand.Parameters.AddWithValue("@dobStudent", newStudent.StudentBasicInfo.DobStudent);
                    insertBasicInfoCommand.Parameters.AddWithValue("@cnicStudent", newStudent.StudentBasicInfo.CnicStudent);
                    insertBasicInfoCommand.Parameters.AddWithValue("@requiredDarjaStudent", newStudent.StudentBasicInfo.RequiredDarjaStudent);
                    insertBasicInfoCommand.Parameters.Add("@imageStudent", SqlDbType.Image);
                    if (newStudent.StudentBasicInfo.ImageStudent != null)
                    {
                        insertBasicInfoCommand.Parameters["@imageStudent"].Value = newStudent.StudentBasicInfo.ImageStudent;
                    }
                    else
                    {
                        insertBasicInfoCommand.Parameters["@imageStudent"].Value = DBNull.Value;
                    }
                    insertBasicInfoCommand.ExecuteNonQuery();

                    SqlCommand insertFormDateCommand = new SqlCommand(queryFormDate, sqlConnection, sqlTransaction);
                    insertFormDateCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertFormDateCommand.Parameters.AddWithValue("@takmeelDakhlaDate", newStudent.StudentFormDate.TakmeelDakhlaDate);
                    insertFormDateCommand.Parameters.AddWithValue("@ikhrajDate", newStudent.StudentFormDate.IkhrajDate);
                    insertFormDateCommand.ExecuteNonQuery();

                    SqlCommand insertGuardianInfoCommand = new SqlCommand(queryGuardianInfo, sqlConnection, sqlTransaction);
                    insertGuardianInfoCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertGuardianInfoCommand.Parameters.AddWithValue("@nameGuardian", newStudent.StudentGuardianInfo.NameGuardian);
                    insertGuardianInfoCommand.Parameters.AddWithValue("@contactGuardian", newStudent.StudentGuardianInfo.ContactGuardian);
                    insertGuardianInfoCommand.Parameters.AddWithValue("@relationGuardian", newStudent.StudentGuardianInfo.RelationGuardian);
                    insertGuardianInfoCommand.Parameters.AddWithValue("@cnicGuardian", newStudent.StudentGuardianInfo.CnicGuardian);
                    insertGuardianInfoCommand.ExecuteNonQuery();

                    SqlCommand insertKarachiAddressCommand = new SqlCommand(queryKarachiAddress, sqlConnection, sqlTransaction);
                    insertKarachiAddressCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertKarachiAddressCommand.Parameters.AddWithValue("@houseNumber",newStudent.StudentKarachiAddress.HouseNumber);
                    insertKarachiAddressCommand.Parameters.AddWithValue("@blockNumber", newStudent.StudentKarachiAddress.BlockNumber);
                    insertKarachiAddressCommand.Parameters.AddWithValue("@sectorNumber", newStudent.StudentKarachiAddress.SectorNumber);
                    insertKarachiAddressCommand.Parameters.AddWithValue("@area", newStudent.StudentKarachiAddress.Area);
                    insertKarachiAddressCommand.ExecuteNonQuery();

                    SqlCommand insertPermenantAddressCommand = new SqlCommand(queryPermanentAddress, sqlConnection, sqlTransaction);
                    insertPermenantAddressCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertPermenantAddressCommand.Parameters.AddWithValue("@zila", newStudent.StudentPermanentAddress.Zila);
                    insertPermenantAddressCommand.Parameters.AddWithValue("@tehseel", newStudent.StudentPermanentAddress.Tehseel);
                    insertPermenantAddressCommand.Parameters.AddWithValue("@dakKhana", newStudent.StudentPermanentAddress.DakKhana);
                    insertPermenantAddressCommand.Parameters.AddWithValue("@village", newStudent.StudentPermanentAddress.Village);
                    insertPermenantAddressCommand.ExecuteNonQuery();

                    SqlCommand insertQawaifCommand = new SqlCommand(queryQawaif, sqlConnection, sqlTransaction);
                    insertQawaifCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertQawaifCommand.Parameters.AddWithValue("@hifz", newStudent.StudentQawaif.Hifz);
                    insertQawaifCommand.Parameters.AddWithValue("@nazira", newStudent.StudentQawaif.Nazira);
                    insertQawaifCommand.Parameters.AddWithValue("@darseNizami", newStudent.StudentQawaif.DarseNizami);
                    insertQawaifCommand.Parameters.AddWithValue("@lastIdara", newStudent.StudentQawaif.LastIdara);
                    insertQawaifCommand.Parameters.AddWithValue("@asriTaleem", newStudent.StudentQawaif.AsriTaleem);
                    insertQawaifCommand.ExecuteNonQuery();

                    SqlCommand insertRihaishCommand = new SqlCommand(queryRihaish, sqlConnection, sqlTransaction);
                    insertRihaishCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertRihaishCommand.Parameters.AddWithValue("@imdadi", newStudent.StudentRihaish.Imdadi);
                    insertRihaishCommand.Parameters.AddWithValue("@rihaish", newStudent.StudentRihaish.Rihaish);
                    insertRihaishCommand.ExecuteNonQuery();

                    sqlTransaction.Commit();    

                    MessageBox.Show("Data inserted");
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("No rows inserted. Insertion Rollbacked.");
                    sqlTransaction.Rollback();
                }
            }

        }

        public static void editStudent(Student newStudent)
        {
            String[] year = newStudent.StudentFormDate.TakmeelDakhlaDate.Split('-');

            using(sqlConnection = new SqlConnection(connectionString))
            {
                string queryDakhlaNum = "UPDATE StudentDakhlaNumber SET darja = @darja, darjaYear = @darjaYear WHERE dakhlaNumber = @dakhlaNumber";
                string queryBasicInfo = "UPDATE StudentBasicInfo SET nameStudent = @nameStudent, contactStudent = @contactStudent, fatherNameStudent = @fatherNameStudent,imageStudent = @imageStudent, dobStudent = @dobStudent, cnicStudent = @cnicStudent, requiredDarjaStudent = @requiredDarjaStudent WHERE dakhlaNumber = @dakhlaNumber";
                string queryFormDate = "UPDATE StudentFormDate SET takmeelDakhlaDate = @takmeelDakhlaDate, ikhrajDate = @ikhrajDate WHERE dakhlaNumber = @dakhlaNumber";
                string queryGuardianInfo = "UPDATE StudentGuardianInfo SET nameGuardian = @nameGuardian, contactGuardian = @contactGuardian, relationGuardian = @relationGuardian, cnicGuardian = @cnicGuardian WHERE dakhlaNumber = @dakhlaNumber";
                string queryPermanentAddress = "UPDATE StudentPermanentAddress SET zila = @zila, tehseel = @tehseel, dakKhana = @dakKhana, village = @village WHERE dakhlaNumber = @dakhlaNumber";
                string queryKarachiAddress = "UPDATE StudentKarachiAddress SET houseNumber = @houseNumber, blockNumber = @blockNumber, sectorNumber = @sectorNumber, area = @area WHERE dakhlaNumber = @dakhlaNumber";
                string queryQawaif = "UPDATE StudentQawaif SET hifz = @hifz, nazira = @nazira, darseNizami = @darseNizami, lastIdara = @lastIdara, asriTaleem = @asriTaleem WHERE dakhlaNumber = @dakhlaNumber";
                string queryRihaish = "UPDATE StudentRihaish SET imdadi = @imdadi, rihaish = @rihaish WHERE dakhlaNumber = @dakhlaNumber";
                try
                {
                    sqlConnection.Open();

                    sqlTransaction = sqlConnection.BeginTransaction();

                    SqlCommand insertDakhlaNumCommand = new SqlCommand(queryDakhlaNum, sqlConnection, sqlTransaction);
                    insertDakhlaNumCommand.Parameters.AddWithValue("@darja", newStudent.StudentDakhlaNumber.Darja);
                    insertDakhlaNumCommand.Parameters.AddWithValue("@darjaYear", year[0]);
                    insertDakhlaNumCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertDakhlaNumCommand.ExecuteNonQuery();
                    
                    SqlCommand insertBasicInfoCommand = new SqlCommand(queryBasicInfo, sqlConnection, sqlTransaction);
                    insertBasicInfoCommand.Parameters.AddWithValue("@nameStudent", newStudent.StudentBasicInfo.NameStudent);
                    insertBasicInfoCommand.Parameters.AddWithValue("@contactStudent", newStudent.StudentBasicInfo.ContactStudent);
                    insertBasicInfoCommand.Parameters.AddWithValue("@fatherNameStudent", newStudent.StudentBasicInfo.FatherNameStudent);
                    insertBasicInfoCommand.Parameters.Add("@imageStudent", SqlDbType.Image);
                    if(newStudent.StudentBasicInfo.ImageStudent != null)
                    {
                        insertBasicInfoCommand.Parameters["@imageStudent"].Value = newStudent.StudentBasicInfo.ImageStudent;
                    }
                    else
                    {
                        insertBasicInfoCommand.Parameters["@imageStudent"].Value = DBNull.Value;
                    }
                    insertBasicInfoCommand.Parameters.AddWithValue("@dobStudent", newStudent.StudentBasicInfo.DobStudent);
                    insertBasicInfoCommand.Parameters.AddWithValue("@cnicStudent", newStudent.StudentBasicInfo.CnicStudent);
                    insertBasicInfoCommand.Parameters.AddWithValue("@requiredDarjaStudent", newStudent.StudentBasicInfo.RequiredDarjaStudent);
                    insertBasicInfoCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertBasicInfoCommand.ExecuteNonQuery();
                    
                    SqlCommand insertFormDateCommand = new SqlCommand(queryFormDate, sqlConnection, sqlTransaction);
                    insertFormDateCommand.Parameters.AddWithValue("@takmeelDakhlaDate", newStudent.StudentFormDate.TakmeelDakhlaDate);
                    insertFormDateCommand.Parameters.AddWithValue("@ikhrajDate", newStudent.StudentFormDate.IkhrajDate);
                    insertFormDateCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertFormDateCommand.ExecuteNonQuery();

                    SqlCommand insertGuardianInfoCommand = new SqlCommand(queryGuardianInfo, sqlConnection, sqlTransaction);
                    insertGuardianInfoCommand.Parameters.AddWithValue("@nameGuardian", newStudent.StudentGuardianInfo.NameGuardian);
                    insertGuardianInfoCommand.Parameters.AddWithValue("@contactGuardian", newStudent.StudentGuardianInfo.ContactGuardian);
                    insertGuardianInfoCommand.Parameters.AddWithValue("@relationGuardian", newStudent.StudentGuardianInfo.RelationGuardian);
                    insertGuardianInfoCommand.Parameters.AddWithValue("@cnicGuardian", newStudent.StudentGuardianInfo.CnicGuardian);
                    insertGuardianInfoCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertGuardianInfoCommand.ExecuteNonQuery();

                    SqlCommand insertKarachiAddressCommand = new SqlCommand(queryKarachiAddress, sqlConnection, sqlTransaction);
                    insertKarachiAddressCommand.Parameters.AddWithValue("@houseNumber", newStudent.StudentKarachiAddress.HouseNumber);
                    insertKarachiAddressCommand.Parameters.AddWithValue("@blockNumber", newStudent.StudentKarachiAddress.BlockNumber);
                    insertKarachiAddressCommand.Parameters.AddWithValue("@sectorNumber", newStudent.StudentKarachiAddress.SectorNumber);
                    insertKarachiAddressCommand.Parameters.AddWithValue("@area", newStudent.StudentKarachiAddress.Area);
                    insertKarachiAddressCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertKarachiAddressCommand.ExecuteNonQuery();

                    SqlCommand insertPermenantAddressCommand = new SqlCommand(queryPermanentAddress, sqlConnection, sqlTransaction);
                    insertPermenantAddressCommand.Parameters.AddWithValue("@zila", newStudent.StudentPermanentAddress.Zila);
                    insertPermenantAddressCommand.Parameters.AddWithValue("@tehseel", newStudent.StudentPermanentAddress.Tehseel);
                    insertPermenantAddressCommand.Parameters.AddWithValue("@dakKhana", newStudent.StudentPermanentAddress.DakKhana);
                    insertPermenantAddressCommand.Parameters.AddWithValue("@village", newStudent.StudentPermanentAddress.Village);
                    insertPermenantAddressCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertPermenantAddressCommand.ExecuteNonQuery();

                    SqlCommand insertQawaifCommand = new SqlCommand(queryQawaif, sqlConnection, sqlTransaction);
                    insertQawaifCommand.Parameters.AddWithValue("@hifz", newStudent.StudentQawaif.Hifz);
                    insertQawaifCommand.Parameters.AddWithValue("@nazira", newStudent.StudentQawaif.Nazira);
                    insertQawaifCommand.Parameters.AddWithValue("@darseNizami", newStudent.StudentQawaif.DarseNizami);
                    insertQawaifCommand.Parameters.AddWithValue("@lastIdara", newStudent.StudentQawaif.LastIdara);
                    insertQawaifCommand.Parameters.AddWithValue("@asriTaleem", newStudent.StudentQawaif.AsriTaleem);
                    insertQawaifCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertQawaifCommand.ExecuteNonQuery();

                    SqlCommand insertRihaishCommand = new SqlCommand(queryRihaish, sqlConnection, sqlTransaction);
                    insertRihaishCommand.Parameters.AddWithValue("@imdadi", newStudent.StudentRihaish.Imdadi);
                    insertRihaishCommand.Parameters.AddWithValue("@rihaish", newStudent.StudentRihaish.Rihaish);
                    insertRihaishCommand.Parameters.AddWithValue("@dakhlaNumber", newStudent.StudentDakhlaNumber.DakhlaNumber);
                    insertRihaishCommand.ExecuteNonQuery();

                    sqlTransaction.Commit();

                    MessageBox.Show("Data updated");

                    
                }
                catch(Exception ex)
                {
                    sqlTransaction.Rollback();
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("No rows Updated. Transsaction Rollbacked.");

                }
            }
        }
        
        public static List<Student> getAllStudents(Boolean active)
        {
            string getAllActiveQuery;
            if (active)
            {
                getAllActiveQuery = "select * from StudentDakhlaNumber sd, StudentBasicInfo sb, StudentFormDate fd, StudentGuardianInfo sg, StudentKarachiAddress sk, StudentPermanentAddress sp, StudentQawaif sq, StudentRihaish srh where sd.activeIndex = 1 AND (sd.dakhlaNumber = sb.dakhlaNumber AND sd.dakhlaNumber = fd.dakhlaNumber AND sd.dakhlaNumber = sg.dakhlaNumber AND sd.dakhlaNumber = sk.dakhlaNumber AND sd.dakhlaNumber = sp.dakhlaNumber AND sd.dakhlaNumber = sq.dakhlaNumber AND sd.dakhlaNumber = srh.dakhlaNumber)";
            }
            else
            {
                getAllActiveQuery = "select * from StudentDakhlaNumber sd, StudentBasicInfo sb, StudentFormDate fd, StudentGuardianInfo sg, StudentKarachiAddress sk, StudentPermanentAddress sp, StudentQawaif sq, StudentRihaish srh where sd.activeIndex = 0 AND (sd.dakhlaNumber = sb.dakhlaNumber AND sd.dakhlaNumber = fd.dakhlaNumber AND sd.dakhlaNumber = sg.dakhlaNumber AND sd.dakhlaNumber = sk.dakhlaNumber AND sd.dakhlaNumber = sp.dakhlaNumber AND sd.dakhlaNumber = sq.dakhlaNumber AND sd.dakhlaNumber = srh.dakhlaNumber)";
            }
            List<Student> studentList = new List<Student>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand getAllCommand = new SqlCommand(getAllActiveQuery, sqlConnection);
                    var reader = getAllCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Student student = new Student();

                        student.StudentRegistrationNumber.RegistrationNumber = reader.GetInt32(1);

                        student.StudentDakhlaNumber.DakhlaNumber = reader.GetInt32(0);
                        student.StudentDakhlaNumber.FormNumber = reader.GetInt32(2);
                        student.StudentDakhlaNumber.Darja = reader.GetString(3);
                        student.StudentDakhlaNumber.DarjaYear = reader.GetString(4);
                        student.StudentDakhlaNumber.ActiveIndex = reader.GetBoolean(5);
                        
                        student.StudentBasicInfo.NameStudent = reader.GetString(7);
                        student.StudentBasicInfo.ContactStudent = reader.GetString(8);
                        student.StudentBasicInfo.FatherNameStudent = reader.GetString(9);
                        student.StudentBasicInfo.DobStudent = reader.GetDateTime(10).Date;
                        student.StudentBasicInfo.CnicStudent = reader.GetString(11);
                        student.StudentBasicInfo.RequiredDarjaStudent = reader.GetString(12);
                        if(reader[13] != System.DBNull.Value)
                        {
                            student.StudentBasicInfo.ImageStudent = (Byte[])(reader[13]);
                        }
                        else
                        {
                            student.StudentBasicInfo.ImageStudent = null;
                        }
                        
                        student.StudentFormDate.TakmeelDakhlaDate = reader.GetString(15);
                        student.StudentFormDate.IkhrajDate = reader.GetString(16);

                        student.StudentGuardianInfo.NameGuardian = reader.GetString(18);
                        student.StudentGuardianInfo.ContactGuardian = reader.GetString(19);
                        student.StudentGuardianInfo.RelationGuardian = reader.GetString(20);
                        student.StudentGuardianInfo.CnicGuardian = reader.GetString(21);

                        student.StudentKarachiAddress.HouseNumber = reader.GetString(23);
                        student.StudentKarachiAddress.SectorNumber = reader.GetString(24);
                        student.StudentKarachiAddress.BlockNumber = reader.GetString(25);
                        student.StudentKarachiAddress.Area = reader.GetString(26);

                        student.StudentPermanentAddress.Zila = reader.GetString(28);
                        student.StudentPermanentAddress.Tehseel = reader.GetString(29);
                        student.StudentPermanentAddress.DakKhana = reader.GetString(30);
                        student.StudentPermanentAddress.Village = reader.GetString(31);

                        student.StudentQawaif.Hifz = reader.GetBoolean(33);
                        student.StudentQawaif.Nazira = reader.GetBoolean(34);
                        student.StudentQawaif.DarseNizami = reader.GetString(35);
                        student.StudentQawaif.LastIdara = reader.GetString(36);
                        student.StudentQawaif.AsriTaleem = reader.GetString(37);

                        student.StudentRihaish.Imdadi = reader.GetBoolean(39);
                        student.StudentRihaish.Rihaish = reader.GetBoolean(40);

                        studentList.Add(student);
                    }
                    return studentList;
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                return studentList;
            }
        }

        public static Boolean deleteStudent(List<string> dakhlaNumbers)
        {
            bool result = false;
            string queryDelete = "update StudentDakhlaNumber set activeIndex = 0 where dakhlaNumber in ( ";
            foreach(string dakhlaNumber in dakhlaNumbers)
            {
                queryDelete += dakhlaNumber +", ";
            }
            queryDelete = queryDelete.Remove(queryDelete.Length - 2);
            queryDelete += ")";
            
            using(sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand deleteCommand = new SqlCommand(queryDelete, sqlConnection);
                    deleteCommand.ExecuteNonQuery();
                    MessageBox.Show("Students deleted Successfully");

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString()+"\n Students not deleted successfully");
                }
            }
            return result;
        }

        public static List<String> getDakhlaNumbers(string regNumber)
        {
            List<String> dakhlaNumbers = new List<String>();
            try
            {
                using (sqlConnection = new SqlConnection(connectionString))
                {
                    string queryGetDakhlaNumber = "select dakhlaNumber from StudentDakhlaNumber where registrationNumber = @regNumber AND activeIndex = 0";
                    sqlConnection.Open();
                    SqlCommand getDakhlaNumberCommand = new SqlCommand(queryGetDakhlaNumber, sqlConnection);
                    getDakhlaNumberCommand.Parameters.AddWithValue("@regNumber", regNumber);
                    SqlDataReader reader2 = getDakhlaNumberCommand.ExecuteReader();
                    while (reader2.Read())
                    {
                        dakhlaNumbers.Add(reader2[0].ToString());
                    }
                    reader2.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dakhlaNumbers;
        }

        public static void backUpDatabase()
        {
            try
            {
                var backUpDatabasePath = ConfigurationManager.AppSettings["BackUpDatabasePath"];
                var backupFileName = String.Format("{0}-{1}.bak",backUpDatabasePath, DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));
                using (sqlConnection = new SqlConnection(connectionString))
                {
                    var backupQuery = String.Format("BACKUP DATABASE ImdadUlUloom TO DISK='{0}'", backupFileName);
                    sqlConnection.Open();
                    SqlCommand backupQueryCommand = new SqlCommand(backupQuery, sqlConnection);
                    backupQueryCommand.ExecuteNonQuery();
                    MessageBox.Show("Backup Completed Successfully");
                    sqlConnection.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
