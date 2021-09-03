using BusinessLayer.Entities;
using DbProject;
using System;

namespace Servizi
{
    public class ServiziMenu
    {
      
        public static Giocatore AccessoGiocatore(Giocatore giocatore, out bool isLogged)
        {
            GiocatoreRepository gr = new GiocatoreRepository();
            isLogged = false;
            Giocatore giocatoreDB = new Giocatore();
            giocatoreDB = gr.VerificaGiocatore(giocatore);
            if (giocatoreDB == null)
            {
                return giocatoreDB;
            }
            if (giocatoreDB.Psswrd == giocatore.Psswrd)
                isLogged = true;
        
            return giocatoreDB;
        }

        public static bool RegistraGiocatore(Giocatore giocatore)
        {
            GiocatoreRepository gr = new GiocatoreRepository();
            bool isRegistered = false;
            try
            {
                gr.AggiungiGiocatore(giocatore);
                Console.WriteLine("Registrazione avvenuta con successo!");
                isRegistered = true;
            }
            catch(System.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex.Message + "\n");
                Console.WriteLine($"Il nome utente {giocatore.Username} è già in uso!");
            }

            return isRegistered;

        }

        internal static bool ModificaGiocatore(Giocatore giocatore, Eroe eroe, ref bool levelUserChanged)
        {
            GiocatoreRepository gr = new GiocatoreRepository();
            bool UserModified = false;
            if (eroe.Livello == 3 && giocatore.IsAdmin == false)
            {
                giocatore.IsAdmin = true;
                levelUserChanged = true;
                UserModified = gr.ModificaGiocatore(giocatore);
            }
            return UserModified;
        }
    }
}
