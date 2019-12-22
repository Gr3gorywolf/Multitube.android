using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace App1.Models
{
    public class IpData
    {
        [JsonProperty(PropertyName = "ipactual")]
        public string Ip { get; set; }
        public Dictionary<string, string> Ips { get; set; }
        public IpData(string ip, Dictionary<string, string> ips)
        {
            this.Ip = ip;
            this.Ips = ips;
        }
    }
}