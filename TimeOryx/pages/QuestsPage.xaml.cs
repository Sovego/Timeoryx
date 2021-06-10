using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Xamarin.Forms.ItemTappedEventArgs;

namespace TimeOryx
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestsPage : ContentPage
    {
        public static ObservableCollection<Quests> QuestsList { get; set; }
        public QuestsPage()
        {
            InitializeComponent();
            QuestsList = new ObservableCollection<Quests>();
            SfListView listView = new SfListView
            {
                ItemSpacing = new Thickness(0,2),
                // Определяем источник данных
                ItemsSource = QuestsList,             
                // Определяем формат отображения данных
                ItemTemplate = new DataTemplate(() =>
                {
                    Label titleLabel = new Label { FontSize=24};
                    titleLabel.TextColor = Color.AliceBlue;
                    titleLabel.SetBinding(Label.TextProperty, "Title");
                    titleLabel.HorizontalTextAlignment = TextAlignment.Center;
                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            BackgroundColor = Color.FromHex("8d8d8d"),
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = { titleLabel}
                        }
                    };
                })
            };
            listView.ItemTapped += ListViewOnItemTapped;
            StackLayout.Children.Add(listView);
        }

        private void ListViewOnItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            Quests teQuests = (Quests)e.ItemData;
            string message = "";
            if (teQuests.Task == null)
            {
                message += "Описание: Отсутствует \n" ;
            }
            else
            {
                message += "Описание: " + teQuests.Task + "\n";
            }
            if (teQuests.DateStart == null)
            {
                message += "Срок: Бессрочно \n";
            }
            else
            {
                message += "Срок: " + teQuests.DateStart + " - " + teQuests.DateEnd + "\n";
            }
            
            DisplayAlert(teQuests.Title, message, "Ok");
        }

        protected override void OnAppearing()
        {
            string[] _tempStrings = new string[5];
            Quests tempQuests = new Quests();
            base.OnAppearing();
            int i = 0;
            if (QuestsList.Count==0)
            {
                if (File.Exists(Path.Combine(PathFile.Folderpath, "Quests.json")))
                {
                    var temp = File.OpenText(Path.Combine(PathFile.Folderpath, "Quests.json"));
                    while (!temp.EndOfStream)
                    {
                        var str = temp.ReadLine();
                        var questInfo = JsonConvert.DeserializeObject<Quests>(str);
                        QuestsList.Add(questInfo);
                    }
                    temp.Close();
                }
            }
        }
        


        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddQuestPage());
        }

        private void SelectDoneQuest(object sender, EventArgs e)
        {
            if (QuestsList.Count == 0)
            {
                DisplayAlert("Ошибка", "Целей нет", "Ok");
            }
            else
            {
                Navigation.PushModalAsync(new SelectDoneQuest());
            }
           
        }

        public static void Refresh()
        {
            using (StreamWriter fs = new StreamWriter(Path.Combine(PathFile.Folderpath, "Quests.json"), false))
            {
                if (QuestsList != null)
                    foreach (var iQuests in QuestsList)
                    {
                        var jsonstr = JsonConvert.SerializeObject(iQuests);
                        fs.WriteLine(jsonstr);
                    }
            }
        }
    }
}