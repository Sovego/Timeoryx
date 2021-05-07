using System;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            StreamWriter teStreamWriter;
            _tempdolist[0] = Entry.Text;
            _tempdolist[1] = Editor.Text;
            _tempdolist[2] = DatePicker.Date.ToString("d");
            _tempdolist[3] = TimePicker.Time.ToString("c");
            if (Box.IsChecked)
            {
                _tempdolist[4] = Convert.ToString(DatePickerEnd.Date);
            }
            //eventDoLists.Add(tempDoList);
            _tempdolist[5] = "///////////";
            teStreamWriter = File.AppendText(Path.Combine(_folderpath, "Todo.dat"));
            foreach (var i in _tempdolist)
            {
                teStreamWriter.WriteLine(i);
            }
            teStreamWriter.Close();
            DoList temp= new DoList();
            temp.Name = _tempdolist[0];
            temp.Description = _tempdolist[1];
            temp.Date = _tempdolist[2];
            temp.Time = _tempdolist[3];
            temp.DateEnd = _tempdolist[4];
            TodoListPage.AddTask(temp);
            Navigation.PopModalAsync();
        }

        private void CBox_OnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Box.IsChecked)
            {
                DatePickerEnd.IsVisible = true;
            }
            else
            {
                DatePickerEnd.IsVisible = false;
            }
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