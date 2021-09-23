using VitNX.Controls;

namespace Example
{
    partial class DialogAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogAbout));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblVersion = new VitNX.Controls.VNXLabel();
            this.VNXLabel2 = new VitNX.Controls.VNXLabel();
            this.VNXLabel1 = new VitNX.Controls.VNXLabel();
            this.lblHeader = new VitNX.Controls.VNXLabel();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblVersion);
            this.pnlMain.Controls.Add(this.VNXLabel2);
            this.pnlMain.Controls.Add(this.VNXLabel1);
            this.pnlMain.Controls.Add(this.lblHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15, 15, 15, 5);
            this.pnlMain.Size = new System.Drawing.Size(343, 229);
            this.pnlMain.TabIndex = 2;
            // 
            // lblVersion
            // 
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblVersion.Location = new System.Drawing.Point(15, 168);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(313, 47);
            this.lblVersion.TabIndex = 7;
            this.lblVersion.Text = "Version: [version]";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // VNXLabel2
            // 
            this.VNXLabel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXLabel2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VNXLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.VNXLabel2.Location = new System.Drawing.Point(15, 89);
            this.VNXLabel2.Name = "VNXLabel2";
            this.VNXLabel2.Size = new System.Drawing.Size(313, 79);
            this.VNXLabel2.TabIndex = 5;
            this.VNXLabel2.Text = "Created because of all the little annoyances and issues with the default .NET con" +
    "trol library.";
            this.VNXLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VNXLabel1
            // 
            this.VNXLabel1.AutoSize = true;
            this.VNXLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VNXLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.VNXLabel1.Location = new System.Drawing.Point(15, 68);
            this.VNXLabel1.Name = "VNXLabel1";
            this.VNXLabel1.Size = new System.Drawing.Size(322, 21);
            this.VNXLabel1.TabIndex = 4;
            this.VNXLabel1.Text = "Controls for WinForms (.NET Framework 4.5)";
            this.VNXLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblHeader.Location = new System.Drawing.Point(15, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(313, 53);
            this.lblHeader.TabIndex = 3;
            this.lblHeader.Text = "VitNX";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DialogAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 274);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About VitNX";
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private VNXLabel lblHeader;
        private VNXLabel VNXLabel1;
        private VNXLabel VNXLabel2;
        private VNXLabel lblVersion;
    }
}