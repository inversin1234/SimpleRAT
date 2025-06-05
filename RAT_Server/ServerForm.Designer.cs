namespace RAT_Server
{
    partial class ServerForm
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
            this.listViewClients = new System.Windows.Forms.ListView();
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.buttonListen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewClients
            // 
            this.listViewClients.HideSelection = false;
            this.listViewClients.Location = new System.Drawing.Point(9, 10);
            this.listViewClients.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listViewClients.Name = "listViewClients";
            this.listViewClients.Size = new System.Drawing.Size(641, 347);
            this.listViewClients.TabIndex = 0;
            this.listViewClients.UseCompatibleStateImageBehavior = false;
            this.listViewClients.View = System.Windows.Forms.View.List;
            this.listViewClients.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewClients_MouseClick);
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.Location = new System.Drawing.Point(654, 10);
            this.textBoxLogs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ReadOnly = true;
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLogs.Size = new System.Drawing.Size(106, 347);
            this.textBoxLogs.TabIndex = 1;
            // 
            // buttonBuild
            // 
            this.buttonBuild.Location = new System.Drawing.Point(9, 366);
            this.buttonBuild.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(112, 24);
            this.buttonBuild.TabIndex = 2;
            this.buttonBuild.Text = "Builder";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
            // 
            // buttonListen
            // 
            this.buttonListen.Location = new System.Drawing.Point(129, 366);
            this.buttonListen.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonListen.Name = "buttonListen";
            this.buttonListen.Size = new System.Drawing.Size(112, 24);
            this.buttonListen.TabIndex = 3;
            this.buttonListen.Text = "Escucha Avanzada";
            this.buttonListen.UseVisualStyleBackColor = true;
            this.buttonListen.Click += new System.EventHandler(this.buttonListen_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 406);
            this.Controls.Add(this.buttonListen);
            this.Controls.Add(this.buttonBuild);
            this.Controls.Add(this.textBoxLogs);
            this.Controls.Add(this.listViewClients);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ServerForm";
            this.Text = "Team hackers 1.57 - RAT";
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ListView listViewClients;
        private System.Windows.Forms.TextBox textBoxLogs;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.Button buttonListen;
    }
}
