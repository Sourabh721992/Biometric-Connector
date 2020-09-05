using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Windows.Forms;

namespace UserInfo.Class
{
    class Common
    {
        //Common Variables
        public static string deviceConnected = "CONNECTED";
        public static string deviceDisconnected = "DISCONNECTED";
        public static string errorMsg = "Some Error Occured. Request you to try again after sometime.";
        public static string deviceConnectionErrorMsg = "Request you to connect to device.";

        //Function will be used to fetch response of Fitcode APIS
        public static string GetWebApiResponse(string url, string strData = "", 
                                               Boolean xmlData = false, Boolean jsonData = false, 
                                               System.Collections.Specialized.NameValueCollection headerData = null, 
                                               string contentType = "", int timeOutSeconds = 0, Boolean isPostMethod = true)
        {
            try
            {
                const SslProtocols _T1s12 = (SslProtocols)0x00000C00;
                const SecurityProtocolType T1s12 = (SecurityProtocolType)_T1s12;
                ServicePointManager.SecurityProtocol = T1s12;

                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)WebRequest.Create(url);
                if (isPostMethod == true)
                    request.Method = "POST";
                else
                    request.Method = "GET";

                if (contentType != "-1")
                {
                    if (contentType != "")
                        request.ContentType = contentType;
                    else if (xmlData == true)
                        request.ContentType = "application/xml";
                    else if (jsonData == true)
                        request.ContentType = "application/json";
                    else
                        request.ContentType = "application/x-www-form-urlencoded";
                }

                if (headerData != null)
                {
                    foreach (string key in headerData)
                    {
                        request.Headers.Add(key, headerData[key]);
                    }
                }

                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";

                if (strData != "")
                {
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(strData);
                    request.ContentLength = bytes.Length;
                    var requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }

                if (timeOutSeconds > 10)
                    request.Timeout = timeOutSeconds * 1000;

                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                string htmlData = String.Empty;
                using (StreamReader sr = new StreamReader(data))
                {
                    htmlData = sr.ReadToEnd();
                }
                return htmlData;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        //The funtion will be used to generate epoch time based on datetime.
        public static long GetEpochTime(DateTime dt)
        {
            DateTime dtMin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((dt - dtMin).TotalSeconds);
        }

        //The following function will return the datetime.
        public static string GetDateTime(long epochSeconds)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(epochSeconds).ToString();
        }

        //The function will be used to resize gridview according to content.
        public static void ResizeGridView(DataGridView dgv)
        {
            DataGridViewElementStates states = DataGridViewElementStates.None;
            dgv.ScrollBars = ScrollBars.None;
            var totalHeight = dgv.Rows.GetRowsHeight(states) + dgv.ColumnHeadersHeight;
            totalHeight += dgv.Rows.Count * 4; // a correction I need
            var totalWidth = dgv.Columns.GetColumnsWidth(states) + dgv.RowHeadersWidth;
            dgv.ClientSize = new Size(totalWidth, totalHeight);
        }

        //The function will be used to generate the new device Id
        public static int GenerateDeviceId()
        {
            try
            {
                int deviceId = 0;

                foreach(DS.Device device in DS.DS.lstDevices)
                {
                    if(deviceId < device.id)
                        deviceId = device.id;
                }
                return (deviceId + 1);
                
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public static string GetErrorMessages(int errorCode)
        {
            string errorMsg = "";
            switch(errorCode)
            {
                case -100:  errorMsg = "Operating failed or data does not exist in device";
                    break;
                case -10:   errorMsg = "Transmitted data length is incorrect";
                    break;
                case -5:    errorMsg = "Entered data already exist in device";
                    break;
                case -4:    errorMsg = "Device memory is almost full";
                    break;
                case -3:    errorMsg = "Size error";
                    break;
                case -2:    errorMsg = "Not able to connect to device";
                    break;
                case -1:    errorMsg = "Request you to connect to device";
                    break;
                case 0:     errorMsg = "Data does not exist in device";
                    break;
                case 1:     errorMsg = "Operation is correct";
                    break;
                case 4:     errorMsg = "Parameters are incorrect";
                    break;
                case 101:   errorMsg = "Device is not able to allocate buffer";
                    break;
            }

            return errorMsg;
        }

    }
}
