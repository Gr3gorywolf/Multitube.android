using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1.Utils
{
    public static class AnimationHelper
    {


        public static void AnimateFAB(Java.Lang.Object animationTarget)
        {
            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(animationTarget, "scaleX", 0f, 1f);
            animacion.SetDuration(510);
            animacion.Start();
            Android.Animation.ObjectAnimator animacion2 = Android.Animation.ObjectAnimator.OfFloat(animationTarget, "scaleY", 0f, 1f);
            animacion2.SetDuration(510);
            animacion2.Start();
        }
        
    }
}