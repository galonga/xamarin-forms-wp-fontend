using System;
using WpApp.Models;

namespace WpApp.Helpers
{
    public interface ITweetStore
    {
        void Save(System.Collections.Generic.List<Tweet> tweets);
        //System.Collections.Generic.List<Hanselman.Shared.Tweet> Load ();
    }
}
