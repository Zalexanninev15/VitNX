﻿using VitNX.UI.ControlsV1.Controls;

namespace Examples1
{
    partial class DockConsole
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
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstConsole = new VitNX_ListView();
            this.SuspendLayout();
            // 
            // lstConsole
            // 
            this.lstConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstConsole.Location = new System.Drawing.Point(0, 25);
            this.lstConsole.MultiSelect = true;
            this.lstConsole.Name = "lstConsole";
            this.lstConsole.Size = new System.Drawing.Size(500, 175);
            this.lstConsole.TabIndex = 0;
            this.lstConsole.Text = "VitNX_ListView1";
            // 
            // DockConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstConsole);
            this.DefaultDockArea = VitNX.UI.ControlsV1.Docking.VitNX_DockArea.Bottom;
            this.DockText = "Console";
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::Examples1.Icons.Console;
            this.Name = "DockConsole";
            this.SerializationKey = "DockConsole";
            this.Size = new System.Drawing.Size(500, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private VitNX.UI.ControlsV1.Controls.VitNX_ListView lstConsole;
    }
}