using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Composite
{
    public abstract class Componente
    {
        public string Nombre { get; set; }
        public abstract void AgregarHijo(Componente c);
        public abstract IList<Componente> ObtenerHijos();
    }
}
