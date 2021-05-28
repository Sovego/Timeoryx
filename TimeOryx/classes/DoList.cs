using System;
using System.Collections.Generic;
using System.Text;

namespace TimeOryx
{
    public class DoList
    {
        public string Name { get; set; }
        public string Description{ get; set; }
        public string Time{ get; set; }
        public string Date{ get; set; }
       
    }
   public class TimeComparer : IComparer<DoList>
    {
        public int Compare(DoList p1, DoList p2)
        {
            if (Convert.ToDateTime(p1.Time).Hour > Convert.ToDateTime(p2.Time).Hour)
                return 1;
            else if (Convert.ToDateTime(p1.Time).Hour < Convert.ToDateTime(p2.Time).Hour)
                return -1;
            else
                return 0;
        }
    }
}
