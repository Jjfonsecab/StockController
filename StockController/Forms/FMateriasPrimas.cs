using StockController.Class;
using StockController.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
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
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            VerificarExistencia();            
        }
        private void btnStock_Click(object sender, EventArgs e)
        {
            ListarGrid();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ListarTodo();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcionSeleccionada = comboBox.SelectedItem.ToString();

            switch (opcionSeleccionada)
            {                
                case "Nombre":
                    txtBusqueda.Text = "Ingrese un nombre...";
                    break;
                case "Fecha de Compra":
                    txtBusqueda.Text = "AAAA-MM-DD";
                    break;
                case "Proveedor":
                    txtBusqueda.Text = "Ingrese un proveedor...";
                    break;
            }
        }        
        private void dgvMateriasPrimas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PersonalizarColumnasGrid();
        }
        private void dgvMateriasPrimas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ListarGrid();
        }
        //Metodos
        private void ListarGrid()
        {
            dgvMateriasPrimas.DataSource = MateriasPrimas.Listar();
            DbDatos.OcultarIds(dgvMateriasPrimas);
            PersonalizarColumnasGrid();
        }
        private void ListarTodo()
        {
            dgvMateriasPrimas.DataSource = MateriasPrimas.ListarTodo();
            DbDatos.OcultarIds(dgvMateriasPrimas);
            PersonalizarColumnasGrid();
        }
        private void ToUpperText()
        {
            txtBusqueda.CharacterCasing = CharacterCasing.Upper;
            txtBusqueda.Click += TextBox_Click;
        }
        private void PersonalizarColumnasGrid()
        {
            // Itera sobre todas las columnas del DataGridView
            foreach (DataGridViewColumn columna in dgvMateriasPrimas.Columns)
            {
                // Asegúrate de que la columna tenga un nombre
                if (!string.IsNullOrEmpty(columna.Name))
                {

                    // Puedes personalizar las columnas según su nombre o cualquier otra condición necesaria
                    if (columna.Name == "TotalCantidad")
                    {
                        dgvMateriasPrimas.Columns["TotalCantidad"].HeaderText = "Total";
                        DataGridViewCellStyle estiloCeldaNumerica = new DataGridViewCellStyle();
                        estiloCeldaNumerica.Alignment = DataGridViewContentAlignment.MiddleRight;// Alinea a la derecha
                        estiloCeldaNumerica.Format = "N0";
                        columna.DefaultCellStyle = estiloCeldaNumerica;
                        DbDatos.OcultarIds(dgvMateriasPrimas);
                       
                    }
                    else if (columna.Name == "Precio" || columna.Name == "Cantidad")
                    {
                        DataGridViewCellStyle estiloCeldaNumerica = new DataGridViewCellStyle();
                        estiloCeldaNumerica.Alignment = DataGridViewContentAlignment.MiddleRight; // Alinea a la derecha
                        estiloCeldaNumerica.Format = "N0";
                        columna.DefaultCellStyle = estiloCeldaNumerica;
                        DbDatos.OcultarIds(dgvMateriasPrimas);
                    }
                    else if (columna.Name == "fecha_compra")
                    {
                        dgvMateriasPrimas.Columns["fecha_compra"].HeaderText = "Fecha";
                        DbDatos.OcultarIds(dgvMateriasPrimas);
                    }
                }
            }
        }

        private void VerificarExistencia()
        {
            if (comboBox.SelectedIndex != -1)
            {
                string opcionSeleccionada = comboBox.SelectedItem.ToString();
                string valorBusqueda = txtBusqueda.Text.Trim();

                // Diccionario que mapea las opciones del ComboBox a los nombres de los stored procedures
                Dictionary<string, string> storedProcedures = new Dictionary<string, string>
                {
                    { "Nombre", "BuscarMateriaPrimaPorNombre" },
                    { "Fecha de Compra", "BuscarMateriaPrimaPorFecha" },
                    { "Proveedor", "BuscarMateriaPrimaPorProveedor" }
                };

                // Verifica si la opción seleccionada está en el diccionario
                if (storedProcedures.ContainsKey(opcionSeleccionada))
                {
                    // Obtiene el nombre del stored procedure correspondiente
                    string nombreStoredProcedure = storedProcedures[opcionSeleccionada];
                    // Lista de parámetros para enviar al stored procedure
                    List<Parametro> parametros = new List<Parametro>
                    {
                        new Parametro("@NombreBuscado", valorBusqueda)
                    };

                    DataTable resultado = DbDatos.Listar(nombreStoredProcedure, parametros);
                    if (resultado != null && resultado.Rows.Count > 0)
                    {
                        // Muestra los resultados en el DataGridView
                        dgvMateriasPrimas.DataSource = resultado;
                        PersonalizarColumnasGrid();
                    }
                    else
                        // No se encontraron resultados
                        MessageBox.Show("Item no Encontrado.", "Item Inexistente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Por favor seleccione una opción de búsqueda válida.");
            }
            else
                MessageBox.Show("Por favor seleccione una opción de búsqueda.");

        }
        private void TextBox_Click(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }

    }

}
