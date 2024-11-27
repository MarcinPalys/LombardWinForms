using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt_Lombard_1
{
    public class DataGridViewEvent_Model
    {
       
        int iloscPrzeczytanych = 0;
        
        public int SelectRow(DataGridView s)
        {
            try
            {
                var wiersz = (Int32)s.SelectedRows[0].Index;
                return wiersz;
            }
            catch
            {
                return -1;
            }
        }
        public void WriteDataGridView(DataGridView dataGridView, int[] IdArray, string[] NameArray, string[] SurnameArray, int iloscWierszy)
        {
            int licznik = 0;
            
            if(iloscPrzeczytanych>iloscWierszy)
            {
                iloscPrzeczytanych = 0;
            }
            int doPrzeczytania = iloscWierszy - iloscPrzeczytanych;
            dataGridView.Rows.Clear();
            if (doPrzeczytania<6)
            {
                
            }
                if (doPrzeczytania <= 6)
                {
                    while (doPrzeczytania >= 1)
                    {
                        dataGridView.Rows.Add();
                        dataGridView.Rows[licznik].Cells[0].Value = IdArray[iloscPrzeczytanych];
                        dataGridView.Rows[licznik].Cells[1].Value = NameArray[iloscPrzeczytanych];
                        dataGridView.Rows[licznik].Cells[2].Value = SurnameArray[iloscPrzeczytanych];
                        iloscPrzeczytanych++;
                        doPrzeczytania--;
                        licznik++;
                    }
                }
                else
                {
                    while (licznik < 6)
                    {
                        dataGridView.Rows.Add();
                        dataGridView.Rows[licznik].Cells[0].Value = IdArray[iloscPrzeczytanych];
                        dataGridView.Rows[licznik].Cells[1].Value = NameArray[iloscPrzeczytanych];
                        dataGridView.Rows[licznik].Cells[2].Value = SurnameArray[iloscPrzeczytanych];
                        licznik++;
                        iloscPrzeczytanych++;
                    }
                   
                }
        }
        public void PreviousPage(DataGridView dataGridView, int[] IdArray, string[] NameArray, string[] SurnameArray)
        {
            int licznikWierszy = dataGridView.RowCount-1;
            dataGridView.Rows.Clear();
            int poczatek = 0;
            int licznik = 0;
            
            if (licznikWierszy<6)
            {
                poczatek =  iloscPrzeczytanych - licznikWierszy - 6;
                iloscPrzeczytanych = poczatek;
                
                while (licznik < 6)
                {
                    dataGridView.Rows.Add();
                    dataGridView.Rows[licznik].Cells[0].Value = IdArray[poczatek];
                    dataGridView.Rows[licznik].Cells[1].Value = NameArray[poczatek];
                    dataGridView.Rows[licznik].Cells[2].Value = SurnameArray[poczatek];
                    licznik++;
                    poczatek++;
                    iloscPrzeczytanych++;
                }
            }
            else
            if(licznikWierszy==6)
            {
                poczatek = iloscPrzeczytanych - 12;
                iloscPrzeczytanych = poczatek;
                if(iloscPrzeczytanych<0)
                {
                    iloscPrzeczytanych = 0;
                    poczatek = 0;
                }
                while (licznik < 6)
                {
                    dataGridView.Rows.Add();
                    dataGridView.Rows[licznik].Cells[0].Value = IdArray[poczatek];
                    dataGridView.Rows[licznik].Cells[1].Value = NameArray[poczatek];
                    dataGridView.Rows[licznik].Cells[2].Value = SurnameArray[poczatek];
                    licznik++;
                    poczatek++;
                    iloscPrzeczytanych++;
                }
            } 
        }
        public void WrtieDataGridViewLoans(DataGridView dataGridView, string[] DataArray, int[] KwotaArray, string[] TerminArray, int[] IdArray)
        {
            int licznikX = DataArray.Length;
            int y = 0;
            while (licznikX !=0)
            {
                dataGridView.Rows.Add();

                dataGridView.Rows[y].Cells[0].Value = IdArray[y];
                dataGridView.Rows[y].Cells[1].Value = DataArray[y];
                dataGridView.Rows[y].Cells[2].Value = KwotaArray[y];
                dataGridView.Rows[y].Cells[3].Value = TerminArray[y];
                y++;
                licznikX--;
                
            }
            
        }
        public void WrtieDataGridRepayment(DataGridView dataGridView, string[] DataArray, int[] KwotaArray)
        {
            int licznikX = DataArray.Length;
            int y = 0;
            while (licznikX != 0)
            {
                dataGridView.Rows.Add();

                dataGridView.Rows[y].Cells[0].Value = DataArray[y];
                dataGridView.Rows[y].Cells[1].Value = KwotaArray[y];
                y++;
                licznikX--;
            }

        }
    }
}
