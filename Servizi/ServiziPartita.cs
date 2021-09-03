using BusinessLayer.Entities;
using DbProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servizi
{
    public class ServiziPartita
    {
        static EroeRepository er = new EroeRepository();

        public static int CalcolaPuntiVita(Personaggio personaggio)
        {
            int livello = personaggio.Livello;
            int puntiVita = 0;
            switch (livello)
            {
                case 1:
                    puntiVita = 20;
                    break;
                case 2:
                    puntiVita = 40;
                    break;
                case 3:
                    puntiVita = 60;
                    break;
                case 4:
                    puntiVita = 80;
                    break;
                case 5:
                    puntiVita = 100;
                    break;
                default:
                    puntiVita = -1;
                    break;
            }
            return puntiVita;
        }

        public static void EroeAttacca(Eroe eroe, ref int puntiVitaMostro)
        {
            puntiVitaMostro -= eroe.Arma.PuntiDanno;
        }

        public static bool EroeFugge()
        {
            bool isEscaped = false;
            Random Fuga = new Random();
            int scappa = Fuga.Next(0, 2);
            if (scappa == 1)
                isEscaped = true;

            return isEscaped;
        }

        public static void MostroAttacca(Mostro mostro, ref int puntiVitaEroe)
        {
            puntiVitaEroe -= mostro.Arma.PuntiDanno;
        }

        public static (bool, bool, bool, bool) Vittoria(Eroe eroe, Mostro mostro,Giocatore giocatore)
        {
            //aggiornare il totale di punti accumulati
            bool levelUserChanged = false;
            bool isUserModified = false;
            ServiziPersonaggio.AggiungiPuntiEroe(eroe, mostro);
            //verificare se l'eroe cambia livello
           bool levelHeroChanged = ServiziPersonaggio.CheckLivelloEroe(eroe);
            //nel caso l'eroe cambi di livello, verificare se il giocatore diventa Admin
            // ed eventualmente modifica il dato del giocatore sul db
            if (levelHeroChanged)
               isUserModified = ServiziMenu.ModificaGiocatore(giocatore, eroe, ref levelUserChanged);
            //funzione che modifica il dato dell'eroe sul db
            bool isHeroModified = er.ModificaEroe(eroe);

            return (levelHeroChanged, levelUserChanged, isHeroModified, isUserModified);

        }
    }
}
