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
    public partial class Form4 : Form
    {
        private Base_Model Base_Model = new Base_Model();
        Loans_Model Loans_Model = new Loans_Model();
        private int id;
        
        public Form4(int _id)
        {
            InitializeComponent();
            id = _id;

            Loans_Model.LoadDateLoans(id,dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoansForm loansForm = new LoansForm(id,dataGridView1);

            loansForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var index = (Int32)dataGridView1.SelectedRows[0].Index;
           // int IdPozyczki = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            Loans_Model.RemoveLoan(dataGridView1,dataGridView1.Rows[index].Cells[0].Value.ToString(), (int)dataGridView1.Rows[index].Cells[1].Value, (string)dataGridView1.Rows[index].Cells[2].Value);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {                
             
                int IdPozyczki = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
               
                Form5 form5 = new Form5(IdPozyczki,this);

                form5.ShowDialog();
                
            }
        }
        
    }
}
