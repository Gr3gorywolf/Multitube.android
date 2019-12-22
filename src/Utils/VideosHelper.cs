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
using VideoLibrary;

namespace App1.Utils
{
    public static class VideosHelper
    {
        public static Client<YouTubeVideo> youtubeclient = null;


        public static string GetVideoTitle(string url)
        {
            string source = "";
            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                source = client.DownloadString(url);
            }
            return HtmlHelper.GetNode("title", source).Replace(" - YouTube", "");
        }
        public static VideoShortInfo GetShortVideoInfo(string elink, bool videoabierto, int calidad)
        {
            string videoDownloadUrl = "";
            string title = "";
            try
            {
                if (youtubeclient == null)
                {
                    youtubeclient = Client.For(YouTube.Default);
                }                
                var video = youtubeclient.GetAllVideosAsync(elink);
                var videos = video.Result;
                title = videos.First().Title.Replace("- YouTube", "");
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
                {
                    var results = videos.Where(info => info.Resolution == calidad && info.AudioFormat == AudioFormat.Aac);
                    if (results.Count() == 0)
                        while (results.Count() == 0)
                        {
                            if (calidad == 360 && results.Count() == 0)
                            {
                                results = results.Where(info => info.Resolution == 240 && info.AudioFormat == AudioFormat.Aac);
                            }
                            else
                            if (calidad == 720 && results.Count() == 0)
                            {
                                results = results.Where(info => info.Resolution == 360 && info.AudioFormat == AudioFormat.Aac);
                            }
                            if (calidad == 240 && results.Count() == 0)
                            {
                                results = results.Where(info => info.Resolution == -1 && info.AudioFormat == AudioFormat.Aac);
                            }
                        }
                    videoDownloadUrl = results.First().GetUriAsync().Result;
                }
                else
                {
                    videoDownloadUrl = videos.First(info => info.Resolution == 240 && info.AudioFormat == AudioFormat.Aac).GetUriAsync().Result;
                }
                VideoShortInfo shortInfo = new VideoShortInfo();
                if (!videoabierto)
                {
                    shortInfo.DownloadUrl = videoDownloadUrl;
                    shortInfo.Title = title;
                    return shortInfo;
                }
                else
                {
                    shortInfo.DownloadUrl = "";
                    shortInfo.Title = title;
                    return shortInfo;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}