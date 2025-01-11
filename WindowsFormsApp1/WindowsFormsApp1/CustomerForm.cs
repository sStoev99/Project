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
    
    public partial class CustomerForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Stanimir\Documents\dbMS.mdf;Integrated Security=False;Connect Timeout=30;Encrypt=False");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public CustomerForm()
        {
            InitializeComponent();
            LoadCustomer();
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
                dgvCustomers.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void customerAdd_Click(object sender, EventArgs e)
        {
            CustomerModuleForm customerModule = new CustomerModuleForm();
            customerModule.btnClear.Enabled = true;
            customerModule.btnSave.Enabled = true;
            customerModule.btnUpdate.Enabled = false;
            customerModule.ShowDialog();

        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomers.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CustomerModuleForm customerModule = new CustomerModuleForm();
                customerModule.lblCID.Text = dgvCustomers.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerModule.txtCName.Text = dgvCustomers.Rows[e.RowIndex].Cells[2].Value.ToString();
                customerModule.txtCPhone.Text = dgvCustomers.Rows[e.RowIndex].Cells[3].Value.ToString();

                customerModule.btnSave.Enabled = false;
                customerModule.btnUpdate.Enabled = true;
                customerModule.btnClear.Enabled = true;
                customerModule.txtCName.Enabled = false;
                customerModule.ShowDialog();


            }

            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this customer?", "Delete Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbCustomer WHERE cid LIKE '" + dgvCustomers.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer deleted.");
                }

            }
            LoadCustomer();
        }
    }
}
