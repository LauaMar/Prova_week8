using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    class Menu
    {
        public static void Start()
        {

            int scelta = -1;
            bool uscita = false;
            while (!uscita)
            {
                Console.WriteLine();
                Console.WriteLine("-----------------");
                Console.WriteLine("MENU' PRINCIPALE");
                Console.WriteLine("-----------------");
                Console.WriteLine("Inserire il codice corrispondente all'azione che si desidera compiere: ");
                Console.WriteLine("[1] --> Accedi");
                Console.WriteLine("[2] --> Registrati");
                Console.WriteLine("[0] --> Esci");
                while (!int.TryParse(Console.ReadLine(), out scelta))
                {
                    Console.WriteLine("Codice inserito non corretto, riprova");
                }
                Giocatore giocatoreDB = new Giocatore();
                switch (scelta)
                {
                    case 1:
                        bool isLogged = MenuManager.Accedi(out giocatoreDB);
                        if (!isLogged)
                            Console.WriteLine("Nickname o password errati!");
                        break;
                    case 2:
                        bool isRegistered = MenuManager.Registrati(ref giocatoreDB);
                        if(isRegistered)
                            MenuNoAdmin.Start(giocatoreDB);
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
            Console.WriteLine("====Alla prossima partita!====");
        }
    }
}
