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
        
       // private List<DoList> eventDoLists = new List<DoList>();
       private string[] _tempdolist =new string[4];

       public AddEventPage()
        {
            InitializeComponent();
        }
       private void Save_OnClicked(object sender, EventArgs e)
        {
            if (Entry.Text == null)
            {
                Navigation.PopModalAsync();
            }
            else
            {
                StreamWriter teStreamWriter;
                _tempdolist[0] = Entry.Text;
                _tempdolist[1] = Editor.Text;
                _tempdolist[2] = DatePicker.Date.ToString("d");
                //eventDoLists.Add(tempDoList);
                _tempdolist[3] = "///////////";
                teStreamWriter = File.AppendText(Path.Combine(PathFile.Folderpath, "Todo.dat"));
                foreach (var i in _tempdolist)
                {
                    teStreamWriter.WriteLine(i);
                }
                teStreamWriter.Close();
                DoList temp= new DoList();
                temp.Name = _tempdolist[0];
                temp.Description = _tempdolist[1];
                temp.Date = _tempdolist[2];
                TodoListPage.AddTask(temp);
                Navigation.PopModalAsync();
            }
            
        }
       
        private void DatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            _tempdolist[2] = e.NewDate.ToString("d");
        }

        private void Cancel_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}