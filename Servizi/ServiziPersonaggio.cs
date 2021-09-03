using BusinessLayer;
using BusinessLayer.Entities;
using DbProject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Servizi
{
    public class ServiziPersonaggio
    {
        static EroeRepository er = new EroeRepository();
        static MostroRepository mr = new MostroRepository();
        public static Categoria ScegliCategoriaPersonaggio(char v)
        {
            List<Categoria> currElencoCategorie = new List<Categoria>();
            switch (v)
            {
                case 'e': //eroe
                    currElencoCategorie = ElencoCategorie.ElencoCatEroe;
                    break;
                case 'm': //mostro
                    currElencoCategorie = ElencoCategorie.ElencoCatMostro;
                    break;
                default:
                    Console.WriteLine("Errore! Il codice inserito non corrisponde a nessuna categoria!");
                    return null;
                    break;

            }
            int catScelta = 0;
            Console.WriteLine("Puoi scegliere tra:");
            foreach (Categoria a in currElencoCategorie)
            {
                Console.WriteLine(a.ToString());
            }
            bool isCorrect = true;
            do
            {
                while (!(int.TryParse(Console.ReadLine(), out catScelta) && catScelta > 0))
                {
                    Console.WriteLine("hai inserito un codice non valido, riprova");
                }
                foreach (Categoria a in currElencoCategorie)
                {
                    if (a.ID == catScelta)
                    {
                        isCorrect = true;
                        return a;
                    }
                }
                Console.WriteLine("Nessuna categoria corrisponde all'ID inserito, riprova:");
                isCorrect = false;
            } while (!isCorrect);
            return null;
        }

        public static Arma ScegliArmaPersonaggio(char v)
        {
            List<Arma> currElencoArmi = new List<Arma>();
            switch (v)
            {
                case 'g': //guerriero
                    currElencoArmi = ElencoArmi.ElencoArmiGuerriero;
                    break;
                case 'm': //mago
                    currElencoArmi = ElencoArmi.ElencoArmiMago;
                    break;
                case 'c': //cultista
                    currElencoArmi = ElencoArmi.ElencoArmiCultista;
                    break;
                case 'o': //orco
                    currElencoArmi = ElencoArmi.ElencoArmiOrco;
                    break;
                case 's': //signore del male
                    currElencoArmi = ElencoArmi.ElencoArmiSigMale;
                    break;
                default:
                    Console.WriteLine("Errore! Il codice inserito non corrisponde a nessun personaggio!");
                    return null;
                    break;

            }
            int armaScelta = 0;
            Console.WriteLine("Puoi scegliere tra:");
            foreach (Arma a in currElencoArmi)
            {
                Console.WriteLine(a.ToString());
            }
            bool isCorrect = true;
            do
            {
                while (!(int.TryParse(Console.ReadLine(), out armaScelta) && armaScelta > 0))
                {
                    Console.WriteLine("hai inserito un codice non valido, riprova");
                }
                foreach (Arma a in currElencoArmi)
                {
                    if (a.ID == armaScelta)
                    {
                        isCorrect = true;
                        return a;
                    }
                }
                Console.WriteLine("Nessun'arma corrisponde all'ID inserito");
                isCorrect = false;
            } while (!isCorrect);
            return null;
        }

        public static bool AggiuntaEroe(Eroe eroeNew, Giocatore giocatore)
        {
            bool isSuccessful = false;
            eroeNew.Livello = 1;
            eroeNew.PuntiAccumulati = 0;
            eroeNew.UsernameOwner = giocatore.Username;

            try
            {
                er.AggiungiEroe(eroeNew);
                isSuccessful = true;
            }
            catch(SqlException exc)
            {
                Console.WriteLine(exc.Message);
            }


            return isSuccessful;
            
        }

        public static bool FiltraEroiPerGiocatore(Giocatore giocatore)
        {
           return er.MostraEroiPerGiocatore(giocatore);
        }

        public static bool EliminazioneEroe(Eroe eroeDaCancellare)
        {
            bool isEliminated =false;
            try
            {
               isEliminated = er.EliminaEroe(eroeDaCancellare);

            } catch(SqlException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return isEliminated;
        }

        public static bool AggiuntaMostro(Mostro mostroNew)
        {
            bool isSuccessful = false;
            try
            {
                mr.AggiungiMostro(mostroNew);
                isSuccessful = true;
            }
            catch (SqlException exc)
            {
                Console.WriteLine(exc.Message);
            }
            return isSuccessful;
        }

        public static void MostraClassificaGlobale()
        {
            er.MostraClassificaGlobale();
        }

        public static Eroe SelezionaEroe(Giocatore giocatore, bool cambiaEroe, Eroe eroeScelto )
        {
            if (cambiaEroe)
            {
                eroeScelto = er.SelezionaEroePerID(eroeScelto, giocatore);
                return eroeScelto;
            }
            else
                return eroeScelto;
        }

        public static Mostro SorteggiaMostro(Eroe eroeScelto)
        {
            Mostro mostroSorteggiato = new Mostro();
            List<Mostro> MostriSfidabili = mr.FiltraMostriPerLivello(eroeScelto);
            if (MostriSfidabili.Count == 0)
            {
                return null;
            }
            else if (MostriSfidabili.Count == 1)
            {
                mostroSorteggiato = MostriSfidabili[0];
                return mostroSorteggiato;
            }

            Random Sorteggio = new Random();
            int posSorteggiato = Sorteggio.Next(0, MostriSfidabili.Count);
            mostroSorteggiato = MostriSfidabili[posSorteggiato];

            return mostroSorteggiato;
        }

        public static void AggiungiPuntiEroe(Eroe eroeScelto, Mostro mostroSorteggiato)
        {
            int livelloMostro = mostroSorteggiato.Livello;
            int puntiAccumulati = livelloMostro * 10;
            eroeScelto.PuntiAccumulati += puntiAccumulati;
        }

        public static bool SottraiPuntiEroe(Eroe eroeScelto, Mostro mostroSorteggiato)
        {
            int livelloMostro = mostroSorteggiato.Livello;
            int puntiDaSottrarre = livelloMostro * 5;
            eroeScelto.PuntiAccumulati -= puntiDaSottrarre;
            if (eroeScelto.PuntiAccumulati < 0)
                eroeScelto.PuntiAccumulati = 0;
            bool isSuccessful = er.ModificaEroe(eroeScelto);
            return isSuccessful;
        }

        public static bool CheckLivelloEroe(Eroe eroe)
        {
            bool levelChanged = false;
            int livello = eroe.Livello;
            int punti = eroe.PuntiAccumulati;
            if ((livello == 1 && punti > 29) || (livello == 2 && punti > 59) || (livello == 3 && punti > 89) || (livello == 4 && punti > 119))
            {
                eroe.Livello += 1;
                eroe.PuntiAccumulati = 0;
                levelChanged = true;
            }
            return levelChanged;
        }
    }
}
