using VitNX.Controls;

namespace Example
{
    partial class DialogControls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogControls));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTreeView = new VitNX.Controls.VNXSectionPanel();
            this.treeTest = new VitNX.Controls.VNXTreeView();
            this.pnlListView = new VitNX.Controls.VNXSectionPanel();
            this.lstTest = new VitNX.Controls.VNXListView();
            this.pnlMessageBox = new VitNX.Controls.VNXSectionPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.VNXComboBox1 = new VitNX.Controls.VNXComboBox();
            this.VNXTitle4 = new VitNX.Controls.VNXTitle();
            this.panel6 = new System.Windows.Forms.Panel();
            this.VNXNumericUpDown1 = new VitNX.Controls.VNXNumericUpDown();
            this.VNXTitle5 = new VitNX.Controls.VNXTitle();
            this.panel5 = new System.Windows.Forms.Panel();
            this.VNXRadioButton3 = new VitNX.Controls.VNXRadioButton();
            this.VNXRadioButton2 = new VitNX.Controls.VNXRadioButton();
            this.VNXRadioButton1 = new VitNX.Controls.VNXRadioButton();
            this.VNXTitle3 = new VitNX.Controls.VNXTitle();
            this.panel4 = new System.Windows.Forms.Panel();
            this.VNXCheckBox2 = new VitNX.Controls.VNXCheckBox();
            this.VNXCheckBox1 = new VitNX.Controls.VNXCheckBox();
            this.VNXTitle2 = new VitNX.Controls.VNXTitle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnMessageBox = new VitNX.Controls.VNXButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDialog = new VitNX.Controls.VNXButton();
            this.VNXTitle1 = new VitNX.Controls.VNXTitle();
            this.VNXGroupBox1 = new VitNX.Controls.VNXGroupBox();
            this.VNXCheckBox3 = new VitNX.Controls.VNXCheckBox();
            this.VNXRadioButton4 = new VitNX.Controls.VNXRadioButton();
            this.pnlMain.SuspendLayout();
            this.tblMain.SuspendLayout();
            this.pnlTreeView.SuspendLayout();
            this.pnlListView.SuspendLayout();
            this.pnlMessageBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VNXNumericUpDown1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.VNXGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tblMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(5, 10, 5, 0);
            this.pnlMain.Size = new System.Drawing.Size(708, 528);
            this.pnlMain.TabIndex = 2;
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 3;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblMain.Controls.Add(this.pnlTreeView, 0, 0);
            this.tblMain.Controls.Add(this.pnlListView, 0, 0);
            this.tblMain.Controls.Add(this.pnlMessageBox, 0, 0);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(5, 10);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 1;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Size = new System.Drawing.Size(698, 518);
            this.tblMain.TabIndex = 0;
            // 
            // pnlTreeView
            // 
            this.pnlTreeView.Controls.Add(this.treeTest);
            this.pnlTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTreeView.Location = new System.Drawing.Point(469, 0);
            this.pnlTreeView.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.pnlTreeView.Name = "pnlTreeView";
            this.pnlTreeView.SectionHeader = "Tree view test";
            this.pnlTreeView.Size = new System.Drawing.Size(224, 518);
            this.pnlTreeView.TabIndex = 14;
            // 
            // treeTest
            // 
            this.treeTest.AllowMoveNodes = true;
            this.treeTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeTest.Location = new System.Drawing.Point(1, 25);
            this.treeTest.MaxDragChange = 20;
            this.treeTest.MultiSelect = true;
            this.treeTest.Name = "treeTest";
            this.treeTest.ShowIcons = true;
            this.treeTest.Size = new System.Drawing.Size(222, 492);
            this.treeTest.TabIndex = 0;
            this.treeTest.Text = "VNXTreeView1";
            // 
            // pnlListView
            // 
            this.pnlListView.Controls.Add(this.lstTest);
            this.pnlListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlListView.Location = new System.Drawing.Point(237, 0);
            this.pnlListView.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.pnlListView.Name = "pnlListView";
            this.pnlListView.SectionHeader = "List view test";
            this.pnlListView.Size = new System.Drawing.Size(222, 518);
            this.pnlListView.TabIndex = 13;
            // 
            // lstTest
            // 
            this.lstTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTest.Location = new System.Drawing.Point(1, 25);
            this.lstTest.MultiSelect = true;
            this.lstTest.Name = "lstTest";
            this.lstTest.Size = new System.Drawing.Size(220, 492);
            this.lstTest.TabIndex = 7;
            this.lstTest.Text = "VNXListView1";
            // 
            // pnlMessageBox
            // 
            this.pnlMessageBox.Controls.Add(this.panel1);
            this.pnlMessageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMessageBox.Location = new System.Drawing.Point(5, 0);
            this.pnlMessageBox.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.pnlMessageBox.Name = "pnlMessageBox";
            this.pnlMessageBox.SectionHeader = "Controls test";
            this.pnlMessageBox.Size = new System.Drawing.Size(222, 518);
            this.pnlMessageBox.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.VNXGroupBox1);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 25);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(220, 492);
            this.panel1.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.VNXComboBox1);
            this.panel7.Controls.Add(this.VNXTitle4);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(10, 349);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(200, 63);
            this.panel7.TabIndex = 23;
            // 
            // VNXComboBox1
            // 
            this.VNXComboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.VNXComboBox1.FormattingEnabled = true;
            this.VNXComboBox1.Items.AddRange(new object[] {
            "Item 1",
            "Item 2",
            "This is a really long item in the collection to check out how text is clipped",
            "Item 4"});
            this.VNXComboBox1.Location = new System.Drawing.Point(0, 26);
            this.VNXComboBox1.Name = "VNXComboBox1";
            this.VNXComboBox1.Size = new System.Drawing.Size(200, 24);
            this.VNXComboBox1.TabIndex = 20;
            // 
            // VNXTitle4
            // 
            this.VNXTitle4.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXTitle4.Location = new System.Drawing.Point(0, 0);
            this.VNXTitle4.Name = "VNXTitle4";
            this.VNXTitle4.Size = new System.Drawing.Size(200, 26);
            this.VNXTitle4.TabIndex = 21;
            this.VNXTitle4.Text = "ComboBox";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.VNXNumericUpDown1);
            this.panel6.Controls.Add(this.VNXTitle5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(10, 285);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 64);
            this.panel6.TabIndex = 22;
            // 
            // VNXNumericUpDown1
            // 
            this.VNXNumericUpDown1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXNumericUpDown1.Location = new System.Drawing.Point(0, 26);
            this.VNXNumericUpDown1.Name = "VNXNumericUpDown1";
            this.VNXNumericUpDown1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VNXNumericUpDown1.Size = new System.Drawing.Size(200, 23);
            this.VNXNumericUpDown1.TabIndex = 24;
            // 
            // VNXTitle5
            // 
            this.VNXTitle5.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXTitle5.Location = new System.Drawing.Point(0, 0);
            this.VNXTitle5.Name = "VNXTitle5";
            this.VNXTitle5.Size = new System.Drawing.Size(200, 26);
            this.VNXTitle5.TabIndex = 23;
            this.VNXTitle5.Text = "Numeric Up/Down";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.VNXRadioButton3);
            this.panel5.Controls.Add(this.VNXRadioButton2);
            this.panel5.Controls.Add(this.VNXRadioButton1);
            this.panel5.Controls.Add(this.VNXTitle3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(10, 185);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 100);
            this.panel5.TabIndex = 12;
            // 
            // VNXRadioButton3
            // 
            this.VNXRadioButton3.AutoSize = true;
            this.VNXRadioButton3.Checked = true;
            this.VNXRadioButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXRadioButton3.Enabled = false;
            this.VNXRadioButton3.Location = new System.Drawing.Point(0, 64);
            this.VNXRadioButton3.Name = "VNXRadioButton3";
            this.VNXRadioButton3.Size = new System.Drawing.Size(200, 19);
            this.VNXRadioButton3.TabIndex = 4;
            this.VNXRadioButton3.TabStop = true;
            this.VNXRadioButton3.Text = "Disabled radio button";
            // 
            // VNXRadioButton2
            // 
            this.VNXRadioButton2.AutoSize = true;
            this.VNXRadioButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXRadioButton2.Location = new System.Drawing.Point(0, 45);
            this.VNXRadioButton2.Name = "VNXRadioButton2";
            this.VNXRadioButton2.Size = new System.Drawing.Size(200, 19);
            this.VNXRadioButton2.TabIndex = 3;
            this.VNXRadioButton2.Text = "Radio button";
            // 
            // VNXRadioButton1
            // 
            this.VNXRadioButton1.AutoSize = true;
            this.VNXRadioButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXRadioButton1.Location = new System.Drawing.Point(0, 26);
            this.VNXRadioButton1.Name = "VNXRadioButton1";
            this.VNXRadioButton1.Size = new System.Drawing.Size(200, 19);
            this.VNXRadioButton1.TabIndex = 2;
            this.VNXRadioButton1.Text = "Radio button";
            // 
            // VNXTitle3
            // 
            this.VNXTitle3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXTitle3.Location = new System.Drawing.Point(0, 0);
            this.VNXTitle3.Name = "VNXTitle3";
            this.VNXTitle3.Size = new System.Drawing.Size(200, 26);
            this.VNXTitle3.TabIndex = 16;
            this.VNXTitle3.Text = "Radio buttons";
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.VNXCheckBox2);
            this.panel4.Controls.Add(this.VNXCheckBox1);
            this.panel4.Controls.Add(this.VNXTitle2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(10, 111);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel4.Size = new System.Drawing.Size(200, 74);
            this.panel4.TabIndex = 11;
            // 
            // VNXCheckBox2
            // 
            this.VNXCheckBox2.AutoSize = true;
            this.VNXCheckBox2.Checked = true;
            this.VNXCheckBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.VNXCheckBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXCheckBox2.Enabled = false;
            this.VNXCheckBox2.Location = new System.Drawing.Point(0, 45);
            this.VNXCheckBox2.Name = "VNXCheckBox2";
            this.VNXCheckBox2.Size = new System.Drawing.Size(200, 19);
            this.VNXCheckBox2.TabIndex = 13;
            this.VNXCheckBox2.Text = "Disabled checkbox";
            // 
            // VNXCheckBox1
            // 
            this.VNXCheckBox1.AutoSize = true;
            this.VNXCheckBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXCheckBox1.Location = new System.Drawing.Point(0, 26);
            this.VNXCheckBox1.Name = "VNXCheckBox1";
            this.VNXCheckBox1.Size = new System.Drawing.Size(200, 19);
            this.VNXCheckBox1.TabIndex = 12;
            this.VNXCheckBox1.Text = "Enabled checkbox";
            // 
            // VNXTitle2
            // 
            this.VNXTitle2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXTitle2.Location = new System.Drawing.Point(0, 0);
            this.VNXTitle2.Name = "VNXTitle2";
            this.VNXTitle2.Size = new System.Drawing.Size(200, 26);
            this.VNXTitle2.TabIndex = 15;
            this.VNXTitle2.Text = "Check boxes";
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.btnMessageBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 71);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel3.Size = new System.Drawing.Size(200, 40);
            this.panel3.TabIndex = 10;
            // 
            // btnMessageBox
            // 
            this.btnMessageBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMessageBox.Location = new System.Drawing.Point(0, 0);
            this.btnMessageBox.Name = "btnMessageBox";
            this.btnMessageBox.Padding = new System.Windows.Forms.Padding(5);
            this.btnMessageBox.Size = new System.Drawing.Size(200, 30);
            this.btnMessageBox.TabIndex = 12;
            this.btnMessageBox.Text = "Message Box";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.btnDialog);
            this.panel2.Controls.Add(this.VNXTitle1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panel2.Size = new System.Drawing.Size(200, 61);
            this.panel2.TabIndex = 5;
            // 
            // btnDialog
            // 
            this.btnDialog.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDialog.Location = new System.Drawing.Point(0, 26);
            this.btnDialog.Name = "btnDialog";
            this.btnDialog.Padding = new System.Windows.Forms.Padding(5);
            this.btnDialog.Size = new System.Drawing.Size(200, 30);
            this.btnDialog.TabIndex = 4;
            this.btnDialog.Text = "Dialog";
            // 
            // VNXTitle1
            // 
            this.VNXTitle1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXTitle1.Location = new System.Drawing.Point(0, 0);
            this.VNXTitle1.Name = "VNXTitle1";
            this.VNXTitle1.Size = new System.Drawing.Size(200, 26);
            this.VNXTitle1.TabIndex = 14;
            this.VNXTitle1.Text = "Dialogs";
            // 
            // VNXGroupBox1
            // 
            this.VNXGroupBox1.AutoSize = true;
            this.VNXGroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.VNXGroupBox1.Controls.Add(this.VNXRadioButton4);
            this.VNXGroupBox1.Controls.Add(this.VNXCheckBox3);
            this.VNXGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXGroupBox1.Location = new System.Drawing.Point(10, 412);
            this.VNXGroupBox1.Name = "VNXGroupBox1";
            this.VNXGroupBox1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.VNXGroupBox1.Size = new System.Drawing.Size(200, 69);
            this.VNXGroupBox1.TabIndex = 24;
            this.VNXGroupBox1.TabStop = false;
            this.VNXGroupBox1.Text = "GroupBox";
            // 
            // VNXCheckBox3
            // 
            this.VNXCheckBox3.AutoSize = true;
            this.VNXCheckBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXCheckBox3.Location = new System.Drawing.Point(10, 21);
            this.VNXCheckBox3.Name = "VNXCheckBox3";
            this.VNXCheckBox3.Size = new System.Drawing.Size(180, 19);
            this.VNXCheckBox3.TabIndex = 0;
            this.VNXCheckBox3.Text = "Checkbox";
            // 
            // VNXRadioButton4
            // 
            this.VNXRadioButton4.AutoSize = true;
            this.VNXRadioButton4.Dock = System.Windows.Forms.DockStyle.Top;
            this.VNXRadioButton4.Location = new System.Drawing.Point(10, 40);
            this.VNXRadioButton4.Name = "VNXRadioButton4";
            this.VNXRadioButton4.Size = new System.Drawing.Size(180, 19);
            this.VNXRadioButton4.TabIndex = 1;
            this.VNXRadioButton4.TabStop = true;
            this.VNXRadioButton4.Text = "Radio button";
            // 
            // DialogControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 573);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DialogControls";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controls";
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.ResumeLayout(false);
            this.tblMain.ResumeLayout(false);
            this.pnlTreeView.ResumeLayout(false);
            this.pnlListView.ResumeLayout(false);
            this.pnlMessageBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VNXNumericUpDown1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.VNXGroupBox1.ResumeLayout(false);
            this.VNXGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private VNXSectionPanel pnlTreeView;
        private VNXTreeView treeTest;
        private VNXSectionPanel pnlListView;
        private VNXListView lstTest;
        private VNXSectionPanel pnlMessageBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private VNXButton btnDialog;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private VNXButton btnMessageBox;
        private VNXCheckBox VNXCheckBox2;
        private VNXCheckBox VNXCheckBox1;
        private System.Windows.Forms.Panel panel5;
        private VNXRadioButton VNXRadioButton2;
        private VNXRadioButton VNXRadioButton1;
        private VNXRadioButton VNXRadioButton3;
        private VNXTitle VNXTitle1;
        private VNXTitle VNXTitle2;
        private VNXTitle VNXTitle3;
        private System.Windows.Forms.Panel panel7;
        private VNXComboBox VNXComboBox1;
        private VNXTitle VNXTitle4;
        private System.Windows.Forms.Panel panel6;
        private VNXNumericUpDown VNXNumericUpDown1;
        private VNXTitle VNXTitle5;
        private VNXGroupBox VNXGroupBox1;
        private VNXRadioButton VNXRadioButton4;
        private VNXCheckBox VNXCheckBox3;
    }
}