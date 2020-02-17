using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SyncAppGUI
{
    struct settings
    {
        public static string syncType;
        public enum SyncTypes
        {
            TimeInterval,
            SetTimes
        }
        public static int interval=1;
        public static DateTime intervalDate;
        public static List<DateTime> dateTimes;
        public static readonly string resetSave= Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)+"\\SyncApp\\";
        public static string defaultSave = resetSave;
        public void Read()
        {

        }
    }
}
