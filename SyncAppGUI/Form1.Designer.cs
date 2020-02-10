namespace SyncAppGUI
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.pathGrid = new System.Windows.Forms.DataGridView();
            this.addButton = new System.Windows.Forms.Button();
            this.textSource = new System.Windows.Forms.TextBox();
            this.textTarget = new System.Windows.Forms.TextBox();
            this.browseSource = new System.Windows.Forms.Button();
            this.browseTarget = new System.Windows.Forms.Button();
            this.swapButton = new System.Windows.Forms.Button();
            this.labelSource = new System.Windows.Forms.Label();
            this.labelTarget = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.syncNowButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonSave = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonOpen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pathGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pathGrid
            // 
            this.pathGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pathGrid.Location = new System.Drawing.Point(149, 184);
            this.pathGrid.Name = "pathGrid";
            this.pathGrid.Size = new System.Drawing.Size(462, 150);
            this.pathGrid.TabIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(149, 155);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(26, 20);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // textSource
            // 
            this.textSource.AllowDrop = true;
            this.textSource.Location = new System.Drawing.Point(181, 155);
            this.textSource.Name = "textSource";
            this.textSource.Size = new System.Drawing.Size(100, 20);
            this.textSource.TabIndex = 2;
            this.textSource.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextSource_DragDrop);
            this.textSource.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextSource_DragEnter);
            this.textSource.Validating += new System.ComponentModel.CancelEventHandler(this.textSource_Validating);
            // 
            // textTarget
            // 
            this.textTarget.AllowDrop = true;
            this.textTarget.Location = new System.Drawing.Point(445, 154);
            this.textTarget.Name = "textTarget";
            this.textTarget.Size = new System.Drawing.Size(100, 20);
            this.textTarget.TabIndex = 3;
            this.textTarget.DragDrop += new System.Windows.Forms.DragEventHandler(this.textTarget_DragDrop);
            this.textTarget.DragEnter += new System.Windows.Forms.DragEventHandler(this.textTarget_DragEnter);
            this.textTarget.Validating += new System.ComponentModel.CancelEventHandler(this.textTarget_Validating);
            // 
            // browseSource
            // 
            this.browseSource.Location = new System.Drawing.Point(287, 154);
            this.browseSource.Name = "browseSource";
            this.browseSource.Size = new System.Drawing.Size(56, 21);
            this.browseSource.TabIndex = 4;
            this.browseSource.Text = "Browse";
            this.browseSource.UseVisualStyleBackColor = true;
            this.browseSource.Click += new System.EventHandler(this.BrowseSource_Click);
            // 
            // browseTarget
            // 
            this.browseTarget.Location = new System.Drawing.Point(551, 154);
            this.browseTarget.Name = "browseTarget";
            this.browseTarget.Size = new System.Drawing.Size(60, 21);
            this.browseTarget.TabIndex = 5;
            this.browseTarget.Text = "Browse";
            this.browseTarget.UseVisualStyleBackColor = true;
            this.browseTarget.Click += new System.EventHandler(this.BrowseTarget_Click);
            // 
            // swapButton
            // 
            this.swapButton.Location = new System.Drawing.Point(361, 154);
            this.swapButton.Name = "swapButton";
            this.swapButton.Size = new System.Drawing.Size(63, 24);
            this.swapButton.TabIndex = 6;
            this.swapButton.Text = "Swap";
            this.swapButton.UseVisualStyleBackColor = true;
            this.swapButton.Click += new System.EventHandler(this.SwapButton_Click);
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Location = new System.Drawing.Point(182, 136);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(0, 13);
            this.labelSource.TabIndex = 7;
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(445, 135);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(0, 13);
            this.labelTarget.TabIndex = 8;
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Location = new System.Drawing.Point(147, 135);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(0, 13);
            this.labelError.TabIndex = 9;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(150, 369);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(461, 23);
            this.clearButton.TabIndex = 10;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // syncNowButton
            // 
            this.syncNowButton.Location = new System.Drawing.Point(287, 61);
            this.syncNowButton.Name = "syncNowButton";
            this.syncNowButton.Size = new System.Drawing.Size(137, 53);
            this.syncNowButton.TabIndex = 11;
            this.syncNowButton.Text = "SyncNow";
            this.syncNowButton.UseVisualStyleBackColor = true;
            this.syncNowButton.Click += new System.EventHandler(this.syncNowButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadoutToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.fileToolStripMenuItem.Text = "Options";
            // 
            // loadoutToolStripMenuItem
            // 
            this.loadoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.loadoutToolStripMenuItem.Name = "loadoutToolStripMenuItem";
            this.loadoutToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.loadoutToolStripMenuItem.Text = "Loadout";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(618, 184);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.Text = "SavePaths";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(618, 214);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 14;
            this.buttonOpen.Text = "Open Paths";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.ButtonOpen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.syncNowButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.labelSource);
            this.Controls.Add(this.swapButton);
            this.Controls.Add(this.browseTarget);
            this.Controls.Add(this.browseSource);
            this.Controls.Add(this.textTarget);
            this.Controls.Add(this.textSource);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.pathGrid);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pathGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView pathGrid;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TextBox textSource;
        private System.Windows.Forms.TextBox textTarget;
        private System.Windows.Forms.Button browseSource;
        private System.Windows.Forms.Button browseTarget;
        private System.Windows.Forms.Button swapButton;
        private System.Windows.Forms.Label labelSource;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button syncNowButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonOpen;
    }
}

