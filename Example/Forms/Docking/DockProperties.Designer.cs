using VitNX.Config;
using VitNX.Docking;

namespace Example
{
    partial class DockProperties
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.VNXRadioButton3 = new VitNX.Controls.VNXRadioButton();
            this.VNXRadioButton2 = new VitNX.Controls.VNXRadioButton();
            this.VNXRadioButton1 = new VitNX.Controls.VNXRadioButton();
            this.VNXTitle1 = new VitNX.Controls.VNXTitle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.VNXCheckBox3 = new VitNX.Controls.VNXCheckBox();
            this.VNXCheckBox2 = new VitNX.Controls.VNXCheckBox();
            this.VNXCheckBox1 = new VitNX.Controls.VNXCheckBox();
            this.VNXTitle2 = new VitNX.Controls.VNXTitle();
            this.VNXScrollBar1 = new VitNX.Controls.VNXScrollBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.VNXTitle3 = new VitNX.Controls.VNXTitle();
            this.cmbList = new VitNX.Controls.VNXDropdownList();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.panel3);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.panel2);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 25);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(10, 10, 5, 10);
            this.pnlMain.Size = new System.Drawing.Size(265, 425);
            this.pnlMain.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.VNXRadioButton3);
            this.panel1.Controls.Add(this.VNXRadioButton2);
            this.panel1.Controls.Add(this.VNXRadioButton1);
            this.panel1.Controls.Add(this.VNXTitle1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 103);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel1.Size = new System.Drawing.Size(250, 93);
            this.panel1.TabIndex = 2;
            // 
            // VNXRadioButton3
            // 
            this.VNXRadioButton3.AutoSize = true;
            this.VNXRadioButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXRadioButton3.Enabled = false;
            this.VNXRadioButton3.Location = new System.Drawing.Point(0, 64);
            this.VNXRadioButton3.Name = "VNXRadioButton3";
            this.VNXRadioButton3.Size = new System.Drawing.Size(250, 19);
            this.VNXRadioButton3.TabIndex = 6;
            this.VNXRadioButton3.TabStop = true;
            this.VNXRadioButton3.Text = "Disabled radiobutton";
            // 
            // VNXRadioButton2
            // 
            this.VNXRadioButton2.AutoSize = true;
            this.VNXRadioButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXRadioButton2.Location = new System.Drawing.Point(0, 45);
            this.VNXRadioButton2.Name = "VNXRadioButton2";
            this.VNXRadioButton2.Size = new System.Drawing.Size(250, 19);
            this.VNXRadioButton2.TabIndex = 5;
            this.VNXRadioButton2.TabStop = true;
            this.VNXRadioButton2.Text = "Radiobutton";
            // 
            // VNXRadioButton1
            // 
            this.VNXRadioButton1.AutoSize = true;
            this.VNXRadioButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXRadioButton1.Location = new System.Drawing.Point(0, 26);
            this.VNXRadioButton1.Name = "VNXRadioButton1";
            this.VNXRadioButton1.Size = new System.Drawing.Size(250, 19);
            this.VNXRadioButton1.TabIndex = 4;
            this.VNXRadioButton1.TabStop = true;
            this.VNXRadioButton1.Text = "Radiobutton";
            // 
            // VNXTitle1
            // 
            this.VNXTitle1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXTitle1.Location = new System.Drawing.Point(0, 0);
            this.VNXTitle1.Name = "VNXTitle1";
            this.VNXTitle1.Size = new System.Drawing.Size(250, 26);
            this.VNXTitle1.TabIndex = 7;
            this.VNXTitle1.Text = "Radio buttons";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.VNXCheckBox3);
            this.panel2.Controls.Add(this.VNXCheckBox2);
            this.panel2.Controls.Add(this.VNXCheckBox1);
            this.panel2.Controls.Add(this.VNXTitle2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel2.Size = new System.Drawing.Size(250, 93);
            this.panel2.TabIndex = 1;
            // 
            // VNXCheckBox3
            // 
            this.VNXCheckBox3.AutoSize = true;
            this.VNXCheckBox3.Checked = true;
            this.VNXCheckBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.VNXCheckBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXCheckBox3.Enabled = false;
            this.VNXCheckBox3.Location = new System.Drawing.Point(0, 64);
            this.VNXCheckBox3.Name = "VNXCheckBox3";
            this.VNXCheckBox3.Size = new System.Drawing.Size(250, 19);
            this.VNXCheckBox3.TabIndex = 6;
            this.VNXCheckBox3.Text = "Disabled checked checkbox";
            // 
            // VNXCheckBox2
            // 
            this.VNXCheckBox2.AutoSize = true;
            this.VNXCheckBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXCheckBox2.Enabled = false;
            this.VNXCheckBox2.Location = new System.Drawing.Point(0, 45);
            this.VNXCheckBox2.Name = "VNXCheckBox2";
            this.VNXCheckBox2.Size = new System.Drawing.Size(250, 19);
            this.VNXCheckBox2.TabIndex = 5;
            this.VNXCheckBox2.Text = "Disabled checkbox";
            // 
            // VNXCheckBox1
            // 
            this.VNXCheckBox1.AutoSize = true;
            this.VNXCheckBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXCheckBox1.Location = new System.Drawing.Point(0, 26);
            this.VNXCheckBox1.Name = "VNXCheckBox1";
            this.VNXCheckBox1.Size = new System.Drawing.Size(250, 19);
            this.VNXCheckBox1.TabIndex = 4;
            this.VNXCheckBox1.Text = "Checkbox";
            // 
            // VNXTitle2
            // 
            this.VNXTitle2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXTitle2.Location = new System.Drawing.Point(0, 0);
            this.VNXTitle2.Name = "VNXTitle2";
            this.VNXTitle2.Size = new System.Drawing.Size(250, 26);
            this.VNXTitle2.TabIndex = 8;
            this.VNXTitle2.Text = "Check boxes";
            // 
            // VNXScrollBar1
            // 
            this.VNXScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.VNXScrollBar1.Enabled = false;
            this.VNXScrollBar1.Location = new System.Drawing.Point(265, 25);
            this.VNXScrollBar1.Maximum = 5;
            this.VNXScrollBar1.Minimum = 1;
            this.VNXScrollBar1.Name = "VNXScrollBar1";
            this.VNXScrollBar1.Size = new System.Drawing.Size(15, 425);
            this.VNXScrollBar1.TabIndex = 1;
            this.VNXScrollBar1.Text = "VNXScrollBar1";
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.cmbList);
            this.panel3.Controls.Add(this.VNXTitle3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 196);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel3.Size = new System.Drawing.Size(250, 62);
            this.panel3.TabIndex = 3;
            // 
            // VNXTitle3
            // 
            this.VNXTitle3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXTitle3.Location = new System.Drawing.Point(0, 0);
            this.VNXTitle3.Name = "VNXTitle3";
            this.VNXTitle3.Size = new System.Drawing.Size(250, 26);
            this.VNXTitle3.TabIndex = 7;
            this.VNXTitle3.Text = "Lists";
            // 
            // cmbList
            // 
            this.cmbList.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbList.Location = new System.Drawing.Point(0, 26);
            this.cmbList.Name = "cmbList";
            this.cmbList.Size = new System.Drawing.Size(250, 26);
            this.cmbList.TabIndex = 8;
            this.cmbList.Text = "VNXDropdownList1";
            // 
            // DockProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.VNXScrollBar1);
            this.DefaultDockArea = VitNX.Docking.VNXDockArea.Right;
            this.DockText = "Properties";
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::Example.Icons.properties_16xLG;
            this.Name = "DockProperties";
            this.SerializationKey = "DockProperties";
            this.Size = new System.Drawing.Size(280, 450);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel panel1;
        private VitNX.Controls.VNXRadioButton VNXRadioButton3;
        private VitNX.Controls.VNXRadioButton VNXRadioButton2;
        private VitNX.Controls.VNXRadioButton VNXRadioButton1;
        private VitNX.Controls.VNXTitle VNXTitle1;
        private System.Windows.Forms.Panel panel2;
        private VitNX.Controls.VNXCheckBox VNXCheckBox3;
        private VitNX.Controls.VNXCheckBox VNXCheckBox2;
        private VitNX.Controls.VNXCheckBox VNXCheckBox1;
        private VitNX.Controls.VNXTitle VNXTitle2;
        private VitNX.Controls.VNXScrollBar VNXScrollBar1;
        private System.Windows.Forms.Panel panel3;
        private VitNX.Controls.VNXTitle VNXTitle3;
        private VitNX.Controls.VNXDropdownList cmbList;
    }
}
