using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
   public class MenuNoAdmin
    {
        public static void Start(Giocatore giocatoreDB)
        {
            int scelta = -1;
            bool uscita = false;
            while (!uscita)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("Menù di gioco utenti NON admin");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Inserire il codice corrispondente all'azione che si desidera compiere: ");
                Console.WriteLine("[1] --> Gioca");
                Console.WriteLine("[2] --> Crea nuovo Eroe");
                Console.WriteLine("[3] --> Elimina Eroe");
                Console.WriteLine("[0] --> Esci");
                while (!int.TryParse(Console.ReadLine(), out scelta))
                {
                    Console.WriteLine("Codice inserito non corretto, riprova");
                }

                switch (scelta)
                {
                    case 1:
                        MenuManager.Gioca(giocatoreDB);
                        break;
                    case 2:
                        try
                        {
                            MenuManager.CreaEroe(giocatoreDB);
                        }
                        catch (NullReferenceException exc)
                        {
                            Console.WriteLine(exc.Message);
                        }
                        break;
                    case 3:
                        MenuManager.EliminaEroe(giocatoreDB);
                        break;
                    case 0:
                        uscita = true;
                        break;
                    default:
                        Console.WriteLine("La scelta effettuata non corrisponde a nessuna azione!");
                        Console.WriteLine();
                        break;
                }

            }
        }
    }
}
