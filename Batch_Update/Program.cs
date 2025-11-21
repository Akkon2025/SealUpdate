using System;
using System.Collections.Generic;
using SAPbouiCOM.Framework;

namespace Batch_Update
{
    class Program
    {

        public static SAPbobsCOM.Company oCompany { get; set; }
        public static SAPbouiCOM.Application SBO_Application { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application oApp = null;
                if (args.Length < 1)
                {
                    oApp = new Application();
                }
                else
                {
                    oApp = new Application(args[0]);
                }
                Menu MyMenu = new Menu();
                MyMenu.AddMenuItems();
                oApp.RegisterMenuEventHandler(MyMenu.SBO_Application_MenuEvent);
                Application.SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
                oCompany = (SAPbobsCOM.Company)Application.SBO_Application.Company.GetDICompany();

                List<ComboList> SealStatus = new List<ComboList>();
                SealStatus.Add(new ComboList { Value = "1", Description = "Null" });
                SealStatus.Add(new ComboList { Value = "2", Description = "Success" });
                SealStatus.Add(new ComboList { Value = "3", Description = "Failed" });

                TableCreate.CreateUserFields("ORDR", "SealStatus", "Seal Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, SAPbobsCOM.BoFldSubTypes.st_None, combolist: SealStatus);

                oApp.Run();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        static void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Exit Add-On
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    break;
                default:
                    break;
            }
        }
    }
}
