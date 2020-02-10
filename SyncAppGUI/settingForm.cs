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
    public partial class settingForm : Form
    {
        public settingForm()
        {
            InitializeComponent();
            SettingsGUI();
        }
        public void SettingsGUI()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "HH:mm";
            dateTimePicker1.ShowUpDown = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker1.MinDate = DateTime.MinValue;
            if (settings.syncType == settings.SyncTypes.TimeInterval.ToString())
            {
                dateTimePicker1.Value = settings.intervalDate;
                radiob_interval.Checked = true;
            }
            else dateTimePicker1.Value = new DateTime(year:DateTime.Now.Year,month:DateTime.Now.Month,day:DateTime.Now.Day,hour:00, minute: 00, second:00);
            
            dateTimePicker1.Hide();



            maskedTextBox1.Mask = "00:00";
            maskedTextBox1.TextMaskFormat = MaskFormat.IncludeLiterals;
            maskedTextBox1.SelectionStart = 0;
            maskedTextBox1.SelectionLength = 0;
            if (settings.syncType == settings.SyncTypes.SetTimes.ToString())
            {
                syncDateTime = settings.dateTimes.ConvertAll(x => Convert.ToString(x));
                syncDateTime.Select(x => x.Substring(x.Length - 6).Trim());
                foreach(string s in syncDateTime)
                {
                    int index = timeGrid.Rows.Add();
                    timeGrid.Rows[index].Cells[0].Value = s;

                }
                radiob_Set.Checked = true;

            }
            maskedTextBox1.Hide();
            
        }
        static List<string> syncDateTime = new List<string>();
        static string syncAt;
        private void radiob_Set_CheckedChanged(object sender, EventArgs e)
        {
            syncAt = radiob_Set.Text;
            dateTimePicker1.Hide();

            timeGrid.Show();
            buttonAdd.Show();
            maskedTextBox1.Show();

            label3.Text = "Synchronize at the following time(s):";
            
        }

        private void radiob_Interval_CheckedChanged(object sender, EventArgs e)
        {
            syncAt = radiob_interval.Text;
            dateTimePicker1.Show();

            timeGrid.Hide();
            buttonAdd.Hide();
            maskedTextBox1.Hide();

            label3.Text = "Synchronize at the following time intervals:";

        }

        private static bool Evaluate(string text)
        {
            Regex timeFormat = new Regex(@"^(?:[01][0-9]|2[0-3]):[0-5][0-9]$");
            if (timeFormat.IsMatch(text)) return true;
            else return false;
            
        }
        private void MaskedTextBox1_Validating(object sender, CancelEventArgs e)
        {
            if (maskedTextBox1.Text == "  :")
            {
                maskedTextBox1.BackColor = Color.White;
                maskedTextBox1.ForeColor = Color.Black;
                
            }
            else if (!Evaluate(maskedTextBox1.Text))
            {
                maskedTextBox1.BackColor = Color.Red;
                maskedTextBox1.ForeColor = Color.White;
            }
            else
            {
                maskedTextBox1.BackColor = Color.White;
                maskedTextBox1.ForeColor = Color.Black;
            }
        }

        private void MaskedTextBox1_Click(object sender, EventArgs e)
        {
            
            if (maskedTextBox1.MaskedTextProvider.LastAssignedPosition > -1)
            {
                maskedTextBox1.SelectionStart = maskedTextBox1.MaskedTextProvider.LastAssignedPosition+1;
                maskedTextBox1.SelectionLength = 0;
            }
            else
            {
                maskedTextBox1.SelectionStart = 0;
                maskedTextBox1.SelectionLength = 0;
            }
        }

        private void MaskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (maskedTextBox1.MaskedTextProvider.LastAssignedPosition - maskedTextBox1.SelectionStart < 1 && maskedTextBox1.MaskedTextProvider.LastAssignedPosition>=0)
                {
                    maskedTextBox1.Select(maskedTextBox1.MaskedTextProvider.LastAssignedPosition, 0);
                }
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (Evaluate(maskedTextBox1.Text)&&syncDateTime.Contains(maskedTextBox1.Text)==false)
            {
                int index = timeGrid.Rows.Add();
                timeGrid.Rows[index].Cells[0].Value = maskedTextBox1.Text;
                
                syncDateTime.Add(maskedTextBox1.Text);
                timeGrid.EndEdit();
            }
            
            
        }

        private void TimeGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                
                if (e.ColumnIndex == timeGrid.Rows[e.RowIndex].Cells["Delete"].ColumnIndex)
                {
                    syncDateTime.Remove(timeGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                    timeGrid.Rows.Remove(timeGrid.Rows[e.RowIndex]);

                }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (radiob_interval.Checked == true)
            {
                settings.syncType = settings.SyncTypes.TimeInterval.ToString();
                settings.interval = dateTimePicker1.Value.Hour * 60 * 60 * 1000 + dateTimePicker1.Value.Minute * 60 * 1000;
                settings.intervalDate = dateTimePicker1.Value.Date;
            }
            else
            {
                settings.syncType = settings.SyncTypes.SetTimes.ToString();
                settings.dateTimes =syncDateTime.ConvertAll(x => DateTime.ParseExact(x, "HH:mm", null));
            }
        }
    }
}
