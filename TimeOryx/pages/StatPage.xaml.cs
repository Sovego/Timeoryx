using System;
using System.IO;
using Newtonsoft.Json;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeOryx
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatPage : ContentPage
    {
        public static Stats StatInfo;
        public StatPage()
        {
            InitializeComponent();
        }

        public static void ReadStat()
        {
            StatInfo = new Stats();
            if (File.Exists(Path.Combine(PathFile.Folderpath, "Stat.json")))
            {
                var temp = File.OpenText(Path.Combine(PathFile.Folderpath, "Stat.json"));
                var str = temp.ReadLine();
                StatInfo = JsonConvert.DeserializeObject<Stats>(str);
                temp.Close();
            }
            else
            {
                StatInfo.AllDo = 0;
                StatInfo.AllQuest = 0;
                StatInfo.NumbDoDone = 0;
                StatInfo.NumbQuestDone = 0;
            }
        }

        public static void Refresh()
        {
            using (StreamWriter fs = new StreamWriter(Path.Combine(PathFile.Folderpath,"Stat.json"),false))
            {
                var jsonstr = JsonConvert.SerializeObject(StatInfo);
                fs.WriteLine(jsonstr);
               
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AllDo.Text = $"Всего дел: {StatInfo.AllDo}";
            AllQuest.Text = $"Всего целей: {StatInfo.AllQuest}";
            DoneDo.Text = $"Выполнено дел: {StatInfo.NumbDoDone}";
            DoneQuest.Text = $"Выполнено целей: {StatInfo.NumbQuestDone}";
        }
    }
}