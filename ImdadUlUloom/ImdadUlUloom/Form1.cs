using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace ImdadUlUloom
{
    public partial class adminLoginPage : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        SqlConnection sqlConnection;

        public adminLoginPage()
        {
            InitializeComponent();
        }

        private void adminLoginBtn_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string query = "select top 1 adminName, adminPassword from adminInfo where adminName = '"+adminNameTB.Text+"' ";
            string userName = " ";
            string password = " ";
            try
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    userName = reader["adminName"].ToString();
                    password = reader["adminPassword"].ToString();
                    break;
                }
                sqlConnection.Close();
                if(userName == adminNameTB.Text && password == adminPasswordTB.Text)
                {
                    LandingPage landingPage = new LandingPage();
                    this.Hide();
                    landingPage.Show();
                }
                else
                {
                    MessageBox.Show("Incorrect Username/Password");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void adminLoginPage_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
