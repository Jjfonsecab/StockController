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
    public partial class FInicio : Form
    {
        public FInicio()
        {
            InitializeComponent();
        }
        private void btnCliente_Click(object sender, EventArgs e)
        {
            FCliente formcliente = new FCliente();
            formcliente.Show();
        }
        private void btnCompras_Click(object sender, EventArgs e)
        {
            FCompras formCompras = new FCompras();
            formCompras.Show();
        }
        private void btnMateriasPrimas_Click(object sender, EventArgs e)
        {
            FMateriasPrimas formmateriasprimas = new FMateriasPrimas();
            formmateriasprimas.Show();
        }
        private void btnProductos_Click(object sender, EventArgs e)
        {
            FProductos formProductos = new FProductos();
            formProductos.Show();
        }
    }
}
