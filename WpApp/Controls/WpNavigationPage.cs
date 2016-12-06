using System;
using Xamarin.Forms;

namespace WpApp.Controls
{
    public class WpNavigationPage : NavigationPage
    {
        public WpNavigationPage(Page root) : base(root)
        {
            Init();
        }

        public WpNavigationPage()
        {
            Init();
        }

        void Init()
        {

            BarBackgroundColor = Color.FromHex("#03A9F4");
            BarTextColor = Color.White;
        }
    }
}

