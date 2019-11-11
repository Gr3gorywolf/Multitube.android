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

namespace App1
{
  
    public class customvideoview : VideoView
    {
        private int mForceHeight = 0;
        private int mForceWidth = 0;
        public customvideoview(Context context) : base(context)
        {

        }

        public customvideoview(Context context, Android.Util.IAttributeSet attrs) : base(context, attrs)
        {
        }

        public customvideoview(Context context, Android.Util.IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {

        }
        public void setDimensions(int w, int h)
        {
            this.mForceHeight = h;
            this.mForceWidth = w;

        }
        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
         //   int width = GetDefaultSize(Width / 2, widthMeasureSpec);
         //  int height = GetDefaultSize(Height / 2, heightMeasureSpec);
            //SetMeasuredDimension(width, height / 2);// setMeasuredDimension(width, height);
            //int width = (int)Resources.GetDimension(Resource.Dimension.ProductD_Pic_W);
           /// int height = (int)Resources.GetDimension(Resource.Dimension.ProductD_Pic_H);
           /// 
       
           if(mForceHeight>0 && mForceWidth > 0)
            {
                SetMeasuredDimension(mForceWidth, mForceHeight);
            }
            else
            {
                SetMeasuredDimension(widthMeasureSpec, heightMeasureSpec);
            }
          
        }


    }
}