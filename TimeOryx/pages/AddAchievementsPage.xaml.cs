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
    public partial class AddAchievementsPage : ContentPage
    {

        private string _folderpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private string[] _tempdolist = new string[6];

        public AddAchievementsPage()
        {
            InitializeComponent();
        }


        //Включение настройки даты окончания достижения
        private void CBox_OnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Box.IsChecked) { DatePickerEnd.IsVisible = true; }
            else { DatePickerEnd.IsVisible = false; }
        }


        //сохранить данные
        /*private void Save_OnClicked(object sender, EventArgs e)
        {
            StreamWriter teStreamWriter;
            _tempdolist[0] = Entry.Text;
            _tempdolist[1] = Editor.Text;
           // _tempdolist[2] = DatePicker.Date.ToString("d");
            //_tempdolist[3] = TimePicker.Time.ToString("c");
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
            DoList temp = new DoList();
            temp.Name = _tempdolist[0];
            temp.Description = _tempdolist[1];
            temp.Date = _tempdolist[2];
            temp.Time = _tempdolist[3];
            temp.DateEnd = _tempdolist[4];
            Achievements.Refresh(temp);
            Navigation.PopModalAsync();

        } */




        //Выход из создания достижения
        private void Cancel_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        /*  private void SwitchCell_OnOnChanged(object sender, ToggledEventArgs e)
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
          }*/
    }
}