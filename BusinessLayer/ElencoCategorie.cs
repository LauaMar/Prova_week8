using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ElencoCategorie
    {
        public static List<Categoria> ElencoCatEroe = new List<Categoria>
        {
            new CategoriaEroe{ID=101, Nome ="Guerriero"},
            new CategoriaEroe{ID=102, Nome ="Mago"},
        };
        public static List<Categoria> ElencoCatMostro = new List<Categoria>
        {
            new CategoriaMostro{ID = 201, Nome = "Cultista"},
            new CategoriaMostro{ID = 202, Nome = "Orco"},
            new CategoriaMostro{ID = 203, Nome = "Signore del male"},
        };
    }
}
