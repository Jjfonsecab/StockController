namespace StockController
{
    partial class ClienteForm
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
            this.components = new System.ComponentModel.Container();
            this.dgvDatosCliente = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datosGb = new System.Windows.Forms.GroupBox();
            this.txtCiudad = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.guardarBtn = new System.Windows.Forms.Button();
            this.eliminarBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosCliente)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.datosGb.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDatosCliente
            // 
            this.dgvDatosCliente.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDatosCliente.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDatosCliente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatosCliente.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvDatosCliente.Location = new System.Drawing.Point(40, 250);
            this.dgvDatosCliente.Name = "dgvDatosCliente";
            this.dgvDatosCliente.RowHeadersWidth = 62;
            this.dgvDatosCliente.RowTemplate.Height = 28;
            this.dgvDatosCliente.Size = new System.Drawing.Size(680, 305);
            this.dgvDatosCliente.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(130, 36);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(129, 32);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // datosGb
            // 
            this.datosGb.Controls.Add(this.txtCiudad);
            this.datosGb.Controls.Add(this.label4);
            this.datosGb.Controls.Add(this.label3);
            this.datosGb.Controls.Add(this.label1);
            this.datosGb.Controls.Add(this.txtDireccion);
            this.datosGb.Controls.Add(this.txtNombre);
            this.datosGb.Location = new System.Drawing.Point(40, 43);
            this.datosGb.Name = "datosGb";
            this.datosGb.Size = new System.Drawing.Size(501, 175);
            this.datosGb.TabIndex = 1;
            this.datosGb.TabStop = false;
            this.datosGb.Text = "Datos del Cliente: ";
            // 
            // txtCiudad
            // 
            this.txtCiudad.Location = new System.Drawing.Point(209, 116);
            this.txtCiudad.Name = "txtCiudad";
            this.txtCiudad.Size = new System.Drawing.Size(258, 26);
            this.txtCiudad.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Ciudad :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Direccion :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nombre :";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(209, 76);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(258, 26);
            this.txtDireccion.TabIndex = 2;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(209, 37);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(258, 26);
            this.txtNombre.TabIndex = 0;
            this.txtNombre.TextChanged += new System.EventHandler(this.ClienteForm_Load);
            // 
            // guardarBtn
            // 
            this.guardarBtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.guardarBtn.Location = new System.Drawing.Point(581, 60);
            this.guardarBtn.Name = "guardarBtn";
            this.guardarBtn.Size = new System.Drawing.Size(139, 43);
            this.guardarBtn.TabIndex = 2;
            this.guardarBtn.Text = "Guardar";
            this.guardarBtn.UseVisualStyleBackColor = false;
            this.guardarBtn.Click += new System.EventHandler(this.guardarBtn_Click);
            // 
            // eliminarBtn
            // 
            this.eliminarBtn.BackColor = System.Drawing.Color.RosyBrown;
            this.eliminarBtn.Location = new System.Drawing.Point(581, 122);
            this.eliminarBtn.Name = "eliminarBtn";
            this.eliminarBtn.Size = new System.Drawing.Size(139, 43);
            this.eliminarBtn.TabIndex = 4;
            this.eliminarBtn.Text = "Eliminar";
            this.eliminarBtn.UseVisualStyleBackColor = false;
            this.eliminarBtn.Click += new System.EventHandler(this.eliminarBtn_Click);
            // 
            // ClienteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 567);
            this.Controls.Add(this.eliminarBtn);
            this.Controls.Add(this.guardarBtn);
            this.Controls.Add(this.datosGb);
            this.Controls.Add(this.dgvDatosCliente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ClienteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cliente";
            this.Load += new System.EventHandler(this.ClienteForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosCliente)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.datosGb.ResumeLayout(false);
            this.datosGb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDatosCliente;
        private System.Windows.Forms.GroupBox datosGb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button guardarBtn;
        private System.Windows.Forms.Button eliminarBtn;
        private System.Windows.Forms.TextBox txtCiudad;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
    }
}

