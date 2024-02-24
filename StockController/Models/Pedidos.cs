using StockController.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    class Pedidos
    {
        public int IdPedido { get; set; }
        //Ojo con la relacion de las foreign keys
        public int IdCliente { get; set; }        
        public string Anotaciones { get; set; }
        public DateTime FechaRecibido { get; set; }
        public DateTime FechaEntrega { get; set; }

        public static bool Guardar(Pedidos pedidos, bool editar)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro("@id_pedido", pedidos.IdPedido),
                new Parametro("@id_cliente", pedidos.IdCliente),
                new Parametro("@Anotaciones", pedidos.Anotaciones),
                new Parametro("@fecha_recibido", pedidos.FechaRecibido),
                new Parametro("@fecha_entrega",  pedidos.FechaEntrega),
                new Parametro("@Editar", editar)
            };

            return DbDatos.Ejecutar("Pedidos_Agregar", parametros);
        }        
        public static DataTable Listar()
        {
            return DbDatos.Listar("Pedido_Mostrar");
        }
        public static DataTable ListarTodo()
        {
            return DbDatos.Listar("Pedidos_Listar");
        }
    }
}
