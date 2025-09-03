using BLogicSystems.XPos.Resources;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public partial class NumericTextBox : System.Windows.Forms.TextBox
    {
        private NumberFormatInfo numberFormatInfo;
        string formatSpecifier = "c";
        private decimal maxValue = 999999999;
        InputPosition inputPosition = InputPosition.NormalDigit;
        bool handleTextChanged = true;

        public NumericTextBox()
        {
            this.numberFormatInfo = BLogicSystems.XPos.Utilites.Languages.Languages.GetDefaultCulture().NumberFormat;
            //System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.Clone() as NumberFormatInfo;
        }

        public decimal MaxValue
        {
            get
            {
                return this.maxValue;
            }
            set
            {
                this.maxValue = value;
            }
        }

        public decimal Value
        {
            get
            {
                try
                {
                    if (this.Text == string.Empty) return 0;
                    return Convert.ToDecimal(this.Text);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                if (this.Value != value)
                {
                    this.handleTextChanged = false;
                    this.Text = value.ToString(this.formatSpecifier, this.numberFormatInfo);
                    this.handleTextChanged = true;
                }
            }
        }

        public string FormatSpecifier
        {
            get { return this.formatSpecifier; }
            set
            {
                this.formatSpecifier = value;
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (this.IsInputChar(e.KeyChar))
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        /// <summary>
        /// format entered number base on NumberFormatInfo and FormatSpecifier
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
            try
            {
                this.handleTextChanged = false;
                if (this.Text == string.Empty) this.Text = "0";
                decimal value = Convert.ToDecimal(this.Text);
                if (value > this.MaxValue) value = this.MaxValue;
                this.Text = value.ToString(formatSpecifier, numberFormatInfo);
            }
            catch
            {
                string message = MessageResources.ResourceManager.GetString("InvalidNumber");
                string caption = MessageResources.ResourceManager.GetString("CAPTION");
                //BLogicMessageBox.ShowErrorSmall(message, caption);
                MessageBox.Show(message, caption);
                e.Cancel = true;
            }
            finally
            {
                this.handleTextChanged = true;
            }
        }

        /// <summary>
        /// format entered number base on NumberFormatInfo and FormatSpecifier
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            if (!this.handleTextChanged) return;
            base.OnTextChanged(e);
            if (this.Text == string.Empty || this.inputPosition == InputPosition.DecimalDegit) return;

            try
            {
                decimal value = Convert.ToDecimal(this.Text);
                if (value > this.maxValue) value = maxValue;
                string text = this.Text.Substring(0, this.SelectionStart);
                this.SelectionStart -= text.Length - text.Replace(numberFormatInfo.CurrencyGroupSeparator, string.Empty).Length;

                text = value.ToString();

                int index = text.IndexOf(numberFormatInfo.CurrencyDecimalSeparator);
                if (index == -1 && this.Text.IndexOf(numberFormatInfo.CurrencyDecimalSeparator) == this.Text.Length - 1)
                {
                    index = text.Length;
                    text = text + numberFormatInfo.CurrencyDecimalSeparator;
                    this.SelectionStart++;
                }
                int selectionStart = this.SelectionStart;
                if (index == -1) index = text.Length;
                while (index - 3 > 0)
                {
                    index -= 3;
                    text = text.Insert(index, numberFormatInfo.CurrencyGroupSeparator);
                    if (index < this.SelectionStart) selectionStart++;
                }

                if (this.Text != text)
                {
                    this.handleTextChanged = false;
                    this.Text = text;
                    this.handleTextChanged = true;
                }
                this.SelectionStart = selectionStart;
            }
            catch
            {
                this.Text = string.Empty;
            }
        }

        /// <summary>
        /// alow only digits, currency decimal separator and control keys
        /// </summary>
        /// <param name="charCode"></param>
        /// <returns></returns>
        protected override bool IsInputChar(char charCode)
        {
            this.inputPosition = InputPosition.NormalDigit;
            if (charCode.ToString() == numberFormatInfo.CurrencyDecimalSeparator && this.Text.IndexOf(numberFormatInfo.CurrencyDecimalSeparator) == -1)
            {
                this.inputPosition = InputPosition.DecimalSeparator;
                return true;
            }
            if (char.IsNumber(charCode))
            {
                int index = this.Text.IndexOf(numberFormatInfo.CurrencyDecimalSeparator);
                if (index != -1 && index < this.SelectionStart + this.SelectionLength) this.inputPosition = InputPosition.DecimalDegit;
                return true;
            }
            if (char.IsControl(charCode)) return true;
            return false;
        }
    }

    enum InputPosition
    {
        DecimalSeparator,
        NormalDigit,
        DecimalDegit
    }
}
