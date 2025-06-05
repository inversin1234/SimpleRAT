namespace RAT_Server
{
    partial class TaskManagerForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.listViewProcesses = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCPU = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.menuKillProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStartProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();
            // 
            // listViewProcesses
            // 
            this.listViewProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderID,
            this.columnHeaderCPU});
            this.listViewProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewProcesses.FullRowSelect = true;
            this.listViewProcesses.GridLines = true;
            this.listViewProcesses.HideSelection = false;
            this.listViewProcesses.Location = new System.Drawing.Point(0, 0);
            this.listViewProcesses.Name = "listViewProcesses";
            this.listViewProcesses.Size = new System.Drawing.Size(800, 450);
            this.listViewProcesses.TabIndex = 0;
            this.listViewProcesses.UseCompatibleStateImageBehavior = false;
            this.listViewProcesses.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Nombre";
            this.columnHeaderName.Width = 400;
            // 
            // columnHeaderID
            // 
            this.columnHeaderID.Text = "ID";
            this.columnHeaderID.Width = 100;
            // 
            // columnHeaderCPU
            // 
            this.columnHeaderCPU.Text = "Uso de CPU";
            this.columnHeaderCPU.Width = 150;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuKillProcess,
            this.menuStartProcess});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(181, 70);
            // 
            // menuKillProcess
            // 
            this.menuKillProcess.Name = "menuKillProcess";
            this.menuKillProcess.Size = new System.Drawing.Size(180, 22);
            this.menuKillProcess.Text = "Finalizar Proceso";
            this.menuKillProcess.Click += new System.EventHandler(this.menuKillProcess_Click);
            // 
            // menuStartProcess
            // 
            this.menuStartProcess.Name = "menuStartProcess";
            this.menuStartProcess.Size = new System.Drawing.Size(180, 22);
            this.menuStartProcess.Text = "Iniciar Proceso";
            this.menuStartProcess.Click += new System.EventHandler(this.menuStartProcess_Click);
            // 
            // TaskManagerForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.listViewProcesses);
            this.Name = "TaskManagerForm";
            this.Text = "Administrador de Tareas - RAT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskManagerForm_FormClosing);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ListView listViewProcesses;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderID;
        private System.Windows.Forms.ColumnHeader columnHeaderCPU;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuKillProcess;
        private System.Windows.Forms.ToolStripMenuItem menuStartProcess;
    }
}
