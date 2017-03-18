using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using WpApp.Models;

namespace WpApp.ViewsModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<HomeMenuItem> MenuItems { get; set; }
        readonly AppTracker tracker;

        public HomeViewModel()
        {
            this.tracker = new AppTracker();

            CanLoadMore = true;
            Title = "Hanselman";
            MenuItems = new ObservableCollection<HomeMenuItem>();

            MenuItems.Add(new HomeMenuItem {
                Id = 0,
                Title = "About",
                MenuType = MenuType.Home,
                Icon = "about.png"
            });

            MenuItems.Add(new HomeMenuItem {
                Id = 1,
                Title = "Blog",
                MenuType = MenuType.Blog,
                Icon = "blog.png"
            });

            MenuItems.Add(new HomeMenuItem {
                Id = 2,
                Title = "Twitter",
                MenuType = MenuType.Twitter,
                Icon = "twitternav.png"
            });

            MenuItems.Add(new HomeMenuItem {
                Id = 3,
                Title = "Hanselminutes",
                MenuType = MenuType.Podcast,
                Icon = "hm.png"
            });

            MenuItems.Add(new HomeMenuItem {
                Id = 4,
                Title = "Ratchet & The Geek",
                MenuType = MenuType.Artists,
                Icon = "ratchet.png"
            });
        }
    }
}
