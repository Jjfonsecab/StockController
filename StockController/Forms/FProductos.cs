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
        }

        bool Editar;
        int IdProductos;
        bool Modificar;
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos(txtNombre, txtPrecio, txtCantidad, txtDescripcion, txtTiempo))
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
        private void btnInicio_Click(object sender, EventArgs e)
        {

        }
        /*
        private void comboBoxNombre_TextChanged(object sender, EventArgs e)
        {
            comboBox_TextChanged(sender, e);
        }*/

        //Methods
        private bool Guardar()
        {
            if (ValidarCampos())
            {
                Productos productos = new Productos()
                {
                    Nombre = txtNombre.Text,
                    Precio = Convert.ToDecimal(txtPrecio.Text),
                    Cantidad = Convert.ToInt32(txtCantidad.Text),
                    Descripcion = txtDescripcion.Text,
                    TiempoProduccion = txtTiempo.Text,

                };
                return Productos.Guardar(productos, Editar);
            }
            else
            {
                MessageBox.Show("Complete los campos antes de guardar .", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private bool GuardarEditado()
        {
            if (IdProductos > 0)
            {

                Productos productos = new Productos()
                {
                    IdProducto = IdProductos,  // Asegúrate de tener el ID correcto
                    Nombre = txtNombre.Text,
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
                    return MateriasPrimas.Eliminar(IdProductos);
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
        /*
        private void ActualizarDatosComboBox(ComboBox comboBox, string parametroBusqueda)//toDo
        {
            try
            {
                // Llama al stored procedure para obtener datos relacionados
                List<Parametro> parametros = new List<Parametro>
                {
                    new Parametro("@NombreBuscado", parametroBusqueda)
                };
                DataTable resultados;

                // Decide qué stored procedure llamar según el ComboBox
                if (comboBox == comboBoxNombre)
                {
                    resultados = DbDatos.Listar("RetornarProductoPorNombre", parametros);
                }
                else
                    return;                

                // Muestra los resultados en el ComboBox (ajusta según tus necesidades)
                if (resultados != null && resultados.Rows.Count > 0)
                {
                    comboBox.DataSource = resultados;
                    comboBox.Visible = true;
                    comboBox.DroppedDown = true; // Abre el ComboBox para mostrar los resultados

                    if (comboBox == comboBoxNombre)
                    {
                        comboBox.DisplayMember = "NombreProducto";
                    }
                    else                    
                        comboBox.Visible = false;                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar datos del ComboBox: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                Console.WriteLine("TextChanged Event Fired");
                comboBox.Text = comboBox.Text.ToUpper();
                string nombreBuscado = comboBox.Text;
                ActualizarDatosComboBox(comboBox, nombreBuscado);
            }
        }*/

        
    }
}
