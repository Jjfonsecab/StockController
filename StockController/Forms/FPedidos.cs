using StockController.Class;
using StockController.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StockController.Forms
{
    public partial class FPedidos : Form
    {
        public FPedidos()
        {
            InitializeComponent();
            ToUpperText();
        }

        private void Pedidos_Load(object sender, EventArgs e)
        {
            ListarTodo();
            //MostrarFechaActual();
        }

        bool Editar;
        int IdPedidos;
        int IdCliente;
        bool Modificar;


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(txtCliente, txtAnotaciones, txtFechaEntrega, txtFechaRecibido))
                return;
            if (Modificar)
            {
                if (!GuardarEditado())
                    return;
            }
            else
                if (!Guardar()) return;

            Finalizar();
        }
        private void btnInicio_Click(object sender, EventArgs e)
        {
            FInicio fInicio = Application.OpenForms.OfType<FInicio>().FirstOrDefault();

            if (fInicio == null)
            {
                fInicio = new FInicio();
                fInicio.Show();
            }
            else
                fInicio.BringToFront();

            if (Application.OpenForms.Count > 1)
                this.Close();
            else
                this.Hide();
        }
        private void btnPedidos_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(txtCliente, txtAnotaciones, txtFechaEntrega, txtFechaRecibido))
            {
                MessageBox.Show("Por favor llene todos los campos");
                return;
            }
                
            
            if (!Guardar()) return;

            Finalizar(); 

            FDetallesPedido fDetalles = Application.OpenForms.OfType<FDetallesPedido>().FirstOrDefault();

            if (fDetalles == null)
            {
                fDetalles = new FDetallesPedido();
                fDetalles.Show();
            }
            else
                fDetalles.BringToFront();
            
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IdPedidos = Convert.ToInt32(dgvPedidos.CurrentRow.Cells["id_pedido"].Value);
            txtCliente.Text = dgvPedidos.CurrentRow.Cells["id_cliente"].Value.ToString();
            txtAnotaciones.Text = dgvPedidos.CurrentRow.Cells["fecha_recibido"].Value.ToString();
            txtFechaEntrega.Text = dgvPedidos.CurrentRow.Cells["fecha_entrega"].Value.ToString();            

            Editar = true;
            Modificar = true;
        }
        private TextBox campoSeleccionado;
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime fechaSeleccionada = monthCalendar.SelectionStart;

            if (campoSeleccionado == txtFechaRecibido)
                txtFechaRecibido.Text = fechaSeleccionada.ToString("yyyy-MM-dd");
            
            else if (campoSeleccionado == txtFechaEntrega)            
                txtFechaEntrega.Text = fechaSeleccionada.ToString("yyyy-MM-dd");            
        }

        private DateTime fechaSeleccionada;
        private System.Windows.Forms.TextBox ultimoTextBoxModificado = null;
        private void txtFechaRecibido_Click(object sender, EventArgs e)
        {
            campoSeleccionado = txtFechaRecibido;
        }

        private void txtFechaEntrega_Click(object sender, EventArgs e)
        {
            campoSeleccionado = txtFechaEntrega;
        }
        private void txtCliente_KeyUp(object sender, KeyEventArgs e)
        {
            ultimoTextBoxModificado = txtCliente;
            string searchText = txtCliente.Text;
            BuscarYMostrarResultados("RetornarClientePorId", txtCliente, listBox, "@NombreBuscado", "Nombre");

        }
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1 && ultimoTextBoxModificado != null)
            {
                string selectedText = listBox.SelectedItem.ToString();
                ultimoTextBoxModificado.Text = selectedText;
            }
            else
            {
                MessageBox.Show("No se pudo determinar el TextBox correspondiente.");
            }
        }
        private bool Guardar()
        {
            if (IdCliente <= 0 )
            {
                MessageBox.Show("Selecciona un cliente  válido.");
                return false;
            }
            Pedidos pedidos = new Pedidos()
            {
                IdPedido = IdPedidos,
                IdCliente = IdCliente,
                Anotaciones = txtAnotaciones.Text,
                FechaRecibido = DateTime.Parse(txtFechaRecibido.Text),
                FechaEntrega = DateTime.Parse(txtFechaEntrega.Text)
            };
            
            return Pedidos.Guardar(pedidos, Editar);
        }
        private bool GuardarEditado()
        {
            if (IdPedidos > 0)
            {
                DateTime fechaRecibido = DateTime.Parse(dgvPedidos.CurrentRow.Cells["fecha_recibido"].Value.ToString());
                DateTime fechaEntrega = DateTime.Parse(dgvPedidos.CurrentRow.Cells["fecha_entrega"].Value.ToString());

                Pedidos pedidos = new Pedidos()
                {
                    IdPedido = IdPedidos,
                    IdCliente = IdCliente,
                    Anotaciones = txtAnotaciones.Text,
                    FechaRecibido = fechaRecibido,
                    FechaEntrega = fechaEntrega
                };

                return Pedidos.Guardar(pedidos, true);  // true indica modo edición
            }
            else
            {
                MessageBox.Show("Selecciona una materia prima antes de guardar editado.", "Materia prima no seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private void Finalizar()
        {
            ListarTodo();
            Linpiar();
        }
        private void Linpiar()
        {
            txtCliente.Text = "";
            txtAnotaciones.Text = "";
            txtFechaRecibido.Text = "";
            txtFechaEntrega.Text = "";
            Editar = false;
        }
        private bool ValidarCampos(params Control[] controles)
        {
            foreach (var control in controles)
            {
                if (control is System.Windows.Forms.TextBox textBox && string.IsNullOrEmpty(textBox.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos antes de guardar.", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        private void MostrarFechaActual()
        {
            txtFechaRecibido.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        private void ListarTodo()
        {
            dgvPedidos.DataSource = Pedidos.ListarTodo();
            DbDatos.OcultarIds(dgvPedidos);
            PersonalizarColumnasGrid();
        }
        private void ToUpperText()//El upperText para los comboBox esta en comboBox_TextChanged
        {
            txtCliente.CharacterCasing = CharacterCasing.Upper;
            txtCliente.Click += TextBox_Click;            
            txtAnotaciones.CharacterCasing = CharacterCasing.Upper;
            txtAnotaciones.Click += TextBox_Click;
        }
        private void TextBox_Click(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.TextBox textBox)
            {
                textBox.SelectAll();
            }
        }
        private void PersonalizarColumnasGrid()
        {
            // Itera sobre todas las columnas del DataGridView
            foreach (DataGridViewColumn columna in dgvPedidos.Columns)
            {
                // Asegúrate de que la columna tenga un nombre
                if (!string.IsNullOrEmpty(columna.Name))
                {
                    ConfigurarCabeceraColumna(columna, columna.HeaderText);
                    // Puedes personalizar las columnas según su nombre o cualquier otra condición necesaria
                    if (columna.Name == "id_cliente" )
                    {
                        dgvPedidos.Columns["id_cliente"].HeaderText = "Cliente";
                        DataGridViewCellStyle estiloCeldaNumerica = new DataGridViewCellStyle();
                        estiloCeldaNumerica.Alignment = DataGridViewContentAlignment.MiddleRight; // Alinea a la derecha
                        estiloCeldaNumerica.Format = "N0";
                        columna.DefaultCellStyle = estiloCeldaNumerica;
                        DbDatos.OcultarIds(dgvPedidos);
                    }
                    else if (columna.Name == "fecha_recibido" || columna.Name == "fecha_entrega")
                    {
                        dgvPedidos.Columns["fecha_recibido"].HeaderText = "Fecha Recibido";
                        dgvPedidos.Columns["fecha_entrega"].HeaderText = "Fecha Entrega";
                        DbDatos.OcultarIds(dgvPedidos);
                    }
                }
            }
        }
        private void ConfigurarCabeceraColumna(DataGridViewColumn columna, string nuevoHeaderText)
        {
            columna.HeaderText = nuevoHeaderText;
            columna.HeaderCell.Style.Font = new Font(columna.DataGridView.Font, FontStyle.Bold);
        }
        private void BuscarYMostrarResultados(string nombreProcedimiento, System.Windows.Forms.TextBox textBox, ListBox listBox, string parametroNombre, string nombreColumna)
        {
            string searchText = textBox.Text;

            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro(parametroNombre, searchText)
            };

            DataTable result = DbDatos.Listar(nombreProcedimiento, parametros);

            listBox.Items.Clear();

            if (result != null && result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    if(textBox == txtCliente)
                    {
                        IdCliente = Convert.ToInt32(row["Id_cliente"]);
                        string nombreCliente = row["Nombre_cliente"].ToString();

                        listBox.Items.Add(nombreCliente);

                    }                    
                }                
            }
        }

        
    }
}