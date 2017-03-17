using System;
namespace WpApp
{
    public interface ITracker
    {
        void TrackScreen(string pagename);
        void TrackException(Exception exception, bool isFatal = false);
        void TrackEvent(string action);
        void AssignUserId(string userid);
    }
}
