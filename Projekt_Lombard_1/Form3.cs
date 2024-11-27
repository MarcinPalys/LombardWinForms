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
    public partial class Form3 : Form
    {
        private string name;
        private string surname;
        private DataGridView dataGridView;
        private
        Base_Model base_Model = new Base_Model();
        public Form3(DataGridView dataGridView)
        {
            InitializeComponent();
            this.dataGridView = dataGridView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = txtName.Text;
            surname = txtSurnmae.Text;

            base_Model.ModifyBase(name, surname, dataGridView);
            this.Close();
        }
    }
}
