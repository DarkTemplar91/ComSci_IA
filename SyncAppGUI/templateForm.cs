using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SyncAppGUI
{
    public partial class templateForm : Form
    {
        public templateForm()
        {
            InitializeComponent();
            tabControl1.Dock = Dock;
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Selected += new TabControlEventHandler(Tabs_Selected);
            LoadTemplate();

        }
        static int index = 0;
        public void LoadTemplate()
        {
            foreach (string grid in Directory.GetFiles(settings.defaultSave))
            {
                if (grid.Contains("_grid")&&(grid.Contains(".csv")|| grid.Contains(".txt")))
                {
                    
                    BindingList<pathGridMember> pathGridMembers = new BindingList<pathGridMember>();
                    var array = grid.Split('\\');
                    tabControl1.TabPages.Add(array[array.Length - 1].ToString());
                    DataGridView dgv = new DataGridView();
                    Grid(dgv);
                    dgv.AutoGenerateColumns = false;
                    dgv.DataSource = pathGridMembers;
                    tabControl1.TabPages[tabControl1.TabCount - 1].Controls.Add(dgv);
                    using (StreamReader sr = new StreamReader(grid))
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
            }

            foreach(TabPage tab in tabControl1.TabPages)
            {
                Button delete = new Button();
                Button load = new Button();
                delete.Text = "Delete";
                load.Text = "Load";
                load.Click += new EventHandler(load_Click);
                delete.Click += new EventHandler(delete_Click);
                load.Location = new Point(10,   this.Height-100);
                delete.Location = new Point(100, this.Height-100);
                tab.Controls.Add(delete);
                tab.Controls.Add(load);
            }
        }
        public void Grid(DataGridView pathGrid)
        {

            pathGrid.AllowUserToAddRows = false;
            pathGrid.AllowUserToOrderColumns = true;
            pathGrid.AllowUserToOrderColumns = false;

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
            
            foreach (DataGridViewColumn c in pathGrid.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            pathGrid.Width = tabControl1.Size.Width;
            pathGrid.Height = tabControl1.Size.Height / 5 * 4;
            


        }
        private void load_Click(object sender, EventArgs e)
        {
             
        }
        private void delete_Click(object sender, EventArgs e)
        {
            
            tabControl1.TabPages.Remove(tabControl1.TabPages[index]);
        }
        private void Tabs_Selected(object sender, TabControlEventArgs e)
        {
            index = e.TabPageIndex;
        }


    }
}

