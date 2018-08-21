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
            if (cogida1 == "si" && !mainmenu_Offline.gettearinstancia().envideo)
            {
               
                mainmenu_Offline.gettearinstancia().RunOnUiThread(()=> {
                    mainmenu_Offline.gettearinstancia().play.CallOnClick();
                    
                    });
                // clasesettings.guardarsetting("cquerry", "playpause()");
            }
            else
            if (cogida2 == "si" && !mainmenu_Offline.gettearinstancia().envideo)
            {

                mainmenu_Offline.gettearinstancia().RunOnUiThread(() => {
                    mainmenu_Offline.gettearinstancia().adelante.CallOnClick();

                });
                //  clasesettings.guardarsetting("cquerry", "siguiente()");
            }
            else
            if (cogida3 == "si" && !mainmenu_Offline.gettearinstancia().envideo)
            {

                mainmenu_Offline.gettearinstancia().RunOnUiThread(() => {
                    mainmenu_Offline.gettearinstancia().atras.CallOnClick();

                });
                //  clasesettings.guardarsetting("cquerry", "anterior()");
            }
            else
             if (cogida4 == "si" && !mainmenu_Offline.gettearinstancia().envideo)
            {

                mainmenu_Offline.gettearinstancia().RunOnUiThread(() => {
                    mainmenu_Offline.gettearinstancia().adelantar.CallOnClick();

                });
                /// clasesettings.guardarsetting("cquerry", "adelantar()");
            }
            else
            if (cogida5 == "si" && !mainmenu_Offline.gettearinstancia().envideo)
            {

                mainmenu_Offline.gettearinstancia().RunOnUiThread(() => {
                    mainmenu_Offline.gettearinstancia().atrazar.CallOnClick();

                });
                ///  clasesettings.guardarsetting("cquerry", "atrazar()");
            }

            ///////////////////////////////////////cuando esta en video
            //////////////////////////////////////////////////////

            if (cogida1 == "si" && mainmenu_Offline.gettearinstancia().envideo)
            {

                actvideo.gettearinstacia().RunOnUiThread(() => {
                    actvideo.gettearinstacia().playpause.CallOnClick();

                });
                // clasesettings.guardarsetting("cquerry", "playpause()");
            }
            else
          if (cogida2 == "si" && mainmenu_Offline.gettearinstancia().envideo)
            {

                actvideo.gettearinstacia().RunOnUiThread(() => {
                    actvideo.gettearinstacia().siguiente.CallOnClick();

                });
                //  clasesettings.guardarsetting("cquerry", "siguiente()");
            }
            else
          if (cogida3 == "si" && mainmenu_Offline.gettearinstancia().envideo)
            {

                actvideo.gettearinstacia().RunOnUiThread(() => {
                    actvideo.gettearinstacia().anterior.CallOnClick();

                });
                //  clasesettings.guardarsetting("cquerry", "anterior()");
            }
            else
           if (cogida4 == "si" && mainmenu_Offline.gettearinstancia().envideo)
            {

                actvideo.gettearinstacia().RunOnUiThread(() => {
                    actvideo.gettearinstacia().adelantar.CallOnClick();

                });
                /// clasesettings.guardarsetting("cquerry", "adelantar()");
            }
            else
          if (cogida5 == "si" && mainmenu_Offline.gettearinstancia().envideo)
            {

                actvideo.gettearinstacia().RunOnUiThread(() => {
                    actvideo.gettearinstacia().atrazar.CallOnClick();

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
                mainmenu.gettearinstancia().RunOnUiThread(() =>
                {
                    mainmenu.gettearinstancia().play.CallOnClick();
                });
            }
            else
            if (cogida2 == "si")
            {
                mainmenu.gettearinstancia().RunOnUiThread(() =>
                {
                    mainmenu.gettearinstancia().adelante.CallOnClick();
                });
            }
            else
            if (cogida3 == "si")
            {
                mainmenu.gettearinstancia().RunOnUiThread(() =>
                {
                    mainmenu.gettearinstancia().atras.CallOnClick();
                });
            }
            else
             if (cogida4 == "si")
            {
                mainmenu.gettearinstancia().RunOnUiThread(() =>
                {
                    mainmenu.gettearinstancia().adelantar.CallOnClick();
                });
            }
            else
            if (cogida5 == "si")
            {
                mainmenu.gettearinstancia().RunOnUiThread(() =>
                {
                    mainmenu.gettearinstancia().atrazar.CallOnClick();
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