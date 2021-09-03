using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public abstract class Personaggio
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public Categoria Categoria { get; set; }
        public Arma Arma{ get; set; }
        public int Livello { get; set; }
    }
    public class Eroe :Personaggio
    {
        
        public int PuntiAccumulati { get; set; }
        public string UsernameOwner { get; set; }
    }

    // public enum CategoriaEroe
    //{
    //    Guerriero,
    //    Mago
    //}



}
