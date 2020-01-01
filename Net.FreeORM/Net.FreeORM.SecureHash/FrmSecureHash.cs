using Net.FreeORM.SecureHash.Property;
using Net.FreeORM.SecureHash.Test;
using System;
using System.Windows.Forms;

namespace Net.FreeORM.SecureHash
{
    public partial class FrmSecureHash : Form
    {
        public static string Error { get; set; }
        public FrmSecureHash()
        {
            try
            {
                InitializeComponent();
                cmbxConnectionType.DataSource = Enum.GetValues(typeof(ConnectionTypes));
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Server=KRK\SQLEXPRESS; Initial Catalog=GuruTest;User Id=sa; Password=123123;
        private void Test(object sender, EventArgs e)
        {
            if (cmbxConnectionType.SelectedIndex == -1)
            {
                MessageBox.Show("Please, Choose a connectionType.");
                return;
            }

            if (true == string.IsNullOrWhiteSpace(txtConnStr.Text))
            {

                MessageBox.Show(
                    "Connection string can not be empty.",
                    "Warning!..");
                return;
            }

            ConnectionTypes connType = (ConnectionTypes)cmbxConnectionType.Items[cmbxConnectionType.SelectedIndex];
            ConnectionTester.TestConnection(connType, txtConnStr.Text);
            MessageBox.Show(string.Format("{0}", Error), "Result");
        }

        private void BuildProperties(object sender, EventArgs e)
        {
            try
            {
                if (cmbxConnectionType.SelectedIndex == -1)
                {
                    MessageBox.Show("Please, Choose a connectionType.");
                    return;
                }

                if (true == string.IsNullOrWhiteSpace(txtConnStr.Text))
                {
                    MessageBox.Show(
                       "Connection string can not be empty.",
                       "Warning!..");
                    return;
                }


                if (true == string.IsNullOrWhiteSpace(txtPropName.Text))
                {
                    MessageBox.Show(
                        "Connection Name can not be empty.",
                       "Warning!..");
                    return;
                }

                ConnectionTypes connType = (ConnectionTypes)cmbxConnectionType.Items[cmbxConnectionType.SelectedIndex];
                string propStr = PropertyBuilder.Build(connType, txtPropName.Text, txtConnStr.Text);
                txtHashedString.ResetText();
                txtHashedString.AppendText(propStr);
            }
            catch (Exception exc)
            {
                txtHashedString.ResetText();
                txtHashedString.AppendText(exc.Message + "\n" + exc.StackTrace);
            }
        }

    }
}