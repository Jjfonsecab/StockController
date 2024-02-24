using StockController.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    class DetallesPedido
    {        
        public int IdDetalle { get; set; }
        public int IdPedido { get; set;}
        public int IdProducto { get; set;}
        public int Cantidad { get; set;}
        public static bool Guardar(DetallesPedido detalles, bool editar)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro("@id_detalle", detalles.IdDetalle),
                new Parametro("@id_pedido", detalles.IdPedido),
                new Parametro("@id_producto", detalles.IdProducto),
                new Parametro("@Cantidad", detalles.Cantidad),
                new Parametro("@Editar", editar)
            };

            return DbDatos.Ejecutar("Detalles_Agregar", parametros);
        }
        public static DataTable ListarProducto()
        {
            return DbDatos.Listar("Producto_ListarNombreReferencia");
        }
        public static DataTable ObtenerUltimoPedido() 
        {
            return DbDatos.Listar("ObtenerUltimoIdPedido");
        }
    }
    
}
