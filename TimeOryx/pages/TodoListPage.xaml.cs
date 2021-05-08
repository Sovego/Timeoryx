using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Syncfusion.ListView.XForms;
using TimeOryx.pages;
using TimeOryx.views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
            ToDoLists = new ObservableCollection<DoList>();
            SfListView listView = new SfListView 
            {
                // Определяем источник данных
                ItemsSource = ToDoLists,
                ItemSpacing = new Thickness(0,5),
                // Определяем формат отображения данных
                ItemTemplate = new DataTemplate(() =>
                {
                    // привязка к свойству Title
                    Label titleLabel = new Label { FontSize=24, HorizontalOptions = LayoutOptions.CenterAndExpand};
                    titleLabel.TextColor= Color.AliceBlue;
                    titleLabel.SetBinding(Label.TextProperty, "Name");
                    Label timeLabel = new Label { FontSize=24,HorizontalOptions = LayoutOptions.CenterAndExpand};
                    timeLabel.TextColor= Color.AliceBlue;
                    timeLabel.SetBinding(Label.TextProperty, "Time" );
                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            BackgroundColor = Color.FromHex("#8e8e8e"),
                            Padding = new Thickness(0, 0),
                            Orientation = StackOrientation.Vertical,
                            Children = { titleLabel,timeLabel}
                        }
                    };
                })
            };
            listView.ItemTapped += ListViewOnItemTapped;
            StackLayout.Children.Insert(0,listView);
        }

        private void ListViewOnItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            DoList tempDoList = (DoList) e.ItemData;
            string temps="";
            if (tempDoList.Description == String.Empty)
            {
                temps += "Описание "+ "Отсутствует" +"\n";
            }
            else
            {
                temps += "Описание "+ tempDoList.Description +"\n";
            }
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

        public static void RemoveTask()
        {
            Calendar.CalendarEvents.Clear();
            Calendar.ReadCalendar();
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
    }
}