using System;
using System.Windows.Forms;

namespace RAT_Server
{
    public partial class SettingsForm : Form
    {
        public int NewPort { get; private set; }

        public SettingsForm(int currentPort)
        {
            InitializeComponent();
            textBoxPort.Text = currentPort.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxPort.Text.Trim(), out int port) && port > 0 && port <= 65535)
            {
                NewPort = port;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Por favor, introduce un puerto válido entre 1 y 65535.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
