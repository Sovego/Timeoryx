using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeOryx
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddQuestPage : ContentPage
    {
        public AddQuestPage()
        {
            InitializeComponent();
        }

        private void SwitchCell_OnOnChanged(object sender, ToggledEventArgs e)
        {
            if (StackLayout.IsVisible)
            {
                StackLayout.IsVisible = false;
            }
            else
            {
                StackLayout.IsVisible = true;
            }
            
        }

        private void Save(object sender, EventArgs e)
        {
            StreamWriter teStreamWriter;
            Quests teQuests = new Quests();
            string[] temStrings = new string[5];
            temStrings[0] = NameCell.Text;
            temStrings[1] = DescriptionCell.Text;
            temStrings[2] = DatePickerStart.Date.ToString("d");
            temStrings[3] = DatePickerEnd.Date.ToString("d");
            temStrings[4] = "///";
            teStreamWriter = File.AppendText(Path.Combine(PathFile.Folderpath, "Quests.dat"));
            foreach (var i in temStrings)
            {
                teStreamWriter.WriteLine(i);
            }
            teStreamWriter.Close();
            teQuests.Title = NameCell.Text;
            teQuests.Task = DescriptionCell.Text;
            teQuests.DateStart = DatePickerStart.Date.ToString("d");
            teQuests.DateEnd = DatePickerEnd.Date.ToString("d");
            QuestsPage.Refresh(teQuests);
            Navigation.PopModalAsync();
        }
    }
}