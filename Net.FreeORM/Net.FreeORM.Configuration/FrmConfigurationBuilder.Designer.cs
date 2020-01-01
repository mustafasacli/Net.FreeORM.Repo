namespace Net.FreeORM.Configuration
{
    partial class FrmConfigurationBuilder
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Root");
            this.tblLytMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblFileStruct = new System.Windows.Forms.Label();
            this.tblLytSave = new System.Windows.Forms.TableLayoutPanel();
            this.btnSavingPath = new System.Windows.Forms.Button();
            this.txtSavingPath = new System.Windows.Forms.TextBox();
            this.lblSavingPath = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbCtrlConfFile = new System.Windows.Forms.TabControl();
            this.tbPgConFile = new System.Windows.Forms.TabPage();
            this.trVwConf = new System.Windows.Forms.TreeView();
            this.tbPgLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.tblLytMain.SuspendLayout();
            this.tblLytSave.SuspendLayout();
            this.tbCtrlConfFile.SuspendLayout();
            this.tbPgConFile.SuspendLayout();
            this.tbPgLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLytMain
            // 
            this.tblLytMain.ColumnCount = 1;
            this.tblLytMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytMain.Controls.Add(this.lblFileStruct, 0, 0);
            this.tblLytMain.Controls.Add(this.tblLytSave, 0, 2);
            this.tblLytMain.Controls.Add(this.btnSave, 0, 3);
            this.tblLytMain.Controls.Add(this.tbCtrlConfFile, 0, 1);
            this.tblLytMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytMain.Location = new System.Drawing.Point(0, 0);
            this.tblLytMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblLytMain.Name = "tblLytMain";
            this.tblLytMain.RowCount = 4;
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tblLytMain.Size = new System.Drawing.Size(664, 646);
            this.tblLytMain.TabIndex = 0;
            // 
            // lblFileStruct
            // 
            this.lblFileStruct.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFileStruct.AutoSize = true;
            this.lblFileStruct.Location = new System.Drawing.Point(3, 7);
            this.lblFileStruct.Name = "lblFileStruct";
            this.lblFileStruct.Size = new System.Drawing.Size(75, 16);
            this.lblFileStruct.TabIndex = 1;
            this.lblFileStruct.Text = "File Struct :";
            // 
            // tblLytSave
            // 
            this.tblLytSave.ColumnCount = 3;
            this.tblLytSave.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLytSave.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytSave.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLytSave.Controls.Add(this.btnSavingPath, 2, 0);
            this.tblLytSave.Controls.Add(this.txtSavingPath, 1, 0);
            this.tblLytSave.Controls.Add(this.lblSavingPath, 0, 0);
            this.tblLytSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytSave.Location = new System.Drawing.Point(3, 525);
            this.tblLytSave.Name = "tblLytSave";
            this.tblLytSave.RowCount = 1;
            this.tblLytSave.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytSave.Size = new System.Drawing.Size(658, 56);
            this.tblLytSave.TabIndex = 2;
            // 
            // btnSavingPath
            // 
            this.btnSavingPath.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSavingPath.Location = new System.Drawing.Point(547, 5);
            this.btnSavingPath.Name = "btnSavingPath";
            this.btnSavingPath.Size = new System.Drawing.Size(101, 45);
            this.btnSavingPath.TabIndex = 0;
            this.btnSavingPath.Text = "Saving Path";
            this.btnSavingPath.UseVisualStyleBackColor = true;
            // 
            // txtSavingPath
            // 
            this.txtSavingPath.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSavingPath.Font = new System.Drawing.Font("Tahoma", 10.75F);
            this.txtSavingPath.Location = new System.Drawing.Point(123, 15);
            this.txtSavingPath.Name = "txtSavingPath";
            this.txtSavingPath.ReadOnly = true;
            this.txtSavingPath.Size = new System.Drawing.Size(412, 25);
            this.txtSavingPath.TabIndex = 1;
            // 
            // lblSavingPath
            // 
            this.lblSavingPath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSavingPath.AutoSize = true;
            this.lblSavingPath.Location = new System.Drawing.Point(10, 20);
            this.lblSavingPath.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblSavingPath.Name = "lblSavingPath";
            this.lblSavingPath.Size = new System.Drawing.Size(84, 16);
            this.lblSavingPath.TabIndex = 2;
            this.lblSavingPath.Text = "Saving Path :";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.Location = new System.Drawing.Point(271, 587);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(122, 56);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // tbCtrlConfFile
            // 
            this.tbCtrlConfFile.Controls.Add(this.tbPgConFile);
            this.tbCtrlConfFile.Controls.Add(this.tbPgLog);
            this.tbCtrlConfFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCtrlConfFile.Location = new System.Drawing.Point(3, 33);
            this.tbCtrlConfFile.Name = "tbCtrlConfFile";
            this.tbCtrlConfFile.SelectedIndex = 0;
            this.tbCtrlConfFile.Size = new System.Drawing.Size(658, 486);
            this.tbCtrlConfFile.TabIndex = 4;
            // 
            // tbPgConFile
            // 
            this.tbPgConFile.Controls.Add(this.trVwConf);
            this.tbPgConFile.Location = new System.Drawing.Point(4, 25);
            this.tbPgConFile.Name = "tbPgConFile";
            this.tbPgConFile.Padding = new System.Windows.Forms.Padding(3);
            this.tbPgConFile.Size = new System.Drawing.Size(650, 457);
            this.tbPgConFile.TabIndex = 0;
            this.tbPgConFile.Text = "Conf File Struct";
            this.tbPgConFile.UseVisualStyleBackColor = true;
            // 
            // trVwConf
            // 
            this.trVwConf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trVwConf.Location = new System.Drawing.Point(3, 3);
            this.trVwConf.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trVwConf.Name = "trVwConf";
            treeNode1.Name = "nodeRoot";
            treeNode1.Text = "Root";
            this.trVwConf.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.trVwConf.Size = new System.Drawing.Size(644, 451);
            this.trVwConf.TabIndex = 0;
            this.trVwConf.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trVwConf_NodeMouseDoubleClick);
            // 
            // tbPgLog
            // 
            this.tbPgLog.Controls.Add(this.txtLog);
            this.tbPgLog.Location = new System.Drawing.Point(4, 25);
            this.tbPgLog.Name = "tbPgLog";
            this.tbPgLog.Padding = new System.Windows.Forms.Padding(3);
            this.tbPgLog.Size = new System.Drawing.Size(650, 457);
            this.tbPgLog.TabIndex = 1;
            this.tbPgLog.Text = "Log";
            this.tbPgLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(644, 451);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // FrmConfigurationBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 646);
            this.Controls.Add(this.tblLytMain);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(680, 685);
            this.MinimumSize = new System.Drawing.Size(680, 685);
            this.Name = "FrmConfigurationBuilder";
            this.Text = "Configuration File Builder";
            this.tblLytMain.ResumeLayout(false);
            this.tblLytMain.PerformLayout();
            this.tblLytSave.ResumeLayout(false);
            this.tblLytSave.PerformLayout();
            this.tbCtrlConfFile.ResumeLayout(false);
            this.tbPgConFile.ResumeLayout(false);
            this.tbPgLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLytMain;
        private System.Windows.Forms.TreeView trVwConf;
        private System.Windows.Forms.Label lblFileStruct;
        private System.Windows.Forms.TableLayoutPanel tblLytSave;
        private System.Windows.Forms.Button btnSavingPath;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabControl tbCtrlConfFile;
        private System.Windows.Forms.TabPage tbPgConFile;
        private System.Windows.Forms.TabPage tbPgLog;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.TextBox txtSavingPath;
        private System.Windows.Forms.Label lblSavingPath;
    }
}

