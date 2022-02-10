using VitNX.Controls;

namespace Example
{
    partial class DockProject
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
            this.components = new System.ComponentModel.Container();
            this.treeProject = new VitNX.Controls.VitNX_TreeView();
            this.vitNXProgressBarStyle21 = new VitNX.Controls.VitNX_ProgressBarRounded();
            this.vitNXButton1 = new VitNX.Controls.VitNX_Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.vitNXProgressBar1 = new VitNX.Controls.VitNX_ProgressBar();
            this.vitNXButton2 = new VitNX.Controls.VitNX_Button();
            this.SuspendLayout();
            // 
            // treeProject
            // 
            this.treeProject.AllowMoveNodes = true;
            this.treeProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeProject.Location = new System.Drawing.Point(0, 25);
            this.treeProject.MaxDragChange = 20;
            this.treeProject.MultiSelect = true;
            this.treeProject.Name = "treeProject";
            this.treeProject.ShowIcons = true;
            this.treeProject.Size = new System.Drawing.Size(280, 425);
            this.treeProject.TabIndex = 0;
            this.treeProject.Text = "VitNX_TreeView1";
            // 
            // vitNXProgressBarStyle21
            // 
            this.vitNXProgressBarStyle21.CustomText = "";
            this.vitNXProgressBarStyle21.Location = new System.Drawing.Point(19, 398);
            this.vitNXProgressBarStyle21.Name = "vitNXProgressBarStyle21";
            this.vitNXProgressBarStyle21.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(179)))), ((int)(((byte)(243)))));
            this.vitNXProgressBarStyle21.Size = new System.Drawing.Size(165, 25);
            this.vitNXProgressBarStyle21.TabIndex = 1;
            this.vitNXProgressBarStyle21.TextColor = System.Drawing.Color.Black;
            this.vitNXProgressBarStyle21.TextFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.vitNXProgressBarStyle21.VisualMode = VitNX.Controls.VitNX_ProgressBarDisplayMode.Percentage;
            // 
            // vitNXButton1
            // 
            this.vitNXButton1.Location = new System.Drawing.Point(19, 325);
            this.vitNXButton1.Name = "vitNXButton1";
            this.vitNXButton1.Padding = new System.Windows.Forms.Padding(5);
            this.vitNXButton1.Size = new System.Drawing.Size(165, 29);
            this.vitNXButton1.TabIndex = 2;
            this.vitNXButton1.Text = "Progress Bar Tester";
            this.vitNXButton1.Click += new System.EventHandler(this.vitNXButton1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 60;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // vitNXProgressBar1
            // 
            this.vitNXProgressBar1.CustomText = "";
            this.vitNXProgressBar1.Location = new System.Drawing.Point(19, 364);
            this.vitNXProgressBar1.Name = "vitNXProgressBar1";
            this.vitNXProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.vitNXProgressBar1.Size = new System.Drawing.Size(165, 25);
            this.vitNXProgressBar1.TabIndex = 3;
            this.vitNXProgressBar1.TextColor = System.Drawing.Color.Black;
            this.vitNXProgressBar1.TextFont = new System.Drawing.Font("Arial", 12F);
            this.vitNXProgressBar1.VisualMode = VitNX.Controls.VitNX_ProgressBarDisplayMode.Percentage;
            // 
            // vitNXButton2
            // 
            this.vitNXButton2.Location = new System.Drawing.Point(19, 249);
            this.vitNXButton2.Name = "vitNXButton2";
            this.vitNXButton2.Padding = new System.Windows.Forms.Padding(5);
            this.vitNXButton2.Size = new System.Drawing.Size(165, 54);
            this.vitNXButton2.TabIndex = 4;
            this.vitNXButton2.Text = "Folder Select Dialog Tester\r\n(Not a standart C# Dialog)";
            this.vitNXButton2.Click += new System.EventHandler(this.vitNXButton2_Click);
            // 
            // DockProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vitNXButton2);
            this.Controls.Add(this.vitNXProgressBar1);
            this.Controls.Add(this.vitNXButton1);
            this.Controls.Add(this.vitNXProgressBarStyle21);
            this.Controls.Add(this.treeProject);
            this.DefaultDockArea = VitNX.Docking.VitNX_DockArea.Left;
            this.DockText = "Project Explorer";
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::Example.Icons.application_16x;
            this.Name = "DockProject";
            this.SerializationKey = "DockProject";
            this.Size = new System.Drawing.Size(280, 450);
            this.ResumeLayout(false);

        }

        #endregion

        private VitNX_TreeView treeProject;
        private VitNX_ProgressBarRounded vitNXProgressBarStyle21;
        private VitNX_Button vitNXButton1;
        private System.Windows.Forms.Timer timer1;
        private VitNX_ProgressBar vitNXProgressBar1;
        private VitNX_Button vitNXButton2;
    }
}
