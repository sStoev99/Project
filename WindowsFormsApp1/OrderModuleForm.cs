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

namespace WindowsFormsApp1
{
    public partial class OrderModuleForm : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Stanimir\Documents\dbMS.mdf;Integrated Security=False;Connect Timeout=30;Encrypt=False");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int quantity = 0;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadProducts();
            LoadCustomer();
           
        }
        public void LoadProducts()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();
        }
        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomers.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCustomer", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomers.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }



        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GetQty();
            if (Convert.ToInt16(nQuantity.Value) > quantity)
            {
                MessageBox.Show("Instock quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToInt16(nQuantity.Value) > 0)
            {
                int total = Convert.ToInt16(txtPrice.Text) * Convert.ToInt16(nQuantity.Value);
                txtTotal.Text = total.ToString();
            }
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();

        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCustomerId.Text = dgvCustomers.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCustomerName.Text = dgvCustomers.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtProductId.Text == "")
                {
                    MessageBox.Show("Please select product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to insert this order?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbOrder(oDate, pid, cid, quantity, price, total)VALUES(@oDate, @pid, @cid, @quantity, @price, @total)", con);
                    cm.Parameters.AddWithValue("@oDate", tdOrder.Value);
                    cm.Parameters.AddWithValue("@pid", Convert.ToInt16(txtProductId.Text));
                    cm.Parameters.AddWithValue("@cid", Convert.ToInt16(txtCustomerId.Text));
                    cm.Parameters.AddWithValue("@quantity", Convert.ToInt16(nQuantity.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt16(txtPrice.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToInt16(txtTotal.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Order has been seccessfully inserted.");


                    cm = new SqlCommand("UPDATE tbProduct SET  quantity = (quantity-@quantity) WHERE productid LIKE '" + txtProductId.Text + "' ", con);
                    cm.Parameters.AddWithValue("@quantity", Convert.ToInt16(nQuantity.Value));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }

        }

        private void Clear()
        {
            txtCId.Clear();
            txtCName.Clear();
            txtPrice.Clear();
            txtProductId.Clear();
            txtProductName.Clear();
            nQuantity.Value = 1;
            tdOrder.Value = DateTime.Now;

        }

       

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }


        private void GetQty()
        {
            cm = new SqlCommand("SELECT quantity FROM tbProduct WHERE productid ='" + txtProductId.Text + "'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                quantity = Convert.ToInt16(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }
    }
}

