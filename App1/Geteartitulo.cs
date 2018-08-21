using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace GR3_UiF
{
    class Geteartitulo
    {

      public  string GetVideoTitle(JObject json)
        {
            JToken title = json["args"]["title"];

            return title == null ? String.Empty : title.ToString();
        }
  public  JObject LoadJson(string url)
        {
            string pageSource = "";
         
                Thread.CurrentThread.IsBackground = true;
               pageSource = HttpHelper.DownloadString(url);




        
            var dataRegex = new Regex(@"ytplayer\.config\s*=\s*(\{.+?\});", RegexOptions.Multiline);

            string extractedJson = dataRegex.Match(pageSource).Result("$1");
            
            return JObject.Parse(extractedJson);
        }

    }
    internal static class HttpHelper
    {
        public  static string DownloadString(string url)
        {
#if PORTABLE
            var request = WebRequest.Create(url);
            request.Method = "GET";

            System.Threading.Tasks.Task<WebResponse> task = System.Threading.Tasks.Task.Factory.FromAsync(
                request.BeginGetResponse,
                asyncResult => request.EndGetResponse(asyncResult),
                null);

            return task.ContinueWith(t => ReadStreamFromResponse(t.Result)).Result;
#else
            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                return client.DownloadString(url);
            }
#endif
        }
    }
}
