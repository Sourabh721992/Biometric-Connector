using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Security.Authentication;
using System.Configuration;
using System.Windows.Forms;

namespace UserInfo.Class
{
    class APILayer
    {
        private readonly static object dicUserLock = new object();

        //The below function will validate the license key
        public static Boolean ValidateLicenseKey(out string resMsg)
        {
            try
            {
                string strLicenceKey = ConfigurationManager.AppSettings["id"].ToString();
                if (String.IsNullOrEmpty(strLicenceKey))
                {
                    resMsg = "Request you to enter License Key";
                    return false;
                }
                else {
                    string response = Common.GetWebApiResponse(url: "https://app.fitcode.in/API2/validateLicenseKey",
                                                               strData: "license_key=" +strLicenceKey,
                                                               contentType: "application/x-www-form-urlencoded",
                                                               isPostMethod: true);

                    Dictionary<string, object> resObj = JsonConvert.DeserializeObject<Dictionary<string,object>>(response);

                    //Setting the response message.
                    resMsg = Convert.ToString(resObj["message"]);

                    //Check the validity of the gym
                    //The below condition says the license is valid
                    if (Convert.ToInt16(resObj["flag"]) == 1)
                    {
                        foreach(var gym in ((Newtonsoft.Json.Linq.JObject)resObj["data"]))
                        {
                            switch(gym.Key)
                            {
                                case "id": DS.DS.gymObj.gymId = Convert.ToInt32(gym.Value);
                                    break;
                                case "name": DS.DS.gymObj.gymName = Convert.ToString(gym.Value);
                                    break;
                                case "subscription_date" : 
                                    DS.DS.gymObj.subscriptionDt = Convert.ToString(gym.Value);
                                    break;
                                case "expiry_date":
                                    DS.DS.gymObj.expiryDt = Convert.ToString(gym.Value);
                                    break;
                            }
                        }
                        return true;
                    }
                    //License has some problem.
                    else
                    {
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                resMsg = "In ValidateLicenseKey: " + ex.Message;
                return false;
            }
        }

        //The function will retrieve the added devices for the gym.
        public static Boolean GetAddedDevices(out string resMsg)
        {
            try
            {
                string response = Common.GetWebApiResponse(url: "https://app.fitcode.in/API2/getDevices",
                                                                   strData: "unique_id=" + DS.DS.gymObj.gymId.ToString(),
                                                                   contentType: "application/x-www-form-urlencoded",
                                                                   isPostMethod: true);

                Dictionary<string, object> resObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

                if (Convert.ToInt16(resObj["flag"]) == 1)
                {
                    foreach (var gym in ((Newtonsoft.Json.Linq.JObject)resObj["data"]))
                    {
                        if(gym.Key == "devices")
                        {
                            foreach(var device in (gym.Value))
                            {
                                DS.Device deviceObj = new DS.Device();
                                foreach (var deviceData in ((Newtonsoft.Json.Linq.JObject)device))
                                {
                                    switch(deviceData.Key)
                                    {
                                        case "device_id": deviceObj.id = Convert.ToInt32(deviceData.Value);
                                            break;
                                        case "device_name": deviceObj.name = Convert.ToString(deviceData.Value);
                                            break;
                                        case "device_model": deviceObj.model = Convert.ToString(deviceData.Value);
                                            break;
                                        case "ip": deviceObj.ip = Convert.ToString(deviceData.Value);
                                            break;
                                        case "port": deviceObj.port = Convert.ToInt32(deviceData.Value);
                                            break;
                                        case "date": deviceObj.date = Convert.ToString(deviceData.Value);
                                            break;
                                    }
                                }
                                //Adding disconnected status as of now.
                                deviceObj.status = Common.deviceDisconnected;
                                DS.DS.lstDevices.Add(deviceObj);
                            }
                        }
                    }
                    resMsg = "";
                }
                //No Devices added for the gym.
                else
                {
                    //Check if there are no devices, or some other error occured.
                    if (Convert.ToString(resObj["message"]) != "No Devices found.")
                    {
                        //There is some problem, occured.
                        resMsg = Convert.ToString(resObj["message"]);
                    }
                    else
                        resMsg = "";

                }
                return true;
            }
            catch(Exception ex)
            {
                resMsg = "In GetAddedDevices: " + Common.errorMsg;
                return false;
            }

        }

        //The below function will be used to add new device for the gym
        public static Boolean AddNewDevices(out string resMsg, int gymId, int deviceId, string deviceName, 
                                            string deviceModel, string ip, string port, string date)
        {
            try
            {
                string requestBody = "unique_id=" + DS.DS.gymObj.gymId.ToString() +
                                      "&device_id=" + deviceId.ToString() +
                                      "&device_name=" + deviceName +
                                      "&device_model=" + deviceModel +
                                      "&ip=" + ip +
                                      "&port=" + port +
                                      "&date=" + date;

                string response = Common.GetWebApiResponse(url: "https://app.fitcode.in/API2/saveDeviceData",
                                                               strData: requestBody,
                                                               contentType: "application/x-www-form-urlencoded",
                                                               isPostMethod: true);

                Dictionary<string, object> resObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

                //Setting the response message.
                resMsg = Convert.ToString(resObj["message"]);
                if (Convert.ToInt32(resObj["flag"]) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                resMsg = Common.errorMsg;
                return false;
            }
        }

        //The below function will be used to delete the device for the gym.
        public static Boolean DeleteDevice(out string resMsg, int gymId, int deviceId)
        {
            try
            {
                string requestBody = "unique_id=" + DS.DS.gymObj.gymId.ToString() +
                                      "&device_id=" + deviceId.ToString();

                string response = Common.GetWebApiResponse(url: "https://app.fitcode.in/API2/deleteDevice",
                                                               strData: requestBody,
                                                               contentType: "application/x-www-form-urlencoded",
                                                               isPostMethod: true);

                Dictionary<string, object> resObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

                //Setting the response message.
                resMsg = Convert.ToString(resObj["message"]);
                if (Convert.ToInt32(resObj["flag"]) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                resMsg = Common.errorMsg;
                return false;
            }
        }

        //The below function will be used to update devices for the gym
        public static Boolean UpdateDevices(out string resMsg, int gymId, int deviceId, string deviceName,
                                            string deviceModel, string ip, string port, string date)
        {
            try
            {
                string requestBody = "unique_id=" + DS.DS.gymObj.gymId.ToString() +
                                      "&device_id=" + deviceId.ToString() +
                                      "&device_name=" + deviceName +
                                      "&device_model=" + deviceModel +
                                      "&ip=" + ip +
                                      "&port=" + port +
                                      "&date=" + date;

                string response = Common.GetWebApiResponse(url: "https://app.fitcode.in/API2/updateDevice",
                                                               strData: requestBody,
                                                               contentType: "application/x-www-form-urlencoded",
                                                               isPostMethod: true);

                Dictionary<string, object> resObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

                //Setting the response message.
                resMsg = Convert.ToString(resObj["message"]);
                if (Convert.ToInt32(resObj["flag"]) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                resMsg = Common.errorMsg;
                return false;
            }
        }

        //The function will be used to validate the user.
        public static Boolean ValidateUser(out string resMsg, int userId)
        {
            try
            {
                string requestBody = "unique_id=" + userId.ToString();

                string response = Common.GetWebApiResponse(url: "https://app.fitcode.in/API2/validateUser",
                                                               strData: requestBody,
                                                               contentType: "application/x-www-form-urlencoded",
                                                               isPostMethod: true);

                Dictionary<string, object> resObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

                //Setting the response message.
                resMsg = Convert.ToString(resObj["message"]);
                if (Convert.ToInt32(resObj["flag"]) == 1)
                {
                    DS.User user = new DS.User();
                    foreach (var userData in ((Newtonsoft.Json.Linq.JObject)resObj["data"]))
                    {
                        switch (userData.Key)
                        {
                            case "id":
                                user.id = userId;
                                break;
                            case "name":
                                user.name = Convert.ToString(userData.Value);
                                break;
                            case "type":
                                user.type = Convert.ToString(userData.Value);
                                break;
                            case "expiry_date":
                                user.expiryDt = Convert.ToString(userData.Value);
                                break;
                        }
                    }
                    // Adding user enable status
                    user.enabled = 1;
                    user.inDevice = 0;
                    user.date = Convert.ToString(DateTime.Now);

                    lock (dicUserLock)
                    {
                        if(DS.DS.dicUsers.ContainsKey(user.id))
                        {
                            //Update the user
                            DS.DS.dicUsers[user.id] = user;
                        }
                        else 
                            DS.DS.dicUsers.Add(user.id, user);
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                resMsg = Common.errorMsg;
                return false;
            }
        }

        //The function will add user in the database.
        public static void AddUserInDb(int gymId, int userId, byte enabled, string userName, string userType, 
                                       byte inDevice, string expiryDt, string dt)
        {
            string requestBody = "gym_id=" + gymId +
                                 "&user_id=" + userId.ToString() +
                                 "&enabled=" + enabled.ToString() +
                                 "&user_name=" + userName +
                                 "&user_type=" + userType +
                                 "&indevice=" + inDevice.ToString() +
                                 "&expiry_date=" + expiryDt +
                                 "&date=" + dt;

            string response = Common.GetWebApiResponse(url: "https://app.fitcode.in/API2/addBiometricUser",
                                                            strData: requestBody,
                                                             contentType: "application/x-www-form-urlencoded",
                                                             isPostMethod: true);

            Dictionary<string, object> resObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

            //Setting the response message.
            string resMsg = Convert.ToString(resObj["message"]);
            if (Convert.ToInt32(resObj["flag"]) == 1)
            {
                //return true;
            }
            else
            {
               // return false;
            }

        }

        //Update the user Added  in DB
       public static void UpdateUserInDb(int gymId, int userId, byte enabled, string userName, string userType,
                                       byte inDevice, string expiryDt, string dt)
        {
            string requestBody = "gym_id=" + gymId +
                                 "&user_id=" + userId.ToString() +
                                 "&enabled=" + enabled.ToString() +
                                 "&user_name=" + userName +
                                 "&user_type=" + userType +
                                 "&indevice=" + inDevice.ToString() +
                                 "&expiry_date=" + expiryDt +
                                 "&date=" + dt;

            string response = Common.GetWebApiResponse(url: "https://app.fitcode.in/API2/updateBiometricUser",
                                                       strData: requestBody,
                                                       contentType: "application/x-www-form-urlencoded",
                                                       isPostMethod: true);

            Dictionary<string, object> resObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

            //Setting the response message.
            string resMsg = Convert.ToString(resObj["message"]);
            if (Convert.ToInt32(resObj["flag"]) == 1)
            {
                //return true;
            }
            else
            {
                // return false;
            }

        }

        //Get Added Users of the gym.
        public static Boolean GetAddedUsers(out string resMsg)
        {
            try
            {
                string response = Common.GetWebApiResponse(url: "https://app.fitcode.in/API2/getBiometricUsers",
                                                                 strData: "gym_id=" + DS.DS.gymObj.gymId.ToString(),
                                                                 contentType: "application/x-www-form-urlencoded",
                                                                 isPostMethod: true);

                Dictionary<string, object> resObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

                if (Convert.ToInt16(resObj["flag"]) == 1)
                {
                    AddedUserResponse userResponse = new AddedUserResponse();
                    userResponse = JsonConvert.DeserializeObject<AddedUserResponse>(response);

                    //Add the added users in our DS
                    foreach(AddedUser user in userResponse.data)
                    {
                        DS.User userObj = new DS.User();
                        userObj.id = user.user_id;
                        userObj.date = user.date;
                        userObj.enabled = user.enabled;
                        userObj.expiryDt = user.expiry_date;
                        userObj.inDevice = user.indevice;
                        userObj.name = user.user_name;
                        userObj.type = user.user_type;

                        //Add in internal DS
                        lock (dicUserLock)
                        {
                            if (!DS.DS.dicUsers.ContainsKey(userObj.id))
                            {
                                DS.DS.dicUsers.Add(userObj.id, userObj);
                            }
                        }
                    }
                    resMsg = "";
                }
                //No Users added for the gym.
                else
                {
                    //Check if there are no users, or some other error occured.
                    if (Convert.ToString(resObj["message"]) != " No users found")
                    {
                        //There is some problem, occured.
                        resMsg = Convert.ToString(resObj["message"]);
                    }
                    else
                        resMsg = "";

                }
                return true;
            }
            catch (Exception ex)
            {
                resMsg = "In GetAddedUsers: " + Common.errorMsg;
                return false;
            }
        }

        //Get current datetime.
        public static DateTime GetCurrentDateTime()
        {
            string response = Common.GetWebApiResponse(url: "https://app.fitcode.in/API2/getDateTime",
                                                       strData: "",
                                                       contentType: "application/x-www-form-urlencoded",
                                                       isPostMethod: true);

            Dictionary<string, object> resObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

            if(Convert.ToInt32(resObj["flag"]) == 1)
            {
                foreach (var currentDate in ((Newtonsoft.Json.Linq.JObject)resObj["data"]))
                {
                    return Convert.ToDateTime(currentDate.Value);
                }
            }
            return DateTime.Now;
        }

        //Get Attendance Sync datetime.
        public static DateTime GetAttendanceSyncDateTime()
        {
            try
            {
                string response = Common.GetWebApiResponse(url: "https://app.fitcode.in/API2/getSyncDate",
                                                           strData: "gym_id=" + DS.DS.gymObj.gymId.ToString(),
                                                           contentType: "application/x-www-form-urlencoded",
                                                           isPostMethod: true);

                Dictionary<string, object> resObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

                if (Convert.ToInt32(resObj["flag"]) == 1)
                {
                    foreach (var attendanceSyncDate in ((Newtonsoft.Json.Linq.JObject)resObj["data"]))
                    {
                        return Convert.ToDateTime(attendanceSyncDate.Value);
                    }
                }
                return DateTime.MinValue;
            }
            catch(Exception ex)
            {
                return DateTime.MinValue;
            }
        }


        //Log attendance information in the system
        public static void LogAttendanceInfo(string attendanInfo)
        {
            try
            {
                string strbody = "unique_id=" + DS.DS.gymObj.gymId.ToString() +
                                 "&device_id=" + DS.DS.lstDevices[0].id +
                                 "&data=" + attendanInfo;
                string response = Common.GetWebApiResponse(url: "https://app.fitcode.in/API2/addAttendanceLog",
                                                           strData: strbody,
                                                           contentType: "application/x-www-form-urlencoded",
                                                           isPostMethod: true);

                Dictionary<string, object> resObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
                if(Convert.ToInt32(resObj["flag"]) == 1)
                {

                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                
            }

        }

        //Method will be used to save attendance date in the system
        public static void SaveAttendanceDate(DateTime dtAttendanceSyncDate)
        {
            try
            {
                string strbody = "gym_id=" + DS.DS.gymObj.gymId.ToString() +
                                 "&attendanceSyncDate=" + dtAttendanceSyncDate.ToString("yyyy-MM-dd HH:mm:ss");

                string response = Common.GetWebApiResponse(url: "https://app.fitcode.in/API2/saveSyncDate",
                                                           strData: strbody,
                                                           contentType: "application/x-www-form-urlencoded",
                                                           isPostMethod: true);

                Dictionary<string, object> resObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
                if (Convert.ToInt32(resObj["flag"]) == 1)
                {

                }
                else
                {

                }
            }
            catch(Exception ex)
            {

            }
        }

        public class AddedUserResponse
        {
            public int flag { get; set; }

            public string message { get; set; }

            public List<AddedUser> data { get; set; }
        }

        public class AddedUser
        {
            public int gym_id { get; set; }
            public int user_id { get; set; }
            public byte enabled { get; set; }
            public string user_name { get; set; }
            public string user_type { get; set; }
            public byte indevice { get; set; }
            public string expiry_date { get; set; }
            public string date { get; set; }
        }
    }
}
