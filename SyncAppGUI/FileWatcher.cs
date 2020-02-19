using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SyncAppGUI
{
    public class FileWatcher
    {
        //Objects that will do the monitoring process
        private pathGridMember DataMember;
        private pathGridMember DataMember_backward;
        private FileSystemWatcher watcher;
        private FileSystemWatcher watcher_backward;

        private bool watcherOn = false;
        private string path;
        private string target;
        private string WatcherConnection;

        public string Path => path;
        public string Target => target;
        public string WatcherConnection1 => WatcherConnection;

        //the use of this list prevents the bug that FileSystemWatcher raises an event twice (from StackOverFlow)
        private List<string> _changedFiles = new List<string>();

        //Stores files to be deleted and the exceptions in the case of Mirror synchronization.
        private static Queue<string> deletePath = new Queue<string>();
        private static List<string> exceptionPath = new List<string>();

        //Initializes the watcher(s)
        public FileWatcher(string u_path, string u_target, string connection)
        {
            path = u_path;
            target = u_target;
            WatcherConnection = connection;

            DataMember = new pathGridMember(path, target);

            if (connection == "Mirror")
            {
                DataMember_backward = new pathGridMember(target, path);
                watcher_backward = new FileSystemWatcher();

                watcher_backward.Path = DataMember_backward.Source;
                watcher_backward.NotifyFilter = NotifyFilters.LastWrite
                                              | NotifyFilters.FileName
                                              | NotifyFilters.DirectoryName
                                              | NotifyFilters.Size;
                watcher_backward.IncludeSubdirectories = true;

                watcher_backward.Deleted += OnMirrorDeleted;
                watcher_backward.Changed += OnMirror;
                watcher_backward.Created += OnMirror;
                watcher_backward.Renamed += OnMirrorRenamed;
            }

            watcher = new FileSystemWatcher();

            watcher.Path = DataMember.Source;

            watcher.NotifyFilter = NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.Size;
            watcher.IncludeSubdirectories = true;

            switch (connection)
            {
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
                    watcher.Deleted += OnMirrorDeleted;
                    watcher.Changed += OnMirror;
                    watcher.Created += OnMirror;
                    watcher.Renamed += OnMirrorRenamed;
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

        //writes byte by byte, used instead of File.Copy and Directory.Copy
        private void Write(string n_path, string n_target)
        {
            var buffer = new byte[1024 * 1024];
            var bytesread = 0;
            using (FileStream sr = new FileStream(n_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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

        //EventHandlers for changes in the directory
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


            if (e.FullPath.EndsWith(".tmp")) return;

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
                        Write(e.FullPath, e.FullPath.Replace(DataMember.SourceDirectory, DataMember.TargetDirectory));
                        break;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(3000);
                    }
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
        }
        private void OnDeleted(object source, FileSystemEventArgs e)
        {
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
                deletePath.Enqueue(e.FullPath.Replace(DataMember.SourceDirectory, DataMember.TargetDirectory));
            }
            else if (source.Equals(watcher_backward))
            {
                deletePath.Enqueue(e.FullPath.Replace(DataMember_backward.SourceDirectory, DataMember_backward.TargetDirectory));
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
        }
        private void OnRenamed(object source, RenamedEventArgs e)
        {
            lock (_changedFiles)
            {
                if (_changedFiles.Contains(e.FullPath))
                {
                    return;
                }
                _changedFiles.Add(e.FullPath);
            }

            string[] oldpath = e.OldFullPath.Split('\\');
            string oldname = oldpath[oldpath.Length - 1];

            File.Delete(DataMember.TargetDirectory + oldname);
            FileAttributes attr = File.GetAttributes(e.FullPath);

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                Directory.CreateDirectory(e.FullPath.Replace(path, target));
            }
            else
            {
                Write(e.FullPath, e.FullPath.Replace(path, target));
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
        }

        private void OnMirrorRenamed(object source, RenamedEventArgs e)
        {
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
                    Directory.Move(e.OldFullPath.Replace(DataMember.SourceDirectory, DataMember.TargetDirectory), e.FullPath.Replace(DataMember.SourceDirectory, DataMember.TargetDirectory));
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
                    Directory.Move(e.OldFullPath.Replace(DataMember_backward.SourceDirectory, DataMember_backward.TargetDirectory), e.FullPath.Replace(DataMember_backward.SourceDirectory, DataMember_backward.TargetDirectory));
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
            if (source.Equals(watcher))
            {
                watcher_backward.EnableRaisingEvents = false;
                if (e.ChangeType == WatcherChangeTypes.Changed || e.ChangeType == WatcherChangeTypes.Created)
                {
                    OnChanged(source, e);
                }

                if (syncNow.DirEqual(DataMember.SourceDirectory, DataMember.TargetDirectory) == false ||
                    syncNow.DirEqual(DataMember_backward.SourceDirectory, DataMember_backward.TargetDirectory) == false)
                {
                    syncNow.MirrorBoth(DataMember.SourceDirectory, DataMember.TargetDirectory);
                }
                watcher_backward.EnableRaisingEvents = true;
            }
            else if (source.Equals(watcher_backward))
            {
                watcher.EnableRaisingEvents = false;
                if (e.ChangeType == WatcherChangeTypes.Changed || e.ChangeType == WatcherChangeTypes.Created)
                {
                    OnChanged(source, e);
                }

                if (syncNow.DirEqual(DataMember.SourceDirectory, DataMember.TargetDirectory) == false ||
                    syncNow.DirEqual(DataMember_backward.SourceDirectory, DataMember_backward.TargetDirectory) == false)
                {
                    syncNow.MirrorBoth(DataMember.SourceDirectory, DataMember.TargetDirectory);
                }
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
        }
        private void OnMirrorDeleted(object source, FileSystemEventArgs e)
        {
            lock (_changedFiles)
            {
                if (_changedFiles.Contains(e.FullPath))
                {
                    return;
                }
                _changedFiles.Add(e.FullPath);
            }


            if (source.Equals(watcher) && exceptionPath.Contains(e.FullPath) == false)
            {
                deletePath.Enqueue(e.FullPath.Replace(DataMember.SourceDirectory, DataMember.TargetDirectory));
                exceptionPath.Add(e.FullPath.Replace(DataMember.SourceDirectory, DataMember.TargetDirectory));
            }
            else if (source.Equals(watcher_backward) && exceptionPath.Contains(e.FullPath) == false)
            {
                deletePath.Enqueue(e.FullPath.Replace(DataMember_backward.SourceDirectory, DataMember_backward.TargetDirectory));
                exceptionPath.Add(e.FullPath.Replace(DataMember_backward.SourceDirectory, DataMember_backward.TargetDirectory));
            }
            else
            {
                return;
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
        }

        //call method to delete paths in main!
        public static void DeletePaths()
        {
            do
            {
                while (deletePath.Count != 0)
                {
                    string path = deletePath.Peek();
                    if (File.Exists(path) || Directory.Exists(path))
                    {
                        try
                        {
                            File.Delete(path);
                            deletePath.Dequeue();
                        }
                        catch
                        {
                            Directory.Delete(path);
                            deletePath.Dequeue();
                        }
                    }
                    else if (File.Exists(path) == false)
                    {
                        deletePath.Dequeue();
                    }
                    else break;
                }
                exceptionPath.Clear();

            } while (true);
        }
    }
}