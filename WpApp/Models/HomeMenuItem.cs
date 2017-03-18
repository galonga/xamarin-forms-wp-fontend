using System;

namespace WpApp.Models
{
    public enum MenuType
    {
        Home,
        Blog,
        Twitter,
        Podcast,
        Artists
    }
    public class HomeMenuItem : BaseModel
    {
        public HomeMenuItem()
        {
            MenuType = MenuType.Home;
        }
        public string Icon { get; set; }
        public MenuType MenuType { get; set; }
    }
}

