using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using TimeOryx.pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Syncfusion.DataSource;
using Syncfusion.ListView.XForms;
using ItemTappedEventArgs = Xamarin.Forms.ItemTappedEventArgs;

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
            SfListView listView = new SfListView
            {
                ItemSpacing = new Thickness(0,2),
                // Определяем источник данных
                ItemsSource = ToDoLists,
                
                // Определяем формат отображения данных
                ItemTemplate = new DataTemplate(() =>
                {
                    // привязка к свойству Title
                    Label titleLabel = new Label { FontSize=24};
                    titleLabel.TextColor= Color.AliceBlue;
                    titleLabel.SetBinding(Label.TextProperty, "Name");
                    titleLabel.HorizontalTextAlignment = TextAlignment.Center;
                    Label timeLabel = new Label { FontSize=24};
                    timeLabel.TextColor= Color.AliceBlue;
                    timeLabel.SetBinding(Label.TextProperty, "Time");
                    timeLabel.HorizontalTextAlignment = TextAlignment.Center;
                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            BackgroundColor = Color.FromHex("8d8d8d"),
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = { titleLabel,timeLabel}
                        }
                    };
                })
            };
            listView.DataSource.SortDescriptors.Add(new SortDescriptor()
            {
                PropertyName = "Time",
                Direction = ListSortDirection.Ascending,
            });
            listView.RefreshView();
            listView.ItemTapped += ListViewOnItemTapped;
            StackLayout.Children.Insert(0,listView);
        }

        private void ListViewOnItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            DoList tempDoList = (DoList) e.ItemData;
            string temps = "";
            if (tempDoList.Description == String.Empty)
            {
                temps += "Описание: " + "Отсутствует" + "\n";
            }
            else
            {
                temps += "Описание: " + tempDoList.Description + "\n";
            }
            temps += "Время: " + tempDoList.Time + "\n";
            DisplayAlert(tempDoList.Name, temps, "Ok");
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ToDoLists.Count == 0)
            {
                if (CalendarPage.CalendarEvents != null)
                {

                    TempCalendarEvents = new List<DoList>(CalendarPage.CalendarEvents);
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
            if (CalendarPage.CalendarEvents != null)
            {

                TempCalendarEvents = new List<DoList>(CalendarPage.CalendarEvents);
                while (TempCalendarEvents.Exists(list => list.Date == Temps))
                {
                    DoList tempDoList = TempCalendarEvents.Find(list => list.Date == Temps);
                    ToDoLists.Add(tempDoList);
                    TempCalendarEvents.Remove(tempDoList);
                }
            }
            using (StreamWriter fs = new StreamWriter(Path.Combine(PathFile.Folderpath, "Todo.json"), false))
            {
                if (CalendarPage.CalendarEvents != null)
                    foreach (var iDoList in CalendarPage.CalendarEvents)
                    {
                        var jsonstr = JsonConvert.SerializeObject(iDoList);
                        fs.WriteLine(jsonstr);
                    }
            }
        }

        public static void AddTask(DoList tempdolist)
        {
            CalendarPage.CalendarEvents.Add(tempdolist);
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