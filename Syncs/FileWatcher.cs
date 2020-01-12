using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Security.AccessControl;

namespace Syncs
{
    class FileWatcher
    {
        DataHandling DataMember;
        DataHandling DataMember_backward;
        FileSystemWatcher watcher;
        FileSystemWatcher watcher_backward;

        bool watcherOn = false;
        bool let = false;
        string path;
        string target;
        string WatcherConnection;

        List<string> _changedFiles = new List<string>();
        public void CreateWatcher(string u_path, string u_target, string connection)
        {
            path = u_path;
            target = u_target;
            WatcherConnection = connection;
            
            DataMember = new DataHandling(path, target);
            if (connection == "Mirror")
            {
                DataMember_backward = new DataHandling(target, path);
                watcher_backward = new FileSystemWatcher();

                watcher_backward.Path = DataMember_backward.sourceDirectory;
                watcher_backward.NotifyFilter = NotifyFilters.LastWrite
                                              | NotifyFilters.FileName
                                              | NotifyFilters.DirectoryName
                                              | NotifyFilters.Size;
                watcher_backward.IncludeSubdirectories = true;

                watcher_backward.Deleted += OnDeleted;
                watcher_backward.Changed += OnMirror;
                watcher_backward.Created += OnMirror;
                watcher_backward.Renamed += OnMirroRenamed;


            }


            watcher = new FileSystemWatcher();

            watcher.Path = DataMember.sourceDirectory;

            watcher.NotifyFilter = NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.Size;
            watcher.IncludeSubdirectories = true;

            switch (connection)
            {
                //calls method more than once
                case "Constructive":
                    watcher.Changed += OnChanged;
                    watcher.Created += OnChanged;
                    watcher.Deleted += OnDeleted;
                    watcher.Renamed += OnRenamed;
                    break;
                case "Destructive":
                    watcher.Changed += OnChanged;
                    watcher.Created += OnChanged;
                    watcher.Renamed += OnRenamed;

                    break;
                case "Mirror":
                    watcher.Deleted += OnDeleted;
                    watcher.Changed += OnMirror;
                    watcher.Created += OnMirror;
                    watcher.Renamed += OnMirroRenamed;

                    break;
            }
            



        }
        public void TurnOnWatcher()
        {
            watcher.EnableRaisingEvents = true;
            watcherOn = true;
            if (WatcherConnection == "Mirror")
            {
                watcher_backward.EnableRaisingEvents = true;
            }

        }
        void Write(string n_path, string n_target)
        {
            var buffer = new byte[1024 * 1024];
            var bytesread = 0;
            using (FileStream sr = new FileStream(n_path, FileMode.Open,FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bufferedStream = new BufferedStream(sr))
            using (FileStream sr2 = new FileStream(n_target, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            using (BufferedStream swb = new BufferedStream(sr2))
            {
                while (true)
                {
                    bytesread = bufferedStream.Read(buffer, 0, buffer.Length);
                    if (bytesread == 0) break;
                    swb.Write(buffer, 0, bytesread);

                }
                swb.Flush();
            }
        }


        private void OnChanged(object source, FileSystemEventArgs e)
        {
            lock (_changedFiles)
            {
                if (_changedFiles.Contains(e.FullPath))
                {
                    return;
                }
                _changedFiles.Add(e.FullPath);
            }

            Console.WriteLine("Event called - {0}", e.Name);
            Stopwatch sw = new Stopwatch();
            sw.Start();



            FileInfo file = new FileInfo(e.FullPath);
            FileAttributes attr = File.GetAttributes(e.FullPath);
            string newPath = e.FullPath.Replace(e.FullPath, target);


            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {

                foreach (string dirPath in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
                {
                    if (!Directory.Exists(dirPath.Replace(path, newPath)))
                    {
                        Directory.CreateDirectory(dirPath.Replace(path, newPath));
                    }

                }
                foreach (string sourcePath in Directory.GetFiles(e.FullPath, "*", SearchOption.AllDirectories))
                {

                    while (true)
                    {
                        try
                        {
                            Write(sourcePath, sourcePath.Replace(path, target));
                            break;
                        }
                        catch (Exception)
                        {
                            Thread.Sleep(30000);
                        }
                    }
                }


            }
            else
            {

                while (true)
                {
                    try
                    {

                        Write(e.FullPath, DataMember.targetDirectory + file.Name);
                        break;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(30000);
                    }
                }

            }

            sw.Stop();
            Console.WriteLine("Time elapsed: {0}", sw.Elapsed);

            System.Timers.Timer timer = new System.Timers.Timer(1000) { AutoReset = false };
            timer.Elapsed += (timerElapsedSender, timerElapsedArgs) =>
            {
                lock (_changedFiles)
                {
                    _changedFiles.Remove(e.FullPath);
                }
            };
            timer.Start();

        }    
        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("Event raised by {0}: {1}", e.Name,e.ChangeType);
            FileInfo file = new FileInfo(e.FullPath);
            if (source.Equals(watcher))
            {
                try
                {
                    Directory.Delete(e.FullPath.Replace(DataMember.sourceDirectory, DataMember.targetDirectory));

                }
                catch (DirectoryNotFoundException)
                {
                    //for duplicate ecent
                }
                catch (IOException)
                {
                    File.Delete(e.FullPath.Replace(DataMember.sourceDirectory, DataMember.targetDirectory));
                }
                
                


            }
            else if (source.Equals(watcher_backward))
            {
                try
                {
                    Directory.Delete(e.FullPath.Replace(DataMember_backward.sourceDirectory, DataMember_backward.targetDirectory),true);

                }
                catch (IOException)
                {
                    File.Delete(e.FullPath.Replace(DataMember_backward.sourceDirectory, DataMember_backward.targetDirectory));
                }
                catch
                {
                    //duplicate events
                }
            }
            
            
            
            
        }
        private void OnRenamed(object source, RenamedEventArgs e)
        {
         
            string[] oldpath = e.OldFullPath.Split('\\');
            string oldname = oldpath[oldpath.Length - 1];

            File.Delete(DataMember.targetDirectory + oldname);
            FileAttributes attr = File.GetAttributes(e.FullPath);

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                Directory.CreateDirectory(e.FullPath.Replace(path, target));
            }
            else
            {
                Write(e.FullPath, e.FullPath.Replace(path, target));
            }
        }
        private void OnMirror(object source, FileSystemEventArgs e)
        {
            
            lock (_changedFiles)
            {
                if (_changedFiles.Contains(e.FullPath))
                {
                    return;
                }
                _changedFiles.Add(e.FullPath);
            }
            Console.WriteLine("Event raised by {0}: {1}",e.Name, e.ChangeType);
            

            if (source.Equals(watcher))
            {
                
                watcher_backward.EnableRaisingEvents = false;
                
                Mirror.MirrorBoth(DataMember.sourceDirectory, DataMember.targetDirectory);
                watcher_backward.EnableRaisingEvents = true;

            }
            else if (source.Equals(watcher_backward))
            {

                watcher.EnableRaisingEvents = false;
                Mirror.MirrorBoth(DataMember_backward.sourceDirectory, DataMember_backward.targetDirectory);
                watcher.EnableRaisingEvents = true;

            }

            System.Timers.Timer timer = new System.Timers.Timer(1000) { AutoReset = false };
            timer.Elapsed += (timerElapsedSender, timerElapsedArgs) =>
            {
                lock (_changedFiles)
                {
                    _changedFiles.Remove(e.FullPath);
                }
            };
            timer.Start();
            Console.WriteLine("EventHandler finished");
        }
        private void OnMirroRenamed(object source, RenamedEventArgs e)
        {
            Console.WriteLine("Event raised by {0}: {1}", e.Name, e.ChangeType);
            lock (_changedFiles)
            {
                if (_changedFiles.Contains(e.FullPath))
                {
                    return;
                }
                _changedFiles.Add(e.FullPath);
            }
            if (source.Equals(watcher))
            {
                try
                {
                    Directory.Move(e.OldFullPath.Replace(DataMember.sourceDirectory, DataMember.targetDirectory), e.FullPath.Replace(DataMember.sourceDirectory, DataMember.targetDirectory));

                }
                catch (DirectoryNotFoundException)
                {
                    //If the renamed event is raised twice, the Directory.Move would point to a non-existant library. This solves it.
                }
                catch
                {

                }
            }
            else if (source.Equals(watcher_backward))
            {
                try
                {
                    Directory.Move(e.OldFullPath.Replace(DataMember_backward.sourceDirectory, DataMember_backward.targetDirectory), e.FullPath.Replace(DataMember_backward.sourceDirectory, DataMember_backward.targetDirectory));

                }
                catch (DirectoryNotFoundException)
                {
                    //If the renamed event is raised twice, the Directory.Move would point to a non-existant library. This solves it.
                }
                catch
                {

                }
            }
            
            System.Timers.Timer timer = new System.Timers.Timer(1000) { AutoReset = false };
            timer.Elapsed += (timerElapsedSender, timerElapsedArgs) =>
            {
                lock (_changedFiles)
                {
                    _changedFiles.Remove(e.FullPath);
                }
            };
            timer.Start();
            Console.WriteLine("EventHandler finished");
        }
    }
}
