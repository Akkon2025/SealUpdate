using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;


namespace Batch_Update.Helper
{
    public class AttachmentCreate
    {
        //public string FileName;
        public string FilePath;
        public byte[] FileArray;

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        OpenFileDialog _oFileDialog;
        SaveFileDialog _oSaveFileDialog;
        FolderBrowserDialog _oFolderBrowserDialog;

        public string saveFilePath
        {
            get { return _oSaveFileDialog.FileName; }
            set { _oSaveFileDialog.FileName = value; }
        }

        public string folderbrowserPath
        {
            get { return _oFolderBrowserDialog.SelectedPath; }
            set { _oFolderBrowserDialog.SelectedPath = value; }
        }

        // Properties
        public string FileName
        {
            get { return _oFileDialog.FileName; }
            set { _oFileDialog.FileName = value; }
        }

        public string Filter
        {
            get { return _oFileDialog.Filter; }
            set { _oFileDialog.Filter = value; }
        }

        public string InitialDirectory
        {
            get { return _oFileDialog.InitialDirectory; }
            set { _oFileDialog.InitialDirectory = value; }
        }

        public AttachmentCreate()
        {
            _oFileDialog = new OpenFileDialog();
            _oFileDialog.Multiselect = true;
            _oSaveFileDialog = new SaveFileDialog();
            _oFolderBrowserDialog = new FolderBrowserDialog();
        }



