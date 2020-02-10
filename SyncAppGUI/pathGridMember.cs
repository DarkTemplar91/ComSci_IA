using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncAppGUI
{
    class pathGridMember
    {
        string sourcePath;
        string targetPath;
        string sourceFolder;
        string targetFolder;
        
        public string Source => sourcePath;
        public string Target => targetPath;
        
        public string SFolder => sourceFolder;
        
        public string TFolder => targetFolder;

        bool autoSync;
        public enum syncTypes
        {
            Mirror,
            Constructive,
            Destructive
        }
        string sync_Type;
        public string SyncType { get { return sync_Type; } set { sync_Type = value; } }

        public bool AutoSync
        {
            get { return autoSync; }
            set { autoSync = value; }
        }
        public pathGridMember(string source, string target)
        {
            targetPath = target;
            targetFolder = Trim(target);
            sourcePath = source;
            sourceFolder = Trim(source);
        }
        private static string Trim(string source)
        {
            string[] temp = source.Split('\\');
            return temp[temp.Length - 1];
        }
    }
}
