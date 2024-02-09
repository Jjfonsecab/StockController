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
    public partial class FMateriasPrimas : Form
    {
        public FMateriasPrimas()
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(txtNombre, txtPrecio, txtCantidad, txtProveedor, txtFecha))
                return;

            if (!Guardar()) return;

            Finalizar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMateriasPrimas.SelectedRows.Count > 0)
                {
                    DataGridViewCell cell = dgvMateriasPrimas.CurrentRow.Cells["id_materia_prima"];

                    // Verifica si la celda no es null y si su valor puede ser convertido a un entero
                    if (cell != null && cell.Value != null && int.TryParse(cell.Value.ToString(), out int idMateriaPrima))
                    {
                        IdMateriasPrimas = idMateriaPrima;
                        // Otros códigos relacionados con la eliminación
                        Eliminar();
                    }
                    else
                    {
                        // Muestra un mensaje si la celda o su valor no pueden ser convertidos a un entero
                        MessageBox.Show("No se pudo obtener el ID de la materia prima.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Muestra un mensaje si no hay filas seleccionadas
                    MessageBox.Show("Selecciona una fila antes de eliminar.", "Fila no seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Finalizar();
            }
        }
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                Precio = Convert.ToInt32(txtPrecio.Text),
                Cantidad = Convert.ToInt32(txtCantidad.Text),
                Proveedor = txtProveedor.Text,
                Fecha = DateTime.Parse(txtFecha.Text),
                Comentarios = txtComentarios.Text
            };
            return MateriasPrimas.Guardar(materiasPrimas, Editar);
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

    }
}
