using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Docking
{
    [ToolboxItem(false)]
    public class VitNX_DockContent : UserControl
    {
        public event EventHandler DockTextChanged;

        private string _dockText;
        private Image _icon;

        [Category("Appearance")]
        [Description("Determines the text that will appear in the content tabs and headers.")]
        public string DockText
        {
            get { return _dockText; }
            set
            {
                var oldText = _dockText;
                _dockText = value;
                if (DockTextChanged != null)
                    DockTextChanged(this, null);
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Determines the icon that will appear in the content tabs and headers.")]
        public Image Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                Invalidate();
            }
        }

        [Category("Layout")]
        [Description("Determines the default area of the dock panel this content will be added to.")]
        [DefaultValue(VitNX_DockArea.Document)]
        public VitNX_DockArea DefaultDockArea { get; set; }

        [Category("Behavior")]
        [Description("Determines the key used by this content in the dock serialization.")]
        public string SerializationKey { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VitNX_DockPanel DockPanel { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VitNX_DockRegion DockRegion { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VitNX_DockGroup DockGroup { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VitNX_DockArea DockArea { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Order { get; set; }

        public VitNX_DockContent()
        { }

        public virtual void Close()
        {
            if (DockPanel != null)
                DockPanel.RemoveContent(this);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (DockPanel == null)
                return;
            DockPanel.ActiveContent = this;
        }
    }
}