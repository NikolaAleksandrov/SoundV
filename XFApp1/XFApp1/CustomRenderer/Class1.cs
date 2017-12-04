using System;
using Xamarin.Forms;

namespace XFApp1.CustomRenderer
{
    public class SwipeableImage : Image
    {
        public event EventHandler SwipedUp;
        public event EventHandler SwipedDown;
        public event EventHandler SwipedLeft;
        public event EventHandler SwipedRight;
        public event EventHandler Tapped;

        public void RaiseSwipedUp()
        {
            if (SwipedUp != null)
                SwipedUp(this, new EventArgs());
        }

        public void RaiseSwipedDown()
        {
            if (SwipedDown != null)
                SwipedDown(this, new EventArgs());
        }

        public void RaiseSwipedLeft()
        {
            if (SwipedLeft != null)
                SwipedLeft(this, new EventArgs());
        }

        public void RaiseSwipedRight()
        {
            if (SwipedRight != null)
                SwipedRight(this, new EventArgs());
        }

        public void RaiseTapped()
        {
            if (Tapped != null)
            {
                Tapped(this, new EventArgs());
            }
        }



    }
}
