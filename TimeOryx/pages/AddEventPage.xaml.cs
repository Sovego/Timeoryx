using System;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;


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
            //_tempdolist[5] = "///////////";
            //teStreamWriter = File.AppendText(Path.Combine(_folderpath, "Todo.dat"));
            //foreach (var i in _tempdolist)
            //{
            //    teStreamWriter.WriteLine(i);
            //}
            //teStreamWriter.Close();

            TodoListPage.AddTask(temp);
            Navigation.PopModalAsync();

        }

       
        private void Cancel_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void DatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            _tempdolist[2] = e.NewDate.ToString("d");
        }

        private void TimePicker_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _tempdolist[3] = TimePicker.Time.ToString("c");
        }
    }
}