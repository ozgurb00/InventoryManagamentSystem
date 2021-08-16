using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;    

namespace InventoryManagement
{
    public partial class ManageProducts : Form
    {
        
        public ManageProducts()
        {
            InitializeComponent();
        }
        
        SqlConnection Con = new SqlConnection(@"Data Source=(local);Initial Catalog=Inventorydb;Integrated Security=True");
        void fillcategory()
        {
            string query = "select * from CategoryTb1";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.ValueMember = "CatName";
                CatCombo.DataSource = dt;
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource = dt;
                Con.Close();
            }
            catch
            {

            }
        }

        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        void filterbycategory()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTb1 where ProdCat = '"+SearchCombo.SelectedValue.ToString()+"'";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ManageProducts_Load(object sender, EventArgs e)
        {
            fillcategory();
            populate();
        }

        private void CatCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into ProductTb1 values ('"+ProductIdTb.Text+"', '"+ProductNameTb.Text+"', '"+ProductQtyTb.Text+"', '"+ProductPriceTb.Text+"', '"+DescriptionTb.Text+"', '"+CatCombo.SelectedValue.ToString()+"')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Added...");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ProductIdTb.Text == "")
            {
                MessageBox.Show("Enter The Product Id");
            }
            else
            {
                Con.Open();
                string myquery = "delete from ProductTb1 where ProductId = '" + ProductIdTb.Text + "'";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product succesfully deleted");
                Con.Close();
                populate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update ProductTb1 set ProdName = '"+ProductNameTb.Text+"', ProdQty = '"+int.Parse(ProductQtyTb.Text)+"', ProdPrice = '"+int.Parse(ProductPriceTb.Text)+"', ProdDesc = '"+DescriptionTb.Text+"', ProdCat = '"+ CatCombo.SelectedValue.ToString() + "' where ProductId = '"+int.Parse(ProductIdTb.Text)+"'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Updated...");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            filterbycategory();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void SearchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ProductsGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductsGV.CurrentRow.Selected = true;
            ProductIdTb.Text = ProductsGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            ProductNameTb.Text = ProductsGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            ProductQtyTb.Text = ProductsGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            ProductPriceTb.Text = ProductsGV.Rows[e.RowIndex].Cells[3].Value.ToString();
            DescriptionTb.Text = ProductsGV.Rows[e.RowIndex].Cells[4].Value.ToString();
            CatCombo.Text = ProductsGV.Rows[e.RowIndex].Cells[5].Value.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
