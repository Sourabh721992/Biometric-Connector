using System;
using System.Collections.Generic;
using System.Text;

namespace UserInfo.DS
{
    static class DS
    {
        static DS()
        {
            gymObj = new Gym();

            lstDevices = new List<Device>();

            dicUsers = new Dictionary<int,User>();

            lstAttendance = new List<AttendanceInfo>();
        }
        //The below object will live as long as program runs.
        public static Gym gymObj { get; set; }

        //The belwo object will store Device Info
        public static List<Device> lstDevices {get; set;}

        //The below object will store user details
        public static Dictionary<int,User> dicUsers { get; set; }

        //The below object will store attendance info of user.
        public static List<AttendanceInfo> lstAttendance { get; set; }
    }

    class Gym
    {
        public int gymId { get; set; }
        public string subscriptionDt { get; set; }
        public string expiryDt { get; set; }
        public string gymName { get; set; }
        
    }

    class Device
    {
        public int id { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
        public string date { get; set; }
        public string status { get; set; }
    }

    class User
    {
        public int id { get; set; }
        public byte enabled { get; set; }
        public string name { get; set; }    
        public string type { get; set; }
        public byte inDevice { get; set; }
        public string expiryDt { get; set; }
        public string date { get; set; }
    }

    class AttendanceInfo
    {
        public int uid { get; set; }
        public string time { get; set; }
    }
}
