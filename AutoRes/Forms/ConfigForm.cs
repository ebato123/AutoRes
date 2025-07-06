using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AutoRes.Forms
{
    public partial class ConfigForm : Form
    {
        public string _path = String.Empty;
        public string _program = String.Empty;
        public string _resolution = String.Empty;
        public string _scale = String.Empty;
        public List<Configuration> _configurations = new List<Configuration>();

        public ConfigForm(string path)
        {
            InitializeComponent();

            _path = path;
            _program = Path.GetFileNameWithoutExtension(_path);
            _configurations = ConfigurationService.Load();
            this.Text = $"Configuración de {_program}";
            this.cbResolution.Text = "1920x1080"; // Default resolution
            this.cbScale.Text = "100%"; // Default scale
        }

        private void keyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                _resolution = cbResolution.Text;
                _scale = cbScale.Text;

                if (string.IsNullOrWhiteSpace(_resolution) || string.IsNullOrWhiteSpace(_scale))
                {
                    MessageBox.Show("Por favor, complete todos los campos antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Configuration conf = _configurations.Find(c => c.Name.Equals(_program, StringComparison.OrdinalIgnoreCase));
                if (conf != null)
                {
                    if (MessageBox.Show($"Ya existe una configuración para {conf.Name} ({conf.Resolution}).\n\n¿Desea sobreescribir esta configuración?","ADVERTENCIA",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                    { 
                        conf.Path = _path;
                        conf.Resolution = _resolution;
                        ConfigurationService.Update(conf.Id, conf);
                    }

                    Close();
                }
                else
                {
                    _configurations.Add(new Configuration
                    {
                        Name = _program,
                        Path = _path,
                        Resolution = _resolution,
                    });
                    ConfigurationService.Save(_configurations); 
                }

                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
