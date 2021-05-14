﻿using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using TimeOryx.pages;
using TimeOryx.views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TimeOryx
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoListPage : ContentPage
    {
      public static string Temps = DateTime.Today.ToString("d");
        private static List<DoList> TempCalendarEvents { get; set; }
        public static ObservableCollection<DoList> ToDoLists { get; set; }
        DoList _tempDoList = new DoList();
       
        private string[] _tempStrings = new string[5];
        public TodoListPage()
        {
            InitializeComponent();

            NotificationCenter.Current.NotificationReceived += Current_NotificationReceived;

            NotificationCenter.Current.NotificationTapped += Current_NotificationTapped;

            ToDoLists = new ObservableCollection<DoList>();
            ListView listView = new ListView
            {
                HasUnevenRows = true,
                // Определяем источник данных
                ItemsSource = ToDoLists,
 
                // Определяем формат отображения данных
                ItemTemplate = new DataTemplate(() =>
                {
                    // привязка к свойству Title
                    Label titleLabel = new Label { FontSize=18};
                    titleLabel.TextColor= Color.AliceBlue;
                    titleLabel.SetBinding(Label.TextProperty, "Name");
                    Label timeLabel = new Label { FontSize=18};
                    timeLabel.TextColor= Color.AliceBlue;
                    timeLabel.SetBinding(Label.TextProperty, "Time");
                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            BackgroundColor = Color.Black,
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = { titleLabel,timeLabel}
                        }
                    };
                })
            };
            listView.ItemTapped += ListViewOnItemTapped;
            StackLayout.Children.Insert(0,listView);
        }

        private void ListViewOnItemTapped(object sender, ItemTappedEventArgs e)
        {
            DoList tempDoList = (DoList)e.Item;
            string temps="";
            if (tempDoList.Description == String.Empty)
            {
                temps += "Описание: "+ "Отсутствует" +"\n";
            }
            else
            {
                temps += "Описание: "+ tempDoList.Description +"\n";
            }
            temps += "Время: " + tempDoList.Time + "\n";
            DisplayAlert(tempDoList.Name, temps, "Ok");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ToDoLists.Count == 0)
            {
                if (Calendar.CalendarEvents != null)
                {

                    TempCalendarEvents = new List<DoList>(Calendar.CalendarEvents);
                    while (TempCalendarEvents.Exists(list => list.Date == Temps))
                    {
                        DoList tempDoList = TempCalendarEvents.Find(list => list.Date == Temps);
                        ToDoLists.Add(tempDoList);
                        TempCalendarEvents.Remove(tempDoList);
                    }
                    // UpdateChildrenLayout();
                }
            }
        }

        public static void Refresh()
        {
            ToDoLists.Clear();
            if (Calendar.CalendarEvents != null)
            {

                TempCalendarEvents = new List<DoList>(Calendar.CalendarEvents);
                while (TempCalendarEvents.Exists(list => list.Date == Temps))
                {
                    DoList tempDoList = TempCalendarEvents.Find(list => list.Date == Temps);
                    ToDoLists.Add(tempDoList);
                    TempCalendarEvents.Remove(tempDoList);
                }
            }
        }

        public static void AddTask(DoList tempdolist)
        {
           // ToDoLists.Add(tempdolist);
            Calendar.CalendarEvents.Add(tempdolist);
            Refresh();
        }
        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddEventPage());
        }

        private void SelectDone(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SelectDone());
        }


        //Notification

        private void Current_NotificationTapped(NotificationTappedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                DisplayAlert("Nobody", e.Data, "OK");
            });
        }

        private void Current_NotificationReceived(NotificationReceivedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                DisplayAlert(e.Title, e.Description, "OK");
            });
        }


        //Composition of notification
        private void Button_Clicked(object sender, EventArgs e)
        {
            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = "Test description",
                Title = "Notification",
                ReturningData = "Dummy Data",
                NotificationId = 1337,
                //Repeats = NotificationRepeat.Daily,
                NotifyTime = DateTime.Now.AddSeconds(5)
            };

            NotificationCenter.Current.Show(notification);

        }

    }
}