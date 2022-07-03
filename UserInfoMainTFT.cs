using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UserInfo.Class;

namespace UserInfo
{
    public partial class TFT : Form
    {
        public TFT()
        {

            InitializeComponent();

            ///MessageBox.Show("Request you to provide device name, model, ip and, port", "Error");
            //MessageBox.Show("Loading useful information. Request you to wait.", "Info");
            //Check whether the license of the organisation is expired or not.
            //If expired, show the message and ask them to renew it.
            //If valid, load the form to proceed further.
            string strValidMsg = "";
            Boolean isValid = APILayer.ValidateLicenseKey(out strValidMsg);
            if (isValid)
            {
                //Once the license is valid, we will get the devices for the gym.
                string strFetchedMsg = "";
                Boolean isFetched = APILayer.GetAddedDevices(out strFetchedMsg);

                if (isFetched)
                {
                    if (!String.IsNullOrEmpty(strFetchedMsg))
                        CloseApplication(strFetchedMsg);
                    else
                    {
                        //Set Gym Name 
                        lblGymName.Text = DS.DS.gymObj.gymName;

                        //Getting Added users for the gym.
                        string strUserMsg = "";
                        if (APILayer.GetAddedUsers(out strUserMsg))
                        {
                            isUserFetchedFromDB = true;
                            BindUsersGridView();
                        }
                        else
                        {
                            //Problem occured while fetching the devices of the gym
                            CloseApplication(strUserMsg);
                        }
                        //Devices Data is fetched.
                        CheckDevicesAndConnect();

                        //Fill the mandatory labels
                        lblLicenseValidVal.Text = DS.DS.gymObj.expiryDt;
                        lblPurchasedVal.Text = DS.DS.gymObj.subscriptionDt;

                        //Close Other Tabs
                        tab.TabPages.Remove(tabPageFT);
                        tab.TabPages.Remove(tabPageCardReader);
                        tab.TabPages.Remove(tabPageLogR);
                    }
                }
                else
                {
                    //Problem occured while fetching the devices of the gym
                    CloseApplication(strFetchedMsg);
                }

                //There will be the thread which will check the validity of the persons coming to gym. 
                //If the persons validity has expired, remove his access in the system.
                Thread thVerify = new Thread(new ThreadStart(VerifyGymAndCustomers));
                thVerify.IsBackground = true;
                thVerify.Start();

                //There will be the thred which will be sendig the attendance information of the user.
                Thread theAttendance = new Thread(new ThreadStart(SendAttendanceInfo));
                theAttendance.IsBackground = true;
                theAttendance.Start();

                //There will be the thread that will validate system datetime with sever datetime.
                //In case of difference greater than 5 mins, it will close the application
                //Thread thDateTime = new Thread(new ThreadStart(ValidateSystemDateTime));
                //thDateTime.IsBackground = true;
                //thDateTime.Start();

                Common.ResizeGridView(gridviewDevices);
                Common.ResizeGridView(gridviewUsers);

            }
            else
            {
                //This is done so that when there is a problem with license, we will simply
                //inform the user about the same and on click of okay, we will close the form.
                CloseApplication(strValidMsg);
            }
        }

        //Variables which require global Scope
        Boolean isUserFetchedFromDB = false;
        private readonly object dicUserLock = new object(); //Lock variable. Because the user info will be shared in two threads.
        private readonly object listAttendanceLock = new object(); //Lock variable. Attendance info will be sdhared in two threads.
        private static DateTime dtLastAttendanceSync = System.DateTime.Now; //The date tells us the attendance synced last.

        //Create Standalone SDK class dynamicly.
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

        #region Threads

