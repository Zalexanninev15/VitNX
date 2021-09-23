using VitNX.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace VitNX.Forms
{
    public partial class VNXDialog : VNXForm
    {
        #region Field Region

        private VNXDialogButton _dialogButtons = VNXDialogButton.Ok;
        private List<VNXButton> _buttons;

        #endregion

        #region Button Region

        protected VNXButton btnOk;
        protected VNXButton btnCancel;
        protected VNXButton btnClose;
        protected VNXButton btnYes;
        protected VNXButton btnNo;
        protected VNXButton btnAbort;
        protected VNXButton btnRetry;
        protected VNXButton btnIgnore;

        #endregion

        #region Property Region

        [Description("Determines the type of the dialog window.")]
        [DefaultValue(VNXDialogButton.Ok)]
        public VNXDialogButton DialogButtons
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

        #endregion

        #region Constructor Region

        public VNXDialog()
        {
            InitializeComponent();

            _buttons = new List<VNXButton>
                {
                    btnAbort, btnRetry, btnIgnore, btnOk,
                    btnCancel, btnClose, btnYes, btnNo
                };
        }

        #endregion

        #region Event Handler Region

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            SetButtons();
        }

        #endregion

        #region Method Region

        private void SetButtons()
        {
            foreach (var btn in _buttons)
                btn.Visible = false;

            switch (_dialogButtons)
            {
                case VNXDialogButton.Ok:
                    ShowButton(btnOk, true);
                    AcceptButton = btnOk;
                    break;
                case VNXDialogButton.Close:
                    ShowButton(btnClose, true);
                    AcceptButton = btnClose;
                    CancelButton = btnClose;
                    break;
                case VNXDialogButton.OkCancel:
                    ShowButton(btnOk);
                    ShowButton(btnCancel, true);
                    AcceptButton = btnOk;
                    CancelButton = btnCancel;
                    break;
                case VNXDialogButton.AbortRetryIgnore:
                    ShowButton(btnAbort);
                    ShowButton(btnRetry);
                    ShowButton(btnIgnore, true);
                    AcceptButton = btnAbort;
                    CancelButton = btnIgnore;
                    break;
                case VNXDialogButton.RetryCancel:
                    ShowButton(btnRetry);
                    ShowButton(btnCancel, true);
                    AcceptButton = btnRetry;
                    CancelButton = btnCancel;
                    break;
                case VNXDialogButton.YesNo:
                    ShowButton(btnYes);
                    ShowButton(btnNo, true);
                    AcceptButton = btnYes;
                    CancelButton = btnNo;
                    break;
                case VNXDialogButton.YesNoCancel:
                    ShowButton(btnYes);
                    ShowButton(btnNo);
                    ShowButton(btnCancel, true);
                    AcceptButton = btnYes;
                    CancelButton = btnCancel;
                    break;
            }

            SetFlowSize();
        }

        private void ShowButton(VNXButton button, bool isLast = false)
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

        #endregion
    }
}
