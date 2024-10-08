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
    public partial class FrmStatistics : Form
    {
        public FrmStatistics()
        {
            InitializeComponent();
        }

        private void FrmStatistics_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Server=DESKTOP-COQ9ECD\\SQLEXPRESS; initial Catalog=NorthwindDb; integrated Security=true");

            //Sorgu1
            connection.Open();
            SqlCommand command=new SqlCommand("Select Count(*) from Products",connection);
            SqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
            label1.Text = "toplam ürün sayısı" + reader[0];
            }
            connection.Close();
            //Sorgu2
            connection.Open();
            SqlCommand command2 = new SqlCommand("Select * from Products Where UnitsInStock=(Select max(UnitsInStock) from Products)", connection);
            SqlDataReader reader2 = command2.ExecuteReader();
            if (reader2.Read())
            {
                label2.Text = "en fazla stoklu ürümn" + reader2[1] + "/" + reader2[6];
            }
            connection.Close();

            //Sorgu3
            connection.Open();
            SqlCommand command3 = new SqlCommand("Select Avg(UnitPrice) from Products where CategoryId=(Select CategoryId from Categories where CategoryName='Seafood')", connection);
            SqlDataReader reader3 = command3.ExecuteReader();
            if (reader3.Read())
            {
                label3.Text = "deniz ürünü ort fiyat"  +reader3[0];
            }
            connection.Close();
        }
    }
}
