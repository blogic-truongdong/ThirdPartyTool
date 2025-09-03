using System;
using System.Drawing;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public partial class BTextBox : TextBox
    {
        private string _placeholderText = "Enter text here...";
        private bool _isPlaceholderActive = true;
        private Color _placeholderColor = Color.Gray;
        private Color _defaultForeColor;

        public string PlaceholderText
        {
            get { return _placeholderText; }
            set
            {
                _placeholderText = value;
                ShowPlaceholder();
            }
        }

        public Color PlaceholderColor
        {
            get { return _placeholderColor; }
            set
            {
                _placeholderColor = value;
                if (_isPlaceholderActive)
                    this.ForeColor = _placeholderColor;
            }
        }

        public BTextBox()
        {
            _defaultForeColor = this.ForeColor;
            this.GotFocus += RemovePlaceholder;
            this.LostFocus += ShowPlaceholder;
            this.TextChanged += OnTextChangedHandler;
        }

        private void ShowPlaceholder(object sender = null, EventArgs e = null)
        {
            if (string.IsNullOrWhiteSpace(this.Text))
            {
                _isPlaceholderActive = true;
                this.Text = _placeholderText;
                this.ForeColor = _placeholderColor;
            }
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (_isPlaceholderActive)
            {
                _isPlaceholderActive = false;
                this.Text = "";
                this.ForeColor = _defaultForeColor;
            }
        }

        private void OnTextChangedHandler(object sender, EventArgs e)
        {
            if (!_isPlaceholderActive && this.ForeColor == _placeholderColor)
            {
                this.ForeColor = _defaultForeColor;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ShowPlaceholder();
        }

        // Lấy text thực sự (không trả về placeholder)
        public string GetRealText()
        {
            return _isPlaceholderActive ? string.Empty : this.Text;
        }
    }
}
