using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch_Update
{

    class UserDataCreation
    {
        //
        public static bool RegisterUDO(string UDOCode, string UDOName, SAPbobsCOM.BoUDOObjType UDOType, Dictionary<string, string> fields, string UDOHTableName = "", string UDODTableName = "", SAPbobsCOM.BoYesNoEnum LogOption = SAPbobsCOM.BoYesNoEnum.tNO)
        {
            bool functionReturnValue = false;
            bool ActionSuccess = false;
            try
            {
                functionReturnValue = false;
                SAPbobsCOM.UserObjectsMD v_udoMD = default(SAPbobsCOM.UserObjectsMD);
                v_udoMD = (SAPbobsCOM.UserObjectsMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);
                v_udoMD.Code = UDOCode;
                v_udoMD.Name = UDOName;
                v_udoMD.ObjectType = UDOType;
                v_udoMD.TableName = UDOHTableName;
                v_udoMD.CanDelete = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.CanFind = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.CanCancel = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.CanClose = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.EnableEnhancedForm = SAPbobsCOM.BoYesNoEnum.tNO;
                v_udoMD.MenuItem = SAPbobsCOM.BoYesNoEnum.tNO;
                v_udoMD.Code = UDOCode;
                v_udoMD.Name = UDOName;
                v_udoMD.TableName = UDOHTableName;
                v_udoMD.MenuCaption = UDODTableName;
                v_udoMD.MenuCaption = UDOName;
                //v_udoMD.FatherMenuID = 3584;
                // v_udoMD.MenuUID=UDOName;
                //v_udoMD.ManageSeries = SAPbobsCOM.BoYesNoEnum.tNO;
                //v_udoMD.RebuildEnhancedForm = SAPbobsCOM.BoYesNoEnum.tNO;
                //v_udoMD.OverwriteDllfile = SAPbobsCOM.BoYesNoEnum.tYES; 



                #region Bul Alanlarının Eklenmesi
                foreach (var item in fields)
                {
                    v_udoMD.FindColumns.ColumnAlias = item.Key;
                    v_udoMD.FindColumns.ColumnDescription = item.Value;
                    v_udoMD.FindColumns.Add();
                }

                #endregion

                #region Görünecek Alanlarının Eklenmesi

                int count = 0;
                foreach (var item in fields)
                {
                    count++;
                    v_udoMD.FormColumns.FormColumnAlias = item.Key;
                    v_udoMD.FormColumns.FormColumnDescription = item.Value;
                    if (count > 1)
                    {
                        v_udoMD.FormColumns.Editable = SAPbobsCOM.BoYesNoEnum.tYES;
                    }

                    v_udoMD.FormColumns.Add();
                }

                #endregion

                //var a = v_udoMD.Add();
                //string errMsg = Program.oCompany.GetLastErrorDescription();
                if (v_udoMD.Add() == 0)
                {
                    functionReturnValue = true;
                    if (Program.oCompany.InTransaction)
                        Program.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                    SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Successfully Registered UDO >" + UDOCode + ">" + UDOName + " >" + Program.oCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                }
                else
                {
                    SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Failed to Register UDO >" + UDOCode + ">" + UDOName + " >" + Program.oCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    functionReturnValue = false;
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(v_udoMD);
                v_udoMD = null;
                GC.Collect();
                if (ActionSuccess == false & Program.oCompany.InTransaction)
                    Program.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
            }
            catch (Exception ex)
            {
                if (Program.oCompany.InTransaction)
                    Program.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
            }
            return functionReturnValue;
        }

        public static bool MeRegisterUDO(string UDOCode, string UDOName, SAPbobsCOM.BoUDOObjType UDOType, string UDOHTableName, string UDODTableName = "")
        {
            bool functionReturnValue = false;
            bool ActionSuccess = false;
            try
            {
                functionReturnValue = false;
                SAPbobsCOM.UserObjectsMD v_udoMD = default(SAPbobsCOM.UserObjectsMD);
                v_udoMD = (SAPbobsCOM.UserObjectsMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);
                v_udoMD.Code = UDOCode;
                v_udoMD.Name = UDOName;
                v_udoMD.ObjectType = UDOType;
                v_udoMD.TableName = UDOHTableName;
                v_udoMD.CanDelete = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.CanFind = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.CanCancel = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.CanClose = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tNO;
                v_udoMD.EnableEnhancedForm = SAPbobsCOM.BoYesNoEnum.tNO;
                v_udoMD.MenuItem = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.Code = UDOCode;
                v_udoMD.Name = UDOName;
                v_udoMD.TableName = UDOHTableName;
                v_udoMD.MenuCaption = UDOName;

                if (UDOName == "WorkSheet")
                {
                    v_udoMD.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tYES;
                    v_udoMD.EnableEnhancedForm = SAPbobsCOM.BoYesNoEnum.tNO;

                    v_udoMD.MenuCaption = "WorkSheet";
                    v_udoMD.FatherMenuID = 11520;
                    v_udoMD.Position = -1;
                    //v_udoMD.t
                }

                //if (LogOption == SAPbobsCOM.BoYesNoEnum.tYES)
                //{
                //    v_udoMD.CanLog = SAPbobsCOM.BoYesNoEnum.tYES;
                //    v_udoMD.LogTableName = "A" + UDOHTableName;
                //}
                v_udoMD.ObjectType = UDOType;

                //for (Int16 i = 0; i <= FindField.GetLength(0) - 1; i++)
                //{
                //    if (i > 0)
                //        v_udoMD.FindColumns.Add();
                //    v_udoMD.FindColumns.ColumnAlias = FindField(i, 0);
                //    v_udoMD.FindColumns.ColumnDescription = FindField(i, 1);
                //}

                if (v_udoMD.Add() == 0)
                {
                    functionReturnValue = true;
                    if (Program.oCompany.InTransaction)
                        Program.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                    SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Successfully Registered UDO >" + UDOCode + ">" + UDOName + " >" + Program.oCompany.GetLastErrorDescription().ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                }
                else
                {
                    SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Failed to Register UDO >" + UDOCode + ">" + UDOName + " >" + Program.oCompany.GetLastErrorDescription().ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    //MessageBox.Show(Program.oCompany.GetLastErrorDescription).ToString();
                    functionReturnValue = false;
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(v_udoMD);
                v_udoMD = null;
                GC.Collect();
                if (ActionSuccess == false & Program.oCompany.InTransaction)
                    Program.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
            }
            catch (Exception ex)
            {
                if (Program.oCompany.InTransaction)
                    Program.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
            }
            return functionReturnValue;
        }

        public static bool RegisterUDOWithChildTable(string UDOCode, string UDOName, SAPbobsCOM.BoUDOObjType UDOType, Dictionary<string, string> fields, string UDOHTableName = "", string UDODTableName = "", SAPbobsCOM.BoYesNoEnum LogOption = SAPbobsCOM.BoYesNoEnum.tNO, List<ChildTable> chList = null)
        {
            {
                bool functionReturnValue = false;
                bool ActionSuccess = false;
                try
                {
                    functionReturnValue = false;
                    SAPbobsCOM.UserObjectsMD v_udoMD = default(SAPbobsCOM.UserObjectsMD);
                    v_udoMD = (SAPbobsCOM.UserObjectsMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);
                    v_udoMD.Code = UDOCode;
                    v_udoMD.Name = UDOName;
                    v_udoMD.ObjectType = UDOType;
                    v_udoMD.TableName = UDOHTableName;
                    v_udoMD.CanDelete = SAPbobsCOM.BoYesNoEnum.tYES;
                    v_udoMD.CanFind = SAPbobsCOM.BoYesNoEnum.tYES;
                    v_udoMD.CanCancel = SAPbobsCOM.BoYesNoEnum.tYES;
                    v_udoMD.CanClose = SAPbobsCOM.BoYesNoEnum.tYES;
                    v_udoMD.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tNO;
                    v_udoMD.EnableEnhancedForm = SAPbobsCOM.BoYesNoEnum.tNO;
                    v_udoMD.CanLog = SAPbobsCOM.BoYesNoEnum.tNO;


                    if (LogOption == SAPbobsCOM.BoYesNoEnum.tYES)
                    {
                        v_udoMD.CanLog = SAPbobsCOM.BoYesNoEnum.tYES;
                        v_udoMD.LogTableName = "A" + UDOHTableName;
                    }

                    #region Bul Alanlarının Eklenmesi

                    foreach (var item in fields)
                    {
                        v_udoMD.FindColumns.ColumnAlias = item.Key;
                        v_udoMD.FindColumns.ColumnDescription = item.Value;
                        v_udoMD.FindColumns.Add();
                    }

                    #endregion

                    #region chList


                    int ChildNumber = 0;
                    foreach (var ch in chList)
                    {
                        ChildNumber++;
                        v_udoMD.ChildTables.TableName = ch.TableName;
                        if (ChildNumber != chList.Count())
                        {
                            v_udoMD.ChildTables.Add();
                        }
                        if (ChildNumber == chList.Count())
                        {
                            int SetCurrentLine = 0;
                            foreach (var item in ch.FormColumn)
                            {
                                v_udoMD.FormColumns.SetCurrentLine(SetCurrentLine);
                                v_udoMD.FormColumns.SonNumber = ChildNumber;
                                v_udoMD.FormColumns.FormColumnAlias = item.FormColumnAlias;
                                v_udoMD.FormColumns.FormColumnDescription = item.FormColumnDescription;
                                v_udoMD.FormColumns.Editable = item.Editable;
                                v_udoMD.FormColumns.Add();

                                SetCurrentLine++;
                            }
                        }

                        v_udoMD.EnhancedFormColumns.ColumnAlias = "DocEntry";
                        v_udoMD.EnhancedFormColumns.ColumnDescription = "DocEntry";
                        v_udoMD.EnhancedFormColumns.ColumnIsUsed = SAPbobsCOM.BoYesNoEnum.tNO;
                        v_udoMD.EnhancedFormColumns.ColumnNumber = 1;
                        v_udoMD.EnhancedFormColumns.ChildNumber = ChildNumber;
                        v_udoMD.EnhancedFormColumns.Add();

                        v_udoMD.EnhancedFormColumns.ColumnAlias = "LineId";
                        v_udoMD.EnhancedFormColumns.ColumnDescription = "LineId";
                        v_udoMD.EnhancedFormColumns.ColumnIsUsed = SAPbobsCOM.BoYesNoEnum.tNO;
                        v_udoMD.EnhancedFormColumns.ColumnNumber = 2;
                        v_udoMD.EnhancedFormColumns.ChildNumber = ChildNumber;
                        v_udoMD.EnhancedFormColumns.Add();

                        v_udoMD.EnhancedFormColumns.ColumnAlias = "Object";
                        v_udoMD.EnhancedFormColumns.ColumnDescription = "Object";
                        v_udoMD.EnhancedFormColumns.ColumnIsUsed = SAPbobsCOM.BoYesNoEnum.tNO;
                        v_udoMD.EnhancedFormColumns.ColumnNumber = 3;
                        v_udoMD.EnhancedFormColumns.ChildNumber = ChildNumber;
                        v_udoMD.EnhancedFormColumns.Add();

                        v_udoMD.EnhancedFormColumns.ColumnAlias = "LogInst";
                        v_udoMD.EnhancedFormColumns.ColumnDescription = "LogInst";
                        v_udoMD.EnhancedFormColumns.ColumnIsUsed = SAPbobsCOM.BoYesNoEnum.tNO;
                        v_udoMD.EnhancedFormColumns.ColumnNumber = 4;
                        v_udoMD.EnhancedFormColumns.ChildNumber = ChildNumber;
                        v_udoMD.EnhancedFormColumns.Add();


                        int columNumber = 5;
                        foreach (var item in ch.FormColumn.Where(k => k.FormColumnAlias != "DocEntry"))
                        {
                            v_udoMD.EnhancedFormColumns.ColumnAlias = item.FormColumnAlias;
                            v_udoMD.EnhancedFormColumns.ColumnDescription = item.FormColumnDescription;
                            v_udoMD.EnhancedFormColumns.ColumnIsUsed = item.Editable;
                            v_udoMD.EnhancedFormColumns.Editable = item.Editable;
                            v_udoMD.EnhancedFormColumns.ColumnNumber = columNumber;
                            v_udoMD.EnhancedFormColumns.ChildNumber = ChildNumber;
                            v_udoMD.EnhancedFormColumns.Add();

                            columNumber++;
                        }

                    }

                    #endregion

                    #region Görünecek Alanlarının Eklenmesi

                    int count = 0;
                    foreach (var item in fields)
                    {
                        count++;
                        v_udoMD.FormColumns.FormColumnAlias = item.Key;
                        v_udoMD.FormColumns.FormColumnDescription = item.Value;
                        if (count > 1)
                        {
                            v_udoMD.FormColumns.Editable = SAPbobsCOM.BoYesNoEnum.tYES;
                        }

                        v_udoMD.FormColumns.Add();
                    }

                    #endregion


                    if (v_udoMD.Add() == 0)
                    {
                        functionReturnValue = true;
                        if (Program.oCompany.InTransaction)
                            Program.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                        SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Successfully Registered UDO >" + UDOCode + ">" + UDOName + " >" + Program.oCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    }
                    else
                    {
                        SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Failed to Register UDO >" + UDOCode + ">" + UDOName + " >" + Program.oCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                        functionReturnValue = false;
                    }
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(v_udoMD);
                    v_udoMD = null;
                    GC.Collect();
                    if (ActionSuccess == false & Program.oCompany.InTransaction)
                        Program.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }
                catch (Exception ex)
                {
                    if (Program.oCompany.InTransaction)
                        Program.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }
                return functionReturnValue;
            }

        }
        public static bool UDOExists(string code)
        {

            GC.Collect();
            SAPbobsCOM.UserObjectsMD v_udoMD = default(SAPbobsCOM.UserObjectsMD);
            v_udoMD = (SAPbobsCOM.UserObjectsMD)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);
            bool v_ReturnCode = false;
            v_ReturnCode = v_udoMD.GetByKey(code);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(v_udoMD);
            v_udoMD = null;
            return v_ReturnCode;
        }
        public static bool CreateFunction(string FunctionName, string Function)
        {
            bool functionReturnValue = false;
            functionReturnValue = false;
            long v_RetVal = 0;
            long v_ErrCode = 0;
            string v_ErrMsg = "";
            try
            {
                if (!FunctionExists(FunctionName))
                {
                    SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Creating Procedure " + FunctionName + " ...................", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                    if (v_RetVal != 0)
                    {
                        SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Failed to Create Procedure " + FunctionName + v_ErrCode + " " + v_ErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                        return false;
                    }
                    else
                    {
                        SAPbobsCOM.Recordset oRsObjectExists = (SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        oRsObjectExists.DoQuery(Function);
                        SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("[" + FunctionName + "] - Created Successfully!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

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
            }
            return functionReturnValue;
        }
        public static bool CreateTrigger(string TriggerName, string Trigger)
        {
            bool functionReturnValue = false;
            functionReturnValue = false;
            long v_RetVal = 0;
            long v_ErrCode = 0;
            string v_ErrMsg = "";
            try
            {
                if (!TriggerExists(TriggerName))
                {
                    SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Creating Trigger " + TriggerName + " ...................", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                    if (v_RetVal != 0)
                    {
                        SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Failed to Create Procedure " + TriggerName + v_ErrCode + " " + v_ErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                        return false;
                    }
                    else
                    {
                        SAPbobsCOM.Recordset oRsObjectExists = (SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        oRsObjectExists.DoQuery(Trigger);
                        SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("[" + TriggerName + "] - Created Successfully!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

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
            }
            return functionReturnValue;
        }
        public static bool FunctionExists(string FunctionName)
        {
            bool oFlag = false;
            string oObjectExists = "SELECT  1 FROM    Information_schema.Routines WHERE   Specific_schema = 'dbo' AND specific_name = '" + FunctionName + "' ";
            oObjectExists += " AND Routine_Type = 'FUNCTION' ";
            SAPbobsCOM.Recordset oRsObjectExists = (SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRsObjectExists.DoQuery(oObjectExists);
            if (oRsObjectExists.RecordCount == 1)
            {
                oFlag = true;
            }
            return oFlag;
        }
        public static bool TriggerExists(string TriggerName)
        {
            bool oFlag = false;
            string oObjectExists = "SELECT trigger_name = name, trigger_owner = USER_NAME(uid), table_name = OBJECT_NAME(parent_obj),";
            oObjectExists += " isupdate = OBJECTPROPERTY( id, 'ExecIsUpdateTrigger'), isdelete = OBJECTPROPERTY( id, 'ExecIsDeleteTrigger'),";
            oObjectExists += " isinsert = OBJECTPROPERTY( id, 'ExecIsInsertTrigger'), isafter = OBJECTPROPERTY( id, 'ExecIsAfterTrigger'),";
            oObjectExists += " isinsteadof = OBJECTPROPERTY( id, 'ExecIsInsteadOfTrigger'),status = CASE OBJECTPROPERTY(id, 'ExecIsTriggerDisabled') WHEN 1 THEN 'Disabled' ELSE 'Enabled' END ";
            oObjectExists += " FROM sysobjects WHERE type = 'TR' and name='" + TriggerName + "'";
            SAPbobsCOM.Recordset oRsObjectExists = (SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRsObjectExists.DoQuery(oObjectExists);
            if (oRsObjectExists.RecordCount == 1)
            {
                oFlag = true;
            }
            //oFlag = Convert.ToBoolean(oRsObjectExists.Fields.Item(0).Value);
            return oFlag;
        }
    }
}
