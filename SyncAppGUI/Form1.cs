using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace SyncAppGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Name = "AutoSyncApp";
            pathGrid.AutoGenerateColumns = false;
            Grid();
            pathGrid.CellContentClick += new DataGridViewCellEventHandler(pathGrid_CellValueChanged);
            pathGrid.CurrentCellDirtyStateChanged += new EventHandler(pathGrid_DirtyCell);
            pathGrid.CellClick += new DataGridViewCellEventHandler(pathGrid_OnClick);
            pathGrid.DataSource = pathGridMembers;
            toolTip1.SetToolTip(pathGrid, "yaaay");
            toolTip1.SetToolTip(addButton, "Add new folder pair");
            toolTip1.SetToolTip(swapButton, "Swap the two texts");
            toolTip1.SetToolTip(browseSource, "Select the source folder");
            toolTip1.SetToolTip(browseTarget, "Select the target folder");
            saveFileDialog1.Filter= "Text Files (.txt)| *.txt|Comma Seperated Values File (.csv)|*.csv";
            saveFileDialog1.DefaultExt = "*.csv";
            openFileDialog1.Filter= "Text Files (.txt)| *.txt|Comma Seperated Values File (.csv)|*.csv";
            openFileDialog1.DefaultExt = saveFileDialog1.InitialDirectory;

        }
        
        static readonly BindingList<pathGridMember> pathGridMembers = new BindingList<pathGridMember>();
        
        public void Grid()
        {
            
            this.pathGrid.AllowUserToAddRows = false;
            this.pathGrid.AllowUserToOrderColumns = true;
            this.pathGrid.AllowUserToOrderColumns = false;

            //add sorting to BindingList later as this does not work
            foreach (DataGridViewColumn col in this.pathGrid.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

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

           foreach(DataGridViewColumn c in pathGrid.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            

        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textSource.Text) == false || Directory.Exists(textTarget.Text) == false) return;
            if (string.IsNullOrEmpty(textTarget.Text) && string.IsNullOrEmpty(textSource.Text)) ;
            else if(string.IsNullOrEmpty(textTarget.Text) || string.IsNullOrEmpty(textSource.Text))
            {
                labelError.Text = "Select two folders!";
            }
            else if ((pathGridMembers.Any(x => (x.Source == textSource.Text) && (x.Target == textTarget.Text)) == false) && Evaluation(textSource.Text) == true && Evaluation(textTarget.Text) == true)
            {
                if (pathGridMembers.Any(x => (x.Source == textTarget.Text) && (x.Target == textSource.Text)))
                {
                    labelError.ForeColor = Color.Red;
                    labelError.Text = "An identical folder pair is already being monitored. Set SyncType to \"Mirror\" if you wish to monitor both folders!";
                }
                else
                {
                    
                    string s = "";
                    string t = "";
                    if (textSource.Text.EndsWith("\\"))
                    {
                        s = textSource.Text.Substring(0, textSource.Text.Length - 2);
                    }
                    else if (textTarget.Text.EndsWith("\\"))
                    {
                        t = textTarget.Text.Substring(0, textTarget.Text.Length - 2);
                    }
                    else
                    {
                        s = textSource.Text;
                        t = textTarget.Text;

                        if (IsSubdirectory(s, t) || IsSubdirectory(t, s))
                        {
                            labelError.Text = "One of the folders is a subfolder of the other one!";

                            return;
                        }
                        
                    }

                    pathGridMembers.Add(new pathGridMember(s, t));
                    pathGrid.Refresh();

                }
            }
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
            using (FolderBrowserDialog browser=new FolderBrowserDialog())
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
            foreach (string file in files) textSource.Text=file;

        }
        private void TextSource_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop,false))
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
        private bool Evaluation(string source)
        {
            Regex r = new Regex(@"^(([a-zA-Z]\:)|(\\))(\\{1}|((\\{1})[^\\]([^/:*?<>""|]*))+)$");
            if (string.IsNullOrWhiteSpace(source)) return false;
            if (r.IsMatch(source)) return true;
            else return false;
        }
        private void textSource_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textSource.Text))
            {
                textSource.BackColor = Color.White;
                textSource.ForeColor = Color.Black;
                labelSource.Text = null;
                labelError.Text = null;
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
                labelError.Text = null;
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

            if (e.ColumnIndex == pathGrid.Columns["AutoSync"].Index)
            {
                DataGridViewCheckBoxCell ch1 = (DataGridViewCheckBoxCell)pathGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (ch1.Value == null) ch1.Value = false;

                int index=pathGridMembers.IndexOf(pathGridMembers.First(x => (x.Source == (string)pathGrid.Rows[e.RowIndex].Cells["Source path"].Value) && (x.Target == (string)pathGrid.Rows[e.RowIndex].Cells["Target path"].Value)));
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
            pathGrid.EndEdit();
        }
        private void pathGrid_DirtyCell(object sender, EventArgs e)
        {
            if (pathGrid.IsCurrentCellDirty &&pathGrid.CurrentCell is DataGridViewComboBoxCell)
            {
                int index = pathGridMembers.IndexOf(pathGridMembers.First
                    (x => (x.Source == (string)pathGrid.Rows[pathGrid.CurrentRow.Index].Cells["Source path"].Value)
                    && (x.Target == (string)pathGrid.Rows[pathGrid.CurrentRow.Index].Cells["Target path"].Value)));
                
                pathGridMembers[index].SyncType=(string)pathGrid.CurrentCell.Value;
            }
        }
        private void pathGrid_OnClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex>=0)
            {
                if (e.ColumnIndex == pathGrid.Rows[e.RowIndex].Cells["Delete"].ColumnIndex)
                {
                    int index = pathGridMembers.IndexOf(pathGridMembers.First(x => (x.Source == (string)pathGrid.Rows[e.RowIndex].Cells["Source path"].Value) && (x.Target == (string)pathGrid.Rows[e.RowIndex].Cells["Target path"].Value)));
                    pathGridMembers.Remove(pathGridMembers[index]);
                    pathGrid.EndEdit();

                }
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            textSource.Text = null;
            textTarget.Text = null;
            textSource.Focus();
            textTarget.Focus();
            textTarget.Focus();
            swapButton.Focus();
        }

        private void syncNowButton_Click(object sender, EventArgs e)
        {

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
            DialogResult res=saveFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                String fileName = saveFileDialog1.FileName;
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
                Load(fileName);
            }

        }
        public void Save(string path)
        {
            using (StreamWriter sr = new StreamWriter(path.Substring(0, path.Length - 4) + "_grid" + path.Substring(path.Length - 4))) 
            {
                for(int n = 0; n < pathGrid.Rows.Count; n++)
                {
                    string row = pathGridMembers[n].SFolder + ";" + pathGridMembers[n].Source +
                        ";" + pathGridMembers[n].Target + ";" + pathGridMembers[n].TFolder + ";" +
                        pathGridMembers[n].AutoSync + ";" + pathGridMembers[n].SyncType + ";";
                    sr.WriteLine(row);
                }
            }
        }
        public void Load(string path)
        {
            
            using (StreamReader sr=new StreamReader(path))
            {
                string line = "";
                pathGridMembers.Clear();
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

        private void ManageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            templateForm temp = new templateForm();
            temp.Show();
        }
    }
    
}
