using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Syncs
{
    class Mirror
    {
        public static string TrimEnd(string path1)
        {
            char[] arr = new char[path1.Length];
            
            if (path1.ToArray()[path1.ToArray().Length - 1] == '\\')
            {

                arr = path1.ToArray();
                int counter = 0;
                for (int n = path1.ToArray().Length - 1; n >= 0; n--)
                {
                    if (arr[n] == '\\')
                    {
                        counter++;
                    }
                    else break;
                }
                arr = new char[path1.Length - counter];
                for (int n = 0; n < arr.Length; n++)
                {
                    arr[n] = path1[n];
                }

                return new string(arr);
            }
            else return path1;
            
        }
        public static bool IsSubdirectory(string parentDir, string subDir)
        {
            
            string p1 = parentDir;
            string p2 = subDir;
            p1 = TrimEnd(parentDir);
            p2 = TrimEnd(subDir);
            DirectoryInfo di1 = new DirectoryInfo(p1);
            DirectoryInfo di2 = new DirectoryInfo(p2);
            bool isParent = false;
            while (di2.Parent != null)
            {
                if (di2.Parent.FullName == di1.FullName)
                {
                    isParent = true;
                    break;
                }
                else di2 = di2.Parent;
            }
            return isParent;
        }

        void MirrorDir(string d1Path, string d2Path)
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
            TrimEnd(d1path);
            TrimEnd(d2path);
            Mirror m = new Mirror();
            m.MirrorDir(d1path, d2path);
            m.MirrorDir(d2path, d1path);
            
        }

        bool FileEquals(string p1, string p2)
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
    }
}
