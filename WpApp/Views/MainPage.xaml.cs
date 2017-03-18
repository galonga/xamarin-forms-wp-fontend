using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WpApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void UpdateLabel()
        {
            Label.Text = string.Format(Label.Text, Title);
        }
    }
}
