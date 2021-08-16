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
    public partial class ManageCustomers : Form
    {
        public ManageCustomers()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(local);Initial Catalog=Inventorydb;Integrated Security=True");

        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from CustomerTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomersGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }


        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into CustomerTb1 values('" + CustomerId.Text + "', '" + CustomerNameTb.Text + "', '" + CustomerPhoneTb.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Added...");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void ManageCustomers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CustomerId.Text == "")
            {
                MessageBox.Show("Enter The Customer Id");
            }
            else
            {
                Con.Open();
                string myquery = "delete from CustomerTb1 where CustId = '" + CustomerId.Text + "'";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer succesfully deleted");
                Con.Close();
                populate();
            }
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*CustomerId.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
            CustomerNameTb.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();
            CustomerPhoneTb.Text = CustomersGV.SelectedRows[0].Cells[2].Value.ToString();*/
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from OrderTb1 where CustId = " + CustomerId.Text + "", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            OrderLabel.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter sda1 = new SqlDataAdapter("Select Sum(TotalAmt) from OrderTb1 where CustId = " + CustomerId.Text + "", Con);
           
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            OrdersAmountPnl.Text = dt1.Rows[0][0].ToString();

            SqlDataAdapter sda2 = new SqlDataAdapter("Select Max(OrderDate) from OrderTb1 where CustId = " + CustomerId.Text + "", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            DatePanel.Text = dt2.Rows[0][0].ToString();
            Con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update CustomerTb1 set CustName = '"+CustomerNameTb.Text+"', CustPhone = '"+CustomerPhoneTb.Text+"' where CustId = '"+int.Parse(CustomerId.Text)+"'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Updated...");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }

        private void CustomersGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomersGV.CurrentRow.Selected = true;
            CustomerId.Text = CustomersGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            CustomerNameTb.Text = CustomersGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            CustomerPhoneTb.Text = CustomersGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from OrderTb1 where CustId = " + CustomerId.Text + "", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            OrderLabel.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter sda1 = new SqlDataAdapter("select Sum(TotalAmt) from OrderTb1 where CustId = " + CustomerId.Text + "", Con);
            
            DataTable dt1 = new DataTable();
            sda.Fill(dt1);
            AmountLabel.Text = dt1.Rows[0][0].ToString();
            SqlDataAdapter sda2 = new SqlDataAdapter("select Max(OrderDate) from OrderTb1 where CustId = " + CustomerId.Text + "", Con);

            DataTable dt2 = new DataTable();
            sda.Fill(dt2);
            AmountLabel.Text = dt2.Rows[0][0].ToString();
            Con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
