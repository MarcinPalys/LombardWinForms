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
    public partial class LoansForm : Form
    {
        private int id;
        private DataGridView dataGridView1;
        DataGridViewEvent_Model DataGridViewEvent_Model = new DataGridViewEvent_Model();
        Base_Model base_Model = new Base_Model();
        Loans_Model loans_Model1 = new Loans_Model();
        public LoansForm(int _id, DataGridView dataGridView)
        {
            InitializeComponent();
            dataGridView1 = dataGridView;
            id = _id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dataPozyczki = dateTimePicker1.Text;
            int kwota = Convert.ToInt32(txtKwota.Text);
            string terminZwrotu = dateTimePicker2.Text;

            Loans_Model loans_Model = new Loans_Model();
            loans_Model.AddToBase(dataPozyczki,kwota,terminZwrotu, dataGridView1, id);
            dataGridView1.Rows.Clear();
            loans_Model1.LoadDateLoans(id, dataGridView1);   
        }
    }
}
