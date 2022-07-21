using BLL.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Archivo : Acceso
    {
        public const string CREAR_ARCHIVO = "INSERT INTO Archivo (Nombre, Tamanio, CarpetaId) OUTPUT inserted.Id VALUES (@parNombre, @parTamanio, @parCarpetaId)";
        // public const string LISTAR_ARCHIVOS = "SELECT a.Nombre, a.Tamanio FROM Archivo a INNER JOIN Carpeta c on c.Id = a.CarpetaId WHERE c.UsuarioId = @parUsuarioId AND c.Ruta =  @parRuta";
        public const string LISTAR_ARCHIVOS = "SELECT a.Nombre, a.Tamanio FROM Archivo a INNER JOIN Carpeta c on c.Id = a.CarpetaId WHERE c.UsuarioId = {0} AND c.Ruta = {1}";

        public int CrearArchivo(BE.Archivo archivo)
        {
            try
            {
                ExecuteCommandText = CREAR_ARCHIVO;

                ExecuteParameters.Parameters.Clear();

                ExecuteParameters.Parameters.AddWithValue("@parNombre", archivo.Nombre);
                ExecuteParameters.Parameters.AddWithValue("@parTamanio", archivo.Tamanio);
                ExecuteParameters.Parameters.AddWithValue("@parCarpetaId", archivo.CarpetaId);

                return ExecuteNonEscalar();
            }
            catch
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public List<BE.Archivo> ListarArchivos(int usuarioId, string ruta)
        {
            SelectCommandText = String.Format(LISTAR_ARCHIVOS, usuarioId, ruta);
            DataSet ds = ExecuteNonReader();
            DataTable dt = ds.Tables[0];

            List<BE.Archivo> archivos = new List<BE.Archivo>();
            foreach (DataRow item in dt.Rows)
            {
                BE.Archivo archivo = new BE.Archivo
                {
                    Nombre = item["Nombre"].ToString(),
                    Tamanio = float.Parse(item["Tamanio"].ToString()),
                };
                archivos.Add(archivo);
            }

            return archivos;
        }
    }
}
