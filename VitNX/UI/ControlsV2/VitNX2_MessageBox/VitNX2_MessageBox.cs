using System.Windows.Forms;

namespace VitNX.UI.ControlsV2
{
    public abstract class VitNX2_MessageBox
    {
        public static DialogResult Show(string text)
        {
            DialogResult result;
            using (var msgForm = new VitNX2_MessageBox_Form(text))
                result = msgForm.ShowDialog();
            return result;
        }

        public static DialogResult Show(string text, string caption)
        {
            DialogResult result;
            using (var msgForm = new VitNX2_MessageBox_Form(text, caption))
                result = msgForm.ShowDialog();
            return result;
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            DialogResult result;
            using (var msgForm = new VitNX2_MessageBox_Form(text, caption, buttons))
                result = msgForm.ShowDialog();
            return result;
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            DialogResult result;
            using (var msgForm = new VitNX2_MessageBox_Form(text, caption, buttons, icon))
                result = msgForm.ShowDialog();
            return result;
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            DialogResult result;
            using (var msgForm = new VitNX2_MessageBox_Form(text, caption, buttons, icon, defaultButton))
                result = msgForm.ShowDialog();
            return result;
        }

        public static DialogResult Show(IWin32Window owner, string text)
        {
            DialogResult result;
            using (var msgForm = new VitNX2_MessageBox_Form(text))
                result = msgForm.ShowDialog(owner);
            return result;
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption)
        {
            DialogResult result;
            using (var msgForm = new VitNX2_MessageBox_Form(text, caption))
                result = msgForm.ShowDialog(owner);
            return result;
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            DialogResult result;
            using (var msgForm = new VitNX2_MessageBox_Form(text, caption, buttons))
                result = msgForm.ShowDialog(owner);
            return result;
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            DialogResult result;
            using (var msgForm = new VitNX2_MessageBox_Form(text, caption, buttons, icon))
                result = msgForm.ShowDialog(owner);
            return result;
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            DialogResult result;
            using (var msgForm = new VitNX2_MessageBox_Form(text, caption, buttons, icon, defaultButton))
                result = msgForm.ShowDialog(owner);
            return result;
        }
    }
}