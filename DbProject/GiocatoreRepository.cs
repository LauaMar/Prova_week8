using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DbProject
{
    public class GiocatoreRepository: IGiocatoreRep
    {
        public static string connectionString = @"Server=(localdb)\mssqllocaldb;Database=MostriVsEroi;Trusted_Connection=True;";
        
        public Giocatore VerificaGiocatore(Giocatore giocatore) 
        {
           
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open)
                    Console.WriteLine("Errore di connessione...");

                SqlCommand leggi = conn.CreateCommand();
                leggi.CommandType = System.Data.CommandType.Text;
                leggi.CommandText = $"SELECT * FROM Giocatore WHERE Username = @Username;";
                leggi.CommandType = System.Data.CommandType.Text;
                leggi.Parameters.AddWithValue("@Username", giocatore.Username);
                Giocatore giocatoreDB = new Giocatore();
                object pass = null;
                object isAdmin = null;
                SqlDataReader reader = leggi.ExecuteReader();
                if(!reader.HasRows)
                {
                    return giocatoreDB;
                }
                while (reader.Read())
                {
                    pass = reader["Psswrd"];
                    isAdmin = reader["IsAdmin"];
                }
                giocatoreDB.Username = giocatore.Username;
                giocatoreDB.Psswrd = (string)pass;
                giocatoreDB.IsAdmin = (bool)isAdmin;
                return giocatoreDB;
            }
        }

        public void AggiungiGiocatore(Giocatore giocatore)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Errore di connessione...");

                DataSet dsGiocatore = new DataSet();
                SqlDataAdapter giocatoreAdapter = new();

                SqlCommand selectGiocatore = new SqlCommand("SELECT * FROM Giocatore", conn);

                giocatoreAdapter.SelectCommand = selectGiocatore; 
                giocatoreAdapter.InsertCommand = GetGiocatoreInsertCommand(conn);

                giocatoreAdapter.Fill(dsGiocatore, "Giocatore"); 

                conn.Close();

                DataRow newRow = dsGiocatore.Tables["Giocatore"].NewRow(); 

                newRow["Username"] = giocatore.Username;
                newRow["Psswrd"] = giocatore.Psswrd;
                newRow["IsAdmin"] = giocatore.IsAdmin;

                dsGiocatore.Tables["Giocatore"].Rows.Add(newRow);

                giocatoreAdapter.Update(dsGiocatore, "Giocatore");

            }
        }

        public bool ModificaGiocatore(Giocatore giocatore)
        {
            bool isModified = false;
            using (SqlConnection conn = new SqlConnection(GiocatoreRepository.connectionString))
            {

                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Errore di connessione...");

                SqlDataAdapter giocatoreAdapter = new();
                DataSet dsGiocatore = new DataSet();

                SqlCommand selectGiocatore = new SqlCommand("SELECT * FROM Giocatore", conn);

                giocatoreAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;


                giocatoreAdapter.SelectCommand = selectGiocatore;

                giocatoreAdapter.UpdateCommand = GetGiocatoreUpdateCommand(conn);

                giocatoreAdapter.Fill(dsGiocatore, "Giocatore");

                conn.Close();

                DataRow rowToChange = dsGiocatore.Tables["Giocatore"].Rows.Find(giocatore.Username);

                if (rowToChange != null)
                {

                    rowToChange["IsAdmin"] = giocatore.IsAdmin;

                    //Console.WriteLine($"ROW STATE: {rowToChange.RowState.ToString()}");
                    isModified = true;
                    giocatoreAdapter.Update(dsGiocatore, "Giocatore");
                }
            }
            return isModified;
        }

        #region MetodiSupporto

        private static SqlCommand GetGiocatoreInsertCommand(SqlConnection conn)
        {
            SqlCommand insertCommand = new SqlCommand();
            insertCommand.Connection = conn;

            insertCommand.CommandText = "INSERT INTO Giocatore " +
                      "VALUES(@Username,@Psswrd,@IsAdmin)";


            insertCommand.CommandType = System.Data.CommandType.Text;

            insertCommand.Parameters.Add(
                new SqlParameter(
                    "@Username",
                    SqlDbType.NVarChar,
                    15,
                    "Username"
                    )
                );

            insertCommand.Parameters.Add(
                new SqlParameter(
                    "@Psswrd",
                    SqlDbType.NVarChar,
                    15,
                    "Psswrd"
                    )
                );

            insertCommand.Parameters.Add(
                new SqlParameter(
                    "@IsAdmin",
                    SqlDbType.Bit,
                    15,
                    "IsAdmin"
                    )
                );

            return insertCommand;
        }

        private static SqlCommand GetGiocatoreUpdateCommand(SqlConnection conn)
        {
            SqlCommand updateCommand = new SqlCommand();

            updateCommand.Connection = conn;

            updateCommand.CommandText = "UPDATE Giocatore " +
                      "SET IsAdmin= @IsAdmin WHERE Username = @Username";


            updateCommand.CommandType = System.Data.CommandType.Text;

            updateCommand.Parameters.Add(
                new SqlParameter(
                    "@Username",
                    SqlDbType.NVarChar,
                    15,
                    "Username"
                    )
                );

            updateCommand.Parameters.Add(
                new SqlParameter(
                    "@IsAdmin",
                    SqlDbType.Bit,
                    15,
                    "IsAdmin"
                    )
                );

            return updateCommand;
        }
        #endregion
    }
}
