using StockController.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    class Cliente
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }

        public static bool Guardar(Cliente cliente, bool editar)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@id_cliente", cliente.IdCliente),
                new Parametro("@Nombre_cliente", cliente.NombreCliente),
                new Parametro("@Direccion", cliente.Direccion),
                new Parametro("@Ciudad", cliente.Ciudad),
                new Parametro("@Editar", editar)
            };

            return DbDatos.Ejecutar("Cliente_Agregar", parametros);
        }
        public static bool Eliminar(int idCliente)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@id_cliente", idCliente)
            };

            return DbDatos.Ejecutar("Cliente_Eliminar", parametros);
        }
        public static DataTable Listar()
        {
            return DbDatos.Listar("Cliente_Listar");
        }
        public static bool DatosRepetidos(string nombreProcedimiento, List<Parametro> parametros)
        {
            DataTable tabla = DbDatos.Listar(nombreProcedimiento, parametros);
            
            return tabla.Rows.Count > 0;
        }

    }
}
