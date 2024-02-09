using System;
using System.Collections.Generic;
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
        public Cliente Cliente { get; set; }

        public int IdProducto { get; set; }
        public Productos Productos { get; set; }

        public string Anotaciones { get; set; }
        public DateTime FechaRecibido { get; set; }
        public DateTime FechaEntrega { get; set; }
    }
}
