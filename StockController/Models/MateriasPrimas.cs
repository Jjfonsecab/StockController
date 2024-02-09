using StockController.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    class MateriasPrimas
    {
        public int IdMateriaPrima { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Proveedor { get; set; }
        public DateTime Fecha { get; set; }//ojo porque puede haber error con la hora que este va a retornar
        public string Comentarios { get; set; }

        public static bool Guardar(MateriasPrimas materiaprima, bool editar)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro("@id_materia_prima", materiaprima.IdMateriaPrima),
                new Parametro("@Nombre", materiaprima.Nombre),
                new Parametro("@Precio", materiaprima.Precio),
                new Parametro("@Cantidad", materiaprima.Cantidad),
                new Parametro("@Proveedor", materiaprima.Proveedor),
                new Parametro("@fecha_compra",  materiaprima.Fecha),
                new Parametro("@Comentarios", materiaprima.Comentarios),
                new Parametro("@Editar", editar)
            };

            return DbDatos.Ejecutar("MateriaPrima_Agregar", parametros);
        }
        public static bool Eliminar(int idMateriaPrima)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@id_materia_prima", idMateriaPrima) 
            };
            return DbDatos.Ejecutar("MateriaPrima_Eliminar", parametros);
        }
        public static DataTable Listar()
        {
            return DbDatos.Listar("MateriaPrima_Listar");
        }
    }
}
