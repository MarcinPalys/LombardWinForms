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
    public partial class Form2 : Form
    {
        private string name;
        private string surname;
        private string drivingLicense;
        private string gender;
        private string information;
        private DataGridView dataGridView;
        private 
        Base_Model base_Model = new Base_Model();
        public Form2(DataGridView dataGridView)
        {
            InitializeComponent();
            this.dataGridView = dataGridView;
        }
        private void button1_Click(object sender, EventArgs e)
        { 
            name = txtName.Text;
            surname = txtSurnmae.Text;
            information = richTextBox1.Text;
            if(checkBox1.Checked == true)
            {
                drivingLicense += " "+checkBox1.Text;
            }
            if (checkBox2.Checked == true)
            {
                drivingLicense += " "+checkBox2.Text;
            }
            if (checkBox3.Checked == true)
            {
                drivingLicense += " "+checkBox3.Text;
            }
            if (checkBox4.Checked == true)
            {
                drivingLicense += " "+checkBox4.Text;
            }
            if (checkBox5.Checked == true)
            {
                drivingLicense += " "+checkBox5.Text;
            }
            if (checkBox6.Checked == true)
            {
                drivingLicense += " "+checkBox6.Text;
            }
            if (checkBox7.Checked == true)
            {
                drivingLicense += " "+checkBox7.Text;
            }
            if (checkBox8.Checked == true)
            {
                drivingLicense += " "+checkBox8.Text;
            }
            if (checkBox9.Checked == true)
            {
                drivingLicense += " "+checkBox9.Text;
            }
            if(radioButton1.Checked == true)
            {
                gender = radioButton1.Text;
            }
            if (radioButton2.Checked == true)
            {
                gender = radioButton2.Text;
            }
            base_Model.AddToBase(name, surname, drivingLicense ,gender,information,dataGridView);
            this.Close();
        }
    }
}
