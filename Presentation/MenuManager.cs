using BusinessLayer;
using BusinessLayer.Entities;
using Servizi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Presentation
{
    public class MenuManager
    {
        //Richiedere solamente i dati e verificare che possano essere corretti...
        //Le operazioni vanno svolte nella classe BusinessLayer
        internal static bool Accedi(out Giocatore giocatoreDB)
        {
            Console.WriteLine("====Accedi====");
            Giocatore giocatore = new Giocatore();

            string username = String.Empty;
            while (string.IsNullOrEmpty(username) || username.Length>15)
            {
                Console.WriteLine("Inserisci il tuo username (massimo 15 caratteri):");
                username = Console.ReadLine();
            }

            string password = String.Empty;
            do
            {
                Console.WriteLine("Inserisci la tua password (massimo 15 caratteri):");
                password = Console.ReadLine();
            } while (string.IsNullOrEmpty(password) || password.Length>15);

            giocatore.Username = username;

            giocatore.Psswrd = password;

            bool isLogged = false;
            /*Giocatore*/ giocatoreDB = ServiziMenu.AccessoGiocatore(giocatore, out isLogged);
            if(isLogged)
            {
                if (giocatoreDB.IsAdmin)
                    MenuAdmin.Start(giocatoreDB);
                else
                    MenuNoAdmin.Start(giocatoreDB);
            }
            return isLogged;
                
        }

        internal static bool Registrati(ref Giocatore giocatore)
        {
            Console.WriteLine("====Registrati====");
            do
            {
                Console.WriteLine("Inserisci il tuo username (massimo 15 caratteri):");
                giocatore.Username = Console.ReadLine();
            } while (string.IsNullOrEmpty(giocatore.Username) || giocatore.Username.Length > 15);

            do
            {
                Console.WriteLine("Inserisci la tua password (massimo 15 caratteri):");
                giocatore.Psswrd = Console.ReadLine();
            } while (string.IsNullOrEmpty(giocatore.Psswrd) || giocatore.Psswrd.Length > 15);

            return ServiziMenu.RegistraGiocatore(giocatore);

        }

        internal static void CreaEroe(Giocatore giocatore)
        {
            Console.WriteLine("====Crea un nuovo eroe ====");
            Eroe eroeNew = new Eroe();
            while (string.IsNullOrEmpty(eroeNew.Nome)||eroeNew.Nome.Length>20)
            {
                Console.WriteLine("Inserisci il nome del tuo eroe (massimo 20 caratteri):");
                eroeNew.Nome = Console.ReadLine();
            }

            Console.WriteLine("Scegli la categoria del tuo eroe:");
            eroeNew.Categoria = ServiziPersonaggio.ScegliCategoriaPersonaggio('e');
            //Console.WriteLine("[1] --> Guerriero,");
            //Console.WriteLine("[2] --> Mago.");
            //while(!(int.TryParse(Console.ReadLine(), out catEroe) && (catEroe==1 || catEroe==2)))
            //{
            //    Console.WriteLine("Hai inserito un valore che non corrisponde a nessuna opzione!");
            //}
            //eroeNew.CategoriaE = (CategoriaEroe)(catEroe - 1);

            Arma armaScelta = null;
            Console.WriteLine($"Scegli l'arma per il tuo {eroeNew.Categoria.Nome}:");
            if (eroeNew.Categoria.Nome == null)
                return;
            else if (eroeNew.Categoria.Nome == "Guerriero")
            {
                armaScelta = ServiziPersonaggio.ScegliArmaPersonaggio('g');
            }
            else if (eroeNew.Categoria.Nome == "Mago")
            {
                armaScelta = ServiziPersonaggio.ScegliArmaPersonaggio('m');
            }
            eroeNew.Arma = armaScelta;

            bool isSuccess = ServiziPersonaggio.AggiuntaEroe(eroeNew, giocatore);

            if(isSuccess)
            { 
                Console.WriteLine("Eroe inserito");
            }            
           
        }

        internal static void EliminaEroe(Giocatore giocatore)
        {
            Console.WriteLine("====Elimina un eroe====");
            Console.WriteLine("I tuoi eroi sono: ");
            Console.WriteLine();
            bool hasHeroes= ServiziPersonaggio.FiltraEroiPerGiocatore(giocatore);
            if (!hasHeroes)
            {
                Console.WriteLine(); 
                Console.WriteLine("Non hai nessun eroe!");
                return;
            }
            int sceltaID = -1;
            Eroe eroeDaCancellare = new Eroe();
            Console.WriteLine();
            Console.WriteLine("Inserisci l'ID dell'eroe da eliminare:");
            while (!(int.TryParse(Console.ReadLine(), out sceltaID) && sceltaID > 0))
                Console.WriteLine("L'ID inserito non è valido!");

            eroeDaCancellare.ID = sceltaID;
            bool isEliminated = ServiziPersonaggio.EliminazioneEroe(eroeDaCancellare);
            if (isEliminated)
                Console.WriteLine("L'eroe è stato cancellato");
            else
                Console.WriteLine("Non è stato possibile cancellare l'eroe scelto.");
        }

        internal static void CreaMostro()
        {
            Console.WriteLine("====Crea un nuovo mostro====");
            Mostro mostroNew = new Mostro();
            while (string.IsNullOrEmpty(mostroNew.Nome)||mostroNew.Nome.Length>20)
            {
                Console.WriteLine("Inserisci il nome del mostro (massimo 20 caratteri):");
                mostroNew.Nome = Console.ReadLine();
            }

            Console.WriteLine("Scegli la categoria del mostro:");
            mostroNew.Categoria = ServiziPersonaggio.ScegliCategoriaPersonaggio('m');

            Arma armaScelta = null;
            Console.WriteLine($"Scegli l'arma per il tuo {mostroNew.Categoria.Nome}:");
            if (mostroNew.Categoria.Nome == null)
                return;
            else if (mostroNew.Categoria.Nome == "Cultista")
            {
                armaScelta = ServiziPersonaggio.ScegliArmaPersonaggio('c');
            }
            else if (mostroNew.Categoria.Nome == "Orco")
            {
                armaScelta = ServiziPersonaggio.ScegliArmaPersonaggio('o');
            }
            else if (mostroNew.Categoria.Nome == "Signore del male")
            {
                armaScelta = ServiziPersonaggio.ScegliArmaPersonaggio('s');
            }
            mostroNew.Arma = armaScelta;
            if (armaScelta == null)
                return;

            int livelloScelto = -1;
            do
                Console.WriteLine("Scegli il livello del mostro (intero compreso tra 1 e 5):");
            while (!(int.TryParse(Console.ReadLine(), out livelloScelto) && livelloScelto >= 1 && livelloScelto <= 5));

            mostroNew.Livello = livelloScelto;

            bool isSuccess = ServiziPersonaggio.AggiuntaMostro(mostroNew);

            if (isSuccess)
            {
                Console.WriteLine("Mostro inserito");
            }
        }

        internal static void MostraClassificaGlobale()
        {
            Console.WriteLine("====Classifica Globale====");
            Console.WriteLine();
            Console.WriteLine("I 10 migliori eroi sono: ");
            Console.WriteLine();
            ServiziPersonaggio.MostraClassificaGlobale();

        }

        internal static void Gioca(Giocatore giocatore)
        {
            bool nuovaPartita = true;
            bool cambiaEroe = true;
            Eroe eroePrecedente = new Eroe();
            Eroe eroeScelto = new Eroe();
            do
            {
                Console.WriteLine("====Inizia la partita!====");
                Console.WriteLine();
                //selezione eroe 
                eroeScelto = EroePartita(cambiaEroe, giocatore, eroeScelto, eroePrecedente);
                if (eroeScelto == null)
                    return;

                Console.WriteLine($"Hai scelto di usare il {eroeScelto.Categoria.Nome} {eroeScelto.Nome}!");
                Console.WriteLine("Che l'avventura abbia inizio...");

                //Sorteggio del mostro
                Mostro mostroSorteggiato = ServiziPersonaggio.SorteggiaMostro(eroeScelto);
                if (mostroSorteggiato == null)
                {
                    Console.WriteLine("Spiacenti, non ci sono mostri al tuo livello!");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine($"Sfiderai {mostroSorteggiato.Nome}, che è un {mostroSorteggiato.Categoria.Nome} di livello {mostroSorteggiato.Livello}...");
                Console.WriteLine($"La sua arma è {mostroSorteggiato.Arma.Nome} (Punti danno: {mostroSorteggiato.Arma.PuntiDanno})");
                Console.WriteLine();

                //Partita
                int winner = Partita(eroeScelto, mostroSorteggiato);

                // Aggiornamento punteggi e livelli

                AggiornaEroeGiocatore(winner, giocatore, eroeScelto, mostroSorteggiato);

                // nuova partita?
                int giocaAncora = -1;
                Console.WriteLine("Vuoi giocare ancora?");
                Console.WriteLine("[1] --> Gioca una nuova partita");
                Console.WriteLine("[0] --> Esci");
                while (!(int.TryParse(Console.ReadLine(), out giocaAncora) && (giocaAncora == 1 || giocaAncora == 0)))
                    Console.WriteLine("Scelta effettuata non valida!");
                switch(giocaAncora)
                {
                    case 1:
                        eroePrecedente = CambiaEroe(ref cambiaEroe, eroeScelto);
                        break;
                    case 0:
                        nuovaPartita = false;
                        break;
                    default:
                        Console.WriteLine("Scelta effettuata non valida!");
                        break;
                }

            } while (nuovaPartita);
            Console.WriteLine();


        }

        public static Eroe EroePartita(bool cambiaEroe, Giocatore giocatore,Eroe eroeScelto, Eroe eroePrecedente)
        {
            if (cambiaEroe)
            {
                //Scelta del giocatore da parte dell'utente
                Console.WriteLine("Scegli con quale eroe giocare (inserisci il suo id):");
                bool hasHeroes = ServiziPersonaggio.FiltraEroiPerGiocatore(giocatore);
                if (!hasHeroes)
                {
                    Console.WriteLine();
                    Console.WriteLine("ATTENZIONE: Per poter giocare devi avere almeno un eroe!");
                    return null;
                }
                Console.WriteLine();
                int sceltaID = -1;
                while (!(int.TryParse(Console.ReadLine(), out sceltaID) && sceltaID > 0))
                    Console.WriteLine("L'ID inserito non è valido!");

                eroeScelto.ID = sceltaID;
                eroeScelto = ServiziPersonaggio.SelezionaEroe(giocatore, cambiaEroe, eroeScelto);
                if (eroeScelto == null)
                {
                    Console.WriteLine("Non è stato possibile trovare l'eroe scelto!");
                    return null;
                }
                Console.WriteLine();
            }
            else
            {
                eroeScelto = eroePrecedente;
            }
            return eroeScelto;
        }

        public static int Partita(Eroe eroe, Mostro mostro)
        {
            int winner = -1;
            int puntiVitaEroe = ServiziPartita.CalcolaPuntiVita(eroe);
            int puntiVitaMostro = ServiziPartita.CalcolaPuntiVita(mostro);
            while(puntiVitaEroe >0 && puntiVitaMostro >0)
            {
                Console.WriteLine("Tocca a te!");
                Console.WriteLine("[1] --> Attacca");
                Console.WriteLine("[2] --> Fuggi");
                Console.WriteLine();
                int azioneScelta = 0;
                while (!(int.TryParse(Console.ReadLine(), out azioneScelta) && (azioneScelta == 1 || azioneScelta == 2)))
                    Console.WriteLine("Il codice inserito non è valido");
                switch (azioneScelta)
                {
                    case 1:
                        //attacca
                        ServiziPartita.EroeAttacca(eroe, ref puntiVitaMostro);
                        Console.WriteLine("Hai attaccato il mostro!");
                        Console.WriteLine($"punti vita mostro: {puntiVitaMostro}");
                        break;
                    case 2:
                        //fuggi
                        bool isEscaped = ServiziPartita.EroeFugge();
                        if (isEscaped)
                            return winner = 0;
                        else
                            Console.WriteLine("Spiacenti, la tua fuga non è riuscita...");
                        break;
                    default:
                        Console.WriteLine("Non hai scelto nessuna azione");
                        return -1;
                        break;
                }
                Console.WriteLine();
                if (puntiVitaMostro <= 0)
                    break;
                //attacca il mostro
                ServiziPartita.MostroAttacca(mostro, ref puntiVitaEroe);
                Console.WriteLine("Il mostro ti ha attaccato!");
                Console.WriteLine($"punti vita eroe: {puntiVitaEroe}");
            }
            if (puntiVitaMostro <= 0)
                winner = 2;
            else if (puntiVitaEroe <= 0)
                winner = 1;

            return winner;
        }

        public static void AggiornaEroeGiocatore(int winner, Giocatore giocatore, Eroe eroeScelto, Mostro mostroSorteggiato)
        {
            if (winner == 2) //il giocatore ha vinto
            {
                Console.WriteLine("====Complimenti, hai vinto!====");
                (bool levelHeroChanged, bool levelUserChanged, bool isHeroModified, bool isUserModified)
                     = ServiziPartita.Vittoria(eroeScelto, mostroSorteggiato, giocatore);
                if (!isHeroModified)
                {
                    Console.WriteLine("Impossibile aggiornare i dati dell'eroe sul DataBase");
                    Console.WriteLine();
                }
                if (!isUserModified && levelUserChanged)
                {
                    Console.WriteLine("Impossibile aggiornare i dati del giocatore sul DataBase");
                    Console.WriteLine();
                }
                if (levelHeroChanged)
                {
                    Console.WriteLine($"====Il tuo eroe ha raggiunto il livello {eroeScelto.Livello}====");
                    Console.WriteLine();
                }
                if (levelUserChanged)
                {
                    Console.WriteLine($"====Sei diventato un Admin!====");
                    Console.WriteLine();
                }
                Console.WriteLine($"Il tuo eroe {eroeScelto.Nome} ha adesso accumulato {eroeScelto.PuntiAccumulati} punti!");
                Console.WriteLine();
            }
            else if (winner == 1) //il giocatore ha perso
            {
                Console.WriteLine("====Peccato, il mostro ha avuto la meglio su di te!====");
                Console.WriteLine($"Il tuo eroe {eroeScelto.Nome} mantiene i suoi {eroeScelto.PuntiAccumulati} punti!");
            }
            else if (winner == 0) //il giocatore è fuggito
            {
                Console.WriteLine("====Hai avuto fortuna, sei riuscito a fuggire...====");
                bool isSuccessful = ServiziPersonaggio.SottraiPuntiEroe(eroeScelto, mostroSorteggiato);
                if (!isSuccessful)
                    Console.WriteLine("Ho riscontrato qualche problema nell'aggiornare i dati dell'eroe...");

                Console.WriteLine($"Il tuo eroe {eroeScelto.Nome} attualmente ha {eroeScelto.PuntiAccumulati} punti!");
            }
            else //non dovrebbe accadere, però...
            {
                Console.WriteLine("====Errore partita!====");
                return;
            }
        }
        public static Eroe CambiaEroe(ref bool cambiaEroe, Eroe eroeScelto)
        {
            Eroe eroePrecedente = new Eroe();
            Console.WriteLine("Con quale eroe vuoi giocare?");
            Console.WriteLine("[1] --> Cambia eroe");
            Console.WriteLine("[2] --> Continua con lo stesso eroe");
            int scelta = -1;
            while (!(int.TryParse(Console.ReadLine(), out scelta) && (scelta == 1 || scelta == 2)))
                Console.WriteLine("Scelta non valida!");

            switch(scelta)
            {
                case 1:
                    cambiaEroe = true;
                    break;
                case 2:
                    cambiaEroe = false;
                    eroePrecedente = eroeScelto;
                    break;
                default:
                    Console.WriteLine("Scelta non valida!");
                    break;
            }
            return eroePrecedente;



        }
    }
}
