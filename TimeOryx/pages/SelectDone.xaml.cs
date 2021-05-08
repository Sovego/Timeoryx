using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.ListView.XForms;
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

        public SelectDone()
        {
            InitializeComponent();
            SfListView listView = new  SfListView
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
                    titleLabel.SetBinding(Label.TextProperty, "Name");
                    Label timeLabel = new Label {FontSize = 18};
                    timeLabel.TextColor = Color.AliceBlue;
                    timeLabel.SetBinding(Label.TextProperty, "Time");
                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            BackgroundColor = Color.Black,
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
    }
}