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
            //paths should be set
            string p1 = "C:\\examplepath";
            string p2 = "C:\\exampletarget";
            FileWatcher fw = new FileWatcher();
            fw.CreateWatcher(p1, p2, "Mirror");
            fw.TurnOnWatcher();
            Console.ReadLine();

        }
        


    }
    
}
