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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            if (!ValidarCampos(txtNombre, txtPrecio, txtCantidad, txtProveedor, txtFecha))
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
            txtNombre.Text = dgvMateriasPrimas.CurrentRow.Cells["Nombre"].Value.ToString();
            txtPrecio.Text = dgvMateriasPrimas.CurrentRow.Cells["Precio"].Value.ToString();
            txtCantidad.Text = dgvMateriasPrimas.CurrentRow.Cells["Cantidad"].Value.ToString();
            txtPrecio.Text = dgvMateriasPrimas.CurrentRow.Cells["Proveedor"].Value.ToString();

            DateTime fechaOriginal = DateTime.Parse(dgvMateriasPrimas.CurrentRow.Cells["fecha_compra"].Value.ToString());
            txtFecha.Text = fechaOriginal.ToString("yyyy-MM-dd"); txtComentarios.Text = dgvMateriasPrimas.CurrentRow.Cells["Comentarios"].Value.ToString();

            txtComentarios.Text = dgvMateriasPrimas.CurrentRow.Cells["Comentarios"].Value.ToString();

            Editar = true;
            Modificar = true;
        }
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtNombre.Text;

            BuscarYMostrarResultados("RetornarMateriaPrimaPorNombre", txtNombre, listBox, "@NombreBuscado", "Nombre");

        }
        private void txtProveedor_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtProveedor.Text;

            BuscarYMostrarResultados("RetornarMateriaPrimaPorProveedor", txtProveedor, listBox, "@NombreBuscado", "Proveedor");

        }

        private System.Windows.Forms.TextBox ultimoTextBoxModificado = null;
        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            ultimoTextBoxModificado = txtNombre;
            string searchText = txtNombre.Text;
            BuscarYMostrarResultados("RetornarMateriaPrimaPorNombre", txtNombre, listBox, "@NombreBuscado", "Nombre");

        }
        private void txtProveedor_KeyUp(object sender, KeyEventArgs e)
        {
            ultimoTextBoxModificado = txtProveedor;
            string searchText = txtProveedor.Text;
            BuscarYMostrarResultados("RetornarMateriaPrimaPorProveedor", txtProveedor, listBox, "@NombreBuscado", "Proveedor");

        }
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1 && ultimoTextBoxModificado != null)
            {
                // Obtiene el texto seleccionado en el ListBox
                string selectedText = listBox.SelectedItem.ToString();

                // Agrega el texto al TextBox correspondiente
                ultimoTextBoxModificado.Text = selectedText;
            }
            else
            {
                // Depuración: Muestra un mensaje si no se pudo determinar el TextBox correspondiente
                MessageBox.Show("No se pudo determinar el TextBox correspondiente.");
            }
        }

        //Metodos
        private bool Guardar()
        {
            MateriasPrimas materiasPrimas = new MateriasPrimas()
            {
                Nombre = txtNombre.Text,
                Precio = Convert.ToDecimal(txtPrecio.Text),
                Cantidad = Convert.ToInt32(txtCantidad.Text),
                Proveedor = txtProveedor.Text,
                Fecha = DateTime.Parse(txtFecha.Text),
                Comentarios = txtComentarios.Text,
                IdMateriaPrima = IdMateriasPrimas
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
            txtProveedor.Text = "";
            txtComentarios.Text = "";
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
            txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        private void ListarTodo()
        {
            dgvMateriasPrimas.DataSource = MateriasPrimas.ListarTodo();
            DbDatos.OcultarIds(dgvMateriasPrimas);
        }
        private void ToUpperText()//El upperText para los comboBox esta en comboBox_TextChanged
        {
            txtNombre.CharacterCasing = CharacterCasing.Upper;
            txtNombre.Click += TextBox_Click;
            txtProveedor.CharacterCasing = CharacterCasing.Upper;
            txtProveedor.Click += TextBox_Click;
            txtPrecio.CharacterCasing = CharacterCasing.Upper;
            txtPrecio.Click += TextBox_Click;
            txtCantidad.CharacterCasing = CharacterCasing.Upper;
            txtCantidad.Click += TextBox_Click;
            txtFecha.CharacterCasing = CharacterCasing.Upper;
            txtComentarios.CharacterCasing = CharacterCasing.Upper;
        }
        private void TextBox_Click(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.TextBox textBox)
            {
                textBox.SelectAll();
            }
        }               
        private void BuscarYMostrarResultados(string nombreProcedimiento, System.Windows.Forms.TextBox textBox, ListBox listBox, string parametroNombre, string nombreColumna)
        {
            // Obtén el texto del TextBox
            string searchText = textBox.Text;

            // Define los parámetros para el procedimiento almacenado
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro(parametroNombre, searchText)
                // Agrega otros parámetros según sea necesario
            };

            // Llama al método Listar para obtener los resultados de la consulta
            DataTable result = DbDatos.Listar(nombreProcedimiento, parametros);

            // Limpia el ListBox antes de agregar nuevos elementos
            listBox.Items.Clear();

            // Verifica si hay resultados y llena el ListBox
            if (result != null && result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    // Agrega los elementos al ListBox
                    listBox.Items.Add(row[nombreColumna].ToString());
                }
            }

        }
        
    }
}
