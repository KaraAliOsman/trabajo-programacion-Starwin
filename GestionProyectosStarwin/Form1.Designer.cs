namespace GestionProyectosStarwin
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new System.Windows.Forms.Label();
            txtNombreCliente = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            txtDescripcion = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            numPresupuesto = new System.Windows.Forms.NumericUpDown();
            label4 = new System.Windows.Forms.Label();
            dtpFechaEntrega = new System.Windows.Forms.DateTimePicker();
            label5 = new System.Windows.Forms.Label();
            cmbEstado = new System.Windows.Forms.ComboBox();
            btnAgregar = new System.Windows.Forms.Button();
            dgvProyectos = new System.Windows.Forms.DataGridView();
            label6 = new System.Windows.Forms.Label();
            txtFiltroNombre = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            txtIdEliminar = new System.Windows.Forms.TextBox();
            btnEliminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)numPresupuesto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProyectos).BeginInit();
            SuspendLayout();

            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 15);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(94, 15);
            label1.Text = "Nombre Cliente:";
            // 
            // txtNombreCliente
            // 
            txtNombreCliente.Location = new System.Drawing.Point(112, 12);
            txtNombreCliente.Name = "txtNombreCliente";
            txtNombreCliente.Size = new System.Drawing.Size(280, 23);
            txtNombreCliente.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 44);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(72, 15);
            label2.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new System.Drawing.Point(112, 41);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new System.Drawing.Size(280, 23);
            txtDescripcion.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(410, 15);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(76, 15);
            label3.Text = "Presupuesto:";
            // 
            // numPresupuesto
            // 
            numPresupuesto.DecimalPlaces = 2;
            numPresupuesto.Location = new System.Drawing.Point(492, 12);
            numPresupuesto.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            numPresupuesto.Name = "numPresupuesto";
            numPresupuesto.Size = new System.Drawing.Size(120, 23);
            numPresupuesto.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(410, 44);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(40, 15);
            label4.Text = "Fecha:";
            // 
            // dtpFechaEntrega
            // 
            dtpFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpFechaEntrega.Location = new System.Drawing.Point(492, 41);
            dtpFechaEntrega.Name = "dtpFechaEntrega";
            dtpFechaEntrega.Size = new System.Drawing.Size(120, 23);
            dtpFechaEntrega.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(12, 73);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(45, 15);
            label5.Text = "Estado:";
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new System.Drawing.Point(112, 70);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new System.Drawing.Size(280, 23);
            cmbEstado.TabIndex = 4;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new System.Drawing.Point(630, 12);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new System.Drawing.Size(158, 81);
            btnAgregar.TabIndex = 5;
            btnAgregar.Text = "Agregar Proyecto";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;

            // Configuración de la tabla de datos
            // 
            // dgvProyectos
            // 
            dgvProyectos.AllowUserToAddRows = false;
            dgvProyectos.AllowUserToDeleteRows = false;
            dgvProyectos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvProyectos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProyectos.Location = new System.Drawing.Point(12, 110);
            dgvProyectos.Name = "dgvProyectos";
            dgvProyectos.Size = new System.Drawing.Size(776, 360);
            dgvProyectos.TabIndex = 6;
            dgvProyectos.CellValueChanged += dgvProyectos_CellValueChanged;

            // Configuración de controles de acciones (filtrar y eliminar) en la parte inferior
            // 
            // label6
            // 
            label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(12, 484);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(107, 15);
            label6.Text = "Filtrar por nombre:";
            // 
            // txtFiltroNombre
            // 
            txtFiltroNombre.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            txtFiltroNombre.Location = new System.Drawing.Point(125, 481);
            txtFiltroNombre.Name = "txtFiltroNombre";
            txtFiltroNombre.Size = new System.Drawing.Size(250, 23);
            txtFiltroNombre.TabIndex = 7;
            txtFiltroNombre.TextChanged += txtFiltroNombre_TextChanged;
            // 
            // label7
            // 
            label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(470, 484);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(92, 15);
            label7.Text = "ID para Eliminar:";
            // 
            // txtIdEliminar
            // 
            txtIdEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            txtIdEliminar.Location = new System.Drawing.Point(568, 481);
            txtIdEliminar.Name = "txtIdEliminar";
            txtIdEliminar.Size = new System.Drawing.Size(80, 23);
            txtIdEliminar.TabIndex = 8;
            // 
            // btnEliminar
            // 
            btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnEliminar.Location = new System.Drawing.Point(654, 479);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new System.Drawing.Size(134, 25);
            btnEliminar.TabIndex = 9;
            btnEliminar.Text = "Eliminar Proyecto";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;

            // Configuración final del formulario
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 518);
            Controls.Add(btnEliminar);
            Controls.Add(txtIdEliminar);
            Controls.Add(label7);
            Controls.Add(txtFiltroNombre);
            Controls.Add(label6);
            Controls.Add(dgvProyectos);
            Controls.Add(btnAgregar);
            Controls.Add(cmbEstado);
            Controls.Add(label5);
            Controls.Add(dtpFechaEntrega);
            Controls.Add(label4);
            Controls.Add(numPresupuesto);
            Controls.Add(label3);
            Controls.Add(txtDescripcion);
            Controls.Add(label2);
            Controls.Add(txtNombreCliente);
            Controls.Add(label1);
            MinimumSize = new System.Drawing.Size(816, 420);
            Name = "Form1";
            Text = "Gestión de Proyectos - Starwin PVC";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numPresupuesto).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProyectos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numPresupuesto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaEntrega;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView dgvProyectos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFiltroNombre;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIdEliminar;
        private System.Windows.Forms.Button btnEliminar;
    }
}