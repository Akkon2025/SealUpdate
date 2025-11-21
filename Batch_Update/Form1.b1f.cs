using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using SAPbouiCOM.Framework;

namespace Batch_Update
{
    [FormAttribute("Batch_Update.Form1", "Form1.b1f")]
    class Form1 : UserFormBase
    {
        public Form1()
        {

        }
        class Logger
        {
            private static readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", $"log_{DateTime.Now:yyyy-MM-dd}.txt");

            public static void Log(string message)
            {
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));  
                    string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
                     
                    File.AppendAllText(logFilePath, logMessage + Environment.NewLine, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show($"Loglama hatası: {ex.Message}", "Hata", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_1").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_17").Specific));
            this.Button0.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button0_ClickBefore);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_19").Specific));
            this.Button1.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button1_ClickBefore);
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_4").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_5").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_6").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_8").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_9").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_12").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_14").Specific));
            this.ComboBox1 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_10").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_15").Specific));
            this.ComboBox2 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_16").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_18").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_20").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_21").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_22").Specific));
            this.OnCustomInitialize();

        }

        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Grid Grid0;

        private void OnCustomInitialize()
        {
        }
        public SAPbouiCOM.Form oForm;
        public SAPbouiCOM.DataTable dt;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;

        private void Button0_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                oForm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.ActiveForm;
                dt = oForm.DataSources.DataTables.Add("dt");
                dt.Clear();
            }
            catch (Exception)
            {
            }

            string voyage = EditText0.Value;
            string pol = EditText1.Value;
            string pod = EditText2.Value;
            string fullempty = ComboBox0.Value;
            string srv = ComboBox1.Value;
            string hblpol = EditText5.Value;
            string hblvoy = EditText6.Value;
            string status = ComboBox2.Value;

            string sql = "select DISTINCT  T0.\"DocEntry\"  as \"DocEntry\",  T1.\"VisOrder\"  ,U_BLAKKONNO as \" BL Akkon No \" ,T1.U_CONTAINERNO as \"Container No\", T1.U_SEALNO AS \"Seal No\" , T1.\"U_VGM\" AS \"VGM\",T1.\"U_FULLEMPTY\" AS \"FULLEMPTY\"  FROM ORDR T0 LEFT JOIN RDR1 T1 ON T1.\"DocEntry\" = T0.\"DocEntry\" WHERE T0.U_VOYAGE = '" + voyage + "'";

            if (pol != "")
            {
                sql += "AND T0.U_PORTOFLOADING = '" + pol + "'";
            }
            if (pod != "")
            {
                sql += "AND T0.U_PORTOFDISCHARGE = '" + pod + "'";
            }
            if (fullempty != "")
            {
                sql += "AND T1.U_FULLEMPTY = '" + fullempty + "'";
            }
            if (srv != "")
            {
                sql += "AND T0.\"U_SRV\" = '" + srv + "'";
            }
            if (hblpol != "")
            {
                sql += "AND T0.\"U_HBLPOL\" = '" + hblpol + "'";
            }
            if (hblvoy != "")
            {
                sql += "AND T0.\"U_HBLVoyage\" = '" + hblvoy + "'";
            }
            if (status !="")
            {
                sql += "AND T0.\"U_SealStatus\"='"+status+"'";
            }


            sql += "AND T0.\"DocStatus\" = 'O' AND T0.U_QUOT_APPROSTAT = 'Yes' AND T0.\"Confirmed\" = 'Y'  ORDER BY T1.U_CONTAINERNO  ASC  ";

            dt.ExecuteQuery(sql);
            Grid0.DataTable = dt;

            Grid0.Columns.Item(0).Editable = false;
            Grid0.Columns.Item(1).Editable = false;
            Grid0.Columns.Item(2).Editable = false;
            Grid0.Columns.Item(3).Editable = false;
            Grid0.Columns.Item(6).Editable = false;
            //Grid0.Columns.Item(7).Editable = false;
            Logger.Log( "Listeleme geldi");
        }
       
        private string CleanInput(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            // Sadece harf, rakam, boşluk, tire, nokta, alt çizgi vb. izin ver
            var regex = new System.Text.RegularExpressions.Regex(@"[^a-zA-Z0-9\s\-_.,]");
            return regex.Replace(input, "").Trim();
        }

        private void Button1_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            SAPbobsCOM.Company oCompany = Program.oCompany;

            if (Grid0.Rows.SelectedRows.Count > 0)
            {
                string cntNo = ""; 

                try
                {
                    int successCount = 0;
                    int failCount = 0;

                    for (int i = 0; i < Grid0.Rows.Count; i++) 
                    {
                        if (Grid0.Rows.IsSelected(i))
                        {
                            try
                            {
                                SAPbobsCOM.Documents oSalesOrder = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders); //sales order dökümanına bağlanıyor 
                                oSalesOrder.GetByKey(Convert.ToInt32(Grid0.DataTable.Columns.Item(0).Cells.Item(i).Value.ToString())); // docentrysine göre sales order bilgilerini getiriyor

                                cntNo = Grid0.DataTable.Columns.Item(3).Cells.Item(i).Value.ToString();

                                oSalesOrder.Lines.SetCurrentLine(Convert.ToInt32(Grid0.DataTable.Columns.Item(1).Cells.Item(i).Value));

                                //sales order satırlarını geziyor
                                oSalesOrder.Lines.UserFields.Fields.Item("U_SEALNO").Value = CleanInput(Grid0.DataTable.Columns.Item(4).Cells.Item(i).Value.ToString());  
                                oSalesOrder.Lines.UserFields.Fields.Item("U_VGM").Value = Convert.ToInt32(CleanInput(Grid0.DataTable.Columns.Item(5).Cells.Item(i).Value.ToString()));

                                int ret = oSalesOrder.Update();

                                if (ret != 0)
                                {
                                    string errMsg = oCompany.GetLastErrorDescription();
                                    
                                    Logger.Log($"{cntNo} için hata: {errMsg}");
                                    // Grid0.DataTable.SetValue(7, i, errMsg);
                                    if (oSalesOrder.GetByKey(Convert.ToInt32(Grid0.DataTable.Columns.Item(0).Cells.Item(i).Value)))
                                    {
                                        oSalesOrder.UserFields.Fields.Item("U_SealStatus").Value = "3";// "Failed";
                                        oSalesOrder.Update();
                                        failCount++;
                                    }

                                    Grid0.CommonSetting.SetRowBackColor(i + 1, 255);

                                    continue;
                                }
                                else
                                {
                                    SAPbobsCOM.Documents oUpdateDoc = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);
                                    if (oUpdateDoc.GetByKey(Convert.ToInt32(Grid0.DataTable.Columns.Item(0).Cells.Item(i).Value)))
                                    {
                                        oUpdateDoc.UserFields.Fields.Item("U_SealStatus").Value = "2";// "Success";
                                        oUpdateDoc.Update();
                                        successCount++;
                                    }
                                     
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.Log($"{cntNo} için beklenmeyen hata: {ex.Message}");
                                Grid0.CommonSetting.SetRowBackColor(i + 1, 255);

                                continue;
                            } 
                        } 
                    }

                    Application.SBO_Application.MessageBox(+Grid0.Rows.SelectedRows.Count + " BL - Seal No / Vgm Updated");
                    string voyage = EditText0.Value.Trim();
                    string pol = EditText1.Value.Trim();
                    string pod = EditText2.Value.Trim();

                    string sql = @"
                        SELECT DISTINCT  
                            T0.""DocEntry"" AS ""DocEntry"",  
                            T1.""VisOrder"",  
                            U_BLAKKONNO AS ""BL Akkon No"",  
                            T1.U_CONTAINERNO AS ""Container No"",  
                            T1.U_SEALNO AS ""Seal No"",  
                            T1.""U_VGM"" AS ""VGM""
                        FROM ORDR T0
                        LEFT JOIN RDR1 T1 ON T1.""DocEntry"" = T0.""DocEntry""
                        WHERE 
                            (@voyage = '' OR T0.U_VOYAGE = @voyage)
                            AND (@pol = '' OR T0.U_PORTOFLOADING = @pol)
                            AND (@pod = '' OR T0.U_PORTOFDISCHARGE = @pod)
                            AND T0.""DocStatus"" = 'O'
                            AND T0.U_QUOT_APPROSTAT = 'Yes'
                            AND T0.""Confirmed"" = 'Y'
                        ORDER BY T1.U_CONTAINERNO ASC
                        ";
                     
                    sql = sql.Replace("@voyage", "'" + voyage + "'")
                             .Replace("@pol", "'" + pol + "'")
                             .Replace("@pod", "'" + pod + "'");

                    dt.ExecuteQuery(sql);
                    Grid0.DataTable = dt;

                    #region gereksiz kod tekrarı vardı. yukardaki sorguyla değiştirildi.


                    //if (EditText0.Value != "" && EditText1.Value != "" && EditText2.Value != "")
                    //{
                    //    dt.ExecuteQuery("select DISTINCT  T0.\"DocEntry\"  as \"DocEntry\",  T1.\"VisOrder\"  ,U_BLAKKONNO as \" BL Akkon No \" ,T1.U_CONTAINERNO as \"Container No\", T1.U_SEALNO AS \"Seal No\" , T1.\"U_VGM\" AS \"VGM\"FROM ORDR T0 LEFT JOIN RDR1 T1 ON T1.\"DocEntry\" = T0.\"DocEntry\" WHERE T0.U_VOYAGE = '" + EditText0.Value.ToString() + "' AND T0.U_PORTOFLOADING = '" + EditText1.Value.ToString() + "' AND T0.U_PORTOFDISCHARGE = '" + EditText2.Value.ToString() + "' AND T0.\"DocStatus\" = 'O' AND T0.U_QUOT_APPROSTAT = 'Yes' AND T0.\"Confirmed\" = 'Y'  ORDER BY T1.U_CONTAINERNO  ASC  ");
                    //    Grid0.DataTable = dt;
                    //}
                    //else if (EditText0.Value != "" && EditText1.Value != "")
                    //{
                    //    dt.ExecuteQuery("select DISTINCT  T0.\"DocEntry\"  as \"DocEntry\",  T1.\"VisOrder\"  ,U_BLAKKONNO as \" BL Akkon No \" ,T1.U_CONTAINERNO as \"Container No\", T1.U_SEALNO AS \"Seal No\" , T1.\"U_VGM\" AS \"VGM\"FROM ORDR T0 LEFT JOIN RDR1 T1 ON T1.\"DocEntry\" = T0.\"DocEntry\" WHERE T0.U_VOYAGE = '" + EditText0.Value.ToString() + "' AND T0.U_PORTOFLOADING = '" + EditText1.Value.ToString() + "' AND T0.\"DocStatus\" = 'O' AND T0.\"Confirmed\" = 'Y'  AND T0.U_QUOT_APPROSTAT = 'Yes' ORDER BY T1.U_CONTAINERNO ASC  ");
                    //    Grid0.DataTable = dt;
                    //}
                    //else if (EditText0.Value != "" && EditText2.Value != "")
                    //{
                    //    dt.ExecuteQuery("select DISTINCT  T0.\"DocEntry\"  as \"DocEntry\",  T1.\"VisOrder\"  ,U_BLAKKONNO as \" BL Akkon No \" ,T1.U_CONTAINERNO as \"Container No\", T1.U_SEALNO AS \"Seal No\" , T1.\"U_VGM\" AS \"VGM\"FROM ORDR T0 LEFT JOIN RDR1 T1 ON T1.\"DocEntry\" = T0.\"DocEntry\" WHERE T0.U_VOYAGE = '" + EditText0.Value.ToString() + "' AND T0.U_PORTOFDISCHARGE = '" + EditText2.Value.ToString() + "' AND T0.\"DocStatus\" = 'O' AND T0.\"Confirmed\" = 'Y'  AND T0.U_QUOT_APPROSTAT = 'Yes' ORDER BY T1.U_CONTAINERNO ASC  ");
                    //    Grid0.DataTable = dt;
                    //}
                    #endregion
                   
                    this.Grid0.DataTable = dt;

                    Grid0.Columns.Item(0).Editable = false;
                    Grid0.Columns.Item(1).Editable = false;
                    Grid0.Columns.Item(2).Editable = false;
                    Grid0.Columns.Item(3).Editable = false;
                    EditText3.Value = successCount.ToString();
                    EditText4.Value = failCount.ToString();
                    // Grid0.Columns.Item(7).Editable = false;
                }
                catch (Exception ex)
                {
                    Application.SBO_Application.MessageBox("Error Code = 5002 - " + cntNo + ex.ToString());
                }
            }
            else
            {
                string containerNo = "";

                int successCount = 0;
                int failCount = 0;

                int totalRows = Grid0.Rows.Count;

                #region 20.11.2025 kod iyileştirmesi özge


                for (int i = 0; i <= Grid0.Rows.Count - 1; i++)
                {
                     
                    Application.SBO_Application.StatusBar.SetText((i + 1).ToString() + " / " + totalRows.ToString() + " - update in BL", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                     
                    string xx = Grid0.DataTable.Columns.Item(0).Cells.Item(i).Value.ToString();
                    // Sales order docentrye göre sales order satırlarını gezip seal güncelliyor
                    SAPbobsCOM.Documents oSalesOrder = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders); //sales order dökümanına bağlanıyor 
                    oSalesOrder.GetByKey(Convert.ToInt32(xx)); // docentrysine göre sales order bilgilerini getiriyor

                    oSalesOrder.Lines.SetCurrentLine(Convert.ToInt32(Grid0.DataTable.Columns.Item(1).Cells.Item(i).Value));
                 
                    //sales order satırlarını geziyor
                    oSalesOrder.Lines.UserFields.Fields.Item("U_SEALNO").Value = Grid0.DataTable.Columns.Item(4).Cells.Item(i).Value.ToString(); //seal no alanını güncelliyor.
                    oSalesOrder.Lines.UserFields.Fields.Item("U_VGM").Value = Convert.ToInt32(Grid0.DataTable.Columns.Item(5).Cells.Item(i).Value.ToString());

                    int ret = oSalesOrder.Update();

                    if (ret != 0)
                    {
                        // Application.SBO_Application.MessageBox(containerNo + "için: " + oCompany.GetLastErrorDescription().ToString());
                        string errMsg = oCompany.GetLastErrorDescription();
                        Logger.Log(containerNo + "için: " + errMsg.ToString());
                        Grid0.CommonSetting.SetRowBackColor(i + 1, 255);
                        //Grid0.DataTable.Columns.Item(7).Cells.Item(i).Value = errMsg.ToString();
                        if (oSalesOrder.GetByKey(Convert.ToInt32(Grid0.DataTable.Columns.Item(0).Cells.Item(i).Value)))
                        {
                            oSalesOrder.UserFields.Fields.Item("U_SealStatus").Value = "3";// "Failed";
                            oSalesOrder.Update();
                            failCount++;
                        }

                        Grid0.CommonSetting.SetRowBackColor(i + 1, 255);
                    
                        continue;
                    }
                    else
                    {
                        SAPbobsCOM.Documents oUpdateDoc = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);
                        if (oUpdateDoc.GetByKey(Convert.ToInt32(Grid0.DataTable.Columns.Item(0).Cells.Item(i).Value)))
                        {
                            oUpdateDoc.UserFields.Fields.Item("U_SealStatus").Value = "2";// "Success";
                            oUpdateDoc.Update();
                            successCount++;
                        }

                    }
             

                } 
                #endregion


                try
                {

                    #region gereksiz for döngüsü
                    //int successCount = 0;
                    //int failCount = 0;

                    //int batchSize = 60;
                    //int rowCount = Grid0.Rows.Count;
                    //int batchCount = (rowCount + batchSize - 1) / batchSize;

                    //for (int batchIndex = 0; batchIndex < batchCount; batchIndex++)
                    //{
                    //    for (int i = batchIndex * batchSize; i < Math.Min(rowCount, (batchIndex + 1) * batchSize); i++)
                    //    {
                    //        try
                    //        {
                    //            SAPbobsCOM.Documents oSalesOrder = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders); //sales order dökümanına bağlanıyor 
                    //            oSalesOrder.GetByKey(Convert.ToInt32(Grid0.DataTable.Columns.Item(0).Cells.Item(i).Value.ToString())); // docentrysine göre sales order bilgilerini getiriyor

                    //            containerNo = Grid0.DataTable.Columns.Item(3).Cells.Item(i).Value.ToString();

                    //            oSalesOrder.Lines.SetCurrentLine(Convert.ToInt32(Grid0.DataTable.Columns.Item(1).Cells.Item(i).Value));

                    //            //sales order satırlarını geziyor
                    //            oSalesOrder.Lines.UserFields.Fields.Item("U_SEALNO").Value = CleanInput(Grid0.DataTable.Columns.Item(4).Cells.Item(i).Value.ToString());
                    //            oSalesOrder.Lines.UserFields.Fields.Item("U_VGM").Value = Convert.ToInt32(CleanInput(Grid0.DataTable.Columns.Item(5).Cells.Item(i).Value.ToString()));


                    //            int ret = oSalesOrder.Update();

                    //            if (ret != 0)
                    //            {
                    //                // Application.SBO_Application.MessageBox(containerNo + "için: " + oCompany.GetLastErrorDescription().ToString());
                    //                string errMsg = oCompany.GetLastErrorDescription();
                    //                Logger.Log(containerNo + "için: " + errMsg.ToString());
                    //                Grid0.CommonSetting.SetRowBackColor(i + 1, 255);
                    //                //Grid0.DataTable.Columns.Item(7).Cells.Item(i).Value = errMsg.ToString();
                    //                if (oSalesOrder.GetByKey(Convert.ToInt32(Grid0.DataTable.Columns.Item(0).Cells.Item(i).Value)))
                    //                {
                    //                    oSalesOrder.UserFields.Fields.Item("U_SealStatus").Value = "3";// "Failed";
                    //                    oSalesOrder.Update();
                    //                    failCount++;
                    //                }

                    //                Grid0.CommonSetting.SetRowBackColor(i + 1, 255);

                    //                continue;
                    //            }
                    //            else
                    //            {
                    //                SAPbobsCOM.Documents oUpdateDoc = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);
                    //                if (oUpdateDoc.GetByKey(Convert.ToInt32(Grid0.DataTable.Columns.Item(0).Cells.Item(i).Value)))
                    //                {
                    //                    oUpdateDoc.UserFields.Fields.Item("U_SealStatus").Value = "2";// "Success";
                    //                    oUpdateDoc.Update();
                    //                    successCount++;
                    //                }

                    //            }
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            Logger.Log($"{containerNo} için beklenmeyen hata: {ex.Message}");
                    //            Grid0.CommonSetting.SetRowBackColor(i + 1, 255);

                    //            continue;
                    //        }

                    //    }
                    //} 
                    #endregion

                    //Application.SBO_Application.MessageBox(+Grid0.Rows.Count + " BL - Seal No / Vgm Updated");

                    string voyage = EditText0.Value.Trim();
                    string pol = EditText1.Value.Trim();
                    string pod = EditText2.Value.Trim();

                    string sql = @"
                        SELECT DISTINCT  
                            T0.""DocEntry"" AS ""DocEntry"",  
                            T1.""VisOrder"",  
                            U_BLAKKONNO AS ""BL Akkon No"",  
                            T1.U_CONTAINERNO AS ""Container No"",  
                            T1.U_SEALNO AS ""Seal No"",  
                            T1.""U_VGM"" AS ""VGM""
                        FROM ORDR T0
                        LEFT JOIN RDR1 T1 ON T1.""DocEntry"" = T0.""DocEntry""
                        WHERE 
                            (@voyage = '' OR T0.U_VOYAGE = @voyage)
                            AND (@pol = '' OR T0.U_PORTOFLOADING = @pol)
                            AND (@pod = '' OR T0.U_PORTOFDISCHARGE = @pod)
                            AND T0.""DocStatus"" = 'O'
                            AND T0.U_QUOT_APPROSTAT = 'Yes'
                            AND T0.""Confirmed"" = 'Y'
                        ORDER BY T1.U_CONTAINERNO ASC
                        ";

                    sql = sql.Replace("@voyage", "'" + voyage + "'")
                             .Replace("@pol", "'" + pol + "'")
                             .Replace("@pod", "'" + pod + "'");

                    dt.ExecuteQuery(sql);
                    Grid0.DataTable = dt;
                     
                    #region Sorgu sadeleştirildi.
                     
                    //if (EditText0.Value != "" && EditText1.Value != "" && EditText2.Value != "")
                    //{
                    //    dt.ExecuteQuery("select DISTINCT  T0.\"DocEntry\"  as \"DocEntry\",  T1.\"VisOrder\"  ,U_BLAKKONNO as \" BL Akkon No \" ,T1.U_CONTAINERNO as \"Container No\", T1.U_SEALNO AS \"Seal No\" , T1.\"U_VGM\" AS \"VGM\"FROM ORDR T0 LEFT JOIN RDR1 T1 ON T1.\"DocEntry\" = T0.\"DocEntry\" WHERE T0.U_VOYAGE = '" + EditText0.Value.ToString() + "' AND T0.U_PORTOFLOADING = '" + EditText1.Value.ToString() + "' AND T0.U_PORTOFDISCHARGE = '" + EditText2.Value.ToString() + "' AND T0.\"DocStatus\" = 'O' AND T0.U_QUOT_APPROSTAT = 'Yes' AND T0.\"Confirmed\" = 'Y'  ORDER BY T1.U_CONTAINERNO  ASC  ");
                    //    Grid0.DataTable = dt;
                    //}
                    //else if (EditText0.Value != "" && EditText1.Value != "")
                    //{
                    //    dt.ExecuteQuery("select DISTINCT  T0.\"DocEntry\"  as \"DocEntry\",  T1.\"VisOrder\"  ,U_BLAKKONNO as \" BL Akkon No \" ,T1.U_CONTAINERNO as \"Container No\", T1.U_SEALNO AS \"Seal No\" , T1.\"U_VGM\" AS \"VGM\"FROM ORDR T0 LEFT JOIN RDR1 T1 ON T1.\"DocEntry\" = T0.\"DocEntry\" WHERE T0.U_VOYAGE = '" + EditText0.Value.ToString() + "' AND T0.U_PORTOFLOADING = '" + EditText1.Value.ToString() + "' AND T0.\"DocStatus\" = 'O' AND T0.\"Confirmed\" = 'Y'  AND T0.U_QUOT_APPROSTAT = 'Yes' ORDER BY T1.U_CONTAINERNO ASC  ");
                    //    Grid0.DataTable = dt;
                    //}
                    //else if (EditText0.Value != "" && EditText2.Value != "")
                    //{
                    //    dt.ExecuteQuery("select DISTINCT  T0.\"DocEntry\"  as \"DocEntry\",  T1.\"VisOrder\"  ,U_BLAKKONNO as \" BL Akkon No \" ,T1.U_CONTAINERNO as \"Container No\", T1.U_SEALNO AS \"Seal No\" , T1.\"U_VGM\" AS \"VGM\"FROM ORDR T0 LEFT JOIN RDR1 T1 ON T1.\"DocEntry\" = T0.\"DocEntry\" WHERE T0.U_VOYAGE = '" + EditText0.Value.ToString() + "' AND T0.U_PORTOFDISCHARGE = '" + EditText2.Value.ToString() + "' AND T0.\"DocStatus\" = 'O' AND T0.\"Confirmed\" = 'Y'  AND T0.U_QUOT_APPROSTAT = 'Yes' ORDER BY T1.U_CONTAINERNO ASC  ");
                    //    Grid0.DataTable = dt;
                    //}
                    #endregion
                    this.Grid0.DataTable = dt;

                    Grid0.Columns.Item(0).Editable = false;
                    Grid0.Columns.Item(1).Editable = false;
                    Grid0.Columns.Item(2).Editable = false;
                    Grid0.Columns.Item(3).Editable = false;
                    EditText3.Value = successCount.ToString();
                    EditText4.Value = failCount.ToString();
                    // Grid0.Columns.Item(7).Editable = false;
                }
                catch (Exception ex)
                {
                    Application.SBO_Application.MessageBox("Error Code = 5001 - " + containerNo + ex.ToString());
                }

               
            }
        }
         
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.EditText EditText6;
        private SAPbouiCOM.ComboBox ComboBox1;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.ComboBox ComboBox2;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.EditText EditText4;
    }
}