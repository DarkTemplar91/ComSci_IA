using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncAppGUI
{
    public partial class Form1 : Form
    {
        static bool startUp = true;
        public Form1()
        {

            InitializeComponent();

            //Sets minimum size of the main form so all controls are visible
            this.MinimumSize = new Size(820, 490);
            this.Icon = new Icon("icon.ico");
            this.Name = "AutoSyncApp";

            SplitInit();

            pathGrid.AutoGenerateColumns = false;
            Grid();
            pathGrid.CurrentCellDirtyStateChanged += new EventHandler(pathGrid_DirtyCell);
            pathGrid.CellClick += new DataGridViewCellEventHandler(pathGrid_OnClick);
            pathGrid.DataSource = pathGridMembers;

            toolTip1.SetToolTip(addButton, "Add new folder pair");
            toolTip1.SetToolTip(swapButton, "Swap the two texts");
            toolTip1.SetToolTip(browseSource, "Select the source folder");
            toolTip1.SetToolTip(browseTarget, "Select the target folder");
            toolTip1.SetToolTip(textSource, textSource.Text);
            toolTip1.SetToolTip(textTarget, textTarget.Text);

            saveFileDialog1.Filter = "Text Files (.txt)| *.txt|Comma Seperated Values File (.csv)|*.csv";
            saveFileDialog1.DefaultExt = "*.csv";
            openFileDialog1.Filter = "Text Files (.txt)| *.txt|Comma Seperated Values File (.csv)|*.csv";
            openFileDialog1.DefaultExt = saveFileDialog1.InitialDirectory;

            pathGridMembers.ListChanged += new ListChangedEventHandler(bindingList_Changed);
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(Timer1_Tick);

            notifyIcon1.Text = "SyncApp";
            notifyIcon1.BalloonTipText = "Sync App minimized";
            notifyIcon1.BalloonTipTitle = "Sync App";
            notifyIcon1.Icon = new Icon("icon.ico");

            setupTimer();
            passWatcher();
            timer1.Start();
            startUp = false;


        }
        //Stores the data to be loaded into the DataGridView
        static public BindingList<pathGridMember> pathGridMembers = new BindingList<pathGridMember>();
        //List os FileWatcher objects that monitor the directories
        static List<FileWatcher> fileWatchers = new List<FileWatcher>();
        //Timer for interval synchroniztion
        static System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        public void Grid()
        {

            this.pathGrid.AllowUserToAddRows = false;
            this.pathGrid.AllowUserToOrderColumns = false;
            this.pathGrid.AllowUserToResizeColumns = false;

            this.pathGrid.ColumnCount = 4;
            this.pathGrid.Columns[0].DataPropertyName = "SFolder";
            this.pathGrid.Columns[0].Name = "Source Folder";
            this.pathGrid.Columns[1].DataPropertyName = "Source";
            this.pathGrid.Columns[1].Name = "Source Path";
            this.pathGrid.Columns[2].DataPropertyName = "Target";
            this.pathGrid.Columns[2].Name = "Target Path";
            this.pathGrid.Columns[3].DataPropertyName = "TFolder";
            this.pathGrid.Columns[3].Name = "Target Folder";
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            this.pathGrid.Columns.Add(checkBoxColumn);
            this.pathGrid.Columns[4].Name = "AutoSync";
            this.pathGrid.Columns[4].DataPropertyName = "AutoSync";
            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
            comboBoxColumn.DataSource = Enum.GetNames(typeof(pathGridMember.syncTypes));
            this.pathGrid.Columns.Add(comboBoxColumn);
            this.pathGrid.Columns[5].Name = "Sync Type";
            this.pathGrid.Columns[5].DataPropertyName = "SyncType";
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "Delete",
                Text = "Delete",
                Resizable = DataGridViewTriState.False,

                FlatStyle = FlatStyle.Standard
            };
            this.pathGrid.Columns.Add(buttonColumn);
            RefresResizableColumns(pathGrid);


        }
        public void RefresResizableColumns(DataGridView grid)
        {
            for (int n = 0; n < grid.ColumnCount; n++)
            {
                grid.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            grid.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            grid.Refresh();
            for (int n = 0; n < grid.ColumnCount; n++)
            {
                int colw = grid.Columns[n].Width;
                grid.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grid.Columns[n].Width = colw;
            }
            grid.AllowUserToResizeColumns = true;
            grid.Refresh();
        }

        //Checks if path input is valid using RegularExpressions
        private bool Evaluation(string source)
        {
            Regex r = new Regex(@"^(([a-zA-Z]\:)|(\\))(\\{1}|((\\{1})[^\\]([^/:*?<>""|]*))+)$");
            if (string.IsNullOrWhiteSpace(source)) return false;
            if (r.IsMatch(source)) return true;
            else return false;
        }
        //Saves data from DataGridView to file
        public void Save(string path)
        {
            //create new StreamWriter object to write.
            //Automatically discarded after using block end
            using (StreamWriter sr = new StreamWriter(path.Substring(0, path.Length - 4) + "_grid" +
                path.Substring(path.Length - 4)))
            {
                for (int n = 0; n < pathGrid.Rows.Count; n++)
                {
                    string row = pathGridMembers[n].SourceDirectory + ";" + pathGridMembers[n].Source +
                        ";" + pathGridMembers[n].Target + ";" + pathGridMembers[n].TargetDirectory + ";" +
                        pathGridMembers[n].AutoSync + ";" + pathGridMembers[n].SyncType + ";";
                    sr.WriteLine(row);
                }
            }
        }
        //Reads in data and loads it to DataGridView
        void LoadDGV(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                pathGridMembers.Clear();
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split(';');
                    pathGridMembers.Add(new pathGridMember(arr[1], arr[2]));
                    if (arr[3] == pathGridMember.syncTypes.Constructive.ToString())
                    {
                        pathGridMembers.Last().SyncType = pathGridMember.syncTypes.Constructive.ToString();
                    }
                    else if (arr[3] == pathGridMember.syncTypes.Destructive.ToString())
                    {
                        pathGridMembers.Last().SyncType = pathGridMember.syncTypes.Destructive.ToString();
                    }
                    else if (arr[3] == null) ;
                    else
                    {
                        pathGridMembers.Last().SyncType = pathGridMember.syncTypes.Mirror.ToString();
                    }
                    pathGridMembers.Last().AutoSync = Convert.ToBoolean(arr[4]);

                }

            }

        }
        //Checks if the two directories are subdirectories of each other or not
        public static bool IsSubdirectory(string parentDir, string subDir)
        {

            string p1 = parentDir;
            string p2 = subDir;
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
        private void setupTimer()
        {
            if (settings.interval != 0)
            {
                timer1.Interval = settings.interval;
                timer1.Start();
            }
            else
            {
                timer1.Interval = 6000;
                timer1.Start();
            }
        }
        //Display Ballon with information
        private void showBalloon(string title, string body)
        {
            using (NotifyIcon notifyIcon = new NotifyIcon())
            {
                notifyIcon.Visible = true;

                if (title != null)
                {
                    notifyIcon.BalloonTipTitle = title;
                }

                if (body != null)
                {
                    notifyIcon.BalloonTipText = body;
                }
                notifyIcon.Icon = SystemIcons.Application;
                notifyIcon.ShowBalloonTip(3000);
            }
        }
        //Makes changes in fileWatchers list, add, removes object
        public void passWatcher()
        {
            //Checks if there is a FileWatcher object in the list for which the
            //automatic synchronization has been turned off
            for (int n = 0; n < pathGridMembers.Count; n++)
            {
                //BindingList that populates the DataGridView
                if (pathGridMembers[n].AutoSync == false)
                {
                    try
                    {
                        if (fileWatchers.Count > 0)
                        {
                            //Checks if there are identical items in the two lists. If there are
                            //they should be removed as AutoSync is no longer turned on.
                            fileWatchers.Remove(fileWatchers.First(x => (x.Path == pathGridMembers[n].Source
                            && x.Target == pathGridMembers[n].Target &&
                            x.WatcherConnection1 == pathGridMembers[n].SyncType)));
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }



            }
            //If an item was removed from the list, it should be removed from the fileWatchers list as well
            //Monitoring no longer needed!
            List<int> indexes = new List<int>();
            for (int i = 0; i < fileWatchers.Count; i++)
            {
                bool contain = false;
                for (int n = 0; n < pathGridMembers.Count; n++)
                {
                    if (pathGridMembers[n].SyncType == fileWatchers[i].WatcherConnection1 &&
                        pathGridMembers[n].Source == fileWatchers[i].Path &&
                        pathGridMembers[n].Target == fileWatchers[i].Target)
                    {
                        contain = true;
                        break;
                    }
                }
                if (contain == false)
                {
                    //If there are no matches, it stores the index to delete later
                    indexes.Add(i);
                }
            }
            indexes.Sort();
            for (int n = 0; n < indexes.Count; n++)
            {
                //remove at index as they no longer exist in DGV
                fileWatchers.RemoveAt(indexes[n]);
                indexes = indexes.ConvertAll(x => x = x - 1);

            }

            for (int n = 0; n < pathGridMembers.Count; n++)
            {
                if (pathGridMembers[n].AutoSync == true)
                {
                    if (fileWatchers.Any(x => (x.Path == pathGridMembers[n].Source && x.Target ==
                    pathGridMembers[n].Target && x.WatcherConnection1 == pathGridMembers[n].SyncType)))
                    {

                    }
                    else
                    {
                        //If folder pair is not among the monitored folders,
                        //add them by creating a new object of the FileWatcher
                        fileWatchers.Add(new FileWatcher(pathGridMembers[n].Source,
                            pathGridMembers[n].Target, pathGridMembers[n].SyncType));
                        fileWatchers[fileWatchers.Count - 1].TurnOnWatcher();
                    }
                }
            }
        }
        //SplitContainer dimensions
        public void SplitInit()
        {
            try
            {
                splitContainer1.Panel2MinSize = Width / 10 * 7;
                splitContainer1.SplitterDistance = splitContainer1.Panel2MinSize;
                splitContainer2.Panel2MinSize = (Height / 10 * 6) - 10;
                splitContainer2.SplitterDistance = splitContainer2.Panel2MinSize;
            }
            catch (Exception e)
            {

            }
        }



        //EventHandlers
        private void AddButton_Click(object sender, EventArgs e)
        {

            if (Directory.Exists(textSource.Text) == false || Directory.Exists(textTarget.Text) == false) return;
            if (string.IsNullOrEmpty(textTarget.Text) || string.IsNullOrEmpty(textSource.Text)) return;
            if (textSource.Text == textTarget.Text)
            {
                textError.BackColor = DefaultBackColor;
                textError.ForeColor = Color.Red;
                textError.Text = "The two folders are identical!";
                return;
            }
            else if (string.IsNullOrEmpty(textTarget.Text) || string.IsNullOrEmpty(textSource.Text))
            {
                textError.Text = "Select two folders!";
            }
            else if ((pathGridMembers.Any(x => (x.Source == textSource.Text) && (x.Target == textTarget.Text)) == false) && Evaluation(textSource.Text) == true && Evaluation(textTarget.Text) == true)
            {
                if (pathGridMembers.Any(x => (x.Source == textTarget.Text) && (x.Target == textSource.Text)))
                {
                    textError.BackColor = DefaultBackColor;
                    textError.ForeColor = Color.Red;
                    textError.Text = "An identical folder pair is already being monitored. Set SyncType to \"Mirror\" if you wish to monitor both folders!";
                }
                else
                {

                    string s = "";
                    string t = "";
                    if (textSource.Text.EndsWith("\\"))
                    {
                        if (textSource.Text.Length == 3) ;
                        else s = textSource.Text.Substring(0, textSource.Text.Length - 2);
                    }
                    if (textTarget.Text.EndsWith("\\"))
                    {
                        if (textTarget.Text.Length == 3) ;
                        else t = textTarget.Text.Substring(0, textTarget.Text.Length - 2);
                    }
                    if (textTarget.Text.EndsWith("\\") == false && textSource.Text.EndsWith("\\") == false)
                    {

                        s = textSource.Text;
                        t = textTarget.Text;


                        if (IsSubdirectory(s, t) || IsSubdirectory(t, s))
                        {
                            textError.BackColor = DefaultBackColor;
                            textError.ForeColor = Color.Red;
                            textError.Text = "One of the folders is a subfolder of the other one!";

                            return;
                        }

                    }


                    pathGridMembers.Add(new pathGridMember(s, t));
                    pathGrid.Refresh();
                    textError.Text = null;
                    textSource.Text = null;
                    textTarget.Text = null;
                }
            }
            pathGridMember temp = new pathGridMember("", "");
            pathGridMembers.Add(temp);
            pathGridMembers.Remove(temp);

        }
        private void BrowseSource_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog browser = new FolderBrowserDialog())
            {
                browser.ShowNewFolderButton = true;
                browser.Description = "Select source folder for Syncing";
                browser.RootFolder = Environment.SpecialFolder.MyComputer;
                browser.ShowDialog();
                textSource.Text = browser.SelectedPath;
            }
        }
        private void BrowseTarget_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog browser = new FolderBrowserDialog())
            {
                browser.ShowNewFolderButton = true;
                browser.Description = "Select target folder for Syncing";
                browser.RootFolder = Environment.SpecialFolder.MyComputer;
                browser.ShowDialog();
                textTarget.Text = browser.SelectedPath;
            }
        }
        private void SwapButton_Click(object sender, EventArgs e)
        {

            string temp = textSource.Text;
            textSource.Text = textTarget.Text;
            textTarget.Text = temp;
            textSource.Focus();
            textTarget.Focus();
            textTarget.Focus();
            swapButton.Focus();
        }
        private void TextSource_DragEnter(object sender, DragEventArgs e)
        {

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) textSource.Text = file;

        }
        private void TextSource_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        private void textTarget_DragEnter(object sender, DragEventArgs e)
        {

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) textTarget.Text = file;
        }
        private void textTarget_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        private void textSource_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textSource.Text))
            {
                textSource.BackColor = Color.White;
                textSource.ForeColor = Color.Black;
                labelSource.Text = null;
                textError.Text = null;
            }
            else if (Evaluation(textSource.Text) == false)
            {

                textSource.BackColor = Color.Red;
                textSource.ForeColor = Color.WhiteSmoke;

                labelSource.ForeColor = Color.Red;
                labelSource.Text = "Path input is invalid!";
            }
            else if (Directory.Exists(textSource.Text) != true)
            {
                textSource.BackColor = Color.Red;
                textSource.ForeColor = Color.WhiteSmoke;
                labelSource.ForeColor = Color.Red;
                labelSource.Text = "The folder does not exist!";
            }
            else
            {
                labelSource.Text = null;
                textSource.BackColor = Color.White;
                textSource.ForeColor = Color.Black;
            }
        }
        private void textTarget_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textTarget.Text))
            {
                labelTarget.Text = null;
                textTarget.BackColor = Color.White;
                textTarget.ForeColor = Color.Black;
                textError.Text = null;
            }
            else if (Evaluation(textTarget.Text) == false)
            {

                textTarget.BackColor = Color.Red;
                textTarget.ForeColor = Color.WhiteSmoke;
                labelTarget.ForeColor = Color.Red;
                labelTarget.Text = "Path input is invalid!";
            }
            else if (Directory.Exists(textTarget.Text) != true)
            {
                textTarget.BackColor = Color.Red;
                textTarget.ForeColor = Color.WhiteSmoke;
                labelTarget.ForeColor = Color.Red;
                labelTarget.Text = "The folder does not exist!";
            }
            else
            {
                labelTarget.Text = null;
                textTarget.BackColor = Color.White;
                textTarget.ForeColor = Color.Black;
            }
        }

        private void pathGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == pathGrid.Columns["AutoSync"].Index)
                {
                    DataGridViewCheckBoxCell ch1 = (DataGridViewCheckBoxCell)pathGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    if (ch1.Value == null) ch1.Value = false;

                    int index = pathGridMembers.IndexOf(pathGridMembers.First(x => (x.Source == (string)pathGrid.Rows[e.RowIndex].Cells["Source path"].Value) && (x.Target == (string)pathGrid.Rows[e.RowIndex].Cells["Target path"].Value)));
                    switch (ch1.Value.ToString())
                    {
                        case "True":

                            pathGridMembers[index].AutoSync = false;
                            ch1.Value = false;
                            break;
                        case "False":
                            pathGridMembers[index].AutoSync = true;
                            ch1.Value = true;
                            break;
                    }
                }
                passWatcher();
                pathGrid.EndEdit();
            }
        }
        private void pathGrid_DirtyCell(object sender, EventArgs e)
        {
            if (pathGrid.IsCurrentCellDirty && pathGrid.CurrentCell is DataGridViewComboBoxCell)
            {
                int index = pathGridMembers.IndexOf(pathGridMembers.First
                    (x => (x.Source == (string)pathGrid.Rows[pathGrid.CurrentRow.Index].Cells["Source path"].Value)
                    && (x.Target == (string)pathGrid.Rows[pathGrid.CurrentRow.Index].Cells["Target path"].Value)));

                pathGridMembers[index].SyncType = (string)pathGrid.CurrentCell.Value;
            }
            pathGridMember temp = new pathGridMember("", "");
            pathGridMembers.Add(temp);
            pathGridMembers.Remove(temp);
        }
        private void pathGrid_OnClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == pathGrid.Rows[e.RowIndex].Cells["Delete"].ColumnIndex)
                {
                    int index = pathGridMembers.IndexOf(pathGridMembers.First(x => (x.Source == (string)pathGrid.Rows[e.RowIndex].Cells["Source path"].Value) && (x.Target == (string)pathGrid.Rows[e.RowIndex].Cells["Target path"].Value)));
                    pathGridMembers.Remove(pathGridMembers[index]);
                    pathGrid.EndEdit();


                }
            }
            pathGridMember temp = new pathGridMember("", "");
            pathGridMembers.Add(temp);
            pathGridMembers.Remove(temp);

            passWatcher();
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (pathGridMembers.Count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("This will delete all folder pairs that are being monitored.\nDo you wish to continue?", "Load", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    pathGridMembers.Clear();
                    pathGrid.EndEdit();
                }
            }
        }
        private void syncNowButton_Click(object sender, EventArgs e)
        {
            backgroundWorker1.WorkerReportsProgress = false;
            syncNowButton.Enabled = false;
            textError.Visible = false;
            backgroundWorker1.RunWorkerAsync(pathGrid);

        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingForm form = new settingForm();
            form.ShowDialog();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(settings.defaultSave))
            {
                Directory.CreateDirectory(settings.defaultSave);
            }

            saveFileDialog1.InitialDirectory = settings.defaultSave;
            //Get response from dialog
            DialogResult res = saveFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                //Get the path where the data should be saved
                String fileName = saveFileDialog1.FileName;
                //call save method
                Save(fileName);
            }
        }
        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(settings.defaultSave))
            {
                Directory.CreateDirectory(settings.defaultSave);
            }
            openFileDialog1.InitialDirectory = settings.defaultSave;
            DialogResult res = openFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                LoadDGV(fileName);
            }

        }
        private void ManageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            templateForm temp = new templateForm();
            temp.Show();
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            pathGrid.DataSource = pathGridMembers;
            pathGrid.EndEdit();

        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            SplitInit();
            if (startUp == false)
            {
                RefresResizableColumns(pathGrid);

            }
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(3000);
                this.ShowInTaskbar = false;
            }
        }
        private void bindingList_Changed(object sender, ListChangedEventArgs e)
        {

            if (pathGridMembers.Any(x => (x.SyncType == null || x.SyncType == "")))
            {
                textError.BackColor = DefaultBackColor;
                textError.ForeColor = Color.Red;
                textError.Text = "There are folder pairs with undefined SyncTypes! Those will not be monitored.";
                syncNowButton.Enabled = false;
            }
            else
            {
                syncNowButton.Enabled = true;
                textError.Text = null;
            }

            passWatcher();
        }
        private void PathGrid_Resize(object sender, EventArgs e)
        {
            if (startUp == false)
            {
                RefresResizableColumns(pathGrid);
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = settings.interval;
            if (settings.syncType == settings.SyncTypes.TimeInterval.ToString())
            {
                timer1.Stop();
                timer1.Interval = settings.interval;
                BindingList<pathGridMember> toSync = new BindingList<pathGridMember>();
                for (int n = 0; n < pathGridMembers.Count; n++)
                {
                    if (pathGridMembers[n].AutoSync == false)
                    {
                        toSync.Add(pathGridMembers[n]);
                    }
                }
                Task.Run(() => syncNow.MirrorList(toSync));
                showBalloon("Sync is Done!", "Next Synchronization is in: " + settings.intervalDate.TimeOfDay);
                timer1.Start();
            }

        }
        //The backgroundworker handles the synchronization process initiated by the syncNow button
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1 = sender as BackgroundWorker;

            syncNow.Sync(pathGridMembers, backgroundWorker1, e);
        }
        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            syncNowButton.Enabled = true;
            textError.Visible = true;
            backgroundWorker1.Dispose();
        }
        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
        }
    }

}
