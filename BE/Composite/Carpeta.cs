using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Composite
{
    public class Carpeta : Componente
    {
        private List<Componente> _hijos;
        public Carpeta()
        {
            _hijos = new List<Componente>();
        }

        public override void AgregarHijo(Componente c)
        {
            _hijos.Add(c);
        }

        public override IList<Componente> ObtenerHijos()
        {
            return _hijos.ToArray();
        }
    }
}
