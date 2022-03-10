using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using VitNX.Functions.WinControllers;
using VitNX.UI.ControlsV1.Docking;
using VitNX.UI.ControlsV1.Forms;
using VitNX.UI.ControlsV1.Win32;

using static VitNX.Functions.Win32.Enums;

namespace Example
{
    public partial class MainForm : VitNX_Form
    {
        private List<VitNX_DockContent> _toolWindows = new List<VitNX_DockContent>();
        private DockProject _dockProject;
        private DockProperties _dockProperties;
        private DockConsole _dockConsole;
        private DockLayers _dockLayers;
        private DockHistory _dockHistory;

        public MainForm()
        {
            InitializeComponent();
            Application.AddMessageFilter(new ControlScrollFilter());
            Application.AddMessageFilter(DockPanel.DockContentDragFilter);
            Application.AddMessageFilter(DockPanel.DockResizeFilter);
            HookEvents();
            _dockProject = new DockProject();
            _dockProperties = new DockProperties();
            _dockConsole = new DockConsole();
            _dockLayers = new DockLayers();
            _dockHistory = new DockHistory();
            _toolWindows.Add(_dockProject);
            _toolWindows.Add(_dockProperties);
            _toolWindows.Add(_dockConsole);
            _toolWindows.Add(_dockLayers);
            _toolWindows.Add(_dockHistory);
            if (File.Exists("dockpanel.config"))
                DeserializeDockPanel("dockpanel.config");
            else
            {
                foreach (var toolWindow in _toolWindows)
                    DockPanel.AddContent(toolWindow);
                DockPanel.AddContent(_dockHistory, _dockLayers.DockGroup);
            }
            BuildWindowMenu();
            DockPanel.AddContent(new DockDocument("Document 1", Icons.document_16xLG));
            DockPanel.AddContent(new DockDocument("Document 2", Icons.document_16xLG));
            DockPanel.AddContent(new DockDocument("Document 3", Icons.document_16xLG));
        }

        private void HookEvents()
        {
            FormClosing += MainForm_FormClosing;
            DockPanel.ContentAdded += DockPanel_ContentAdded;
            DockPanel.ContentRemoved += DockPanel_ContentRemoved;
            mnuNewFile.Click += NewFile_Click;
            mnuClose.Click += Close_Click;
            btnNewFile.Click += NewFile_Click;
            mnuDialog.Click += Dialog_Click;
            mnuProject.Click += Project_Click;
            mnuProperties.Click += Properties_Click;
            mnuConsole.Click += Console_Click;
            mnuLayers.Click += Layers_Click;
            mnuHistory.Click += History_Click;
            mnuAbout.Click += About_Click;
        }

        private void ToggleToolWindow(VitNX_ToolWindow toolWindow)
        {
            if (toolWindow.DockPanel == null)
                DockPanel.AddContent(toolWindow);
            else
                DockPanel.RemoveContent(toolWindow);
        }

        private void BuildWindowMenu()
        {
            mnuProject.Checked = DockPanel.ContainsContent(_dockProject);
            mnuProperties.Checked = DockPanel.ContainsContent(_dockProperties);
            mnuConsole.Checked = DockPanel.ContainsContent(_dockConsole);
            mnuLayers.Checked = DockPanel.Contains(_dockLayers);
            mnuHistory.Checked = DockPanel.Contains(_dockHistory);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SerializeDockPanel("dockpanel.config");
        }

        private void DockPanel_ContentAdded(object sender, DockContentEventArgs e)
        {
            if (_toolWindows.Contains(e.Content))
                BuildWindowMenu();
        }

        private void DockPanel_ContentRemoved(object sender, DockContentEventArgs e)
        {
            if (_toolWindows.Contains(e.Content))
                BuildWindowMenu();
        }

        private void NewFile_Click(object sender, EventArgs e)
        {
            var newFile = new DockDocument("New document", Icons.document_16xLG);
            DockPanel.AddContent(newFile);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Dialog_Click(object sender, EventArgs e)
        {
            var test = new DialogControls();
            test.ShowDialog();
        }

        private void Project_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockProject);
        }

        private void Properties_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockProperties);
        }

        private void Console_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockConsole);
        }

        private void Layers_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockLayers);
        }

        private void History_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockHistory);
        }

        private void About_Click(object sender, EventArgs e)
        {
            var about = new DialogAbout();
            about.ShowDialog();
        }

        private void WarningMessageBox_Click(object sender, EventArgs e)
        {
            VitNX_MessageBox.ShowWarning("This is a warning", "VitNX UI - Example");
        }

        private void QuestionMessageBox_Click(object sender, EventArgs e)
        {
            DialogResult a = VitNX_MessageBox.ShowQuestion("This is a question", "VitNX UI - Example");
            if (a == DialogResult.Yes)
                VitNX_MessageBox.ShowInformation("Your choice is Yes", "VitNX UI - Example");
            if (a == DialogResult.No)
                VitNX_MessageBox.ShowInformation("Your choice is No", "VitNX UI - Example");
        }

        private void SetTaskBarProgressBar_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "")
                toolStripTextBox1.Text = "50";
            if (toolStripComboBox1.SelectedItem == "Type")
                toolStripComboBox1.SelectedItem = "Normal";
            if (Convert.ToInt32(toolStripTextBox1.Text) <= 100)
            {
                switch (toolStripComboBox1.SelectedItem)
                {
                    case "Normal":
                        {
                            TaskBarProgressBar.SetState(Handle, TASKBAR_STATES.Normal);
                            TaskBarProgressBar.SetValue(Handle, Convert.ToInt32(toolStripTextBox1.Text), 100);
                            break;
                        }
                    case "Indeterminate":
                        TaskBarProgressBar.SetState(Handle, TASKBAR_STATES.Indeterminate);
                        break;
                    case "NoProgress":
                        TaskBarProgressBar.SetState(Handle, TASKBAR_STATES.NoProgress);
                        break;
                    case "Error":
                        {
                            TaskBarProgressBar.SetState(Handle, TASKBAR_STATES.Error);
                            TaskBarProgressBar.SetValue(Handle, Convert.ToInt32(toolStripTextBox1.Text), 100);
                            break;
                        }
                    case "Paused":
                        {
                            TaskBarProgressBar.SetState(Handle, TASKBAR_STATES.Paused);
                            TaskBarProgressBar.SetValue(Handle, Convert.ToInt32(toolStripTextBox1.Text), 100);
                            break;
                        }
                }
            }
            else { VitNX_MessageBox.ShowError("You need to enter from 0 to 100!", "VitNX UI - Example"); }
        }

        private void SerializeDockPanel(string path)
        {
            var state = DockPanel.GetDockPanelState();
            SerializerHelper.Serialize(state, path);
        }

        private void DeserializeDockPanel(string path)
        {
            var state = SerializerHelper.Deserialize<DockPanelState>(path);
            DockPanel.RestoreDockPanelState(state, GetContentBySerializationKey);
        }

        private VitNX_DockContent GetContentBySerializationKey(string key)
        {
            foreach (var window in _toolWindows)
            {
                if (window.SerializationKey == key)
                    return window;
            }
            return null;
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {

        }
    }
}