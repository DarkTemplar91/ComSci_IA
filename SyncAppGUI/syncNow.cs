using System;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace SyncAppGUI
{
    class syncNow
    {
        public static void MirrorList(BindingList<pathGridMember> list)
        {
            BindingList<pathGridMember> temp = list;
            for (int n = 0; n < temp.Count; n++)
            {
                if (temp[n].SyncType != null || temp[n].SyncType != "")
                {
                    string type = temp[n].SyncType;
                    string source = temp[n].Source;
                    string target = temp[n].Target;
                    if (type == pathGridMember.syncTypes.Mirror.ToString())
                    {
                        MirrorBoth(source, target);
                    }
                    else if (type == pathGridMember.syncTypes.Constructive.ToString() ||
                        type == pathGridMember.syncTypes.Destructive.ToString())
                    {

                        MirrorDir(source, target);
                    }
                }
            }
        }
        static void MirrorDir(string d1Path, string d2Path)
        {


            while (true)
            {
                try
                {
                    foreach (string dirPath in Directory.GetDirectories(d1Path, "*", SearchOption.AllDirectories))
                    {
                        if (!Directory.Exists(dirPath.Replace(d1Path, d2Path)))
                        {
                            Directory.CreateDirectory(dirPath.Replace(d1Path, d2Path));
                        }

                    }
                    foreach (string filePath in Directory.GetFiles(d1Path, "*", SearchOption.AllDirectories))
                    {
                        //checks if identical files exist in the two directories and deletes the second instance
                        //Directory one (left side) name should be carried on
                        if (File.Exists(filePath.Replace(d1Path, d2Path)))
                        {
                            string newPath = filePath.Replace(d1Path, d2Path);
                            FileInfo fi1 = new FileInfo(filePath);
                            FileInfo fi2 = new FileInfo(newPath);
                            if (FileEquals(filePath, newPath)) ;
                            else if (FileEquals(filePath, newPath) && File.GetLastWriteTime(filePath) > File.GetLastWriteTime(newPath))
                            {
                                File.Delete(newPath);
                            }
                            else if (FileEquals(filePath, newPath) && File.GetLastWriteTime(filePath) < File.GetLastWriteTime(newPath)) ;
                            else
                            {
                                throw new Exception("Two different files with identical names exist!");
                            }

                        }
                        //If file does not exist there, it should be copied to the second directory
                        if (!File.Exists(filePath.Replace(d1Path, d2Path)))
                        {

                            File.Copy(filePath, filePath.Replace(d1Path, d2Path), true);


                        }
                    }
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(3000);
                }
            }





        }
        public static void MirrorBoth(string d1path, string d2path)
        {

            MirrorDir(d1path, d2path);
            MirrorDir(d2path, d1path);

        }
        public static void Sync(BindingList<pathGridMember> list, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //Backgroundworker calls this method
            BindingList<pathGridMember> temp = list;
            //iterate through the list
            for (int n = 0; n < temp.Count; n++)
            {
                if (temp[n].SyncType != null || temp[n].SyncType != "")
                {
                    string type = temp[n].SyncType;
                    string source = temp[n].Source;
                    string target = temp[n].Target;
                    //Calls method depending on synchronization type
                    if (type == pathGridMember.syncTypes.Mirror.ToString())
                    {
                        MirrorBoth(source, target);
                    }
                    else if (type == pathGridMember.syncTypes.Constructive.ToString()
                        || type == pathGridMember.syncTypes.Destructive.ToString())
                    {

                        MirrorDir(source, target);
                    }
                }
            }
        }

        public static bool FileEquals(string p1, string p2)
        {

            try
            {
                byte[] file1 = File.ReadAllBytes(p1);
                byte[] file2 = File.ReadAllBytes(p2);

                if (file1.Length == file2.Length)
                {
                    for (int n = 1; n < file1.Length; n++)
                    {
                        if (file1[n] != file2[n])
                        {
                            return false;
                        }

                    }
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool DirEqual(string p1, string p2)
        {
            foreach (string path in Directory.GetDirectories(p1, "*", SearchOption.AllDirectories))
            {
                if (!Directory.Exists(path.Replace(p1, p2)))
                {
                    return false;
                }
            }
            foreach (string path in Directory.GetFiles(p1, "*", SearchOption.AllDirectories))
            {
                if (!File.Exists(path.Replace(p1, p2)))
                {
                    return false;
                }
                else
                {
                    try
                    {


                        if (FileEquals(path, path.Replace(p1, p2)) == false)
                        {
                            return false;
                        }
                    }
                    catch (IOException)
                    {

                    }
                }

            }
            return true;
        }
    }
}
