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
using System.Threading;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Token;
using System.IO;
using System.Net.Sockets;
using Firebase.Xamarin.Database.Query;

namespace App1
{
    [Service(Exported =true)]
    public class serviciointerpreter : IntentService
    {
        public IBinder Binder { get; private set; }

        protected override void OnHandleIntent(Android.Content.Intent intent)
        {

            string cogida1 = intent.GetStringExtra("querry1");
            string cogida2 = intent.GetStringExtra("querry2");
            string cogida3= intent.GetStringExtra("querry3");
            string cogida4 = intent.GetStringExtra("querry4");
            string cogida5 = intent.GetStringExtra("querry5");
            if (cogida1 == "si")
            {
             
                playeroffline.gettearinstancia().playpause.CallOnClick();
                // clasesettings.guardarsetting("cquerry", "playpause()");
            }
            else
            if (cogida2 == "si")
            {
                playeroffline.gettearinstancia().siguiente.CallOnClick();
                //  clasesettings.guardarsetting("cquerry", "siguiente()");
            }
            else
            if (cogida3 == "si")
            {
                playeroffline.gettearinstancia().anterior.CallOnClick();
                //  clasesettings.guardarsetting("cquerry", "anterior()");
            }
            else
             if (cogida4 == "si")
            {
                playeroffline.gettearinstancia().adelantar.CallOnClick();
                /// clasesettings.guardarsetting("cquerry", "adelantar()");
            }
            else
            if (cogida5 == "si")
            {
                playeroffline.gettearinstancia().atrazar.CallOnClick();
                ///  clasesettings.guardarsetting("cquerry", "atrazar()");
            }
          



               this.StopSelf();
        }

        public override IBinder OnBind(Intent intent)
        {



            return this.Binder;
        }
        public override void OnCreate()
        {
            base.OnCreate();
            

     
        }
    }


    /// //////////////////////////////////////////////////////////////////////////////////////
    [Service(Exported = true)]
    public class serviciointerpreter3 : IntentService
    {
        public IBinder Binder { get; private set; }

        protected override void OnHandleIntent(Android.Content.Intent intent)
        {

            string cogida1 = intent.GetStringExtra("querry1");
            string cogida2 = intent.GetStringExtra("querry2");
            string cogida3 = intent.GetStringExtra("querry3");
            string cogida4 = intent.GetStringExtra("querry4");
            string cogida5 = intent.GetStringExtra("querry5");
            if (cogida1 == "si" && !MainmenuOffline.gettearinstancia().envideo)
            {
               
                MainmenuOffline.gettearinstancia().RunOnUiThread(()=> {
                    MainmenuOffline.gettearinstancia().play.CallOnClick();
                    
                    });
                // clasesettings.guardarsetting("cquerry", "playpause()");
            }
            else
            if (cogida2 == "si" && !MainmenuOffline.gettearinstancia().envideo)
            {

                MainmenuOffline.gettearinstancia().RunOnUiThread(() => {
                    MainmenuOffline.gettearinstancia().adelante.CallOnClick();

                });
                //  clasesettings.guardarsetting("cquerry", "siguiente()");
            }
            else
            if (cogida3 == "si" && !MainmenuOffline.gettearinstancia().envideo)
            {

                MainmenuOffline.gettearinstancia().RunOnUiThread(() => {
                    MainmenuOffline.gettearinstancia().atras.CallOnClick();

                });
                //  clasesettings.guardarsetting("cquerry", "anterior()");
            }
            else
             if (cogida4 == "si" && !MainmenuOffline.gettearinstancia().envideo)
            {

                MainmenuOffline.gettearinstancia().RunOnUiThread(() => {
                    MainmenuOffline.gettearinstancia().adelantar.CallOnClick();

                });
                /// clasesettings.guardarsetting("cquerry", "adelantar()");
            }
            else
            if (cogida5 == "si" && !MainmenuOffline.gettearinstancia().envideo)
            {

                MainmenuOffline.gettearinstancia().RunOnUiThread(() => {
                    MainmenuOffline.gettearinstancia().atrazar.CallOnClick();

                });
                ///  clasesettings.guardarsetting("cquerry", "atrazar()");
            }



            this.StopSelf();
        }

