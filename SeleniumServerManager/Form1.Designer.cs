
namespace SeleniumServerManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.driverUpdateButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // driverUpdateButton
            // 
            this.driverUpdateButton.Location = new System.Drawing.Point(75, 282);
            this.driverUpdateButton.Name = "driverUpdateButton";
            this.driverUpdateButton.Size = new System.Drawing.Size(148, 23);
            this.driverUpdateButton.TabIndex = 0;
            this.driverUpdateButton.Text = "Update Driver";
            this.driverUpdateButton.UseVisualStyleBackColor = true;
            this.driverUpdateButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 74);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Manual Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(207, 74);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 450);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.driverUpdateButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Selenium Server Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button driverUpdateButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
    }
}

