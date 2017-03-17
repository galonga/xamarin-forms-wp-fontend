using System;
using System.Collections.Generic;

namespace WpApp
{
    public class ErrorTracker : ITracker
    {
        public void TrackScreen(string pagename)
        {
        }

        public void TrackException(Exception exception, bool isFatal)
        {
            HockeyApp.MetricsManager.TrackEvent(
                exception.Message,
                new Dictionary<string, string> { { "stackTrace", exception?.StackTrace } },
                new Dictionary<string, double> { { "source", exception.HResult } }
            );
        }

        public void TrackEvent(string action)
        {
            HockeyApp.MetricsManager.TrackEvent(action);
        }

        public void AssignUserId(string userid)
        {
        }
    }
}
