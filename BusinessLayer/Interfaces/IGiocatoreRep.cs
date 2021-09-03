using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IGiocatoreRep
    {
        public Giocatore VerificaGiocatore(Giocatore giocatore);
        public void AggiungiGiocatore(Giocatore giocatore);
        public bool ModificaGiocatore(Giocatore giocatore);

    }
}
