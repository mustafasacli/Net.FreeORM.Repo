namespace Net.FreeORM.SecureHash
{
    partial class FrmSecureHash
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
            this.tblLytMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPropName = new System.Windows.Forms.Label();
            this.lblConnStr = new System.Windows.Forms.Label();
            this.txtPropName = new System.Windows.Forms.TextBox();
            this.txtConnStr = new System.Windows.Forms.TextBox();
            this.lblConnectionType = new System.Windows.Forms.Label();
            this.cmbxConnectionType = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnHashString = new System.Windows.Forms.Button();
            this.txtHashedString = new System.Windows.Forms.RichTextBox();
            this.tblLytMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLytMain
            // 
            this.tblLytMain.ColumnCount = 1;
            this.tblLytMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytMain.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tblLytMain.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tblLytMain.Controls.Add(this.txtHashedString, 0, 1);
            this.tblLytMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytMain.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.tblLytMain.Location = new System.Drawing.Point(0, 0);
            this.tblLytMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblLytMain.Name = "tblLytMain";
            this.tblLytMain.RowCount = 3;
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblLytMain.Size = new System.Drawing.Size(554, 561);
            this.tblLytMain.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.lblPropName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblConnStr, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtPropName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtConnStr, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblConnectionType, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbxConnectionType, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(548, 112);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblPropName
            // 
            this.lblPropName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPropName.AutoSize = true;
            this.lblPropName.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblPropName.Location = new System.Drawing.Point(30, 47);
            this.lblPropName.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.lblPropName.Name = "lblPropName";
            this.lblPropName.Size = new System.Drawing.Size(119, 17);
            this.lblPropName.TabIndex = 0;
            this.lblPropName.Text = "Connection Name :";
            // 
            // lblConnStr
            // 
            this.lblConnStr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConnStr.AutoSize = true;
            this.lblConnStr.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblConnStr.Location = new System.Drawing.Point(30, 84);
            this.lblConnStr.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.lblConnStr.Name = "lblConnStr";
            this.lblConnStr.Size = new System.Drawing.Size(118, 17);
            this.lblConnStr.TabIndex = 1;
            this.lblConnStr.Text = "Connection String :";
            // 
            // txtPropName
            // 
            this.txtPropName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPropName.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtPropName.Location = new System.Drawing.Point(174, 43);
            this.txtPropName.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.txtPropName.Name = "txtPropName";
            this.txtPropName.Size = new System.Drawing.Size(215, 25);
            this.txtPropName.TabIndex = 2;
            // 
            // txtConnStr
            // 
            this.txtConnStr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtConnStr.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtConnStr.Location = new System.Drawing.Point(174, 80);
            this.txtConnStr.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.txtConnStr.Name = "txtConnStr";
            this.txtConnStr.Size = new System.Drawing.Size(364, 25);
            this.txtConnStr.TabIndex = 3;
            // 
            // lblConnectionType
            // 
            this.lblConnectionType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConnectionType.AutoSize = true;
            this.lblConnectionType.Location = new System.Drawing.Point(30, 10);
            this.lblConnectionType.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.lblConnectionType.Name = "lblConnectionType";
            this.lblConnectionType.Size = new System.Drawing.Size(112, 17);
            this.lblConnectionType.TabIndex = 4;
            this.lblConnectionType.Text = "Connection Type :";
            // 
            // cmbxConnectionType
            // 
            this.cmbxConnectionType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbxConnectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxConnectionType.FormattingEnabled = true;
            this.cmbxConnectionType.Location = new System.Drawing.Point(174, 8);
            this.cmbxConnectionType.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.cmbxConnectionType.Name = "cmbxConnectionType";
            this.cmbxConnectionType.Size = new System.Drawing.Size(215, 25);
            this.cmbxConnectionType.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnTestConnection, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnHashString, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 505);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(548, 52);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnTestConnection.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnTestConnection.Location = new System.Drawing.Point(358, 3);
            this.btnTestConnection.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(87, 46);
            this.btnTestConnection.TabIndex = 0;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.Test);
            // 
            // btnHashString
            // 
            this.btnHashString.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnHashString.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnHashString.Location = new System.Drawing.Point(465, 3);
            this.btnHashString.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnHashString.Name = "btnHashString";
            this.btnHashString.Size = new System.Drawing.Size(80, 46);
            this.btnHashString.TabIndex = 1;
            this.btnHashString.Text = "Build Property";
            this.btnHashString.UseVisualStyleBackColor = true;
            this.btnHashString.Click += new System.EventHandler(this.BuildProperties);
            // 
            // txtHashedString
            // 
            this.txtHashedString.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtHashedString.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtHashedString.Location = new System.Drawing.Point(8, 124);
            this.txtHashedString.Name = "txtHashedString";
            this.txtHashedString.ReadOnly = true;
            this.txtHashedString.Size = new System.Drawing.Size(538, 373);
            this.txtHashedString.TabIndex = 2;
            this.txtHashedString.Text = "";
            // 
            // FrmSecureHash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 561);
            this.Controls.Add(this.tblLytMain);
            this.MaximumSize = new System.Drawing.Size(570, 600);
            this.MinimumSize = new System.Drawing.Size(570, 600);
            this.Name = "FrmSecureHash";
            this.Text = "Secure Hash";
            this.tblLytMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLytMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblPropName;
        private System.Windows.Forms.Label lblConnStr;
        private System.Windows.Forms.TextBox txtPropName;
        private System.Windows.Forms.TextBox txtConnStr;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnHashString;
        private System.Windows.Forms.RichTextBox txtHashedString;
        private System.Windows.Forms.Label lblConnectionType;
        private System.Windows.Forms.ComboBox cmbxConnectionType;
    }
}