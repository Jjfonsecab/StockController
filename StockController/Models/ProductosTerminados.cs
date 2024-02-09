using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    class ProductosTerminados
    {
        public int IdProductoTerminado { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set;}
        public string Anotacion { get; set; }
    }
}
