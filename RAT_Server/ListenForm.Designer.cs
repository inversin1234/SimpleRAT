namespace RAT_Server
{
    partial class ListenForm
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
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.buttonStartListening = new System.Windows.Forms.Button();
            this.buttonStopListening = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(12, 35);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(200, 22);
            this.textBoxPort.TabIndex = 0;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(12, 15);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(50, 17);
            this.labelPort.TabIndex = 1;
            this.labelPort.Text = "Puerto:";
            // 
            // buttonStartListening
            // 
            this.buttonStartListening.Location = new System.Drawing.Point(12, 75);
            this.buttonStartListening.Name = "buttonStartListening";
            this.buttonStartListening.Size = new System.Drawing.Size(200, 30);
            this.buttonStartListening.TabIndex = 2;
            this.buttonStartListening.Text = "Empezar a Escuchar";
            this.buttonStartListening.UseVisualStyleBackColor = true;
            this.buttonStartListening.Click += new System.EventHandler(this.buttonStartListening_Click);
            // 
            // buttonStopListening
            // 
            this.buttonStopListening.Location = new System.Drawing.Point(12, 115);
            this.buttonStopListening.Name = "buttonStopListening";
            this.buttonStopListening.Size = new System.Drawing.Size(200, 30);
            this.buttonStopListening.TabIndex = 3;
            this.buttonStopListening.Text = "Detener Escucha";
            this.buttonStopListening.UseVisualStyleBackColor = true;
            this.buttonStopListening.Click += new System.EventHandler(this.buttonStopListening_Click);
            // 
            // ListenForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 160);
            this.Controls.Add(this.buttonStopListening);
            this.Controls.Add(this.buttonStartListening);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.textBoxPort);
            this.Name = "ListenForm";
            this.Text = "Escucha Avanzada";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Button buttonStartListening;
        private System.Windows.Forms.Button buttonStopListening;
    }
}
