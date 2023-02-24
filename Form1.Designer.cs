namespace S3DownloadFile
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Downloadbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Downloadbutton
            // 
            this.Downloadbutton.Location = new System.Drawing.Point(344, 197);
            this.Downloadbutton.Name = "Downloadbutton";
            this.Downloadbutton.Size = new System.Drawing.Size(98, 35);
            this.Downloadbutton.TabIndex = 0;
            this.Downloadbutton.Text = "Download";
            this.Downloadbutton.UseVisualStyleBackColor = true;
            this.Downloadbutton.Click += new System.EventHandler(this.Downloadbutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Downloadbutton);
            this.Name = "Form1";
            this.Text = "Download A file from S3";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Downloadbutton;
    }
}

