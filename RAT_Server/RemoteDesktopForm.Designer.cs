namespace RAT_Server
{
    partial class RemoteDesktopForm
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
            this.pictureBoxRemoteScreen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRemoteScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxRemoteScreen
            // 
            this.pictureBoxRemoteScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxRemoteScreen.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxRemoteScreen.Name = "pictureBoxRemoteScreen";
            this.pictureBoxRemoteScreen.Size = new System.Drawing.Size(800, 450);
            this.pictureBoxRemoteScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxRemoteScreen.TabIndex = 0;
            this.pictureBoxRemoteScreen.TabStop = false;
            // 
            // RemoteDesktopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBoxRemoteScreen);
            this.Name = "RemoteDesktopForm";
            this.Text = "Escritorio Remoto - RAT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoteDesktopForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRemoteScreen)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.PictureBox pictureBoxRemoteScreen;
    }
}
