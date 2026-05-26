using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Seal_Update
{
    [FormAttribute("Seal_Update.Form2", "Form2.b1f")]
    class Form2 : UserFormBase
    {
        public Form2()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.StaticText10 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_23").Specific));
            this.StaticText11 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_24").Specific));
            this.StaticText12 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_25").Specific));
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("Item_29").Specific));
            this.Button2.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button2_ClickBefore);
            this.Button3 = ((SAPbouiCOM.Button)(this.GetItem("Item_30").Specific));
            this.Button3.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button3_ClickBefore);
            this.StaticText13 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_31").Specific));
            this.StaticText14 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_33").Specific));
            this.StaticText15 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_35").Specific));
            this.StaticText16 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_37").Specific));
            this.StaticText17 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_39").Specific));
            this.StaticText18 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_41").Specific));
            this.EditText12 = ((SAPbouiCOM.EditText)(this.GetItem("Item_42").Specific));
            this.StaticText19 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_43").Specific));
            this.EditText13 = ((SAPbouiCOM.EditText)(this.GetItem("Item_44").Specific));
            this.EditText21 = ((SAPbouiCOM.EditText)(this.GetItem("Item_67").Specific));
            this.EditText22 = ((SAPbouiCOM.EditText)(this.GetItem("Item_68").Specific));
            this.EditText23 = ((SAPbouiCOM.EditText)(this.GetItem("Item_69").Specific));
            this.EditText24 = ((SAPbouiCOM.EditText)(this.GetItem("Item_70").Specific));
            this.EditText25 = ((SAPbouiCOM.EditText)(this.GetItem("Item_71").Specific));
            this.ComboBox9 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_73").Specific));
            this.ComboBox10 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_74").Specific));
            this.ComboBox11 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_75").Specific));
            this.CheckBox1 = ((SAPbouiCOM.CheckBox)(this.GetItem("Item_77").Specific));
            this.CheckBox1.ClickAfter += new SAPbouiCOM._ICheckBoxEvents_ClickAfterEventHandler(this.CheckBox1_ClickAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.LoadAfter += new LoadAfterHandler(this.Form_LoadAfter);

        }

        private SAPbouiCOM.Grid Grid0;

        private void OnCustomInitialize()
        {
            SAPbouiCOM.Form oFormMenu = SAPbouiCOM.Framework.Application.SBO_Application.Forms.GetForm("169", 0);

            this.UIAPIRawForm.Left = oFormMenu.Left + oFormMenu.Width + 20;

        }

        public SAPbouiCOM.Form oForm;
        public SAPbouiCOM.DataTable dt;
        private SAPbouiCOM.StaticText StaticText10;
        private SAPbouiCOM.StaticText StaticText11;
        private SAPbouiCOM.StaticText StaticText12;
        private SAPbouiCOM.Button Button2;
        private SAPbouiCOM.Button Button3;
        private SAPbouiCOM.StaticText StaticText13;
        private SAPbouiCOM.StaticText StaticText14;
        private SAPbouiCOM.StaticText StaticText15;
        private SAPbouiCOM.StaticText StaticText16;
        private SAPbouiCOM.StaticText StaticText17;
        private SAPbouiCOM.StaticText StaticText18;
        private SAPbouiCOM.EditText EditText12;
        private SAPbouiCOM.StaticText StaticText19;
        private SAPbouiCOM.EditText EditText13;

        private void Form_LoadAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            //throw new System.NotImplementedException();

        }

        private SAPbouiCOM.EditText EditText21;
        private SAPbouiCOM.EditText EditText22;
        private SAPbouiCOM.EditText EditText23;
        private SAPbouiCOM.EditText EditText24;
        private SAPbouiCOM.EditText EditText25;
        private SAPbouiCOM.ComboBox ComboBox9;
        private SAPbouiCOM.ComboBox ComboBox10;
        private SAPbouiCOM.ComboBox ComboBox11;

        SAPbobsCOM.Company oCompany = Program.oCompany;

        private void Button2_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            oForm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.ActiveForm;
            oForm.Freeze(true);
            try
            {
                try
                {
                    dt = oForm.DataSources.DataTables.Item("dt");
                    dt.Clear();
                }
                catch
                {
                    dt = oForm.DataSources.DataTables.Add("dt");
                    dt.Clear();
                }

                string voyage = EditText21.Value;
                string pol = EditText22.Value;
                string pod = EditText23.Value;
                string fullempty = ComboBox10.Value;
                string srv = ComboBox9.Value;
                string hblpol = EditText24.Value;
                string hblvoy = EditText25.Value;
                string status = ComboBox11.Value;

                string sql = "select DISTINCT CAST('N' AS NVARCHAR(1)) AS \"Select\",  T0.\"DocEntry\"  as \"DocEntry\",  T1.\"VisOrder\"  ,U_BLAKKONNO as \" BL Akkon No \" ,T1.U_CONTAINERNO as \"Container No\", T1.U_SEALNO AS \"Seal No\" , T1.\"U_VGM\" AS \"VGM\", CAST('' AS NVARCHAR(254)) AS \"Update Seal No\", CASE WHEN CAST(0 AS INTEGER)=0 then Null else CAST(0 AS INTEGER) END  AS \"Update VGM\",T1.\"U_FULLEMPTY\" AS \"FULLEMPTY\", " + "CAST('' AS NVARCHAR(500)) AS \"Status Message\" " + "FROM ORDR T0 LEFT JOIN RDR1 T1 ON T1.\"DocEntry\" = T0.\"DocEntry\" WHERE T0.U_VOYAGE = '" + voyage + "' ";

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
                if (status != "")
                {
                    sql += "AND T0.\"U_SealStatus\"='" + status + "'";
                }


                sql += "AND T0.\"DocStatus\" = 'O' AND T0.U_QUOT_APPROSTAT = 'Yes' AND T0.\"Confirmed\" = 'Y'  ORDER BY T1.U_CONTAINERNO  ASC";

                dt.ExecuteQuery(sql);

                Grid0.DataTable = dt;
                Grid0.Columns.Item("Select").Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;

                SAPbouiCOM.CheckBoxColumn oCheckCol;

                oCheckCol = (SAPbouiCOM.CheckBoxColumn)Grid0.Columns.Item("Select");

                oCheckCol.Editable = true;

                Grid0.Columns.Item("DocEntry").Editable = false;
                Grid0.Columns.Item("VisOrder").Editable = false;
                Grid0.Columns.Item("BL Akkon No").Editable = false;
                Grid0.Columns.Item("Container No").Editable = false;
                Grid0.Columns.Item("FULLEMPTY").Editable = false;
                Grid0.Columns.Item("Status Message").Editable = false;
                Grid0.Columns.Item("Seal No").Editable = false;
                Grid0.Columns.Item("VGM").Editable = false;
                Grid0.Columns.Item("Status Message").TitleObject.Sortable = true;
                // Logger.Log("Listeleme geldi");
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.Message);
                return;
            }
            finally
            {
                oForm.Freeze(false);
            }
        }

        private string CleanInput(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            // Sadece harf, rakam, boşluk, tire, nokta, alt çizgi vb. izin ver
            var regex = new System.Text.RegularExpressions.Regex(@"[^a-zA-Z0-9\s\-_.,]");
            return regex.Replace(input, "").Trim();
        }


        private void Button3_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
             

            SAPbouiCOM.DataTable dt = oForm.DataSources.DataTables.Item("dt");

            int successCount = 0;
            int failCount = 0;
            Dictionary<string, string> errorMessages = new Dictionary<string, string>();
            List<string> successKeys = new List<string>();
            int redColor = Color.Red.R | (Color.Red.G << 8) | (Color.Red.B << 16);

            // XML olarak al
            string xmlData = dt.SerializeAsXML(SAPbouiCOM.BoDataTableXmlSelect.dxs_DataOnly);

            // XML parse et
            XDocument xDoc = XDocument.Parse(xmlData);

            // Choose alanı YES olanları çek
            var selectedRows = xDoc.Descendants("Row")
             .Where(row =>
             {
                 // "Select" kolonunu bul
                 var selectCell = row.Descendants("Cell")
                                      .FirstOrDefault(c =>
                                          (string)c.Element("ColumnUid") == "Select");

                 return selectCell != null &&
                        (string)selectCell.Element("Value") == "Y";
             })
             .ToList();
          

            int currentIndex = 0;
           

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string selected = dt.GetValue("Select", i).ToString();
                if (selected != "1" && selected != "Y") continue;

                currentIndex++;

                string cntNo = dt.GetValue("Container No", i).ToString();
                string key = "";

               
                try
                {
                    Application.SBO_Application.StatusBar.SetText($"{currentIndex} / {selectedRows.Count} processing...[{cntNo}]", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                    this.UIAPIRawForm.Freeze(true);

                    int docEntry = Convert.ToInt32(dt.GetValue("DocEntry", i).ToString());
                    int visOrder = Convert.ToInt32(dt.GetValue("VisOrder", i).ToString());
                    string sealNo = CleanInput(dt.GetValue("Update Seal No", i).ToString());
                    string vgmStr = dt.GetValue("Update VGM", i).ToString();
                    key = docEntry + "_" + cntNo;

                    SAPbobsCOM.Documents oSalesOrder = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);

                    if (!oSalesOrder.GetByKey(docEntry))
                    {
                        dt.SetValue("Status Message", i, "No record found.");
                        Grid0.CommonSetting.SetCellFontColor(i + 1, 11, redColor);
                        failCount++;
                        continue;
                    }
                    else
                    {
                        Grid0.CommonSetting.SetCellFontColor(i + 1, 11, 0);
                    }
                    if (vgmStr.Length > 6)
                    {

                        dt.SetValue("Status Message", i, "✘ The VGM must be a maximum of 6 characters.");
                        failCount++;
                        Grid0.CommonSetting.SetCellFontColor(i + 1, 11, redColor);
                        continue;
                    }
                    else
                    {
                        Grid0.CommonSetting.SetCellFontColor(i + 1, 11, 0);
                    }
                    oSalesOrder.Lines.SetCurrentLine(visOrder);
                    oSalesOrder.Lines.UserFields.Fields.Item("U_SEALNO").Value = sealNo;
                    oSalesOrder.Lines.UserFields.Fields.Item("U_VGM").Value = vgmStr;

                    int ret = oSalesOrder.Update();

                    if (ret == 0)
                    {
                        SAPbobsCOM.Documents oUpdateDoc = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);
                        if (oUpdateDoc.GetByKey(docEntry))
                        {
                            oUpdateDoc.UserFields.Fields.Item("U_SealStatus").Value = "2";
                            oUpdateDoc.Update();
                            successCount++;
                            successKeys.Add(key);
                        }
                        Grid0.CommonSetting.SetCellFontColor(i + 1, 11, 0);
                        dt.SetValue("Status Message", i, "Success");

                    }
                    else
                    {
                        string errMsg = oCompany.GetLastErrorDescription();
                        //Logger.Log($"{cntNo} için hata: {errMsg}");

                        errorMessages[key] = errMsg;

                        dt.SetValue("Status Message", i, errMsg);
                        Grid0.CommonSetting.SetCellFontColor(i + 1, 11, redColor);

                        // U_SealStatus = 3 (Failed)
                        SAPbobsCOM.Documents oFailDoc = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);
                        if (oFailDoc.GetByKey(docEntry))
                        {
                            oFailDoc.UserFields.Fields.Item("U_SealStatus").Value = "3";
                            oFailDoc.Update();
                        }
                        failCount++;

                    }
                }
                catch (Exception ex)
                {
                    dt.SetValue("Status Message", i, "✘ Exception: " + ex.Message);
                    Grid0.CommonSetting.SetCellFontColor(i + 1, 11, redColor);
                    failCount++;
                }
                finally
                {
                    this.UIAPIRawForm.Freeze(false);
                }

            }

            SAPbouiCOM.EditText txtSuccess = (SAPbouiCOM.EditText)oForm.Items.Item("Item_42").Specific;
            txtSuccess.Value = successCount.ToString();

            SAPbouiCOM.EditText txtFail = (SAPbouiCOM.EditText)oForm.Items.Item("Item_44").Specific;
            txtFail.Value = failCount.ToString();

            Grid0.AutoResizeColumns();
        }
        private SAPbouiCOM.CheckBox CheckBox1;

        private bool _allSelected = false;

        private void CheckBox1_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            _allSelected = !_allSelected;
            string value = _allSelected ? "Y" : "N";
            Application.SBO_Application.StatusBar.SetText(_allSelected ? "All rows are selected..." : "The elections are being canceled...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
             
            oForm.Freeze(true);
            try
            {
                SAPbouiCOM.DataTable dtb = Grid0.DataTable;

                Enumerable.Range(0, dtb.Rows.Count)
                    .ToList()
                    .ForEach(i => dtb.SetValue("Select", i, value));
               
            }
            finally
            {
                oForm.Freeze(false);

                Application.SBO_Application.StatusBar.SetText(_allSelected ? "All lines have been selected." : "The elections have been canceled.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }

        }
    }
}
