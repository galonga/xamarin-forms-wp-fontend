﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using WpApp.Views;
using BottomBar.XamarinForms;
using WpApp.ViewModels;
using WpApp.Helpers.Tracking;

namespace WpApp
{
    public class App : Application
    {
        public static bool IsWindows10 { get; set; }

        public App(
            Func<IBlogFeedViewModel> blogFeedViewModel,
            IAppTracker tracker,
            BottomBarPage mainPage
        ){
            
            mainPage.BarBackgroundColor = Color.White;


            // You can only define the color for the active icon if you set the Bottombar to fixed mode
            // So if you want to try this, just uncomment the next two lines

            //bottomBarPage.BarTextColor = Color.Blue; // Setting Color of selected Text and Icon
            //bottomBarPage.FixedMode = true;

            // Whith BarTheme you can select between light and dark theming when using FixedMode
            // When using DarkTheme you can set the Background Color by adding a colors.xml to you Android.Resources.Values
            // with content
            //
            //  <color name="white">#ffffff</color>
            //  < color name = "bb_darkBackgroundColor" >#000000</color>
            //
            // by setting "white" you can select the color of the non selected items and texts in dark theme
            // The Difference between DarkThemeWithAlpha and DarkThemeWithoutAlpha is that WithAlpha will draw not selected items with halfe the 
            // intensity instaed of solid "white" value
            //
            // Uncomment next line to use Dark Theme
            // bottomBarPage.BarTheme = BottomBarPage.BarThemeTypes.

            var homePage = new BlogPage { Title = "Home", Icon = "ic_home.png" };
            homePage.SetTabColor(null);
            mainPage.Children.Add(homePage);

            var blogPage = new BlogPage { Title = "Blog", Icon = "ic_view_list.png" };
            blogPage.SetTabColor(Color.FromHex("#5D4037"));
            mainPage.Children.Add(blogPage);

            var podcastPage = new BlogPage { Title = "Podcast", Icon = "ic_queue_music.png" };
            podcastPage.SetTabColor(Color.FromHex("#7B1FA2"));
            mainPage.Children.Add(podcastPage);

            var artistsPage = new BlogPage { Title = "Artists", Icon = "ic_people.png" };
            artistsPage.SetTabColor(Color.FromHex("#FF5252"));
            mainPage.Children.Add(artistsPage);

            var settingPage = new BlogPage { Title = "Settings", Icon = "ic_settings.png" };
            settingPage.SetTabColor(Color.FromHex("#FF9800"));
            mainPage.Children.Add(settingPage);


            //for (int i = 0; i < tabTitles.Length; ++i) {
            //    string title = tabTitles[i];
            //    string tabColor = tabColors[i];

            //    //FileImageSource icon = (FileImageSource)FileImageSource.FromFile(string.Format("ic_{0}.png", title.ToLowerInvariant()));

            //    // create tab page
            //    var tabPage = new MainPage() {
            //        Title = title,
            //        Icon = "hm.png"
            //    };

            //    // set tab color
            //    if (tabColor != null) {
            //        tabPage.SetTabColor(Color.FromHex(tabColor));
            //    }

            //    // set label based on title
            //    tabPage.UpdateLabel();

            //    // add tab pag to tab control
            //    bottomBarPage.Children.Add(tabPage);
            //}

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
