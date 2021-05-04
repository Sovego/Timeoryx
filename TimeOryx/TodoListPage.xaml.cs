using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeOryx
{
    public class DoList
    {
        public string Name { get; set; }
        public string Description{ get; set; }
        public string Time{ get; set; }
        public string DateEnd{ get; set; }
        public string Date{ get; set; }
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoListPage : ContentPage
    {
        public static ObservableCollection<DoList> ToDoLists { get; set; }
        //public ObservableCollection<DoList> ToDoLists => toDoLists;
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
                    ImageButton buttondel = new ImageButton {Source ="/img/del.png"};
                    
                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            BackgroundColor = Color.Black,
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children = { titleLabel,buttondel}
                        }
                    };
                })
            };
            StackLayout.Children.Insert(0,listView);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           
            _tempDoList = new DoList();
            int i = 0;
            if (ToDoLists.Count==0)
            {
                if (File.Exists(Path.Combine(_folderpath, "Todo.dat")))
                {
                    var temp = File.OpenText(Path.Combine(_folderpath, "Todo.dat"));
                    while (!temp.EndOfStream)
                    {
                        var temstr = temp.ReadLine();
                        if (temstr == "///////////")
                        {
                            _tempDoList.Name = _tempStrings[0];
                            _tempDoList.Description = _tempStrings[1];
                            _tempDoList.Date = _tempStrings[2];
                            _tempDoList.Time = _tempStrings[3];
                            _tempDoList.DateEnd = _tempStrings[4];
                            ToDoLists.Add(_tempDoList);
                            i = 0;
                            _tempDoList = new DoList();
                            continue;
                        }
                        _tempStrings[i] = temstr;
                        i++;
                    }
                    temp.Close();
                }
            }
            UpdateChildrenLayout();
        }

        public static void AddTask(DoList tempdolist)
        {
            ToDoLists.Add(tempdolist);
        }
        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddEventPage());
        }
    }
}