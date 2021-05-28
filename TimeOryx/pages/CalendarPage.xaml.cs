using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Models;

namespace TimeOryx
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        private string _folderpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private string[] _tempStrings = new string[5];
        private DoList _tempDoList = new DoList();
        public static List<DoList> CalendarEvents { get; set; }
        public static CalendarEventCollection CalendarCollection { get; set; }
        public CalendarPage()
        {
            InitializeComponent();
            _tempDoList = new DoList();
            int i = 0;
            CalendarEvents = new List<DoList>();
            CalendarCollection = new CalendarEventCollection();
            Random rand = new Random(DateTime.Now.Hour+DateTime.Now.Minute+DateTime.Now.Second);
            CalendarInlineEvent calendarInlineEvent = new CalendarInlineEvent();
            if (CalendarEvents.Count == 0)
            {
                if (File.Exists(Path.Combine(_folderpath, "Todo.json")))
                {
                    var temp = File.OpenText(Path.Combine(_folderpath, "Todo.json"));
                    while (!temp.EndOfStream)
                    {
                        var str = temp.ReadLine();
                        var eventInfo = JsonConvert.DeserializeObject<DoList>(str);
                        CalendarEvents.Add(eventInfo);
                        //int r = rand.Next(256);
                        //int g = rand.Next(200);
                        //int b = rand.Next(150);
                        //calendarInlineEvent.Color = Color.FromRgb(r, g, b);
                        calendarInlineEvent.Subject = eventInfo.Name;
                        DateTime time = DateTime.Parse(eventInfo.Time);
                        DateTime date = DateTime.Parse(eventInfo.Date);
                       date = date.AddHours(time.Hour);
                       date = date.AddMinutes(time.Minute);
                       date = date.AddSeconds(time.Second);
                        calendarInlineEvent.StartTime = date;
                        calendarInlineEvent.EndTime = date;
                        //calendarInlineEvent.IsAllDay = true;
                        CalendarCollection.Add(calendarInlineEvent);
                        calendarInlineEvent = new CalendarInlineEvent();
                    }
                    temp.Close();
                }
            }

            SfCalendar.DataSource = CalendarCollection;
        }
    }
}