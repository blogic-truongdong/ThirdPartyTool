
using BLogicSystems.POSBackOffice.Control.Base;
using System;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public partial class CustomDateTimePicker : UserControl
    {
        public event EventHandler DateTimeValuesChanged;
        private DateTime StartDate;
        private DateTime EndDate;
        private DateTime dateTimeValues;
        private PoperContainer popDateRange;
        private CustomCalendar dateRange;

        public DateTime Value
        {
            get => dateTimeValues;
            set
            {
                dateTimeValues = value;
                lblDateRange.Text = value.ToShortDateString();
            }
        }

        public DateTime EndValue
        {
            get
            {
                return dateTimeValues.Date.AddDays(1).AddMilliseconds(-1);
            }
            set
            {
                dateTimeValues = value.Date;
                lblDateRange.Text = value.ToShortDateString();
            }
        }

        public CustomDateTimePicker()
        {
            InitializeComponent();

            dateRange = new CustomCalendar();
            dateRange.DateTimeValuesChanged += DateRange_DateTimeValuesChanged;
            dateRange.DoneDateTimeValuesChanged += DateRange_DoneDateTimeValuesChanged;
            popDateRange = new PoperContainer(dateRange);

            lblDateRange.Click += LblDateRange_Click;
            btnDateRange.Click += LblDateRange_Click;

            btnNext.GotFocus += BtnControls_GotFocus;
            btnPrevious.GotFocus += BtnControls_GotFocus;
            lblDateRange.GotFocus += BtnControls_GotFocus;
            btnDateRange.GotFocus += BtnControls_GotFocus;

            btnNext.Click += BtnNext_Click;
            btnPrevious.Click += BtnPrevious_Click;
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            Value = Value.AddDays(-1);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            Value = Value.AddDays(1);
        }

        private void DateRange_DoneDateTimeValuesChanged(object sender, DateTime e)
        {
            this.Value = e;

            this.popDateRange.Close();

            DateTimeValuesChanged?.Invoke(null, null);
        }

        private void DateRange_DateTimeValuesChanged(object sender, DateTime e)
        {
            Value = e;

            DateTimeValuesChanged?.Invoke(null, null);
        }

        private void BtnControls_GotFocus(object sender, EventArgs e)
        {
            txtFocus.Focus();
        }

        private void LblDateRange_Click(object sender, System.EventArgs e)
        {
            //StartDate = EndDate = Convert.ToDateTime(labelDateRange.Text);
            //dateRange.UnRegisterEventCalendar();
            //this.dateRange.Day_Selected -= new BLogicSystems.POSBackOffice.Control.ReportControl.DEventHandler(dateRange_Day_Selected);
            //dateRange.monthCalendar.SelectArea(StartDate, EndDate);
            dateRange.DateTimeValues = dateTimeValues;
            popDateRange.Show(this);
            //dateRange.RegisterEventCalendar();
            //dateRange.DaySelected(StartDate, EndDate);
            //this.dateRange.Day_Selected += new BLogicSystems.POSBackOffice.Control.ReportControl.DEventHandler(dateRange_Day_Selected);
            //CheckButtonDateRange(dateRange, (EndDate - StartDate).Days + 1, StartDate, EndDate);
        }
    }
}
