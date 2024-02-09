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
        bool Modificar;

        private void guardarBtn_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(txtNombre, txtCiudad, txtDireccion1)) return;
            
            if(!Modificar)
                if (!VerificarExistencia()) return;

            if (!Guardar()) return;

            Finalizar();
        }

        private void eliminarBtn_Click(object sender, EventArgs e)
        {

            if (dgvDatosCliente.SelectedRows.Count == 0 || dgvDatosCliente.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una fila antes de eliminar.");
                return;
            }

            if (dgvDatosCliente.SelectedRows.Count > 0)
            {
                DataGridViewCell cell = dgvDatosCliente.CurrentRow.Cells["id_cliente"];
                int id_cliente = Convert.ToInt32(cell.Value);
                IdCliente = id_cliente;
                Eliminar();
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
            txtDireccion1.Text = "";
            Editar = false;
        }
        private bool Guardar()
        {
            Cliente cliente = new Cliente()
            {
                NombreCliente = txtNombre.Text,
                Direccion = txtDireccion1.Text,
                Ciudad = txtCiudad.Text,
                IdCliente = IdCliente
            };

            return Cliente.Guardar(cliente, Editar);
        }
        private bool Eliminar()
        {
            int idCliente = IdCliente;

            return Cliente.Eliminar(idCliente);

        }
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IdCliente = Convert.ToInt32(dgvDatosCliente.CurrentRow.Cells["id_cliente"].Value);
            txtNombre.Text = dgvDatosCliente.CurrentRow.Cells["Nombre_cliente"].Value.ToString();
            txtDireccion1.Text = dgvDatosCliente.CurrentRow.Cells["Direccion"].Value.ToString();
            txtCiudad.Text = dgvDatosCliente.CurrentRow.Cells["Ciudad"].Value.ToString();

            Editar = true;
            Modificar = true;
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

            if (Cliente.DatosRepetidos("Cliente_VerificarExistencia", parametros))
            {
                MessageBox.Show("El cliente con el mismo nombre ya existe en la base de datos.", "Cliente Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
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
            txtDireccion1.CharacterCasing = CharacterCasing.Upper;
        }


    }
}
