using System.Windows.Forms;

namespace RAT_Server
{
    partial class FileManagerForm
    {
        private System.ComponentModel.IContainer components = null;
        private ListView listViewFiles;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderType;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuUpload;
        private ToolStripMenuItem menuDelete;
        private TextBox textBoxPath;
        private Button buttonUp;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.listViewFiles = new ListView();
            this.columnHeaderName = new ColumnHeader();
            this.columnHeaderType = new ColumnHeader();
            this.contextMenu = new ContextMenuStrip();
            this.menuUpload = new ToolStripMenuItem();
            this.menuDelete = new ToolStripMenuItem();
            this.textBoxPath = new TextBox();
            this.buttonUp = new Button();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewFiles
            // 
            this.listViewFiles.Columns.AddRange(new ColumnHeader[] {
                this.columnHeaderName,
                this.columnHeaderType});
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.Location = new System.Drawing.Point(12, 41);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(776, 397);
            this.listViewFiles.TabIndex = 0;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = View.Details;
            this.listViewFiles.DoubleClick += new System.EventHandler(this.listViewFiles_DoubleClick);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Nombre";
            this.columnHeaderName.Width = 500;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Tipo";
            this.columnHeaderType.Width = 100;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new ToolStripItem[] {
                this.menuUpload,
                this.menuDelete});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(181, 70);
            // 
            // menuUpload
            // 
            this.menuUpload.Name = "menuUpload";
            this.menuUpload.Size = new System.Drawing.Size(180, 22);
            this.menuUpload.Text = "Subir Archivo";
            this.menuUpload.Click += new System.EventHandler(this.menuUpload_Click);
            // 
            // menuDelete
            // 
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new System.Drawing.Size(180, 22);
            this.menuDelete.Text = "Eliminar";
            this.menuDelete.Click += new System.EventHandler(this.menuDelete_Click);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(12, 12);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(677, 20);
            this.textBoxPath.TabIndex = 1;
            // 
            // buttonUp
            // 
            this.buttonUp.Location = new System.Drawing.Point(695, 10);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(93, 23);
            this.buttonUp.TabIndex = 2;
            this.buttonUp.Text = "Subir";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // FileManagerForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.contextMenu;
            this.Controls.Add(this.buttonUp);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.listViewFiles);
            this.Name = "FileManagerForm";
            this.Text = "Administrador de Archivos - RAT";
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
