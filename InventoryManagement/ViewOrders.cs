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
    public partial class ViewOrders : Form
    {
        public ViewOrders()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(local);Initial Catalog=Inventorydb;Integrated Security=True");

        void populateorders()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from OrderTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                OrdersGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        private void ViewOrders_Load(object sender, EventArgs e)
        {
            populateorders();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void OrdersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Order Summary", new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("Order Id" + OrdersGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century", 25, FontStyle.Regular), Brushes.Black, new Point(230, 150));
            e.Graphics.DrawString("Customer Id" + OrdersGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century", 25, FontStyle.Regular), Brushes.Black, new Point(230, 150));
            e.Graphics.DrawString("Customer Name" + OrdersGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century", 25, FontStyle.Regular), Brushes.Black, new Point(230, 150));
            e.Graphics.DrawString("Order Date" + OrdersGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century", 25, FontStyle.Regular), Brushes.Black, new Point(230, 150));
            e.Graphics.DrawString("PoweredByCodeSpace", new Font("Century", 25, FontStyle.Regular), Brushes.Black, new Point(230, 150));
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
