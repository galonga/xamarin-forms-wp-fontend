using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using XamarinFormsAnalyticsWrapper.Services;

namespace WpApp
{
    public class AppTracker : ITracker
    {
        public List<ITracker> trackers { private set; get; } = new List<ITracker>();

        public AppTracker() {
            this.trackers.Add(new AnalyticsTracker(DependencyService.Get<IAnalyticsService>()));
            this.trackers.Add(new ErrorTracker());
        }

        public void RegisterTracker(ITracker tracker)
        {
            trackers.Add(tracker);
        }

        public void AssignUserId(string userid)
        {
            Debug.WriteLine(String.Format("Track UserId: {0}", userid));
            foreach (var tracker in trackers) {
                tracker.AssignUserId(userid);
            }
        }

        public void TrackEvent(string action)
        {
            Debug.WriteLine(String.Format("Track Event: {0}", action));
            foreach (var tracker in trackers) {
                tracker.TrackEvent(action);
            }
        }

        public void TrackException(Exception exception, bool isFatal = false)
        {
            Debug.WriteLine(String.Format("Track Exception: {0}", exception.Message));
            foreach (var tracker in trackers) {
                tracker.TrackException(exception, isFatal);
            }
        }

        public void TrackScreen(string pagename)
        {
            Debug.WriteLine(String.Format("Track Page: {0}", pagename));
            foreach (var tracker in trackers) {
                tracker.TrackScreen(pagename);
            }
        }
    }
}
