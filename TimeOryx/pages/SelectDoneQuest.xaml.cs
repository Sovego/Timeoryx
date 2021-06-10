using System;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeOryx
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectDoneQuest : ContentPage
    {
        private List<Quests> selectedList { get; set; }
        private static SfListView listView;
        public SelectDoneQuest()
        {
            InitializeComponent();
            listView = new  SfListView
            {
                // Определяем источник данных
                ItemsSource = QuestsPage.QuestsList,
                SelectionMode = Syncfusion.ListView.XForms.SelectionMode.Multiple,
                // Определяем формат отображения данных
                ItemTemplate = new DataTemplate(() =>
                {
                    
                    // привязка к свойству Title
                    Label titleLabel = new Label {FontSize = 18};
                    titleLabel.TextColor = Color.Black;
                    titleLabel.HorizontalTextAlignment = TextAlignment.Center;
                    titleLabel.SetBinding(Label.TextProperty, "Title");
                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = {titleLabel}
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
            selectedList = new List<Quests>(listView.SelectedItems.Cast<Quests>());
            foreach (var iDoList in selectedList)
            {
                QuestsPage.QuestsList.Remove(iDoList);
                
            }

            Navigation.PopModalAsync();
            QuestsPage.Refresh();
            StatPage.StatInfo.NumbQuestDone += selectedList.Count;
            StatPage.Refresh();
        }

        private void Cancel(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        }
    }

