namespace Last.Tools.Operator
{
    partial class MainWindow
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
            this.tabControlPlugins = new System.Windows.Forms.TabControl();
            this.listViewLog = new System.Windows.Forms.ListView();
            this.columnHeaderDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLogLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // tabControlPlugins
            // 
            this.tabControlPlugins.Location = new System.Drawing.Point(12, 12);
            this.tabControlPlugins.Name = "tabControlPlugins";
            this.tabControlPlugins.SelectedIndex = 0;
            this.tabControlPlugins.Size = new System.Drawing.Size(1240, 501);
            this.tabControlPlugins.TabIndex = 0;
            // 
            // listViewLog
            // 
            this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderDateTime,
            this.columnHeaderLogLevel,
            this.columnHeaderMessage});
            this.listViewLog.Location = new System.Drawing.Point(12, 519);
            this.listViewLog.Name = "listViewLog";
            this.listViewLog.Size = new System.Drawing.Size(1240, 150);
            this.listViewLog.TabIndex = 1;
            this.listViewLog.UseCompatibleStateImageBehavior = false;
            this.listViewLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderDateTime
            // 
            this.columnHeaderDateTime.Text = "DateTime";
            this.columnHeaderDateTime.Width = 100;
            // 
            // columnHeaderLogLevel
            // 
            this.columnHeaderLogLevel.Text = "LogLevel";
            this.columnHeaderLogLevel.Width = 75;
            // 
            // columnHeaderMessage
            // 
            this.columnHeaderMessage.Text = "Message";
            this.columnHeaderMessage.Width = 1000;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.listViewLog);
            this.Controls.Add(this.tabControlPlugins);
            this.Name = "MainWindow";
            this.Text = "Last.Tools.Operator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlPlugins;
        private System.Windows.Forms.ListView listViewLog;
        private System.Windows.Forms.ColumnHeader columnHeaderDateTime;
        private System.Windows.Forms.ColumnHeader columnHeaderLogLevel;
        private System.Windows.Forms.ColumnHeader columnHeaderMessage;
    }
}

