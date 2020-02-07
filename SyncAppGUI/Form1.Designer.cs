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
            this.asasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ddadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fsfsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pathGrid)).BeginInit();
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
            // asasToolStripMenuItem
            // 
            this.asasToolStripMenuItem.Name = "asasToolStripMenuItem";
            this.asasToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // ddadToolStripMenuItem
            // 
            this.ddadToolStripMenuItem.Name = "ddadToolStripMenuItem";
            this.ddadToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // sdToolStripMenuItem
            // 
            this.sdToolStripMenuItem.Name = "sdToolStripMenuItem";
            this.sdToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // fsfsToolStripMenuItem
            // 
            this.fsfsToolStripMenuItem.Name = "fsfsToolStripMenuItem";
            this.fsfsToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(150, 340);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(461, 23);
            this.clearButton.TabIndex = 10;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pathGrid)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem asasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ddadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fsfsToolStripMenuItem;
        private System.Windows.Forms.Button clearButton;
    }
}

