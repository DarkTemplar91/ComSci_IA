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

namespace SyncAppGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pathGrid.AutoGenerateColumns = false;
            Grid();
            pathGrid.CellContentClick += new DataGridViewCellEventHandler(pathGrid_CellValueChanged);
            pathGrid.CurrentCellDirtyStateChanged += new EventHandler(pathGrid_DirtyCell);
            pathGrid.CellClick += new DataGridViewCellEventHandler(pathGrid_OnClick);
            pathGrid.DataSource = pathGridMembers;
            
        }


        static BindingList<pathGridMember> pathGridMembers = new BindingList<pathGridMember>();
        


        public void Grid()
        {
            
            pathGrid.AllowUserToAddRows = false;
            pathGrid.AllowUserToOrderColumns = true;
            pathGrid.AllowUserToOrderColumns = false;

            //add sorting to BindingList later as this does not work
            foreach (DataGridViewColumn col in pathGrid.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            pathGrid.ColumnCount = 4;
            pathGrid.Columns[0].DataPropertyName = "SFolder";
            pathGrid.Columns[0].Name = "Source Folder";
            pathGrid.Columns[1].DataPropertyName = "Source";
            pathGrid.Columns[1].Name = "Source Path";
            pathGrid.Columns[2].DataPropertyName = "Target";
            pathGrid.Columns[2].Name = "Target Path";
            pathGrid.Columns[3].DataPropertyName = "TFolder";
            pathGrid.Columns[3].Name = "Target Folder";
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            pathGrid.Columns.Add(checkBoxColumn);
            pathGrid.Columns[4].Name = "AutoSync";
            pathGrid.Columns[4].DataPropertyName = "AutoSync";
            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
            comboBoxColumn.DataSource = Enum.GetNames(typeof(pathGridMember.syncTypes));
            pathGrid.Columns.Add(comboBoxColumn);
            pathGrid.Columns[5].Name = "Sync Type";
            pathGrid.Columns[5].DataPropertyName = "SyncType";
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "Delete";
            buttonColumn.HeaderText = "Delete";
            buttonColumn.Text = "Delete";
            buttonColumn.Resizable = DataGridViewTriState.False;

            buttonColumn.FlatStyle = FlatStyle.Standard;
            pathGrid.Columns.Add(buttonColumn);
            
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
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
                    pathGridMembers.Add(new pathGridMember(textSource.Text, textTarget.Text));
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
            string temp = "";
            temp = textSource.Text;
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
                labelSource.Text = "";
                labelError.Text = "";
            }
            else if (Evaluation(textSource.Text) == false)
            {

                textSource.BackColor = Color.Red;
                textSource.ForeColor = Color.WhiteSmoke;

                labelSource.ForeColor = Color.Red;
                labelSource.Text = "Path input is invalid!";
            }
            else
            {
                labelSource.Text = "";
                textSource.BackColor = Color.White;
                textSource.ForeColor = Color.Black;
            }
        }
        private void textTarget_Validating(object sender, CancelEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(textTarget.Text))
            {
                labelTarget.Text = "";
                textTarget.BackColor = Color.White;
                textTarget.ForeColor = Color.Black;
                labelError.Text = "";
            }
            else if (Evaluation(textTarget.Text) == false)
            {

                textTarget.BackColor = Color.Red;
                textTarget.ForeColor = Color.WhiteSmoke;
                labelTarget.ForeColor = Color.Red;
                labelTarget.Text = "Path input is invalid!";
            }
            else
            {
                labelTarget.Text = "";
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
        //handles the changes in the combobox
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

    }
    
}
