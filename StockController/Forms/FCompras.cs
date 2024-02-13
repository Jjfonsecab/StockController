using StockController.Class;
using StockController.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockController.Forms
{
    public partial class FCompras : Form
    {
        public FCompras()
        {
            InitializeComponent();
            ToUpperText();
        }
        private void FMateriasPrimas_Load(object sender, EventArgs e)
        {
            ListarGrid();
            MostrarFechaActual();
        }

        bool Editar;
        int IdMateriasPrimas;
        bool Modificar;

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(txtNombre, txtPrecio, txtCantidad, txtProveedor, txtFecha))
                return;
            if (Modificar)
            {
                if(!GuardarEditado())
                    return;
            }
            else
                if (!Guardar()) return;

            Finalizar();
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMateriasPrimas.SelectedRows.Count == 0 || dgvMateriasPrimas.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una fila antes de eliminar.");
                return;
            }

            if (dgvMateriasPrimas.SelectedRows.Count > 0)
            {
                DataGridViewCell cell = dgvMateriasPrimas.CurrentRow.Cells["id_materia_prima"];
                int id_materia_prima = Convert.ToInt32(cell.Value);
                IdMateriasPrimas = id_materia_prima;
                Eliminar();
                Finalizar();
            }
        }
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            IdMateriasPrimas = Convert.ToInt32(dgvMateriasPrimas.CurrentRow.Cells["id_materia_prima"].Value);
            txtNombre.Text = dgvMateriasPrimas.CurrentRow.Cells["Nombre"].Value.ToString();
            txtPrecio.Text = dgvMateriasPrimas.CurrentRow.Cells["Precio"].Value.ToString();
            txtCantidad.Text = dgvMateriasPrimas.CurrentRow.Cells["Cantidad"].Value.ToString();
            txtProveedor.Text = dgvMateriasPrimas.CurrentRow.Cells["Proveedor"].Value.ToString();
           
            DateTime fechaOriginal = DateTime.Parse(dgvMateriasPrimas.CurrentRow.Cells["fecha_compra"].Value.ToString());
            txtFecha.Text = fechaOriginal.ToString("yyyy-MM-dd"); txtComentarios.Text = dgvMateriasPrimas.CurrentRow.Cells["Comentarios"].Value.ToString();

            txtComentarios.Text = dgvMateriasPrimas.CurrentRow.Cells["Comentarios"].Value.ToString();

            Editar = true;
            Modificar = true;
        }
        private void Finalizar()
        {
            ListarGrid();
            Linpiar();
        }

        private void Linpiar()
        {
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
            txtProveedor.Text = "";
            txtComentarios.Text = "";
            Editar = false;
        }
        private bool Guardar()
        {
            MateriasPrimas materiasPrimas = new MateriasPrimas()
            {
                Nombre = txtNombre.Text,
                Precio = Convert.ToDecimal(txtPrecio.Text),
                Cantidad = Convert.ToInt32(txtCantidad.Text),
                Proveedor = txtProveedor.Text,
                Fecha = DateTime.Parse(txtFecha.Text),
                Comentarios = txtComentarios.Text
            };

            return MateriasPrimas.Guardar(materiasPrimas, Editar);
        }
        private bool GuardarEditado()
        {
            if (IdMateriasPrimas > 0)
            {
                // Obtén la fecha original almacenada
                DateTime fechaOriginal = DateTime.Parse(dgvMateriasPrimas.CurrentRow.Cells["fecha_compra"].Value.ToString());

                MateriasPrimas materiasPrimasEditado = new MateriasPrimas()
                {
                    IdMateriaPrima = IdMateriasPrimas,  // Asegúrate de tener el ID correcto
                    Nombre = txtNombre.Text,
                    Precio = Convert.ToDecimal(txtPrecio.Text),
                    Cantidad = Convert.ToInt32(txtCantidad.Text),
                    Proveedor = txtProveedor.Text,
                    Fecha = fechaOriginal,  // Utiliza la fecha original
                    Comentarios = txtComentarios.Text
                };

                return MateriasPrimas.Guardar(materiasPrimasEditado, true);  // true indica modo edición
            }
            else
            {
                MessageBox.Show("Selecciona una materia prima antes de guardar editado.", "Materia prima no seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private bool Eliminar()
        {
            if (IdMateriasPrimas > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar esta materia prima?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                    return MateriasPrimas.Eliminar(IdMateriasPrimas);
                else
                    return false;
            }
            else
            {
                MessageBox.Show("Selecciona una materia prima antes de eliminar.", "Materia prima no seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private bool ValidarCampos(params TextBox[] campos)
        {
            foreach (var campo in campos)
            {
                if (string.IsNullOrEmpty(campo.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos antes de guardar.", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        private void ListarGrid()
        {
            dgvMateriasPrimas.DataSource = MateriasPrimas.Listar();
            DbDatos.OcultarIds(dgvMateriasPrimas);
        }
        private void ToUpperText()
        {
            txtNombre.CharacterCasing = CharacterCasing.Upper;
            txtPrecio.CharacterCasing = CharacterCasing.Upper;
            txtCantidad.CharacterCasing = CharacterCasing.Upper;
            txtProveedor.CharacterCasing = CharacterCasing.Upper;
            txtFecha.CharacterCasing = CharacterCasing.Upper;
            txtComentarios.CharacterCasing = CharacterCasing.Upper;
        }
        private void MostrarFechaActual()
        {
            txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        private bool VerificarExistencia()
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@Nombre", txtNombre.Text)
            };

            if (MateriasPrimas.DatosRepetidos("MateriaPrima_VerificarExistencia", parametros))
            {
                MessageBox.Show("Ya existe una materia prima con el mismo nombre en la base de datos.", "Materia Prima Duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
