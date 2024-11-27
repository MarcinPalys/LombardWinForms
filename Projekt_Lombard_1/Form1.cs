using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Projekt_Lombard_1
{
    public partial class Form1 : Form
    {
        Base_Model base_Model = new Base_Model();
        DataGridViewEvent_Model dataGridViewEvent = new DataGridViewEvent_Model();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            base_Model.SelectBase(dataGridView1);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 3)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                Form4 form4 = new Form4(id);

                form4.ShowDialog();

            }
            dataGridViewEvent.SelectRow(sender as DataGridView);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(dataGridView1);
            
            form2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base_Model.UserDeletion(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            base_Model.CleanBase(dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(dataGridView1);

            form3.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            base_Model.NextPage(dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            base_Model.PreviousPage(dataGridView1);
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            base_Model.searchData(dataGridView1, txtSearch.Text);
        }
        private void dodajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dodajToolStripMenuItem.Checked = true;
            usuńToolStripMenuItem.Checked = false;
            wyczyśćToolStripMenuItem.Checked = false;
            modyfikujToolStripMenuItem.Checked = false;

            Form2 form2 = new Form2(dataGridView1);

            form2.ShowDialog();
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dodajToolStripMenuItem.Checked = false;
            usuńToolStripMenuItem.Checked = true;
            wyczyśćToolStripMenuItem.Checked = false;
            modyfikujToolStripMenuItem.Checked = false;

            base_Model.UserDeletion(dataGridView1);
        }

        private void wyczyśćToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dodajToolStripMenuItem.Checked = false;
            usuńToolStripMenuItem.Checked = false;
            wyczyśćToolStripMenuItem.Checked = true;
            modyfikujToolStripMenuItem.Checked = false;

            base_Model.CleanBase(dataGridView1);
        }

        private void modyfikujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dodajToolStripMenuItem.Checked = false;
            usuńToolStripMenuItem.Checked = false;
            wyczyśćToolStripMenuItem.Checked = false;
            modyfikujToolStripMenuItem.Checked = true;

            Form3 form3 = new Form3(dataGridView1);

            form3.ShowDialog();
        }

        private void dodajToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(dataGridView1);

            form2.ShowDialog();
        }

        private void usunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base_Model.UserDeletion(dataGridView1);
        }

        private void wyczyśćToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            base_Model.CleanBase(dataGridView1);
        }

        private void modyfikujToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(dataGridView1);

            form3.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if(txtSearch.Text == "")
            {
                base_Model.SelectBase(dataGridView1);
            }
            else
            {
                base_Model.searchData(dataGridView1, txtSearch.Text);
            }
        }        
    }
}