        public override IBinder OnBind(Intent intent)
        {



            return this.Binder;
        }
        public override void OnCreate()
        {
            base.OnCreate();



        }
    }

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////
    /// </summary>
    /// 
    /// 
    /// 
    /// 
    [Service(Exported = true)]
    public class serviciointerpreter23 : IntentService
    {
        public IBinder Binder { get; private set; }

        protected override void OnHandleIntent(Android.Content.Intent intent)
        {



            StopService(new Intent(this, typeof(serviciostreaming)));
            this.StopSelf();






        }

        public override IBinder OnBind(Intent intent)
        {



            return this.Binder;
        }
        public override void OnCreate()
        {
            base.OnCreate();



        }


        public  void si(object sender, EventArgs e)
        {
          

        }
        public  void no(object sender, EventArgs e)
        {
            
            this.StopSelf();
        }
    }



    ////////////////////////////////////////////////////////////////////



    [Service(Exported = true)]
    public class serviciointerpreter234 : IntentService
    {
        public IBinder Binder { get; private set; }

        protected override async void OnHandleIntent(Android.Content.Intent intent)
        {



            ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var resultado = await scanner.Scan();
            if (resultado != null)
            {             

                Random rondom = new Random();
                char[] array = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm' };
                string serial = rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString()
                    +
                    rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString()
                     +
                    rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString()
                     +
                    rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString()
                     +
                    rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString();

                //  Toast.MakeText(this, "Por favor espere mientras se vinculan los dispositivos", ToastLength.Long).Show();
                try
                {
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDozWqE4WZwrY_VVutTTnIlzbG-NkEni_I"));

                    var auth = await authProvider.SignInWithEmailAndPasswordAsync("gregoryalexandercabral@gmail.com", "gregory123456");
                    var token = auth.FirebaseToken;
                    var firebase = new FirebaseClient("https://dadass-d7a51.firebaseio.com");
                    var mapita = new Dictionary<string, string>();
                    mapita.Add(MultiHelper.DeviceId, serial);

                    await firebase.Child("WEB").Child(resultado.Text).WithAuth(token).PatchAsync<Dictionary<string, string>>(mapita);
                  

                }
                catch (Exception xd) {
                    Console.WriteLine(xd.Message);
                }


            }
            this.StopSelf();






        }

        public override IBinder OnBind(Intent intent)
        {



            return this.Binder;
        }
        public override void OnCreate()
        {
            base.OnCreate();



        }


        public void si(object sender, EventArgs e)
        {


        }
        public void no(object sender, EventArgs e)
        {

            this.StopSelf();
        }
    }





    /// //////////////////////////////////////////////////////////////////

    [Service(Exported = true)]
    public class serviciointerpreter2 : IntentService
    {
        public IBinder Binder { get; private set; }

        protected override void OnHandleIntent(Android.Content.Intent intent)
        {
            string ipadress=intent.GetStringExtra("ipadre");
            string cogida1 = intent.GetStringExtra("querry1");
            string cogida2 = intent.GetStringExtra("querry2");
            string cogida3 = intent.GetStringExtra("querry3");
            string cogida4 = intent.GetStringExtra("querry4");
            string cogida5 = intent.GetStringExtra("querry5");
            if (cogida1 == "si")
            {
                Mainmenu.gettearinstancia().RunOnUiThread(() =>
                {
                    Mainmenu.gettearinstancia().play.CallOnClick();
                });
            }
            else
            if (cogida2 == "si")
            {
                Mainmenu.gettearinstancia().RunOnUiThread(() =>
                {
                    Mainmenu.gettearinstancia().adelante.CallOnClick();
                });
            }
            else
            if (cogida3 == "si")
            {
                Mainmenu.gettearinstancia().RunOnUiThread(() =>
                {
                    Mainmenu.gettearinstancia().atras.CallOnClick();
                });
            }
            else
             if (cogida4 == "si")
            {
                Mainmenu.gettearinstancia().RunOnUiThread(() =>
                {
                    Mainmenu.gettearinstancia().adelantar.CallOnClick();
                });
            }
            else
            if (cogida5 == "si")
            {
                Mainmenu.gettearinstancia().RunOnUiThread(() =>
                {
                    Mainmenu.gettearinstancia().atrazar.CallOnClick();
                });
            }




            this.StopSelf();
        }

        public override IBinder OnBind(Intent intent)
        {



            return this.Binder;
        }
        public override void OnCreate()
        {
            base.OnCreate();



        }
    }

}