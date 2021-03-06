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
using Android.Media;
namespace App1
{


    public class eventargs:EventArgs
    {
       public string data { get; set; }
    }

    [BroadcastReceiver]
    [IntentFilter(new[] { Intent.ActionMediaButton, AudioManager.ActionAudioBecomingNoisy })]
  
    public class broadcast_receiver : BroadcastReceiver
    {
       
        public string ComponentName { get { return Class.Name; } }
      
        public override void OnReceive(Context context, Intent intent)
        {

            if (playeroffline.gettearinstancia() != null || YoutubePlayerServerActivity.gettearinstancia() != null || Mainmenu.gettearinstancia() != null)
            {

                if (intent.Action != Intent.ActionMediaButton && intent.Action != AudioManager.ActionAudioBecomingNoisy)
                {
                    return;
                }
                else
                {
                    if (intent.Action == Intent.ActionMediaButton)
                    {



                        var keyEvent = (KeyEvent)intent.GetParcelableExtra(Intent.ExtraKeyEvent);



                        if (keyEvent.Action == KeyEventActions.Down)
                        {


                            switch (keyEvent.KeyCode)
                            {
                                case Android.Views.Keycode.Headsethook:





                                    if (playeroffline.gettearinstancia() != null)
                                    {
                                        Android.OS.Handler mHandler = new Android.OS.Handler();
                                        mHandler.PostDelayed(new Action(() => { playeroffline.gettearinstancia().counter = 0; }), 500);

                                        playeroffline.gettearinstancia().millis = SystemClock.CurrentThreadTimeMillis();





                                        if (playeroffline.gettearinstancia().counter < 1)
                                        {
                                            playeroffline.gettearinstancia().counter++;

                                            playeroffline.gettearinstancia().RunOnUiThread(() =>
                                            {
                                                playeroffline.gettearinstancia().playpause.CallOnClick();

                                            });
                                        }
                                        else
                                        {

                                            playeroffline.gettearinstancia().counter = 0;
                                            playeroffline.gettearinstancia().RunOnUiThread(() =>
                                            {
                                                playeroffline.gettearinstancia().siguiente.CallOnClick();



                                            });
                                        }






                                    }
                                    else
                                        if (YoutubePlayerServerActivity.gettearinstancia() != null)
                                    {





                                        Android.OS.Handler mHandler = new Android.OS.Handler();
                                        mHandler.PostDelayed(new Action(() => { YoutubePlayerServerActivity.gettearinstancia().counter = 0; }), 500);

                                        YoutubePlayerServerActivity.gettearinstancia().millis = SystemClock.CurrentThreadTimeMillis();





                                        if (YoutubePlayerServerActivity.gettearinstancia().counter < 1)
                                        {
                                            YoutubePlayerServerActivity.gettearinstancia().counter++;

                                            YoutubePlayerServerActivity.gettearinstancia().RunOnUiThread(() =>
                                            {
                                                YoutubePlayerServerActivity.gettearinstancia().imgPlay.CallOnClick();

                                            });
                                        }
                                        else
                                        {

                                            YoutubePlayerServerActivity.gettearinstancia().counter = 0;
                                            YoutubePlayerServerActivity.gettearinstancia().RunOnUiThread(() =>
                                            {
                                                YoutubePlayerServerActivity.gettearinstancia().imgNext.CallOnClick();



                                            });
                                        }












                                    }


                                    break;
                                case Keycode.MediaPlayPause:
                                    if (playeroffline.gettearinstancia() != null)
                                    {
                                        playeroffline.gettearinstancia().RunOnUiThread(() =>
                                        {
                                            playeroffline.gettearinstancia().playpause.CallOnClick();

                                        });
                                    }
                                    else
                                    if (YoutubePlayerServerActivity.gettearinstancia() != null)
                                    {

                                        YoutubePlayerServerActivity.gettearinstancia().RunOnUiThread(() =>
                                        {
                                            YoutubePlayerServerActivity.gettearinstancia().imgPlay.CallOnClick();

                                        });

                                    }

                                    break;
                                case Keycode.MediaNext:
                                    if (playeroffline.gettearinstancia() != null)
                                    {
                                        playeroffline.gettearinstancia().RunOnUiThread(() =>
                                        {
                                            playeroffline.gettearinstancia().siguiente.CallOnClick();

                                        });
                                    }
                                    else
                                    if (YoutubePlayerServerActivity.gettearinstancia() != null)
                                    {

                                        YoutubePlayerServerActivity.gettearinstancia().RunOnUiThread(() =>
                                        {
                                            YoutubePlayerServerActivity.gettearinstancia().imgNext.CallOnClick();

                                        });

                                    }

                                    break;
                                case Keycode.MediaPlay:
                                    if (playeroffline.gettearinstancia() != null)
                                    {
                                        playeroffline.gettearinstancia().RunOnUiThread(() =>
                                        {
                                            playeroffline.gettearinstancia().playpause.CallOnClick();

                                        });
                                    }
                                    else
                                  if (YoutubePlayerServerActivity.gettearinstancia() != null)
                                    {

                                        YoutubePlayerServerActivity.gettearinstancia().RunOnUiThread(() =>
                                        {
                                            YoutubePlayerServerActivity.gettearinstancia().imgPlay.CallOnClick();

                                        });

                                    }
                                    break;

                                case Keycode.MediaPause:
                                    if (playeroffline.gettearinstancia() != null)
                                    {
                                        playeroffline.gettearinstancia().RunOnUiThread(() =>
                                        {
                                            playeroffline.gettearinstancia().playpause.CallOnClick();

                                        });
                                    }
                                    else
                            if (YoutubePlayerServerActivity.gettearinstancia() != null)
                                    {

                                        YoutubePlayerServerActivity.gettearinstancia().RunOnUiThread(() =>
                                        {
                                            YoutubePlayerServerActivity.gettearinstancia().imgPlay.CallOnClick();

                                        });

                                    }
                                    break;
                                case Keycode.MediaPrevious:
                                    if (playeroffline.gettearinstancia() != null)
                                    {
                                        playeroffline.gettearinstancia().RunOnUiThread(() =>
                                        {
                                            playeroffline.gettearinstancia().anterior.CallOnClick();

                                        });
                                    }
                                    else
                                    if (YoutubePlayerServerActivity.gettearinstancia() != null)
                                    {

                                        YoutubePlayerServerActivity.gettearinstancia().RunOnUiThread(() =>
                                        {
                                            YoutubePlayerServerActivity.gettearinstancia().imgBack.CallOnClick();

                                        });

                                    }
                                    break;
                            }
                        }


                    }
                    else
                    {

                        if (Clouding_service.gettearinstancia() != null)
                        {

                            if (YoutubePlayerServerActivity.gettearinstancia() != null)
                            {
                                YoutubePlayerServerActivity.gettearinstancia().RunOnUiThread(() => YoutubePlayerServerActivity.gettearinstancia().imgPlay.SetBackgroundResource(Resource.Drawable.playbutton2));
                                Clouding_service.gettearinstancia().musicaplayer.Pause();
                              
                            }
                        }
                        else
                        if (Clouding_serviceoffline.gettearinstancia() != null)
                        {
                            if (playeroffline.gettearinstancia() != null)
                            {
                                playeroffline.gettearinstancia().RunOnUiThread(() => playeroffline.gettearinstancia().playpause.SetBackgroundResource(Resource.Drawable.playbutton2));
                                Clouding_serviceoffline.gettearinstancia().musicaplayer.Pause();
                               
                            }
                        }
                    }



                }
            }
            else {

            }
    }
    }
}