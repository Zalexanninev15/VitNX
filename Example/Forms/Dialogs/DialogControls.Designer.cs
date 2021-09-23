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
            this.pnlTreeView = new VitNX.Controls.VitNXSectionPanel();
            this.treeTest = new VitNX.Controls.VitNXTreeView();
            this.pnlListView = new VitNX.Controls.VitNXSectionPanel();
            this.lstTest = new VitNX.Controls.VitNXListView();
            this.pnlMessageBox = new VitNX.Controls.VitNXSectionPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.VitNXComboBox1 = new VitNX.Controls.VitNXComboBox();
            this.VitNXTitle4 = new VitNX.Controls.VitNXTitle();
            this.panel6 = new System.Windows.Forms.Panel();
            this.VitNXNumericUpDown1 = new VitNX.Controls.VitNXNumericUpDown();
            this.VitNXTitle5 = new VitNX.Controls.VitNXTitle();
            this.panel5 = new System.Windows.Forms.Panel();
            this.VitNXRadioButton3 = new VitNX.Controls.VitNXRadioButton();
            this.VitNXRadioButton2 = new VitNX.Controls.VitNXRadioButton();
            this.VitNXRadioButton1 = new VitNX.Controls.VitNXRadioButton();
            this.VitNXTitle3 = new VitNX.Controls.VitNXTitle();
            this.panel4 = new System.Windows.Forms.Panel();
            this.VitNXCheckBox2 = new VitNX.Controls.VitNXCheckBox();
            this.VitNXCheckBox1 = new VitNX.Controls.VitNXCheckBox();
            this.VitNXTitle2 = new VitNX.Controls.VitNXTitle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnMessageBox = new VitNX.Controls.VitNXButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDialog = new VitNX.Controls.VitNXButton();
            this.VitNXTitle1 = new VitNX.Controls.VitNXTitle();
            this.VitNXGroupBox1 = new VitNX.Controls.VitNXGroupBox();
            this.VitNXCheckBox3 = new VitNX.Controls.VitNXCheckBox();
            this.VitNXRadioButton4 = new VitNX.Controls.VitNXRadioButton();
            this.pnlMain.SuspendLayout();
            this.tblMain.SuspendLayout();
            this.pnlTreeView.SuspendLayout();
            this.pnlListView.SuspendLayout();
            this.pnlMessageBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VitNXNumericUpDown1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.VitNXGroupBox1.SuspendLayout();
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
            this.treeTest.Text = "VitNXTreeView1";
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
            this.lstTest.Text = "VitNXListView1";
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
            this.panel1.Controls.Add(this.VitNXGroupBox1);
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
            this.panel7.Controls.Add(this.VitNXComboBox1);
            this.panel7.Controls.Add(this.VitNXTitle4);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(10, 349);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(200, 63);
            this.panel7.TabIndex = 23;
            // 
            // VitNXComboBox1
            // 
            this.VitNXComboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.VitNXComboBox1.FormattingEnabled = true;
            this.VitNXComboBox1.Items.AddRange(new object[] {
            "Item 1",
            "Item 2",
            "This is a really long item in the collection to check out how text is clipped",
            "Item 4"});
            this.VitNXComboBox1.Location = new System.Drawing.Point(0, 26);
            this.VitNXComboBox1.Name = "VitNXComboBox1";
            this.VitNXComboBox1.Size = new System.Drawing.Size(200, 24);
            this.VitNXComboBox1.TabIndex = 20;
            // 
            // VitNXTitle4
            // 
            this.VitNXTitle4.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXTitle4.Location = new System.Drawing.Point(0, 0);
            this.VitNXTitle4.Name = "VitNXTitle4";
            this.VitNXTitle4.Size = new System.Drawing.Size(200, 26);
            this.VitNXTitle4.TabIndex = 21;
            this.VitNXTitle4.Text = "ComboBox";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.VitNXNumericUpDown1);
            this.panel6.Controls.Add(this.VitNXTitle5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(10, 285);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 64);
            this.panel6.TabIndex = 22;
            // 
            // VitNXNumericUpDown1
            // 
            this.VitNXNumericUpDown1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXNumericUpDown1.Location = new System.Drawing.Point(0, 26);
            this.VitNXNumericUpDown1.Name = "VitNXNumericUpDown1";
            this.VitNXNumericUpDown1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VitNXNumericUpDown1.Size = new System.Drawing.Size(200, 23);
            this.VitNXNumericUpDown1.TabIndex = 24;
            // 
            // VitNXTitle5
            // 
            this.VitNXTitle5.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXTitle5.Location = new System.Drawing.Point(0, 0);
            this.VitNXTitle5.Name = "VitNXTitle5";
            this.VitNXTitle5.Size = new System.Drawing.Size(200, 26);
            this.VitNXTitle5.TabIndex = 23;
            this.VitNXTitle5.Text = "Numeric Up/Down";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.VitNXRadioButton3);
            this.panel5.Controls.Add(this.VitNXRadioButton2);
            this.panel5.Controls.Add(this.VitNXRadioButton1);
            this.panel5.Controls.Add(this.VitNXTitle3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(10, 185);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 100);
            this.panel5.TabIndex = 12;
            // 
            // VitNXRadioButton3
            // 
            this.VitNXRadioButton3.AutoSize = true;
            this.VitNXRadioButton3.Checked = true;
            this.VitNXRadioButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXRadioButton3.Enabled = false;
            this.VitNXRadioButton3.Location = new System.Drawing.Point(0, 64);
            this.VitNXRadioButton3.Name = "VitNXRadioButton3";
            this.VitNXRadioButton3.Size = new System.Drawing.Size(200, 19);
            this.VitNXRadioButton3.TabIndex = 4;
            this.VitNXRadioButton3.TabStop = true;
            this.VitNXRadioButton3.Text = "Disabled radio button";
            // 
            // VitNXRadioButton2
            // 
            this.VitNXRadioButton2.AutoSize = true;
            this.VitNXRadioButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXRadioButton2.Location = new System.Drawing.Point(0, 45);
            this.VitNXRadioButton2.Name = "VitNXRadioButton2";
            this.VitNXRadioButton2.Size = new System.Drawing.Size(200, 19);
            this.VitNXRadioButton2.TabIndex = 3;
            this.VitNXRadioButton2.Text = "Radio button";
            // 
            // VitNXRadioButton1
            // 
            this.VitNXRadioButton1.AutoSize = true;
            this.VitNXRadioButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXRadioButton1.Location = new System.Drawing.Point(0, 26);
            this.VitNXRadioButton1.Name = "VitNXRadioButton1";
            this.VitNXRadioButton1.Size = new System.Drawing.Size(200, 19);
            this.VitNXRadioButton1.TabIndex = 2;
            this.VitNXRadioButton1.Text = "Radio button";
            // 
            // VitNXTitle3
            // 
            this.VitNXTitle3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXTitle3.Location = new System.Drawing.Point(0, 0);
            this.VitNXTitle3.Name = "VitNXTitle3";
            this.VitNXTitle3.Size = new System.Drawing.Size(200, 26);
            this.VitNXTitle3.TabIndex = 16;
            this.VitNXTitle3.Text = "Radio buttons";
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.VitNXCheckBox2);
            this.panel4.Controls.Add(this.VitNXCheckBox1);
            this.panel4.Controls.Add(this.VitNXTitle2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(10, 111);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel4.Size = new System.Drawing.Size(200, 74);
            this.panel4.TabIndex = 11;
            // 
            // VitNXCheckBox2
            // 
            this.VitNXCheckBox2.AutoSize = true;
            this.VitNXCheckBox2.Checked = true;
            this.VitNXCheckBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.VitNXCheckBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXCheckBox2.Enabled = false;
            this.VitNXCheckBox2.Location = new System.Drawing.Point(0, 45);
            this.VitNXCheckBox2.Name = "VitNXCheckBox2";
            this.VitNXCheckBox2.Size = new System.Drawing.Size(200, 19);
            this.VitNXCheckBox2.TabIndex = 13;
            this.VitNXCheckBox2.Text = "Disabled checkbox";
            // 
            // VitNXCheckBox1
            // 
            this.VitNXCheckBox1.AutoSize = true;
            this.VitNXCheckBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXCheckBox1.Location = new System.Drawing.Point(0, 26);
            this.VitNXCheckBox1.Name = "VitNXCheckBox1";
            this.VitNXCheckBox1.Size = new System.Drawing.Size(200, 19);
            this.VitNXCheckBox1.TabIndex = 12;
            this.VitNXCheckBox1.Text = "Enabled checkbox";
            // 
            // VitNXTitle2
            // 
            this.VitNXTitle2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXTitle2.Location = new System.Drawing.Point(0, 0);
            this.VitNXTitle2.Name = "VitNXTitle2";
            this.VitNXTitle2.Size = new System.Drawing.Size(200, 26);
            this.VitNXTitle2.TabIndex = 15;
            this.VitNXTitle2.Text = "Check boxes";
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
            this.panel2.Controls.Add(this.VitNXTitle1);
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
            // VitNXTitle1
            // 
            this.VitNXTitle1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXTitle1.Location = new System.Drawing.Point(0, 0);
            this.VitNXTitle1.Name = "VitNXTitle1";
            this.VitNXTitle1.Size = new System.Drawing.Size(200, 26);
            this.VitNXTitle1.TabIndex = 14;
            this.VitNXTitle1.Text = "Dialogs";
            // 
            // VitNXGroupBox1
            // 
            this.VitNXGroupBox1.AutoSize = true;
            this.VitNXGroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.VitNXGroupBox1.Controls.Add(this.VitNXRadioButton4);
            this.VitNXGroupBox1.Controls.Add(this.VitNXCheckBox3);
            this.VitNXGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXGroupBox1.Location = new System.Drawing.Point(10, 412);
            this.VitNXGroupBox1.Name = "VitNXGroupBox1";
            this.VitNXGroupBox1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.VitNXGroupBox1.Size = new System.Drawing.Size(200, 69);
            this.VitNXGroupBox1.TabIndex = 24;
            this.VitNXGroupBox1.TabStop = false;
            this.VitNXGroupBox1.Text = "GroupBox";
            // 
            // VitNXCheckBox3
            // 
            this.VitNXCheckBox3.AutoSize = true;
            this.VitNXCheckBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXCheckBox3.Location = new System.Drawing.Point(10, 21);
            this.VitNXCheckBox3.Name = "VitNXCheckBox3";
            this.VitNXCheckBox3.Size = new System.Drawing.Size(180, 19);
            this.VitNXCheckBox3.TabIndex = 0;
            this.VitNXCheckBox3.Text = "Checkbox";
            // 
            // VitNXRadioButton4
            // 
            this.VitNXRadioButton4.AutoSize = true;
            this.VitNXRadioButton4.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNXRadioButton4.Location = new System.Drawing.Point(10, 40);
            this.VitNXRadioButton4.Name = "VitNXRadioButton4";
            this.VitNXRadioButton4.Size = new System.Drawing.Size(180, 19);
            this.VitNXRadioButton4.TabIndex = 1;
            this.VitNXRadioButton4.TabStop = true;
            this.VitNXRadioButton4.Text = "Radio button";
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
            ((System.ComponentModel.ISupportInitialize)(this.VitNXNumericUpDown1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.VitNXGroupBox1.ResumeLayout(false);
            this.VitNXGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private VitNXSectionPanel pnlTreeView;
        private VitNXTreeView treeTest;
        private VitNXSectionPanel pnlListView;
        private VitNXListView lstTest;
        private VitNXSectionPanel pnlMessageBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private VitNXButton btnDialog;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private VitNXButton btnMessageBox;
        private VitNXCheckBox VitNXCheckBox2;
        private VitNXCheckBox VitNXCheckBox1;
        private System.Windows.Forms.Panel panel5;
        private VitNXRadioButton VitNXRadioButton2;
        private VitNXRadioButton VitNXRadioButton1;
        private VitNXRadioButton VitNXRadioButton3;
        private VitNXTitle VitNXTitle1;
        private VitNXTitle VitNXTitle2;
        private VitNXTitle VitNXTitle3;
        private System.Windows.Forms.Panel panel7;
        private VitNXComboBox VitNXComboBox1;
        private VitNXTitle VitNXTitle4;
        private System.Windows.Forms.Panel panel6;
        private VitNXNumericUpDown VitNXNumericUpDown1;
        private VitNXTitle VitNXTitle5;
        private VitNXGroupBox VitNXGroupBox1;
        private VitNXRadioButton VitNXRadioButton4;
        private VitNXCheckBox VitNXCheckBox3;
    }
}