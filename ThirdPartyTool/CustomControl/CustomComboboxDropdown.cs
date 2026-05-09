using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public partial class CustomComboboxDropdown : UserControl
    {
        public event EventHandler SelectedIndexChanged;
        public object SelectedValue;
        public string SelectedText;
        public DataTable DataSource;
        public string ValueMember;
        public string DisplayMember;

        public CustomComboboxDropdown()
        {
            InitializeComponent();
        }

        public void InitData()
        {
            try
            {
                if (DataSource == null)
                    return;

                int count = DataSource.Rows.Count;

                if (count >= 10)
                    this.Height = 302;
                else
                    this.Height = count * 30 + 2;

                this.Controls.Clear();
                if (DataSource?.Rows.Count > 0)
                {
                    for (int i = DataSource.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow row = DataSource.Rows[i];
                        Button button = new Button();
                        button.Dock = DockStyle.Top;
                        button.FlatStyle = FlatStyle.Flat;
                        button.FlatAppearance.BorderSize = 0;
                        button.BackColor = Color.White;
                        button.Width = this.Width - 6;
                        button.Height = 30;
                        button.Text = Utility.GetStringFromRow(DisplayMember, row);
                        button.Tag = Utility.GetStringFromRow(ValueMember, row);
                        button.Font = this.Font;
                        button.Margin = new Padding(0);
                        button.TabStop = false;
                        button.TextAlign = ContentAlignment.MiddleLeft;
                        button.Click += Button_Click;
                        this.Controls.Add(button);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            SelectedValue = (sender as Button)?.Tag;
            SelectedText = (sender as Button)?.Text;
            SelectedIndexChanged?.Invoke(null, null);
        }

        private void CustomComboboxDropdown_Load(object sender, EventArgs e)
        {

        }
    }
}
