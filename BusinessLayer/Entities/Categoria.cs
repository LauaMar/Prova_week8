using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public abstract class Categoria
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public override string ToString()
        {
            return $"[{ID}] --> {Nome}";
        }
    }

    public class CategoriaEroe : Categoria { }
    public class CategoriaMostro : Categoria { }


}
