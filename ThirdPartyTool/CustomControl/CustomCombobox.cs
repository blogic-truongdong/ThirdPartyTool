using BLogicSystems.POSBackOffice.Control.Base;
using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public partial class CustomCombobox : UserControl
    {
        public event EventHandler SelectedIndexChanged;

        private PoperContainer popup;
        private CustomComboboxDropdown dropdown;

        public DataTable DataSource;
        public string DisplayMember;
        public string ValueMember;
        public int SelectedIndex = -1;
        public object SelectedValue = null;
        public object SelectedItem = null;
        public string SelectedText = "";

        public CustomCombobox()
        {
            InitializeComponent();

            lblText.Click += LblText_Click;
            btnArrow.Click += LblText_Click;

            this.Load += CustomCombobox_Load;


        }

        private void CustomCombobox_Load(object sender, System.EventArgs e)
        {

            try
            {
                lblText.Font = this.Font;
                if (DataSource?.Rows.Count > 0 && (SelectedIndex != -1 || SelectedValue != null))
                {
                    if (SelectedIndex != -1)
                    {
                        DataRow row = DataSource.Rows[SelectedIndex];
                        if (row != null)
                        {
                            lblText.Text = Utility.GetStringFromRow(DisplayMember, row);
                        }
                        return;
                    }

                    if (SelectedValue != null)
                    {
                        DataRow[] drs = DataSource.Select($"{ValueMember} = '{SelectedValue}'", "");
                        if (drs?.Length > 0)
                        {
                            lblText.Text = Utility.GetStringFromRow(DisplayMember, drs[0]);
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void LblText_Click(object sender, System.EventArgs e)
        {
            dropdown = new CustomComboboxDropdown();
            dropdown.Font = this.Font;
            dropdown.DataSource = DataSource;
            dropdown.ValueMember = ValueMember;
            dropdown.DisplayMember = DisplayMember;
            dropdown.Width = this.Width;
            dropdown.InitData();
            popup = new PoperContainer(dropdown);
            dropdown.SelectedIndexChanged += Dropdown_SelectedIndexChangedEvent;
            popup.Show(this);
        }

        private void Dropdown_SelectedIndexChangedEvent(object sender, System.EventArgs e)
        {
            popup.Close();
            SelectedValue = dropdown.SelectedValue;
            lblText.Text = dropdown.SelectedText;
            dropdown.Dispose();
            SelectedIndexChanged?.Invoke(null, null);
        }
    }
}
