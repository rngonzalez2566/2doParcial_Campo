using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Composite
{
    public class Archivo : Componente
    {
        public string Tamanio { get; set; }

        public override void AgregarHijo(Componente c)
        {

        }

        public override IList<Componente> ObtenerHijos()
        {
            return null;
        }
    }
}
