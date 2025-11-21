using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework; 

namespace Batch_Update
{
    class TableCreate
    {
        public static long v_RetVal;
        public static int v_ErrCode;
        public static string v_ErrMsg = "";


        public static bool CreateTable(string TableName, string TableDesc, SAPbobsCOM.BoUTBTableType TableType)
        {
            bool functionReturnValue = false;
            functionReturnValue = false;

            try
            {
                if (!TableCreate.TableExists(TableName))
                {
                    SAPbobsCOM.UserTablesMD v_UserTableMD = default(SAPbobsCOM.UserTablesMD);
                    Application.SBO_Application.StatusBar.SetText("Creating Table " + TableName + " ...................", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                    v_UserTableMD = (SAPbobsCOM.UserTablesMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
                    v_UserTableMD.TableName = TableName;
                    v_UserTableMD.TableDescription = TableDesc;
                    v_UserTableMD.TableType = TableType;
                    v_RetVal = v_UserTableMD.Add();
                    if (v_RetVal != 0)
                    {
                        Program.oCompany.GetLastError(out v_ErrCode, out v_ErrMsg);
                        Application.SBO_Application.StatusBar.SetText("Failed to Create Table " + TableDesc + v_ErrCode + " " + v_ErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserTableMD);
                        v_UserTableMD = null;
                        return false;
                    }
                    else
                    {
                        Application.SBO_Application.StatusBar.SetText("[" + TableName + "] - " + TableDesc + " Created Successfully!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserTableMD);
                        v_UserTableMD = null;
                        return true;
                    }
                }
                else
                {
                    GC.Collect();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText("" + ":> " + ex.Message + " @ " + ex.Source);
            }
            return functionReturnValue;
        }

        public static bool ColumnExists(string TableName, string FieldID)
        {
            try
            {
                SAPbobsCOM.Recordset rs = (SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                bool oFlag = true;
                rs.DoQuery("Select 1 from \"CUFD\" Where \"TableID\"='" + TableName.Trim() + "' and \"AliasID\"='" + FieldID.Trim() + "'");
                if (rs.EoF)
                    oFlag = false;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(rs);
                rs = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return oFlag;
            }
            catch (Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText(ex.Message);
            }
            return true;
        }




        public static bool TableExists(string TableName)
        {
            SAPbobsCOM.UserTablesMD oTables = default(SAPbobsCOM.UserTablesMD);
            bool oFlag = false;
            oTables = (SAPbobsCOM.UserTablesMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
            oFlag = oTables.GetByKey(TableName);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oTables);
            return oFlag;
        }


        public static bool CreateUserFields(string TableName, string FieldName, string FieldDescription, SAPbobsCOM.BoFieldTypes type, long size = 0, SAPbobsCOM.BoFldSubTypes subType = SAPbobsCOM.BoFldSubTypes.st_None, string LinkedTable = "", string DefaultValue = "", List<ComboList> combolist = null)
        {
            try
            {
                if (TableName.StartsWith("@") == true)
                {
                    if (!TableCreate.ColumnExists(TableName, FieldName))
                    {
                        SAPbobsCOM.UserFieldsMD v_UserField = default(SAPbobsCOM.UserFieldsMD);
                        v_UserField = (SAPbobsCOM.UserFieldsMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);

                        v_UserField.TableName = TableName;
                        v_UserField.Name = FieldName;
                        v_UserField.Description = FieldDescription;
                        v_UserField.Type = type;
                        if (type != SAPbobsCOM.BoFieldTypes.db_Date)
                        {
                            if (size != 0)
                            {
                                if (type == SAPbobsCOM.BoFieldTypes.db_Numeric)
                                {
                                    v_UserField.EditSize = 11;
                                }
                                else
                                {
                                    v_UserField.Size = (int)size;
                                }


                            }
                        }
                        if (subType != SAPbobsCOM.BoFldSubTypes.st_None)
                        {
                            v_UserField.SubType = subType;
                        }
                        if (!string.IsNullOrEmpty(LinkedTable))
                            v_UserField.LinkedTable = LinkedTable;
                        if (!string.IsNullOrEmpty(DefaultValue))
                            v_UserField.DefaultValue = DefaultValue;


                        if (combolist != null)
                        {
                            foreach (var item in combolist)
                            {
                                v_UserField.ValidValues.Value = item.Value;
                                v_UserField.ValidValues.Description = item.Description;
                                v_UserField.ValidValues.Add();
                            }
                        }


                        v_RetVal = v_UserField.Add();
                        if (v_RetVal != 0)
                        {
                            Program.oCompany.GetLastError(out v_ErrCode, out v_ErrMsg);
                            Application.SBO_Application.StatusBar.SetText("Failed to add UserField masterid" + v_ErrCode + " " + v_ErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField);
                            v_UserField = null;
                            return false;
                        }
                        else
                        {
                            Application.SBO_Application.StatusBar.SetText("[" + TableName + "] - " + FieldDescription + " added successfully!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField);
                            v_UserField = null;
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                if (TableName.StartsWith("@") == false)
                {
                    if (!TableCreate.UDFExists(TableName, FieldName))
                    {
                        SAPbobsCOM.UserFieldsMD v_UserField = (SAPbobsCOM.UserFieldsMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                        v_UserField.TableName = TableName;
                        v_UserField.Name = FieldName;
                        v_UserField.Description = FieldDescription;
                        v_UserField.Type = type;
                        if (type != SAPbobsCOM.BoFieldTypes.db_Date)
                        {
                            if (size != 0)
                            {
                                if (type == SAPbobsCOM.BoFieldTypes.db_Numeric)
                                {
                                    v_UserField.EditSize = 11;
                                }
                                else
                                {
                                    v_UserField.Size = (int)size;
                                }

                            }
                        }
                        if (subType != SAPbobsCOM.BoFldSubTypes.st_None)
                        {
                            v_UserField.SubType = subType;
                        }

                        //#region Geçerli Değerler

                        if (combolist != null)
                        {
                            foreach (var item in combolist)
                            {
                                v_UserField.ValidValues.Value = item.Value;
                                v_UserField.ValidValues.Description = item.Description;
                                v_UserField.ValidValues.Add();
                            }
                        }

                        if (!string.IsNullOrEmpty(LinkedTable))
                            v_UserField.LinkedTable = LinkedTable;
                        if (!string.IsNullOrEmpty(DefaultValue))
                            v_UserField.DefaultValue = DefaultValue;

                        v_RetVal = v_UserField.Add();
                        if (v_RetVal != 0)
                        {
                            Program.oCompany.GetLastError(out v_ErrCode, out v_ErrMsg);
                            Application.SBO_Application.StatusBar.SetText("Failed to add UserField " + FieldDescription + " - " + v_ErrCode + " " + v_ErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField);
                            v_UserField = null;
                            return false;
                        }
                        else
                        {
                            Application.SBO_Application.StatusBar.SetText(" & TableName & - " + FieldDescription + " added successfully!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField);
                            v_UserField = null;
                            return true;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
            return true;
        }

        public static bool CreateUserFields2(string TableName, string FieldName, string FieldDescription, SAPbobsCOM.BoFieldTypes type, long size = 0, SAPbobsCOM.BoFldSubTypes subType = SAPbobsCOM.BoFldSubTypes.st_None, string LinkedTable = "", string DefaultValue = "", List<ComboList> combolist = null)
        {
            try
            {
                if (TableName.StartsWith("@") == true)
                {
                    if (!TableCreate.ColumnExists(TableName, FieldName))
                    {
                        SAPbobsCOM.UserFieldsMD v_UserField = default(SAPbobsCOM.UserFieldsMD);
                        v_UserField = null;
                        v_UserField = (SAPbobsCOM.UserFieldsMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                        v_UserField.TableName = TableName;
                        v_UserField.Name = FieldName;
                        v_UserField.Description = FieldDescription;
                        v_UserField.Type = type;
                        //v_UserField.Mandatory = SAPbobsCOM.BoYesNoEnum.tYES;
                        if (type != SAPbobsCOM.BoFieldTypes.db_Date)
                        {
                            if (size != 0)
                            {
                                if (type == SAPbobsCOM.BoFieldTypes.db_Numeric)
                                {
                                    v_UserField.EditSize = 11;
                                }
                                else
                                {
                                    v_UserField.Size = (int)size;
                                }


                            }
                        }
                        if (subType != SAPbobsCOM.BoFldSubTypes.st_None)
                        {
                            v_UserField.SubType = subType;
                        }
                        if (!string.IsNullOrEmpty(LinkedTable))
                            v_UserField.LinkedTable = LinkedTable;
                        if (!string.IsNullOrEmpty(DefaultValue))
                            v_UserField.DefaultValue = DefaultValue;


                        if (combolist != null)
                        {
                            foreach (var item in combolist)
                            {
                                v_UserField.ValidValues.Value = item.Value;
                                v_UserField.ValidValues.Description = item.Description;
                                v_UserField.ValidValues.Add();
                            }
                        }

                        v_RetVal = v_UserField.Add();
                        if (v_RetVal != 0)
                        {
                            Program.oCompany.GetLastError(out v_ErrCode, out v_ErrMsg);
                            Application.SBO_Application.StatusBar.SetText("Failed to add UserField masterid" + v_ErrCode + " " + v_ErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField);
                            v_UserField = null;
                            return false;
                        }
                        else
                        {
                            Application.SBO_Application.StatusBar.SetText("[" + TableName + "] - " + FieldDescription + " added successfully!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField);
                            v_UserField = null;
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                if (TableName.StartsWith("@") == false)
                {
                    if (!TableCreate.UDFExists(TableName, FieldName))
                    {
                        SAPbobsCOM.UserFieldsMD v_UserField = (SAPbobsCOM.UserFieldsMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                        v_UserField.TableName = TableName;
                        v_UserField.Name = FieldName;
                        v_UserField.Description = FieldDescription;
                        v_UserField.Type = type;
                        if (type != SAPbobsCOM.BoFieldTypes.db_Date)
                        {
                            if (size != 0)
                            {
                                if (type == SAPbobsCOM.BoFieldTypes.db_Numeric)
                                {
                                    v_UserField.EditSize = 11;
                                }
                                else
                                {
                                    v_UserField.Size = (int)size;
                                }

                            }
                        }
                        if (subType != SAPbobsCOM.BoFldSubTypes.st_None)
                        {
                            v_UserField.SubType = subType;
                        }
                        if (!string.IsNullOrEmpty(LinkedTable))
                            v_UserField.LinkedTable = LinkedTable;
                        v_RetVal = v_UserField.Add();
                        if (v_RetVal != 0)
                        {
                            Program.oCompany.GetLastError(out v_ErrCode, out v_ErrMsg);
                            Application.SBO_Application.StatusBar.SetText("Failed to add UserField " + FieldDescription + " - " + v_ErrCode + " " + v_ErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField);
                            v_UserField = null;
                            return false;
                        }
                        else
                        {
                            Application.SBO_Application.StatusBar.SetText(" & TableName & - " + FieldDescription + " added successfully!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField);
                            v_UserField = null;
                            return true;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.Message);
            }
            return true;
        }
        public static bool CreateUserFields3(string TableName, string FieldName, string FieldDescription, SAPbobsCOM.BoFieldTypes type, long size = 0, SAPbobsCOM.BoFldSubTypes subType = SAPbobsCOM.BoFldSubTypes.st_None, string LinkedTable = "", string DefaultValue = "", List<ComboList> combolist = null)
        {
            try
            {
                if (TableName.StartsWith("@") == true)
                {
                    if (!TableCreate.ColumnExists(TableName, FieldName))
                    {
                        SAPbobsCOM.UserFieldsMD v_UserField2 = default(SAPbobsCOM.UserFieldsMD);
                        v_UserField2 = (SAPbobsCOM.UserFieldsMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);

                        v_UserField2.TableName = TableName;
                        v_UserField2.Name = FieldName;
                        v_UserField2.Description = FieldDescription;
                        v_UserField2.Type = type;
                        if (type != SAPbobsCOM.BoFieldTypes.db_Date)
                        {
                            if (size != 0)
                            {
                                if (type == SAPbobsCOM.BoFieldTypes.db_Numeric)
                                {
                                    v_UserField2.EditSize = 11;
                                }
                                else
                                {
                                    v_UserField2.Size = (int)size;
                                }


                            }
                        }
                        if (subType != SAPbobsCOM.BoFldSubTypes.st_None)
                        {
                            v_UserField2.SubType = subType;
                        }
                        if (!string.IsNullOrEmpty(LinkedTable))
                            v_UserField2.LinkedTable = LinkedTable;
                        if (!string.IsNullOrEmpty(DefaultValue))
                            v_UserField2.DefaultValue = DefaultValue;


                        if (combolist != null)
                        {
                            foreach (var item in combolist)
                            {
                                v_UserField2.ValidValues.Value = item.Value;
                                v_UserField2.ValidValues.Description = item.Description;
                                v_UserField2.ValidValues.Add();
                            }
                        }




                        v_RetVal = v_UserField2.Add();
                        if (v_RetVal != 0)
                        {
                            Program.oCompany.GetLastError(out v_ErrCode, out v_ErrMsg);
                            Application.SBO_Application.StatusBar.SetText("Failed to add UserField masterid" + v_ErrCode + " " + v_ErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField2);
                            v_UserField2 = null;
                            return false;
                        }
                        else
                        {
                            Application.SBO_Application.StatusBar.SetText("[" + TableName + "] - " + FieldDescription + " added successfully!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField2);
                            v_UserField2 = null;
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                if (TableName.StartsWith("@") == false)
                {
                    if (!TableCreate.UDFExists(TableName, FieldName))
                    {
                        SAPbobsCOM.UserFieldsMD v_UserField3 = (SAPbobsCOM.UserFieldsMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                        v_UserField3.TableName = TableName;
                        v_UserField3.Name = FieldName;
                        v_UserField3.Description = FieldDescription;
                        v_UserField3.Type = type;
                        if (type != SAPbobsCOM.BoFieldTypes.db_Date)
                        {
                            if (size != 0)
                            {
                                if (type == SAPbobsCOM.BoFieldTypes.db_Numeric)
                                {
                                    v_UserField3.EditSize = 11;
                                }
                                else
                                {
                                    v_UserField3.Size = (int)size;
                                }

                            }
                        }
                        if (subType != SAPbobsCOM.BoFldSubTypes.st_None)
                        {
                            v_UserField3.SubType = subType;
                        }

                        if (!string.IsNullOrEmpty(LinkedTable))
                            v_UserField3.LinkedTable = LinkedTable;
                        v_RetVal = v_UserField3.Add();
                        if (v_RetVal != 0)
                        {
                            Program.oCompany.GetLastError(out v_ErrCode, out v_ErrMsg);
                            Application.SBO_Application.StatusBar.SetText("Failed to add UserField " + FieldDescription + " - " + v_ErrCode + " " + v_ErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField3);
                            v_UserField3 = null;
                            return false;
                        }
                        else
                        {
                            Application.SBO_Application.StatusBar.SetText(" & TableName & - " + FieldDescription + " added successfully!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField3);
                            v_UserField3 = null;
                            return true;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
            return true;
        }
        public static bool CreateUserFields4(string TableName, string FieldName, string FieldDescription, SAPbobsCOM.BoFieldTypes type, long size = 0, SAPbobsCOM.BoFldSubTypes subType = SAPbobsCOM.BoFldSubTypes.st_None, string LinkedTable = "", string DefaultValue = "", List<ComboList> combolist = null)
        {
            try
            {
                if (TableName.StartsWith("@") == true)
                {
                    if (!TableCreate.ColumnExists(TableName, FieldName))
                    {
                        SAPbobsCOM.UserFieldsMD v_UserField3 = default(SAPbobsCOM.UserFieldsMD);
                        v_UserField3 = (SAPbobsCOM.UserFieldsMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);

                        v_UserField3.TableName = TableName;
                        v_UserField3.Name = FieldName;
                        v_UserField3.Description = FieldDescription;
                        v_UserField3.Type = type;
                        if (type != SAPbobsCOM.BoFieldTypes.db_Date)
                        {
                            if (size != 0)
                            {
                                if (type == SAPbobsCOM.BoFieldTypes.db_Numeric)
                                {
                                    v_UserField3.EditSize = 11;
                                }
                                else
                                {
                                    v_UserField3.Size = (int)size;
                                }


                            }
                        }
                        if (subType != SAPbobsCOM.BoFldSubTypes.st_None)
                        {
                            v_UserField3.SubType = subType;
                        }
                        if (!string.IsNullOrEmpty(LinkedTable))
                            v_UserField3.LinkedTable = LinkedTable;
                        if (!string.IsNullOrEmpty(DefaultValue))
                            v_UserField3.DefaultValue = DefaultValue;


                        if (combolist != null)
                        {
                            foreach (var item in combolist)
                            {
                                v_UserField3.ValidValues.Value = item.Value;
                                v_UserField3.ValidValues.Description = item.Description;
                                v_UserField3.ValidValues.Add();
                            }
                        }



                        v_RetVal = v_UserField3.Add();
                        if (v_RetVal != 0)
                        {
                            Program.oCompany.GetLastError(out v_ErrCode, out v_ErrMsg);
                            Application.SBO_Application.StatusBar.SetText("Failed to add UserField masterid" + v_ErrCode + " " + v_ErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField3);
                            v_UserField3 = null;
                            return false;
                        }
                        else
                        {
                            Application.SBO_Application.StatusBar.SetText("[" + TableName + "] - " + FieldDescription + " added successfully!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField3);
                            v_UserField3 = null;
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                if (TableName.StartsWith("@") == false)
                {
                    if (!TableCreate.UDFExists(TableName, FieldName))
                    {
                        SAPbobsCOM.UserFieldsMD v_UserField3 = (SAPbobsCOM.UserFieldsMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                        v_UserField3.TableName = TableName;
                        v_UserField3.Name = FieldName;
                        v_UserField3.Description = FieldDescription;
                        v_UserField3.Type = type;
                        if (type != SAPbobsCOM.BoFieldTypes.db_Date)
                        {
                            if (size != 0)
                            {
                                if (type == SAPbobsCOM.BoFieldTypes.db_Numeric)
                                {
                                    v_UserField3.EditSize = 11;
                                }
                                else
                                {
                                    v_UserField3.Size = (int)size;
                                }

                            }
                        }
                        if (subType != SAPbobsCOM.BoFldSubTypes.st_None)
                        {
                            v_UserField3.SubType = subType;
                        }

                        //#region Geçerli Değerler

                        //if (TableName == "OCRD" && FieldName == "MUSDUR")
                        //{ 
                        //    v_UserField.ValidValues.Value = "1";
                        //    v_UserField.ValidValues.Description = "Yeni";
                        //    v_UserField.ValidValues.Add();
                        //    v_UserField.ValidValues.Value = "2";
                        //    v_UserField.ValidValues.Description = "Eski";
                        //    v_UserField.ValidValues.Add(); 
                        //} 
                        //#endregion 

                        if (!string.IsNullOrEmpty(LinkedTable))
                            v_UserField3.LinkedTable = LinkedTable;
                        v_RetVal = v_UserField3.Add();
                        if (v_RetVal != 0)
                        {
                            Program.oCompany.GetLastError(out v_ErrCode, out v_ErrMsg);
                            Application.SBO_Application.StatusBar.SetText("Failed to add UserField " + FieldDescription + " - " + v_ErrCode + " " + v_ErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField3);
                            v_UserField3 = null;
                            return false;
                        }
                        else
                        {
                            Application.SBO_Application.StatusBar.SetText(" & TableName & - " + FieldDescription + " added successfully!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_UserField3);
                            v_UserField3 = null;
                            return true;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
            return true;
        }


        public static bool UDFExists(string TableName, string FieldID)
        {
            try
            {
                SAPbobsCOM.Recordset rs = (SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                bool oFlag = true;
                dynamic aa = "Select 1 from [CUFD] Where TableID='" + TableName.Trim() + "' and AliasID='" + FieldID.Trim() + "'";
                rs.DoQuery("Select 1 from \"CUFD\" Where \"TableID\"='" + TableName.Trim() + "' and \"AliasID\"='" + FieldID.Trim() + "'");
                if (rs.EoF)
                    oFlag = false;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(rs);
                rs = null;
                GC.Collect();
                return oFlag;

            }
            catch (Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText(ex.Message);
            }
            return true;
        }


        public static void CreateUserFieldsFloat()
        {
            try
            {
                SAPbobsCOM.UserFieldsMD userFields = default(SAPbobsCOM.UserFieldsMD);
                userFields = (SAPbobsCOM.UserFieldsMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                userFields.TableName = "";
                userFields.Name = "";
                userFields.Description = "";
                userFields.Type = SAPbobsCOM.BoFieldTypes.db_Float;
                userFields.SubType = SAPbobsCOM.BoFldSubTypes.st_Price;
                userFields.EditSize = 20;
                int errCode = userFields.Add();

                if (errCode != 0)
                {
                    string errMsg;
                    Program.oCompany.GetLastError(out errCode, out errMsg);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

}
