using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProject
{
    public class MostroRepository: IMostroRep
    {
        
        public void AggiungiMostro(Mostro mostro)
        {
            using (SqlConnection conn = new SqlConnection(GiocatoreRepository.connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Errore di connessione...");

                SqlDataAdapter mostroAdapter = new();
                DataSet dsMostro = new DataSet();

                SqlCommand selectMostro = new SqlCommand("SELECT * FROM Mostro", conn);

                mostroAdapter.SelectCommand = selectMostro;
                mostroAdapter.InsertCommand = GetMostroInsertCommand(conn);

                mostroAdapter.Fill(dsMostro, "Mostro");

                conn.Close();

                DataRow newRow = dsMostro.Tables["Mostro"].NewRow();

                newRow["Nome"] = mostro.Nome;
                newRow["CategoriaID"] = mostro.Categoria.ID;
                newRow["ArmaID"] = mostro.Arma.ID;
                newRow["Livello"] = mostro.Livello;

                dsMostro.Tables["Mostro"].Rows.Add(newRow);

                mostroAdapter.Update(dsMostro, "Mostro");

            }
        }
        public List<Mostro> FiltraMostriPerLivello(Eroe eroeScelto)
        {
            List<Mostro> mostriSfidabili = new List<Mostro>();
            
            Arma armaMostro;
            //CategoriaMostro catMostro = new CategoriaMostro();

            using (SqlConnection conn = new SqlConnection(GiocatoreRepository.connectionString))
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open)
                    Console.WriteLine("Errore di connessione...");

                SqlCommand leggi = conn.CreateCommand();
                leggi.CommandType = System.Data.CommandType.Text;
                leggi.CommandText = $"SELECT m.ID, m.Nome, c.ID as [IDCategoria], c.Nome as [Categoria], m.Livello, " +
                    "a.ID as [IDArma], a.Nome as [Arma], a.PuntiDanno FROM Mostro m INNER JOIN Categoria c " +
                    "ON m.CategoriaID = c.ID INNER JOIN Arma a ON m.ArmaID = a.ID WHERE Livello <= @Livello";

                leggi.CommandType = System.Data.CommandType.Text;
                leggi.Parameters.AddWithValue("@Livello", eroeScelto.Livello);

                SqlDataReader reader = leggi.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }
                while (reader.HasRows)
                {
                    
                    while (reader.Read())
                    {
                        Mostro currMostro = new Mostro();
                        CategoriaMostro catMostro = new CategoriaMostro();
                        currMostro.ID = (int)reader.GetValue("ID");
                        currMostro.Nome = (string)reader.GetValue("Nome");
                        catMostro.ID = (int)reader.GetValue("IDCategoria");
                        catMostro.Nome = (string)reader.GetValue("Categoria");
                        currMostro.Livello = (int)reader.GetValue("Livello");
                        if (catMostro.Nome == "Cultista")
                            armaMostro = new ArmaCultista();
                        else if (catMostro.Nome == "Orco")
                            armaMostro = new ArmaOrco();
                        else
                            armaMostro = new ArmaSigMale();

                        armaMostro.ID = (int)reader.GetValue("IDArma");
                        armaMostro.Nome = (string)reader.GetValue("Arma");
                        armaMostro.PuntiDanno = (int)reader.GetValue("PuntiDanno");

                        currMostro.Categoria = catMostro;
                        currMostro.Arma = armaMostro;
                        mostriSfidabili.Add(currMostro);

                    }
                    reader.NextResult();
                }
                conn.Close();

            }

            return mostriSfidabili;
        }

        #region Metodi di Supporto

        private static SqlCommand GetMostroInsertCommand(SqlConnection conn)
        {
            SqlCommand insertCommand = new SqlCommand();
            insertCommand.Connection = conn;

            insertCommand.CommandText = "INSERT INTO Mostro (Nome, CategoriaID, ArmaID, Livello)" +
                      "VALUES(@Nome,@CategoriaID,@ArmaID,@Livello)";


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

            return insertCommand;
        }

        #endregion
    }
}
