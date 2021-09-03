using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ElencoArmi
    {
        public static List<Arma> ElencoArmiGuerriero = new List<Arma>
        {
            new ArmaGuerriero{ID=101,Nome="Alabarda", PuntiDanno = 15},
            new ArmaGuerriero{ID=102,Nome="Ascia", PuntiDanno = 8},
            new ArmaGuerriero{ID=103,Nome="Mazza", PuntiDanno = 5},
            new ArmaGuerriero{ID=104,Nome="Spada", PuntiDanno = 10},
            new ArmaGuerriero{ID=105,Nome="Spadone", PuntiDanno = 15},

        };

        public static List<Arma> ElencoArmiMago = new List<Arma>
        {
            new ArmaMago{ID=201, Nome="Arco e frecce", PuntiDanno = 8},
            new ArmaMago{ID=202, Nome="Bacchetta", PuntiDanno = 5},
            new ArmaMago{ID=203, Nome="Bastone magico", PuntiDanno = 10},
            new ArmaMago{ID=204, Nome="Onda d'urto", PuntiDanno = 15},
            new ArmaMago{ID=205, Nome="Pugnale", PuntiDanno = 5},

        };

        public static List<Arma> ElencoArmiCultista = new List<Arma>
        {
            new ArmaCultista{ID=301, Nome="Discorso noioso", PuntiDanno = 4},
            new ArmaCultista{ID=302, Nome="Farneticazione", PuntiDanno = 7},
            new ArmaCultista{ID=303, Nome="Imprecazione", PuntiDanno = 5},
            new ArmaCultista{ID=304, Nome="Magia nera", PuntiDanno = 3},
        };

        public static List<Arma> ElencoArmiOrco = new List<Arma>
        {
            new ArmaOrco{ID=401, Nome ="Arco", PuntiDanno =7},
            new ArmaOrco{ID=402, Nome ="Clava", PuntiDanno = 5},
            new ArmaOrco{ID=403, Nome ="Spada rotta", PuntiDanno = 3},
            new ArmaOrco{ID=404, Nome ="Mazza chiodata", PuntiDanno = 10},
        };

        public static List<Arma> ElencoArmiSigMale = new List<Arma>
        {
            new ArmaSigMale{ID=501, Nome ="Alabarda del drago", PuntiDanno = 30},
            new ArmaSigMale{ID=502, Nome ="Divinazione", PuntiDanno = 15},
            new ArmaSigMale{ID=503, Nome ="Fulmine", PuntiDanno = 10},
            new ArmaSigMale{ID=504, Nome ="Fulmine celeste", PuntiDanno = 15},
            new ArmaSigMale{ID=505, Nome ="Tempesta", PuntiDanno = 8},
            new ArmaSigMale{ID=506, Nome ="Tempesta oscura", PuntiDanno = 15},
        };
    }
}
