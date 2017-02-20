namespace NSeriesMonitores {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panelBotones = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCargarEquiposNuevos = new System.Windows.Forms.Button();
            this.btnCargarNSeriesMonitores = new System.Windows.Forms.Button();
            this.btnComprobarNSerie = new System.Windows.Forms.Button();
            this.btnGrabarDatos = new System.Windows.Forms.Button();
            this.btnGrabarGrid = new System.Windows.Forms.Button();
            this.labelSeleccionados = new System.Windows.Forms.Label();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.gbMensajes = new System.Windows.Forms.GroupBox();
            this.tbMensajes = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnInforme = new System.Windows.Forms.Button();
            this.panelBotones.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.gbMensajes.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.flowLayoutPanel1);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBotones.Location = new System.Drawing.Point(3, 3);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(999, 100);
            this.panelBotones.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnCargarEquiposNuevos);
            this.flowLayoutPanel1.Controls.Add(this.btnCargarNSeriesMonitores);
            this.flowLayoutPanel1.Controls.Add(this.btnComprobarNSerie);
            this.flowLayoutPanel1.Controls.Add(this.btnGrabarDatos);
            this.flowLayoutPanel1.Controls.Add(this.btnGrabarGrid);
            this.flowLayoutPanel1.Controls.Add(this.btnInforme);
            this.flowLayoutPanel1.Controls.Add(this.labelSeleccionados);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(950, 100);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // btnCargarEquiposNuevos
            // 
            this.btnCargarEquiposNuevos.AutoSize = true;
            this.btnCargarEquiposNuevos.Location = new System.Drawing.Point(3, 3);
            this.btnCargarEquiposNuevos.Name = "btnCargarEquiposNuevos";
            this.btnCargarEquiposNuevos.Size = new System.Drawing.Size(129, 23);
            this.btnCargarEquiposNuevos.TabIndex = 0;
            this.btnCargarEquiposNuevos.Text = "Cargar Equipos Nuevos";
            this.btnCargarEquiposNuevos.UseVisualStyleBackColor = true;
            this.btnCargarEquiposNuevos.Click += new System.EventHandler(this.btnCargarEquiposNuevos_Click);
            // 
            // btnCargarNSeriesMonitores
            // 
            this.btnCargarNSeriesMonitores.AutoSize = true;
            this.btnCargarNSeriesMonitores.Location = new System.Drawing.Point(138, 3);
            this.btnCargarNSeriesMonitores.Name = "btnCargarNSeriesMonitores";
            this.btnCargarNSeriesMonitores.Size = new System.Drawing.Size(184, 23);
            this.btnCargarNSeriesMonitores.TabIndex = 1;
            this.btnCargarNSeriesMonitores.Text = "Cargar Números de Serie Monitores";
            this.btnCargarNSeriesMonitores.UseVisualStyleBackColor = true;
            this.btnCargarNSeriesMonitores.Click += new System.EventHandler(this.btnCargarNSeriesMonitores_Click);
            // 
            // btnComprobarNSerie
            // 
            this.btnComprobarNSerie.AutoSize = true;
            this.btnComprobarNSerie.Location = new System.Drawing.Point(328, 3);
            this.btnComprobarNSerie.Name = "btnComprobarNSerie";
            this.btnComprobarNSerie.Size = new System.Drawing.Size(140, 23);
            this.btnComprobarNSerie.TabIndex = 2;
            this.btnComprobarNSerie.Text = "Comprobar Números Serie";
            this.btnComprobarNSerie.UseVisualStyleBackColor = true;
            this.btnComprobarNSerie.Click += new System.EventHandler(this.btnComprobarNSerie_Click);
            // 
            // btnGrabarDatos
            // 
            this.btnGrabarDatos.AutoSize = true;
            this.btnGrabarDatos.Location = new System.Drawing.Point(474, 3);
            this.btnGrabarDatos.Name = "btnGrabarDatos";
            this.btnGrabarDatos.Size = new System.Drawing.Size(140, 23);
            this.btnGrabarDatos.TabIndex = 3;
            this.btnGrabarDatos.Text = "Grabar Datos";
            this.btnGrabarDatos.UseVisualStyleBackColor = true;
            this.btnGrabarDatos.Click += new System.EventHandler(this.btnGrabarDatos_Click);
            // 
            // btnGrabarGrid
            // 
            this.btnGrabarGrid.AutoSize = true;
            this.btnGrabarGrid.Location = new System.Drawing.Point(620, 3);
            this.btnGrabarGrid.Name = "btnGrabarGrid";
            this.btnGrabarGrid.Size = new System.Drawing.Size(140, 23);
            this.btnGrabarGrid.TabIndex = 5;
            this.btnGrabarGrid.Text = "Grabar Grid";
            this.btnGrabarGrid.UseVisualStyleBackColor = true;
            this.btnGrabarGrid.Click += new System.EventHandler(this.btnGrabarGrid_Click);
            // 
            // labelSeleccionados
            // 
            this.labelSeleccionados.AutoSize = true;
            this.labelSeleccionados.Location = new System.Drawing.Point(912, 0);
            this.labelSeleccionados.Name = "labelSeleccionados";
            this.labelSeleccionados.Size = new System.Drawing.Size(35, 13);
            this.labelSeleccionados.TabIndex = 4;
            this.labelSeleccionados.Text = "label1";
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.Controls.Add(this.dgv);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(3, 126);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Padding = new System.Windows.Forms.Padding(3);
            this.panelPrincipal.Size = new System.Drawing.Size(999, 297);
            this.panelPrincipal.TabIndex = 0;
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 3);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(993, 291);
            this.dgv.TabIndex = 0;
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            // 
            // gbMensajes
            // 
            this.gbMensajes.Controls.Add(this.tbMensajes);
            this.gbMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbMensajes.Location = new System.Drawing.Point(3, 423);
            this.gbMensajes.Name = "gbMensajes";
            this.gbMensajes.Padding = new System.Windows.Forms.Padding(4);
            this.gbMensajes.Size = new System.Drawing.Size(999, 171);
            this.gbMensajes.TabIndex = 1;
            this.gbMensajes.TabStop = false;
            this.gbMensajes.Text = "Mensajes";
            // 
            // tbMensajes
            // 
            this.tbMensajes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMensajes.Location = new System.Drawing.Point(4, 17);
            this.tbMensajes.Multiline = true;
            this.tbMensajes.Name = "tbMensajes";
            this.tbMensajes.Size = new System.Drawing.Size(991, 150);
            this.tbMensajes.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar.Location = new System.Drawing.Point(3, 103);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(999, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 1;
            // 
            // btnInforme
            // 
            this.btnInforme.AutoSize = true;
            this.btnInforme.Location = new System.Drawing.Point(766, 3);
            this.btnInforme.Name = "btnInforme";
            this.btnInforme.Size = new System.Drawing.Size(140, 23);
            this.btnInforme.TabIndex = 6;
            this.btnInforme.Text = "Informe";
            this.btnInforme.UseVisualStyleBackColor = true;
            this.btnInforme.Click += new System.EventHandler(this.btnInforme_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 597);
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.gbMensajes);
            this.Controls.Add(this.panelBotones);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panelPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.gbMensajes.ResumeLayout(false);
            this.gbMensajes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnCargarEquiposNuevos;
        private System.Windows.Forms.Button btnCargarNSeriesMonitores;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.GroupBox gbMensajes;
        private System.Windows.Forms.TextBox tbMensajes;
        private System.Windows.Forms.Button btnComprobarNSerie;
        private System.Windows.Forms.Button btnGrabarDatos;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelSeleccionados;
        private System.Windows.Forms.Button btnGrabarGrid;
        private System.Windows.Forms.Button btnInforme;
    }
}

