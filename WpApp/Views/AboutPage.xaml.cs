using Xamarin.Forms;
using Plugin.Share;
using WpApp.Helpers;

namespace WpApp.Views
{
    public partial class AboutPage : ContentPage
    {

        void OpenBrowser(string url)
        {
            CrossShare.Current.OpenBrowser(url, new Plugin.Share.Abstractions.BrowserOptions {
                ChromeShowTitle = true,
                ChromeToolbarColor = new Plugin.Share.Abstractions.ShareColor { R = 3, G = 169, B = 244, A = 255 },
                UseSafairReaderMode = true,
                UseSafariWebViewController = true
            });
        }
        public AboutPage()
        {
            InitializeComponent();

            twitter.GestureRecognizers.Add(new TapGestureRecognizer() {
                Command = new Command(() => {
                    //try to launch twitter or tweetbot app, else launch browser
                    var launch = DependencyService.Get<ILaunchTwitter>();
                    if (launch == null || !launch.OpenUserName("MrGalonga"))
                        OpenBrowser("http://m.twitter.com/MrGalonga");
                })
            });

            facebook.GestureRecognizers.Add(new TapGestureRecognizer() {
                Command = new Command(() => OpenBrowser("https://m.facebook.com/MrGalonga"))
            });


            instagram.GestureRecognizers.Add(new TapGestureRecognizer() {
                Command = new Command(() => OpenBrowser("https://www.instagram.com/MrGalonga"))
            });
        }
    }
}
