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
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(local);Initial Catalog=Inventorydb;Integrated Security=True");

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from UserTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                UsersGV.DataSource = ds.Tables[0];
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
                SqlCommand cmd = new SqlCommand("insert into UserTb1 values('" + unameTb.Text + "', '" + FnameTb.Text + "', '" + PasswordTb.Text + "', '" + PhoneTb.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Added...");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(PhoneTb.Text == "")
            {
                MessageBox.Show("Enter The Users Phone Number");
            }
            else
            {
                Con.Open();
                string myquery = "delete from UserTb1 where Uphone = '" + PhoneTb.Text + "'";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User succesfully deleted");
                Con.Close();
                populate();
            }
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*unameTb.Text = UsersGV.Rows[0].Cells[0].Value.ToString();
            FnameTb.Text = UsersGV.Rows[0].Cells[1].Value.ToString();
            PasswordTb.Text = UsersGV.Rows[0].Cells[2].Value.ToString();
            PhoneTb.Text = UsersGV.Rows[0].Cells[3].Value.ToString();*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update UserTb1 set Uname = '"+unameTb.Text+"', Ufullname='"+FnameTb.Text+"', Upassword = '"+PasswordTb.Text+"' where Uphone = '"+PhoneTb.Text+"'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Updated...");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void UsersGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UsersGV.CurrentRow.Selected = true;
            unameTb.Text = UsersGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            FnameTb.Text = UsersGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            PasswordTb.Text = UsersGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            PhoneTb.Text = UsersGV.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
}
