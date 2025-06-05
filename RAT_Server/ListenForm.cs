using System;
using System.Windows.Forms;

namespace RAT_Server
{
    public partial class ListenForm : Form
    {
        private ServerForm serverForm;

        public ListenForm(ServerForm serverForm)
        {
            InitializeComponent();
            this.serverForm = serverForm;
        }

        private void buttonStartListening_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxPort.Text, out int port) && port >= 1 && port <= 65535)
            {
                serverForm.ChangeListeningPort(port);
                MessageBox.Show($"Servidor ahora está escuchando en el puerto {port}.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor, introduce un puerto válido (entre 1 y 65535).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonStopListening_Click(object sender, EventArgs e)
        {
            serverForm.ChangeListeningPort(0); // Detener el servidor
            MessageBox.Show("El servidor ha dejado de escuchar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
