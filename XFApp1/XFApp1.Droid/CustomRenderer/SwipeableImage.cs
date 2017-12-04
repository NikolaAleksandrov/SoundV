using System;
using Android.Views;
using Xamarin.Forms.Platform.Android;
using XFApp1.CustomRenderer;
using Xamarin.Forms;
using XFApp1.Droid.CustomRenderer;

[assembly: ExportRenderer(typeof(SwipeableImage), typeof(SwipeableDroidImageRenderer))]
namespace XFApp1.Droid.CustomRenderer
{
    public class SwipeableDroidImageRenderer : ImageRenderer
    {
        public float X1 { get; set; }
        public float X2 { get; set; }
        public float Y1 { get; set; }
        public float Y2 { get; set; }
        private  FancyGestureListener _listener;
        private  GestureDetector _detector;
        public SwipeableImage SwipeableImage { get; set; }
        public override bool OnTouchEvent(MotionEvent e)
        {

            if (e.ActionMasked == MotionEventActions.Down)
            {
                X1 = e.GetX();
                Y1 = e.GetY();

                return true;
            }

            X2 = e.GetX();
            Y2 = e.GetY();

            var xChange = X1 - X2;
            var yChange = Y1 - Y2;

            var xChangeSize = Math.Abs(xChange);
            var yChangeSize = Math.Abs(yChange);
           
            if (xChangeSize > 30 || yChangeSize > 30)
            {
                if (xChangeSize > yChangeSize)
                {
                    // horizontal
                    if (X1 > X2)
                    {
                        // left
                        SwipeableImage.RaiseSwipedLeft();
                    }
                    else
                    {
                        // right
                        SwipeableImage.RaiseSwipedRight();
                    }
                }
                else
                {
                    // vertical
                    if (Y1 > Y2)
                    {
                        // up
                        SwipeableImage.RaiseSwipedUp();
                    }
                    else
                    {
                        // down
                        SwipeableImage.RaiseSwipedDown();
                    }
                }
            }


            return base.OnTouchEvent(e);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> ev)
        {
            base.OnElementChanged(ev);
           
            SwipeableImage = (SwipeableImage)ev.NewElement;
            _listener = new FancyGestureListener(SwipeableImage);
            _detector = new GestureDetector(_listener);
            if (ev.NewElement == null)
            {
                this.GenericMotion -= HandleGenericMotion;
                this.Touch -= HandleTouch;
            }

            if (ev.OldElement == null)
            {
                this.GenericMotion += HandleGenericMotion;
                this.Touch += HandleTouch;
            }

        }
        void HandleTouch(object sender, TouchEventArgs e)
        {
            _detector.OnTouchEvent(e.Event);
        }

        void HandleGenericMotion(object sender, GenericMotionEventArgs e)
        {
            _detector.OnTouchEvent(e.Event);
        }

    }
}