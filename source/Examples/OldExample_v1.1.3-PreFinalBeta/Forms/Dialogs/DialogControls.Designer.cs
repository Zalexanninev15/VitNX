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
            this.pnlTreeView = new VitNX.Controls.VitNX_SectionPanel();
            this.treeTest = new VitNX.Controls.VitNX_TreeView();
            this.pnlListView = new VitNX.Controls.VitNX_SectionPanel();
            this.lstTest = new VitNX.Controls.VitNX_ListView();
            this.pnlMessageBox = new VitNX.Controls.VitNX_SectionPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.VitNX_GroupBox1 = new VitNX.Controls.VitNX_GroupBox();
            this.VitNX_RadioButton4 = new VitNX.Controls.VitNX_RadioButton();
            this.VitNX_CheckBox3 = new VitNX.Controls.VitNX_CheckBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.VitNX_ComboBox1 = new VitNX.Controls.VitNX_ComboBox();
            this.VitNX_Title4 = new VitNX.Controls.VitNX_Title();
            this.panel6 = new System.Windows.Forms.Panel();
            this.VitNX_NumericUpDown1 = new VitNX.Controls.VitNX_NumericUpDown();
            this.VitNX_Title5 = new VitNX.Controls.VitNX_Title();
            this.panel5 = new System.Windows.Forms.Panel();
            this.VitNX_RadioButton3 = new VitNX.Controls.VitNX_RadioButton();
            this.VitNX_RadioButton2 = new VitNX.Controls.VitNX_RadioButton();
            this.VitNX_RadioButton1 = new VitNX.Controls.VitNX_RadioButton();
            this.VitNX_Title3 = new VitNX.Controls.VitNX_Title();
            this.panel4 = new System.Windows.Forms.Panel();
            this.VitNX_CheckBox2 = new VitNX.Controls.VitNX_CheckBox();
            this.VitNX_CheckBox1 = new VitNX.Controls.VitNX_CheckBox();
            this.VitNX_Title2 = new VitNX.Controls.VitNX_Title();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnMessageBox = new VitNX.Controls.VitNX_Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDialog = new VitNX.Controls.VitNX_Button();
            this.VitNX_Title1 = new VitNX.Controls.VitNX_Title();
            this.pnlMain.SuspendLayout();
            this.tblMain.SuspendLayout();
            this.pnlTreeView.SuspendLayout();
            this.pnlListView.SuspendLayout();
            this.pnlMessageBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.VitNX_GroupBox1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VitNX_NumericUpDown1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.treeTest.Text = "VitNX_TreeView1";
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
            this.lstTest.Text = "VitNX_ListView1";
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
            this.panel1.Controls.Add(this.VitNX_GroupBox1);
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
            // VitNX_GroupBox1
            // 
            this.VitNX_GroupBox1.AutoSize = true;
            this.VitNX_GroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.VitNX_GroupBox1.Controls.Add(this.VitNX_RadioButton4);
            this.VitNX_GroupBox1.Controls.Add(this.VitNX_CheckBox3);
            this.VitNX_GroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_GroupBox1.Location = new System.Drawing.Point(10, 411);
            this.VitNX_GroupBox1.Name = "VitNX_GroupBox1";
            this.VitNX_GroupBox1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.VitNX_GroupBox1.Size = new System.Drawing.Size(200, 69);
            this.VitNX_GroupBox1.TabIndex = 24;
            this.VitNX_GroupBox1.TabStop = false;
            this.VitNX_GroupBox1.Text = "GroupBox";
            // 
            // VitNX_RadioButton4
            // 
            this.VitNX_RadioButton4.AutoSize = true;
            this.VitNX_RadioButton4.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_RadioButton4.Location = new System.Drawing.Point(10, 40);
            this.VitNX_RadioButton4.Name = "VitNX_RadioButton4";
            this.VitNX_RadioButton4.Size = new System.Drawing.Size(180, 19);
            this.VitNX_RadioButton4.TabIndex = 1;
            this.VitNX_RadioButton4.TabStop = true;
            this.VitNX_RadioButton4.Text = "Radio button";
            // 
            // VitNX_CheckBox3
            // 
            this.VitNX_CheckBox3.AutoSize = true;
            this.VitNX_CheckBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_CheckBox3.Location = new System.Drawing.Point(10, 21);
            this.VitNX_CheckBox3.Name = "VitNX_CheckBox3";
            this.VitNX_CheckBox3.Size = new System.Drawing.Size(180, 19);
            this.VitNX_CheckBox3.TabIndex = 0;
            this.VitNX_CheckBox3.Text = "Checkbox";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.VitNX_ComboBox1);
            this.panel7.Controls.Add(this.VitNX_Title4);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(10, 348);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(200, 63);
            this.panel7.TabIndex = 23;
            // 
            // VitNX_ComboBox1
            // 
            this.VitNX_ComboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.VitNX_ComboBox1.FormattingEnabled = true;
            this.VitNX_ComboBox1.Items.AddRange(new object[] {
            "Item 1",
            "Item 2",
            "This is a really long item in the collection to check out how text is clipped",
            "Item 4"});
            this.VitNX_ComboBox1.Location = new System.Drawing.Point(0, 26);
            this.VitNX_ComboBox1.Name = "VitNX_ComboBox1";
            this.VitNX_ComboBox1.Size = new System.Drawing.Size(200, 24);
            this.VitNX_ComboBox1.TabIndex = 20;
            // 
            // VitNX_Title4
            // 
            this.VitNX_Title4.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_Title4.Location = new System.Drawing.Point(0, 0);
            this.VitNX_Title4.Name = "VitNX_Title4";
            this.VitNX_Title4.Size = new System.Drawing.Size(200, 26);
            this.VitNX_Title4.TabIndex = 21;
            this.VitNX_Title4.Text = "ComboBox";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.VitNX_NumericUpDown1);
            this.panel6.Controls.Add(this.VitNX_Title5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(10, 284);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 64);
            this.panel6.TabIndex = 22;
            // 
            // VitNX_NumericUpDown1
            // 
            this.VitNX_NumericUpDown1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_NumericUpDown1.Location = new System.Drawing.Point(0, 26);
            this.VitNX_NumericUpDown1.Name = "VitNX_NumericUpDown1";
            this.VitNX_NumericUpDown1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VitNX_NumericUpDown1.Size = new System.Drawing.Size(200, 23);
            this.VitNX_NumericUpDown1.TabIndex = 24;
            // 
            // VitNX_Title5
            // 
            this.VitNX_Title5.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_Title5.Location = new System.Drawing.Point(0, 0);
            this.VitNX_Title5.Name = "VitNX_Title5";
            this.VitNX_Title5.Size = new System.Drawing.Size(200, 26);
            this.VitNX_Title5.TabIndex = 23;
            this.VitNX_Title5.Text = "Numeric Up/Down";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.VitNX_RadioButton3);
            this.panel5.Controls.Add(this.VitNX_RadioButton2);
            this.panel5.Controls.Add(this.VitNX_RadioButton1);
            this.panel5.Controls.Add(this.VitNX_Title3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(10, 184);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 100);
            this.panel5.TabIndex = 12;
            // 
            // VitNX_RadioButton3
            // 
            this.VitNX_RadioButton3.AutoSize = true;
            this.VitNX_RadioButton3.Checked = true;
            this.VitNX_RadioButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_RadioButton3.Enabled = false;
            this.VitNX_RadioButton3.Location = new System.Drawing.Point(0, 64);
            this.VitNX_RadioButton3.Name = "VitNX_RadioButton3";
            this.VitNX_RadioButton3.Size = new System.Drawing.Size(200, 19);
            this.VitNX_RadioButton3.TabIndex = 4;
            this.VitNX_RadioButton3.TabStop = true;
            this.VitNX_RadioButton3.Text = "Disabled radio button";
            // 
            // VitNX_RadioButton2
            // 
            this.VitNX_RadioButton2.AutoSize = true;
            this.VitNX_RadioButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_RadioButton2.Location = new System.Drawing.Point(0, 45);
            this.VitNX_RadioButton2.Name = "VitNX_RadioButton2";
            this.VitNX_RadioButton2.Size = new System.Drawing.Size(200, 19);
            this.VitNX_RadioButton2.TabIndex = 3;
            this.VitNX_RadioButton2.Text = "Radio button";
            // 
            // VitNX_RadioButton1
            // 
            this.VitNX_RadioButton1.AutoSize = true;
            this.VitNX_RadioButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_RadioButton1.Location = new System.Drawing.Point(0, 26);
            this.VitNX_RadioButton1.Name = "VitNX_RadioButton1";
            this.VitNX_RadioButton1.Size = new System.Drawing.Size(200, 19);
            this.VitNX_RadioButton1.TabIndex = 2;
            this.VitNX_RadioButton1.Text = "Radio button";
            // 
            // VitNX_Title3
            // 
            this.VitNX_Title3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_Title3.Location = new System.Drawing.Point(0, 0);
            this.VitNX_Title3.Name = "VitNX_Title3";
            this.VitNX_Title3.Size = new System.Drawing.Size(200, 26);
            this.VitNX_Title3.TabIndex = 16;
            this.VitNX_Title3.Text = "Radio buttons";
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.VitNX_CheckBox2);
            this.panel4.Controls.Add(this.VitNX_CheckBox1);
            this.panel4.Controls.Add(this.VitNX_Title2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(10, 110);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel4.Size = new System.Drawing.Size(200, 74);
            this.panel4.TabIndex = 11;
            // 
            // VitNX_CheckBox2
            // 
            this.VitNX_CheckBox2.AutoSize = true;
            this.VitNX_CheckBox2.Checked = true;
            this.VitNX_CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.VitNX_CheckBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_CheckBox2.Enabled = false;
            this.VitNX_CheckBox2.Location = new System.Drawing.Point(0, 45);
            this.VitNX_CheckBox2.Name = "VitNX_CheckBox2";
            this.VitNX_CheckBox2.Size = new System.Drawing.Size(200, 19);
            this.VitNX_CheckBox2.TabIndex = 13;
            this.VitNX_CheckBox2.Text = "Disabled checkbox";
            // 
            // VitNX_CheckBox1
            // 
            this.VitNX_CheckBox1.AutoSize = true;
            this.VitNX_CheckBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_CheckBox1.Location = new System.Drawing.Point(0, 26);
            this.VitNX_CheckBox1.Name = "VitNX_CheckBox1";
            this.VitNX_CheckBox1.Size = new System.Drawing.Size(200, 19);
            this.VitNX_CheckBox1.TabIndex = 12;
            this.VitNX_CheckBox1.Text = "Enabled checkbox";
            // 
            // VitNX_Title2
            // 
            this.VitNX_Title2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_Title2.Location = new System.Drawing.Point(0, 0);
            this.VitNX_Title2.Name = "VitNX_Title2";
            this.VitNX_Title2.Size = new System.Drawing.Size(200, 26);
            this.VitNX_Title2.TabIndex = 15;
            this.VitNX_Title2.Text = "Check boxes";
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.btnMessageBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 70);
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
            this.btnMessageBox.Text = "Information MessageBox";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.btnDialog);
            this.panel2.Controls.Add(this.VitNX_Title1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panel2.Size = new System.Drawing.Size(200, 60);
            this.panel2.TabIndex = 5;
            // 
            // btnDialog
            // 
            this.btnDialog.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDialog.Location = new System.Drawing.Point(0, 26);
            this.btnDialog.Name = "btnDialog";
            this.btnDialog.Padding = new System.Windows.Forms.Padding(5);
            this.btnDialog.Size = new System.Drawing.Size(200, 29);
            this.btnDialog.TabIndex = 4;
            this.btnDialog.Text = "Error MessageBox";
            // 
            // VitNX_Title1
            // 
            this.VitNX_Title1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VitNX_Title1.Location = new System.Drawing.Point(0, 0);
            this.VitNX_Title1.Name = "VitNX_Title1";
            this.VitNX_Title1.Size = new System.Drawing.Size(200, 26);
            this.VitNX_Title1.TabIndex = 14;
            this.VitNX_Title1.Text = "Dialogs";
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
            this.VitNX_GroupBox1.ResumeLayout(false);
            this.VitNX_GroupBox1.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VitNX_NumericUpDown1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private VitNX_SectionPanel pnlTreeView;
        private VitNX_TreeView treeTest;
        private VitNX_SectionPanel pnlListView;
        private VitNX_ListView lstTest;
        private VitNX_SectionPanel pnlMessageBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private VitNX_Button btnDialog;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private VitNX_Button btnMessageBox;
        private VitNX_CheckBox VitNX_CheckBox2;
        private VitNX_CheckBox VitNX_CheckBox1;
        private System.Windows.Forms.Panel panel5;
        private VitNX_RadioButton VitNX_RadioButton2;
        private VitNX_RadioButton VitNX_RadioButton1;
        private VitNX_RadioButton VitNX_RadioButton3;
        private VitNX_Title VitNX_Title1;
        private VitNX_Title VitNX_Title2;
        private VitNX_Title VitNX_Title3;
        private System.Windows.Forms.Panel panel7;
        private VitNX_ComboBox VitNX_ComboBox1;
        private VitNX_Title VitNX_Title4;
        private System.Windows.Forms.Panel panel6;
        private VitNX_NumericUpDown VitNX_NumericUpDown1;
        private VitNX_Title VitNX_Title5;
        private VitNX_GroupBox VitNX_GroupBox1;
        private VitNX_RadioButton VitNX_RadioButton4;
        private VitNX_CheckBox VitNX_CheckBox3;
    }
}