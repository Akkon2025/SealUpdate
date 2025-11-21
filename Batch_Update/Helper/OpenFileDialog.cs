using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Batch_Update.Helper
{
    public class openFolderDialog
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        FolderBrowserDialog _oFileDialog;

        // Properties
        public string FileName
        {
            get { return _oFileDialog.SelectedPath; }
            set { _oFileDialog.SelectedPath = value; }
        }

        //public string Filter
        //{
        //    get { return _oFileDialog.Filter; }
        //    set { _oFileDialog.Filter = value; }
        //}

        //public string InitialDirectory
        //{
        //    get { return _oFileDialog.InitialDirectory; }
        //    set { _oFileDialog.InitialDirectory = value; }
        //}

        // Constructor
        public void GetFileNameClass()
        {
            _oFileDialog = new FolderBrowserDialog();
        }

        // Methods

        public void GetFileName()
        {
            GetFileNameClass();
            IntPtr ptr = GetForegroundWindow();
            WindowWrapper oWindow = new WindowWrapper(ptr);
            if (_oFileDialog.ShowDialog(oWindow) != DialogResult.OK)
            {
                _oFileDialog.SelectedPath = string.Empty;
            }
            oWindow = null;
        }
    }

    public class WindowWrapper : System.Windows.Forms.IWin32Window
    {
        private IntPtr _hwnd;

        // Property
        public virtual IntPtr Handle
        {
            get { return _hwnd; }
        }

        // Constructor
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }
    }
}
