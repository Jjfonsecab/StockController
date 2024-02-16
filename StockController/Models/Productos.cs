﻿using StockController.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    class Productos
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public string TiempoProduccion { get; set;}//Ojo, verificar si el time span funciona

        public static bool Guardar(Productos productos, bool editar)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro("@id_producto", productos.IdProducto),
                new Parametro("@Nombre", productos.Nombre),
                new Parametro("@Cantidad", productos.Cantidad),
                new Parametro("@Precio_final", productos.Precio),
                new Parametro("@Descripcion", productos.Descripcion),
                new Parametro("@tiempo_produccion",  productos.TiempoProduccion),
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
