using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using VitNX.UI.ControlsV1.BasedOnDarkUI.Controls;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Forms
{
    public partial class VitNX_Dialog : VitNX_Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            Functions.Windows.WindowSAndControls.WindowS.SetWindowsTenAndHighStyleForWinFormTitleToDark(Handle);
        }

        private VitNX_DialogButton _dialogButtons = VitNX_DialogButton.Ok;
        private List<VitNX_Button> _buttons;
        protected VitNX_Button btnOk;
        protected VitNX_Button btnCancel;
        protected VitNX_Button btnClose;
        protected VitNX_Button btnYes;
        protected VitNX_Button btnNo;
        protected VitNX_Button btnAbort;
        protected VitNX_Button btnRetry;
        protected VitNX_Button btnIgnore;

        [Description("Determines the type of the dialog window.")]
        [DefaultValue(VitNX_DialogButton.Ok)]
        public VitNX_DialogButton DialogButtons
        {
            get { return _dialogButtons; }
            set
            {
                if (_dialogButtons == value)
                    return;
                _dialogButtons = value;
                SetButtons();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TotalButtonSize { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new IButtonControl AcceptButton
        {
            get { return base.AcceptButton; }
            private set { base.AcceptButton = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new IButtonControl CancelButton
        {
            get { return base.CancelButton; }
            private set { base.CancelButton = value; }
        }

        public VitNX_Dialog()
        {
            InitializeComponent();
            _buttons = new List<VitNX_Button>
                {
                    btnAbort, btnRetry, btnIgnore, btnOk,
                    btnCancel, btnClose, btnYes, btnNo
                };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetButtons();
        }

        private void SetButtons()
        {
            foreach (var btn in _buttons)
                btn.Visible = false;
            switch (_dialogButtons)
            {
                case VitNX_DialogButton.Ok:
                    ShowButton(btnOk, true);
                    AcceptButton = btnOk;
                    break;
                case VitNX_DialogButton.Close:
                    ShowButton(btnClose, true);
                    AcceptButton = btnClose;
                    CancelButton = btnClose;
                    break;
                case VitNX_DialogButton.OkCancel:
                    ShowButton(btnOk);
                    ShowButton(btnCancel, true);
                    AcceptButton = btnOk;
                    CancelButton = btnCancel;
                    break;
                case VitNX_DialogButton.AbortRetryIgnore:
                    ShowButton(btnAbort);
                    ShowButton(btnRetry);
                    ShowButton(btnIgnore, true);
                    AcceptButton = btnAbort;
                    CancelButton = btnIgnore;
                    break;
                case VitNX_DialogButton.RetryCancel:
                    ShowButton(btnRetry);
                    ShowButton(btnCancel, true);
                    AcceptButton = btnRetry;
                    CancelButton = btnCancel;
                    break;
                case VitNX_DialogButton.YesNo:
                    ShowButton(btnYes);
                    ShowButton(btnNo, true);
                    AcceptButton = btnYes;
                    CancelButton = btnNo;
                    break;
                case VitNX_DialogButton.YesNoCancel:
                    ShowButton(btnYes);
                    ShowButton(btnNo);
                    ShowButton(btnCancel, true);
                    AcceptButton = btnYes;
                    CancelButton = btnCancel;
                    break;
            }
            SetFlowSize();
        }

        private void ShowButton(VitNX_Button button, 
            bool isLast = false)
        {
            button.SendToBack();
            if (!isLast)
                button.Margin = new Padding(0, 0, 10, 0);
            button.Visible = true;
        }

        private void SetFlowSize()
        {
            var width = flowInner.Padding.Horizontal;
            foreach (var btn in _buttons)
            {
                if (btn.Visible)
                    width += btn.Width + btn.Margin.Right;
            }
            flowInner.Width = width;
            TotalButtonSize = width;
        }
    }
}