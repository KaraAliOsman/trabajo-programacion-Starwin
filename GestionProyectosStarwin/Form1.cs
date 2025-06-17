using GestionProyectosStarwin.Datos;
using GestionProyectosStarwin.Modelos;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace GestionProyectosStarwin
{
    public partial class Form1 : Form
    {
        private readonly AppContexto _contexto;

        public Form1()
        {
            InitializeComponent();
            _contexto = new AppContexto();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _contexto.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar o crear la base de datos: {ex.Message}", "Error Cr�tico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            ConfigurarDataGridView();
            CargarEstadosComboBox();
            CargarProyectos();
        }

        private void ConfigurarDataGridView()
        {
            dgvProyectos.AutoGenerateColumns = false;
            dgvProyectos.Columns.Clear();

            dgvProyectos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", HeaderText = "ID", ReadOnly = true });
            dgvProyectos.Columns.Add(new DataGridViewTextBoxColumn { Name = "NombreCliente", DataPropertyName = "NombreCliente", HeaderText = "Nombre del Cliente", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProyectos.Columns.Add(new DataGridViewTextBoxColumn { Name = "DescripcionProyecto", DataPropertyName = "DescripcionProyecto", HeaderText = "Descripci�n", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProyectos.Columns.Add(new DataGridViewTextBoxColumn { Name = "PresupuestoTotal", DataPropertyName = "PresupuestoTotal", HeaderText = "Presupuesto", DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });
            dgvProyectos.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaEntregaEstimada", DataPropertyName = "FechaEntregaEstimada", HeaderText = "Fecha Entrega", DefaultCellStyle = new DataGridViewCellStyle { Format = "d" } });
            dgvProyectos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", DataPropertyName = "Estado", HeaderText = "Estado" });
        }

        private void CargarEstadosComboBox()
        {
            cmbEstado.Items.Add("Cotizado");
            cmbEstado.Items.Add("En Producci�n");
            cmbEstado.Items.Add("Instalado");
            cmbEstado.Items.Add("Pagado");
            cmbEstado.SelectedIndex = 0;
        }

        private void CargarProyectos()
        {
            string filtro = txtFiltroNombre.Text.ToLower();
            var proyectos = _contexto.Proyectos
                                     .AsNoTracking()
                                     .Where(p => string.IsNullOrEmpty(filtro) || p.NombreCliente.ToLower().Contains(filtro))
                                     .ToList();
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
                MessageBox.Show(errores, "Datos no v�lidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _contexto.Proyectos.Add(nuevoProyecto);
                _contexto.SaveChanges();
                MessageBox.Show("Proyecto agregado exitosamente.", "�xito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormularioCreacion();
                CargarProyectos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurri� un error al guardar el proyecto: {ex.Message}", "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (!int.TryParse(txtIdEliminar.Text, out int idParaEliminar))
            {
                MessageBox.Show("El ID ingresado no es un n�mero v�lido.", "ID Inv�lido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var proyectoAEliminar = _contexto.Proyectos.Find(idParaEliminar);
            if (proyectoAEliminar == null)
            {
                MessageBox.Show($"No se encontr� ning�n proyecto con el ID {idParaEliminar}.", "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show($"�Est� seguro de que desea eliminar el proyecto de '{proyectoAEliminar.NombreCliente}'?", "Confirmar Eliminaci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                _contexto.Proyectos.Remove(proyectoAEliminar);
                _contexto.SaveChanges();
                MessageBox.Show("Proyecto eliminado correctamente.", "�xito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtIdEliminar.Clear();
                CargarProyectos();
            }
        }

        private void dgvProyectos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var idProyecto = (int)dgvProyectos.Rows[e.RowIndex].Cells["Id"].Value;

            try
            {
                var proyectoAActualizar = _contexto.Proyectos.Find(idProyecto);
                if (proyectoAActualizar == null) return;
                object nuevoValor = dgvProyectos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                var nombrePropiedad = dgvProyectos.Columns[e.ColumnIndex].DataPropertyName;
                var propiedad = typeof(Proyecto).GetProperty(nombrePropiedad);
                if (propiedad != null && propiedad.CanWrite)
                {
                    var valorConvertido = Convert.ChangeType(nuevoValor, propiedad.PropertyType);
                    propiedad.SetValue(proyectoAActualizar, valorConvertido);
                }
                _contexto.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el dato: {ex.Message}", "Error de Actualizaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            _contexto?.Dispose();
        }
    }
}