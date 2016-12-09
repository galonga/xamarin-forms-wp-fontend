using System;
namespace WpApp
{
    public interface ITracker
    {
        void TrackScreen(string pagename);
        void TrackException(string exception, bool isFatal);
        void TrackEvent(string action);
        void AssignUserId(string userid);
    }
}
