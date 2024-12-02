using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZEgorov
{
    internal class data
    {
        static public string conStr = $@"host=127.0.0.1;uid=root;pwd=root;database=weaponshop;";
        static public string usrName;
        static public string usrSurname;
        static public string usrPatr;
        static public string role;
        static public int userId;

        public static string path = AppDomain.CurrentDomain.BaseDirectory + @"Photo\";
    }
}
