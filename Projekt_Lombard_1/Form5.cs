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
    public partial class Form5 : Form
    {
        int id;
     
        Loans_Model Loans_Model = new Loans_Model();
        Form4 form4;
        public Form5(int id_, Form4 _form4)
        {
            InitializeComponent();
            id = id_;
          
            form4 = _form4;
            Loans_Model.LoadDateRepayment(id, dataGridView1);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var text = txt1.Text;
            Loans_Model.AddToBaseRepayment(Convert.ToInt32(text), dataGridView1, id);
            this.Close();
            form4.Close();
            
        }
    }
}
