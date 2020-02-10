namespace SyncAppGUI
{
    partial class settingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radiob_Set = new System.Windows.Forms.RadioButton();
            this.radiob_interval = new System.Windows.Forms.RadioButton();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.timeGrid = new System.Windows.Forms.DataGridView();
            this.SyncTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.backButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.timeGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label2.Location = new System.Drawing.Point(14, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(295, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "If AutoSync is off synchronization happens at:";
            // 
            // radiob_Set
            // 
            this.radiob_Set.AutoSize = true;
            this.radiob_Set.Location = new System.Drawing.Point(17, 64);
            this.radiob_Set.Name = "radiob_Set";
            this.radiob_Set.Size = new System.Drawing.Size(63, 17);
            this.radiob_Set.TabIndex = 2;
            this.radiob_Set.TabStop = true;
            this.radiob_Set.Text = "Set time";
            this.radiob_Set.UseVisualStyleBackColor = true;
            this.radiob_Set.CheckedChanged += new System.EventHandler(this.radiob_Set_CheckedChanged);
            // 
            // radiob_interval
            // 
            this.radiob_interval.AutoSize = true;
            this.radiob_interval.Location = new System.Drawing.Point(86, 64);
            this.radiob_interval.Name = "radiob_interval";
            this.radiob_interval.Size = new System.Drawing.Size(85, 17);
            this.radiob_interval.TabIndex = 2;
            this.radiob_interval.TabStop = true;
            this.radiob_interval.Text = "Time interval";
            this.radiob_interval.UseVisualStyleBackColor = true;
            this.radiob_interval.CheckedChanged += new System.EventHandler(this.radiob_Interval_CheckedChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 119);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(84, 20);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // timeGrid
            // 
            this.timeGrid.AllowUserToAddRows = false;
            this.timeGrid.AllowUserToDeleteRows = false;
            this.timeGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.timeGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SyncTime,
            this.Delete});
            this.timeGrid.Location = new System.Drawing.Point(12, 145);
            this.timeGrid.Name = "timeGrid";
            this.timeGrid.ReadOnly = true;
            this.timeGrid.Size = new System.Drawing.Size(243, 82);
            this.timeGrid.TabIndex = 4;
            this.timeGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TimeGrid_CellContentClick);
            // 
            // SyncTime
            // 
            this.SyncTime.HeaderText = "Sync Time";
            this.SyncTime.Name = "SyncTime";
            this.SyncTime.ReadOnly = true;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(124, 119);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(24, 20);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "button1";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(16, 119);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(100, 20);
            this.maskedTextBox1.TabIndex = 7;
            this.maskedTextBox1.Click += new System.EventHandler(this.MaskedTextBox1_Click);
            this.maskedTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaskedTextBox1_KeyDown);
            this.maskedTextBox1.Validating += new System.ComponentModel.CancelEventHandler(this.MaskedTextBox1_Validating);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(16, 233);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 8;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(180, 233);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Save at every:";
            // 
            // settingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 272);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.timeGrid);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.radiob_interval);
            this.Controls.Add(this.radiob_Set);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "settingForm";
            this.Text = "settingForm";
            ((System.ComponentModel.ISupportInitialize)(this.timeGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radiob_Set;
        private System.Windows.Forms.RadioButton radiob_interval;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView timeGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn SyncTime;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label3;
    }
}