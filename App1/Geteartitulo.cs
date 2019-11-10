using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace App1
{
    class Geteartitulo
    {

        public string GetVideoTitle(string url)
        {

            var source=HttpHelper.DownloadString(url);
           return Html.GetNode("title", source).Replace(" - YouTube","");
        }
    

    }
    internal static class HttpHelper
    {
        public static string DownloadString(string url)
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







    internal static class Text
    {
        public static string StringBetween(string prefix, string suffix, string parent)
        {
            int start = parent.IndexOf(prefix) + prefix.Length;

            if (start < prefix.Length)
                return string.Empty;

            int end = parent.IndexOf(suffix, start);

            if (end == -1)
                end = parent.Length;

            return parent.Substring(start, end - start);
        }


    }


    internal static class Html
    {
        // TODO: Refactor?
        public static string GetNode(string name, string source) =>
            WebUtility.HtmlDecode(
                Text.StringBetween(
                    '<' + name + '>', "</" + name + '>', source));

        public static IEnumerable<string> GetUrisFromManifest(string source)
        {
            string opening = "<BaseURL>";
            string closing = "</BaseURL>";
            int start = source.IndexOf(opening);
            if (start != -1)
            {
                string temp = source.Substring(start);
                var uris = temp.Split(new string[] { opening }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(v => v.Substring(0, v.IndexOf(closing)));
                return uris;
            }
            throw new NotSupportedException();
        }
    }
}

