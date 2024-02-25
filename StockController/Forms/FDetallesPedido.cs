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
    public partial class FDetallesPedido : Form
    {
        private int idPedidos;
                
        public FDetallesPedido()
        {
            InitializeComponent();
            ToUpperText();
        }

        private void FDetallesPedido_Load(object sender, EventArgs e)
        {
            ListarProductos();
        }

        bool Editar;
        int IdDetalle;
        int IdPedidos;
        int IdProducto;
        bool Modificar;

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(txtProducto, txtReferencia, txtCantidad))
                return;

            DataTable resultado = DetallesPedido.ObtenerUltimoPedido();
            // Verifica si hay al menos una fila en el resultado y que el campo "id_pedido" existe
            if (resultado.Rows.Count > 0 && resultado.Columns.Contains("id_pedido"))
            {
                // Obtén el valor de id_pedido de la primera fila
                int idPedido = Convert.ToInt32(resultado.Rows[0]["id_pedido"]);

                // Ahora puedes usar el idPedido según sea necesario en tu lógica
                // Por ejemplo, podrías asignarlo a una variable de clase para su uso posterior
                this.IdPedidos = idPedido;
            }

            if (Modificar)
            {
                if (!GuardarEditado())
                    return;
            }
            else
                if (!Guardar()) return;

            Finalizar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)//toDo
        {
            IdDetalle = Convert.ToInt32(dgvProductos.CurrentRow.Cells["id_detalle"]);
            txtProducto.Text = dgvProductos.CurrentRow.Cells["id_producto"].Value.ToString();
            txtReferencia.Text = dgvProductos.CurrentRow.Cells["id_producto"].Value.ToString();
            txtCantidad.Text = dgvProductos.CurrentRow.Cells["Cantidad"].Value.ToString();
        
            Editar = true;
            Modificar = true;
        }
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1 && ultimoTextBoxModificado != null)
            {
                string selectedText = listBox.SelectedItem.ToString();
                ultimoTextBoxModificado.Text = selectedText;
            }
            else
                MessageBox.Show("No se pudo determinar el TextBox correspondiente.");

        }
        private System.Windows.Forms.TextBox ultimoTextBoxModificado = null;

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
        //Metodos
        private bool Guardar()
        {
            
            DetallesPedido detalle = new DetallesPedido()
            {
                IdDetalle = IdDetalle,
                IdPedido = IdPedidos,
                IdProducto = IdProducto,       
                Cantidad = Convert.ToInt32(txtCantidad.Text)
            };

            return DetallesPedido.Guardar(detalle, Editar);
        }
        private bool GuardarEditado()
        {
            if (IdPedidos > 0)
            {
                DetallesPedido detalle = new DetallesPedido()
                {
                    IdDetalle = IdDetalle,
                    IdPedido = IdPedidos,
                    IdProducto = IdProducto,
                    Cantidad = Convert.ToInt32(txtCantidad.Text)
                };

                return DetallesPedido.Guardar(detalle, true);  
            }
            else
            {
                MessageBox.Show("Selecciona un producto antes de guardar editado.", "Materia prima no seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private void Finalizar()
        {
            ListarProductos();
            Linpiar();
        }
        private void Linpiar()
        {
            txtProducto.Text = "";
            txtReferencia.Text = "";
            txtCantidad.Text = "";
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
        private void ListarProductos()
        {
            dgvProductos.DataSource = DetallesPedido.ListarProducto();
            DbDatos.OcultarIds(dgvProductos);
            //PersonalizarColumnasGrid();
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
                    if (textBox == txtProducto)
                    {
                        IdProducto = Convert.ToInt32(row["Id_producto"]);
                        string nombreProducto = row["Nombre"].ToString();

                        listBox.Items.Add(nombreProducto);
                    }
                    if (textBox == txtReferencia)
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
        private void ToUpperText()//El upperText para los comboBox esta en comboBox_TextChanged
        {
            txtProducto.CharacterCasing = CharacterCasing.Upper;
            txtProducto.Click += TextBox_Click;
            txtReferencia.CharacterCasing = CharacterCasing.Upper;
            txtReferencia.Click += TextBox_Click;
        }
        private void TextBox_Click(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.TextBox textBox)
            {
                textBox.SelectAll();
            }
        }
        
    }
}
