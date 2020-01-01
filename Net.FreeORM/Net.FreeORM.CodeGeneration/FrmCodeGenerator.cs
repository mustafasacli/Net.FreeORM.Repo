namespace Net.FreeORM.CodeGeneration
{
    using Net.FreeORM.CodeGeneration.Source.BO;
    using Net.FreeORM.CodeGeneration.Source.Configuration;
    using Net.FreeORM.CodeGeneration.Source.Enumeration;
    using Net.FreeORM.CodeGeneration.Source.Generate;
    using Net.FreeORM.CodeGeneration.Source.Printing;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;

    public partial class FrmCodeGenerator : Form
    {
        #region [ Private Fields ]

        CodeGenerator tableGenerator;
        Printer clsPrint;
        private bool _isLstCheckTableBusy = false;
        private bool _isChkSelectAllBusy = false;

        List<Table> allTables;

        #endregion


        #region [ FrmCodeGenerator Ctor ]

        public FrmCodeGenerator()
        {
            InitializeComponent();
            allTables = new List<Table>();
            tableGenerator = new CodeGenerator();
            clsPrint = new Printer();
        }

        #endregion


        #region [ Form Load - 0 ]

        private void FormLoad(object sender, EventArgs e)
        {
            LoadCombo();
        }

        #endregion


        #region [ LoadCombo method ]

        private void LoadCombo()
        {
            try
            {
                cmbConnectionType.Items.Clear();
                foreach (var item in Enum.GetValues(typeof(ConnectionTypes)))
                {
                    cmbConnectionType.Items.Add(item);
                }
                cmbConnectionType.Refresh();
                /*
                cmbConnectionType.DataSource = new string[]{
        "SqlExpress","SqlServer","EnterpriseDB","DB2",
        "Oracle","MySQL","OleDb",
        "SQL-Lite","FireBird"
        };
                */
                //AppConfiguration.Settings.ConnTypes;
            }
            catch (Exception exc)
            {
                LogException(exc, 0);
            }
        }

        #endregion


        #region [ txtNameSpace Text Changing Event ]

        private void TxtNameSpaceTextChangeEvent(object sender, EventArgs e)
        {
            this.clsPrint.NameSpace = this.txtNameSpace.Text;
        }

        #endregion


        #region [ Method of Selected Index Changed of cmbConnectionTypes - 1 ]

        private void CmbConnectionTypeIndexChange(object sender, EventArgs e)
        {
            try
            {
                tableGenerator.ConnType = (ConnectionTypes)cmbConnectionType.SelectedItem;

                if (this.cmbConnectionType.SelectedIndex != -1)
                {
                    this.txtConnectionString.Text = AppConfiguration.Settings[
                        this.cmbConnectionType.SelectedItem.ToString()].ToString();
                }
                else
                    this.txtConnectionString.Text = string.Empty;
            }
            catch (Exception exc)
            {
                this.LogException(exc, 1);
            }
            finally
            {
                tableGenerator.ConnectionString = this.txtConnectionString.Text;
                CodeGenerator.ConnStr = this.txtConnectionString.Text;
                CodeGenerator.Index = this.cmbConnectionType.SelectedIndex;
            }
        }

        #endregion


        #region [ btnBrowse Click event - 2 ]

        private void Browse(object sender, EventArgs e)
        {
            try
            {
                browserDestinationDialog.Description = "Kaydedilecek klasör";
                DialogResult dialogRes = browserDestinationDialog.ShowDialog();

                if (dialogRes == DialogResult.OK)
                {
                    clsPrint.SavingPath = browserDestinationDialog.SelectedPath;
                    txtSaveToPath.Text = clsPrint.SavingPath;
                }

            }
            catch (Exception exc)
            {
                tbpgLog.Select();
                LogException(exc, 2);
            }
        }

        #endregion


        #region [ btnGenerate Click event - 3 ]

        private void Generate(object sender, EventArgs e)
        {
            try
            {
                if (NameSpaceController.IsNullOrSpace(clsPrint.SavingPath))
                {
                    MessageBox.Show("Saving Path is not empty.");
                    return;
                }

                if (!NameSpaceController.ControlNameSpace(clsPrint.NameSpace))
                {
                    MessageBox.Show("NameSpace includes forbidden characters.");
                    return;
                }
                chklstTables.Enabled = false;
                Generate();
                MessageBox.Show("All Tables created.", "Result Info:");
            }
            catch (Exception exc)
            {
                tbpgLog.Select();
                LogException(exc, 3);
            }
            finally
            {
                chklstTables.Enabled = true;
            }
        }


        #endregion


        #region [ Generate method ]

        private void Generate()
        {
            try
            {
                List<string> tableNames = new List<string>();
                foreach (var item in chklstTables.CheckedItems)
                {
                    tableNames.Add(item.ToString());
                }

                List<Table> tables = new List<Table>();
                Table tbl;

                for (int tableCounter = 0; tableCounter < allTables.Count; tableCounter++)
                {
                    if (tableNames.Contains(allTables[tableCounter].TableName))
                    {
                        tbl = allTables[tableCounter];
                        tbl.NameSpace = clsPrint.NameSpace;
                        tables.Add(tbl);
                    }
                }

                /*
                for (int tableCounter = 0; tableCounter < tables.Count; tableCounter++)
                {
                    tbl.TableColumns = tableGenerator.GetColumnsOfTable(tbl.TableName);
                    tables[tableCounter] = tbl;
                }
                for (int tableCounter = 0; tableCounter < tables.Count; tableCounter++)
                {
                    tbl = tables[tableCounter];
                    tbl.IdColumn = tableGenerator.GetIdColumnOfTable(tables[tableCounter].TableName);
                    tables[tableCounter] = tbl;
                }
                */
                clsPrint.PrintClassTable(tables);
                StringBuilder sBuilder = new StringBuilder();
                foreach (Table cls in tables)
                {
                    sBuilder.AppendFormat("{0},\n", cls.TableName);
                }
                StringBuilder newBuilder = new StringBuilder(
                    sBuilder.ToString().Substring(0, sBuilder.ToString().Length - 1));
                newBuilder.AppendLine(" tables created.");
                txtLog.AppendText(newBuilder.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ btnGetTables Click event ]

        private void GetTables(object sender, EventArgs e)
        {
            allTables = new List<Table>();
            try
            {
                this.clsPrint.SavingPath = string.Empty;
                this.chklstTables.Items.Clear();
                allTables = tableGenerator.GetTablesAndColumns(tableGenerator.ConnType, tableGenerator.ConnectionString); //tableGenerator.GetTableList();
            }
            catch (Exception exc)
            {
                this.tbpgLog.Select();
                this.LogException(exc, 5);
            }
            finally
            {
                WriteTables();
            }
        }

        #endregion


        #region [ WriteTables method ]

        private void WriteTables()
        {
            foreach (Table clazztable in allTables)
            {
                this.chklstTables.Items.Add(clazztable.TableName, CheckState.Checked);
            }
        }

        #endregion


        #region [ LogException method ]

        private void LogException(Exception exc, int exceptionType)
        {
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat(
                    "\nException Date: {0}\n", DateTime.Now.ToString("hh:mm:ss dd.MM.yyyy")).AppendFormat(
                    "Exception Type: {0}\n", exceptionType).AppendFormat(
                    "Exception Message: {0}\n", exc.Message).AppendFormat(
                    "Exception Stack Trace: {0}\n", exc.StackTrace);

                txtLog.AppendText(strBuilder.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "An Error has occured.");
            }
        }

        #endregion


        #region [ Save Settings - 5 ]

        private void SaveSettings(object sender, EventArgs e)
        {
            try
            {
                int index = cmbConnectionType.SelectedIndex;
                if (index != -1)
                {
                    clsPrint.SavingPath = string.Empty;
                    //settin
                    AppConfiguration.Settings.SaveSetting(
                        cmbConnectionType.SelectedItem.ToString(), txtConnectionString.Text);
                    // tableGenerator.ConnectionString = txtConnectionString.Text;
                    LoadCombo();
                    cmbConnectionType.SelectedIndex = -1;
                    cmbConnectionType.SelectedIndex = index;
                }
            }
            catch (Exception ex)
            {
                tbpgLog.Select();
                LogException(ex, 5);
            }
        }

        #endregion


        #region [ Select - DeSelect All - 6 ]

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLstCheckTableBusy == true)
                    return;

                _isChkSelectAllBusy = true;
                if (chklstTables.Items == null)
                    return;

                if (chklstTables.Items.Count == 0)
                    return;

                for (int itemCounter = 0; itemCounter < chklstTables.Items.Count; itemCounter++)
                {
                    chklstTables.SetItemChecked(itemCounter, chkSelectAll.Checked);
                }
            }
            catch (Exception ex)
            {
                tbpgLog.Select();
                LogException(ex, 6);
            }
            finally
            {
                _isChkSelectAllBusy = false;
            }
        }

        #endregion


        #region [ chklstTables_ItemCheck method - 7 ]

        private void chklstTables_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            try
            {
                if (_isChkSelectAllBusy == true)
                    return;

                _isLstCheckTableBusy = true;

                if (chklstTables.SelectedItems.Count == chklstTables.Items.Count)
                {
                    chkSelectAll.CheckState = CheckState.Checked;
                    return;
                }

                if (chklstTables.SelectedItems.Count == 0)
                {
                    chkSelectAll.CheckState = CheckState.Unchecked;
                    return;
                }

                chkSelectAll.CheckState = CheckState.Indeterminate;
            }
            catch (Exception ex)
            {
                tbpgLog.Select();
                LogException(ex, 7);
            }
            finally
            {
                _isLstCheckTableBusy = false;
            }
        }

        #endregion

    }

    internal static class NameSpaceController
    {

        public static bool ControlNameSpace(string _nameSpace)
        {
            try
            {
                if (IsNullOrSpace(_nameSpace) == false)
                {
                    char[] chStr = _nameSpace.ToCharArray();
                    int ii = (int)chStr[0];

                    if (ii > 47 && ii < 58)
                        return false;

                    bool retBool = true;
                    foreach (var ch in chStr)
                    {
                        int i = (int)ch;
                        retBool &=
                            ((i > 47 && i < 58) ||
                             (i > 64 && i < 91) ||
                             (i > 96 && i < 123) ||
                             (i == 95) || (i == 46));
                    }
                    return retBool;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static bool IsNullOrSpace(string str)
        {
            if (str == null)
                return true;
            else
                return str.Trim().Length == 0;
        }
    }
}