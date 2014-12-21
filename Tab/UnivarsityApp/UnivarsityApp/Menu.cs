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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        string ConnectionString = @"Data Source = RUMAN; Database= UniversityDB; Integrated Security = true";
        private void saveButton_Click(object sender, EventArgs e)
        {
            
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

       //student Search, Update, Delete

        ListViewItem lvi = new ListViewItem();
        private void searchButton_Click(object sender, EventArgs e)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            string studentId = searchIdTextBox.Text;
            string sqlQuery = "";
            if (String.IsNullOrEmpty(studentId))
            {

                sqlQuery = "select * from tStudent";
            }
            else
            {
                sqlQuery = "select * from tStudent where Id = '" + studentId + "'";
            }
            LoadStudentListView(sqlQuery, Connection);
        }

        private void LoadStudentListView(string sqlQuery, SqlConnection Connection)
        {

            SqlCommand command = new SqlCommand(sqlQuery, Connection);
            SqlDataReader aReader = command.ExecuteReader();
            string[] stuStrings = new string[5];
            listView1.Items.Clear();
            while (aReader.Read())
            {
                Student aStudent = new Student();
                aStudent.studentID = aReader["Id"].ToString();
                aStudent.studentName = aReader["Name"].ToString();
                aStudent.email = aReader["Email"].ToString();
                aStudent.address = aReader["Address"].ToString();
                aStudent.phone = aReader["PhoneNumber"].ToString();
                stuStrings[0] = aStudent.studentID;
                stuStrings[1] = aStudent.studentName;
                stuStrings[2] = aStudent.email;
                stuStrings[3] = aStudent.address;
                stuStrings[4] = aStudent.phone;
                lvi = new ListViewItem(stuStrings);
                listView1.Items.Add(lvi);
                lvi.Tag = aStudent;
            }
            Connection.Close();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem selectedItem = listView1.SelectedItems[0];
            Student selectedStudent = (Student)selectedItem.Tag;
            studentId.Text = selectedStudent.studentID;
            studentNameTextBox.Text = selectedStudent.studentName;
            studentEmailTextBos.Text = selectedStudent.email;
            studentAddTextBox.Text = selectedStudent.address;
            studentPhNumTextBox.Text = selectedStudent.phone;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.Open();

            int id = Convert.ToInt32(studentId.Text);
            string name = studentNameTextBox.Text;
            string emailAddress = studentEmailTextBos.Text;
            string address = studentAddTextBox.Text;
            string phNumber = studentPhNumTextBox.Text;

            string sqlQuery = "UPDATE tStudent set Name='" + name + "', Email='" + emailAddress + "', Address='" + address + "', PhoneNumber='" + phNumber + "' WHERE Id = '" + id + "'";
            SqlCommand command = new SqlCommand(sqlQuery, Connection);
            int rowEffected = command.ExecuteNonQuery();
            if (rowEffected > 0)
            {
                MessageBox.Show("Update SuccessFully");
            }
            sqlQuery = "select * from tStudent";
            LoadStudentListView(sqlQuery, Connection);
            Connection.Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            string id = studentId.Text;
            if (!String.IsNullOrEmpty(id))
            {
                string sqlQuery = "Delete from tStudent where Id = '" + id + "'";
                SqlCommand command = new SqlCommand(sqlQuery, Connection);
                int rowEffected = command.ExecuteNonQuery();
                if (rowEffected > 0)
                {
                    MessageBox.Show("Deleted SuccessFully");
                }
                sqlQuery = "select * from tStudent";
                LoadStudentListView(sqlQuery, Connection);
                Connection.Close();
            }
            else
            {
                MessageBox.Show("No ID Selected");
            }

        }

     
        }
    }
