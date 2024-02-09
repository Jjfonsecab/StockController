using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    class Productos
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        public TimeSpan TiempoProduccion { get; set;}//Ojo, verificar si el time span funciona
    }
}
