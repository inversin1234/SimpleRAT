namespace RAT_Server
{
    partial class BuilderForm
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
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelIP = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.checkBoxCreateShortcut = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(12, 35);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(200, 20);
            this.textBoxIP.TabIndex = 0;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(12, 85);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(200, 20);
            this.textBoxPort.TabIndex = 1;
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(12, 15);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(17, 13);
            this.labelIP.TabIndex = 2;
            this.labelIP.Text = "IP";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(12, 65);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(38, 13);
            this.labelPort.TabIndex = 3;
            this.labelPort.Text = "Puerto";
            // 
            // buttonBuild
            // 
            this.buttonBuild.Location = new System.Drawing.Point(12, 150);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(200, 30);
            this.buttonBuild.TabIndex = 4;
            this.buttonBuild.Text = "Build";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
            // 
            // checkBoxCreateShortcut
            // 
            this.checkBoxCreateShortcut.AutoSize = true;
            this.checkBoxCreateShortcut.Location = new System.Drawing.Point(12, 120);
            this.checkBoxCreateShortcut.Name = "checkBoxCreateShortcut";
            this.checkBoxCreateShortcut.Size = new System.Drawing.Size(92, 17);
            this.checkBoxCreateShortcut.TabIndex = 5;
            this.checkBoxCreateShortcut.Text = "Iniciar con PC";
            this.checkBoxCreateShortcut.UseVisualStyleBackColor = true;
            // 
            // BuilderForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 200);
            this.Controls.Add(this.checkBoxCreateShortcut);
            this.Controls.Add(this.buttonBuild);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxIP);
            this.Name = "BuilderForm";
            this.Text = "Builder - RAT";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.CheckBox checkBoxCreateShortcut;
    }
}
