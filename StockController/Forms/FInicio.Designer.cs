namespace StockController.Forms
{
    partial class FInicio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FInicio));
            this.btnCliente = new System.Windows.Forms.PictureBox();
            this.btnCompras = new System.Windows.Forms.PictureBox();
            this.btnMateriasPrimas = new System.Windows.Forms.PictureBox();
            this.btnProductos = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCompras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMateriasPrimas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCliente
            // 
            this.btnCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnCliente.Image")));
            this.btnCliente.Location = new System.Drawing.Point(679, 33);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(59, 52);
            this.btnCliente.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCliente.TabIndex = 0;
            this.btnCliente.TabStop = false;
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // btnCompras
            // 
            this.btnCompras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompras.Image = ((System.Drawing.Image)(resources.GetObject("btnCompras.Image")));
            this.btnCompras.Location = new System.Drawing.Point(549, 102);
            this.btnCompras.Name = "btnCompras";
            this.btnCompras.Size = new System.Drawing.Size(59, 52);
            this.btnCompras.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCompras.TabIndex = 1;
            this.btnCompras.TabStop = false;
            this.btnCompras.Click += new System.EventHandler(this.btnCompras_Click);
            // 
            // btnMateriasPrimas
            // 
            this.btnMateriasPrimas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMateriasPrimas.Image = ((System.Drawing.Image)(resources.GetObject("btnMateriasPrimas.Image")));
            this.btnMateriasPrimas.Location = new System.Drawing.Point(679, 163);
            this.btnMateriasPrimas.Name = "btnMateriasPrimas";
            this.btnMateriasPrimas.Size = new System.Drawing.Size(59, 52);
            this.btnMateriasPrimas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnMateriasPrimas.TabIndex = 2;
            this.btnMateriasPrimas.TabStop = false;
            this.btnMateriasPrimas.Click += new System.EventHandler(this.btnMateriasPrimas_Click);
            // 
            // btnProductos
            // 
            this.btnProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProductos.Image = ((System.Drawing.Image)(resources.GetObject("btnProductos.Image")));
            this.btnProductos.Location = new System.Drawing.Point(549, 260);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(59, 52);
            this.btnProductos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnProductos.TabIndex = 3;
            this.btnProductos.TabStop = false;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(680, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cliente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(535, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Compras";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(641, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Materias Primas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(535, 346);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Productos";
            // 
            // FInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 435);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnProductos);
            this.Controls.Add(this.btnMateriasPrimas);
            this.Controls.Add(this.btnCompras);
            this.Controls.Add(this.btnCliente);
            this.Name = "FInicio";
            this.Text = "Inicio";
            ((System.ComponentModel.ISupportInitialize)(this.btnCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCompras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMateriasPrimas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnCliente;
        private System.Windows.Forms.PictureBox btnCompras;
        private System.Windows.Forms.PictureBox btnMateriasPrimas;
        private System.Windows.Forms.PictureBox btnProductos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}