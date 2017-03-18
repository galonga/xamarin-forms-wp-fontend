using System;

namespace WpApp.Helpers.Tracking
{
    public interface IAppTracker : ITracker
    {
        void RegisterTracker(ITracker tracker);
    }
}
