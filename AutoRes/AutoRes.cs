using AutoRes.Forms;
using System.Configuration;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AutoRes
{
    public partial class AutoRes : Form
    {
        private bool exitingFromTray = false;

        public AutoRes()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Programas ejecutables (*.exe)|*.exe";
                ofd.Title = "Seleccionar programa";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string exePath = ofd.FileName;

                    ConfigForm configForm = new ConfigForm(exePath);
                    if (configForm.ShowDialog() == DialogResult.OK)
                    {
                        loadConfigurations();
                    }
                }
            }
        }

        private void AutoRes_Load(object sender, EventArgs e)
        {
            loadConfigurations();
            dgvConfigs.CellValueChanged += dgvConfigs_CellValueChanged;
        }

        private void loadConfigurations()
        {
            try
            {
                dgvConfigs.Rows.Clear();

                var configurations = ConfigurationService.Load();
                foreach (var config in configurations)
                {
                    dgvConfigs.Rows.Add(
                        false,
                        config.Name,
                        config.Resolution,
                        $"{config.Scale}%",
                        config.Id
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar configuraciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvConfigs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvConfigs.Columns["dgvColSelect"].Index)
            {
                bool anyChecked = false;

                foreach (DataGridViewRow r in dgvConfigs.Rows)
                {
                    if (r.IsNewRow) continue;
                    var value = r.Cells["dgvColSelect"].Value;
                    if (value is bool isChecked && isChecked)
                    {
                        anyChecked = true;
                        break;
                    }
                }

                btnDelete.Enabled = anyChecked;
            }
        }


        private void dgvConfigs_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvConfigs.IsCurrentCellDirty &&
                dgvConfigs.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvConfigs.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvConfigs_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgvConfigs.Columns["dgvColSelect"].Index)
            {
                bool marcarTodos = true;
                foreach (DataGridViewRow row in dgvConfigs.Rows)
                {
                    var value = row.Cells["dgvColSelect"].Value;
                    if (value is bool isChecked && !isChecked)
                    {
                        marcarTodos = true;
                        break;
                    }
                    marcarTodos = false;
                }

                foreach (DataGridViewRow row in dgvConfigs.Rows)
                {
                    row.Cells["dgvColSelect"].Value = marcarTodos;
                }

                btnDelete.Enabled = marcarTodos;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = dgvConfigs.Rows.Count - 1; i >= 0; i--)
            {
                var row = dgvConfigs.Rows[i];
                if (row.IsNewRow) continue;

                if (row.Cells["dgvColSelect"].Value is bool isChecked && isChecked)
                {
                    ConfigurationService.Delete(Guid.Parse(row.Cells["dgvColID"].Value.ToString()));
                    dgvConfigs.Rows.RemoveAt(i);
                }
            }

        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                SaveChanges();

                btnSaveChanges.ForeColor = Color.Green;
                btnSaveChanges.Text = "Guardar ✅";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar cambios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSaveChanges.ForeColor = Color.Red;
                btnSaveChanges.Text = "Guardar ❌";
                return;
            }
        }

        private void AutoRes_FormClosing(object sender, FormClosingEventArgs e)
        {
            var configs = ConfigurationService.Load();
            bool cambiosDetectados = false;

            foreach (DataGridViewRow row in dgvConfigs.Rows)
            {
                if (row.IsNewRow) continue;

                string programName = row.Cells["dgvColProgram"].Value?.ToString();
                var conf = configs.FirstOrDefault(c => c.Name.Equals(programName, StringComparison.OrdinalIgnoreCase));

                if (conf != null)
                {
                    string dgvResolution = row.Cells["dgvColResolution"].Value?.ToString();
                    string dgvScaleRaw = row.Cells["dgvColScale"].Value?.ToString();
                    int dgvScale = int.TryParse(dgvScaleRaw?.TrimEnd('%'), out int parsedScale) ? parsedScale : conf.Scale;

                    if (conf.Resolution != dgvResolution || conf.Scale != dgvScale)
                    {
                        cambiosDetectados = true;
                        break;
                    }
                }
            }

            if (cambiosDetectados)
            {
                var result = MessageBox.Show(
                    "Se detectaron cambios en las configuraciones. ¿Desea guardarlos antes de salir?",
                    "Guardar cambios",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    SaveChanges();
                }
            }

            if (!exitingFromTray)
            {
                e.Cancel = true;
                this.Hide();
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(1000, "AutoRes", "La aplicación sigue funcionando en segundo plano.", ToolTipIcon.Info);
            }
        }

        private void SaveChanges()
        {
            var configs = ConfigurationService.Load();

            foreach (DataGridViewRow row in dgvConfigs.Rows)
            {
                if (row.IsNewRow) continue;

                string programName = row.Cells["dgvColProgram"].Value?.ToString();
                var conf = configs.FirstOrDefault(c => c.Name.Equals(programName, StringComparison.OrdinalIgnoreCase));

                if (conf != null)
                {
                    conf.Resolution = row.Cells["dgvColResolution"].Value?.ToString();

                    if (int.TryParse(row.Cells["dgvColScale"].Value?.ToString().TrimEnd('%'), out int scale))
                        conf.Scale = scale;

                    ConfigurationService.Update(conf.Id, conf);
                }
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        private void tsmiCerrarAutoRes_Click(object sender, EventArgs e)
        {
            exitingFromTray = true;
            notifyIcon.Visible = false;
            System.Windows.Forms.Application.Exit();
        }
    }
}
