using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using TimeOryx.views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Xamarin.Forms.ItemTappedEventArgs;
using SelectionMode = Syncfusion.ListView.XForms.SelectionMode;

namespace TimeOryx.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectDone : ContentPage
    {
        private ObservableCollection<DoList> selectedList { get; set; }
        private SfListView listView;
        private DoList _tempDoList;
        private List<DoList> teDoLists;
        private string[] _tempStrings = new string[4];

        public SelectDone()
        {
            InitializeComponent();
            listView = new SfListView
            {
                // Определяем источник данных
                ItemsSource = TodoListPage.ToDoLists,
                SelectionMode = SelectionMode.Multiple,
                ItemSpacing = new Thickness(0,5),
                // Определяем формат отображения данных
                ItemTemplate = new DataTemplate(() =>
                {

                    // привязка к свойству Title
                    Label titleLabel = new Label {FontSize = 20, TextColor = Color.AliceBlue};

                    titleLabel.SetBinding(Label.TextProperty, "Name");
                    Label timeLabel = new Label {FontSize = 20, TextColor = Color.AliceBlue};

                    timeLabel.SetBinding(Label.TextProperty, "Time");
                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            //BackgroundColor = Color.FromHex("#8e8e8e"),
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = {titleLabel, timeLabel}
                        }
                    };
                })
            };


            StackLayout.Children.Insert(0, listView);
        }


        private void Save(object sender, EventArgs e)
        {
            teDoLists = new List<DoList>();
            _tempDoList = new DoList();
            DoList tempDo = new DoList();
            int i = 0;
            StreamWriter temStreamWriter;
            
            if (File.Exists(Path.Combine(PathFile.Folderpath, "Todo.dat")))
            {
                var temStreamReader = File.OpenText(Path.Combine(PathFile.Folderpath, "Todo.dat"));
                while (!temStreamReader.EndOfStream)
                {
                    var temstr = temStreamReader.ReadLine();
                    if (temstr == "///////////")
                    {
                        _tempDoList.Name = _tempStrings[0];
                        _tempDoList.Description = _tempStrings[1];
                        _tempDoList.Date = _tempStrings[2];
                        teDoLists.Add(_tempDoList);
                        i = 0;
                        _tempDoList = new DoList();
                        continue;
                    }
                    _tempStrings[i] = temstr;
                    i++;
                }
                temStreamReader.Close();
            }
            temStreamWriter = File.CreateText(Path.Combine(PathFile.Folderpath, "Todo.dat"));
            selectedList = listView.SelectedItems.Cast<DoList>().ToObservableCollection();
            foreach (var j in selectedList)
            {
                tempDo=teDoLists.Find(list => list.Date == j.Date && list.Name == j.Name);
                teDoLists.Remove(tempDo);
            }

            foreach (var j in teDoLists)
            {
                _tempStrings[0] = j.Name;
                _tempStrings[1] = j.Description;
                _tempStrings[2] = j.Date;
                _tempStrings[3] = "///////////";
                foreach (var k in _tempStrings)
                {
                    temStreamWriter.WriteLine(k);
                }
            }
            temStreamWriter.Close();
            TodoListPage.RemoveTask();
            Navigation.PopModalAsync();
        }
    }
}

