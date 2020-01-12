using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Syncs
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string path = "C:\\Users\\Brigi\\Desktop\\Erik - CS IA\\";
            string target = "C:\\Users\\Brigi\\Desktop\\Try\\";
            string c = "";
            string path2= "C:\\Users\\Brigi\\Desktop\\sourcetry2\\";
            string target2 = "C:\\Users\\Brigi\\Desktop\\targettry2\\";

            List<FileWatcher> fileWatchers = new List<FileWatcher>();
            fileWatchers.Add(new FileWatcher());
            fileWatchers.Add(new FileWatcher());
            fileWatchers[0].CreateWatcher(path, target, c);
            fileWatchers[1].CreateWatcher(path2, target2, c);
            fileWatchers.ForEach(x => x.TurnOnWatcher());
            */
            //try pther threading
            string p1 = "C:\\Users\\Brigi\\Desktop\\folder1\\";
            string p2 = "C:\\Users\\Brigi\\Desktop\\folder2\\";
            FileWatcher fw = new FileWatcher();
            fw.CreateWatcher(p1, p2, "Mirror");
            fw.TurnOnWatcher();
            Console.ReadLine();

        }
        


    }
    
}
