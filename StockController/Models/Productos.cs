using StockController.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    class Productos
    {
        [Key]
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Referencia { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public string TiempoProduccion { get; set;}

        public static bool Guardar(Productos productos, bool editar)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro("@id_producto", productos.IdProducto),
                new Parametro("@Nombre", productos.Nombre),
                new Parametro("@Cantidad", productos.Cantidad),
                new Parametro("@Precio", productos.Precio),
                new Parametro("@Descripcion", productos.Descripcion),
                new Parametro("@tiempo_produccion",  productos.TiempoProduccion),
                new Parametro("@Referencia", productos.Referencia),
                new Parametro("@Editar", editar)
            };

            return DbDatos.Ejecutar("Producto_Agregar", parametros);
        }
        public static bool Eliminar(int idMateriaPrima)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@id_producto", idMateriaPrima)
            };
            return DbDatos.Ejecutar("Producto_Eliminar", parametros);
        }
        public static DataTable ListarTodo()
        {
            return DbDatos.Listar("Producto_Listar");
        }
    }
}
