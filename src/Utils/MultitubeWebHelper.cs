using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1.Utils
{
    class MultitubeWebHelper
    {
        public static void UpdateMultitubeWeb(string version)
        {
            var cliente = new WebClient();
            var archivo = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/version.gr3v");
            archivo.Write(version);
            archivo.Close();
            cliente.DownloadFile(new System.Uri("https://gr3gorywolf.github.io/Multitubeweb/update.zip"), Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/update.zip");

            ZipArchive ar = new ZipArchive(File.Open(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/update.zip", FileMode.Open, FileAccess.Read, FileShare.Read));
            var extpath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/";

            foreach (var entry in ar.Entries)
            {

                if (entry.FullName.EndsWith("/"))
                {
                    if (!Directory.Exists(extpath + entry.FullName))
                        Directory.CreateDirectory(extpath + entry.FullName);
                }
                else
                {
                    using (var xd = File.Create(extpath + entry.FullName))
                    {
                        entry.Open().CopyTo(xd);
                        xd.Close();
                    }
                }
            }
            File.Delete(extpath + "update.zip");
        }
    }
}