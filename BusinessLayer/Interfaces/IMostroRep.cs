using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
   public interface IMostroRep
    {
        public void AggiungiMostro(Mostro mostro);
        public List<Mostro> FiltraMostriPerLivello(Eroe eroeScelto);
    }
}
