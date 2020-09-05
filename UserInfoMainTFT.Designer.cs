using System.Drawing;

namespace UserInfo
{
    partial class TFT
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Timer rtTimer;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TFT));
            this.UserIDTimer = new System.Windows.Forms.Timer(this.components);
            this.Body = new System.Windows.Forms.GroupBox();
            this.tab = new System.Windows.Forms.TabControl();
            this.tabDevice = new System.Windows.Forms.TabPage();
            this.pictureBoxResetDeviceDetails = new System.Windows.Forms.PictureBox();
            this.btnConnectDevice = new System.Windows.Forms.Button();
            this.btnDeleteDevice = new System.Windows.Forms.Button();
            this.btnUpdateDevice = new System.Windows.Forms.Button();
            this.btnAddNewDevice = new System.Windows.Forms.Button();
            this.lblDeviceDetails = new System.Windows.Forms.Label();
            this.txtDevicePort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtDeviceIP = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtDeviceModel = new System.Windows.Forms.TextBox();
            this.lblDeviceModel = new System.Windows.Forms.Label();
            this.txtDeviceName = new System.Windows.Forms.TextBox();
            this.lblDeviceName = new System.Windows.Forms.Label();
            this.lblAddedDevices = new System.Windows.Forms.Label();
            this.gridviewDevices = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.txtSearchUserId = new System.Windows.Forms.TextBox();
            this.lblSerachUserId = new System.Windows.Forms.Label();
            this.pictureBoxClearUserId = new System.Windows.Forms.PictureBox();
            this.btnAddAccess = new System.Windows.Forms.Button();
            this.gridviewUsers = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddedtoDevice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAddedUsers = new System.Windows.Forms.Label();
            this.btnRemoveAccess = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.lblUserId = new System.Windows.Forms.Label();
            this.lblUserDetails = new System.Windows.Forms.Label();
            this.tabAttendance = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClearAttendanceLogs = new System.Windows.Forms.Button();
            this.btnManualSync = new System.Windows.Forms.Button();
            this.lblManualSync = new System.Windows.Forms.Label();
            this.lblClearAttendanceLogs = new System.Windows.Forms.Label();
            this.lvAttendanceDetails = new System.Windows.Forms.ListView();
            this.UserId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UserType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblAttendanceDetails = new System.Windows.Forms.Label();
            this.tabPageFT = new System.Windows.Forms.TabPage();
            this.gbFingerTm9and10 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpload9 = new System.Windows.Forms.Button();
            this.btnDownloadTmp = new System.Windows.Forms.Button();
            this.btnBatchUpdate = new System.Windows.Forms.Button();
            this.gbEvent = new System.Windows.Forms.GroupBox();
            this.gbDataBase = new System.Windows.Forms.GroupBox();
            this.DeleteFpTm = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.DownloadFPTemDB = new System.Windows.Forms.Button();
            this.btnDatabase = new System.Windows.Forms.Button();
            this.gbDeleteEnrollData = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.cbUserIDDE = new System.Windows.Forms.ComboBox();
            this.btnDeleteEnrollData = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.cbBackupDE = new System.Windows.Forms.ComboBox();
            this.gbDeleteUserFingerTm = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSSR_DelUserTmpExt = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cbFingerIndex = new System.Windows.Forms.ComboBox();
            this.cbUserIDTmp = new System.Windows.Forms.ComboBox();
            this.gbClearTMAndAdmis = new System.Windows.Forms.GroupBox();
            this.btnClearAdministrators = new System.Windows.Forms.Button();
            this.btnClearDataTmps = new System.Windows.Forms.Button();
            this.btnClearDataUserInfo = new System.Windows.Forms.Button();
            this.lvDownload = new System.Windows.Forms.ListView();
            this.ch1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageCardReader = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btnGetHIDEventCardNumAsStr = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.lbRTShow = new System.Windows.Forms.ListBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.lvCard = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnGetStrCardNumber = new System.Windows.Forms.Button();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.cbUserId_Card = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.chbEnabled = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnSetStrCardNumber = new System.Windows.Forms.Button();
            this.label89 = new System.Windows.Forms.Label();
            this.cbPrivilege = new System.Windows.Forms.ComboBox();
            this.txtCardnumber = new System.Windows.Forms.TextBox();
            this.Privilege = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.tabPageLogR = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnClearGLog = new System.Windows.Forms.Button();
            this.btnGetDeviceStatus = new System.Windows.Forms.Button();
            this.btnGetGeneralLogData = new System.Windows.Forms.Button();
            this.lvLogs = new System.Windows.Forms.ListView();
            this.lvLogsch1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvLogsch2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvLogsch3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvLogsch4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvLogsch5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvLogsch6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvLogsch7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblDataSyncVal = new System.Windows.Forms.Label();
            this.lblPurchasedVal = new System.Windows.Forms.Label();
            this.lblLicenseValidVal = new System.Windows.Forms.Label();
            this.lblDataSyncKey = new System.Windows.Forms.Label();
            this.lblPurchasedKey = new System.Windows.Forms.Label();
            this.lblExpiryDateKey = new System.Windows.Forms.Label();
            this.lblGymName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            rtTimer = new System.Windows.Forms.Timer(this.components);
            this.Body.SuspendLayout();
            this.tab.SuspendLayout();
            this.tabDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResetDeviceDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewDevices)).BeginInit();
            this.tabUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClearUserId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewUsers)).BeginInit();
            this.tabAttendance.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageFT.SuspendLayout();
            this.gbFingerTm9and10.SuspendLayout();
            this.gbEvent.SuspendLayout();
            this.gbDataBase.SuspendLayout();
            this.gbDeleteEnrollData.SuspendLayout();
            this.gbDeleteUserFingerTm.SuspendLayout();
            this.gbClearTMAndAdmis.SuspendLayout();
            this.tabPageCardReader.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.tabPageLogR.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // rtTimer
            // 
            rtTimer.Interval = 800;
            rtTimer.Tick += new System.EventHandler(this.rtTimer_Tick);
            // 
            // UserIDTimer
            // 
            this.UserIDTimer.Enabled = true;
            this.UserIDTimer.Tick += new System.EventHandler(this.UserIDTimer_Tick);
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.Silver;
            this.Body.Controls.Add(this.tab);
            this.Body.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.Body.Location = new System.Drawing.Point(1, 91);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(989, 497);
            this.Body.TabIndex = 84;
            this.Body.TabStop = false;
            this.Body.Text = "Request you to select appropriate tab";
            // 
            // tab
            // 
            this.tab.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tab.Controls.Add(this.tabDevice);
            this.tab.Controls.Add(this.tabUsers);
            this.tab.Controls.Add(this.tabAttendance);
            this.tab.Controls.Add(this.tabPageFT);
            this.tab.Controls.Add(this.tabPageCardReader);
            this.tab.Controls.Add(this.tabPageLogR);
            this.tab.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.tab.ItemSize = new System.Drawing.Size(111, 30);
            this.tab.Location = new System.Drawing.Point(6, 19);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(975, 465);
            this.tab.TabIndex = 8;
            this.tab.SelectedIndexChanged += new System.EventHandler(this.tab_SelectedIndexChanged);
            // 
            // tabDevice
            // 
            this.tabDevice.Controls.Add(this.pictureBoxResetDeviceDetails);
            this.tabDevice.Controls.Add(this.btnConnectDevice);
            this.tabDevice.Controls.Add(this.btnDeleteDevice);
            this.tabDevice.Controls.Add(this.btnUpdateDevice);
            this.tabDevice.Controls.Add(this.btnAddNewDevice);
            this.tabDevice.Controls.Add(this.lblDeviceDetails);
            this.tabDevice.Controls.Add(this.txtDevicePort);
            this.tabDevice.Controls.Add(this.lblPort);
            this.tabDevice.Controls.Add(this.txtDeviceIP);
            this.tabDevice.Controls.Add(this.lblIP);
            this.tabDevice.Controls.Add(this.txtDeviceModel);
            this.tabDevice.Controls.Add(this.lblDeviceModel);
            this.tabDevice.Controls.Add(this.txtDeviceName);
            this.tabDevice.Controls.Add(this.lblDeviceName);
            this.tabDevice.Controls.Add(this.lblAddedDevices);
            this.tabDevice.Controls.Add(this.gridviewDevices);
            this.tabDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabDevice.Location = new System.Drawing.Point(4, 34);
            this.tabDevice.Name = "tabDevice";
            this.tabDevice.Size = new System.Drawing.Size(967, 427);
            this.tabDevice.TabIndex = 3;
            this.tabDevice.Text = "Device Management";
            this.tabDevice.UseVisualStyleBackColor = true;
            // 
            // pictureBoxResetDeviceDetails
            // 
            this.pictureBoxResetDeviceDetails.Image = global::UserInfo.Properties.Resources.reset;
            this.pictureBoxResetDeviceDetails.Location = new System.Drawing.Point(851, 43);
            this.pictureBoxResetDeviceDetails.Name = "pictureBoxResetDeviceDetails";
            this.pictureBoxResetDeviceDetails.Size = new System.Drawing.Size(22, 20);
            this.pictureBoxResetDeviceDetails.TabIndex = 18;
            this.pictureBoxResetDeviceDetails.TabStop = false;
            this.pictureBoxResetDeviceDetails.Click += new System.EventHandler(this.pictureBoxResetDeviceDetails_Click);
            // 
            // btnConnectDevice
            // 
            this.btnConnectDevice.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnConnectDevice.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnectDevice.ForeColor = System.Drawing.Color.White;
            this.btnConnectDevice.Location = new System.Drawing.Point(734, 93);
            this.btnConnectDevice.Margin = new System.Windows.Forms.Padding(5);
            this.btnConnectDevice.Name = "btnConnectDevice";
            this.btnConnectDevice.Size = new System.Drawing.Size(140, 40);
            this.btnConnectDevice.TabIndex = 16;
            this.btnConnectDevice.Text = "Connect Devices";
            this.btnConnectDevice.UseVisualStyleBackColor = false;
            this.btnConnectDevice.Click += new System.EventHandler(this.btnConnectDevice_Click);
            // 
            // btnDeleteDevice
            // 
            this.btnDeleteDevice.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnDeleteDevice.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteDevice.ForeColor = System.Drawing.Color.White;
            this.btnDeleteDevice.Location = new System.Drawing.Point(524, 93);
            this.btnDeleteDevice.Margin = new System.Windows.Forms.Padding(5);
            this.btnDeleteDevice.Name = "btnDeleteDevice";
            this.btnDeleteDevice.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteDevice.TabIndex = 15;
            this.btnDeleteDevice.Text = "Delete Device";
            this.btnDeleteDevice.UseVisualStyleBackColor = false;
            this.btnDeleteDevice.Click += new System.EventHandler(this.btnDeleteDevice_Click);
            // 
            // btnUpdateDevice
            // 
            this.btnUpdateDevice.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnUpdateDevice.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateDevice.ForeColor = System.Drawing.Color.White;
            this.btnUpdateDevice.Location = new System.Drawing.Point(313, 93);
            this.btnUpdateDevice.Margin = new System.Windows.Forms.Padding(5);
            this.btnUpdateDevice.Name = "btnUpdateDevice";
            this.btnUpdateDevice.Size = new System.Drawing.Size(140, 40);
            this.btnUpdateDevice.TabIndex = 14;
            this.btnUpdateDevice.Text = "Update Device";
            this.btnUpdateDevice.UseVisualStyleBackColor = false;
            this.btnUpdateDevice.Click += new System.EventHandler(this.btnUpdateDevice_Click);
            // 
            // btnAddNewDevice
            // 
            this.btnAddNewDevice.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnAddNewDevice.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewDevice.ForeColor = System.Drawing.Color.White;
            this.btnAddNewDevice.Location = new System.Drawing.Point(103, 93);
            this.btnAddNewDevice.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddNewDevice.Name = "btnAddNewDevice";
            this.btnAddNewDevice.Size = new System.Drawing.Size(140, 40);
            this.btnAddNewDevice.TabIndex = 13;
            this.btnAddNewDevice.Text = "Add New Device";
            this.btnAddNewDevice.UseVisualStyleBackColor = false;
            this.btnAddNewDevice.Click += new System.EventHandler(this.btnAddNewDevice_Click);
            // 
            // lblDeviceDetails
            // 
            this.lblDeviceDetails.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDeviceDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDeviceDetails.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeviceDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDeviceDetails.Location = new System.Drawing.Point(0, 0);
            this.lblDeviceDetails.Name = "lblDeviceDetails";
            this.lblDeviceDetails.Size = new System.Drawing.Size(967, 37);
            this.lblDeviceDetails.TabIndex = 12;
            this.lblDeviceDetails.Text = "Device Details";
            this.lblDeviceDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDevicePort
            // 
            this.txtDevicePort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDevicePort.Location = new System.Drawing.Point(762, 40);
            this.txtDevicePort.Name = "txtDevicePort";
            this.txtDevicePort.Size = new System.Drawing.Size(53, 23);
            this.txtDevicePort.TabIndex = 11;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.Location = new System.Drawing.Point(717, 42);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(32, 17);
            this.lblPort.TabIndex = 10;
            this.lblPort.Text = "Port";
            // 
            // txtDeviceIP
            // 
            this.txtDeviceIP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeviceIP.Location = new System.Drawing.Point(591, 40);
            this.txtDeviceIP.Name = "txtDeviceIP";
            this.txtDeviceIP.Size = new System.Drawing.Size(100, 23);
            this.txtDeviceIP.TabIndex = 9;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP.Location = new System.Drawing.Point(560, 42);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(18, 17);
            this.lblIP.TabIndex = 8;
            this.lblIP.Text = "IP";
            // 
            // txtDeviceModel
            // 
            this.txtDeviceModel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeviceModel.Location = new System.Drawing.Point(382, 40);
            this.txtDeviceModel.Name = "txtDeviceModel";
            this.txtDeviceModel.Size = new System.Drawing.Size(150, 23);
            this.txtDeviceModel.TabIndex = 5;
            // 
            // lblDeviceModel
            // 
            this.lblDeviceModel.AutoSize = true;
            this.lblDeviceModel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeviceModel.Location = new System.Drawing.Point(281, 42);
            this.lblDeviceModel.Name = "lblDeviceModel";
            this.lblDeviceModel.Size = new System.Drawing.Size(88, 17);
            this.lblDeviceModel.TabIndex = 4;
            this.lblDeviceModel.Text = "Device Model";
            // 
            // txtDeviceName
            // 
            this.txtDeviceName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeviceName.Location = new System.Drawing.Point(103, 40);
            this.txtDeviceName.Name = "txtDeviceName";
            this.txtDeviceName.Size = new System.Drawing.Size(150, 23);
            this.txtDeviceName.TabIndex = 3;
            // 
            // lblDeviceName
            // 
            this.lblDeviceName.AutoSize = true;
            this.lblDeviceName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeviceName.Location = new System.Drawing.Point(5, 42);
            this.lblDeviceName.Name = "lblDeviceName";
            this.lblDeviceName.Size = new System.Drawing.Size(85, 17);
            this.lblDeviceName.TabIndex = 2;
            this.lblDeviceName.Text = "Device Name";
            // 
            // lblAddedDevices
            // 
            this.lblAddedDevices.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblAddedDevices.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddedDevices.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAddedDevices.Location = new System.Drawing.Point(0, 160);
            this.lblAddedDevices.Name = "lblAddedDevices";
            this.lblAddedDevices.Size = new System.Drawing.Size(967, 37);
            this.lblAddedDevices.TabIndex = 1;
            this.lblAddedDevices.Text = "Added Devices";
            this.lblAddedDevices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridviewDevices
            // 
            this.gridviewDevices.AllowUserToAddRows = false;
            this.gridviewDevices.AllowUserToDeleteRows = false;
            this.gridviewDevices.BackgroundColor = System.Drawing.Color.White;
            this.gridviewDevices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridviewDevices.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridviewDevices.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(72)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridviewDevices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridviewDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridviewDevices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.name,
            this.model,
            this.ip,
            this.port,
            this.status,
            this.date});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridviewDevices.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridviewDevices.EnableHeadersVisualStyles = false;
            this.gridviewDevices.Location = new System.Drawing.Point(0, 200);
            this.gridviewDevices.Name = "gridviewDevices";
            this.gridviewDevices.ReadOnly = true;
            this.gridviewDevices.RowTemplate.Height = 25;
            this.gridviewDevices.Size = new System.Drawing.Size(964, 155);
            this.gridviewDevices.TabIndex = 0;
            this.gridviewDevices.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridviewDevices_RowHeaderMouseClick);
            // 
            // id
            // 
            this.id.HeaderText = "Device Id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 85;
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 180;
            // 
            // model
            // 
            this.model.HeaderText = "Model";
            this.model.Name = "model";
            this.model.ReadOnly = true;
            this.model.Width = 165;
            // 
            // ip
            // 
            this.ip.HeaderText = "IP";
            this.ip.Name = "ip";
            this.ip.ReadOnly = true;
            // 
            // port
            // 
            this.port.HeaderText = "Port";
            this.port.Name = "port";
            this.port.ReadOnly = true;
            this.port.Width = 70;
            // 
            // status
            // 
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 120;
            // 
            // date
            // 
            this.date.HeaderText = "Date";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 200;
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.txtSearchUserId);
            this.tabUsers.Controls.Add(this.lblSerachUserId);
            this.tabUsers.Controls.Add(this.pictureBoxClearUserId);
            this.tabUsers.Controls.Add(this.btnAddAccess);
            this.tabUsers.Controls.Add(this.gridviewUsers);
            this.tabUsers.Controls.Add(this.lblAddedUsers);
            this.tabUsers.Controls.Add(this.btnRemoveAccess);
            this.tabUsers.Controls.Add(this.btnAddUser);
            this.tabUsers.Controls.Add(this.txtUserId);
            this.tabUsers.Controls.Add(this.lblUserId);
            this.tabUsers.Controls.Add(this.lblUserDetails);
            this.tabUsers.Location = new System.Drawing.Point(4, 34);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Size = new System.Drawing.Size(967, 427);
            this.tabUsers.TabIndex = 4;
            this.tabUsers.Text = "User Management";
            this.tabUsers.UseVisualStyleBackColor = true;
            // 
            // txtSearchUserId
            // 
            this.txtSearchUserId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchUserId.Location = new System.Drawing.Point(81, 159);
            this.txtSearchUserId.Name = "txtSearchUserId";
            this.txtSearchUserId.Size = new System.Drawing.Size(150, 23);
            this.txtSearchUserId.TabIndex = 23;
            this.txtSearchUserId.TextChanged += new System.EventHandler(this.txtSearchUserId_TextChanged);
            // 
            // lblSerachUserId
            // 
            this.lblSerachUserId.AutoSize = true;
            this.lblSerachUserId.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerachUserId.Location = new System.Drawing.Point(5, 162);
            this.lblSerachUserId.Name = "lblSerachUserId";
            this.lblSerachUserId.Size = new System.Drawing.Size(50, 17);
            this.lblSerachUserId.TabIndex = 22;
            this.lblSerachUserId.Text = "User Id";
            // 
            // pictureBoxClearUserId
            // 
            this.pictureBoxClearUserId.Image = global::UserInfo.Properties.Resources.reset;
            this.pictureBoxClearUserId.Location = new System.Drawing.Point(246, 43);
            this.pictureBoxClearUserId.Name = "pictureBoxClearUserId";
            this.pictureBoxClearUserId.Size = new System.Drawing.Size(22, 20);
            this.pictureBoxClearUserId.TabIndex = 21;
            this.pictureBoxClearUserId.TabStop = false;
            this.pictureBoxClearUserId.Click += new System.EventHandler(this.pictureBoxClearUserId_Click);
            // 
            // btnAddAccess
            // 
            this.btnAddAccess.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnAddAccess.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAccess.ForeColor = System.Drawing.Color.White;
            this.btnAddAccess.Location = new System.Drawing.Point(464, 30);
            this.btnAddAccess.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddAccess.Name = "btnAddAccess";
            this.btnAddAccess.Size = new System.Drawing.Size(140, 40);
            this.btnAddAccess.TabIndex = 20;
            this.btnAddAccess.Text = "Add Access";
            this.btnAddAccess.UseVisualStyleBackColor = false;
            this.btnAddAccess.Click += new System.EventHandler(this.btnAddAccess_Click);
            // 
            // gridviewUsers
            // 
            this.gridviewUsers.AllowUserToAddRows = false;
            this.gridviewUsers.AllowUserToDeleteRows = false;
            this.gridviewUsers.BackgroundColor = System.Drawing.Color.White;
            this.gridviewUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridviewUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridviewUsers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(72)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridviewUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridviewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridviewUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.AddedtoDevice,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn7,
            this.CreatedDate});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridviewUsers.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridviewUsers.EnableHeadersVisualStyles = false;
            this.gridviewUsers.Location = new System.Drawing.Point(1, 196);
            this.gridviewUsers.Name = "gridviewUsers";
            this.gridviewUsers.ReadOnly = true;
            this.gridviewUsers.RowTemplate.Height = 25;
            this.gridviewUsers.Size = new System.Drawing.Size(964, 155);
            this.gridviewUsers.TabIndex = 19;
            this.gridviewUsers.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridviewUsers_RowHeaderMouseClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "User Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 140;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "User Type";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 140;
            // 
            // AddedtoDevice
            // 
            this.AddedtoDevice.HeaderText = "In Device";
            this.AddedtoDevice.Name = "AddedtoDevice";
            this.AddedtoDevice.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Enabled";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Expiry Date";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 200;
            // 
            // CreatedDate
            // 
            this.CreatedDate.HeaderText = "Created Date";
            this.CreatedDate.Name = "CreatedDate";
            this.CreatedDate.ReadOnly = true;
            this.CreatedDate.Width = 200;
            // 
            // lblAddedUsers
            // 
            this.lblAddedUsers.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblAddedUsers.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddedUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAddedUsers.Location = new System.Drawing.Point(0, 112);
            this.lblAddedUsers.Name = "lblAddedUsers";
            this.lblAddedUsers.Size = new System.Drawing.Size(967, 37);
            this.lblAddedUsers.TabIndex = 18;
            this.lblAddedUsers.Text = "Added Users";
            this.lblAddedUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRemoveAccess
            // 
            this.btnRemoveAccess.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnRemoveAccess.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAccess.ForeColor = System.Drawing.Color.White;
            this.btnRemoveAccess.Location = new System.Drawing.Point(627, 30);
            this.btnRemoveAccess.Margin = new System.Windows.Forms.Padding(5);
            this.btnRemoveAccess.Name = "btnRemoveAccess";
            this.btnRemoveAccess.Size = new System.Drawing.Size(140, 40);
            this.btnRemoveAccess.TabIndex = 17;
            this.btnRemoveAccess.Text = "Remove Access";
            this.btnRemoveAccess.UseVisualStyleBackColor = false;
            this.btnRemoveAccess.Click += new System.EventHandler(this.btnRemoveAccess_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnAddUser.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUser.ForeColor = System.Drawing.Color.White;
            this.btnAddUser.Location = new System.Drawing.Point(301, 30);
            this.btnAddUser.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(140, 40);
            this.btnAddUser.TabIndex = 16;
            this.btnAddUser.Text = "Add New User";
            this.btnAddUser.UseVisualStyleBackColor = false;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // txtUserId
            // 
            this.txtUserId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserId.Location = new System.Drawing.Point(81, 40);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(150, 23);
            this.txtUserId.TabIndex = 15;
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserId.Location = new System.Drawing.Point(5, 42);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(50, 17);
            this.lblUserId.TabIndex = 14;
            this.lblUserId.Text = "User Id";
            // 
            // lblUserDetails
            // 
            this.lblUserDetails.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblUserDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUserDetails.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUserDetails.Location = new System.Drawing.Point(0, 0);
            this.lblUserDetails.Name = "lblUserDetails";
            this.lblUserDetails.Size = new System.Drawing.Size(967, 37);
            this.lblUserDetails.TabIndex = 13;
            this.lblUserDetails.Text = "User Details";
            this.lblUserDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabAttendance
            // 
            this.tabAttendance.Controls.Add(this.groupBox1);
            this.tabAttendance.Controls.Add(this.lvAttendanceDetails);
            this.tabAttendance.Controls.Add(this.lblAttendanceDetails);
            this.tabAttendance.Location = new System.Drawing.Point(4, 34);
            this.tabAttendance.Name = "tabAttendance";
            this.tabAttendance.Size = new System.Drawing.Size(967, 427);
            this.tabAttendance.TabIndex = 5;
            this.tabAttendance.Text = "Attendance";
            this.tabAttendance.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.btnClearAttendanceLogs);
            this.groupBox1.Controls.Add(this.btnManualSync);
            this.groupBox1.Controls.Add(this.lblManualSync);
            this.groupBox1.Controls.Add(this.lblClearAttendanceLogs);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(510, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 170);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Manual Sync or Clear Attendance Records";
            // 
            // btnClearAttendanceLogs
            // 
            this.btnClearAttendanceLogs.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnClearAttendanceLogs.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAttendanceLogs.ForeColor = System.Drawing.Color.White;
            this.btnClearAttendanceLogs.Location = new System.Drawing.Point(247, 92);
            this.btnClearAttendanceLogs.Margin = new System.Windows.Forms.Padding(5);
            this.btnClearAttendanceLogs.Name = "btnClearAttendanceLogs";
            this.btnClearAttendanceLogs.Size = new System.Drawing.Size(140, 40);
            this.btnClearAttendanceLogs.TabIndex = 17;
            this.btnClearAttendanceLogs.Text = "Clear Logs";
            this.btnClearAttendanceLogs.UseVisualStyleBackColor = false;
            this.btnClearAttendanceLogs.Click += new System.EventHandler(this.btnClearAttendanceLogs_Click);
            // 
            // btnManualSync
            // 
            this.btnManualSync.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnManualSync.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManualSync.ForeColor = System.Drawing.Color.White;
            this.btnManualSync.Location = new System.Drawing.Point(247, 32);
            this.btnManualSync.Margin = new System.Windows.Forms.Padding(5);
            this.btnManualSync.Name = "btnManualSync";
            this.btnManualSync.Size = new System.Drawing.Size(140, 40);
            this.btnManualSync.TabIndex = 19;
            this.btnManualSync.Text = "Manual Sync";
            this.btnManualSync.UseVisualStyleBackColor = false;
            this.btnManualSync.Click += new System.EventHandler(this.btnManualSync_Click);
            // 
            // lblManualSync
            // 
            this.lblManualSync.AutoSize = true;
            this.lblManualSync.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManualSync.ForeColor = System.Drawing.Color.Red;
            this.lblManualSync.Location = new System.Drawing.Point(6, 43);
            this.lblManualSync.Name = "lblManualSync";
            this.lblManualSync.Size = new System.Drawing.Size(195, 17);
            this.lblManualSync.TabIndex = 18;
            this.lblManualSync.Text = "Manually sync attendance logs";
            // 
            // lblClearAttendanceLogs
            // 
            this.lblClearAttendanceLogs.AutoSize = true;
            this.lblClearAttendanceLogs.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClearAttendanceLogs.ForeColor = System.Drawing.Color.Red;
            this.lblClearAttendanceLogs.Location = new System.Drawing.Point(6, 103);
            this.lblClearAttendanceLogs.Name = "lblClearAttendanceLogs";
            this.lblClearAttendanceLogs.Size = new System.Drawing.Size(214, 17);
            this.lblClearAttendanceLogs.TabIndex = 17;
            this.lblClearAttendanceLogs.Text = "Clear attendance logs from device";
            // 
            // lvAttendanceDetails
            // 
            this.lvAttendanceDetails.BackColor = System.Drawing.Color.White;
            this.lvAttendanceDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvAttendanceDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UserId,
            this.UserName,
            this.UserType,
            this.DateTime});
            this.lvAttendanceDetails.GridLines = true;
            this.lvAttendanceDetails.HideSelection = false;
            this.lvAttendanceDetails.Location = new System.Drawing.Point(1, 52);
            this.lvAttendanceDetails.Name = "lvAttendanceDetails";
            this.lvAttendanceDetails.Size = new System.Drawing.Size(503, 354);
            this.lvAttendanceDetails.TabIndex = 15;
            this.lvAttendanceDetails.UseCompatibleStateImageBehavior = false;
            this.lvAttendanceDetails.View = System.Windows.Forms.View.Details;
            // 
            // UserId
            // 
            this.UserId.Text = "User ID";
            this.UserId.Width = 70;
            // 
            // UserName
            // 
            this.UserName.Text = "Name";
            this.UserName.Width = 130;
            // 
            // UserType
            // 
            this.UserType.Text = "User Type";
            this.UserType.Width = 100;
            // 
            // DateTime
            // 
            this.DateTime.Text = "Date & Time";
            this.DateTime.Width = 200;
            // 
            // lblAttendanceDetails
            // 
            this.lblAttendanceDetails.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblAttendanceDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAttendanceDetails.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttendanceDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAttendanceDetails.Location = new System.Drawing.Point(0, 0);
            this.lblAttendanceDetails.Name = "lblAttendanceDetails";
            this.lblAttendanceDetails.Size = new System.Drawing.Size(967, 37);
            this.lblAttendanceDetails.TabIndex = 14;
            this.lblAttendanceDetails.Text = "Attendance Details";
            this.lblAttendanceDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPageFT
            // 
            this.tabPageFT.BackColor = System.Drawing.Color.White;
            this.tabPageFT.Controls.Add(this.gbFingerTm9and10);
            this.tabPageFT.Controls.Add(this.gbEvent);
            this.tabPageFT.Controls.Add(this.lvDownload);
            this.tabPageFT.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPageFT.ForeColor = System.Drawing.Color.DarkBlue;
            this.tabPageFT.Location = new System.Drawing.Point(4, 34);
            this.tabPageFT.Name = "tabPageFT";
            this.tabPageFT.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFT.Size = new System.Drawing.Size(967, 427);
            this.tabPageFT.TabIndex = 0;
            this.tabPageFT.Text = "FingerTemplates";
            // 
            // gbFingerTm9and10
            // 
            this.gbFingerTm9and10.BackColor = System.Drawing.SystemColors.Window;
            this.gbFingerTm9and10.Controls.Add(this.label10);
            this.gbFingerTm9and10.Controls.Add(this.label8);
            this.gbFingerTm9and10.Controls.Add(this.label3);
            this.gbFingerTm9and10.Controls.Add(this.btnUpload9);
            this.gbFingerTm9and10.Controls.Add(this.btnDownloadTmp);
            this.gbFingerTm9and10.Controls.Add(this.btnBatchUpdate);
            this.gbFingerTm9and10.ForeColor = System.Drawing.Color.Blue;
            this.gbFingerTm9and10.Location = new System.Drawing.Point(5, 306);
            this.gbFingerTm9and10.Name = "gbFingerTm9and10";
            this.gbFingerTm9and10.Size = new System.Drawing.Size(481, 113);
            this.gbFingerTm9and10.TabIndex = 92;
            this.gbFingerTm9and10.TabStop = false;
            this.gbFingerTm9and10.Text = "Fingerprint Templates of 9.0 && 10.0 Arithmetic";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Crimson;
            this.label10.Location = new System.Drawing.Point(149, 89);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(224, 15);
            this.label10.TabIndex = 76;
            this.label10.Text = "Upload Fingerprint Templates one by one";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Crimson;
            this.label8.Location = new System.Drawing.Point(148, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(222, 15);
            this.label8.TabIndex = 75;
            this.label8.Text = "Upload Fingerprint Templates in batches.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Crimson;
            this.label3.Location = new System.Drawing.Point(113, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(243, 15);
            this.label3.TabIndex = 74;
            this.label3.Text = "Download Fingerprint Templates one by one.";
            // 
            // btnUpload9
            // 
            this.btnUpload9.ForeColor = System.Drawing.Color.Black;
            this.btnUpload9.Location = new System.Drawing.Point(7, 83);
            this.btnUpload9.Name = "btnUpload9";
            this.btnUpload9.Size = new System.Drawing.Size(134, 23);
            this.btnUpload9.TabIndex = 73;
            this.btnUpload9.Text = "UploadFPTmp(common)";
            this.btnUpload9.UseVisualStyleBackColor = true;
            this.btnUpload9.Click += new System.EventHandler(this.btnUploadTmp_Click);
            // 
            // btnDownloadTmp
            // 
            this.btnDownloadTmp.ForeColor = System.Drawing.Color.Black;
            this.btnDownloadTmp.Location = new System.Drawing.Point(7, 24);
            this.btnDownloadTmp.Name = "btnDownloadTmp";
            this.btnDownloadTmp.Size = new System.Drawing.Size(94, 23);
            this.btnDownloadTmp.TabIndex = 2;
            this.btnDownloadTmp.Text = "DownloadFPTmp";
            this.btnDownloadTmp.UseVisualStyleBackColor = true;
            this.btnDownloadTmp.Click += new System.EventHandler(this.btnDownloadTmp_Click);
            // 
            // btnBatchUpdate
            // 
            this.btnBatchUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnBatchUpdate.Location = new System.Drawing.Point(7, 53);
            this.btnBatchUpdate.Name = "btnBatchUpdate";
            this.btnBatchUpdate.Size = new System.Drawing.Size(134, 23);
            this.btnBatchUpdate.TabIndex = 5;
            this.btnBatchUpdate.Text = "UploadFPTmp(batch)";
            this.btnBatchUpdate.UseVisualStyleBackColor = true;
            this.btnBatchUpdate.Click += new System.EventHandler(this.btnBatchUpdate_Click);
            // 
            // gbEvent
            // 
            this.gbEvent.BackColor = System.Drawing.Color.SkyBlue;
            this.gbEvent.Controls.Add(this.gbDataBase);
            this.gbEvent.Controls.Add(this.gbDeleteEnrollData);
            this.gbEvent.Controls.Add(this.gbDeleteUserFingerTm);
            this.gbEvent.Controls.Add(this.gbClearTMAndAdmis);
            this.gbEvent.Location = new System.Drawing.Point(492, 2);
            this.gbEvent.Name = "gbEvent";
            this.gbEvent.Size = new System.Drawing.Size(470, 417);
            this.gbEvent.TabIndex = 87;
            this.gbEvent.TabStop = false;
            // 
            // gbDataBase
            // 
            this.gbDataBase.BackColor = System.Drawing.Color.White;
            this.gbDataBase.Controls.Add(this.DeleteFpTm);
            this.gbDataBase.Controls.Add(this.label16);
            this.gbDataBase.Controls.Add(this.label15);
            this.gbDataBase.Controls.Add(this.label13);
            this.gbDataBase.Controls.Add(this.DownloadFPTemDB);
            this.gbDataBase.Controls.Add(this.btnDatabase);
            this.gbDataBase.ForeColor = System.Drawing.Color.Black;
            this.gbDataBase.Location = new System.Drawing.Point(6, 289);
            this.gbDataBase.Name = "gbDataBase";
            this.gbDataBase.Size = new System.Drawing.Size(457, 103);
            this.gbDataBase.TabIndex = 89;
            this.gbDataBase.TabStop = false;
            this.gbDataBase.Text = "Database Connection";
            // 
            // DeleteFpTm
            // 
            this.DeleteFpTm.ForeColor = System.Drawing.Color.DarkBlue;
            this.DeleteFpTm.Location = new System.Drawing.Point(9, 78);
            this.DeleteFpTm.Name = "DeleteFpTm";
            this.DeleteFpTm.Size = new System.Drawing.Size(147, 23);
            this.DeleteFpTm.TabIndex = 84;
            this.DeleteFpTm.Text = "DeleteFPTm( in Database )";
            this.DeleteFpTm.UseVisualStyleBackColor = true;
            this.DeleteFpTm.Click += new System.EventHandler(this.DeleteFpTm_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(174, 82);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(285, 15);
            this.label16.TabIndex = 83;
            this.label16.Text = "Delete Fingerprint Templates one by one in database.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(173, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(306, 15);
            this.label15.TabIndex = 82;
            this.label15.Text = "Download Fingerprint Templates one by one in database.";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(174, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(306, 15);
            this.label13.TabIndex = 81;
            this.label13.Text = "Update Fingerprint Templates one by one from database.";
            // 
            // DownloadFPTemDB
            // 
            this.DownloadFPTemDB.ForeColor = System.Drawing.Color.DarkBlue;
            this.DownloadFPTemDB.Location = new System.Drawing.Point(7, 21);
            this.DownloadFPTemDB.Name = "DownloadFPTemDB";
            this.DownloadFPTemDB.Size = new System.Drawing.Size(160, 23);
            this.DownloadFPTemDB.TabIndex = 80;
            this.DownloadFPTemDB.Text = "DownloadFPTm( in Database)";
            this.DownloadFPTemDB.UseVisualStyleBackColor = true;
            this.DownloadFPTemDB.Click += new System.EventHandler(this.DownloadFPTemDB_Click);
            // 
            // btnDatabase
            // 
            this.btnDatabase.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnDatabase.Location = new System.Drawing.Point(7, 49);
            this.btnDatabase.Name = "btnDatabase";
            this.btnDatabase.Size = new System.Drawing.Size(160, 23);
            this.btnDatabase.TabIndex = 77;
            this.btnDatabase.Text = "UpdateFPTm( from Database)";
            this.btnDatabase.UseVisualStyleBackColor = true;
            this.btnDatabase.Click += new System.EventHandler(this.btnDatabase_Click);
            // 
            // gbDeleteEnrollData
            // 
            this.gbDeleteEnrollData.BackColor = System.Drawing.Color.White;
            this.gbDeleteEnrollData.Controls.Add(this.label14);
            this.gbDeleteEnrollData.Controls.Add(this.label25);
            this.gbDeleteEnrollData.Controls.Add(this.cbUserIDDE);
            this.gbDeleteEnrollData.Controls.Add(this.btnDeleteEnrollData);
            this.gbDeleteEnrollData.Controls.Add(this.label24);
            this.gbDeleteEnrollData.Controls.Add(this.cbBackupDE);
            this.gbDeleteEnrollData.ForeColor = System.Drawing.Color.Black;
            this.gbDeleteEnrollData.Location = new System.Drawing.Point(7, 24);
            this.gbDeleteEnrollData.Name = "gbDeleteEnrollData";
            this.gbDeleteEnrollData.Size = new System.Drawing.Size(457, 85);
            this.gbDeleteEnrollData.TabIndex = 78;
            this.gbDeleteEnrollData.TabStop = false;
            this.gbDeleteEnrollData.Text = "Delete Enrolled Data";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Crimson;
            this.label14.Location = new System.Drawing.Point(9, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(269, 15);
            this.label14.TabIndex = 76;
            this.label14.Text = "Delete enrolled data according to bakcupnumber.";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.DarkBlue;
            this.label25.Location = new System.Drawing.Point(9, 56);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(41, 15);
            this.label25.TabIndex = 18;
            this.label25.Text = "UserID";
            // 
            // cbUserIDDE
            // 
            this.cbUserIDDE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserIDDE.FormattingEnabled = true;
            this.cbUserIDDE.Location = new System.Drawing.Point(58, 52);
            this.cbUserIDDE.Name = "cbUserIDDE";
            this.cbUserIDDE.Size = new System.Drawing.Size(61, 21);
            this.cbUserIDDE.TabIndex = 16;
            // 
            // btnDeleteEnrollData
            // 
            this.btnDeleteEnrollData.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnDeleteEnrollData.Location = new System.Drawing.Point(279, 51);
            this.btnDeleteEnrollData.Name = "btnDeleteEnrollData";
            this.btnDeleteEnrollData.Size = new System.Drawing.Size(141, 23);
            this.btnDeleteEnrollData.TabIndex = 15;
            this.btnDeleteEnrollData.Text = "SSR_DeleteEnrollData";
            this.btnDeleteEnrollData.UseVisualStyleBackColor = true;
            this.btnDeleteEnrollData.Click += new System.EventHandler(this.btnDeleteEnrollData_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.DarkBlue;
            this.label24.Location = new System.Drawing.Point(127, 56);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(90, 15);
            this.label24.TabIndex = 19;
            this.label24.Text = "BackupNumber";
            // 
            // cbBackupDE
            // 
            this.cbBackupDE.FormattingEnabled = true;
            this.cbBackupDE.Items.AddRange(new object[] {
            "1"});
            this.cbBackupDE.Location = new System.Drawing.Point(208, 52);
            this.cbBackupDE.Name = "cbBackupDE";
            this.cbBackupDE.Size = new System.Drawing.Size(48, 21);
            this.cbBackupDE.TabIndex = 17;
            this.cbBackupDE.Text = "1";
            // 
            // gbDeleteUserFingerTm
            // 
            this.gbDeleteUserFingerTm.BackColor = System.Drawing.Color.White;
            this.gbDeleteUserFingerTm.Controls.Add(this.label4);
            this.gbDeleteUserFingerTm.Controls.Add(this.btnSSR_DelUserTmpExt);
            this.gbDeleteUserFingerTm.Controls.Add(this.label9);
            this.gbDeleteUserFingerTm.Controls.Add(this.cbFingerIndex);
            this.gbDeleteUserFingerTm.Controls.Add(this.cbUserIDTmp);
            this.gbDeleteUserFingerTm.ForeColor = System.Drawing.Color.Black;
            this.gbDeleteUserFingerTm.Location = new System.Drawing.Point(7, 126);
            this.gbDeleteUserFingerTm.Name = "gbDeleteUserFingerTm";
            this.gbDeleteUserFingerTm.Size = new System.Drawing.Size(457, 64);
            this.gbDeleteUserFingerTm.TabIndex = 77;
            this.gbDeleteUserFingerTm.TabStop = false;
            this.gbDeleteUserFingerTm.Text = "Delete User\'s Fingerprint Templates";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(9, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "UserID";
            // 
            // btnSSR_DelUserTmpExt
            // 
            this.btnSSR_DelUserTmpExt.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSSR_DelUserTmpExt.Location = new System.Drawing.Point(279, 25);
            this.btnSSR_DelUserTmpExt.Name = "btnSSR_DelUserTmpExt";
            this.btnSSR_DelUserTmpExt.Size = new System.Drawing.Size(115, 23);
            this.btnSSR_DelUserTmpExt.TabIndex = 33;
            this.btnSSR_DelUserTmpExt.Text = "SSR_DelUserTmpExt";
            this.btnSSR_DelUserTmpExt.UseVisualStyleBackColor = true;
            this.btnSSR_DelUserTmpExt.Click += new System.EventHandler(this.btnSSR_DelUserTmpExt_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DarkBlue;
            this.label9.Location = new System.Drawing.Point(124, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 15);
            this.label9.TabIndex = 23;
            this.label9.Text = "FingerIndex";
            // 
            // cbFingerIndex
            // 
            this.cbFingerIndex.FormattingEnabled = true;
            this.cbFingerIndex.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.cbFingerIndex.Location = new System.Drawing.Point(208, 26);
            this.cbFingerIndex.Name = "cbFingerIndex";
            this.cbFingerIndex.Size = new System.Drawing.Size(48, 21);
            this.cbFingerIndex.TabIndex = 29;
            this.cbFingerIndex.Text = "0";
            // 
            // cbUserIDTmp
            // 
            this.cbUserIDTmp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserIDTmp.Location = new System.Drawing.Point(58, 26);
            this.cbUserIDTmp.Name = "cbUserIDTmp";
            this.cbUserIDTmp.Size = new System.Drawing.Size(61, 21);
            this.cbUserIDTmp.TabIndex = 21;
            // 
            // gbClearTMAndAdmis
            // 
            this.gbClearTMAndAdmis.BackColor = System.Drawing.Color.White;
            this.gbClearTMAndAdmis.Controls.Add(this.btnClearAdministrators);
            this.gbClearTMAndAdmis.Controls.Add(this.btnClearDataTmps);
            this.gbClearTMAndAdmis.Controls.Add(this.btnClearDataUserInfo);
            this.gbClearTMAndAdmis.ForeColor = System.Drawing.Color.Black;
            this.gbClearTMAndAdmis.Location = new System.Drawing.Point(7, 209);
            this.gbClearTMAndAdmis.Name = "gbClearTMAndAdmis";
            this.gbClearTMAndAdmis.Size = new System.Drawing.Size(457, 60);
            this.gbClearTMAndAdmis.TabIndex = 65;
            this.gbClearTMAndAdmis.TabStop = false;
            this.gbClearTMAndAdmis.Text = "Clear Templates/UsersInfo/Administrator Privilege";
            // 
            // btnClearAdministrators
            // 
            this.btnClearAdministrators.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnClearAdministrators.Location = new System.Drawing.Point(279, 24);
            this.btnClearAdministrators.Name = "btnClearAdministrators";
            this.btnClearAdministrators.Size = new System.Drawing.Size(131, 23);
            this.btnClearAdministrators.TabIndex = 72;
            this.btnClearAdministrators.Text = "ClearAdministrators";
            this.btnClearAdministrators.UseVisualStyleBackColor = true;
            this.btnClearAdministrators.Click += new System.EventHandler(this.btnClearAdministrators_Click);
            // 
            // btnClearDataTmps
            // 
            this.btnClearDataTmps.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnClearDataTmps.Location = new System.Drawing.Point(10, 24);
            this.btnClearDataTmps.Name = "btnClearDataTmps";
            this.btnClearDataTmps.Size = new System.Drawing.Size(99, 23);
            this.btnClearDataTmps.TabIndex = 64;
            this.btnClearDataTmps.Text = "ClearTmpsData";
            this.btnClearDataTmps.UseVisualStyleBackColor = true;
            this.btnClearDataTmps.Click += new System.EventHandler(this.btnClearDataTmps_Click);
            // 
            // btnClearDataUserInfo
            // 
            this.btnClearDataUserInfo.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnClearDataUserInfo.Location = new System.Drawing.Point(135, 24);
            this.btnClearDataUserInfo.Name = "btnClearDataUserInfo";
            this.btnClearDataUserInfo.Size = new System.Drawing.Size(121, 23);
            this.btnClearDataUserInfo.TabIndex = 63;
            this.btnClearDataUserInfo.Text = "ClearUserInfoData";
            this.btnClearDataUserInfo.UseVisualStyleBackColor = true;
            this.btnClearDataUserInfo.Click += new System.EventHandler(this.btnClearDataUserInfo_Click);
            // 
            // lvDownload
            // 
            this.lvDownload.BackColor = System.Drawing.Color.White;
            this.lvDownload.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvDownload.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch1,
            this.ch2,
            this.ch3,
            this.ch4,
            this.ch5,
            this.ch6,
            this.ch7,
            this.ch8});
            this.lvDownload.GridLines = true;
            this.lvDownload.HideSelection = false;
            this.lvDownload.Location = new System.Drawing.Point(4, 6);
            this.lvDownload.Name = "lvDownload";
            this.lvDownload.Size = new System.Drawing.Size(480, 291);
            this.lvDownload.TabIndex = 1;
            this.lvDownload.UseCompatibleStateImageBehavior = false;
            this.lvDownload.View = System.Windows.Forms.View.Details;
            // 
            // ch1
            // 
            this.ch1.Text = "UserID";
            this.ch1.Width = 54;
            // 
            // ch2
            // 
            this.ch2.Text = "Name";
            this.ch2.Width = 41;
            // 
            // ch3
            // 
            this.ch3.Text = "FingerIndex";
            this.ch3.Width = 52;
            // 
            // ch4
            // 
            this.ch4.Text = "tmpData";
            this.ch4.Width = 72;
            // 
            // ch5
            // 
            this.ch5.Text = "Privilege";
            this.ch5.Width = 77;
            // 
            // ch6
            // 
            this.ch6.Text = "Password";
            this.ch6.Width = 40;
            // 
            // ch7
            // 
            this.ch7.Text = "Ennabled";
            this.ch7.Width = 81;
            // 
            // ch8
            // 
            this.ch8.Text = "Flag";
            // 
            // tabPageCardReader
            // 
            this.tabPageCardReader.Controls.Add(this.groupBox12);
            this.tabPageCardReader.Controls.Add(this.groupBox13);
            this.tabPageCardReader.Controls.Add(this.groupBox14);
            this.tabPageCardReader.Location = new System.Drawing.Point(4, 34);
            this.tabPageCardReader.Name = "tabPageCardReader";
            this.tabPageCardReader.Size = new System.Drawing.Size(967, 427);
            this.tabPageCardReader.TabIndex = 2;
            this.tabPageCardReader.Text = "Card Reader";
            this.tabPageCardReader.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.label19);
            this.groupBox12.Controls.Add(this.btnGetHIDEventCardNumAsStr);
            this.groupBox12.Location = new System.Drawing.Point(1, 364);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(461, 64);
            this.groupBox12.TabIndex = 53;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Get the Latest HID Cardnumber after you have swiped the card on real time event";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(167, 33);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(212, 15);
            this.label19.TabIndex = 43;
            this.label19.Text = "Number of the card that is latest used.";
            // 
            // btnGetHIDEventCardNumAsStr
            // 
            this.btnGetHIDEventCardNumAsStr.Location = new System.Drawing.Point(7, 30);
            this.btnGetHIDEventCardNumAsStr.Name = "btnGetHIDEventCardNumAsStr";
            this.btnGetHIDEventCardNumAsStr.Size = new System.Drawing.Size(149, 23);
            this.btnGetHIDEventCardNumAsStr.TabIndex = 42;
            this.btnGetHIDEventCardNumAsStr.Text = "GetHIDEventCardNumAsStr";
            this.btnGetHIDEventCardNumAsStr.UseVisualStyleBackColor = true;
            this.btnGetHIDEventCardNumAsStr.Click += new System.EventHandler(this.btnGetHIDEventCardNumAsStr_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.lbRTShow);
            this.groupBox13.Location = new System.Drawing.Point(1, 8);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(461, 350);
            this.groupBox13.TabIndex = 50;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Show the RealTime Events Related with the Card";
            // 
            // lbRTShow
            // 
            this.lbRTShow.FormattingEnabled = true;
            this.lbRTShow.Location = new System.Drawing.Point(8, 16);
            this.lbRTShow.Name = "lbRTShow";
            this.lbRTShow.Size = new System.Drawing.Size(445, 329);
            this.lbRTShow.TabIndex = 4;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.label21);
            this.groupBox14.Controls.Add(this.groupBox15);
            this.groupBox14.Controls.Add(this.groupBox16);
            this.groupBox14.Location = new System.Drawing.Point(475, 6);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(491, 427);
            this.groupBox14.TabIndex = 51;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Download or Upload Card Number";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.Crimson;
            this.label21.Location = new System.Drawing.Point(12, 26);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(335, 15);
            this.label21.TabIndex = 46;
            this.label21.Text = "Please make sure your device has an optional ID card module. ";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.lvCard);
            this.groupBox15.Controls.Add(this.btnGetStrCardNumber);
            this.groupBox15.Location = new System.Drawing.Point(6, 52);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(479, 241);
            this.groupBox15.TabIndex = 43;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Download the Card Number(A property of user information)";
            // 
            // lvCard
            // 
            this.lvCard.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvCard.GridLines = true;
            this.lvCard.HideSelection = false;
            this.lvCard.Location = new System.Drawing.Point(6, 16);
            this.lvCard.Name = "lvCard";
            this.lvCard.Size = new System.Drawing.Size(467, 187);
            this.lvCard.TabIndex = 45;
            this.lvCard.UseCompatibleStateImageBehavior = false;
            this.lvCard.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "UserID";
            this.columnHeader1.Width = 54;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 41;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Cardnumber";
            this.columnHeader3.Width = 78;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Privilege";
            this.columnHeader4.Width = 92;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Password";
            this.columnHeader5.Width = 76;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Enabled";
            this.columnHeader6.Width = 84;
            // 
            // btnGetStrCardNumber
            // 
            this.btnGetStrCardNumber.Location = new System.Drawing.Point(177, 209);
            this.btnGetStrCardNumber.Name = "btnGetStrCardNumber";
            this.btnGetStrCardNumber.Size = new System.Drawing.Size(117, 23);
            this.btnGetStrCardNumber.TabIndex = 1;
            this.btnGetStrCardNumber.Text = "GetStrCardNumber";
            this.btnGetStrCardNumber.UseVisualStyleBackColor = true;
            this.btnGetStrCardNumber.Click += new System.EventHandler(this.btnGetStrCardNumber_Click);
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.cbUserId_Card);
            this.groupBox16.Controls.Add(this.txtName);
            this.groupBox16.Controls.Add(this.label22);
            this.groupBox16.Controls.Add(this.label23);
            this.groupBox16.Controls.Add(this.chbEnabled);
            this.groupBox16.Controls.Add(this.txtPassword);
            this.groupBox16.Controls.Add(this.btnSetStrCardNumber);
            this.groupBox16.Controls.Add(this.label89);
            this.groupBox16.Controls.Add(this.cbPrivilege);
            this.groupBox16.Controls.Add(this.txtCardnumber);
            this.groupBox16.Controls.Add(this.Privilege);
            this.groupBox16.Controls.Add(this.label55);
            this.groupBox16.Controls.Add(this.label90);
            this.groupBox16.Location = new System.Drawing.Point(6, 299);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(479, 123);
            this.groupBox16.TabIndex = 44;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Upload the Card Number(part of users information)";
            // 
            // cbUserId_Card
            // 
            this.cbUserId_Card.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserId_Card.FormattingEnabled = true;
            this.cbUserId_Card.Location = new System.Drawing.Point(90, 25);
            this.cbUserId_Card.Name = "cbUserId_Card";
            this.cbUserId_Card.Size = new System.Drawing.Size(69, 21);
            this.cbUserId_Card.TabIndex = 70;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(238, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(76, 23);
            this.txtName.TabIndex = 57;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(40, 28);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(62, 18);
            this.label22.TabIndex = 63;
            this.label22.Text = "User ID";
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(208, 29);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(36, 17);
            this.label23.TabIndex = 64;
            this.label23.Text = "Name";
            // 
            // chbEnabled
            // 
            this.chbEnabled.AutoSize = true;
            this.chbEnabled.Location = new System.Drawing.Point(383, 58);
            this.chbEnabled.Name = "chbEnabled";
            this.chbEnabled.Size = new System.Drawing.Size(15, 14);
            this.chbEnabled.TabIndex = 69;
            this.chbEnabled.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(383, 23);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(67, 23);
            this.txtPassword.TabIndex = 58;
            // 
            // btnSetStrCardNumber
            // 
            this.btnSetStrCardNumber.Location = new System.Drawing.Point(181, 89);
            this.btnSetStrCardNumber.Name = "btnSetStrCardNumber";
            this.btnSetStrCardNumber.Size = new System.Drawing.Size(117, 23);
            this.btnSetStrCardNumber.TabIndex = 0;
            this.btnSetStrCardNumber.Text = "SetStrCardNumber";
            this.btnSetStrCardNumber.UseVisualStyleBackColor = true;
            this.btnSetStrCardNumber.Click += new System.EventHandler(this.btnSetStrCardNumber_Click);
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(332, 60);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(55, 15);
            this.label89.TabIndex = 67;
            this.label89.Text = "Enabled  ";
            // 
            // cbPrivilege
            // 
            this.cbPrivilege.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrivilege.FormattingEnabled = true;
            this.cbPrivilege.Location = new System.Drawing.Point(90, 56);
            this.cbPrivilege.Name = "cbPrivilege";
            this.cbPrivilege.Size = new System.Drawing.Size(69, 21);
            this.cbPrivilege.TabIndex = 59;
            // 
            // txtCardnumber
            // 
            this.txtCardnumber.Location = new System.Drawing.Point(238, 56);
            this.txtCardnumber.Name = "txtCardnumber";
            this.txtCardnumber.Size = new System.Drawing.Size(76, 23);
            this.txtCardnumber.TabIndex = 61;
            // 
            // Privilege
            // 
            this.Privilege.Location = new System.Drawing.Point(30, 59);
            this.Privilege.Name = "Privilege";
            this.Privilege.Size = new System.Drawing.Size(61, 19);
            this.Privilege.TabIndex = 65;
            this.Privilege.Text = "Privilege";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(174, 60);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(76, 15);
            this.label55.TabIndex = 66;
            this.label55.Text = "CardNumber";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(327, 29);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(57, 15);
            this.label90.TabIndex = 68;
            this.label90.Text = "Password";
            // 
            // tabPageLogR
            // 
            this.tabPageLogR.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageLogR.Controls.Add(this.groupBox9);
            this.tabPageLogR.Controls.Add(this.lvLogs);
            this.tabPageLogR.ForeColor = System.Drawing.Color.DarkBlue;
            this.tabPageLogR.Location = new System.Drawing.Point(4, 34);
            this.tabPageLogR.Name = "tabPageLogR";
            this.tabPageLogR.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogR.Size = new System.Drawing.Size(967, 427);
            this.tabPageLogR.TabIndex = 1;
            this.tabPageLogR.Text = "Log Records";
            this.tabPageLogR.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.Color.White;
            this.groupBox9.Controls.Add(this.label11);
            this.groupBox9.Controls.Add(this.label12);
            this.groupBox9.Controls.Add(this.label17);
            this.groupBox9.Controls.Add(this.btnClearGLog);
            this.groupBox9.Controls.Add(this.btnGetDeviceStatus);
            this.groupBox9.Controls.Add(this.btnGetGeneralLogData);
            this.groupBox9.ForeColor = System.Drawing.Color.Blue;
            this.groupBox9.Location = new System.Drawing.Point(498, 9);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(454, 170);
            this.groupBox9.TabIndex = 10;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Download or Clear Attendance Records";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(182, 126);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(139, 15);
            this.label11.TabIndex = 17;
            this.label11.Text = "Clear all attendance logs.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(182, 84);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 15);
            this.label12.TabIndex = 16;
            this.label12.Text = "Get the total of logs.";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(182, 41);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(115, 15);
            this.label17.TabIndex = 15;
            this.label17.Text = "Get attendance logs.";
            // 
            // btnClearGLog
            // 
            this.btnClearGLog.ForeColor = System.Drawing.Color.Black;
            this.btnClearGLog.Location = new System.Drawing.Point(19, 121);
            this.btnClearGLog.Name = "btnClearGLog";
            this.btnClearGLog.Size = new System.Drawing.Size(111, 23);
            this.btnClearGLog.TabIndex = 14;
            this.btnClearGLog.Text = "ClearGLog";
            this.btnClearGLog.UseVisualStyleBackColor = true;
            this.btnClearGLog.Click += new System.EventHandler(this.btnClearGLog_Click);
            // 
            // btnGetDeviceStatus
            // 
            this.btnGetDeviceStatus.ForeColor = System.Drawing.Color.Black;
            this.btnGetDeviceStatus.Location = new System.Drawing.Point(19, 79);
            this.btnGetDeviceStatus.Name = "btnGetDeviceStatus";
            this.btnGetDeviceStatus.Size = new System.Drawing.Size(111, 23);
            this.btnGetDeviceStatus.TabIndex = 13;
            this.btnGetDeviceStatus.Text = "GetRecordCount";
            this.btnGetDeviceStatus.UseVisualStyleBackColor = true;
            this.btnGetDeviceStatus.Click += new System.EventHandler(this.btnGetDeviceStatus_Click);
            // 
            // btnGetGeneralLogData
            // 
            this.btnGetGeneralLogData.ForeColor = System.Drawing.Color.Black;
            this.btnGetGeneralLogData.Location = new System.Drawing.Point(19, 36);
            this.btnGetGeneralLogData.Name = "btnGetGeneralLogData";
            this.btnGetGeneralLogData.Size = new System.Drawing.Size(111, 23);
            this.btnGetGeneralLogData.TabIndex = 12;
            this.btnGetGeneralLogData.Text = "DownloadAttLogs";
            this.btnGetGeneralLogData.UseVisualStyleBackColor = true;
            this.btnGetGeneralLogData.Click += new System.EventHandler(this.btnGetGeneralLogData_Click);
            // 
            // lvLogs
            // 
            this.lvLogs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvLogsch1,
            this.lvLogsch2,
            this.lvLogsch3,
            this.lvLogsch4,
            this.lvLogsch5,
            this.lvLogsch6,
            this.lvLogsch7});
            this.lvLogs.GridLines = true;
            this.lvLogs.HideSelection = false;
            this.lvLogs.Location = new System.Drawing.Point(8, 15);
            this.lvLogs.Name = "lvLogs";
            this.lvLogs.Size = new System.Drawing.Size(471, 411);
            this.lvLogs.TabIndex = 1;
            this.lvLogs.UseCompatibleStateImageBehavior = false;
            this.lvLogs.View = System.Windows.Forms.View.Details;
            // 
            // lvLogsch1
            // 
            this.lvLogsch1.Text = "Count";
            this.lvLogsch1.Width = 40;
            // 
            // lvLogsch2
            // 
            this.lvLogsch2.Text = "EnrollNumber";
            this.lvLogsch2.Width = 109;
            // 
            // lvLogsch3
            // 
            this.lvLogsch3.Text = "VerifyMode";
            this.lvLogsch3.Width = 91;
            // 
            // lvLogsch4
            // 
            this.lvLogsch4.Text = "InOutMode";
            this.lvLogsch4.Width = 79;
            // 
            // lvLogsch5
            // 
            this.lvLogsch5.Text = "Date";
            this.lvLogsch5.Width = 150;
            // 
            // lvLogsch6
            // 
            this.lvLogsch6.Text = "WorkCode";
            this.lvLogsch6.Width = 50;
            // 
            // lvLogsch7
            // 
            this.lvLogsch7.Text = "Reserved";
            this.lvLogsch7.Width = 50;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.panelInfo);
            this.panel1.Controls.Add(this.lblGymName);
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(988, 87);
            this.panel1.TabIndex = 85;
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.panelInfo.Controls.Add(this.lblDataSyncVal);
            this.panelInfo.Controls.Add(this.lblPurchasedVal);
            this.panelInfo.Controls.Add(this.lblLicenseValidVal);
            this.panelInfo.Controls.Add(this.lblDataSyncKey);
            this.panelInfo.Controls.Add(this.lblPurchasedKey);
            this.panelInfo.Controls.Add(this.lblExpiryDateKey);
            this.panelInfo.Location = new System.Drawing.Point(669, 5);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(312, 78);
            this.panelInfo.TabIndex = 1;
            // 
            // lblDataSyncVal
            // 
            this.lblDataSyncVal.AutoSize = true;
            this.lblDataSyncVal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataSyncVal.Location = new System.Drawing.Point(180, 53);
            this.lblDataSyncVal.Name = "lblDataSyncVal";
            this.lblDataSyncVal.Size = new System.Drawing.Size(92, 17);
            this.lblDataSyncVal.TabIndex = 22;
            this.lblDataSyncVal.Text = "lblDataSyncVal";
            // 
            // lblPurchasedVal
            // 
            this.lblPurchasedVal.AutoSize = true;
            this.lblPurchasedVal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPurchasedVal.Location = new System.Drawing.Point(180, 31);
            this.lblPurchasedVal.Name = "lblPurchasedVal";
            this.lblPurchasedVal.Size = new System.Drawing.Size(99, 17);
            this.lblPurchasedVal.TabIndex = 21;
            this.lblPurchasedVal.Text = "lblPurchasedVal";
            // 
            // lblLicenseValidVal
            // 
            this.lblLicenseValidVal.AutoSize = true;
            this.lblLicenseValidVal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseValidVal.Location = new System.Drawing.Point(180, 8);
            this.lblLicenseValidVal.Name = "lblLicenseValidVal";
            this.lblLicenseValidVal.Size = new System.Drawing.Size(109, 17);
            this.lblLicenseValidVal.TabIndex = 20;
            this.lblLicenseValidVal.Text = "lblLicenseValidVal";
            // 
            // lblDataSyncKey
            // 
            this.lblDataSyncKey.AutoSize = true;
            this.lblDataSyncKey.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataSyncKey.Location = new System.Drawing.Point(12, 53);
            this.lblDataSyncKey.Name = "lblDataSyncKey";
            this.lblDataSyncKey.Size = new System.Drawing.Size(102, 17);
            this.lblDataSyncKey.TabIndex = 19;
            this.lblDataSyncKey.Text = "Last synced on ";
            // 
            // lblPurchasedKey
            // 
            this.lblPurchasedKey.AutoSize = true;
            this.lblPurchasedKey.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPurchasedKey.Location = new System.Drawing.Point(12, 31);
            this.lblPurchasedKey.Name = "lblPurchasedKey";
            this.lblPurchasedKey.Size = new System.Drawing.Size(138, 17);
            this.lblPurchasedKey.TabIndex = 18;
            this.lblPurchasedKey.Text = "License purchased on";
            // 
            // lblExpiryDateKey
            // 
            this.lblExpiryDateKey.AutoSize = true;
            this.lblExpiryDateKey.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpiryDateKey.Location = new System.Drawing.Point(12, 8);
            this.lblExpiryDateKey.Name = "lblExpiryDateKey";
            this.lblExpiryDateKey.Size = new System.Drawing.Size(101, 17);
            this.lblExpiryDateKey.TabIndex = 18;
            this.lblExpiryDateKey.Text = "License valid till";
            // 
            // lblGymName
            // 
            this.lblGymName.AutoSize = true;
            this.lblGymName.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.lblGymName.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblGymName.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGymName.ForeColor = System.Drawing.Color.White;
            this.lblGymName.Location = new System.Drawing.Point(11, 20);
            this.lblGymName.Margin = new System.Windows.Forms.Padding(10);
            this.lblGymName.Name = "lblGymName";
            this.lblGymName.Size = new System.Drawing.Size(133, 45);
            this.lblGymName.TabIndex = 0;
            this.lblGymName.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 588);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(987, 30);
            this.pictureBox1.TabIndex = 75;
            this.pictureBox1.TabStop = false;
            // 
            // TFT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(991, 618);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Body);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TFT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fitcode";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TFT_FormClosing);
            this.Body.ResumeLayout(false);
            this.tab.ResumeLayout(false);
            this.tabDevice.ResumeLayout(false);
            this.tabDevice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResetDeviceDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewDevices)).EndInit();
            this.tabUsers.ResumeLayout(false);
            this.tabUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClearUserId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewUsers)).EndInit();
            this.tabAttendance.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageFT.ResumeLayout(false);
            this.gbFingerTm9and10.ResumeLayout(false);
            this.gbFingerTm9and10.PerformLayout();
            this.gbEvent.ResumeLayout(false);
            this.gbDataBase.ResumeLayout(false);
            this.gbDataBase.PerformLayout();
            this.gbDeleteEnrollData.ResumeLayout(false);
            this.gbDeleteEnrollData.PerformLayout();
            this.gbDeleteUserFingerTm.ResumeLayout(false);
            this.gbDeleteUserFingerTm.PerformLayout();
            this.gbClearTMAndAdmis.ResumeLayout(false);
            this.tabPageCardReader.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.tabPageLogR.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer UserIDTimer;
        private System.Windows.Forms.GroupBox Body;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tabPageFT;
        private System.Windows.Forms.GroupBox gbFingerTm9and10;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpload9;
        private System.Windows.Forms.Button btnDownloadTmp;
        private System.Windows.Forms.Button btnBatchUpdate;
        private System.Windows.Forms.GroupBox gbEvent;
        private System.Windows.Forms.GroupBox gbDataBase;
        private System.Windows.Forms.Button DeleteFpTm;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button DownloadFPTemDB;
        private System.Windows.Forms.Button btnDatabase;
        private System.Windows.Forms.GroupBox gbDeleteEnrollData;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cbUserIDDE;
        private System.Windows.Forms.Button btnDeleteEnrollData;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cbBackupDE;
        private System.Windows.Forms.GroupBox gbDeleteUserFingerTm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSSR_DelUserTmpExt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbFingerIndex;
        private System.Windows.Forms.ComboBox cbUserIDTmp;
        private System.Windows.Forms.GroupBox gbClearTMAndAdmis;
        private System.Windows.Forms.Button btnClearAdministrators;
        private System.Windows.Forms.Button btnClearDataTmps;
        private System.Windows.Forms.Button btnClearDataUserInfo;
        private System.Windows.Forms.ListView lvDownload;
        private System.Windows.Forms.ColumnHeader ch1;
        private System.Windows.Forms.ColumnHeader ch2;
        private System.Windows.Forms.ColumnHeader ch3;
        private System.Windows.Forms.ColumnHeader ch4;
        private System.Windows.Forms.ColumnHeader ch5;
        private System.Windows.Forms.ColumnHeader ch6;
        private System.Windows.Forms.ColumnHeader ch7;
        private System.Windows.Forms.ColumnHeader ch8;
        private System.Windows.Forms.TabPage tabPageLogR;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnClearGLog;
        private System.Windows.Forms.Button btnGetDeviceStatus;
        private System.Windows.Forms.Button btnGetGeneralLogData;
        private System.Windows.Forms.ListView lvLogs;
        private System.Windows.Forms.ColumnHeader lvLogsch1;
        private System.Windows.Forms.ColumnHeader lvLogsch2;
        private System.Windows.Forms.ColumnHeader lvLogsch3;
        private System.Windows.Forms.ColumnHeader lvLogsch4;
        private System.Windows.Forms.ColumnHeader lvLogsch5;
        private System.Windows.Forms.ColumnHeader lvLogsch6;
        private System.Windows.Forms.ColumnHeader lvLogsch7;
        private System.Windows.Forms.TabPage tabPageCardReader;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Button btnGetHIDEventCardNumAsStr;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.ListBox lbRTShow;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.ListView lvCard;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnGetStrCardNumber;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.CheckBox chbEnabled;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnSetStrCardNumber;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.ComboBox cbPrivilege;
        private System.Windows.Forms.TextBox txtCardnumber;
        private System.Windows.Forms.Label Privilege;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.ComboBox cbUserId_Card;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TabPage tabDevice;
        private System.Windows.Forms.DataGridView gridviewDevices;
        private System.Windows.Forms.Label lblAddedDevices;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn model;
        private System.Windows.Forms.DataGridViewTextBoxColumn ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn port;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.TextBox txtDeviceModel;
        private System.Windows.Forms.Label lblDeviceModel;
        private System.Windows.Forms.TextBox txtDeviceName;
        private System.Windows.Forms.Label lblDeviceName;
        private System.Windows.Forms.TextBox txtDeviceIP;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox txtDevicePort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblDeviceDetails;
        private System.Windows.Forms.Button btnConnectDevice;
        private System.Windows.Forms.Button btnDeleteDevice;
        private System.Windows.Forms.Button btnUpdateDevice;
        private System.Windows.Forms.Button btnAddNewDevice;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.Label lblUserDetails;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Label lblAddedUsers;
        private System.Windows.Forms.Button btnRemoveAccess;
        private System.Windows.Forms.DataGridView gridviewUsers;
        private System.Windows.Forms.Button btnAddAccess;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddedtoDevice;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblGymName;
        private System.Windows.Forms.TabPage tabAttendance;
        private System.Windows.Forms.Label lblAttendanceDetails;
        private System.Windows.Forms.ListView lvAttendanceDetails;
        private System.Windows.Forms.ColumnHeader UserId;
        private System.Windows.Forms.ColumnHeader UserName;
        private System.Windows.Forms.ColumnHeader DateTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblClearAttendanceLogs;
        private System.Windows.Forms.Label lblManualSync;
        private System.Windows.Forms.Button btnClearAttendanceLogs;
        private System.Windows.Forms.Button btnManualSync;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblDataSyncKey;
        private System.Windows.Forms.Label lblPurchasedKey;
        private System.Windows.Forms.Label lblExpiryDateKey;
        private System.Windows.Forms.Label lblLicenseValidVal;
        private System.Windows.Forms.Label lblDataSyncVal;
        private System.Windows.Forms.Label lblPurchasedVal;
        private System.Windows.Forms.ColumnHeader UserType;
        private System.Windows.Forms.PictureBox pictureBoxResetDeviceDetails;
        private System.Windows.Forms.PictureBox pictureBoxClearUserId;
        private System.Windows.Forms.TextBox txtSearchUserId;
        private System.Windows.Forms.Label lblSerachUserId;
    }
}