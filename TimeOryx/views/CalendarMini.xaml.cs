using System;
using System.Collections.Generic;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;
using SelectionMode = Syncfusion.ListView.XForms.SelectionMode;

namespace TimeOryx
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarMini : ContentView
    {
        public List<DateTime> DayList=new List<DateTime>();

        public CalendarMini()
        {

            InitializeComponent();
           
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                {
                    DayList.Add(DateTime.Now);
                    DayList.Add(DateTime.Now.AddDays(1));
                    DayList.Add(DateTime.Now.AddDays(2));
                    DayList.Add(DateTime.Now.AddDays(3));
                    DayList.Add(DateTime.Now.AddDays(4));
                    DayList.Add(DateTime.Now.AddDays(5));
                    DayList.Add(DateTime.Now.AddDays(6));
                    break;
                }
                case DayOfWeek.Tuesday:
                {
                    DayList.Add(DateTime.Now.AddDays(-1));
                    DayList.Add(DateTime.Now);
                    DayList.Add(DateTime.Now.AddDays(1));
                    DayList.Add(DateTime.Now.AddDays(2));
                    DayList.Add(DateTime.Now.AddDays(3));
                    DayList.Add(DateTime.Now.AddDays(4));
                    DayList.Add(DateTime.Now.AddDays(5));
                    break;
                }
                case DayOfWeek.Wednesday:
                {
                    DayList.Add(DateTime.Now.AddDays(-2));
                    DayList.Add(DateTime.Now.AddDays(-1));
                    DayList.Add(DateTime.Now);
                    DayList.Add(DateTime.Now.AddDays(1));
                    DayList.Add(DateTime.Now.AddDays(2));
                    DayList.Add(DateTime.Now.AddDays(3));
                    DayList.Add(DateTime.Now.AddDays(4));
                    break;
                }
                case DayOfWeek.Thursday:
                {
                    DayList.Add(DateTime.Now.AddDays(-3));
                    DayList.Add(DateTime.Now.AddDays(-2));
                    DayList.Add(DateTime.Now.AddDays(-1));
                    DayList.Add(DateTime.Now);
                    DayList.Add(DateTime.Now.AddDays(1));
                    DayList.Add(DateTime.Now.AddDays(2));
                    DayList.Add(DateTime.Now.AddDays(3));
                    break;
                }
                case DayOfWeek.Friday:
                {
                    DayList.Add(DateTime.Now.AddDays(-4));
                    DayList.Add(DateTime.Now.AddDays(-3));
                    DayList.Add(DateTime.Now.AddDays(-2));
                    DayList.Add(DateTime.Now.AddDays(-1));
                    DayList.Add(DateTime.Now);
                    DayList.Add(DateTime.Now.AddDays(1));
                    DayList.Add(DateTime.Now.AddDays(2));
                    break;
                }
                case DayOfWeek.Saturday:
                {

                    DayList.Add(DateTime.Now.AddDays(-5));
                    DayList.Add(DateTime.Now.AddDays(-4));
                    DayList.Add(DateTime.Now.AddDays(-3));
                    DayList.Add(DateTime.Now.AddDays(-2));
                    DayList.Add(DateTime.Now.AddDays(-1));
                    DayList.Add(DateTime.Now);
                    DayList.Add(DateTime.Now.AddDays(1));
                    break;

                }
                case DayOfWeek.Sunday:
                {
                    DayList.Add(DateTime.Now.AddDays(-6));
                    DayList.Add(DateTime.Now.AddDays(-5));
                    DayList.Add(DateTime.Now.AddDays(-4));
                    DayList.Add(DateTime.Now.AddDays(-3));
                    DayList.Add(DateTime.Now.AddDays(-2));
                    DayList.Add(DateTime.Now.AddDays(-1));
                    DayList.Add(DateTime.Now);
                    break;
                }

            }
            SfListView listView = new SfListView
            {
                SelectionMode = SelectionMode.Single,
                SelectionGesture = TouchGesture.Tap,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = Orientation.Horizontal,
                HeightRequest = 40,
                ItemSize = Application.Current.MainPage.Width/10,
                IsScrollingEnabled = false,
                IsScrollBarVisible = false,
                //AutoFitMode = AutoFitMode.Height,
                //VerticalOptions = LayoutOptions.StartAndExpand,
                ItemSpacing = new Thickness(9,0),
                ItemsSource = DayList,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label dayLabel = new Label(){FontSize = 24, TextColor = Color.AliceBlue, HorizontalTextAlignment = TextAlignment.Center,VerticalTextAlignment = TextAlignment.Center};
                    dayLabel.SetBinding(Label.TextProperty, "Day");
                    return new ViewCell()
                    {
                        View = new StackLayout()
                        {
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            //Spacing = 8,
                            Children = {dayLabel}
                        }
                    };
                })
            };
            listView.ItemTapped += ListViewOnItemTapped;
            Calendar.Children.Add(listView);
        }

        private void ListViewOnItemTapped(object sender, ItemTappedEventArgs e)
        {
            DateTime tempDateTime = Convert.ToDateTime(e.ItemData);
            TodoListPage.temps = tempDateTime.ToString("d");
            TodoListPage.Refresh();
        }
    }
}