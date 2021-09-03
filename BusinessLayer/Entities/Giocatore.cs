using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class Giocatore
    {
        public string Username { get; set; }
        public string Psswrd { get; set; }
        public bool IsAdmin { get; set; }
    }
}
