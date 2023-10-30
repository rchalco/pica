using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMDManagement
{
    internal class cMenu
    {
        public List<Opcion> CargaMenuTI()
        {
            List<Opcion> menu = new List<Opcion>();


            menu.Add(new Opcion { Id = 2, Name = "Conexión", nivel = 1, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 3, Name = "Liberar Bandeja", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 4, Name = "Cerrar Bandejas", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 5, Name = "Abrir y Leer Id Bandejas", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 6, Name = "Reset", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 7, Name = "Reset Lento", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 8, Name = "Read Información ID", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 9, Name = "Entrega", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 10, Name = "Estadisticas", nivel = 1, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 11, Name = "Tabla de Estados", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 12, Name = "Contadores", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 13, Name = "Verificar Estado NMD", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 14, Name = "Datos de Autocomprobación", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 15, Name = "Servicios", nivel = 1, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 16, Name = "Abrir Shutter", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 17, Name = "Cerrar Shutter", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 18, Name = "Emulador Shutter", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 19, Name = "Limpiaza NT y NFs", nivel = 2, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 20, Name = "Bandejas", nivel = 1, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 21, Name = "Serie NMD", nivel = 1, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 22, Name = "Habilitar Detección de Bandejas", nivel = 1, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 23, Name = "Habilitar RESET Lento", nivel = 1, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 25, Name = "Configurar ATM", nivel = 1, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 50, Name = "Salir", nivel = 1, order = 1, Imagen = "" });

            return menu;
        }
        public List<Opcion> CargaMenuAdmin()
        {
            List<Opcion> menu = new List<Opcion>();

            menu.Add(new Opcion { Id = 2, Name = "Configurar ATM", nivel = 1, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 3, Name = "Carga a Dispensador", nivel = 1, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 4, Name = "Arqueo Dispensador", nivel = 1, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 5, Name = "Verificar Estado", nivel = 1, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 6, Name = "Calibrar Dispensador", nivel = 1, order = 1, Imagen = "" });
            menu.Add(new Opcion { Id = 7, Name = "Salir", nivel = 1, order = 1, Imagen = "" });

            return menu;
        }
    }
    public class Opcion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int nivel { get; set; }
        public int order { get; set; }
        public string Imagen { get; set; }

    }
}
