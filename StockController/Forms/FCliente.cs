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

namespace StockController
{
    public partial class ClienteForm : Form
    {
        public ClienteForm()
        {
            InitializeComponent();
            ToUpperText();
        }
        private void ClienteForm_Load(object sender, EventArgs e)
        {
            ListarGrid();
            PersonalizarColumnasGrid();
        }
        bool Editar;
        int IdCliente;

        private void guardarBtn_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(txtNombre, txtCiudad, txtDireccion)) return;

            if (!VerificarExistencia()) return;

            if (!Guardar()) return;

            Finalizar();

        }

        private void eliminarBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatosCliente.SelectedRows.Count > 0)
                {
                    DataGridViewCell cell = dgvDatosCliente.CurrentRow.Cells["id_cliente"];

                    // Verifica si la celda no es null y si su valor puede ser convertido a un entero
                    if (cell != null && cell.Value != null && int.TryParse(cell.Value.ToString(), out int idCliente))
                    {
                        IdCliente = idCliente;
                        // Otros códigos relacionados con la eliminación
                        Eliminar();
                    }
                    else
                    {
                        // Muestra un mensaje si la celda o su valor no pueden ser convertidos a un entero
                        MessageBox.Show("No se pudo obtener el ID del cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void Finalizar()
        {
            ListarGrid();
            Linpiar();
        }
        private void Linpiar()
        {
            txtNombre.Text = "";
            txtCiudad.Text = "";
            txtDireccion.Text = "";
            Editar = false;
        }
        private bool Guardar()
        {
            Cliente cliente = new Cliente()
            {
                NombreCliente = txtNombre.Text,
                Direccion = txtDireccion.Text,
                Ciudad = txtCiudad.Text,
                IdCliente = IdCliente
            };

            return Cliente.Guardar(cliente, Editar);
        }
        private bool Eliminar()
        {
            if (IdCliente > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                    return Cliente.Eliminar(IdCliente);
                else
                    return false;
            }
            else
            {
                MessageBox.Show("Selecciona un cliente antes de eliminar.", "Cliente no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IdCliente = Convert.ToInt32(dgvDatosCliente.CurrentRow.Cells["id_cliente"].Value);
            txtNombre.Text = dgvDatosCliente.CurrentRow.Cells["Nombre_cliente"].Value.ToString();
            txtDireccion.Text = dgvDatosCliente.CurrentRow.Cells["Direccion"].Value.ToString();
            txtCiudad.Text = dgvDatosCliente.CurrentRow.Cells["Ciudad"].Value.ToString();

            Editar = true;
        }
        private void ListarGrid()
        {
            dgvDatosCliente.DataSource = Cliente.Listar();
            DbDatos.OcultarIds(dgvDatosCliente);
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
        private bool VerificarExistencia()
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@Nombre_cliente", txtNombre.Text)
            };

            // Nombre del procedimiento almacenado para verificar duplicados
            string nombreProcedimientoVerificar = "Datos_Existentes_Cliente";

            return !DbDatos.DatosRepetidos(nombreProcedimientoVerificar, parametros);
        }
        private void PersonalizarColumnasGrid()
        {
            dgvDatosCliente.Columns["Nombre_cliente"].HeaderText = "Nombre";

            //Por cuestion de orden en la base de datos, a continuacion se altera la columna Apellido_cliente para ubicarla al lado de Nombre_cliente
            //dgvDatosCliente.Columns["Apellido_cliente"].DisplayIndex = 2;
        }
        //Con las siguientes configuraciones pondremos en mayuscula las entradas de los txtBox

        private void ToUpperText()
        {
            txtNombre.CharacterCasing = CharacterCasing.Upper;
            txtCiudad.CharacterCasing = CharacterCasing.Upper;
            txtDireccion.CharacterCasing = CharacterCasing.Upper;
        }


    }
}
