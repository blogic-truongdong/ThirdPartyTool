using BLogicSystems.XPos.Utilites;
using System;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public partial class IntergeTextBox : TextBox
    {
        private string defaultValue = string.Empty;
        public int Value
        {
            get
            {
                return Utility.GetIntFromObject(this.Text);
            }
            set
            {
                if (value == 0)
                    this.Text = defaultValue;
                else
                    this.Text = value.ToString();
            }
        }

        public string DefaultValue
        {
            get
            {
                return defaultValue;
            }
            set
            {
                this.defaultValue = value;
            }
        }

        public IntergeTextBox()
        {
            InitializeComponent();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        protected override void OnLostFocus(EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                this.Text = this.defaultValue;
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                this.Text = defaultValue;
            }
        }
    }
}
