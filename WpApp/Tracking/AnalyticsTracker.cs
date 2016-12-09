using System;
using XamarinFormsAnalyticsWrapper.Enums;
using XamarinFormsAnalyticsWrapper.Models;
using XamarinFormsAnalyticsWrapper.Services;

namespace WpApp
{
    public class AnalyticsTracker : ITracker
    {
        readonly IAnalyticsService gaService;

        public AnalyticsTracker(IAnalyticsService gaService)
        {
            this.gaService = gaService;
        }

        public void TrackScreen(string pagename)
        {
            try {
                gaService.TrackScreen(pagename, null, null, ProductActions.none, null, null, null, null);
            } catch (Exception) {
            }
        }

        public void TrackException(string exception, bool isFatal)
        {
            try {
                gaService.TrackException(exception, isFatal);
            } catch (Exception) {
            }
        }

        public void TrackEvent(string action)
        {
            try {
                gaService.TrackEvent(new EventData { EventAction = action }, null, null);
            } catch (Exception) {
            }
        }

        public void AssignUserId(string userid)
        {
            try {
                gaService.TrackUserId(userid);
            } catch (Exception) {
            }
        }
    }
}
