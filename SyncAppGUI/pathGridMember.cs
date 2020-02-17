using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.IO;

namespace SyncAppGUI
{
    
    public class pathGridMember
    {
        
        string sourcePath;
        string targetPath;
        string sourceFolder;
        string targetFolder;
        bool autoSync;
        string sync_Type;

        public string Source => sourcePath;
        public string Target => targetPath;
        public string SourceDirectory => sourceFolder;
        public string TargetDirectory => targetFolder;
        public string SyncType { get { return sync_Type; } set { sync_Type = value; } }

        public enum syncTypes
        {
            Mirror,
            Constructive,
            Destructive
        }
        public bool AutoSync
        {
            get { return autoSync; }
            set { autoSync = value; }
        }
        public pathGridMember(string source, string target)
        {
            targetPath = target;
            sourcePath = source;

            if (source != "" && target != "" && source != null && target != null)
            {
                DirectoryInfo sourceInfo = new DirectoryInfo(sourcePath);
                DirectoryInfo targetInfo = new DirectoryInfo(targetPath);

                sourceFolder = sourceInfo.Name;
                targetFolder = targetInfo.Name;
            }
        }
    }
}
