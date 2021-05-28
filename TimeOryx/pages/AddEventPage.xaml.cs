using System;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Syncfusion.SfCalendar.XForms;


namespace TimeOryx
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEventPage : ContentPage
    {
        private string _folderpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
       // private List<DoList> eventDoLists = new List<DoList>();
       private string[] _tempdolist =new string[6];

       public AddEventPage()
        {
            InitializeComponent();
        }
       private void Save_OnClicked(object sender, EventArgs e)
        {
            _tempdolist[2] = DatePicker.Date.ToString("d");
            _tempdolist[3] = TimePicker.Time.ToString("c");
            DoList temp = new DoList();
            temp.Name = EntryName.Text;
            temp.Description = EntryDescription.Text;
            temp.Date = _tempdolist[2];
            temp.Time = _tempdolist[3];
            using (StreamWriter fs = new StreamWriter(Path.Combine(_folderpath,"Todo.json"),true))
            {
               var jsonstr = JsonConvert.SerializeObject(temp);
                fs.WriteLine(jsonstr);
               
            }
            CalendarInlineEvent calendarInlineEvent = new CalendarInlineEvent();
            calendarInlineEvent.Subject = temp.Name;
            DateTime time = DateTime.Parse(temp.Time);
            DateTime date = DateTime.Parse(temp.Date);
            date = date.AddHours(time.Hour);
            date = date.AddMinutes(time.Minute);
            date = date.AddSeconds(time.Second);
            calendarInlineEvent.StartTime = date;
            calendarInlineEvent.EndTime = date;
            CalendarPage.CalendarCollection.Add(calendarInlineEvent);
            CalendarPage.CalendarEvents.Add(temp);
            CalendarPage.CalendarEvents.Sort(new TimeComparer());
            TodoListPage.Refresh();
            Navigation.PopModalAsync();

        }

       
        private void Cancel_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void TimePicker_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
           
        }

        private void DatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            
        }
    }
}