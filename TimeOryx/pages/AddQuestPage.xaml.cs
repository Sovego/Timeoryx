using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Syncfusion.XForms.Buttons;
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

       

        private void Save(object sender, EventArgs e)
        {

            Quests teQuests = new Quests();
            if (EntryName.Text == null)
            {
                Navigation.PopModalAsync();
                return;
            }
            teQuests.Title = EntryName.Text;
            teQuests.Task = EntryDescription.Text;
            if (InputLayoutStart.IsVisible)
            {
                teQuests.DateStart = DatePickerStart.Date.ToString("d");
                teQuests.DateEnd = DatePickerEnd.Date.ToString("d");
            }
            using (StreamWriter fs = new StreamWriter(Path.Combine(PathFile.Folderpath, "Quests1.json"), true))
            {
                var jsonstr = JsonConvert.SerializeObject(teQuests);
                fs.WriteLine(jsonstr);
            }
            QuestsPage.QuestsList.Add(teQuests);
            Navigation.PopModalAsync();
        }

        private void Cancel(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void DatePickerOnOff(object sender, SwitchStateChangedEventArgs e)
        {
            if (!InputLayoutStart.IsVisible)
            {
                InputLayoutStart.IsVisible = true;
                InputLayoutEnd.IsVisible = true;
            }
            else
            {
                InputLayoutEnd.IsVisible = false;
                InputLayoutStart.IsVisible = false;
            }
        }
    }
}