using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncAppGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Grid();
        }

        public void Grid()
        {
            pathGrid.ColumnCount = 4;
            pathGrid.Columns[0].Name = "Source Folder";
            pathGrid.Columns[1].Name = "Source Path";
            pathGrid.Columns[2].Name = "Target Path";
            pathGrid.Columns[3].Name = "Target Folder";
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            pathGrid.Columns.Add(checkBoxColumn);
            pathGrid.Columns[4].Name = "AutoSync";
            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
            pathGrid.Columns.Add(comboBoxColumn);
            pathGrid.Columns[5].Name = "Sync Type";
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "Delete";
            buttonColumn.HeaderText = "Delete";
            pathGrid.Columns.Add(buttonColumn);
            
            pathGrid.AllowUserToAddRows = false;
            pathGrid.AutoGenerateColumns = true;
            
            
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (pathGrid.Rows.Count > 0)
            {
                if (((string)pathGrid[0, pathGrid.Rows.Count - 1].Value==null) == false)
                {
                    pathGrid.Rows.Add();
                }
            }
            else
            {
                pathGrid.Rows.Add();
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
    }
    
}
