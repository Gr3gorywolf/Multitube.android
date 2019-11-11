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
using System.Net.Sockets;
using System.IO;
namespace App1
{
    [Activity(Label = "Multitube")]
    public class actividadacciones : Activity
    {
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
        
          
            this.Finish();
        }
        public override void Finish()
        {
            MultiHelper.ExecuteGarbageCollection();
            base.Finish();
         

        }
    }
}