using System;
using System.Reflection;
using System.Windows.Forms;

namespace VitNX.UI.OtherControls
{
    /// <summary>
    /// Work with the folder dialog, Windows Vista+.
    /// </summary>
    public class NewFolderDialog
    {
        private string _initialDirectory;
        private string _title;
        private string _fileName = "";

        /// <summary>
        /// Gets or sets the initial directory.
        /// </summary>
        public string InitialDirectory
        {
            get
            {
                return string.IsNullOrEmpty(_initialDirectory) ?
                    Environment.CurrentDirectory :
                    _initialDirectory;
            }
            set { _initialDirectory = value; }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get { return _title ?? "Select a folder"; }
            set { _title = value; }
        }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
        }

        /// <summary>
        /// Shows the folder dialog.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool Show()
        {
            return Show(IntPtr.Zero);
        }

        /// <summary>
        /// Are shows the folder dialog.
        /// </summary>
        /// <param name="hWndOwner">The h wnd owner.</param>
        /// <returns>A bool.</returns>
        public bool Show(IntPtr hWndOwner)
        {
            var result = Environment.OSVersion.Version.Major >= 6 ?
                ModernDialog.Show(hWndOwner, InitialDirectory, Title) :
                ShowXpDialog(hWndOwner, InitialDirectory, Title);
            _fileName = result.FileName;
            return result.Result;
        }

        private struct ShowDialogResult
        {
            /// <summary>
            /// Gets or sets a value indicating whether result.
            /// </summary>
            public bool Result { get; set; }

            /// <summary>
            /// Gets or sets the file name.
            /// </summary>
            public string FileName { get; set; }
        }

        private static ShowDialogResult ShowXpDialog(IntPtr ownerHandle,
            string initialDirectory,
            string title)
        {
            var folderBrowserDialog = new FolderBrowserDialog
            { Description = title, SelectedPath = initialDirectory, ShowNewFolderButton = false };
            var dialogResult = new ShowDialogResult();
            if (folderBrowserDialog.ShowDialog(new WindowWrapper(ownerHandle)) == DialogResult.OK)
            {
                dialogResult.Result = true;
                dialogResult.FileName = folderBrowserDialog.SelectedPath;
            }
            return dialogResult;
        }

        private static class ModernDialog
        {
            private const string c_foldersFilter = "Folders|\n";

            private const BindingFlags c_flags = BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic;

            private static readonly Assembly s_windowsFormsAssembly = typeof(FileDialog).Assembly;

            private static readonly Type s_iFileDialogType = s_windowsFormsAssembly.
                GetType("System.Windows.Forms.FileDialogNative+IFileDialog");

            private static readonly MethodInfo s_createVistaDialogMethodInfo = typeof(OpenFileDialog).
                GetMethod("CreateVistaDialog", c_flags);

            private static readonly MethodInfo s_onBeforeVistaDialogMethodInfo = typeof(OpenFileDialog).
                GetMethod("OnBeforeVistaDialog", c_flags);

            private static readonly MethodInfo s_getOptionsMethodInfo = typeof(FileDialog).
                GetMethod("GetOptions", c_flags);

            private static readonly MethodInfo s_setOptionsMethodInfo = s_iFileDialogType.
                GetMethod("SetOptions", c_flags);

            private static readonly uint s_fosPickFoldersBitFlag = (uint)s_windowsFormsAssembly.
                GetType("System.Windows.Forms.FileDialogNative+FOS").
                GetField("FOS_PICKFOLDERS").
                GetValue(null);

            private static readonly ConstructorInfo s_VistaDialogEventsConstructorInfo = s_windowsFormsAssembly.
                GetType("System.Windows.Forms.FileDialog+VistaDialogEvents").
                GetConstructor(c_flags,
                null,
                new[] { typeof(FileDialog) },
                null);

            private static readonly MethodInfo s_adviseMethodInfo = s_iFileDialogType.
                GetMethod("Advise");

            private static readonly MethodInfo s_unAdviseMethodInfo = s_iFileDialogType.
                GetMethod("Unadvise");

            private static readonly MethodInfo s_showMethodInfo = s_iFileDialogType.
                GetMethod("Show");

            /// <summary>
            /// Shows the folder dialog.
            /// </summary>
            /// <param name="ownerHandle">The owner handle.</param>
            /// <param name="initialDirectory">The initial directory.</param>
            /// <param name="title">The title.</param>
            /// <returns>A ShowDialogResult.</returns>
            public static ShowDialogResult Show(IntPtr ownerHandle,
                string initialDirectory,
                string title)
            {
                var openFileDialog = new OpenFileDialog
                {
                    AddExtension = false,
                    CheckFileExists = false,
                    DereferenceLinks = true,
                    Filter = c_foldersFilter,
                    InitialDirectory = initialDirectory,
                    Multiselect = false,
                    Title = title
                };
                var iFileDialog = s_createVistaDialogMethodInfo.Invoke(openFileDialog, new object[] { });
                s_onBeforeVistaDialogMethodInfo.Invoke(openFileDialog, new[] { iFileDialog });
                s_setOptionsMethodInfo.Invoke(iFileDialog, new object[]
                { (uint)s_getOptionsMethodInfo.Invoke(openFileDialog, new object[] { }) | s_fosPickFoldersBitFlag });
                var adviseParametersWithOutputConnectionToken = new[]
                { s_VistaDialogEventsConstructorInfo.Invoke(new object[] { openFileDialog }), 0U };
                s_adviseMethodInfo.Invoke(iFileDialog, adviseParametersWithOutputConnectionToken);
                try
                {
                    int retVal = (int)s_showMethodInfo.Invoke(iFileDialog,
                        new object[] { ownerHandle });
                    return new ShowDialogResult
                    {
                        Result = retVal == 0,
                        FileName = openFileDialog.FileName
                    };
                }
                finally
                {
                    s_unAdviseMethodInfo.Invoke(iFileDialog,
              new[] { adviseParametersWithOutputConnectionToken[1] });
                }
            }
        }

        private class WindowWrapper : IWin32Window
        {
            private readonly IntPtr _handle;

            public WindowWrapper(IntPtr handle)
            { _handle = handle; }

            public IntPtr Handle
            { get { return _handle; } }
        }
    }
}