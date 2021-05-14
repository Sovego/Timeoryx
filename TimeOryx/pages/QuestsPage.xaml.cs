using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            ListView listView = new ListView
            {
                HasUnevenRows = true,
                // Определяем источник данных
                ItemsSource = QuestsList,             
                // Определяем формат отображения данных
                ItemTemplate = new DataTemplate(() =>
                {
                    Label titleLabel = new Label { FontSize=18};
                    titleLabel.TextColor = Color.Black;
                    titleLabel.SetBinding(Label.TextProperty, "Title");
                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
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

        protected override void OnAppearing()
        {
            string[] _tempStrings = new string[5];
            Quests tempQuests = new Quests();
            base.OnAppearing();
            int i = 0;
            if (QuestsList.Count==0)
            {
                if (File.Exists(Path.Combine(PathFile.Folderpath, "Quests.dat")))
                {
                    var temp = File.OpenText(Path.Combine(PathFile.Folderpath, "Quests.dat"));
                    while (!temp.EndOfStream)
                    {
                        var temstr = temp.ReadLine();
                        if (temstr == "///")
                        {
                            tempQuests.Title = _tempStrings[0];
                            tempQuests.Task = _tempStrings[1];
                            tempQuests.DateStart = _tempStrings[2];
                            tempQuests.DateEnd = _tempStrings[3];
                            QuestsList.Add(tempQuests);
                            i = 0;
                            tempQuests = new Quests();
                            continue;
                        }
                        _tempStrings[i] = temstr;
                        i++;
                    }
                    temp.Close();
                }
            }
        }

        public static void Refresh(Quests teQuests)
        {
            QuestsList.Add(teQuests);
        }
        private void ListViewOnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Quests teQuests = (Quests)e.Item;
            string message = "";
            message += "Описание: " + teQuests.Task + "\n";
            message += "Срок: " + teQuests.DateStart + " - " + teQuests.DateEnd + "\n";
            DisplayAlert(teQuests.Title, message, "Ok");
        }


        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddQuestPage());
        }
    }
}