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
    public partial class CategoryForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Stanimir\Documents\dbMS.mdf;Integrated Security=False;Connect Timeout=30;Encrypt=False");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public CategoryForm()
        {
            InitializeComponent();
            LoadCategory();
        }
        public void LoadCategory()
        {
            int i = 0;
            dgvCategory.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCategory.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }

        

       
        private void categoryAdd_Click(object sender, EventArgs e)
        {
            CategoryModuleForm categoryModule = new CategoryModuleForm();
            categoryModule.btnClear.Enabled = true;
            categoryModule.btnSave.Enabled = true;
            categoryModule.btnUpdate.Enabled = false;
            categoryModule.ShowDialog();
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CategoryModuleForm categoryrModule = new CategoryModuleForm();
                categoryrModule.lblCategoryID.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                categoryrModule.txtCategoryName.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();


                categoryrModule.btnSave.Enabled = false;
                categoryrModule.btnUpdate.Enabled = true;
                categoryrModule.btnClear.Enabled = true;
                categoryrModule.ShowDialog();


            }

            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this category?", "Delete Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbCategory WHERE categoryId LIKE '" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer deleted.");
                }

            }
            LoadCategory();
        }
    }
}
