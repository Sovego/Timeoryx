using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.ListView.XForms;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Xamarin.Forms.ItemTappedEventArgs;
using SelectionMode = Syncfusion.ListView.XForms.SelectionMode;

namespace TimeOryx.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectDone : ContentPage
    {
        private List<DoList> selectedList { get; set; }
        private static SfListView listView;

        public SelectDone()
        {
            InitializeComponent();
             listView = new  SfListView
            {
                // Определяем источник данных
                ItemsSource = TodoListPage.ToDoLists,
                SelectionMode  = SelectionMode.Multiple,
                // Определяем формат отображения данных
                ItemTemplate = new DataTemplate(() =>
                {
                    
                    // привязка к свойству Title
                    Label titleLabel = new Label {FontSize = 18};
                    titleLabel.TextColor = Color.AliceBlue;
                    titleLabel.HorizontalTextAlignment = TextAlignment.Center;
                    titleLabel.SetBinding(Label.TextProperty, "Name");
                    Label timeLabel = new Label {FontSize = 18};
                    timeLabel.TextColor = Color.AliceBlue;
                    timeLabel.SetBinding(Label.TextProperty, "Time");
                    timeLabel.HorizontalTextAlignment = TextAlignment.Center;
                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = {titleLabel, timeLabel}
                        }
                    };
                })
            };
            
            listView.ItemTapped += ListViewOnItemTapped;
            StackLayout.Children.Insert(0,listView);
        }

        private void ListViewOnItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {

        }

        private void Save(object sender, EventArgs e)
        {
            selectedList = new List<DoList>(listView.SelectedItems.Cast<DoList>());
            foreach (var iDoList in selectedList)
            {
                CalendarPage.CalendarEvents.Remove(iDoList);
                CalendarInlineEvent calendarInlineEvent = new CalendarInlineEvent();
                calendarInlineEvent.Subject = iDoList.Name;
                DateTime time = DateTime.Parse(iDoList.Time);
                DateTime date = DateTime.Parse(iDoList.Date);
                date = date.AddHours(time.Hour);
                date = date.AddMinutes(time.Minute);
                date = date.AddSeconds(time.Second);
                calendarInlineEvent.StartTime = date;
                calendarInlineEvent.EndTime = date;
                CalendarPage.CalendarCollection.Remove(calendarInlineEvent);
            }

            Navigation.PopModalAsync();
            TodoListPage.Refresh();
        }

        private void Cancel(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}