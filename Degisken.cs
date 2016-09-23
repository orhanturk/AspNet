using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService
{
    public class Degisken
    {
        public static string SqlStr = "Server=" + Properties.Settings.Default.Server + ";Database=" + Properties.Settings.Default.Database + "; Uid=" + Properties.Settings.Default.UserName + ";Pwd=" + Properties.Settings.Default.Password + ";";
    }
}