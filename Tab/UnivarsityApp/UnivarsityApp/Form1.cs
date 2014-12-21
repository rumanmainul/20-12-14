using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnivarsityApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source = RUMAN; Database= UniversityDB; Integrated Security = true";
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.Open();

            string name = nameTextBox.Text;
            string emailAddress = emailTextBox.Text;
            string address = addressTextBox.Text;
            int phNumber = Convert.ToInt32(phoneNumberTextBox.Text);

            string sqlQuery = "insert into tStudent values('" + emailAddress + "', '" + address + "', '" +
                              phNumber + "','" + name + "')";
            SqlCommand command = new SqlCommand(sqlQuery, Connection);
            int rowEffected = command.ExecuteNonQuery();
            if (rowEffected > 0)
            {
                MessageBox.Show("Save SuccessFully");
            }
            Connection.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
