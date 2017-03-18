using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Xml.Linq;
using WpApp.Models;
using XamarinFormsAnalyticsWrapper.Services;
using WpApp.Helpers.Tracking;

namespace WpApp.ViewModels
{
    public class PodcastViewModel : BaseViewModel, IPodcastViewModel
    {
        private string image;
        readonly IAppTracker tracker;

        public PodcastViewModel(IAppTracker tracker)
        {
            this.tracker = tracker;
            image = "ic_queue_music.jpg";
            Title = "MinMon Podcast";

            tracker.TrackScreen(Title);
        }


        private ObservableCollection<FeedItem> feedItems = new ObservableCollection<FeedItem>();

        /// <summary>
        /// gets or sets the feed items
        /// </summary>
        public ObservableCollection<FeedItem> FeedItems {
            get { return feedItems; }
            set { feedItems = value; OnPropertyChanged(); }
        }

        private FeedItem selectedFeedItem;
        /// <summary>
        /// Gets or sets the selected feed item
        /// </summary>
        public FeedItem SelectedFeedItem {
            get { return selectedFeedItem; }
            set {
                selectedFeedItem = value;
                OnPropertyChanged();
            }
        }

        private Command loadItemsCommand;
        /// <summary>
        /// Command to load/refresh items
        /// </summary>
        public Command LoadItemsCommand {
            get { return loadItemsCommand ?? (loadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand())); }
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var error = false;
            try {
                var httpClient = new HttpClient();
                var feed  = "https://www.minmon.de/category/podcast/feed/";
                var responseString = await httpClient.GetStringAsync(feed);

                FeedItems.Clear();
                var items = await ParseFeed(responseString);
                foreach (var feedItem in items) {
                    FeedItems.Add(feedItem);
                }
            } catch (Exception ex){
                error = true;
                tracker.TrackException(ex);
            }

            if (error) {
                var page = new ContentPage();
                var result = page.DisplayAlert("Error", "Unable to load podcast feed.", "OK");

            }


            IsBusy = false;
        }


        /// <summary>
        /// Parse the RSS Feed
        /// </summary>
        /// <param name="rss"></param>
        /// <returns></returns>
        private async Task<List<FeedItem>> ParseFeed(string rss)
        {
            return await Task.Run(() => {
                var xdoc = XDocument.Parse(rss);
                var id = 0;
                return (from item in xdoc.Descendants("item")
                        let enclosure = item.Element("enclosure")
                        where enclosure != null
                        select new FeedItem {
                            Title = (string)item.Element("title"),
                            Description = (string)item.Element("description"),
                            Link = (string)item.Element("link"),
                            PublishDate = (string)item.Element("pubDate"),
                            Category = (string)item.Element("category"),
                            Mp3Url = (string)enclosure.Attribute("url"),
                            Image = image,
                            Id = id++
                        }).ToList();
            });
        }

        /// <summary>
        /// Gets a specific feed item for an Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FeedItem GetFeedItem(int id)
        {
            return FeedItems.FirstOrDefault(i => i.Id == id);
        }
    }
}
