using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;

namespace DbProject
{
    public class EroeRepository: IEroeRep

    {
        
        public void AggiungiEroe(Eroe eroe)
        {
            using (SqlConnection conn = new SqlConnection(GiocatoreRepository.connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Errore di connessione...");

                SqlDataAdapter eroeAdapter = new();
                DataSet dsEroe = new DataSet();

                SqlCommand selectEroe = new SqlCommand("SELECT * FROM Eroe", conn);

                eroeAdapter.SelectCommand = selectEroe;
                eroeAdapter.InsertCommand = GetEroeInsertCommand(conn);

                eroeAdapter.Fill(dsEroe, "Eroe");

                conn.Close();

                DataRow newRow = dsEroe.Tables["Eroe"].NewRow();

                newRow["Nome"] = eroe.Nome;
                newRow["CategoriaID"] = eroe.Categoria.ID;
                newRow["ArmaID"] = eroe.Arma.ID;
                newRow["Livello"] = eroe.Livello;
                newRow["PuntiAccumulati"] = eroe.PuntiAccumulati;
                newRow["UsernameOwner"] = eroe.UsernameOwner;

                dsEroe.Tables["Eroe"].Rows.Add(newRow);

                eroeAdapter.Update(dsEroe, "Eroe");

            }
        }

        public bool MostraEroiPerGiocatore(Giocatore giocatore)
        {
            bool hasEroes = true;
            using (SqlConnection conn = new SqlConnection(GiocatoreRepository.connectionString))
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open)
                    Console.WriteLine("Errore di connessione...");

                SqlCommand leggi = conn.CreateCommand();
                leggi.CommandType = System.Data.CommandType.Text;
                leggi.CommandText = $"SELECT e.ID, e.Nome, c.Nome as [Categoria], e.Livello, e.PuntiAccumulati, "+
                    "a.Nome as [Arma], a.PuntiDanno FROM Eroe e INNER JOIN Categoria c ON e.CategoriaID = c.ID "+
                    "INNER JOIN Arma a ON a.ID = e.ArmaID WHERE UsernameOwner =@UsernameOwner";
                leggi.CommandType = System.Data.CommandType.Text;
                leggi.Parameters.AddWithValue("@UsernameOwner", giocatore.Username);


                SqlDataReader reader = leggi.ExecuteReader();

                if (!reader.HasRows)
                {
                    hasEroes = false;
                    return hasEroes;
                }

                Console.WriteLine();
                Console.WriteLine("{0,-10}{1,-20}{2,-20}{3,-10}{4,-20}{5,-20}{6,-20}", "ID", "Nome","Categoria","Livello","Punti Accumulati", "Arma", "Punti danno");
                Console.WriteLine(new String('_', 120));
        
                while (reader.Read())
                {
                    Console.WriteLine(
                        "{0,-10}{1,-20}{2,-20}{3,-10}{4,-20}{5,-20}{6,-20}",
                        reader["ID"],
                        reader["Nome"],
                        reader["Categoria"],
                        reader["Livello"],
                        reader["PuntiAccumulati"],
                        reader["Arma"],
                        reader["PuntiDanno"]
                        );
                }

                conn.Close();
                return hasEroes;
            }        
        }

