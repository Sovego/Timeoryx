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
       
        private static string[] _tempStrings = new string[4];
        private static DoList _tempDoList = new DoList();
        public static List<DoList> CalendarEvents { get; set; }
        public Calendar()
        {
            InitializeComponent();
            ReadCalendar();

            
        }

        public static void ReadCalendar()
        {
            _tempDoList = new DoList();
            int i = 0;
            CalendarEvents = new List<DoList>();
            if (CalendarEvents.Count==0)
            {
                if (File.Exists(Path.Combine(PathFile.Folderpath, "Todo.dat")))
                {
                    var temp = File.OpenText(Path.Combine(PathFile.Folderpath, "Todo.dat"));
                    while (!temp.EndOfStream)
                    {
                        var temstr = temp.ReadLine();
                        if (temstr == "///////////")
                        {
                            _tempDoList.Name = _tempStrings[0];
                            _tempDoList.Description = _tempStrings[1];
                            _tempDoList.Date = _tempStrings[2];
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