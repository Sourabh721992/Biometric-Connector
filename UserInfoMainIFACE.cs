﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UserInfo
{
    public partial class UserInfoMainIFACE : Form
    {
        public UserInfoMainIFACE()
        {
            InitializeComponent();
        }

        //Create Standalone SDK class dynamicly.
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

        /*************************************************************************************************
        * Before you refer to this demo,we strongly suggest you read the development manual deeply first.*
        * This part is for demonstrating the communication with your device.                             *
        * ************************************************************************************************/
        #region Communication
        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber = 1;//the serial number of the device.After connecting the device ,this value will be changed.
        private static Int32 MyCountFinger;
        private static Int32 MyCountFace;
            
        //If your device supports the TCP/IP communications, you can refer to this.
        //when you are using the tcp/ip communication,you can distinguish different devices by their IP address.
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtIP.Text.Trim() == "" || txtPort.Text.Trim() == "")
            {
                MessageBox.Show("IP and Port cannot be null", "Error");
                return;
            }
            
            int idwErrorCode = 0;
            Cursor = Cursors.WaitCursor;

            if (btnConnect.Text == "DisConnect")
            {
                axCZKEM1.Disconnect();

                this.axCZKEM1.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                this.axCZKEM1.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                this.axCZKEM1.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
                this.axCZKEM1.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
                this.axCZKEM1.OnWriteCard -= new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
                this.axCZKEM1.OnEmptyCard -= new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);

                bIsConnected = false;
                btnConnect.Text = "Connect";
                lblState.Text = "Current State:DisConnected";
                Cursor = Cursors.Default;
                return;
            }

            axCZKEM1.PullMode = 1;
            bIsConnected = axCZKEM1.Connect_Net(txtIP.Text, Convert.ToInt32(txtPort.Text));
            if (bIsConnected == true)
            {
                btnConnect.Text = "DisConnect";
                btnConnect.Refresh();
                lblState.Text = "Current State:Connected";
                iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
               
                if (axCZKEM1.RegEvent(iMachineNumber, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                {
                    this.axCZKEM1.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                    this.axCZKEM1.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                    this.axCZKEM1.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
                    this.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
                    this.axCZKEM1.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
                    this.axCZKEM1.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
                }

                MyCountFinger = 1;
                MyCountFace = 2;
              
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //If your device supports the SerialPort communications, you can refer to this.
        private void btnRsConnect_Click(object sender, EventArgs e)
        {
            if (cbPort.Text.Trim() == "" || cbBaudRate.Text.Trim() == "" || txtMachineSN.Text.Trim() == "")
            {
                MessageBox.Show("Port,BaudRate and MachineSN cannot be null", "Error");
                return;
            }
            int idwErrorCode = 0;
            //accept serialport number from string like "COMi"
            int iPort;
            string sPort = cbPort.Text.Trim();
            for (iPort = 1; iPort < 10; iPort++)
            {
                if (sPort.IndexOf(iPort.ToString()) > -1)
                {
                    break;
                }
            }

            Cursor = Cursors.WaitCursor;
            if (btnRsConnect.Text == "Disconnect")
            {
                axCZKEM1.Disconnect();

                this.axCZKEM1.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                this.axCZKEM1.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                this.axCZKEM1.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
                this.axCZKEM1.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
                this.axCZKEM1.OnWriteCard -= new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
                this.axCZKEM1.OnEmptyCard -= new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);

                bIsConnected = false;
                btnRsConnect.Text = "Connect";
                btnRsConnect.Refresh();
                lblState.Text = "Current State:Disconnected";
                Cursor = Cursors.Default;
                return;
            }

            iMachineNumber = Convert.ToInt32(txtMachineSN.Text.Trim());//when you are using the serial port communication,you can distinguish different devices by their serial port number.
            bIsConnected = axCZKEM1.Connect_Com(iPort, iMachineNumber, Convert.ToInt32(cbBaudRate.Text.Trim()));

            if (bIsConnected == true)
            {
                btnRsConnect.Text = "Disconnect";
                btnRsConnect.Refresh();
                lblState.Text = "Current State:Connected";

                if (axCZKEM1.RegEvent(iMachineNumber, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                {
                    this.axCZKEM1.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                    this.axCZKEM1.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                    this.axCZKEM1.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
                    this.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
                    this.axCZKEM1.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
                    this.axCZKEM1.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }

            Cursor = Cursors.Default;
        }

        #endregion

        /*************************************************************************************************
        * This part is for demonstrating operations with user(download/upload/delete/clear/modify).      *
        * ************************************************************************************************/
        #region UserInfo

        //Download user's 9.0 or 10.0 arithmetic fingerprint templates(in strings)
        //Only TFT screen devices with firmware version Ver 6.60 version later support function "GetUserTmpExStr" and "GetUserTmpEx".
        //'While you are using 9.0 fingerprint arithmetic and your device's firmware version is under ver6.60,you should use the functions "SSR_GetUserTmp" or 
        //"SSR_GetUserTmpStr" instead of "GetUserTmpExStr" or "GetUserTmpEx" in order to download the fingerprint templates.
        private void btnDownloadTmp_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            string sdwEnrollNumber = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;

            int idwFingerIndex;
            string sTmpData = "";
            int iTmpLength = 0;
            int iFlag = 0;

            lvDownload.Items.Clear();
            lvDownload.BeginUpdate();
            axCZKEM1.EnableDevice(iMachineNumber, false);
            Cursor = Cursors.WaitCursor;
            
            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
            axCZKEM1.ReadAllTemplate(iMachineNumber);//read all the users' fingerprint templates to the memory
            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
            {
                for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                {
                    if (axCZKEM1.GetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = sdwEnrollNumber;
                        list.SubItems.Add(sName);
                        list.SubItems.Add(idwFingerIndex.ToString());
                        list.SubItems.Add(sTmpData);
                        list.SubItems.Add(iPrivilege.ToString());
                        list.SubItems.Add(sPassword);
                        if (bEnabled == true)
                        {
                            list.SubItems.Add("true");
                        }
                        else
                        {
                            list.SubItems.Add("false");
                        }
                        list.SubItems.Add(iFlag.ToString());
                        lvDownload.Items.Add(list);
                    }
                }
            }
            lvDownload.EndUpdate();
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;
        }

        //Upload the 9.0 or 10.0 fingerprint arithmetic templates to the device(in strings) in batches.
        //Only TFT screen devices with firmware version Ver 6.60 version later support function "SetUserTmpExStr" and "SetUserTmpEx".
        //While you are using 9.0 fingerprint arithmetic and your device's firmware version is under ver6.60,you should use the functions "SSR_SetUserTmp" or 
        //"SSR_SetUserTmpStr" instead of "SetUserTmpExStr" or "SetUserTmpEx" in order to upload the fingerprint templates.
        private void btnBatchUpdate_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
           
            if (lvDownload.Items.Count == 0)
            {
                MessageBox.Show("There is no data to upload!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sdwEnrollNumber = "";
            string sName = "";
            int idwFingerIndex = 0;
            string sTmpData = "";
            int iPrivilege = 0;
            string sPassword = "";
            string sEnabled = "";
            bool bEnabled = false;
            int iFlag = 1;

            int iUpdateFlag = 1;

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.BeginBatchUpdate(iMachineNumber, iUpdateFlag))//create memory space for batching data
            {
                string sLastEnrollNumber = "";//the former enrollnumber you have upload(define original value as 0)
                for (int i = 0; i < lvDownload.Items.Count; i++)
                {
                    sdwEnrollNumber = lvDownload.Items[i].SubItems[0].Text;
                    sName = lvDownload.Items[i].SubItems[1].Text;
                    idwFingerIndex = Convert.ToInt32(lvDownload.Items[i].SubItems[2].Text);
                    sTmpData = lvDownload.Items[i].SubItems[3].Text;
                    iPrivilege = Convert.ToInt32(lvDownload.Items[i].SubItems[4].Text);
                    sPassword = lvDownload.Items[i].SubItems[5].Text;
                    sEnabled = lvDownload.Items[i].SubItems[6].Text;
                    iFlag = Convert.ToInt32(lvDownload.Items[i].SubItems[7].Text);

                    if (sEnabled == "true")
                    {
                        bEnabled = true;
                    }
                    else
                    {
                        bEnabled = false;
                    }
                    if (sdwEnrollNumber != sLastEnrollNumber)//identify whether the user information(except fingerprint templates) has been uploaded
                    {
                        if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload user information to the memory
                        {
                            axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);//upload templates information to the memory
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
                            Cursor = Cursors.Default;
                            axCZKEM1.EnableDevice(iMachineNumber, true);
                            return;
                        }
                    }
                    else//the current fingerprint and the former one belongs the same user,that is ,one user has more than one template
                    {
                        axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);
                    }
                    sLastEnrollNumber = sdwEnrollNumber;//change the value of iLastEnrollNumber dynamicly
                }               
            }
            axCZKEM1.BatchUpdate(iMachineNumber);//upload all the information in the memory
            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            Cursor = Cursors.Default;
            axCZKEM1.EnableDevice(iMachineNumber, true);
            MessageBox.Show("Successfully upload fingerprint templates in batches , " + "total:" + lvDownload.Items.Count.ToString(), "Success");
        }

        //Upload the 9.0 or 10.0 fingerprint arithmetic templates one by one(in strings)
        //Only TFT screen devices with firmware version Ver 6.60 version later support function "SetUserTmpExStr" and "SetUserTmpEx".
        //While you are using 9.0 fingerprint arithmetic and your device's firmware version is under ver6.60,you should use the functions "SSR_SetUserTmp" or 
        //"SSR_SetUserTmpStr" instead of "SetUserTmpExStr" or "SetUserTmpEx" in order to upload the fingerprint templates.
        private void btnUploadTmp_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (lvDownload.Items.Count == 0)
            {
                MessageBox.Show("There is no data to upload!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sdwEnrollNumber = "";
            string sName = "";
            int idwFingerIndex = 0;
            string sTmpData = "";
            int iPrivilege = 0;
            string sPassword = "";
            int iFlag = 0;
            string sEnabled = "";
            bool bEnabled = false;

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            for (int i = 0; i < lvDownload.Items.Count; i++)
            {
                sdwEnrollNumber = lvDownload.Items[i].SubItems[0].Text.Trim();
                sName = lvDownload.Items[i].SubItems[1].Text.Trim();
                idwFingerIndex = Convert.ToInt32(lvDownload.Items[i].SubItems[2].Text.Trim());
                sTmpData = lvDownload.Items[i].SubItems[3].Text.Trim();
                iPrivilege = Convert.ToInt32(lvDownload.Items[i].SubItems[4].Text.Trim());
                sPassword = lvDownload.Items[i].SubItems[5].Text.Trim();

                sEnabled = lvDownload.Items[i].SubItems[6].Text.Trim();
                iFlag = Convert.ToInt32(lvDownload.Items[i].SubItems[7].Text);
                if (sEnabled == "true")
                {
                    bEnabled = true;
                }
                else
                {
                    bEnabled = false;
                }

                if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload user information to the device
                {
                    axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);//upload templates information to the device
                }
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                    MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
                    Cursor = Cursors.Default;
                    axCZKEM1.EnableDevice(iMachineNumber, true);
                    return;
                }
            }
            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            Cursor = Cursors.Default;
            axCZKEM1.EnableDevice(iMachineNumber, true);
            MessageBox.Show("Successfully Upload fingerprint templates, " + "total:" + lvDownload.Items.Count.ToString(), "Success");
        }
        //Delete a certain user's fingerprint template of specified index
        //You shuold input the the user id and the fingerprint index you will delete
        //The difference between the two functions "SSR_DelUserTmpExt" and "SSR_DelUserTmp" is that the former supports 24 bits' user id.
        private void btnSSR_DelUserTmpExt_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (cbUserIDTmp.Text.Trim() == "" )
            {
                MessageBox.Show("Please input the UserID and FingerIndex first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = cbUserIDTmp.Text.Trim();
            int iFingerIndex = 0;// Convert.ToInt32(cbFingerIndex.Text.Trim());

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.SSR_DelUserTmpExt(iMachineNumber, sUserID, iFingerIndex))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("SSR_DelUserTmpExt,UserID:" + sUserID + " FingerIndex:" + iFingerIndex.ToString(), "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }
        //Clear all the fingerprint templates in the device(While the parameter DataFlag  of the Function "ClearData" is 2 )
        private void btnClearDataTmps_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            int iDataFlag = 2;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.ClearData(iMachineNumber, iDataFlag))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Clear all the fingerprint templates!", "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }
        //Delete all the user information in the device,while the related fingerprint templates will be deleted either. 
        //(While the parameter DataFlag  of the Function "ClearData" is 5 )
        private void btnClearDataUserInfo_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            int iDataFlag = 5;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.ClearData(iMachineNumber, iDataFlag))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Clear all the UserInfo data!", "Success");

                
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }
        //Delete a kind of data that some user has enrolled
        //The range of the Backup Number is from 0 to 9 and the specific meaning of Backup number is described in the development manual,pls refer to it.
        private void btnDeleteEnrollData_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (cbUserIDDE.Text.Trim() == "" || cbBackupDE.Text.Trim() == "")
            {
                MessageBox.Show("Please input the UserID and BackupNumber first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = cbUserIDDE.Text.Trim();
            int iBackupNumber = Convert.ToInt32(cbBackupDE.Text.Trim());

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.SSR_DeleteEnrollData(iMachineNumber, sUserID, iBackupNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("DeleteEnrollData,UserID=" + sUserID + " BackupNumber=" + iBackupNumber.ToString(), "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }
        //Clear all the administrator privilege(not clear the administrators themselves)
        private void btnClearAdministrators_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.ClearAdministrators(iMachineNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Successfully clear administrator privilege from teiminal!", "Success");

              
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
           
        }
        //Download users' face templates(in strings)(For TFT screen IFace series devices only)
        private void btnDownLoadFace_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            string sUserID = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;

            int iFaceIndex = 50;//the only possible parameter value
            string sTmpData = "";
            int iLength = 0;

           
            lvFace.Items.Clear();
            lvFace.BeginUpdate();

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory

            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sUserID, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
            {
                if (axCZKEM1.GetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, ref sTmpData, ref iLength))//get the face templates from the memory
                {
                    ListViewItem list = new ListViewItem();
                    list.Text = sUserID;
                    list.SubItems.Add(sName);
                    list.SubItems.Add(sPassword);
                    list.SubItems.Add(iPrivilege.ToString());
                    list.SubItems.Add(iFaceIndex.ToString());
                    list.SubItems.Add(sTmpData);
                    list.SubItems.Add(iLength.ToString());
                    if (bEnabled == true)
                    {
                        list.SubItems.Add("true");
                    }
                    else
                    {
                        list.SubItems.Add("false");
                    }
                    lvFace.Items.Add(list);
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            lvFace.EndUpdate();
            Cursor = Cursors.Default;
        }
        //Upload users' face template(in strings)(For TFT screen IFace series devices only)
        //Uploading the face templates in batches is not supported temporarily.
        private void btnUploadFace_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = "";
            string sName = "";
            int iFaceIndex = 0;
            string sTmpData = "";
            int iLength = 0;
            int iPrivilege = 0;
            string sPassword = "";
            string sEnabled = "";
            bool bEnabled = false;
          

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            for (int i = 0; i < lvFace.Items.Count; i++)
            {
                sUserID = lvFace.Items[i].SubItems[0].Text;
                sName = lvFace.Items[i].SubItems[1].Text;
                sPassword = lvFace.Items[i].SubItems[2].Text;
                iPrivilege = Convert.ToInt32(lvFace.Items[i].SubItems[3].Text);
                iFaceIndex = Convert.ToInt32(lvFace.Items[i].SubItems[4].Text);
                sTmpData = lvFace.Items[i].SubItems[5].Text;
                iLength = Convert.ToInt32(lvFace.Items[i].SubItems[6].Text);
                if (sEnabled == "true")
                {
                    bEnabled = true;
                }
                else
                {
                    bEnabled = false;
                }

                if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sUserID, sName, sPassword, iPrivilege, bEnabled))//face templates are part of users' information
                {
                    axCZKEM1.SetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, sTmpData, iLength);//upload face templates information to the device
                }
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                    MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
                    Cursor = Cursors.Default;
                    axCZKEM1.EnableDevice(iMachineNumber, true);
                    return;
                }
            }

            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            Cursor = Cursors.Default;
            axCZKEM1.EnableDevice(iMachineNumber, true);
            MessageBox.Show("Successfully Upload the face templates, " + "total:" + lvFace.Items.Count.ToString(), "Success");
        }
        //Delete a certain user's face template according to its id
        private void btnDelUserFace_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (cbUserIDFace.Text.Trim() == "" )
            {
                MessageBox.Show("Please input the UserID first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = cbUserIDFace.Text.Trim();
            int iFaceIndex = 50;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.DelUserFace(iMachineNumber, sUserID, iFaceIndex))
            {
                axCZKEM1.RefreshData(iMachineNumber);
                MessageBox.Show("DelUserFace,UserID=" + sUserID, "Success");

            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;

        }
        //Download specified user's face template (in bytes array)    
        //You can refer to the part of "Udisk data Management" to learn how to manage the user's binary template(Get or Set)
        private void btnGetUserFace_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (cbUserIDFace.Text.Trim() == "")
            {
                MessageBox.Show("Please input the UserID first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = cbUserIDFace.Text.Trim();
            int iFaceIndex = 50;//the only possible parameter value
            int iLength = 128 * 1024;//initialize the length(cannot be zero)
            byte[] byTmpData = new byte[iLength];

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);

            if (axCZKEM1.GetUserFace(iMachineNumber, sUserID, iFaceIndex, ref byTmpData[0], ref iLength))
            {
                //Here you can manage the information of the face templates according to your request.(for example,you can sava them to the database)
                MessageBox.Show("GetUserFace,the  length of the bytes array byTmpData is " + iLength.ToString(), "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }

            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;
        }       
        //Add the existed userid to DropDownLists.

        //Add the existed userid to DropDownLists.
      
       
        bool bAddControl = true;
        private void UserIDTimer_Tick(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                cbUserIDDE.Items.Clear();
                cbUserIDTmp.Items.Clear();
                cbUserIDFace.Items.Clear();
                cbUserId_Card.Items.Clear();
                cbPrivilege.Items.Clear();
                bAddControl = true;
                return;
            }
            else
            {
                if (bAddControl == true)
                {
                    string sEnrollNumber = "";
                    string sName = "";
                    string sPassword = "";
                    int iPrivilege = 0;
                    bool bEnabled = false;

                    axCZKEM1.EnableDevice(iMachineNumber, false);
                    axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
                    while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
                    {
                        cbUserIDDE.Items.Add(sEnrollNumber);
                        cbUserIDTmp.Items.Add(sEnrollNumber);
                        cbUserIDFace.Items.Add(sEnrollNumber);
                        cbUserId_Card.Items.Add(sEnrollNumber);
                        cbPrivilege.Items.Add(iPrivilege);
                    }
                    bAddControl = false;
                    axCZKEM1.EnableDevice(iMachineNumber, true);
                }
                return;
            }
        }
        #endregion
        #region Databse connection for Finger Templates
        // start for fingers  Templates, database connection
        private void DownloadFPTemDB_Click(object sender, EventArgs e)
        {
            if (MyCountFinger == 1)
            {
                if (bIsConnected == false)
                {
                    MessageBox.Show("Please connect the device first!", "Error");
                    return;
                }

                string sName = "", sPassword = "", sTmpData = "", sdwEnrollNumber = "";
                int iPrivilege = 0, idwFingerIndex = 0, iTmpLength = 0, iFlag = 0;
                bool bEnabled = false;
                MyCountFinger = -1;

                lvDownload.Items.Clear();
                lvDownload.BeginUpdate();
                axCZKEM1.EnableDevice(iMachineNumber, false);
                Cursor = Cursors.WaitCursor;

                axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
                axCZKEM1.ReadAllTemplate(iMachineNumber);//read all the users' fingerprint templates to the memory

                while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
                {
                    for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                    {
                        if (axCZKEM1.GetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                        {
                            ListViewItem list = new ListViewItem();
                            list.Text = sdwEnrollNumber.ToString();
                            list.SubItems.Add(sName);
                            list.SubItems.Add(idwFingerIndex.ToString());
                            list.SubItems.Add(sTmpData);
                            list.SubItems.Add(iPrivilege.ToString());
                            list.SubItems.Add(sPassword);
                            if (bEnabled == true)
                            {
                                list.SubItems.Add("true");
                            }
                            else
                            {
                                list.SubItems.Add("false");
                            }
                            list.SubItems.Add(iFlag.ToString());
                            lvDownload.Items.Add(list);

                            // save in msaccess database
                            UICO uicobj = new UICO();
                            uicobj.Insert_User_DetailsIFACE_FingerTm(sdwEnrollNumber, sName, idwFingerIndex, sTmpData, iPrivilege, sPassword, bEnabled, iFlag);
                        }
                    }
                    // MessageBox.Show("Records inserted successfully");
                }

                lvDownload.EndUpdate();
                axCZKEM1.EnableDevice(iMachineNumber, true);
                Cursor = Cursors.Default;

            }
        }
        private void btnDatabase_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            int idwErrorCode = 0;
            string sdwEnrollNumber = "";
            string sName = "";
            int idwFingerIndex = 0;
            string sTmpData = "";
            int iPrivilege = 0;
            string sPassword = "";
            string sEnabled = "";
            string sLastEnrollNumber = "";

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);

            // select data from database
            UICO objupload = new UICO();
            DataTable dt = objupload.UploadDataIFACE_FingerTm();

            // start select data from database to upload in listview
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sdwEnrollNumber = string.IsNullOrEmpty(dt.Rows[i]["User_Id"].ToString()) ? " " : dt.Rows[i]["User_Id"].ToString();
                sName = string.IsNullOrEmpty(dt.Rows[i]["Name"].ToString()) ? " " : dt.Rows[i]["Name"].ToString();
                idwFingerIndex = string.IsNullOrEmpty(dt.Rows[i]["Finger_Index"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["Finger_Index"].ToString());
                sTmpData = string.IsNullOrEmpty(dt.Rows[i]["Finger_Image"].ToString()) ? " " : dt.Rows[i]["Finger_Image"].ToString();
                iPrivilege = string.IsNullOrEmpty(dt.Rows[i]["Privilege"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["Privilege"].ToString());
                sPassword = string.IsNullOrEmpty(dt.Rows[i]["Passwords"].ToString()) ? null : dt.Rows[i]["Passwords"].ToString();
                sEnabled = string.IsNullOrEmpty(dt.Rows[i]["Enabled"].ToString()) ? " " : dt.Rows[i]["Enabled"].ToString();
                int iFlag = Convert.ToInt32(dt.Rows[i]["Flag"].ToString());

                if (sdwEnrollNumber != sLastEnrollNumber)//identify whether the user information(except fingerprint templates) has been uploaded
                {
                    if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, Convert.ToBoolean(sEnabled)))//upload user information to the memory
                    {
                        axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);//upload templates information to the memory
                    }
                    else
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
                        Cursor = Cursors.Default;
                        axCZKEM1.EnableDevice(iMachineNumber, true);
                        return;
                    }
                }
                else//the current fingerprint and the former one belongs the same user,that is ,one user has more than one template
                {
                    axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);
                }
                sLastEnrollNumber = sdwEnrollNumber;//change the value of iLastEnrollNumber dynamicly
            }
            //end
            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            Cursor = Cursors.Default;
            axCZKEM1.EnableDevice(iMachineNumber, true);
            MessageBox.Show("Successfully upload fingerprint templates , " + "total:" + dt.Rows.Count.ToString(), "Success");
        }
        private void DeleteFpTm_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            UICO obj = new UICO();
            obj.DeleteAllEmpTmIFACE_FingerTm();
            MessageBox.Show("Successfully Data delteted from database");
        }
        // end
        #endregion
        #region Database Connectionb for Face templates
        // start For Face templates, database connection
        private void DownloadFaceTemDB_Click(object sender, EventArgs e)
        {
            if(MyCountFace == 2)
            {
                if (bIsConnected == false)
                {
                    MessageBox.Show("Please connect the device first!", "Error");
                    return;
                }

                string sUserID = "";
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = false;

                int iFaceIndex = 50;//the only possible parameter value
                string sTmpData = "";
                int iLength = 0;

                MyCountFace = -2;

                lvFace.Items.Clear();
                lvFace.BeginUpdate();

                Cursor = Cursors.WaitCursor;
                axCZKEM1.EnableDevice(iMachineNumber, false);
                axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory

                while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sUserID, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
                {
                    if (axCZKEM1.GetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, ref sTmpData, ref iLength))//get the face templates from the memory
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = sUserID;
                        list.SubItems.Add(sName);
                        list.SubItems.Add(sPassword);
                        list.SubItems.Add(iPrivilege.ToString());
                        list.SubItems.Add(iFaceIndex.ToString());
                        list.SubItems.Add(sTmpData);
                        list.SubItems.Add(iLength.ToString());
                        if (bEnabled == true)
                        {
                            list.SubItems.Add("true");
                        }
                        else
                        {
                            list.SubItems.Add("false");
                        }
                        lvFace.Items.Add(list);

                        // save in msaccess database
                        UICO uicobj = new UICO();
                        uicobj.Insert_User_DetailsIFACE_FaceTm(sUserID, sName, sPassword, iPrivilege, iFaceIndex, sTmpData, iLength, bEnabled);
                      
                    }
                }
                axCZKEM1.EnableDevice(iMachineNumber, true);
                lvFace.EndUpdate();
                Cursor = Cursors.Default;
            }
        }

        private void btnUpdateFaceDatabase_Click(object sender, EventArgs e)
        {
                        
                if (bIsConnected == false)
                {
                    MessageBox.Show("Please connect the device first!", "Error");
                    return;
                }
                int idwErrorCode = 0;

                string sUserID = "";
                string sName = "";
                int iFaceIndex = 0;
                string sTmpData = "";
                int iLength = 0;
                int iPrivilege = 0;
                string sPassword = "";
                string sEnabled = "";
                bool bEnabled = false;
                string lastUserId = "";

                Cursor = Cursors.WaitCursor;
                axCZKEM1.EnableDevice(iMachineNumber, false);

                // select data from database
                UICO objupload = new UICO();
                DataTable dt = objupload.UploadDataIFACE_FaceTm();

                // start select data from database to upload in listview
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sUserID = string.IsNullOrEmpty(dt.Rows[i]["User_Id"].ToString()) ? " " : dt.Rows[i]["User_Id"].ToString();
                    sName = string.IsNullOrEmpty(dt.Rows[i]["Name"].ToString()) ? " " : dt.Rows[i]["Name"].ToString();
                    sPassword = string.IsNullOrEmpty(dt.Rows[i]["Passwords"].ToString()) ? null : dt.Rows[i]["Passwords"].ToString();
                    iPrivilege = string.IsNullOrEmpty(dt.Rows[i]["Privilege"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["Privilege"].ToString());
                    iFaceIndex = string.IsNullOrEmpty(dt.Rows[i]["Face_Index"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["Face_Index"].ToString());
                    sTmpData = string.IsNullOrEmpty(dt.Rows[i]["Face_Image"].ToString()) ? " " : dt.Rows[i]["Face_Image"].ToString();
                    iLength = string.IsNullOrEmpty(dt.Rows[i]["Face_Length"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["Face_Length"].ToString());
                    sEnabled = string.IsNullOrEmpty(dt.Rows[i]["Enabled"].ToString()) ? " " : dt.Rows[i]["Enabled"].ToString();

                    if (sUserID != lastUserId)//identify whether the user information(except fingerprint templates) has been uploaded
                    {
                        if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sUserID, sName, sPassword, iPrivilege, Convert.ToBoolean(sEnabled)))//upload user information to the memory
                        {
                            axCZKEM1.SetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, sTmpData, iLength);//upload templates information to the memory
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
                            Cursor = Cursors.Default;
                            axCZKEM1.EnableDevice(iMachineNumber, true);
                            return;
                        }
                    }
                    else//the current fingerprint and the former one belongs the same user,that is ,one user has more than one template
                    {
                        axCZKEM1.SetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, sTmpData, iLength);
                    }
                    sUserID = lastUserId ;//change the value of userId dynamicly
                }
                //end                           

                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                Cursor = Cursors.Default;
                axCZKEM1.EnableDevice(iMachineNumber, true);
                MessageBox.Show("Successfully Upload the face templates, " + "total:" + lvFace.Items.Count.ToString(), "Success");
            
        }

        private void btnDeleteFaceTm_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            
            UICO obj = new UICO();
            obj.DeleteAllEmpTmIFACE_FaceTm();
            MessageBox.Show("Successfully Data deleted from database");
        }
        // end        
        #endregion

        /**************************************************************************************************
        * This part is for demonstrating the RealTime Events triggered by your operations on the device.  *
        * Here is part of the real time events, more pls refer to the RTEvents demo                       *
        * *************************************************************************************************/
        #region RealTime Events

        //When you have enrolled a new user,this event will be triggered.
        private void axCZKEM1_OnNewUser(int iEnrollNumber)
        {
            lbRTShow.Items.Add("RTEvent OnNewUser Has been Triggered...");
            lbRTShow.Items.Add("...NewUserID=" + iEnrollNumber.ToString());
        }

        //When you swipe a card to the device, this event will be triggered to show you the number of the card.
        private void axCZKEM1_OnHIDNum(int iCardNumber)
        {
            lbRTShow.Items.Add("RTEvent OnHIDNum Has been Triggered...");
            lbRTShow.Items.Add("...Cardnumber=" + iCardNumber.ToString());
        }

        //When you have emptyed the Mifare card,this event will be triggered.
        private void axCZKEM1_OnEmptyCard(int iActionResult)
        {
            lbRTShow.Items.Add("RTEvent OnEmptyCard Has been Triggered...");
            if (iActionResult == 0)
            {
                lbRTShow.Items.Add("...Empty Mifare Card OK");
            }
            else
            {
                lbRTShow.Items.Add("...Empty Failed");
            }
        }

        //When you have written into the Mifare card ,this event will be triggered.
        private void axCZKEM1_OnWriteCard(int iEnrollNumber, int iActionResult, int iLength)
        {
            lbRTShow.Items.Add("RTEvent OnWriteCard Has been Triggered...");
            if (iActionResult == 0)
            {
                lbRTShow.Items.Add("...Write Mifare Card OK");
                lbRTShow.Items.Add("...EnrollNumber=" + iEnrollNumber.ToString());
                lbRTShow.Items.Add("...TmpLength=" + iLength.ToString());
            }
            else
            {
                lbRTShow.Items.Add("...Write Failed");
            }
        }

        //After you swipe your card to the device,this event will be triggered.
        //If your card passes the verification,the return value  will be user id, or else the value will be -1
        private void axCZKEM1_OnVerify(int iUserID)
        {
            lbRTShow.Items.Add("RTEvent OnVerify Has been Triggered,Verifying...");
            if (iUserID != -1)
            {
                lbRTShow.Items.Add("Verified OK,the UserID is " + iUserID.ToString());
            }
            else
            {
                lbRTShow.Items.Add("Verified Failed... ");
            }
        }

        //If your card passes the verification,this event will be triggered
        private void axCZKEM1_OnAttTransactionEx(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
            lbRTShow.Items.Add("RTEvent OnAttTrasactionEx Has been Triggered,Verified OK");
            lbRTShow.Items.Add("...UserID:" + sEnrollNumber);
            lbRTShow.Items.Add("...isInvalid:" + iIsInValid.ToString());
            lbRTShow.Items.Add("...attState:" + iAttState.ToString());
            lbRTShow.Items.Add("...VerifyMethod:" + iVerifyMethod.ToString());
            lbRTShow.Items.Add("...Workcode:" + iWorkCode.ToString());//the difference between the event OnAttTransaction and OnAttTransactionEx
            lbRTShow.Items.Add("...Time:" + iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString());

            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;
            string sCardnumber = "";

            while (axCZKEM1.SSR_GetUserInfo(iMachineNumber, sEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get user information from memory
            {
                if (axCZKEM1.GetStrCardNumber(out sCardnumber))//get the card number from the memory
                {
                    lbRTShow.Items.Add("...Cardnumber on device:" + sCardnumber);
                    return;
                }
            }
        }

        //After function GetRTLog() is called ,RealTime Events will be triggered. 
        //When you are using these two functions, it will request data from the device forwardly.
        private void rtTimer_Tick(object sender, EventArgs e)
        {
            if (axCZKEM1.ReadRTLog(iMachineNumber))
            {
                while (axCZKEM1.GetRTLog(iMachineNumber))
                {
                    ;
                }
            }
        }

        #endregion

        /**************************************************************************************************
        * This part is for demonstrating  operations on card(ID card and HID card) device.                *
        * It shows how to get or set card number,how to write data to Mifare card or empty it, etc.       *
        * *************************************************************************************************/
        #region Card Operation

        //It is mainly for demonstrating how to download the cardnumber from the device.
        //Card number is part of the user information.
        private void btnGetStrCardNumber_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            string sdwEnrollNumber = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;
            string sCardnumber = "";

            lvCard.Items.Clear();
            lvCard.BeginUpdate();
            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get user information from memory
            {
                if (axCZKEM1.GetStrCardNumber(out sCardnumber))//get the card number from the memory
                {
                    ListViewItem list = new ListViewItem();
                    list.Text = sdwEnrollNumber;
                    list.SubItems.Add(sName);
                    list.SubItems.Add(sCardnumber);
                    list.SubItems.Add(iPrivilege.ToString());
                    list.SubItems.Add(sPassword);
                    if (bEnabled == true)
                    {
                        list.SubItems.Add("true");
                    }
                    else
                    {
                        list.SubItems.Add("false");
                    }
                    lvCard.Items.Add(list);
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            lvCard.EndUpdate();
            Cursor = Cursors.Default;
        }

        //Upload the cardnumber as part of the user information
        private void btnSetStrCardNumber_Click(object sender, EventArgs e)
        {

            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (cbUserId_Card.Text.Trim() == "" || cbPrivilege.Text.Trim() == "" || txtCardnumber.Text.Trim() == "")
            {
                MessageBox.Show("UserID,Privilege,Cardnumber must be inputted first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            bool bEnabled = true;
            if (chbEnabled.Checked)
            {
                bEnabled = true;
            }
            else
            {
                bEnabled = false;
            }
            string sName = txtName.Text.Trim();
            string sPassword = txtPassword.Text.Trim();
            int iPrivilege = Convert.ToInt32(cbPrivilege.Text.Trim());
            string sCardnumber = txtCardnumber.Text.Trim();

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            string sdwEnrollNumber = cbUserId_Card.Text.Trim();
            axCZKEM1.SetStrCardNumber(sCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device
            if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload the user's information(card number included)
            {
                MessageBox.Show("(SSR_)SetUserInfo,UserID:" + sdwEnrollNumber + " Privilege:" + iPrivilege.ToString() + " Enabled:" + bEnabled.ToString(), "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;
        }
        
        #endregion

        #region AttLogs

        //Download the attendance records from the device(For both Black&White and TFT screen devices).
        private void btnGetGeneralLogData_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sdwEnrollNumber = "";
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;

            int iGLCount = 0;
            int iIndex = 0;

            Cursor = Cursors.WaitCursor;
            lvLogs.Items.Clear();
            axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
            if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
            {
                while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                            out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                {
                    iGLCount++;
                    lvLogs.Items.Add(iGLCount.ToString());
                    lvLogs.Items[iIndex].SubItems.Add(sdwEnrollNumber);//modify by Darcy on Nov.26 2009
                    lvLogs.Items[iIndex].SubItems.Add(idwVerifyMode.ToString());
                    lvLogs.Items[iIndex].SubItems.Add(idwInOutMode.ToString());
                    lvLogs.Items[iIndex].SubItems.Add(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString());
                    lvLogs.Items[iIndex].SubItems.Add(idwWorkcode.ToString());
                    iIndex++;
                }
            }
            else
            {
                Cursor = Cursors.Default;
                axCZKEM1.GetLastError(ref idwErrorCode);

                if (idwErrorCode != 0)
                {
                    MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
                }
                else
                {
                    MessageBox.Show("No data from terminal returns!", "Error");
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            Cursor = Cursors.Default;
        }

        //Clear all attendance records from terminal
        private void btnClearGLog_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            lvLogs.Items.Clear();
            axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
            if (axCZKEM1.ClearGLog(iMachineNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("All att Logs have been cleared from teiminal!", "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
        }

        //Get the count of attendance records in from ternimal
        private void btnGetDeviceStatus_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;
            int iValue = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
            if (axCZKEM1.GetDeviceStatus(iMachineNumber, 6, ref iValue)) //Here we use the function "GetDeviceStatus" to get the record's count.The parameter "Status" is 6.
            {
                MessageBox.Show("The count of the AttLogs in the device is " + iValue.ToString(), "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
        }
        #endregion
      
    }
}