        public string showOpenFileDialog(bool openFolder = false)
        {
            //AttachmentCreate oGetFileName = new AttachmentCreate();            
            //oGetFileName.Filter = "All files (*.*)|*.*";
            //oGetFileName.InitialDirectory =
            //    Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //Thread threadGetExcelFile = new Thread(new ThreadStart(oGetFileName.ShowFolderBrowser));            
            ////System.Threading.Thread ShowFolderBrowserThread = default(System.Threading.Thread);  
            //threadGetExcelFile.SetApartmentState(System.Threading.ApartmentState.STA);
            //try
            //{
            //    threadGetExcelFile.Start();
            //    while (!threadGetExcelFile.IsAlive) ; // Wait for thread to get started
            //    Thread.Sleep(1);  // Wait a sec more
            //    threadGetExcelFile.Join();

            //    if (!string.IsNullOrEmpty(oGetFileName.FileName))
            //    {
            //        return oGetFileName.FileName + "|" + oGetFileName._oFileDialog.SafeFileName;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //SBO_Application.MessageBox("FileFile" & ex.Message)
            //    System.Windows.Forms.MessageBox.Show(ex.ToString());
            //}

            //return "";

            AttachmentCreate oGetFileName = new AttachmentCreate();
            oGetFileName._oFileDialog.Multiselect = true; // ✅ Çoklu seçim açıldı
            oGetFileName._oFileDialog.Filter = "All files (*.*)|*.*";
            oGetFileName._oFileDialog.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            Thread threadGetExcelFile = new Thread(new ThreadStart(oGetFileName.ShowFolderBrowser));
            threadGetExcelFile.SetApartmentState(ApartmentState.STA);

            try
            {
                threadGetExcelFile.Start();
                while (!threadGetExcelFile.IsAlive) ; // Thread başlayana kadar bekle
                Thread.Sleep(1);
                threadGetExcelFile.Join();

                if (oGetFileName._oFileDialog.FileNames.Length > 0)
                {
                    // Full path listesi
                    string allFiles = string.Join(";", oGetFileName._oFileDialog.FileNames);

                    // Sadece dosya adları
                    string allSafeFiles = string.Join(";", oGetFileName._oFileDialog.SafeFileNames);

                    return allFiles + "|" + allSafeFiles;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

            return "";
        }


        public string showFileFolderBrowser()
        {
            AttachmentCreate oGetFileName = new AttachmentCreate();
            oGetFileName.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Thread threadGetExcelFile = new Thread(new ThreadStart(oGetFileName.ShowFolderBrowserDialog));
            //System.Threading.Thread ShowFolderBrowserThread = default(System.Threading.Thread);  
            threadGetExcelFile.SetApartmentState(System.Threading.ApartmentState.STA);
            try
            {
                threadGetExcelFile.Start();
                while (!threadGetExcelFile.IsAlive) ; // Wait for thread to get started
                Thread.Sleep(1);  // Wait a sec more
                threadGetExcelFile.Join();

                if (!string.IsNullOrEmpty(oGetFileName.folderbrowserPath))
                {

                    return oGetFileName.folderbrowserPath;
                }
            }
            catch (Exception ex)
            {
                //SBO_Application.MessageBox("FileFile" & ex.Message)
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

            return "";
        }
        public string saveFileDialog(bool openFolder = false)
        {
            AttachmentCreate oGetFileNamePath = new AttachmentCreate();
            oGetFileNamePath.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Thread threadGetExcelFile = new Thread(new ThreadStart(oGetFileNamePath.ShowFolderBrowser_SaveFileDialog));
            //System.Threading.Thread ShowFolderBrowserThread = default(System.Threading.Thread);  
            threadGetExcelFile.SetApartmentState(System.Threading.ApartmentState.STA);
            try
            {
                threadGetExcelFile.Start();
                while (!threadGetExcelFile.IsAlive) ; // Wait for thread to get started
                Thread.Sleep(1);  // Wait a sec more
                threadGetExcelFile.Join();

                if (!string.IsNullOrEmpty(oGetFileNamePath.saveFilePath))
                {

                    return oGetFileNamePath.saveFilePath;
                }
            }
            catch (Exception ex)
            {
                //SBO_Application.MessageBox("FileFile" & ex.Message)
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

            return "";
        }


        public void ShowFolderBrowser()
        {
            //System.Diagnostics.Process[] MyProcs = null;
            //System.Windows.Forms.DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            //openFileDialog1.Multiselect = false;
            ////OpenFile.Filter = "All files(*.CSV)|*.CSV";
            //int filterindex = 0;
            //try
            //{
            //    filterindex = 0;
            //}
            //catch (Exception ex)
            //{
            //}

            //openFileDialog1.FilterIndex = filterindex;

            //openFileDialog1.RestoreDirectory = true;


            IntPtr ptr = GetForegroundWindow();
            WindowWrapper oWindow = new WindowWrapper(ptr);
            //System.Windows.Forms.DialogResult ret = openFileDialog1.ShowDialog(oWindow);
            if (_oFileDialog.ShowDialog(oWindow) != DialogResult.OK) // Test result.
            {
                FileName = _oFileDialog.FileName;

            }
            else
            {
                System.Windows.Forms.Application.ExitThread();
            }
        }


        public void ShowFolderBrowserDialog()
        {

            IntPtr ptr = GetForegroundWindow();
            WindowWrapper oWindow = new WindowWrapper(ptr);
            //System.Windows.Forms.DialogResult ret = openFileDialog1.ShowDialog(oWindow);

            if (_oFolderBrowserDialog.ShowDialog(oWindow) != DialogResult.OK)
            {
                folderbrowserPath = _oFolderBrowserDialog.SelectedPath;
            }
            else
            {

                System.Windows.Forms.Application.ExitThread();
            }
        }

        public void ShowFolderBrowser_SaveFileDialog()
        {
            //System.Diagnostics.Process[] MyProcs = null;
            //System.Windows.Forms.DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            //openFileDialog1.Multiselect = false;
            ////OpenFile.Filter = "All files(*.CSV)|*.CSV";
            //int filterindex = 0;
            //try
            //{
            //    filterindex = 0;
            //}
            //catch (Exception ex)
            //{
            //}

            //openFileDialog1.FilterIndex = filterindex;

            //openFileDialog1.RestoreDirectory = true;


            IntPtr ptr = GetForegroundWindow();
            WindowWrapper oWindow = new WindowWrapper(ptr);
            //System.Windows.Forms.DialogResult ret = openFileDialog1.ShowDialog(oWindow);

            if (_oSaveFileDialog.ShowDialog(oWindow) != DialogResult.OK)
            {
                saveFilePath = _oSaveFileDialog.FileName;
            }
            else
            {

                System.Windows.Forms.Application.ExitThread();
            }
        }
        //public string GetFileName()
        //{
        //    return Result;
        //}



        public void SaveFiles(byte[] fs)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Kaydedilecek yer seçiniz";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                if (!Directory.Exists(saveFileDialog1.InitialDirectory + "\\" + saveFileDialog1.FileName))
                    Directory.CreateDirectory(saveFileDialog1.InitialDirectory + "\\" + saveFileDialog1.FileName);

                MemoryStream memoryStream = new MemoryStream(fs);
                FileStream fileStream = new FileStream(saveFileDialog1.InitialDirectory + "\\" + saveFileDialog1.FileName, FileMode.CreateNew);
                memoryStream.WriteTo(fileStream);
                memoryStream.Close();
                fileStream.Close();
                fileStream = null;
                memoryStream = null;
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.

            }
        }
    }
}
