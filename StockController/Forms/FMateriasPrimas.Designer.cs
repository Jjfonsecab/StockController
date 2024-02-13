namespace StockController.Forms
{
    partial class FMateriasPrimas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMateriasPrimas));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCompras = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.PictureBox();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.btnInicio = new System.Windows.Forms.PictureBox();
            this.btnTodosLosDatos = new System.Windows.Forms.PictureBox();
            this.dgvMateriasPrimas = new System.Windows.Forms.DataGridView();
            this.btnStock = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCompras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBuscar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTodosLosDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriasPrimas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Buscar por : ";
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "Nombre",
            "Fecha de Compra",
            "Proveedor"});
            this.comboBox.Location = new System.Drawing.Point(143, 49);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(147, 28);
            this.comboBox.TabIndex = 2;
            this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStock);
            this.groupBox1.Controls.Add(this.btnCompras);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtBusqueda);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox);
            this.groupBox1.Location = new System.Drawing.Point(200, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 146);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos : ";
            // 
            // btnCompras
            // 
            this.btnCompras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompras.Image = ((System.Drawing.Image)(resources.GetObject("btnCompras.Image")));
            this.btnCompras.Location = new System.Drawing.Point(688, 54);
            this.btnCompras.Name = "btnCompras";
            this.btnCompras.Size = new System.Drawing.Size(57, 53);
            this.btnCompras.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCompras.TabIndex = 6;
            this.btnCompras.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(322, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Datos de busqueda : ";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(608, 54);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(57, 53);
            this.btnBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.TabStop = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Location = new System.Drawing.Point(314, 81);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(259, 26);
            this.txtBusqueda.TabIndex = 3;
            // 
            // btnInicio
            // 
            this.btnInicio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInicio.Image = ((System.Drawing.Image)(resources.GetObject("btnInicio.Image")));
            this.btnInicio.Location = new System.Drawing.Point(89, 62);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(57, 53);
            this.btnInicio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnInicio.TabIndex = 7;
            this.btnInicio.TabStop = false;
            // 
            // btnTodosLosDatos
            // 
            this.btnTodosLosDatos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTodosLosDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnTodosLosDatos.Image")));
            this.btnTodosLosDatos.Location = new System.Drawing.Point(103, 133);
            this.btnTodosLosDatos.Name = "btnTodosLosDatos";
            this.btnTodosLosDatos.Size = new System.Drawing.Size(43, 40);
            this.btnTodosLosDatos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnTodosLosDatos.TabIndex = 7;
            this.btnTodosLosDatos.TabStop = false;
            this.btnTodosLosDatos.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // dgvMateriasPrimas
            // 
            this.dgvMateriasPrimas.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvMateriasPrimas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMateriasPrimas.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvMateriasPrimas.Location = new System.Drawing.Point(40, 220);
            this.dgvMateriasPrimas.Name = "dgvMateriasPrimas";
            this.dgvMateriasPrimas.RowHeadersWidth = 62;
            this.dgvMateriasPrimas.RowTemplate.Height = 28;
            this.dgvMateriasPrimas.Size = new System.Drawing.Size(975, 400);
            this.dgvMateriasPrimas.TabIndex = 8;
            this.dgvMateriasPrimas.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMateriasPrimas_CellFormatting);
            // 
            // btnStock
            // 
            this.btnStock.BackColor = System.Drawing.Color.Gold;
            this.btnStock.Location = new System.Drawing.Point(60, 98);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(75, 42);
            this.btnStock.TabIndex = 7;
            this.btnStock.Text = "Stock";
            this.btnStock.UseVisualStyleBackColor = false;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // FMateriasPrimas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 632);
            this.Controls.Add(this.dgvMateriasPrimas);
            this.Controls.Add(this.btnTodosLosDatos);
            this.Controls.Add(this.btnInicio);
            this.Controls.Add(this.groupBox1);
            this.Name = "FMateriasPrimas";
            this.Text = " Materiales en Stock";
            this.Load += new System.EventHandler(this.FMateriasPrimas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCompras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBuscar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTodosLosDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriasPrimas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.PictureBox btnBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox btnCompras;
        private System.Windows.Forms.PictureBox btnInicio;
        private System.Windows.Forms.PictureBox btnTodosLosDatos;
        private System.Windows.Forms.DataGridView dgvMateriasPrimas;
        private System.Windows.Forms.Button btnStock;
    }
}