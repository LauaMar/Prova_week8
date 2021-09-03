using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IEroeRep
    {
        public void AggiungiEroe(Eroe eroe);
        public bool MostraEroiPerGiocatore(Giocatore giocatore);
        public bool EliminaEroe(Eroe eroe);
        public void MostraClassificaGlobale();
        public Eroe SelezionaEroePerID(Eroe eroe, Giocatore giocatore);
        public bool ModificaEroe(Eroe eroeScelto);


    }
}
