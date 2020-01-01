using System;
using System.Drawing;
using System.Windows.Forms;

namespace Net.FreeORM.Windows.SpannerControl
{
    public class FreeSpanner : TextBox
    {

        #region [ Private Fields ]

        private int _maxValue = 100;
        private int _minValue = 0;
        private int _value = 0;

        #endregion


        #region [ Properties ]

        #region [ Value Property ]

        /// <summary>
        /// Gets, Sets Value.
        /// </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                if (value < _minValue)
                {
                    _value = _minValue;
                }
                else if (value > _maxValue)
                {
                    _value = _maxValue;
                }
                else
                {
                    _value = value;
                }
                this.Text = _value.ToString();
            }
        }

        #endregion

        #region [ MaxValue Property ]

        /// <summary>
        /// Gets, Sets Maxinum Value.
        /// </summary>
        public int MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                if (_maxValue < _minValue)
                {
                    _maxValue += _minValue;
                    _minValue = _maxValue - _minValue;
                    _maxValue -= _minValue;
                }
                Value = _value;
            }
        }

        #endregion

        #region [ MinValue Propetry ]

        /// <summary>
        /// Gets, Sets Mininum Value.
        /// </summary>
        public int MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                if (_maxValue < _minValue)
                {
                    _maxValue += _minValue;
                    _minValue = _maxValue - _minValue;
                    _maxValue -= _minValue;
                }
                Value = _value;
            }
        }

        #endregion

        #endregion


        #region [ Ctor ]

        public FreeSpanner()
            : base()
        {
            this.InitComp();
        }

        #endregion


        #region [ InitComp method ]

        protected void InitComp()
        {
            this.SuspendLayout();
            //
            this.Name = "freeSpanner1";
            //
            this.Size = new Size(20, 20);
            this.TextAlign = HorizontalAlignment.Right;
            ResetSize();
            //
            this.TextChanged += new EventHandler(this.TxtNumChange);
            this.KeyPress += new KeyPressEventHandler(this.Spanner_KeyPress);
            this.KeyDown += new KeyEventHandler(this.Spanner_KeyDown);
            this.MouseWheel += new MouseEventHandler(Spanner_MouseWheel);
            //
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion


        #region [ ResetSize method ]

        private void ResetSize()
        {
            this.MinimumSize = new Size(20, 20);
            this.MaximumSize = new Size(100, 20);
        }

        #endregion


        #region [ OnSizeChanged method ]

        protected override void OnSizeChanged(System.EventArgs e)
        {
            base.OnSizeChanged(e);
            ResetSize();
        }

        #endregion


        #region [ Spanner_MouseWheel method ]

        private void Spanner_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.None)
            {
                this._value = e.Delta > 0 ? _value + 1 : _value - 1;
                if (_value < _minValue)
                {
                    _value = _maxValue;
                }
                else if (_value > _maxValue)
                {
                    _value = _minValue;
                }
                this.Text = _value.ToString();
            }
        }
        #endregion


        #region [ Spanner_KeyPress method ]

        private void Spanner_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !(((int)e.KeyChar >= 47 && (int)e.KeyChar <= 58) || ((int)e.KeyChar == 8));
            if (e.Handled == false)
            {
                this.Value = Util.ToInt(this.Text);
            }
        }

        #endregion


        #region [ Spanner_KeyDown method ]

        private void Spanner_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Up)
            {
                ++_value;
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.Down)
            {
                --_value;
            }
            else
            { }
            if (_value < _minValue)
            {
                _value = _maxValue;
            }
            else if (_value > _maxValue)
            {
                _value = _minValue;
            }
            this.Text = _value.ToString();
        }

        #endregion


        #region [ TextChangedEvent ]

        private void TxtNumChange(object sender, System.EventArgs e)
        {
            this.Value = Util.ToInt(this.Text);
        }

        #endregion


        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Value = Util.ToInt(this.Text);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            e.Handled = !(((int)e.KeyChar >= 47 && (int)e.KeyChar <= 58) || ((int)e.KeyChar == 8));
            if (e.Handled == false)
            {
                this.Value = Util.ToInt(this.Text);
            }
        }



    }

    #region [ Util static class ]

    internal static class Util
    {
        internal static int ToInt(String str)
        {
            try
            {
                int result;
                if (Int32.TryParse(str, out result))
                    return result;
                else
                    return 0;

            }
            catch (Exception)
            {
                return 0;
            }
        }
    }

    #endregion
}
