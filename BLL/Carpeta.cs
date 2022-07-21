using BLL.Conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Carpeta : Acceso
    {
        private const string CREAR_CARPETA = "INSERT INTO Carpeta (Ruta, UsuarioId, CarpetaContenedoraId) OUTPUT inserted.Id VALUES (@parRuta, @parUsuarioId, @parCarpetaContenedoraId)";

        public int CrearCarpeta(BE.Carpeta carpeta)
        {
            try
            {
                ExecuteCommandText = CREAR_CARPETA;

                ExecuteParameters.Parameters.Clear();

                ExecuteParameters.Parameters.AddWithValue("@parRuta", carpeta.Ruta);
                ExecuteParameters.Parameters.AddWithValue("@parUsuarioId", carpeta.UsuarioId);
                ExecuteParameters.Parameters.AddWithValue("@parCarpetaContenedoraId", carpeta.CarpetaContenedoraId);

                return ExecuteNonEscalar();
            }
            catch
            {
                throw new Exception("Error en la base de datos.");
            }
        }
    }
}
