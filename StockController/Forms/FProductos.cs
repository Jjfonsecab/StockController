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
    public partial class FProductos : Form
    {
        public FProductos()
        {
            InitializeComponent();
            ToUpperText();
        }
        private void FProductos_Load(object sender, EventArgs e)
        {
            ListarTodo();
            PersonalizarColumnasGrid();
        }

        bool Editar;
        int IdProductos;
        bool Modificar;

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(txtNombre, txtReferencia,txtPrecio, txtCantidad, txtDescripcion, txtTiempo))
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
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0 || dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una fila antes de eliminar.");
                return;
            }

            if (dgvProductos.SelectedRows.Count > 0)
            {
                DataGridViewCell cell = dgvProductos.CurrentRow.Cells["id_producto"];
                int id_producto = Convert.ToInt32(cell.Value);
                IdProductos = id_producto;
                Eliminar();
                Finalizar();
            }
        }
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatoPunto(txtPrecio, e);
        }
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatoPunto(txtCantidad, e);
        }
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IdProductos = Convert.ToInt32(dgvProductos.CurrentRow.Cells["id_producto"].Value);
            txtNombre.Text = dgvProductos.CurrentRow.Cells["Nombre"].Value.ToString();
            txtPrecio.Text = dgvProductos.CurrentRow.Cells["Precio_final"].Value.ToString();
            txtCantidad.Text = dgvProductos.CurrentRow.Cells["Cantidad"].Value.ToString();
            txtDescripcion.Text = dgvProductos.CurrentRow.Cells["Descripcion"].Value.ToString();
            txtTiempo.Text = dgvProductos.CurrentRow.Cells["tiempo_produccion"].Value.ToString();

            Editar = true;
            Modificar = true;
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

        private System.Windows.Forms.TextBox ultimoTextBoxModificado = null;
        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            ultimoTextBoxModificado = txtNombre;
            string searchText = txtNombre.Text;
            BuscarYMostrarResultados("[RetornarProductoPorNombre]", txtNombre, listBox, "@NombreBuscado", "Nombre");
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
                MessageBox.Show("Seleccione un dato valido.");
            }
        }
        //Methods
        private bool Guardar()
        {
            Productos productos = new Productos()
            {
                Nombre = txtNombre.Text,
                Referencia = txtReferencia.Text,
                Precio = Convert.ToDecimal(txtPrecio.Text),
                Cantidad = Convert.ToInt32(txtCantidad.Text),
                Descripcion = txtDescripcion.Text,
                TiempoProduccion = txtTiempo.Text,
                IdProducto = IdProductos

            };
            return Productos.Guardar(productos, Editar);

        }
        private bool GuardarEditado()
        {
            if (IdProductos > 0)
            {

                Productos productos = new Productos()
                {
                    IdProducto = IdProductos,
                    Nombre = txtNombre.Text,
                    Referencia = txtReferencia.Text,
                    Precio = Convert.ToDecimal(txtPrecio.Text),
                    Cantidad = Convert.ToInt32(txtCantidad.Text),
                    Descripcion = txtDescripcion.Text,
                    TiempoProduccion = txtTiempo.Text
                };

                return Productos.Guardar(productos, true);  // true indica modo edición
            }
            else
            {
                MessageBox.Show("Complete los campos antes de guardar editado.", "Producto no seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

        }
        private bool Eliminar()
        {
            if (IdProductos > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este Producto?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                    return Productos.Eliminar(IdProductos);
                else
                    return false;
            }
            else
            {
                MessageBox.Show("Selecciona un Producto antes de eliminar.", "Producto no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
            txtDescripcion.Text = "";
            txtTiempo.Text = "";
            Editar = false;
        }
        private bool ValidarCampos(params Control[] controles)
        {
            foreach (var control in controles)
            {
                if (control is TextBox textBox && string.IsNullOrEmpty(textBox.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos antes de guardar.", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

            }
            return true;
        }
        private void ListarTodo()
        {
            dgvProductos.DataSource = Productos.ListarTodo();
            DbDatos.OcultarIds(dgvProductos);
        }
        private void ToUpperText()//El upperText para los comboBox esta en comboBox_TextChanged
        {
            txtNombre.CharacterCasing = CharacterCasing.Upper;
            txtNombre.Click += TextBox_Click;
            txtReferencia.CharacterCasing = CharacterCasing.Upper;
            txtReferencia.Click += TextBox_Click;
            txtPrecio.CharacterCasing = CharacterCasing.Upper;
            txtPrecio.Click += TextBox_Click;
            txtCantidad.CharacterCasing = CharacterCasing.Upper;
            txtCantidad.Click += TextBox_Click;
            txtTiempo.CharacterCasing = CharacterCasing.Upper;
            txtTiempo.Click += TextBox_Click;
            txtDescripcion.CharacterCasing = CharacterCasing.Upper;
        }
        private void TextBox_Click(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }
        private void FormatoPunto(TextBox textBox, KeyPressEventArgs e)
        {
            // Verificar si la tecla presionada es un número y no una tecla de control
            if (char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Obtener el texto actual del TextBox
                string currentText = textBox.Text.Replace(".", "");

                // Agregar un punto después de cada tercer carácter
                if (currentText.Length % 3 == 0 && currentText.Length > 0)
                {
                    textBox.Text += ".";
                    textBox.SelectionStart = textBox.Text.Length; // Mover el cursor al final
                }
            }
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
                    listBox.Items.Add(row[nombreColumna].ToString());
                }
            }

        }
        private void PersonalizarColumnasGrid()
        {
            // Itera sobre todas las columnas del DataGridView
            foreach (DataGridViewColumn columna in dgvProductos.Columns)
            {
                ConfigurarCabeceraColumna(columna, columna.HeaderText);
                // Asegúrate de que la columna tenga un nombre
                if (!string.IsNullOrEmpty(columna.Name))
                {

                    // Puedes personalizar las columnas según su nombre o cualquier otra condición necesaria
                    if (columna.Name == "Precio" || columna.Name == "Cantidad")
                    {
                        dgvProductos.Columns["Precio"].HeaderText = "Precio por Unidad";
                        DataGridViewCellStyle estiloCeldaNumerica = new DataGridViewCellStyle();
                        estiloCeldaNumerica.Alignment = DataGridViewContentAlignment.MiddleRight; // Alinea a la derecha
                        estiloCeldaNumerica.Format = "N0";
                        columna.DefaultCellStyle = estiloCeldaNumerica;
                        DbDatos.OcultarIds(dgvProductos);
                    }
                    else if (columna.Name == "tiempo_produccion")
                    {
                        dgvProductos.Columns["tiempo_produccion"].HeaderText = "Tiempo";
                        DbDatos.OcultarIds(dgvProductos);
                    }
                    else if (columna.Name == "Referencia")                    
                        dgvProductos.Columns["Referencia"].DisplayIndex = 2;
                    
                }
            }
        }
        private void ConfigurarCabeceraColumna(DataGridViewColumn columna, string nuevoHeaderText)
        {
            columna.HeaderText = nuevoHeaderText;
            columna.HeaderCell.Style.Font = new Font(columna.DataGridView.Font, FontStyle.Bold);
        }
    }
}
