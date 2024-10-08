using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NorthWindProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Server=DESKTOP-COQ9ECD\\SQLEXPRESS; initial Catalog=NorthwindDb; integrated Security=true");
        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlCommand command = new SqlCommand("Select * from Categories", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCategoryAdd_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Insert into Categories (CategoryName , Description) values (@categoryName , @description)",connection);
            command.Parameters.AddWithValue("@categoryName",txtCategoryName.Text);
            command.Parameters.AddWithValue("@description", txtCategoryDescription.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Ekleme başarılı");
            connection.Close();
        }

        private void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Delete from Categories CategoryId=@p", connection);
            command.Parameters.AddWithValue("@p",txtCategoryId.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Silme başarılı");
            connection.Close();
        }

        private void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Update Categories Set CategoryName=@p1 , Description=@p2 where CategoryId=@p3", connection);
            command.Parameters.AddWithValue("@p1",txtCategoryName.Text);
            command.Parameters.AddWithValue("@p2",txtCategoryDescription.Text);
            command.Parameters.AddWithValue("@p3",txtCategoryId.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("güncelleme başarılı");
            connection.Close();
        }

        private void btnCategorySearch_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command=new SqlCommand("Select CategoryId, CategoryName , Description from Categories where CategoryName=@categoryName",connection);
            command.Parameters.AddWithValue("@categoryName",txtCategoryName.Text);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
