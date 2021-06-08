using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeOryx
{

    public partial class Options : ContentPage
    {
        ApplicationViewModel viewModel;

        

        public Options()
        {
            InitializeComponent();
            viewModel = new ApplicationViewModel() { Navigation = this.Navigation };
            BindingContext = viewModel;

        }

        protected override async void OnAppearing()
        {
            await viewModel.GetFriends();
            base.OnAppearing();
        }

        private void Button_Notification_Clicked(object sender, EventArgs e)
        {

        }

        
    }
}