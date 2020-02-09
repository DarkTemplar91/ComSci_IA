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
    public partial class settingForm : Form
    {
        public settingForm()
        {
            InitializeComponent();
        }
        public void SettingsGUI()
        {

        }
        static string syncAt;
        private void radiob_Set_CheckedChanged(object sender, EventArgs e)
        {
            syncAt = radiob_Set.Text;
        }

        private void radiob_Interval_CheckedChanged(object sender, EventArgs e)
        {
            syncAt = radiob_interval.Text;
        }
        ~settingForm()
        {
            Form1 form = new Form1();
            form.Show();
        }
    }
}
