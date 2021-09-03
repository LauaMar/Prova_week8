using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public abstract class Arma
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int PuntiDanno { get; set; }
        public override string ToString()
        {
            return $"[{ID}] --> {Nome} (punti danno = {PuntiDanno})";
        }
    }

    public class ArmaGuerriero : Arma{ }
    
    public class ArmaMago : Arma{ }
    public class ArmaCultista : Arma{ }
    public class ArmaOrco : Arma { }
    public class ArmaSigMale : Arma { }
}
