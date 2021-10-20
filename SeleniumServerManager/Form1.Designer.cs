
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
            this.currentVersionLabel = new System.Windows.Forms.Label();
            this.latestChromeLabel = new System.Windows.Forms.Label();
            this.seleniumProcessesLabel = new System.Windows.Forms.Label();
            this.refreshUI = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // driverUpdateButton
            // 
            this.driverUpdateButton.Location = new System.Drawing.Point(189, 260);
            this.driverUpdateButton.Name = "driverUpdateButton";
            this.driverUpdateButton.Size = new System.Drawing.Size(93, 23);
            this.driverUpdateButton.TabIndex = 0;
            this.driverUpdateButton.Text = "Update Driver";
            this.driverUpdateButton.UseVisualStyleBackColor = true;
            this.driverUpdateButton.Click += new System.EventHandler(this.driverUpdateButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(207, 56);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(207, 85);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // currentVersionLabel
            // 
            this.currentVersionLabel.AutoSize = true;
            this.currentVersionLabel.Location = new System.Drawing.Point(12, 210);
            this.currentVersionLabel.Name = "currentVersionLabel";
            this.currentVersionLabel.Size = new System.Drawing.Size(121, 13);
            this.currentVersionLabel.TabIndex = 3;
            this.currentVersionLabel.Text = "Current Chrome Version:";
            // 
            // latestChromeLabel
            // 
            this.latestChromeLabel.AutoSize = true;
            this.latestChromeLabel.Location = new System.Drawing.Point(12, 233);
            this.latestChromeLabel.Name = "latestChromeLabel";
            this.latestChromeLabel.Size = new System.Drawing.Size(116, 13);
            this.latestChromeLabel.TabIndex = 4;
            this.latestChromeLabel.Text = "Latest Chrome Version:";
            // 
            // seleniumProcessesLabel
            // 
            this.seleniumProcessesLabel.AutoSize = true;
            this.seleniumProcessesLabel.Location = new System.Drawing.Point(12, 9);
            this.seleniumProcessesLabel.Name = "seleniumProcessesLabel";
            this.seleniumProcessesLabel.Size = new System.Drawing.Size(105, 13);
            this.seleniumProcessesLabel.TabIndex = 5;
            this.seleniumProcessesLabel.Text = "Selenium Processes:";
            // 
            // refreshUI
            // 
            this.refreshUI.Location = new System.Drawing.Point(207, 12);
            this.refreshUI.Name = "refreshUI";
            this.refreshUI.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.refreshUI.Size = new System.Drawing.Size(75, 23);
            this.refreshUI.TabIndex = 6;
            this.refreshUI.Text = "Update UI";
            this.refreshUI.UseVisualStyleBackColor = true;
            this.refreshUI.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 295);
            this.Controls.Add(this.refreshUI);
            this.Controls.Add(this.seleniumProcessesLabel);
            this.Controls.Add(this.latestChromeLabel);
            this.Controls.Add(this.currentVersionLabel);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.driverUpdateButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Selenium Server Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button driverUpdateButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label currentVersionLabel;
        private System.Windows.Forms.Label latestChromeLabel;
        private System.Windows.Forms.Label seleniumProcessesLabel;
        private System.Windows.Forms.Button refreshUI;
    }
}

