using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Projekt_Lombard_1
{
    class Loans_Model
    {
        int Idmein;
        DataGridViewEvent_Model DataGridViewEvent_Model = new DataGridViewEvent_Model();
       
        public void AddToBase(string _dataPozyczki, int _kwota, string _terminZwrotu, DataGridView dataGridView, int id)
        {
            Idmein = id;
            string dataPozyczki = _dataPozyczki;
            int kwota = _kwota;
            string terminZwrotu = _terminZwrotu;

            if (kwota == 0)
            {
                MessageBox.Show("Podaj poprawne dane");
                return;
            }
            else
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=Baza_Uzytkownicy");
                con.Open();
               try
                {
                    var cmd = new SQLiteCommand(con);
                    cmd.CommandText = "INSERT INTO Pozyczki (UzytkownicyId,DataPozyczki,Kwota, TerminZwrotu) VALUES (@id,@dataPozyczki,@kwota,@terminZwrotu)";

                    cmd.Parameters.AddWithValue("@dataPozyczki", dataPozyczki);
                    cmd.Parameters.AddWithValue("@Kwota", kwota);
                    cmd.Parameters.AddWithValue("@TerminZwrotu", terminZwrotu);
                    cmd.Parameters.AddWithValue("@id", Idmein);

                    cmd.ExecuteNonQuery();
                    dataGridView.Rows.Clear();
                    Base_Model base_Model = new Base_Model();
                    LoadDateLoans(Idmein,dataGridView);
                    MessageBox.Show("Dodano nową pożyczkę");
               }
                catch
                {
                    MessageBox.Show("Nie udało się dodanie nowej osoby");
                }

                con.Close();

            }
        }
        public void LoadDateLoans(int id_, DataGridView dataGridView)
        {
            
            Idmein = id_;
            int id = id_;
            int iloscWierszy = 0;
            SQLiteConnection con = new SQLiteConnection("Data Source=Baza_Uzytkownicy");

            con.Open();
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT PozyczkiId, DataPozyczki, Kwota,TerminZwrotu FROM Pozyczki WHERE UzytkownicyId = @id";
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            SQLiteDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    iloscWierszy++;
                }
            }
            string[] DataArray = new string[Convert.ToInt32(iloscWierszy)];
            int[] KwotaArray = new int[Convert.ToInt32(iloscWierszy)];
            string[] TerminArray = new string[Convert.ToInt32(iloscWierszy)];
            int[] IdArray = new int[Convert.ToInt32(iloscWierszy)];

            int i = 0;

            var cmd1 = new SQLiteCommand(con);

            cmd1.CommandText = "SELECT PozyczkiId, DataPozyczki, Kwota,TerminZwrotu FROM Pozyczki WHERE UzytkownicyId = @id";
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@id", id);
            cmd1.ExecuteNonQuery();
            SQLiteDataReader result1 = cmd1.ExecuteReader();
            while (result1.Read())
            {
                IdArray[i] = Convert.ToInt32(result1[0]);
                DataArray[i] = result1[1].ToString();
                KwotaArray[i] = Convert.ToInt32(result1[2]);
                TerminArray[i] = result1[3].ToString();
                i++;

            }
            DataGridViewEvent_Model.WrtieDataGridViewLoans(dataGridView, DataArray, KwotaArray, TerminArray, IdArray);
        }
        public void RemoveLoan(DataGridView dataGridView, string dataPozyczki, int kwota, string dataZwrotu)
        { 
            SQLiteConnection con = new SQLiteConnection("Data Source=Baza_Uzytkownicy");

            con.Open();

            var cmd = new SQLiteCommand(con);
          
                cmd.CommandText = "DELETE FROM Pozyczki WHERE UzytkownicyId=@id AND DataPozyczki=@dataPozyczki AND Kwota=@kwota AND TerminZwrotu=@dataZwrotu";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", Idmein);
                cmd.Parameters.AddWithValue("@dataPozyczki", dataPozyczki);
                cmd.Parameters.AddWithValue("@kwota", kwota);
                cmd.Parameters.AddWithValue("@dataZwrotu", dataZwrotu);
                cmd.ExecuteNonQuery();
                dataGridView.Rows.Clear();
                LoadDateLoans(Idmein, dataGridView);
                MessageBox.Show("Usunięto pożyczkę"); 
        }
        public void LoadDateRepayment(int id_, DataGridView dataGridView)
        {
            Idmein = id_;
  
            int iloscWierszy = 0;
            SQLiteConnection con = new SQLiteConnection("Data Source=Baza_Uzytkownicy");

            con.Open();

            var cmd3 = new SQLiteCommand(con);
            
            //int IdPozyczki=0;


           /* cmd3.CommandText = "SELECT PozyczkiId FROM Pozyczki WHERE UzytkownicyId = @id AND Kwota = @kwota AND DataPozyczki=@dataPozyczki AND TerminZwrotu=@terminZwrotu";
            cmd3.Prepare();
            cmd3.Parameters.AddWithValue("@id", Idmein);           
            cmd3.Parameters.AddWithValue("@kwota", kwota);           
            cmd3.Parameters.AddWithValue("@dataPozyczki", dataPozyczki);
            cmd3.Parameters.AddWithValue("@terminZwrotu", terminZwrotu);
            cmd3.ExecuteNonQuery();
            SQLiteDataReader result3 = cmd3.ExecuteReader();
            if (result3.HasRows)
            {
                while (result3.Read())
                {
                    IdPozyczki = Convert.ToInt32(result3[0]);
                }
            }*/

            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT DataSplaty, Kwota FROM Splaty WHERE PozyczkiId = @id";
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@id", Idmein);
            cmd.ExecuteNonQuery();
            SQLiteDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    iloscWierszy++;
                }
            }
            string[] DataArray = new string[Convert.ToInt32(iloscWierszy)];
            int[] KwotaArray = new int[Convert.ToInt32(iloscWierszy)];
            

            int i = 0;

            var cmd1 = new SQLiteCommand(con);

            cmd1.CommandText = "SELECT DataSplaty, Kwota FROM Splaty WHERE PozyczkiId = @id";
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@id", Idmein);
            cmd1.ExecuteNonQuery();
            SQLiteDataReader result1 = cmd1.ExecuteReader();
            while (result1.Read())
            {
                DataArray[i] = result1[0].ToString();
                KwotaArray[i] = Convert.ToInt32(result1[1]);
                i++;

            }
            DataGridViewEvent_Model.WrtieDataGridRepayment(dataGridView, DataArray, KwotaArray);
        }
        public void AddToBaseRepayment(int kwota, DataGridView dataGridView, int id)
        {
            id = Idmein;
            
            var date= DateTime.Now.ToString("dd/MM/yyyy");
            int kwotaPoprzednia = 0;
            
            if (kwota == 0)
            {
                MessageBox.Show("Podaj poprawne dane");
                return;
            }
            else
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=Baza_Uzytkownicy");
                con.Open();
                //try
                //{
                    var cmd = new SQLiteCommand(con);
                    var cmd1 = new SQLiteCommand(con);
                    var cmd2 = new SQLiteCommand(con);                   
                    int kwotaSQL;
                                                         
                   
                    cmd.CommandText = "SELECT Kwota FROM Pozyczki WHERE PozyczkiId = @id";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@id", Idmein);
                    cmd.ExecuteNonQuery();
                    SQLiteDataReader result = cmd.ExecuteReader();
                    while (result.Read())
                    {
                        kwotaSQL = Convert.ToInt32(result[0]);
                        kwotaPoprzednia = kwota;
                        kwota = kwotaSQL - kwota;
                        
                    }
                    if(kwota<=0)
                    {
                        var cmd5 = new SQLiteCommand(con);

                        cmd5.CommandText = "DELETE FROM Pozyczki WHERE PozyczkiId=@id";
                        cmd5.Prepare();
                        cmd5.Parameters.AddWithValue("@id", Idmein);
                        cmd5.ExecuteNonQuery();
                        dataGridView.Rows.Clear();
                        //LoadDateLoans(Idmein, dataGridView);
                        MessageBox.Show("Spłacono pożyczkę");

                    }
                    else
                    {
                        cmd1.CommandText = "INSERT INTO Splaty (PozyczkiId,DataSplaty,Kwota) VALUES (@id,@dataPozyczki,@kwota)";

                        cmd1.Parameters.AddWithValue("@dataPozyczki", date);
                        cmd1.Parameters.AddWithValue("@Kwota", kwotaPoprzednia);
                        cmd1.Parameters.AddWithValue("@id", Idmein);

                        cmd1.ExecuteNonQuery();

                        cmd2.CommandText = "UPDATE Pozyczki SET Kwota=@kwota WHERE PozyczkiId=@id";
                        cmd2.Prepare();
                        cmd2.Parameters.AddWithValue("@id", Idmein);
                        cmd2.Parameters.AddWithValue("@kwota", kwota);
                        cmd2.ExecuteNonQuery();
                        dataGridView.Rows.Clear();

                        LoadDateRepayment(Idmein, dataGridView);
                        //LoadDateLoans(Idmein, dataGridView);

                        MessageBox.Show("Dodano nową spłatę");
                    }
                con.Close();
            }
                //catch
                //{
                    //MessageBox.Show("Nie udało dodać nowej spłaty");
                   
                //}

               
         //}
        }
    }
}
