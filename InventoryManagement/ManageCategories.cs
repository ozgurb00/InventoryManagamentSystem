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
    public partial class ManageCategories : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(local);Initial Catalog=Inventorydb;Integrated Security=True");

        public ManageCategories()
        {
            InitializeComponent();
        }

        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from CategoryTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CategoryGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into CategoryTb1 values('"+CatIdTb.Text+"', '"+CatNameTb.Text+"')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Added...");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CatIdTb.Text == "")
            {
                MessageBox.Show("Enter The Category Id");
            }
            else
            {
                Con.Open();
                string myquery = "delete from CategoryTb1 where CatId = '" + CatIdTb.Text + "'";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category succesfully deleted");
                Con.Close();
                populate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update CategoryTb1 set CatName = '"+ CatNameTb.Text+"' where CatId = '"+int.Parse(CatIdTb.Text)+"'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Updated...");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void ManageCategories_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void CategoryGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CategoryGV.CurrentRow.Selected = true;
            CatIdTb.Text = CategoryGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            CatNameTb.Text = CategoryGV.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
}
