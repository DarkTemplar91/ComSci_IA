using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SyncAppGUI
{
    //Kind of redundant with PathGridMember class
    
    struct DataHandling
    {
        
        internal string sourceDirectory;
        string directoryName;
        

        internal string targetDirectory;
        string targetName;
        
       
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
