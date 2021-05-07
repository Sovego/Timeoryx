using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TimeOryx
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoListPage : ContentPage
    {
      public static string temps = DateTime.Today.ToString("d");
        private static List<DoList> tempCalendarEvents { get; set; }
        public static ObservableCollection<DoList> ToDoLists { get; set; }
        DoList _tempDoList = new DoList();
        private string _folderpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private string[] _tempStrings = new string[5];
        public TodoListPage()
        {
            InitializeComponent();
            ToDoLists = new ObservableCollection<DoList>();
            ListView listView = new ListView
            {
                HasUnevenRows = true,
                // Определяем источник данных
                ItemsSource = ToDoLists,
 
                // Определяем формат отображения данных
                ItemTemplate = new DataTemplate(() =>
                {
                    // привязка к свойству Name
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
                temps += "Описание "+ "Отсутствует" +"\n";
            }
            else
            {
                temps += "Описание "+ tempDoList.Description +"\n";
            }
            temps += "Время " + tempDoList.Time + "\n";
            DisplayAlert(tempDoList.Name, temps, "Ok");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ToDoLists.Count == 0)
            {
                if (Calendar.CalendarEvents != null)
                {

                    tempCalendarEvents = new List<DoList>(Calendar.CalendarEvents);
                    while (tempCalendarEvents.Exists(list => list.Date == temps))
                    {
                        DoList tempDoList = tempCalendarEvents.Find(list => list.Date == temps);
                        ToDoLists.Add(tempDoList);
                        tempCalendarEvents.Remove(tempDoList);
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

                tempCalendarEvents = new List<DoList>(Calendar.CalendarEvents);
                while (tempCalendarEvents.Exists(list => list.Date == temps))
                {
                    DoList tempDoList = tempCalendarEvents.Find(list => list.Date == temps);
                    ToDoLists.Add(tempDoList);
                    tempCalendarEvents.Remove(tempDoList);
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
    }
}