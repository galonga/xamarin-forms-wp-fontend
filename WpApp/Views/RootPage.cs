﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using WpApp.Models;
using WpApp.ViewsModels;
using WpApp.Controls;

namespace WpApp.Views
{
    public class RootPage : MasterDetailPage
    {
        public static bool IsUWPDesktop { get; set; }
        Dictionary<MenuType, NavigationPage> Pages { get; set; }
        public RootPage()
        {
            Pages = new Dictionary<MenuType, NavigationPage>();
            Master = new MenuPage(this);
            BindingContext = new BaseViewModel {
                Title = "Wordpress.Forms",
                Icon = "slideout.png"
            };
            //setup home page
            NavigateAsync(MenuType.About);

            InvalidateMeasure();
        }

        public async Task NavigateAsync(MenuType id)
        {

            if (Detail != null) {
                if (IsUWPDesktop || Device.Idiom != TargetIdiom.Tablet)
                    IsPresented = false;

                if (Device.OS == TargetPlatform.Android)
                    await Task.Delay(300);
            }

            Page newPage;
            if (!Pages.ContainsKey(id)) {

                switch (id) {
                    case MenuType.About:
                        Pages.Add(id, new WpNavigationPage(new AboutPage()));
                        break;
                    case MenuType.Blog:
                        Pages.Add(id, new WpNavigationPage(new BlogPage()));
                        break;
                    case MenuType.DeveloperLife:
                        Pages.Add(id, new WpNavigationPage(new PodcastPage(id)));
                        break;
                    case MenuType.Hanselminutes:
                        Pages.Add(id, new WpNavigationPage(new PodcastPage(id)));
                        break;
                    case MenuType.Ratchet:
                        Pages.Add(id, new WpNavigationPage(new PodcastPage(id)));
                        break;
                    case MenuType.Twitter:
                        Pages.Add(id, new WpNavigationPage(new TwitterPage()));
                        break;
                }
            }

            newPage = Pages[id];
            if (newPage == null)
                return;

            //pop to root for Windows Phone
            if (Detail != null && Device.OS == TargetPlatform.WinPhone) {
                await Detail.Navigation.PopToRootAsync();
            }

            Detail = newPage;
        }
    }
}

