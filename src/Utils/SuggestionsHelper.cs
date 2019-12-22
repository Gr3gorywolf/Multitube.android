using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App1.Models;
using HtmlAgilityPack;

namespace App1.Utils
{
    public static class SuggestionsHelper
    {
        public static List<Suggestion> GetSuggestions(string link, List<string> exceptVideos)
        {
            try
            {
                if (exceptVideos == null)
                {
                    exceptVideos = new List<string>();
                }
                var suggestions = new List<Suggestion>();
                var document = new HtmlWeb();
                var html = document.LoadFromWebAsync(link).Result;
                var allDivs = html.DocumentNode.SelectNodes("//*[contains(@class,' content-link spf-link  yt-uix-sessionlink      spf-link ')]").ToList();
                foreach (var elem in allDivs)
                {
                    var videourl = "https://www.youtube.com" + elem.Attributes["href"].Value;
                    if (exceptVideos.IndexOf(videourl) == -1)
                    {
                        suggestions.Add(new Suggestion()
                        {
                            Name = WebUtility.HtmlDecode(elem.Attributes["title"].Value),
                            Link = "https://www.youtube.com" + elem.Attributes["href"].Value,
                            Portrait = "https://i.ytimg.com/vi/" + elem.Attributes["href"].Value.Split('=')[1] + "/mqdefault.jpg"
                        });
                    }
                }

                return suggestions;
            }
            catch (Exception)
            {
                return new List<Suggestion>();

            }
        }
    }
}