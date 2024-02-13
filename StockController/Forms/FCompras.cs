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
            ListarTodo();
            MostrarFechaActual();
        }

        bool Editar;
        int IdMateriasPrimas;
        bool Modificar;

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(comboBoxNombre, txtPrecio, txtCantidad, comboBoxProveedor, txtFecha))
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
            comboBoxNombre.Text = dgvMateriasPrimas.CurrentRow.Cells["Nombre"].Value.ToString();
            txtPrecio.Text = dgvMateriasPrimas.CurrentRow.Cells["Precio"].Value.ToString();
            txtCantidad.Text = dgvMateriasPrimas.CurrentRow.Cells["Cantidad"].Value.ToString();
            comboBoxProveedor.Text = dgvMateriasPrimas.CurrentRow.Cells["Proveedor"].Value.ToString();

            DateTime fechaOriginal = DateTime.Parse(dgvMateriasPrimas.CurrentRow.Cells["fecha_compra"].Value.ToString());
            txtFecha.Text = fechaOriginal.ToString("yyyy-MM-dd"); txtComentarios.Text = dgvMateriasPrimas.CurrentRow.Cells["Comentarios"].Value.ToString();

            txtComentarios.Text = dgvMateriasPrimas.CurrentRow.Cells["Comentarios"].Value.ToString();

            Editar = true;
            Modificar = true;
        }
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatoPunto(txtPrecio, e);
        }
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatoPunto(txtCantidad, e);
        }
        private void comboBoxNombre_TextChanged(object sender, EventArgs e)
        {
            comboBox_TextChanged(sender, e);
        }
        private void comboBoxProveedor_TextChanged(object sender, EventArgs e)
        {
            comboBox_TextChanged(sender, e);
        }

        //Metodos
        private bool Guardar()
        {
            MateriasPrimas materiasPrimas = new MateriasPrimas()
            {
                Nombre = comboBoxNombre.Text,
                Precio = Convert.ToDecimal(txtPrecio.Text),
                Cantidad = Convert.ToInt32(txtCantidad.Text),
                Proveedor = comboBoxProveedor.Text,
                Fecha = DateTime.Parse(txtFecha.Text),
                Comentarios = txtComentarios.Text
            };
            //VerificarExistencia();

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
                    Nombre = comboBoxNombre.Text,
                    Precio = Convert.ToDecimal(txtPrecio.Text),
                    Cantidad = Convert.ToInt32(txtCantidad.Text),
                    Proveedor = comboBoxProveedor.Text,
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
        private void Finalizar()
        {
            ListarTodo();
            Linpiar();
        }
        private void Linpiar()
        {
            comboBoxNombre.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
            comboBoxProveedor.Text = "";
            txtComentarios.Text = "";
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
                else if (control is ComboBox comboBox && comboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, selecciona un valor en todos los campos antes de guardar.", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        private void MostrarFechaActual()
        {
            txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        private void ListarTodo()
        {
            dgvMateriasPrimas.DataSource = MateriasPrimas.ListarTodo();
            DbDatos.OcultarIds(dgvMateriasPrimas);
        }
        private void ToUpperText()//El upperText para los comboBox esta en comboBox_TextChanged
        {            
            txtPrecio.CharacterCasing = CharacterCasing.Upper;
            txtPrecio.Click += TextBox_Click;
            txtCantidad.CharacterCasing = CharacterCasing.Upper;
            txtCantidad.Click += TextBox_Click;
            txtFecha.CharacterCasing = CharacterCasing.Upper;
            txtComentarios.CharacterCasing = CharacterCasing.Upper;
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
                    txtPrecio.Text += ".";
                    txtPrecio.SelectionStart = txtPrecio.Text.Length; // Mover el cursor al final
                }
            }
        }               
        private void ActualizarDatosComboBox(ComboBox comboBox, string parametroBusqueda)
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
                    resultados = DbDatos.Listar("RetornarMateriaPrimaPorNombre", parametros);
                }
                else if (comboBox == comboBoxProveedor)
                {
                    resultados = DbDatos.Listar("RetornarMateriaPrimaPorProveedor", parametros);
                }
                else
                {
                    // Manejo de error o lógica adicional si es necesario
                    return;
                }

                // Muestra los resultados en el ComboBox (ajusta según tus necesidades)
                if (resultados != null && resultados.Rows.Count > 0)
                {
                    comboBox.DataSource = resultados;
                    comboBox.Visible = true;
                    comboBox.DroppedDown = true; // Abre el ComboBox para mostrar los resultados

                    if (comboBox == comboBoxNombre)
                    {
                        comboBox.DisplayMember = "Nombre";
                    }
                    else if (comboBox == comboBoxProveedor)
                    {
                        comboBox.DisplayMember = "Proveedor";
                    }
                    else
                    {
                        comboBox.Visible = false;
                    }
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
                comboBox.Text = comboBox.Text.ToUpper();
                string nombreBuscado = comboBox.Text;
                ActualizarDatosComboBox(comboBox, nombreBuscado);
            }
        }
        private void TextBox_Click(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }
        /*
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
        }*/
    }
}
