﻿namespace VitNX2.UI.ControlsV2
{
    partial class VitNX2_MessageBox1_Form
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
            this.windowTitle = new System.Windows.Forms.Panel();
            this.labelCaption = new System.Windows.Forms.Label();
            this.titleExit = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelBody = new System.Windows.Forms.Panel();
            this.labelMessage = new System.Windows.Forms.Label();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.windowTitle.SuspendLayout();
            this.panelBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // windowTitle
            // 
            this.windowTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.windowTitle.Controls.Add(this.labelCaption);
            this.windowTitle.Controls.Add(this.titleExit);
            this.windowTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowTitle.Location = new System.Drawing.Point(2, 2);
            this.windowTitle.Name = "windowTitle";
            this.windowTitle.Size = new System.Drawing.Size(346, 30);
            this.windowTitle.TabIndex = 0;
            this.windowTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.windowTitle_MouseDown);
            // 
            // labelCaption
            // 
            this.labelCaption.AutoSize = true;
            this.labelCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelCaption.ForeColor = System.Drawing.Color.White;
            this.labelCaption.Location = new System.Drawing.Point(9, 8);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(81, 17);
            this.labelCaption.TabIndex = 4;
            this.labelCaption.Text = "labelCaption";
            // 
            // titleExit
            // 
            this.titleExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.titleExit.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.titleExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(185)))), ((int)(((byte)(191)))));
            this.titleExit.Location = new System.Drawing.Point(320, 0);
            this.titleExit.Margin = new System.Windows.Forms.Padding(3);
            this.titleExit.Name = "titleExit";
            this.titleExit.Size = new System.Drawing.Size(26, 30);
            this.titleExit.TabIndex = 3;
            this.titleExit.Text = "X";
            this.titleExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.titleExit.Click += new System.EventHandler(this.titleExit_Click);
            this.titleExit.MouseEnter += new System.EventHandler(this.titleExit_MouseEnter);
            this.titleExit.MouseLeave += new System.EventHandler(this.titleExit_MouseLeave);
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(2, 117);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(346, 55);
            this.panelButtons.TabIndex = 1;
            // 
            // panelBody
            // 
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.panelBody.Controls.Add(this.labelMessage);
            this.panelBody.Controls.Add(this.pictureBoxIcon);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(2, 32);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.panelBody.Size = new System.Drawing.Size(346, 85);
            this.panelBody.TabIndex = 2;
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
            this.labelMessage.Location = new System.Drawing.Point(64, 10);
            this.labelMessage.MaximumSize = new System.Drawing.Size(600, 0);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Padding = new System.Windows.Forms.Padding(5, 5, 10, 15);
            this.labelMessage.Size = new System.Drawing.Size(104, 37);
            this.labelMessage.TabIndex = 1;
            this.labelMessage.Text = "labelMessage";
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxIcon.Location = new System.Drawing.Point(10, 10);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(54, 75);
            this.pictureBoxIcon.TabIndex = 0;
            this.pictureBoxIcon.TabStop = false;
            // 
            // VitNX2_MessageBox1_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(350, 174);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.windowTitle);
            this.MinimumSize = new System.Drawing.Size(350, 149);
            this.Name = "VitNX2_MessageBox1_Form";
            this.Opacity = 0.98D;
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.windowTitle.ResumeLayout(false);
            this.windowTitle.PerformLayout();
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel windowTitle;
        private System.Windows.Forms.Panel panelButtons;
        private VitNX2.UI.ControlsV2.VitNX2_Button button3;
        private VitNX2.UI.ControlsV2.VitNX2_Button button2;
        private VitNX2.UI.ControlsV2.VitNX2_Button button1;
        private System.Windows.Forms.Label titleExit;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Label labelCaption;
    }
}
