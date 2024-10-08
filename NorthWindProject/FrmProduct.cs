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

namespace NorthWindProject
{
    public partial class FrmProduct : Form
    {
        SqlConnection connection = new SqlConnection("Server=DESKTOP-COQ9ECD\\SQLEXPRESS; initial Catalog=NorthwindDb; integrated Security=true");
        public FrmProduct()
        {
            InitializeComponent();
        }
        void ProductList()
        {
            SqlCommand command = new SqlCommand("Select ProductId, ProductName,CategoryName,UnitPrice,UnitsInStock from Products Inner Join Categories on Products.CategoryID=Categories.CategoryID", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable=new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
        void CategoryList()
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select * from Categories", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.DisplayMember = "CategoryName";
            adapter.Fill(dataTable);
            cmbCategory.DataSource = dataTable;
            connection.Close();
        }
        private void FrmProduct_Load(object sender, EventArgs e)
        {
            ProductList();
            CategoryList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = 0;
            connection.Open();
            SqlCommand command=new SqlCommand("Select top(1) ProductId from Products Order by ProductID Desc",connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                id = int.Parse(dataReader[0].ToString());
            }
            connection.Close();
            connection.Open();
            SqlCommand command2 = new SqlCommand("inseRt into Products (ProductId,ProductName, UnitPrice,CategoryId) values (@p1,@p2,@p3,@p4)", connection);
            command2.Parameters.AddWithValue("@p1",id+1);
            command2.Parameters.AddWithValue("@p2",txtProductName.Text);
            command2.Parameters.AddWithValue("@p3",txtUnitPrice.Text);
            command2.Parameters.AddWithValue("@p4",cmbCategory.SelectedValue);
            command2.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("başarı ile eklendi");
        }
    }
}
