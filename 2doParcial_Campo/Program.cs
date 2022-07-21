using BE.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2doParcial_Campo
{
    class Program
    {
        public static string directorioActual;
        static void Main(string[] args)
        {
            string opcion;
            List<Carpeta> carpetas = new List<Carpeta>();

            Menu();
            opcion = Console.ReadLine();

            while (opcion != "DI")
            {
                switch (opcion)
                {
                    case "MD":
                        string directorio;

                        if (string.IsNullOrWhiteSpace(directorioActual))
                        {
                            Console.WriteLine("Ingrese el nombre del directorio: ");
                            directorio = Console.ReadLine();

                            BE.Composite.Carpeta carpeta = new BE.Composite.Carpeta()
                            {
                                Nombre = directorio
                            };

                            directorioActual = directorio;
                            carpetas.Add(carpeta);
                        }
                        else
                        {
                            foreach (Carpeta item in carpetas)
                            {
                                if (item.Nombre == directorioActual)
                                {
                                    Console.WriteLine($"Va a crear una carpeta en la carpeta {directorioActual}");
                                    Console.WriteLine("Ingrese el nombre del directorio: ");
                                    directorio = Console.ReadLine();

                                    BE.Composite.Carpeta carpeta = new BE.Composite.Carpeta()
                                    {
                                        Nombre = directorio
                                    };

                                    directorioActual = directorio;
                                    item.AgregarHijo(carpeta);
                                }
                                else
                                {
                                    Console.WriteLine($"Va a crear una carpeta en la carpeta {directorioActual}");
                                    Console.WriteLine("Ingrese el nombre del directorio: ");
                                    directorio = Console.ReadLine();

                                    BE.Composite.Carpeta carpeta = new BE.Composite.Carpeta()
                                    {
                                        Nombre = directorio
                                    };

                                    IList<Componente> subCarpeta = new List<Componente>();
                                    subCarpeta = item.ObtenerHijos();
                                    agregarCarpeta(subCarpeta, carpeta);
                                    directorioActual = directorio;

                                }
                            }
                        }

                        break;

                    case "MF":
                        string archivo;
                        string tamanio;

                        Console.WriteLine($"Va a crear archivo en la carpeta {directorioActual}");

                        Console.WriteLine($"Ingrese nombre de archivo: ");
                        archivo = Console.ReadLine();

                        Console.WriteLine($"Ingrese tamaño del archivo: ");
                        tamanio = Console.ReadLine();

                        Archivo archivoNuevo = new Archivo()
                        {
                            Nombre = archivo,
                            Tamanio = tamanio
                        };

                        foreach (Carpeta item in carpetas)
                        {
                            if (item.Nombre == directorioActual)
                            {
                                item.AgregarHijo(archivoNuevo);
                            }
                            else
                            {
                                //var hijos = item.ObtenerHijos();
                                //foreach (var item2 in hijos)
                                //{
                                //    if (item2.Nombre == directorioActual)
                                //    {
                                //        item2.AgregarHijo(archivoNuevo);
                                //    }
                                //}
                                IList<Componente> subCarpeta = new List<Componente>();
                                subCarpeta = item.ObtenerHijos();
                                agregarArchivo(subCarpeta, archivoNuevo);
                              
                            }
                        }
                        break;

                    case "LS":
                        Console.Write($"Carpeta actual: {directorioActual}\n");
                        foreach (Carpeta item in carpetas)
                        {
                            if (item.Nombre == directorioActual)
                            {
                                var archivos = item.ObtenerHijos();

                                foreach (Componente arc in archivos)
                                {
                                    if (arc.GetType() == typeof(Archivo))
                                    {

                                        Archivo arc1 = (Archivo)arc;
                                        Console.WriteLine($"Archivo: {arc1.Nombre}, tamaño: {arc1.Tamanio}");
                                        //Console.WriteLine($"Archivo: {arc.Nombre}, tamaño: {arc.Tamanio}");
                                    }
                                    else
                                    {
                                        Carpeta arc1 = (Carpeta)arc;
                                        Console.WriteLine($"Carpeta: {arc1.Nombre}");
                                    }
                                   
                                }
                                Console.ReadKey();
                            }
                            else
                            {
                                IList<Componente> subCarpeta = new List<Componente>();
                                subCarpeta = item.ObtenerHijos();
                                RecorrerArbol(subCarpeta);
                            }

                        }

                        break;

                    case "CD":
                        string direc;

                        Console.WriteLine("Ingrese el nombre del directorio a ingresar: ");
                        direc = Console.ReadLine();

                        foreach (Carpeta item in carpetas)
                        {
                            if (item.Nombre == direc)
                            {
                                directorioActual = item.Nombre;
                                break;
                            }
                            else
                            {
                                IList<Componente> subCarpeta = new List<Componente>();
                                subCarpeta = item.ObtenerHijos();
                                encontrarCarpeta(subCarpeta,direc);
                                break;
                            }
                        }
                        break;
                }

                Console.Clear();
                Console.WriteLine($"Estas parado en: {directorioActual}");
                Menu();
                opcion = Console.ReadLine();
            }

        }

        public static void Menu()
        {
            Console.WriteLine("Ingrese MD para crear un nuevo directorio");
            Console.WriteLine("Ingrese CD para moverse a otro directorio");
            Console.WriteLine("Ingrese MF para crear un archivo dentro del directorio actual");
            Console.WriteLine("Ingrese LS para listar los archivos dentro del directorio actual");
            Console.WriteLine("Ingrese DI para cerrar el programa\n");
        }

        public static void RecorrerArbol(IList<Componente> componentes)
        {
            
            if (componentes != null  && componentes.Count > 0)
            {
                foreach (var componente in componentes)
                {
                    // Agrego
                    if (directorioActual == componente.Nombre)
                    {
                        var archivos = componente.ObtenerHijos();


                        foreach (var arc in archivos)
                        {
                            if (arc.GetType() == typeof(Archivo))
                            {

                                Archivo arc1 = (Archivo)arc;
                                Console.WriteLine($"Archivo: {arc1.Nombre}, tamaño: {arc1.Tamanio}");
                            }
                            else
                            {

                                Carpeta arc1 = (Carpeta)arc;
                                Console.WriteLine($"Carpeta: {arc1.Nombre}");
                            }
                        }
                        Console.ReadKey();


                    }
                    else
                    {
                        RecorrerArbol(componente.ObtenerHijos());
                    }

                }
            }
        }

        public static void agregarCarpeta(IList<Componente> componentes, Carpeta carpeta)
        {
            if (componentes.Count > 0)
            {
                foreach (var componente in componentes)
                {

                    if (directorioActual == componente.Nombre)
                    {
                        componente.AgregarHijo(carpeta);

                    }
                    else
                    {
                        if (componente.GetType() == typeof(Carpeta))
                        {
                            agregarCarpeta(componente.ObtenerHijos(), carpeta);
                        }
                        
                    }
                }

            }
        }

        public static void agregarArchivo(IList<Componente> componentes, Archivo archivo)
        {
            if (componentes.Count > 0)
            {
                foreach (var componente in componentes)
                {

                    if (directorioActual == componente.Nombre)
                    {
                        componente.AgregarHijo(archivo);

                    }
                    else
                    {
                        if (componente.GetType() == typeof(Carpeta))
                        {
                            agregarArchivo(componente.ObtenerHijos(), archivo);
                        }

                    }
                }

            }
        }

        public static void encontrarCarpeta(IList<Componente> componentes, string nombre)
        {
            if (componentes.Count > 0)
            {
                foreach (var componente in componentes)
                {

                    if (nombre == componente.Nombre)
                    {
                        directorioActual = nombre;

                    }
                    else
                    {
                        if (componente.GetType() == typeof(Carpeta))
                        {
                            encontrarCarpeta(componente.ObtenerHijos(), nombre);
                        }

                    }
                }

            }
        }
    }
}

