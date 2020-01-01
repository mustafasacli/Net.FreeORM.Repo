using System.Windows.Forms;

namespace Net.FreeORM.Windows.TextControl
{
    /// <summary>
    /// Description of FreeNumericTextBox.
    /// </summary>
    public class FreeNumericTextBox : TextBox
    {
        public FreeNumericTextBox()
            : base()
        {
            InitComp();
        }

        protected void InitComp()
        {
            this.SuspendLayout();
            this.Font = new System.Drawing.Font("Segoe UI", 10.0f, System.Drawing.FontStyle.Regular);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CausesValidation = false;
            this.HideSelection = false;
            this.ShortcutsEnabled = false;
            this.TabStop = false;
            this.WordWrap = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            e.Handled = !(((int)e.KeyChar >= 47 && (int)e.KeyChar <= 58) || ((int)e.KeyChar == 8));
        }


        public int NumericValue
        {
            get
            {
                int retInt;
                int.TryParse(this.Text, out retInt);
                return retInt;
            }
            set
            {
                this.Text = value.ToString();
            }
        }
    }
}