using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Carpeta
    {
        public int Id { get; set; }

        public string Ruta { get; set; }

        public int UsuarioId { get; set; }

        public int CarpetaContenedoraId { get; set; }
    }
}