        //The below thread will verify the gyms and its customers.
        //Whenever the apllication is started customers will be verified and if application is running
        //in midmight we will verify the gym license as well as members fingerprints.
        public void VerifyGymAndCustomers()
        {
            try
            {
                DateTime dtCurrent, dtTomorrow;
                int dtSeconds;
                string strValidateGym = "", strValidateCustomer = ""; Boolean validateGym = true, validateCustomers = true, removeUser = false;
                while (true)
                {
                    //The logic is written that at every midnight we will be validating gym and customers expirt dt.
                    dtCurrent = APILayer.GetCurrentDateTime();
                    dtTomorrow = dtCurrent.AddDays(1).Date;
                    dtSeconds = Convert.ToInt32((dtTomorrow - dtCurrent).TotalSeconds);

                    //If the difference between Expiry Date and todays date is 15 days,
                    //make licence label as red
                    if ((Convert.ToDateTime(DS.DS.gymObj.expiryDt) - dtCurrent).Days <= 15)
                    {
                        lblLicenseValidVal.ForeColor = Color.Red;
                    }

                    //Validate gym when the expiry date has reached
                    if (Convert.ToDateTime(DS.DS.gymObj.expiryDt) < dtCurrent)
                    {
                        //Logic for validate customers and gym license -- start
                        validateGym = APILayer.ValidateLicenseKey(out strValidateGym);
                    }
                    if (validateGym)
                    {
                        //Gym is validated. Now validate the customers

                        //Create local copy of the dictionary.This is done because I don't want to lock dictionary users.
                        Dictionary<int, DS.User> dicUserCopy = new Dictionary<int, DS.User>();

                        lock (dicUserLock)
                        {
                            foreach (KeyValuePair<int, DS.User> user in DS.DS.dicUsers)
                            {
                                dicUserCopy.Add(user.Key, user.Value);
                            }
                        }
                        foreach (KeyValuePair<int, DS.User> user in dicUserCopy)
                        {
                            //Check for the users with the expiry date.
                            if ((dtCurrent - Convert.ToDateTime(user.Value.expiryDt)).TotalHours > 24)
                            {
                                //Check the user has access to gym or not. In case he has access to gym remove him.
                                if (user.Value.inDevice != 0)
                                {
                                    removeUser = true;
                                    //Check for the user before expiry it.. Checks his expiry has been extended or not
                                    validateCustomers = APILayer.ValidateUser(out strValidateCustomer, user.Value.id);
                                    if (validateCustomers)
                                    {
                                        //Check for his new expiry date
                                        lock (dicUserLock)
                                        {
                                            if ((dtCurrent - Convert.ToDateTime(DS.DS.dicUsers[user.Value.id].expiryDt)).TotalHours > 24)
                                            {
                                                removeUser = true;
                                            }
                                            else
                                                removeUser = false;
                                        }
                                    }

                                    if (removeUser)
                                    {
                                        foreach (DS.Device device in DS.DS.lstDevices)
                                        {
                                            //Remove user from device
                                            Cursor = Cursors.WaitCursor;
                                            axCZKEM1.EnableDevice(device.id, false);//disable the device
                                            if (axCZKEM1.SSR_EnableUser(device.id, user.Value.id.ToString(), false))
                                            {
                                                axCZKEM1.RefreshData(device.id);//the data in the device should be refreshed
                                            }
                                            else
                                            {
                                                int errorCode = -1;
                                                axCZKEM1.GetLastError(ref errorCode);
                                            }
                                            axCZKEM1.EnableDevice(device.id, true);//enable the device
                                            Cursor = Cursors.Default;
                                        }

                                        //Reset value in ,main dictionary and local copy
                                        lock (dicUserLock)
                                        {
                                            DS.DS.dicUsers[user.Value.id].inDevice = 1;
                                            DS.DS.dicUsers[user.Value.id].enabled = 0;
                                            user.Value.inDevice = 1;
                                            user.Value.enabled = 0;
                                        }

                                        //Update in API
                                        APILayer.UpdateUserInDb(gymId: DS.DS.gymObj.gymId, userId: user.Value.id, userName: user.Value.name, userType: user.Value.type,
                                                                inDevice: user.Value.inDevice, expiryDt: user.Value.expiryDt, dt: user.Value.date,
                                                                enabled: user.Value.enabled);
                                        //Bind Users Gridview
                                        //BindUsersGridView();

                                    }
                                }
                                else
                                {
                                    lock (dicUserLock)
                                    {
                                        DS.DS.dicUsers[user.Value.id].inDevice = 0;
                                        DS.DS.dicUsers[user.Value.id].enabled = 0;
                                        user.Value.inDevice = 0;
                                        user.Value.enabled = 0;
                                    }

                                    //Update in API
                                    APILayer.UpdateUserInDb(gymId: DS.DS.gymObj.gymId, userId: user.Value.id, userName: user.Value.name, userType: user.Value.type,
                                                            inDevice: user.Value.inDevice, expiryDt: user.Value.expiryDt, dt: user.Value.date,
                                                            enabled: user.Value.enabled);
                                    //Bind Users Gridview
                                    // BindUsersGridView();
                                }
                            }
                        }

                        //Bind Users Gridview
                        BindUsersGridView();
                        //Clear local dictionary to tell GC to collect it.
                        // dicUserCopy = null;
                    }
                    else
                    {
                        //This is done so that when there is a problem with license, we will simply
                        //inform the user about the same and on click of okay, we will close the form.
                        CloseApplication(strValidateGym);
                    }
                    //Logic for validate customers and gym license -- end

                    strValidateGym = ""; strValidateCustomer = ""; validateGym = true; validateCustomers = true;
                    Thread.Sleep(dtSeconds * 1000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //There will be the thread that will validate system datetime with sever datetime.
        //In case of difference greater than 5 mins, it will close the application
        public void ValidateSystemDateTime()
        {
            try
            {
                DateTime dtLastCurrentDateFetched = System.DateTime.MinValue;
                while (true)
                {
                    dtLastCurrentDateFetched = APILayer.GetCurrentDateTime(); //The datetime value will be used to sync system date with the server date.
                    //Check the System date on which this process is running is valid or not

                    if (Math.Abs((System.DateTime.Now - dtLastCurrentDateFetched).TotalMinutes) > 5)
                    {
                        CloseApplication("Request you to set dateTime of your machine to current datetime.");
                    }

                    Thread.Sleep(5 * 60 * 1000);
                }

            }
            catch (Exception ex)
            {

            }
        }

        //The function will be used to share attendance info of users to database
        public void SendAttendanceInfo()
        {
            string strAttendanceInfo = ""; int count = 0;

            //Get the attendance sync date from the database for the first time when app started.
            dtLastAttendanceSync = APILayer.GetAttendanceSyncDateTime();

            //Set the data sync label
            lblDataSyncVal.Text = dtLastAttendanceSync.ToString("dd-MM-yyyy HH:mm:ss");

            //Get all the attendance details stored in the device. 
            GetAttendanceDetails();

            //Logic is difference between last attendance info shared is 5 mins or 25 users

            while (true)
            {
                lock (listAttendanceLock)
                {
                    count = DS.DS.lstAttendance.Count;
                }
                if ((System.DateTime.Now - dtLastAttendanceSync).TotalMinutes >= 5 || count >= 50)
                {
                    //Share Attendance details
                    lock (listAttendanceLock)
                    {
                        strAttendanceInfo = JsonConvert.SerializeObject(DS.DS.lstAttendance);

                        //Clears the list;
                        DS.DS.lstAttendance.Clear();
                    }

                    //Share attendance info
                    APILayer.LogAttendanceInfo(strAttendanceInfo);

                    lblDataSyncVal.Text = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                    //Last attendance sync date 
                    dtLastAttendanceSync = System.DateTime.MinValue;

                    //Save sync date in database
                    APILayer.SaveAttendanceDate(dtLastAttendanceSync);

                    //Sleeps the thread for 1 minute
                    Thread.Sleep(60 * 1000);
                }
                else
                {
                    Thread.Sleep(30 * 1000);
                }
            }
        }

        #endregion

        #region Functions

        //The below function will check for the  added devices in the system.
        //If added, will try to connect them.
        public void CheckDevicesAndConnect()
        {
            try
            {
                if (DS.DS.lstDevices.Count > 0)
                {
                    foreach (DS.Device device in DS.DS.lstDevices)
                    {
                        Boolean isConnected = false;
                        //Try to connect with devices.
                        if (device.status == Common.deviceDisconnected)
                        {
                            isConnected = axCZKEM1.Connect_Net(device.ip, device.port);
                            if (isConnected)
                            {
                                device.status = Common.deviceConnected;
                                //Do register for realtime events for the device, will help us in preparing realtime logs
                                if (axCZKEM1.RegEvent(device.id, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                                {
                                    //this.axCZKEM1.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                                    this.axCZKEM1.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                                    //this.axCZKEM1.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
                                    //this.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
                                    //this.axCZKEM1.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
                                    //this.axCZKEM1.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
                                }
                                bIsConnected = true;
                            }
                            else
                            {
                                int errorCode = -1;
                                axCZKEM1.GetLastError(ref errorCode);
                                MessageBox.Show("Error Message: " + errorCode.ToString() + "." + Environment.NewLine + Common.GetErrorMessages(errorCode) + "." + Environment.NewLine +
                                                "Request you to take necessary steps and try in sometime.", "Error");
                            }
                        }
                    }
                }
                //Bind the devices gridview
                BindDevicesGridView();
            }
            catch (Exception ex)
            {

            }
        }

        public void BindDevicesGridView()
        {
            try
            {
                int rowIndex = 0;
                gridviewDevices.Rows.Clear();
                DS.DS.lstDevices.Sort(delegate (DS.Device c1, DS.Device c2) { return c1.id.CompareTo(c2.id); });
                foreach (DS.Device device in DS.DS.lstDevices)
                {
                    rowIndex = gridviewDevices.Rows.Add();
                    gridviewDevices.Rows[rowIndex].Cells[0].Value = device.id;
                    gridviewDevices.Rows[rowIndex].Cells[1].Value = device.name;
                    gridviewDevices.Rows[rowIndex].Cells[2].Value = device.model;
                    gridviewDevices.Rows[rowIndex].Cells[3].Value = device.ip;
                    gridviewDevices.Rows[rowIndex].Cells[4].Value = device.port;
                    if (device.status == Common.deviceDisconnected)
                        gridviewDevices.Rows[rowIndex].Cells[5].Style.ForeColor = Color.Red;
                    else
                        gridviewDevices.Rows[rowIndex].Cells[5].Style.ForeColor = Color.Green;
                    gridviewDevices.Rows[rowIndex].Cells[5].Value = device.status;
                    gridviewDevices.Rows[rowIndex].Cells[6].Value = device.date;
                }

                Common.ResizeGridView(gridviewDevices);

                //Reset Gridview Selection
                gridviewDevices.ClearSelection();

                #region Gridview Styling
                gridviewDevices.BorderStyle = BorderStyle.None;
                gridviewDevices.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                gridviewDevices.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                gridviewDevices.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                gridviewDevices.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                gridviewDevices.BackgroundColor = Color.White;

                gridviewDevices.EnableHeadersVisualStyles = false;
                gridviewDevices.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                gridviewDevices.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                gridviewDevices.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        public void BindUsersGridView()
        {
            try
            {
                int rowIndex = 0;
                gridviewUsers.Rows.Clear();

                lock (dicUserLock)
                {
                    foreach (KeyValuePair<int, DS.User> dicUser in DS.DS.dicUsers)
                    {
                        rowIndex = gridviewUsers.Rows.Add();
                        gridviewUsers.Rows[rowIndex].Cells[0].Value = dicUser.Value.id;
                        gridviewUsers.Rows[rowIndex].Cells[1].Value = dicUser.Value.name;
                        gridviewUsers.Rows[rowIndex].Cells[2].Value = dicUser.Value.type;
                        if (dicUser.Value.inDevice == 1)
                            gridviewUsers.Rows[rowIndex].Cells[3].Style.ForeColor = Color.Green;
                        else
                            gridviewUsers.Rows[rowIndex].Cells[3].Style.ForeColor = Color.Red;
                        gridviewUsers.Rows[rowIndex].Cells[3].Value = dicUser.Value.inDevice;
                        if (dicUser.Value.enabled == 1)
                            gridviewUsers.Rows[rowIndex].Cells[4].Style.ForeColor = Color.Green;
                        else
                            gridviewUsers.Rows[rowIndex].Cells[4].Style.ForeColor = Color.Red;
                        gridviewUsers.Rows[rowIndex].Cells[4].Value = dicUser.Value.enabled;
                        gridviewUsers.Rows[rowIndex].Cells[5].Value = dicUser.Value.expiryDt;
                        gridviewUsers.Rows[rowIndex].Cells[6].Value = dicUser.Value.date;
                    }
                }

                Common.ResizeGridView(gridviewUsers);

                //Reset Gridview Selection
                gridviewUsers.ClearSelection();

                #region Gridview Styling
                gridviewUsers.BorderStyle = BorderStyle.None;
                gridviewUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                gridviewUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                gridviewUsers.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                gridviewUsers.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                gridviewUsers.BackgroundColor = Color.White;

                gridviewUsers.EnableHeadersVisualStyles = false;
                gridviewUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                gridviewUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                gridviewUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        public void CloseApplication(string errorMsg)
        {
            DialogResult dialog = MessageBox.Show(errorMsg, "Error");
            if (dialog == DialogResult.OK)
                this.Close();
        }

        public void GetAttendanceDetails()
        {
            try
            {
                foreach (DS.Device device in DS.DS.lstDevices)
                {
                    //Check Device is connected or not 
                    if (device.status == Common.deviceConnected)
                    {
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
                        int idwErrorCode = 0;
                        int iGLCount = 0;
                        int iIndex = 0;
                        DateTime dt = System.DateTime.MinValue;

                        Cursor = Cursors.WaitCursor;

                        axCZKEM1.EnableDevice(device.id, false);//disable the device
                        if (axCZKEM1.ReadGeneralLogData(device.id))//read all the attendance records to the memory
                        {
                            while (axCZKEM1.SSR_GetGeneralLogData(device.id, out sdwEnrollNumber, out idwVerifyMode,
                                                                  out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour,
                                                                  out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                            {
                                iGLCount++;

                                //Store the real time attendance information in the list
                                DS.AttendanceInfo attendanceInfo = new DS.AttendanceInfo();
                                attendanceInfo.uid = Convert.ToInt32(sdwEnrollNumber);
                                attendanceInfo.time = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " +
                                                      idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString();

                                dt = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);

                                if (dt >= dtLastAttendanceSync)
                                {
                                    //Log attendance in List
                                    lock (listAttendanceLock)
                                    {
                                        DS.DS.lstAttendance.Add(attendanceInfo);
                                    }
                                }

                                iIndex++;
                            }
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            axCZKEM1.GetLastError(ref idwErrorCode);

                            if (idwErrorCode != 0)
                            {

                            }
                            else
                            {

                            }
                        }
                        axCZKEM1.EnableDevice(device.id, true);//enable the device
                        Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        /********************************************************************************************************************************************
        * Before you refer to this demo,we strongly suggest you read the development manual deeply first.                                           *
        * This part is for demonstrating the communication with your device.There are 3 communication ways: "TCP/IP","Serial Port" and "USB Client".*
        * The communication way which you can use duing to the model of the device.                                                                 *
        * *******************************************************************************************************************************************/
        #region Communication
        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber = 1;//the serial number of the device.After connecting the device ,this value will be changed.
        private static Int32 MyCount;
        //If your device supports the TCP/IP communications, you can refer to this.
        //when you are using the tcp/ip communication,you can distinguish different devices by their IP address.
        //private void btnConnect_Click(object sender, EventArgs e)
        //{
        //    if (txtIP.Text.Trim() == "" || txtPort.Text.Trim() == "")
        //    {
        //        MessageBox.Show("IP and Port cannot be null", "Error");
        //        return;
        //    }
        //    int idwErrorCode = 0;
        //    Cursor = Cursors.WaitCursor;
        //    if (btnConnect.Text == "DisConnect")
        //    {
        //        axCZKEM1.Disconnect();

        //        this.axCZKEM1.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
        //        this.axCZKEM1.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
        //        this.axCZKEM1.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
        //        this.axCZKEM1.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
        //        this.axCZKEM1.OnWriteCard -= new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
        //        this.axCZKEM1.OnEmptyCard -= new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);

        //        bIsConnected = false;
        //        btnConnect.Text = "Connect";
        //        lblState.Text = "Current State:DisConnected";
        //        Cursor = Cursors.Default;
        //        return;
        //    }

        //    //int pass = 123456;
        //    //bool aa = axCZKEM1.SetCommPassword(pass);
        //    bIsConnected = axCZKEM1.Connect_Net(txtIP.Text, Convert.ToInt32(txtPort.Text));
        //    if (bIsConnected == true )
        //    {
        //        btnConnect.Text = "DisConnect"; 
        //        btnConnect.Refresh();
        //        lblState.Text = "Current State:Connected";
        //        iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
        //        if (axCZKEM1.RegEvent(iMachineNumber, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
        //        {
        //            this.axCZKEM1.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
        //            this.axCZKEM1.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
        //            this.axCZKEM1.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
        //            this.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
        //            this.axCZKEM1.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
        //            this.axCZKEM1.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
        //        } 

        //        MyCount = 1;
        //    }
        //    else
        //    {
        //        axCZKEM1.GetLastError(ref idwErrorCode);
        //        MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Error");
        //    }
        //    Cursor = Cursors.Default;
        //}

        //If your device supports the SerialPort communications, you can refer to this.
        //private void btnRsConnect_Click(object sender, EventArgs e)
        //{
        //    if (cbPort.Text.Trim() == "" || cbBaudRate.Text.Trim() == "" || txtMachineSN.Text.Trim() == "")
        //    {
        //        MessageBox.Show("Port,BaudRate and MachineSN cannot be null", "Error");
        //        return;
        //    }
        //    int idwErrorCode = 0;
        //    //accept serialport number from string like "COMi"
        //    int iPort;
        //    string sPort = cbPort.Text.Trim();
        //    for (iPort = 1; iPort < 10; iPort++)
        //    {
        //        if (sPort.IndexOf(iPort.ToString()) > -1)
        //        {
        //            break;
        //        }
        //    }

        //    Cursor = Cursors.WaitCursor;
        //    if (btnRsConnect.Text == "Disconnect")
        //    {
        //        axCZKEM1.Disconnect();

        //        this.axCZKEM1.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
        //        this.axCZKEM1.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
        //        this.axCZKEM1.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
        //        this.axCZKEM1.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
        //        this.axCZKEM1.OnWriteCard -= new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
        //        this.axCZKEM1.OnEmptyCard -= new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);

        //        bIsConnected = false;
        //        btnRsConnect.Text = "Connect";
        //        btnRsConnect.Refresh();
        //        lblState.Text = "Current State:Disconnected";
        //        Cursor = Cursors.Default;
        //        return;
        //    }

        //    iMachineNumber = Convert.ToInt32(txtMachineSN.Text.Trim());//when you are using the serial port communication,you can distinguish different devices by their serial port number.
        //    bIsConnected = axCZKEM1.Connect_Com(iPort, iMachineNumber, Convert.ToInt32(cbBaudRate.Text.Trim()));

        //    if (bIsConnected == true)
        //    {
        //        btnRsConnect.Text = "Disconnect";
        //        btnRsConnect.Refresh();
        //        lblState.Text = "Current State:Connected";

        //        if (axCZKEM1.RegEvent(iMachineNumber, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
        //        {
        //            this.axCZKEM1.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
        //            this.axCZKEM1.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
        //            this.axCZKEM1.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
        //            this.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
        //            this.axCZKEM1.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
        //            this.axCZKEM1.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
        //        }
        //    }
        //    else
        //    {
        //        axCZKEM1.GetLastError(ref idwErrorCode);
        //        MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Error");
        //    }

        //    Cursor = Cursors.Default;
        //}

        //If your device supports the USBCLient, you can refer to this.
        //Not all series devices can support this kind of connection.Please make sure your device supports USBClient.
        //Connect the device via the virtual serial port created by USBClient
        //private void btnUSBConnect_Click(object sender, EventArgs e)
        //{
        //    int idwErrorCode = 0;

        //    Cursor = Cursors.WaitCursor;

        //    if (btnUSBConnect.Text == "Disconnect")
        //    {
        //        axCZKEM1.Disconnect();

        //        this.axCZKEM1.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
        //        this.axCZKEM1.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
        //        this.axCZKEM1.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
        //        this.axCZKEM1.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
        //        this.axCZKEM1.OnWriteCard -= new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
        //        this.axCZKEM1.OnEmptyCard -= new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);

        //        bIsConnected = false;
        //        btnUSBConnect.Text = "Connect";
        //        btnUSBConnect.Refresh();
        //        lblState.Text = "Current State:Disconnected";
        //        Cursor = Cursors.Default;
        //        return;
        //    }

        //    SearchforUSBCom usbcom = new SearchforUSBCom();
        //    string sCom = "";
        //    bool bSearch = usbcom.SearchforCom(ref sCom);//modify by Darcy on Nov.26 2009
        //    if (bSearch == false)//modify by Darcy on Nov.26 2009
        //    {
        //        MessageBox.Show("Can not find the virtual serial port that can be used", "Error");
        //        Cursor = Cursors.Default;
        //        return;
        //    }

        //    int iPort;
        //    for (iPort = 1; iPort < 10; iPort++)
        //    {
        //        if (sCom.IndexOf(iPort.ToString()) > -1)
        //        {
        //            break;
        //        }
        //    }

        //    iMachineNumber = Convert.ToInt32(txtMachineSN2.Text.Trim());
        //    if (iMachineNumber == 0 || iMachineNumber > 255)
        //    {
        //        MessageBox.Show("The Machine Number is invalid!", "Error");
        //        Cursor = Cursors.Default;
        //        return;
        //    }

        //    int iBaudRate = 115200;//115200 is one possible baudrate value(its value cannot be 0)
        //    bIsConnected = axCZKEM1.Connect_Com(iPort, iMachineNumber, iBaudRate);

        //    if (bIsConnected == true)
        //    {
        //        btnUSBConnect.Text = "Disconnect";
        //        btnUSBConnect.Refresh();
        //        lblState.Text = "Current State:Connected";

        //        if (axCZKEM1.RegEvent(iMachineNumber, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
        //        {
        //            this.axCZKEM1.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
        //            this.axCZKEM1.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
        //            this.axCZKEM1.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
        //            this.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
        //            this.axCZKEM1.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
        //            this.axCZKEM1.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
        //        }
        //    }
        //    else
        //    {
        //        axCZKEM1.GetLastError(ref idwErrorCode);
        //        MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Error");
        //    }

        //    Cursor = Cursors.Default;
        //}

        #endregion

        /*************************************************************************************************
        * Before you refer to this demo,we strongly suggest you read the development manual deeply first.*
        * This part is for demonstrating operations with user(download/upload/delete/clear/modify).      *
        * ************************************************************************************************/
        #region UserInfo

        //Download user's 9.0 or 10.0 arithmetic fingerprint templates(in strings)
        //Only TFT screen devices with firmware version Ver 6.60 version later support function "GetUserTmpExStr" and "GetUserTmpEx".
        //While you are using 9.0 fingerprint arithmetic and your device's firmware version is under ver6.60,you should use the functions "SSR_GetUserTmp" or 
        //"SSR_GetUserTmpStr" instead of "GetUserTmpExStr" or "GetUserTmpEx" in order to download the fingerprint templates.
        private void btnDownloadTmp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(iMachineNumber.ToString());

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

            //string serialNo = "";
            //axCZKEM1.GetSerialNumber(iMachineNumber, out serialNo);// serial no of machine

            lvDownload.Items.Clear();
            lvDownload.BeginUpdate();

            axCZKEM1.EnableDevice(iMachineNumber, false);
            Cursor = Cursors.WaitCursor;

            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory

            axCZKEM1.IsTFTMachine(iMachineNumber);   // to distingush machines

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

            if (cbUserIDTmp.Text.Trim() == "" || cbFingerIndex.Text.Trim() == "")
            {
                MessageBox.Show("Please input the UserID and FingerIndex first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = cbUserIDTmp.Text.Trim();
            int iFingerIndex = Convert.ToInt32(cbFingerIndex.Text.Trim());

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

        //add by Darcy on Nov.23 2009
        //Add the existed userid to DropDownLists.
        bool bAddControl = true;
        private void UserIDTimer_Tick(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                cbUserIDDE.Items.Clear();
                cbUserIDTmp.Items.Clear();
                cbUserId_Card.Items.Clear();
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
        #region Ms-Access Database

        private void DownloadFPTemDB_Click(object sender, EventArgs e)
        {
            if (MyCount == 1)
            {
                if (bIsConnected == false)
                {
                    MessageBox.Show("Please connect the device first!", "Error");
                    return;
                }

                string sName = "", sPassword = "", sTmpData = "", sdwEnrollNumber = "";
                int iPrivilege = 0, idwFingerIndex = 0, iTmpLength = 0, iFlag = 0;
                bool bEnabled = false;
                MyCount = -1;

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
                            uicobj.Insert_User_DetailsTFT(sdwEnrollNumber, sName, idwFingerIndex, sTmpData, iPrivilege, sPassword, bEnabled, iFlag);
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
            DataTable dt = objupload.UploadDataTFT();

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
            obj.DeleteAllEmpTmTFT();
            MessageBox.Show("Successfully Data deleted from database");
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

            string sdwEnrollNumber = "";
            int idwTMachineNumber = 0;
            int idwEMachineNumber = 0;
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;

            int idwErrorCode = 0;
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

        //Clear all attendance records from terminal (For both Black&White and TFT screen devices).
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

        //Get the count of attendance records in from ternimal(For both Black&White and TFT screen devices).
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

        /**************************************************************************************************
        * Before you refer to this demo,we strongly suggest you read the development manual deeply first. *
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

        #region Card Operation

        private void btnGetHIDEventCardNumAsStr_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;
            string sstrHIDEventCardNum = "";

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetHIDEventCardNumAsStr(out sstrHIDEventCardNum))
            {
                MessageBox.Show("GetHIDEventCardNumAsStr!HIDCardNum=" + sstrHIDEventCardNum.ToString(), "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;

        }

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
                    list.Text = sdwEnrollNumber.ToString().Trim();
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
            string sdwEnrollNumber = cbUserId_Card.Text.Trim();
            string sName = txtName.Text.Trim();
            string sPassword = txtPassword.Text.Trim();
            int iPrivilege = Convert.ToInt32(cbPrivilege.Text.Trim());
            string sCardnumber = txtCardnumber.Text.Trim();

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            axCZKEM1.SetStrCardNumber(sCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device
            if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload the user's information(card number included)
            {
                MessageBox.Show("SetUserInfo,UserID:" + sdwEnrollNumber + " Privilege:" + iPrivilege.ToString() + " Enabled:" + bEnabled.ToString(), "Success");
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

        #region DeviceManagementRegion

        int deviceId = -1; //This device ID will be used for updating/deleting device information under device management.

        //The function will be used to add new Devices in the System.
        private void btnAddNewDevice_Click(object sender, EventArgs e)
        {
            //if (DS.DS.lstDevices.Count < 1)
            {
                if (txtDeviceName.Text != "" && txtDeviceModel.Text != "" && txtDeviceIP.Text != "" && txtDevicePort.Text != "")
                {
                    //Get the unique device Id first
                    int deviceId = Common.GenerateDeviceId();
                    //Device Id -1 means some exception has come in while generating the device ID
                    if (deviceId == -1)
                    {
                        MessageBox.Show("Some error occured while generating the Device Id. Request you to try in sometime", "Error");
                    }
                    else
                    {
                        string resMsg = ""; int serverDeviceId = -1;

                        string date = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                        Boolean isAdded = APILayer.AddNewDevices(out resMsg, out serverDeviceId, DS.DS.gymObj.gymId, deviceId, txtDeviceName.Text,
                                                                txtDeviceModel.Text, txtDeviceIP.Text, txtDevicePort.Text, date);
                        if (isAdded)
                        {
                            MessageBox.Show("Device has been added successfully", "Success");
                            //Add device in the list
                            DS.Device device = new DS.Device();
                            device.id = serverDeviceId;
                            device.name = txtDeviceName.Text;
                            device.model = txtDeviceModel.Text;
                            device.ip = txtDeviceIP.Text;
                            device.port = Convert.ToInt32(txtDevicePort.Text);
                            device.status = Common.deviceDisconnected;
                            device.date = date;

                            DS.DS.lstDevices.Add(device);

                            CheckDevicesAndConnect();

                            //Clear all fields 
                            txtDeviceName.Clear();
                            txtDevicePort.Clear();
                            txtDeviceIP.Clear();
                            txtDeviceModel.Clear();
                        }
                        else
                        {
                            MessageBox.Show(resMsg, "Error");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Request you to provide device name, model, ip and, port", "Error");
                }
            }
            //else
            //{
            //    MessageBox.Show("Right now you cannot add more than one device.", "Info");
            //}
        }

        //The function will be used to delete the device.
        private void btnDeleteDevice_Click(object sender, EventArgs e)
        {
            if (txtDeviceName.Text != "" && txtDeviceModel.Text != "" && txtDeviceIP.Text != "" && txtDevicePort.Text != "")
            {
                string resMsg = "";
                Boolean isDeleted = APILayer.DeleteDevice(out resMsg, DS.DS.gymObj.gymId, deviceId);

                if (isDeleted)
                {
                    MessageBox.Show("Device has been deleted successfully", "Success");

                    DS.DS.lstDevices.RemoveAll(device => device.id == deviceId);

                    DisconnectDevice();

                    CheckDevicesAndConnect();

                    //Clear all fields 
                    txtDeviceName.Clear();
                    txtDevicePort.Clear();
                    txtDeviceIP.Clear();
                    txtDeviceModel.Clear();
                }
                else
                {
                    MessageBox.Show(resMsg, "Error");
                }
            }
            else
            {
                MessageBox.Show("Request you to select device from the Added Devices List.", "Error");
            }
        }

        //The event will be called whenever the grid gridview row is selected.
        private void gridviewDevices_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtDeviceName.Text = gridviewDevices.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDeviceModel.Text = gridviewDevices.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtDeviceIP.Text = gridviewDevices.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtDevicePort.Text = gridviewDevices.Rows[e.RowIndex].Cells[4].Value.ToString();
            deviceId = Convert.ToInt32(gridviewDevices.Rows[e.RowIndex].Cells[0].Value);
        }

        private void btnUpdateDevice_Click(object sender, EventArgs e)
        {
            if (txtDeviceName.Text != "" && txtDeviceModel.Text != "" && txtDeviceIP.Text != "" && txtDevicePort.Text != "")
            {
                string resMsg = "";
                string date = System.DateTime.Now.ToString("yyyy-mm-dd HH:MM:ss");
                Boolean isAdded = APILayer.UpdateDevices(out resMsg, DS.DS.gymObj.gymId, deviceId, txtDeviceName.Text,
                                                        txtDeviceModel.Text, txtDeviceIP.Text, txtDevicePort.Text, date);
                if (isAdded)
                {
                    MessageBox.Show("Device has been updated successfully", "Success");

                    //Update the devices in the list
                    foreach (DS.Device device in DS.DS.lstDevices)
                    {
                        if (device.id == deviceId)
                        {
                            device.name = txtDeviceName.Text;
                            device.model = txtDeviceModel.Text;
                            device.ip = txtDeviceIP.Text;
                            device.port = Convert.ToInt32(txtDevicePort.Text);
                            device.status = Common.deviceDisconnected;
                            device.date = date;
                            break;
                        }
                    }

                    DisconnectDevice();

                    CheckDevicesAndConnect();

                    //Clear all fields 
                    txtDeviceName.Clear();
                    txtDevicePort.Clear();
                    txtDeviceIP.Clear();
                    txtDeviceModel.Clear();
                }
                else
                {
                    MessageBox.Show(resMsg, "Error");
                }
            }
            else
            {
                MessageBox.Show("Request you to select device from the Added Devices List.", "Error");
            }
        }

        //The function will all the disconnected devices
        private void btnConnectDevice_Click(object sender, EventArgs e)
        {
            CheckDevicesAndConnect();
        }


        //The function is used to clear the device details entered.
        private void pictureBoxResetDeviceDetails_Click(object sender, EventArgs e)
        {
            if (txtDeviceName.Text != "" || txtDeviceModel.Text != "" || txtDeviceIP.Text != "" || txtDevicePort.Text != "")
            {
                //Reset textbox values
                txtDeviceModel.Clear();
                txtDeviceName.Clear();
                txtDeviceIP.Clear();
                txtDevicePort.Clear();
                gridviewDevices.ClearSelection();
            }
            else
            {
                MessageBox.Show("Device Details are not present.", "Error");
            }
        }

        #endregion

        #region ResettingValuesOnTabChange

        //Reseting controls on tab change.
        private void tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tab.SelectedIndex == 0)
            {
                //Reset textbox values
                txtDeviceModel.Clear();
                txtDeviceName.Clear();
                txtDeviceIP.Clear();
                txtDevicePort.Clear();
                gridviewDevices.ClearSelection();
            }
            else if (tab.SelectedIndex == 1)
            {
                //Check if user if coming to this tab for first time.
                if (!isUserFetchedFromDB)
                {
                    MessageBox.Show("Fetching Added Users for Database. Request you to wait.", "Info");
                    //Fecth User database.
                    string resMsg = "";
                    if (APILayer.GetAddedUsers(out resMsg))
                    {
                        BindUsersGridView();
                    }
                    else
                    {
                        MessageBox.Show(resMsg, "Error");
                    }
                    isUserFetchedFromDB = true;
                }

                //Reset textbox values
                txtUserId.Clear();
                txtSearchUserId.Clear();
                gridviewUsers.ClearSelection();
            }
        }
        #endregion

        #region UserManagementRegion
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserId.Text != "")
                {
                    string resMsg = "";

                    //Check if the user is already added in the system or not
                    lock (dicUserLock)
                    {
                        if (DS.DS.dicUsers.ContainsKey(Convert.ToInt32(txtUserId.Text)))
                        {
                            MessageBox.Show("User is already added in the system.", "Error");
                            return;
                        }
                    }

                    Boolean isAdded = APILayer.ValidateUser(out resMsg, Convert.ToInt32(txtUserId.Text));
                    if (isAdded)
                    {
                        bool addedInAtleastOneDevice = false;
                        foreach (DS.Device device in DS.DS.lstDevices)
                        {
                            string userName = "";

                            //MessageBox.Show("User has been added successfully.", "Success");
                            lock (dicUserLock)
                            {
                                userName = DS.DS.dicUsers[Convert.ToInt32(txtUserId.Text)].name;
                            }
                            //Add the user in the device
                            Cursor = Cursors.WaitCursor;
                            axCZKEM1.EnableDevice(device.id, false);
                            if (axCZKEM1.SSR_SetUserInfo(device.id, txtUserId.Text,
                                                            userName, "", 0, true))//upload the user's information(card number included)
                            {
                                addedInAtleastOneDevice = true;
                            }
                            else
                            {
                                int errorCode = -1;
                                axCZKEM1.GetLastError(ref errorCode);
                                MessageBox.Show("Error Message: " + Common.GetErrorMessages(errorCode) + "." + Environment.NewLine +
                                                "Request you to take necessary steps and try in sometime.", "Error");
                            }
                            axCZKEM1.RefreshData(device.id);//the data in the device should be refreshed
                            axCZKEM1.EnableDevice(iMachineNumber, true);
                            Cursor = Cursors.Default;
                        }

                        if (addedInAtleastOneDevice)
                        {
                            lock (dicUserLock)
                            {
                                DS.DS.dicUsers[Convert.ToInt32(txtUserId.Text)].inDevice = 1;
                                DS.DS.dicUsers[Convert.ToInt32(txtUserId.Text)].enabled = 1;
                            }

                            //Add the user in the database
                            DS.User user = new DS.User();
                            lock (dicUserLock)
                            {
                                user = DS.DS.dicUsers[Convert.ToInt32(txtUserId.Text)];
                            }
                            APILayer.AddUserInDb(gymId: DS.DS.gymObj.gymId, userId: user.id, userName: user.name, userType: user.type,
                                                 inDevice: user.inDevice, expiryDt: user.expiryDt, dt: user.date, enabled: user.enabled);

                            //Add the user in the userList
                            BindUsersGridView();

                            //Reset textboax field
                            txtUserId.Clear();

                            MessageBox.Show("User added in atleast one device", "Success");
                        }
                    }
                    else
                    {
                        MessageBox.Show(resMsg, "Error");
                    }


                }
                else
                {
                    MessageBox.Show("Request you to enter User Id.", "Error");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.errorMsg, "Error");
            }
        }

        private void gridviewUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                txtUserId.Text = gridviewUsers.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAddAccess_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUserId.Text))
            {
                MessageBox.Show("Request you to select user from Added User list", "Error");
            }
            else
            {
                bool accessAddedInAtleastOneDevice = false;
                foreach (DS.Device device in DS.DS.lstDevices)
                {
                    if (device.status == Common.deviceDisconnected)
                    {
                        MessageBox.Show(Common.deviceConnectionErrorMsg, "Error");
                        break;
                    }

                    string userName = "";

                    lock (dicUserLock)
                    {
                        userName = DS.DS.dicUsers[Convert.ToInt32(txtUserId.Text)].name;
                    }
                    //Add the user in the device
                    Cursor = Cursors.WaitCursor;
                    axCZKEM1.EnableDevice(device.id, false);

                    Boolean userAdded = false;
                    byte userDetailsExistInDevice = 0;
                    lock (dicUserLock)
                    {
                        //check status of user 
                        userDetailsExistInDevice = DS.DS.dicUsers[Convert.ToInt32(txtUserId.Text)].inDevice;
                    }
                    if (userDetailsExistInDevice == 1)
                    {
                        userAdded = axCZKEM1.SSR_EnableUser(device.id, txtUserId.Text, true);
                    }
                    else
                    {
                        userAdded = axCZKEM1.SSR_SetUserInfo(device.id, txtUserId.Text,
                                                             userName, "", 0, true);
                    }
                    if (!userAdded)
                    {
                        int errorCode = -1;
                        axCZKEM1.GetLastError(ref errorCode);

                        //MessageBox.Show("Error Message: " + Common.GetErrorMessages(errorCode) + "." + Environment.NewLine +
                        //              "Request you to take necessary steps and try in sometime.", "Error");
                        accessAddedInAtleastOneDevice = false;
                    }
                    else
                    {
                        accessAddedInAtleastOneDevice = true;
                    }
                    axCZKEM1.RefreshData(device.id);//the data in the device should be refreshed
                    axCZKEM1.EnableDevice(device.id, true);
                    Cursor = Cursors.Default;

                }

                //This is return below, so that in case of user added we free the device first.
                //and then hit API and all stuff.
                if (accessAddedInAtleastOneDevice)
                {
                    lock (dicUserLock)
                    {
                        //Enable status of user 
                        DS.DS.dicUsers[Convert.ToInt32(txtUserId.Text)].inDevice = 1;
                        DS.DS.dicUsers[Convert.ToInt32(txtUserId.Text)].enabled = 1;
                    }

                    //Update user in the database
                    DS.User user = new DS.User();
                    lock (dicUserLock)
                    {
                        user = DS.DS.dicUsers[Convert.ToInt32(txtUserId.Text)];
                    }
                    APILayer.UpdateUserInDb(gymId: DS.DS.gymObj.gymId, userId: user.id, userName: user.name, userType: user.type,
                                            inDevice: user.inDevice, expiryDt: user.expiryDt, dt: user.date, enabled: user.enabled);


                    MessageBox.Show("Access has been granted to user successfully in atleast one device", "Success");
                }

                //Add the user in the userList
                BindUsersGridView();

                //Reset textboax field
                txtUserId.Clear();

            }
        }

        private void btnRemoveAccess_Click(object sender, EventArgs e)
        {
            Boolean userDeleted = false;
            if (String.IsNullOrEmpty(txtUserId.Text))
            {
                MessageBox.Show("Request you to select user from Added User list", "Error");
            }
            else
            {
                bool accessRemovedFromAllDevices = false;
                foreach (DS.Device device in DS.DS.lstDevices)
                {
                    if (device.status == Common.deviceDisconnected)
                    {
                        MessageBox.Show(Common.deviceConnectionErrorMsg, "Error");
                        accessRemovedFromAllDevices = false;
                        break;
                    }

                    Cursor = Cursors.WaitCursor;
                    axCZKEM1.EnableDevice(device.id, false);//disable the device
                    userDeleted = axCZKEM1.SSR_DeleteEnrollData(device.id, txtUserId.Text, 12);

                    if (!userDeleted)
                    {
                        accessRemovedFromAllDevices = false;
                        int errorCode = -1;
                        axCZKEM1.GetLastError(ref errorCode);
                        MessageBox.Show("Error Message: " + Common.GetErrorMessages(errorCode) + "." + Environment.NewLine +
                                        "Request you to take necessary steps and try in sometime.", "Error");
                    }
                    else
                        accessRemovedFromAllDevices = true;
                    axCZKEM1.RefreshData(device.id);//the data in the device should be refreshed
                    axCZKEM1.EnableDevice(device.id, true);//enable the device
                    Cursor = Cursors.Default;


                }

                if (accessRemovedFromAllDevices)
                {
                    lock (dicUserLock)
                    {
                        DS.DS.dicUsers[Convert.ToInt32(txtUserId.Text)].inDevice = 0;
                        DS.DS.dicUsers[Convert.ToInt32(txtUserId.Text)].enabled = 0;
                    }

                    //Update user in the database
                    DS.User user = new DS.User();
                    lock (dicUserLock)
                    {
                        user = DS.DS.dicUsers[Convert.ToInt32(txtUserId.Text)];
                    }

                    //Bind Users Gridview
                    BindUsersGridView();

                    APILayer.UpdateUserInDb(gymId: DS.DS.gymObj.gymId, userId: user.id, userName: user.name, userType: user.type,
                                            inDevice: user.inDevice, expiryDt: user.expiryDt, dt: user.date, enabled: user.enabled);


                    MessageBox.Show("User access has been removed successfully.", "Success");
                }

                //Add the user in the userList
                BindUsersGridView();

                txtUserId.Clear();
            }
        }

        private void pictureBoxClearUserId_Click(object sender, EventArgs e)
        {
            if (txtUserId.Text != "")
            {
                //Reset textbox values
                txtUserId.Clear();
                gridviewUsers.ClearSelection();
            }
            else
            {
                MessageBox.Show("User Id is not present.", "Error");
            }
        }

        private void txtSearchUserId_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearchUserId.Text))
            {
                BindUsersGridView();
            }
            else
            {
                int rowIndex = 0;
                gridviewUsers.Rows.Clear();

                lock (dicUserLock)
                {
                    foreach (KeyValuePair<int, DS.User> dicUser in DS.DS.dicUsers)
                    {
                        if (dicUser.Value.id.ToString().StartsWith(txtSearchUserId.Text))
                        {
                            rowIndex = gridviewUsers.Rows.Add();
                            gridviewUsers.Rows[rowIndex].Cells[0].Value = dicUser.Value.id;
                            gridviewUsers.Rows[rowIndex].Cells[1].Value = dicUser.Value.name;
                            gridviewUsers.Rows[rowIndex].Cells[2].Value = dicUser.Value.type;
                            if (dicUser.Value.inDevice == 1)
                                gridviewUsers.Rows[rowIndex].Cells[3].Style.ForeColor = Color.Green;
                            else
                                gridviewUsers.Rows[rowIndex].Cells[3].Style.ForeColor = Color.Red;
                            gridviewUsers.Rows[rowIndex].Cells[3].Value = dicUser.Value.inDevice;
                            if (dicUser.Value.enabled == 1)
                                gridviewUsers.Rows[rowIndex].Cells[4].Style.ForeColor = Color.Green;
                            else
                                gridviewUsers.Rows[rowIndex].Cells[4].Style.ForeColor = Color.Red;
                            gridviewUsers.Rows[rowIndex].Cells[4].Value = dicUser.Value.enabled;
                            gridviewUsers.Rows[rowIndex].Cells[5].Value = dicUser.Value.expiryDt;
                            gridviewUsers.Rows[rowIndex].Cells[6].Value = dicUser.Value.date;
                        }
                    }
                }

                Common.ResizeGridView(gridviewUsers);

                //Reset Gridview Selection
                gridviewUsers.ClearSelection();

                #region Gridview Styling
                gridviewUsers.BorderStyle = BorderStyle.None;
                gridviewUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                gridviewUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                gridviewUsers.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                gridviewUsers.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                gridviewUsers.BackgroundColor = Color.White;

                gridviewUsers.EnableHeadersVisualStyles = false;
                gridviewUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                gridviewUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                gridviewUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                #endregion

            }
        }

        #endregion

        #region DisconnectDevice
        private void TFT_FormClosing(object sender, FormClosingEventArgs e)
        {
            //When the application is getting closed, we will disconnect with te device
            //and de register all events.
            DisconnectDevice();
        }

        public void DisconnectDevice()
        {
            MessageBox.Show("Disconnecting Device.", "Info");
            axCZKEM1.Disconnect();

            //this.axCZKEM1.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
            this.axCZKEM1.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
            //this.axCZKEM1.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
            //this.axCZKEM1.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
            //this.axCZKEM1.OnWriteCard -= new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
            //this.axCZKEM1.OnEmptyCard -= new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
        }
        #endregion

        #region AttendanceRegion
        //The Method will be used to attendance data.
        private void btnManualSync_Click(object sender, EventArgs e)
        {
            foreach (DS.Device device in DS.DS.lstDevices)
            {
                if (device.status == Common.deviceDisconnected)
                {
                    MessageBox.Show(Common.deviceConnectionErrorMsg, "Error");
                    return;
                }

                if ((System.DateTime.Now - dtLastAttendanceSync).TotalMinutes <= 5)
                {
                    MessageBox.Show("Last Data Sync happened on " + lblDataSyncVal.Text + ". There should be a gap of 5 minutes between Data Sync", "Error");
                    return;
                }

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
                DateTime dt = System.DateTime.MinValue;

                int idwErrorCode = 0;
                int iGLCount = 0;
                int iIndex = 0;



                Cursor = Cursors.WaitCursor;
                axCZKEM1.EnableDevice(device.id, false);//disable the device
                if (axCZKEM1.ReadGeneralLogData(device.id))//read all the attendance records to the memory
                {
                    while (axCZKEM1.SSR_GetGeneralLogData(device.id, out sdwEnrollNumber, out idwVerifyMode,
                               out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                    {
                        iGLCount++;

                        //Store the real time attendance infor in the list
                        DS.AttendanceInfo attendanceInfo = new DS.AttendanceInfo();
                        attendanceInfo.uid = Convert.ToInt32(sdwEnrollNumber);
                        attendanceInfo.time = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " +
                                              idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString();
                        dt = new DateTime(idwYear, idwMinute, idwDay, idwHour, idwMinute, idwSecond);

                        if (dt >= dtLastAttendanceSync)
                        {
                            //Log attendance in List
                            lock (listAttendanceLock)
                            {
                                DS.DS.lstAttendance.Add(attendanceInfo);
                            }
                        }

                        iIndex++;
                    }
                }
                else
                {
                    Cursor = Cursors.Default;
                    axCZKEM1.GetLastError(ref idwErrorCode);

                    if (idwErrorCode != 0)
                    {
                        MessageBox.Show("Error Message: " + Common.GetErrorMessages(idwErrorCode) + "." + Environment.NewLine +
                                        "Request you to take necessary steps and try in sometime.", "Error");
                    }
                    else
                    {
                        MessageBox.Show("Device has not returned any data", "Error");
                    }
                }
                axCZKEM1.EnableDevice(device.id, true);//enable the device
                Cursor = Cursors.Default;
            }


            MessageBox.Show("Attendance Details synced successfully. Wait for 5 minutes to get it reflected.", "Success");
        }

        //The below method will clear the attendance logs that persist on device.
        private void btnClearAttendanceLogs_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DS.Device device in DS.DS.lstDevices)
                {
                    if (device.status == Common.deviceDisconnected)
                    {
                        MessageBox.Show(Common.deviceConnectionErrorMsg, "Error");
                        break;
                    }
                    int errorCode = 0;

                    //Clear attendance details from LIstview.
                    lvAttendanceDetails.Items.Clear();

                    axCZKEM1.EnableDevice(device.id, false);//disable the device
                    if (axCZKEM1.ClearGLog(device.id))
                    {
                        axCZKEM1.RefreshData(device.id);//the data in the device should be refreshed
                        MessageBox.Show("All attendance logs have cleared from device.", "Success");
                    }
                    else
                    {
                        axCZKEM1.GetLastError(ref errorCode);
                        MessageBox.Show("Error Message: " + Common.GetErrorMessages(errorCode) + "." + Environment.NewLine +
                                         "Request you to take necessary steps and try in sometime.", "Error");
                    }
                    axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                }
            }
            catch (Exception ex)
            {

            }
        }

        //If your card passes the verification,this event will be triggered
        private void axCZKEM1_OnAttTransactionEx(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear,
                                                 int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
            //Store the real time attendance infor in the list
            DS.AttendanceInfo attendanceInfo = new DS.AttendanceInfo();
            attendanceInfo.uid = Convert.ToInt32(sEnrollNumber);
            attendanceInfo.time = iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " +
                                  iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString();
            //Log attendance in List
            lock (listAttendanceLock)
            {
                DS.DS.lstAttendance.Add(attendanceInfo);
            }

            //Show the real time notification to user
            if (lvAttendanceDetails.Items.Count == 15)
            {
                //Clears the listview
                lvAttendanceDetails.Clear();
            }

            //Add the real time notification in this listview
            lvAttendanceDetails.BeginUpdate();
            ListViewItem list = new ListViewItem();
            list.Text = sEnrollNumber;
            lock (dicUserLock)
            {
                if (DS.DS.dicUsers.ContainsKey(Convert.ToInt32(sEnrollNumber)))
                {
                    list.SubItems.Add(DS.DS.dicUsers[Convert.ToInt32(sEnrollNumber)].name.ToString());
                    list.SubItems.Add(DS.DS.dicUsers[Convert.ToInt32(sEnrollNumber)].type.ToString());
                }
                else
                {
                    list.SubItems.Add("");
                    list.SubItems.Add("");
                }
            }
            list.SubItems.Add(iDay.ToString() + "-" + iMonth.ToString() + "-" + iYear.ToString() + " " +
                              iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString());
            lvAttendanceDetails.Items.Add(list);

            lvAttendanceDetails.EndUpdate();

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
                    lbRTShow.Items.Add("...Cardnumber on Device:" + sCardnumber);
                    return;
                }
            }
        }



        #endregion

        private void btnEnrollUser_Click(object sender, EventArgs e)
        {
            Boolean userDeleted = false;
            if (String.IsNullOrEmpty(txtUserId.Text))
            {
                MessageBox.Show("Request you to select user from Added User list", "Error");
            }
            else
            {
                if (DS.DS.lstDevices[0].status == Common.deviceDisconnected)
                {
                    MessageBox.Show(Common.deviceConnectionErrorMsg, "Error");
                }

                Cursor = Cursors.WaitCursor;
                axCZKEM1.EnableDevice(DS.DS.lstDevices[0].id, false);//disable the device
                axCZKEM1.StartEnroll(Convert.ToInt32(txtUserId.Text), 0);
                axCZKEM1.EnableDevice(DS.DS.lstDevices[0].id, true);//enable the device
                Cursor = Cursors.Default;

                txtUserId.Clear();
            }
        }
    }
}
