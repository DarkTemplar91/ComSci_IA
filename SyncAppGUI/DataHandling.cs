using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SyncAppGUI
{
    
    class DataHandling
    {
        
        internal string sourceDirectory;
        string directoryName;
        double size;

        internal string targetDirectory;
        string targetName;
        double targetSize;

       
       public DataHandling(string path, string target)
        {
            sourceDirectory = path;
            targetDirectory = target;
            

            DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirectory);
            directoryName = directoryInfo.Name;
            
            directoryInfo = new DirectoryInfo(targetDirectory);
            targetName = directoryInfo.Name;
        } 

        
    }
}