        public bool EliminaEroe(Eroe eroe)
        {
            bool isEliminated = false;
            using (SqlConnection conn = new SqlConnection(GiocatoreRepository.connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Errore di connessione...");

                SqlDataAdapter eroeAdapter = new();
                DataSet dsEroe = new DataSet();

                SqlCommand selectEroe = new SqlCommand("SELECT * FROM Eroe", conn);

                eroeAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                eroeAdapter.SelectCommand = selectEroe;

                eroeAdapter.DeleteCommand = GetEroeDeleteCommand(conn);

                eroeAdapter.Fill(dsEroe, "Eroe"); 

                conn.Close(); 

                DataRow rowToChange = dsEroe.Tables["Eroe"].Rows.Find(eroe.ID);
                if (rowToChange != null)
                {
                    rowToChange.Delete();
                    isEliminated = true;
                    eroeAdapter.Update(dsEroe, "Eroe");
                }
            }
            return isEliminated;
        }

        public void MostraClassificaGlobale()
        {
            using (SqlConnection conn = new SqlConnection(GiocatoreRepository.connectionString))
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open)
                    Console.WriteLine("Errore di connessione...");

                SqlCommand leggi = conn.CreateCommand();
                leggi.CommandType = System.Data.CommandType.Text;
                leggi.CommandText = $"SELECT * FROM( SELECT Row_NUMBER() OVER(ORDER BY e.Livello DESC, e.PuntiAccumulati DESC) "
                    +"as [#], e.Nome, c.Nome as [Categoria], e.Livello, e.PuntiAccumulati, a.Nome as [Arma], a.PuntiDanno, "+
                    "g.Username as [Utente] FROM Eroe e INNER JOIN Categoria c ON e.CategoriaID = c.ID INNER JOIN Arma a " +
                    "ON a.ID = e.ArmaID INNER JOIN Giocatore g ON g.Username = e.UsernameOwner) tab WHERE tab.# BETWEEN 1 AND 10";
                leggi.CommandType = System.Data.CommandType.Text;

                SqlDataReader reader = leggi.ExecuteReader();

                Console.WriteLine();
                Console.WriteLine("{0,-10}{1,-20}{2,-20}{3,-10}{4,-20}{5,-20}{6,-20}{7,-20}", "#", "Nome", "Categoria", "Livello", "Punti Accumulati", "Arma", "Punti danno", "Utente");
                Console.WriteLine(new String('_', 140));

                while (reader.Read())
                {
                    //int i = reader.GetInt32(0);
                    //DateTime nascita = reader.GetDateTime(3);

                    Console.WriteLine(
                        "{0,-10}{1,-20}{2,-20}{3,-10}{4,-20}{5,-20}{6,-20}{7,-20}",
                        reader["#"],
                        reader["Nome"],
                        reader["Categoria"],
                        reader["Livello"],
                        reader["PuntiAccumulati"],
                        reader["Arma"],
                        reader["PuntiDanno"],
                        reader["Utente"]
                        );
                }

                conn.Close();
            }
        }

        public Eroe SelezionaEroePerID(Eroe eroe, Giocatore giocatore)
        {
            Eroe eroeScelto = new Eroe();
            eroeScelto.UsernameOwner = giocatore.Username;
            CategoriaEroe categoriaEroe = new CategoriaEroe();
            Arma armaEroe;
            using (SqlConnection conn = new SqlConnection(GiocatoreRepository.connectionString))
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open)
                    Console.WriteLine("Errore di connessione...");

                SqlCommand leggi = conn.CreateCommand();
                leggi.CommandType = System.Data.CommandType.Text;
                leggi.CommandText = $"SELECT e.ID, e.Nome, c.ID as[IDCategoria], c.Nome as [Categoria],e.Livello, e.PuntiAccumulati, " +
                    $"a.ID as [IDArma], a.Nome as [Arma], a.PuntiDanno FROM Eroe e INNER JOIN Categoria c ON e.CategoriaID = c.ID " +
                    $"INNER JOIN Arma a ON a.ID = e.ArmaID INNER JOIN Giocatore g ON g.Username = e.UsernameOwner " +
                    $"WHERE e.ID = @ID AND g.Username = @Username";

                leggi.CommandType = System.Data.CommandType.Text;
                leggi.Parameters.AddWithValue("@ID", eroe.ID);
                leggi.Parameters.AddWithValue("@Username", giocatore.Username);

                SqlDataReader reader = leggi.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                while (reader.Read())
                {
                    eroeScelto.ID = (int)reader.GetValue("ID");
                    eroeScelto.Nome = (string)reader.GetValue("Nome");
                    categoriaEroe.ID = (int)reader.GetValue("IDCategoria");
                    categoriaEroe.Nome = (string)reader.GetValue("Categoria");
                    eroeScelto.Livello = (int)reader.GetValue("Livello");
                    eroeScelto.PuntiAccumulati = (int)reader.GetValue("PuntiAccumulati");
                    if (categoriaEroe.Nome == "Guerriero")
                        armaEroe = new ArmaGuerriero();
                    else
                        armaEroe = new ArmaMago();

                    armaEroe.ID = (int)reader.GetValue("IDArma");
                    armaEroe.Nome = (string)reader.GetValue("Arma");
                    armaEroe.PuntiDanno=(int)reader.GetValue("PuntiDanno");

                    eroeScelto.Categoria = categoriaEroe;
                    eroeScelto.Arma = armaEroe;
                }

                conn.Close();

            }

            return eroeScelto;
        }

        public bool ModificaEroe(Eroe eroeScelto)
        {
            bool isModified = false;
            using (SqlConnection conn = new SqlConnection(GiocatoreRepository.connectionString))
            {

                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Errore di connessione...");

                SqlDataAdapter eroeAdapter = new();
                DataSet dsEroe = new DataSet();

                SqlCommand selectEroe = new SqlCommand("SELECT * FROM Eroe", conn);

                eroeAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey; 


                eroeAdapter.SelectCommand = selectEroe;

                eroeAdapter.UpdateCommand = GetEroeUpdateCommand(conn);

                eroeAdapter.Fill(dsEroe, "Eroe");

                conn.Close();

                DataRow rowToChange = dsEroe.Tables["Eroe"].Rows.Find(eroeScelto.ID);

                if (rowToChange != null)
                {

                    rowToChange["Livello"] = eroeScelto.Livello;
                    rowToChange["PuntiAccumulati"] = eroeScelto.PuntiAccumulati;

                    isModified = true;
                    eroeAdapter.Update(dsEroe, "Eroe");
                }
            }
            return isModified;
        }

        #region MetodiSupporto

        private static SqlCommand GetEroeInsertCommand(SqlConnection conn)
        {
            SqlCommand insertCommand = new SqlCommand();
            insertCommand.Connection = conn;

            insertCommand.CommandText = "INSERT INTO Eroe (Nome, CategoriaID, ArmaID, Livello, PuntiAccumulati, UsernameOwner)" +
                      "VALUES(@Nome,@CategoriaID,@ArmaID,@Livello,@PuntiAccumulati,@UsernameOwner)";


            insertCommand.CommandType = System.Data.CommandType.Text;

            insertCommand.Parameters.Add(
                new SqlParameter(
                    "@Nome",
                    SqlDbType.NVarChar,
                    20,
                    "Nome"
                    )
                );

            insertCommand.Parameters.Add(
                new SqlParameter(
                    "@CategoriaID",
                    SqlDbType.Int,
                    15,
                    "CategoriaID"
                    )
                );

            insertCommand.Parameters.Add(
                new SqlParameter(
                    "@ArmaID",
                    SqlDbType.Int,
                    15,
                    "ArmaID"
                    )
                );

            insertCommand.Parameters.Add(
                new SqlParameter(
                    "@Livello",
                    SqlDbType.Int,
                    15,
                    "Livello"
                    )
                );

            insertCommand.Parameters.Add(
                new SqlParameter(
                    "@UsernameOwner",
                    SqlDbType.NVarChar,
                    15,
                    "UsernameOwner"
                    )
                );

            insertCommand.Parameters.Add(
                new SqlParameter(
                    "@PuntiAccumulati",
                    SqlDbType.Int,
                    15,
                    "PuntiAccumulati"
                    )
                );

            return insertCommand;
        }

        private static SqlCommand GetEroeDeleteCommand(SqlConnection conn)
        {
            SqlCommand deleteCommand = new SqlCommand();
            deleteCommand.Connection = conn;

            deleteCommand.CommandText = "DELETE FROM Eroe WHERE ID = @ID";


            deleteCommand.CommandType = System.Data.CommandType.Text;

            deleteCommand.Parameters.Add(
                new SqlParameter(
                    "@ID",
                    SqlDbType.Int,
                    50, //va sempre messo, ma ha senso solo se ho a che fare con varchar o similari
                    "ID"
                    )
                );
            return deleteCommand;
        }

        private static SqlCommand GetEroeUpdateCommand(SqlConnection conn)
        {
            SqlCommand updateCommand = new SqlCommand();

            updateCommand.Connection = conn;

            updateCommand.CommandText = "UPDATE Eroe " +
                      "SET Livello= @Livello, PuntiAccumulati= @PuntiAccumulati WHERE ID = @ID";


            updateCommand.CommandType = System.Data.CommandType.Text;

            updateCommand.Parameters.Add(
                new SqlParameter(
                    "@ID",
                    SqlDbType.Int,
                    50,
                    "ID"
                    )
                );

            updateCommand.Parameters.Add(
                new SqlParameter(
                    "@Livello",
                    SqlDbType.Int,
                    50, 
                    "Livello"
                    )
                );

            updateCommand.Parameters.Add(
                new SqlParameter(
                    "@PuntiAccumulati",
                    SqlDbType.Int,
                    50, 
                    "PuntiAccumulati"
                    )
                );
            return updateCommand;
        }

        #endregion
    }
}
