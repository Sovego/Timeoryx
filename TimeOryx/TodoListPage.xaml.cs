using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeOryx
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoListPage : ContentPage
    {
        public ObservableCollection<DoList> ToDoLists { get; set; }
        //public ObservableCollection<DoList> ToDoLists => toDoLists;
        private DoList _tempDoList = new DoList();
        private string _folderpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private string[] _tempStrings = new string[5];
        public TodoListPage()
        {
            InitializeComponent();
            ToDoLists = new ObservableCollection<DoList>();
            int i=0;
            if (File.Exists(Path.Combine(_folderpath,"Todo.dat")))
            {
                var temp = File.OpenText(Path.Combine(_folderpath, "Todo.dat"));
                while (!temp.EndOfStream)
                {
                    var temstr = temp.ReadLine();
                    if (temstr == "///////////")
                    {
                        _tempDoList.Name = _tempStrings[0];
                        _tempDoList.Description = _tempStrings[1];
                        _tempDoList.Date = _tempStrings[2];
                        _tempDoList.Time = _tempStrings[3];
                        _tempDoList.DateEnd = _tempStrings[4];
                        ToDoLists.Add(_tempDoList);
                        i = 0;
                        continue;
                    }
                    _tempStrings[i] = temstr;
                    i++;
                }
                temp.Close();
            }
            this.BindingContext = this;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    ToDoLists = new ObservableCollection<DoList>();
        //    int i=0;
        //    if (File.Exists(Path.Combine(_folderpath,"Todo.dat")))
        //    {
        //        var temp = File.OpenText(Path.Combine(_folderpath, "Todo.dat"));
        //        while (!temp.EndOfStream)
        //        {
        //            var temstr = temp.ReadLine();
        //            if (temstr == "///////////")
        //            {
        //                _tempDoList.Name = _tempStrings[0];
        //                _tempDoList.Description = _tempStrings[1];
        //                _tempDoList.Date = _tempStrings[2];
        //                _tempDoList.Time = _tempStrings[3];
        //                _tempDoList.DateEnd = _tempStrings[4];
        //                ToDoLists.Add(_tempDoList);
        //                continue;
        //            }

        //            _tempStrings[i] = temstr;
        //            i++;
        //            if (i == 5)
        //            {
        //                i = 0;
        //            }
        //        }
        //        temp.Close();
        //    }
        //    this.BindingContext = this;
        //    UpdateChildrenLayout();
        //}

        

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddEventPage());
        }
    }
}