using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Net.FreeORM.Extensions
{
    public static class ControlExtensions
    {

        public static void LoadDataTable(this ComboBox cmbx, DataTable dt,
            string displayMember, string valueMember)
        {
            try
            {
                cmbx.DataSource = null;
                cmbx.DataSource = dt;
                cmbx.DisplayMember = displayMember;
                cmbx.ValueMember = valueMember;
                cmbx.Refresh();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void LoadList<T>(this ComboBox cmbx, List<T> list,
            string displayMember, string valueMember) where T : new()
        {
            try
            {
                cmbx.DisplayMember = displayMember;
                cmbx.ValueMember = valueMember;
                cmbx.DataSource = list;
                cmbx.Refresh();
                cmbx.SelectedIndex = -1;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
