using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App1.Models;
using Newtonsoft.Json;

namespace App1.Utils
{
   public static class SocketHelper
    {

        public static string GetHostAddress()
        {
            string ipa = "";
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in localIPs)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipa = ip.ToString();

                }
            }
            return ipa;

        }
        public static bool IsConnected(this TcpClient s)
        {
            try
            {
                return !((s.Client.Poll(1000, SelectMode.SelectRead) && (s.Client.Available == 0)) || !s.Client.Connected);
            }
            catch (Exception) { return false; }
        }

        public static IpData GetIps()
        {
            IpData data = new IpData(null, null);
            try
            {
                data = JsonConvert.DeserializeObject<IpData>(File.ReadAllText(Constants.CachePath+ "/ips.json"));
            }
            catch (Exception)
            {
                File.Delete(Constants.CachePath + "/ips.json");
                data = new IpData("", new Dictionary<string, string>());
            }
            return data;
        }

        public static void SaveIps(IpData data)
        {

            var jsons = JsonConvert.SerializeObject(data);
            var axc = File.CreateText(Constants.CachePath + "/ips.json");
            axc.Write(jsons);
            axc.Close();
        }

        public static string GetIp()
        {


            string inneripadress = "localhost";
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in localIPs)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && ip.ToString().ToLower() != "localhost")
                {
                    inneripadress = ip.ToString();

                }
            }
            return inneripadress;
        }
    }
}