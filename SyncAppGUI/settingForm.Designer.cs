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
            this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Minion Pro", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Synchronization happens at:";
            // 
            // radiob_Set
            // 
            this.radiob_Set.AutoSize = true;
            this.radiob_Set.Location = new System.Drawing.Point(17, 83);
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
            this.radiob_interval.Location = new System.Drawing.Point(108, 83);
            this.radiob_interval.Name = "radiob_interval";
            this.radiob_interval.Size = new System.Drawing.Size(85, 17);
            this.radiob_interval.TabIndex = 2;
            this.radiob_interval.TabStop = true;
            this.radiob_interval.Text = "Time interval";
            this.radiob_interval.UseVisualStyleBackColor = true;
            this.radiob_interval.CheckedChanged += new System.EventHandler(this.radiob_Interval_CheckedChanged);
            // 
            // domainUpDown1
            // 
            this.domainUpDown1.Location = new System.Drawing.Point(17, 119);
            this.domainUpDown1.Name = "domainUpDown1";
            this.domainUpDown1.Size = new System.Drawing.Size(120, 20);
            this.domainUpDown1.TabIndex = 3;
            this.domainUpDown1.Text = "domainUpDown1";
            // 
            // settingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.domainUpDown1);
            this.Controls.Add(this.radiob_interval);
            this.Controls.Add(this.radiob_Set);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "settingForm";
            this.Text = "settingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radiob_Set;
        private System.Windows.Forms.RadioButton radiob_interval;
        private System.Windows.Forms.DomainUpDown domainUpDown1;
    }
}