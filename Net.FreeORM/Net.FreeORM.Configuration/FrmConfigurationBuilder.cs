using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Net.FreeORM.Configuration
{
    public partial class FrmConfigurationBuilder : Form
    {
        public FrmConfigurationBuilder()
        {
            InitializeComponent();
        }

        private void trVwConf_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MessageBox.Show(e.Node.Text);
        }
    }
}