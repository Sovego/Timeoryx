using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeOryx
{


    public partial class Achievements : ContentPage
    {
        public Achievements()
        {
            InitializeComponent();
        }

        private void OpenAddAchievementsPage(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddAchievementsPage());
        }

       // public static void Refresh()
        //{
        //    AchList.Add(teQuests);
       // }

    }
}