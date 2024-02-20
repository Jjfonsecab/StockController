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
            MostrarFechaActual();
        }

        bool Editar;
        int IdPedidos;
        int IdCliente;
        int IdProducto;
        bool Modificar;

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(txtCliente, txtProducto, txtAnotaciones, txtFechaEntrega, txtFechaRecibido))
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
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IdPedidos = Convert.ToInt32(dgvPedidos.CurrentRow.Cells["id_pedido"].Value);
            txtCliente.Text = dgvPedidos.CurrentRow.Cells["id_cliente"].Value.ToString();
            txtProducto.Text = dgvPedidos.CurrentRow.Cells["id_producto"].Value.ToString();
            txtReferencia.Text = dgvPedidos.CurrentRow.Cells["id_producto"].Value.ToString();
            txtAnotaciones.Text = dgvPedidos.CurrentRow.Cells["fecha_recibido"].Value.ToString();
            txtFechaEntrega.Text = dgvPedidos.CurrentRow.Cells["fecha_entrega"].Value.ToString();

            

            Editar = true;
            Modificar = true;
        }
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime fechaSeleccionada = monthCalendar.SelectionStart;

            txtFechaEntrega.Text = fechaSeleccionada.ToString("yyyy-MM-dd");
        }

        private DateTime fechaSeleccionada;
        private System.Windows.Forms.TextBox ultimoTextBoxModificado = null;
        private void txtCliente_KeyUp(object sender, KeyEventArgs e)
        {
            ultimoTextBoxModificado = txtCliente;
            string searchText = txtCliente.Text;
            BuscarYMostrarResultados("RetornarClientePorId", txtCliente, listBox, "@NombreBuscado", "Nombre");

        }
        private void txtProducto_KeyUp(object sender, KeyEventArgs e)
        {
            ultimoTextBoxModificado = txtProducto;
            string searchText = txtProducto.Text;
            BuscarYMostrarResultados("RetornarProductoPorId", txtProducto, listBox, "@NombreBuscado", "Nombre");
        }

        private void txtReferencia_KeyUp(object sender, KeyEventArgs e)
        {
            ultimoTextBoxModificado = txtReferencia;
            string searchText = txtReferencia.Text;
            BuscarYMostrarResultados("RetornarProductoPorReferencia", txtReferencia, listBox, "@NombreBuscado", "Referencia");
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
                // Depuración: Muestra un mensaje si no se pudo determinar el TextBox correspondiente
                MessageBox.Show("No se pudo determinar el TextBox correspondiente.");
            }
        }
        private bool Guardar()
        {
            if (IdCliente <= 0 || IdProducto <= 0)
            {
                MessageBox.Show("Selecciona un cliente y un producto válidos.");
                return false;
            }
            Pedidos pedidos = new Pedidos()
            {
                IdPedido = IdPedidos,
                IdCliente = IdCliente,
                IdProducto = IdProducto,
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
                DateTime fechaOriginal = DateTime.Parse(dgvPedidos.CurrentRow.Cells["fecha_recibido"].Value.ToString());

                Pedidos pedidos = new Pedidos()
                {
                    IdPedido = IdPedidos,
                    IdCliente = IdCliente,
                    IdProducto = IdProducto,
                    Anotaciones = txtAnotaciones.Text,
                    FechaRecibido = fechaOriginal,
                    FechaEntrega = fechaSeleccionada
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
            txtProducto.Text = "";
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
            //DbDatos.OcultarIds(dgvPedidos);
            PersonalizarColumnasGrid();
        }
        private void ToUpperText()//El upperText para los comboBox esta en comboBox_TextChanged
        {
            txtCliente.CharacterCasing = CharacterCasing.Upper;
            txtCliente.Click += TextBox_Click;
            txtProducto.CharacterCasing = CharacterCasing.Upper;
            txtProducto.Click += TextBox_Click;
            txtReferencia.CharacterCasing = CharacterCasing.Upper;
            txtReferencia.Click += TextBox_Click;
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
                    if (columna.Name == "id_cliente" || columna.Name == "id_producto")
                    {
                        dgvPedidos.Columns["id_cliente"].HeaderText = "Cliente";
                        dgvPedidos.Columns["id_producto"].HeaderText = "Productos";
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
                    else if(textBox == txtProducto)
                    {
                        IdProducto = Convert.ToInt32(row["Id_producto"]);
                        string nombreProducto = row["Nombre"].ToString();

                        listBox.Items.Add(nombreProducto);
                    }             
                    if(textBox == txtReferencia)
                    {
                        
                        string nombreSeleccionado = txtProducto.Text;

                        List<Parametro> parametroReferencia = new List<Parametro>
                        {
                            new Parametro("@NombreBuscado", nombreSeleccionado)
                        };
                        DataTable resultReferencia = DbDatos.Listar("RetornarProductoPorReferencia", parametroReferencia);

                        listBox.Items.Clear();

                        if (resultReferencia != null && resultReferencia.Rows.Count > 0)
                        {
                            foreach (DataRow referenciaRow in resultReferencia.Rows)
                            {
                                string referenciaProducto = referenciaRow["Referencia"].ToString();
                                // Agregar la referencia al ListBox
                                listBox.Items.Add(referenciaProducto);
                            }
                        }
                    }
                }                
            }
        }

    }
}