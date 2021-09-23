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
            this.VitNXRadioButton3 = new VitNX.Controls.VitNXRadioButton();
            this.VitNXRadioButton2 = new VitNX.Controls.VitNXRadioButton();
            this.VitNXRadioButton1 = new VitNX.Controls.VitNXRadioButton();
            this.VitNXTitle1 = new VitNX.Controls.VitNXTitle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.VitNXCheckBox3 = new VitNX.Controls.VitNXCheckBox();
            this.VitNXCheckBox2 = new VitNX.Controls.VitNXCheckBox();
            this.VitNXCheckBox1 = new VitNX.Controls.VitNXCheckBox();
            this.VitNXTitle2 = new VitNX.Controls.VitNXTitle();
            this.VitNXScrollBar1 = new VitNX.Controls.VitNXScrollBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.VitNXTitle3 = new VitNX.Controls.VitNXTitle();
            this.cmbList = new VitNX.Controls.VitNXDropdownList();
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
            this.panel1.Controls.Add(this.VitNXRadioButton3);
            this.panel1.Controls.Add(this.VitNXRadioButton2);
            this.panel1.Controls.Add(this.VitNXRadioButton1);
            this.panel1.Controls.Add(this.VitNXTitle1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 103);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel1.Size = new System.Drawing.Size(250, 93);
            this.panel1.TabIndex = 2;
            // 
            // VitNXRadioButton3
            // 
            this.VitNXRadioButton3.AutoSize = true;
            this.VitNXRadioButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXRadioButton3.Enabled = false;
            this.VitNXRadioButton3.Location = new System.Drawing.Point(0, 64);
            this.VitNXRadioButton3.Name = "VitNXRadioButton3";
            this.VitNXRadioButton3.Size = new System.Drawing.Size(250, 19);
            this.VitNXRadioButton3.TabIndex = 6;
            this.VitNXRadioButton3.TabStop = true;
            this.VitNXRadioButton3.Text = "Disabled radiobutton";
            // 
            // VitNXRadioButton2
            // 
            this.VitNXRadioButton2.AutoSize = true;
            this.VitNXRadioButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXRadioButton2.Location = new System.Drawing.Point(0, 45);
            this.VitNXRadioButton2.Name = "VitNXRadioButton2";
            this.VitNXRadioButton2.Size = new System.Drawing.Size(250, 19);
            this.VitNXRadioButton2.TabIndex = 5;
            this.VitNXRadioButton2.TabStop = true;
            this.VitNXRadioButton2.Text = "Radiobutton";
            // 
            // VitNXRadioButton1
            // 
            this.VitNXRadioButton1.AutoSize = true;
            this.VitNXRadioButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXRadioButton1.Location = new System.Drawing.Point(0, 26);
            this.VitNXRadioButton1.Name = "VitNXRadioButton1";
            this.VitNXRadioButton1.Size = new System.Drawing.Size(250, 19);
            this.VitNXRadioButton1.TabIndex = 4;
            this.VitNXRadioButton1.TabStop = true;
            this.VitNXRadioButton1.Text = "Radiobutton";
            // 
            // VitNXTitle1
            // 
            this.VitNXTitle1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXTitle1.Location = new System.Drawing.Point(0, 0);
            this.VitNXTitle1.Name = "VitNXTitle1";
            this.VitNXTitle1.Size = new System.Drawing.Size(250, 26);
            this.VitNXTitle1.TabIndex = 7;
            this.VitNXTitle1.Text = "Radio buttons";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.VitNXCheckBox3);
            this.panel2.Controls.Add(this.VitNXCheckBox2);
            this.panel2.Controls.Add(this.VitNXCheckBox1);
            this.panel2.Controls.Add(this.VitNXTitle2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel2.Size = new System.Drawing.Size(250, 93);
            this.panel2.TabIndex = 1;
            // 
            // VitNXCheckBox3
            // 
            this.VitNXCheckBox3.AutoSize = true;
            this.VitNXCheckBox3.Checked = true;
            this.VitNXCheckBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.VitNXCheckBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXCheckBox3.Enabled = false;
            this.VitNXCheckBox3.Location = new System.Drawing.Point(0, 64);
            this.VitNXCheckBox3.Name = "VitNXCheckBox3";
            this.VitNXCheckBox3.Size = new System.Drawing.Size(250, 19);
            this.VitNXCheckBox3.TabIndex = 6;
            this.VitNXCheckBox3.Text = "Disabled checked checkbox";
            // 
            // VitNXCheckBox2
            // 
            this.VitNXCheckBox2.AutoSize = true;
            this.VitNXCheckBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXCheckBox2.Enabled = false;
            this.VitNXCheckBox2.Location = new System.Drawing.Point(0, 45);
            this.VitNXCheckBox2.Name = "VitNXCheckBox2";
            this.VitNXCheckBox2.Size = new System.Drawing.Size(250, 19);
            this.VitNXCheckBox2.TabIndex = 5;
            this.VitNXCheckBox2.Text = "Disabled checkbox";
            // 
            // VitNXCheckBox1
            // 
            this.VitNXCheckBox1.AutoSize = true;
            this.VitNXCheckBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXCheckBox1.Location = new System.Drawing.Point(0, 26);
            this.VitNXCheckBox1.Name = "VitNXCheckBox1";
            this.VitNXCheckBox1.Size = new System.Drawing.Size(250, 19);
            this.VitNXCheckBox1.TabIndex = 4;
            this.VitNXCheckBox1.Text = "Checkbox";
            // 
            // VitNXTitle2
            // 
            this.VitNXTitle2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXTitle2.Location = new System.Drawing.Point(0, 0);
            this.VitNXTitle2.Name = "VitNXTitle2";
            this.VitNXTitle2.Size = new System.Drawing.Size(250, 26);
            this.VitNXTitle2.TabIndex = 8;
            this.VitNXTitle2.Text = "Check boxes";
            // 
            // VitNXScrollBar1
            // 
            this.VitNXScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.VitNXScrollBar1.Enabled = false;
            this.VitNXScrollBar1.Location = new System.Drawing.Point(265, 25);
            this.VitNXScrollBar1.Maximum = 5;
            this.VitNXScrollBar1.Minimum = 1;
            this.VitNXScrollBar1.Name = "VitNXScrollBar1";
            this.VitNXScrollBar1.Size = new System.Drawing.Size(15, 425);
            this.VitNXScrollBar1.TabIndex = 1;
            this.VitNXScrollBar1.Text = "VitNXScrollBar1";
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.cmbList);
            this.panel3.Controls.Add(this.VitNXTitle3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 196);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel3.Size = new System.Drawing.Size(250, 62);
            this.panel3.TabIndex = 3;
            // 
            // VitNXTitle3
            // 
            this.VitNXTitle3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXTitle3.Location = new System.Drawing.Point(0, 0);
            this.VitNXTitle3.Name = "VitNXTitle3";
            this.VitNXTitle3.Size = new System.Drawing.Size(250, 26);
            this.VitNXTitle3.TabIndex = 7;
            this.VitNXTitle3.Text = "Lists";
            // 
            // cmbList
            // 
            this.cmbList.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbList.Location = new System.Drawing.Point(0, 26);
            this.cmbList.Name = "cmbList";
            this.cmbList.Size = new System.Drawing.Size(250, 26);
            this.cmbList.TabIndex = 8;
            this.cmbList.Text = "VitNXDropdownList1";
            // 
            // DockProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.VitNXScrollBar1);
            this.DefaultDockArea = VitNX.Docking.VitNXDockArea.Right;
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
        private VitNX.Controls.VitNXRadioButton VitNXRadioButton3;
        private VitNX.Controls.VitNXRadioButton VitNXRadioButton2;
        private VitNX.Controls.VitNXRadioButton VitNXRadioButton1;
        private VitNX.Controls.VitNXTitle VitNXTitle1;
        private System.Windows.Forms.Panel panel2;
        private VitNX.Controls.VitNXCheckBox VitNXCheckBox3;
        private VitNX.Controls.VitNXCheckBox VitNXCheckBox2;
        private VitNX.Controls.VitNXCheckBox VitNXCheckBox1;
        private VitNX.Controls.VitNXTitle VitNXTitle2;
        private VitNX.Controls.VitNXScrollBar VitNXScrollBar1;
        private System.Windows.Forms.Panel panel3;
        private VitNX.Controls.VitNXTitle VitNXTitle3;
        private VitNX.Controls.VitNXDropdownList cmbList;
    }
}
