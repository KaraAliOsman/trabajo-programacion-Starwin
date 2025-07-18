using GestionProyectosStarwin.Datos;
using GestionProyectosStarwin.Modelos;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace GestionProyectosStarwin
{
    public partial class Form1 : Form
    {
        private AppContexto contexto;

        public Form1()
        {
            InitializeComponent();
            contexto = new AppContexto();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                contexto.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar o crear la base de datos: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            ConfigurarDataGridView();
            CargarEstadosComboBox();
            CargarProyectos();

            // Conectar el evento de validación
            dgvProyectos.CellValidating += dgvProyectos_CellValidating;
        }

        private void ConfigurarDataGridView()
        {
            dgvProyectos.AutoGenerateColumns = false;
            dgvProyectos.Columns.Clear();

            dgvProyectos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", HeaderText = "ID", ReadOnly = true });
            dgvProyectos.Columns.Add(new DataGridViewTextBoxColumn { Name = "NombreCliente", DataPropertyName = "NombreCliente", HeaderText = "Nombre del Cliente", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProyectos.Columns.Add(new DataGridViewTextBoxColumn { Name = "DescripcionProyecto", DataPropertyName = "DescripcionProyecto", HeaderText = "Descripción", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProyectos.Columns.Add(new DataGridViewTextBoxColumn { Name = "PresupuestoTotal", DataPropertyName = "PresupuestoTotal", HeaderText = "Presupuesto", DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });
            dgvProyectos.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaEntregaEstimada", DataPropertyName = "FechaEntregaEstimada", HeaderText = "Fecha Entrega", DefaultCellStyle = new DataGridViewCellStyle { Format = "d" } });

            // Columna de Estado como ComboBox
            var columnaEstado = new DataGridViewComboBoxColumn
            {
                Name = "Estado",
                DataPropertyName = "Estado",
                HeaderText = "Estado",
                FlatStyle = FlatStyle.Flat
            };
            columnaEstado.Items.Add("Cotizado");
            columnaEstado.Items.Add("En Producción");
            columnaEstado.Items.Add("Instalado");
            columnaEstado.Items.Add("Pagado");
            dgvProyectos.Columns.Add(columnaEstado);
        }

        private void CargarEstadosComboBox()
        {
            cmbEstado.Items.Add("Cotizado");
            cmbEstado.Items.Add("En Producción");
            cmbEstado.Items.Add("Instalado");
            cmbEstado.Items.Add("Pagado");
            cmbEstado.SelectedIndex = 0;
        }

        private void CargarProyectos()
        {
            string filtro = txtFiltroNombre.Text.ToLower();
            var proyectos = contexto.Proyectos.Where(p => string.IsNullOrEmpty(filtro) || p.NombreCliente.ToLower().Contains(filtro)).ToList();
            dgvProyectos.DataSource = proyectos;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var nuevoProyecto = new Proyecto
            {
                NombreCliente = txtNombreCliente.Text.Trim(),
                DescripcionProyecto = txtDescripcion.Text.Trim(),
                PresupuestoTotal = numPresupuesto.Value,
                FechaEntregaEstimada = dtpFechaEntrega.Value,
                Estado = cmbEstado.SelectedItem?.ToString() ?? ""
            };

            var validationContext = new ValidationContext(nuevoProyecto);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(nuevoProyecto, validationContext, validationResults, true);

            if (!isValid)
            {
                var errores = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                MessageBox.Show(errores, "Datos no válidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                contexto.Proyectos.Add(nuevoProyecto);
                contexto.SaveChanges();
                MessageBox.Show("Proyecto agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormularioCreacion();
                CargarProyectos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar el proyecto: {ex.Message}", "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormularioCreacion()
        {
            txtNombreCliente.Clear();
            txtDescripcion.Clear();
            numPresupuesto.Value = 0;
            dtpFechaEntrega.Value = DateTime.Now;
            cmbEstado.SelectedIndex = 0;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdEliminar.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID para eliminar.", "ID Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtIdEliminar.Text, out int idParaEliminar))
            {
                MessageBox.Show("El ID ingresado no es un número válido.", "ID Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var proyectoAEliminar = contexto.Proyectos.Find(idParaEliminar);
            if (proyectoAEliminar == null)
            {
                MessageBox.Show($"No se encontró ningún proyecto con el ID {idParaEliminar}.", "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show($"¿Está seguro de que desea eliminar el proyecto de '{proyectoAEliminar.NombreCliente}'?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                contexto.Proyectos.Remove(proyectoAEliminar);
                contexto.SaveChanges();
                MessageBox.Show("Proyecto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtIdEliminar.Clear();
                CargarProyectos();
            }
        }

        private void dgvProyectos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string nombreColumna = dgvProyectos.Columns[e.ColumnIndex].Name;

            if (dgvProyectos.Rows[e.RowIndex].IsNewRow) return;

            if (nombreColumna.Equals("NombreCliente"))
            {
                if (string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                {
                    MessageBox.Show("El nombre del cliente no puede estar vacío.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
            else if (nombreColumna.Equals("PresupuestoTotal"))
            {
                if (!decimal.TryParse(e.FormattedValue.ToString(), out decimal presupuesto) || presupuesto < 0)
                {
                    MessageBox.Show("Por favor, ingrese un valor numérico válido y positivo.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
            else if (nombreColumna.Equals("FechaEntregaEstimada"))
            {
                if (!DateTime.TryParse(e.FormattedValue.ToString(), out _))
                {
                    MessageBox.Show("Por favor, ingrese una fecha con formato válido.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        private void dgvProyectos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var idProyecto = (int)dgvProyectos.Rows[e.RowIndex].Cells["Id"].Value;
            var proyectoAActualizar = contexto.Proyectos.Find(idProyecto);

            if (proyectoAActualizar == null) return;

            try
            {
                object nuevoValor = dgvProyectos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                var nombrePropiedad = dgvProyectos.Columns[e.ColumnIndex].DataPropertyName;
                var propiedad = typeof(Proyecto).GetProperty(nombrePropiedad);

                if (propiedad != null && propiedad.CanWrite)
                {
                    var valorConvertido = Convert.ChangeType(nuevoValor, propiedad.PropertyType);
                    propiedad.SetValue(proyectoAActualizar, valorConvertido);
                }
                contexto.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el dato: {ex.Message}\nLos cambios no se guardaron.", "Error de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CargarProyectos();
            }
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            CargarProyectos();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            contexto?.Dispose();
        }
    }
}
