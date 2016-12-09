﻿using Plugin.Share;
using Xamarin.Forms;
using WpApp.Models;
using WpApp.Common.Views.Pages;

namespace WpApp.Views
{
    public class BlogDetailsView : BaseContentPage
    {
        public BlogDetailsView(FeedItem item)
        {
            BindingContext = item;
            var webView = new WebView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            webView.Source = new HtmlWebViewSource
            {
                Html = item.Description
            };
            Content = new StackLayout
            {
                Children =
        {
          webView
        }
            };
            var share = new ToolbarItem
            {
                Icon = "ic_share.png",
                Text = "Share",
                Command = new Command(() => CrossShare.Current
                  .Share("Be sure to read @MrGalonga's " + item.Title + " " + item.Link))
            };

            ToolbarItems.Add(share);
        }
    }
}

