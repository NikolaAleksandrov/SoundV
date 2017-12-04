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
using XFApp1.CustomRenderer;
using Plugin.Vibrate;
using System.Threading.Tasks;

namespace XFApp1.Droid.CustomRenderer
{
    class FancyGestureListener : GestureDetector.SimpleOnGestureListener
    {
        private SwipeableImage _swipeableImage;
        public FancyGestureListener(SwipeableImage inp)
        {
            _swipeableImage = inp;
        }
        public override void OnLongPress(MotionEvent e)
        {
            Console.WriteLine("OnLongPress");
            base.OnLongPress(e);
        }

        public override bool OnDoubleTap(MotionEvent e)
        {
           
            _swipeableImage.RaiseTapped();

            Task.Run(async () =>
            {
                CrossVibrate.Current.Vibration(TimeSpan.FromMilliseconds(15));
                await Task.Delay(200);
                CrossVibrate.Current.Vibration(TimeSpan.FromMilliseconds(15));
            });
            return base.OnDoubleTap(e);
        }

        public override bool OnDoubleTapEvent(MotionEvent e)
        {
            Console.WriteLine("OnDoubleTapEvent");
            return base.OnDoubleTapEvent(e);
        }

        public override bool OnSingleTapUp(MotionEvent e)
        {
            Console.WriteLine("OnSingleTapUp");
            return base.OnSingleTapUp(e);
        }

        public override bool OnDown(MotionEvent e)
        {
            Console.WriteLine("OnDown");
            return base.OnDown(e);
        }

        public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            Console.WriteLine("OnFling");
            return base.OnFling(e1, e2, velocityX, velocityY);
        }

        public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            if (Math.Abs(distanceX) > 30 || Math.Abs(distanceY) > 30)
            {
                if(Math.Abs(distanceX) > Math.Abs(distanceY))
                {
                    if (distanceX > 0) { _swipeableImage.RaiseSwipedLeft(); Console.WriteLine("L"); }
                    else { _swipeableImage.RaiseSwipedRight(); Console.WriteLine("R"); }
                }
                else
                {
                    if (distanceY > 0) { _swipeableImage.RaiseSwipedUp(); }
                    else { _swipeableImage.RaiseSwipedDown(); }
                }

            }
                Console.WriteLine("OnScroll: x: "+distanceX.ToString()+"y: "+distanceY.ToString());
            return base.OnScroll(e1, e2, distanceX, distanceY);
        }

        public override void OnShowPress(MotionEvent e)
        {
           
            Console.WriteLine("OnShowPress");
            base.OnShowPress(e);
        }

        public override bool OnSingleTapConfirmed(MotionEvent e)
        {
            CrossVibrate.Current.Vibration(TimeSpan.FromMilliseconds(15));
            Console.WriteLine("OnSingleTapConfirmed");
            return base.OnSingleTapConfirmed(e);
        }
    }
}