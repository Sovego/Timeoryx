using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Models;

namespace TimeOryx.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendar : ContentView
    {
        public EventCollection Events { get; set; }
        private string _folderpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private string[] _tempStrings = new string[5];
        private DoList _tempDoList = new DoList();
        public static List<DoList> CalendarEvents { get; set; }
        public Calendar()
        {
            InitializeComponent();
            _tempDoList = new DoList();
            int i = 0;
            CalendarEvents = new List<DoList>();
            if (CalendarEvents.Count==0)
            {
                if (File.Exists(Path.Combine(_folderpath, "Todo.dat")))
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
                            CalendarEvents.Add(_tempDoList);
                            i = 0;
                            _tempDoList = new DoList();
                            continue;
                        }
                        _tempStrings[i] = temstr;
                        i++;
                    }
                    temp.Close();
                }
            }

            
        }
    }
}