using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt_Lombard_1
{
    public class Base_Model
    {
        DataGridViewEvent_Model dataGridViewEvent_Model = new DataGridViewEvent_Model();
        public void ConnectionBase()
        {
            if (!File.Exists("Baza_Uzytkownicy"))
            {
                SQLiteConnection.CreateFile("Baza_Uzytkownicy");
                SQLiteConnection con = new SQLiteConnection("Data Source=Baza_Uzytkownicy");
                con.Open();
                string table = "CREATE TABLE Uzytkownicy(Id integer PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, Imie TEXT, Nazwisko TEXT, PrawoJazdy TEXT, Plec TEXT, Informacje TEXT)";
                string table1 = "CREATE TABLE Pozyczki(PozyczkiId integer PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL,UzytkownicyId integer,DataPozyczki TEXT,Kwota integer,TerminZwrotu TEXT,ZabezpieczenieId integer,FOREIGN KEY(UzytkownicyId) REFERENCES Uzytkownicy(Id))";
                string table2 = "CREATE TABLE Splaty(SplatyID integer PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL,PozyczkiId integer,DataSplaty TEXT,Kwota integer,FOREIGN KEY(PozyczkiId) REFERENCES Pozyczki(PozyczkiId))";
                SQLiteCommand Comand = new SQLiteCommand(table, con);
                SQLiteCommand Comand1 = new SQLiteCommand(table1, con);
                SQLiteCommand Comand2 = new SQLiteCommand(table2, con);
                Comand.ExecuteNonQuery();
                Comand1.ExecuteNonQuery();
                Comand2.ExecuteNonQuery();

                MessageBox.Show("Utworzono nową bazę");
                con.Close();
            }
        }
        public void TakeData(DataGridView dataGridView)
        {
            double iloscWierszy=0;
            double iloscStron;
            int i = 0;
            
            SQLiteConnection con = new SQLiteConnection("Data Source=Baza_Uzytkownicy");

            con.Open();
            string query = "SELECT Id, Imie,Nazwisko FROM Uzytkownicy";
            
            SQLiteCommand myComand = new SQLiteCommand(query, con);
            SQLiteDataReader result = myComand.ExecuteReader();

            SQLiteCommand myComand1 = new SQLiteCommand(query, con);
            SQLiteDataReader result1 = myComand1.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    iloscWierszy++;
                }
            }
            int[] IdArray = new int[Convert.ToInt32(iloscWierszy)];
            string[] NameArray = new string[Convert.ToInt32(iloscWierszy)];
            string[] SurnameArray = new string[Convert.ToInt32(iloscWierszy)];
            iloscStron = Math.Ceiling(Convert.ToDouble(iloscWierszy / 6));
            
            while(result1.Read())
            {
                IdArray[i] = Convert.ToInt32(result1[0]);
                NameArray[i] = result1[1].ToString();
                SurnameArray[i] = result1[2].ToString();
                i++;
            }
            con.Close();
        }
            
        public void SelectBase(DataGridView dataGridView)
        {
            if(!File.Exists("Baza_Uzytkownicy"))
            {
                ConnectionBase();
            }
           
             double iloscWierszy = 0;
             double iloscStron;
             int i = 0;

           
             SQLiteConnection con = new SQLiteConnection("Data Source=Baza_Uzytkownicy");

             con.Open();
             string query = "SELECT  Id, Imie,Nazwisko FROM Uzytkownicy";

             SQLiteCommand myComand = new SQLiteCommand(query, con);
             SQLiteDataReader result = myComand.ExecuteReader();

             SQLiteCommand myComand1 = new SQLiteCommand(query, con);
             SQLiteDataReader result1 = myComand1.ExecuteReader();

             if (result.HasRows)
             {
                 while (result.Read())
                 {
                     iloscWierszy++;
                 }
             }
             int[] IdArray = new int[Convert.ToInt32(iloscWierszy)];
             string[] NameArray = new string[Convert.ToInt32(iloscWierszy)];
             string[] SurnameArray = new string[Convert.ToInt32(iloscWierszy)];
             iloscStron = Math.Ceiling(Convert.ToDouble(iloscWierszy / 6));

             while (result1.Read())
             {
                 IdArray[i] = Convert.ToInt32(result1[0]);
                 NameArray[i] = result1[1].ToString();
                 SurnameArray[i] = result1[2].ToString();
                 i++;
             }
                
             dataGridViewEvent_Model.WriteDataGridView(dataGridView, IdArray, NameArray, SurnameArray, Convert.ToInt32(iloscWierszy));

            con.Close();

        }
        public void AddToBase(string name, string surname,string drivingLicense,string gender,string information,DataGridView dataGridView1)
        {
            string n = name;
            string s = surname;
            string d = drivingLicense;
            string g = gender;
            string i = information;
            if(n=="" || s =="")
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
                    cmd.CommandText = "INSERT INTO Uzytkownicy (Imie, Nazwisko, PrawoJazdy, Plec, Informacje) VALUES (@name,@surname,@drivingLicense,@gender,@information)";

                    cmd.Parameters.AddWithValue("@name", n);
                    cmd.Parameters.AddWithValue("@surname", s);
                    cmd.Parameters.AddWithValue("@drivingLicense", d);
                    cmd.Parameters.AddWithValue("@gender", g);
                    cmd.Parameters.AddWithValue("@information", i);

                    cmd.ExecuteNonQuery();
                    dataGridView1.Rows.Clear();
                    SelectBase(dataGridView1);
                    MessageBox.Show("Dodano nową osobę");
                }
                catch
                {
                    MessageBox.Show("Nie udało się dodanie nowej osoby");
                }

                con.Close();
            }
            
        }
        public void UserDeletion(DataGridView dataGridView1)
        {
            DataGridViewEvent_Model dataGridViewEvent_Model = new DataGridViewEvent_Model();
            int selectRow = dataGridViewEvent_Model.SelectRow(dataGridView1);
            if(selectRow == -1)
            {
                return;
            }
            else
            {
                var id = dataGridView1.Rows[selectRow].Cells[0].Value;
                SQLiteConnection con = new SQLiteConnection("Data Source=Baza_Uzytkownicy");

                con.Open();

                var cmd = new SQLiteCommand(con);
                try
                {
                    cmd.CommandText = "DELETE FROM Uzytkownicy WHERE Id=@id";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    dataGridView1.Rows.Clear();
                    SelectBase(dataGridView1);
                    MessageBox.Show("Usunięto użytkownika");
                }
                catch (Exception)
                {
                    MessageBox.Show("Nie udało się usunąć użytkownika");
                }
            }
            
        }
        public void CleanBase(DataGridView dataGridView1)
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=Baza_Uzytkownicy");
            var cmd = new SQLiteCommand(con);
            try
            {
                con.Open();
                cmd.CommandText = "DELETE FROM Uzytkownicy";
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                dataGridView1.Rows.Clear();
                SelectBase(dataGridView1);
                MessageBox.Show("Wyczyszczono bazę");
                con.Close();
            }
            catch
            {
                MessageBox.Show("Wyczyszczenie bazy się nie powiodło");
            }
        }
        public void ModifyBase(string name, string surname, DataGridView dataGridView1)
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=Baza_Uzytkownicy");
            var cmd = new SQLiteCommand(con);

            DataGridViewEvent_Model dataGridViewEvent_Model = new DataGridViewEvent_Model();
            int selectRow = dataGridViewEvent_Model.SelectRow(dataGridView1);
            var id = dataGridView1.Rows[selectRow].Cells[0].Value;
            con.Open();
            if (selectRow == -1)
            {
                return;
            }
            if(name.Length<=0 && surname.Length<=0)
            {
                MessageBox.Show("Nie wprowadzono danych");
            }
            else
            {
                if (name.Length <= 0)
                {
                    try
                    {
                        cmd.CommandText = "UPDATE Uzytkownicy SET Nazwisko=@surname WHERE Id=@id";
                        cmd.Prepare();
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@surname", surname);
                        cmd.ExecuteNonQuery();
                        SelectBase(dataGridView1);
                        MessageBox.Show("Zmodyfikowano uzytkownika");   
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nie udana próba zmodyfikowania");
                    }
                }
                else
                if (surname.Length <= 0)
                {
                    try
                    {
                        cmd.CommandText = "UPDATE Uzytkownicy SET Imie=@name WHERE Id=@id";
                        cmd.Prepare();
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.ExecuteNonQuery();
                        SelectBase(dataGridView1);
                        MessageBox.Show("Zmodyfikowano uzytkownika");
                      
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nie udana próba zmodyfikowania");
                    }
                }
                else
                {
                    try
                    {
                        cmd.CommandText = "UPDATE Uzytkownicy SET Imie=@name, Nazwisko=@surname WHERE Id=@id";
                        cmd.Prepare();
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@surname", surname);
                        cmd.ExecuteNonQuery();
                    
                        SelectBase(dataGridView1);
                        MessageBox.Show("Zmodyfikowano uzytkownika");
                       
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nie udana próba zmodyfikowania");
                    }
                }
            }
            
            con.Close();
        }
        public void NextPage(DataGridView dataGridView)
        {
            if(dataGridView.RowCount < 7)
            {
                return;
            }else
            {
                
                SelectBase(dataGridView);
            }
            
        }
        public void PreviousPage(DataGridView dataGridView)
        {
            double iloscWierszy = 0;
            double iloscStron;
            int i = 0;


            SQLiteConnection con = new SQLiteConnection("Data Source=Baza_Uzytkownicy");

            con.Open();
            string query = "SELECT  Id, Imie,Nazwisko FROM Uzytkownicy";
            
            SQLiteCommand myComand = new SQLiteCommand(query, con);
            SQLiteDataReader result = myComand.ExecuteReader();

            SQLiteCommand myComand1 = new SQLiteCommand(query, con);
            SQLiteDataReader result1 = myComand1.ExecuteReader();
            
            if (result.HasRows)
            {
                while (result.Read())
                {
                    iloscWierszy++;
                }
            }
            int[] IdArray = new int[Convert.ToInt32(iloscWierszy)];
            string[] NameArray = new string[Convert.ToInt32(iloscWierszy)];
            string[] SurnameArray = new string[Convert.ToInt32(iloscWierszy)];
            iloscStron = Math.Ceiling(Convert.ToDouble(iloscWierszy / 6));
            while (result1.Read())
            {
                IdArray[i] = Convert.ToInt32(result1[0]);
                NameArray[i] = result1[1].ToString();
                SurnameArray[i] = result1[2].ToString();
                i++;
            }
            if(Convert.ToInt32(dataGridView.Rows[0].Cells[0].Value) == IdArray[0])
            {
                return;
            }
            else
            {
                dataGridViewEvent_Model.PreviousPage(dataGridView, IdArray, NameArray, SurnameArray);
            }                                                                             
        }
        public void searchData(DataGridView dataGridView, string search)
        {
            string searchDate = search;

            SQLiteConnection con = new SQLiteConnection("Data Source=Baza_Uzytkownicy");
            var cmd = new SQLiteCommand(con);
            con.Open();


            cmd.CommandText = "SELECT Id, Imie,Nazwisko FROM Uzytkownicy WHERE nazwisko LIKE '%' || @search || '%' LIMIT 1";
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@search", searchDate);
            cmd.ExecuteNonQuery();
            SQLiteDataReader result1 = cmd.ExecuteReader();
            while(result1.Read())
            {
                dataGridView.Rows.Clear();
            
                dataGridView.Rows[0].Cells[0].Value = result1[0];
                dataGridView.Rows[0].Cells[1].Value = result1[1];
                dataGridView.Rows[0].Cells[2].Value = result1[2];
            }
        }
    }
}
