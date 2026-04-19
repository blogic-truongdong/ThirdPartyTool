
using BLogicSystems.POSBackOffice.Control.Base;
using System;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public partial class CustomDateTimePicker : UserControl
    {
        private DateTime StartDate;
        private DateTime EndDate;
        private DateTime dateTimeValues;
        private PoperContainer popDateRange;
        private CustomCalendar dateRange;

        public DateTime DateTimeValues
        {
            get => dateTimeValues;
            set
            {
                dateTimeValues = value;
                lblDateRange.Text = value.ToShortDateString();
            }
        }

        public CustomDateTimePicker()
        {
            InitializeComponent();

            dateRange = new CustomCalendar();
            dateRange.DoneSelectDayEvent += DateRange_DoneSelectDayEvent;
            dateRange.DoneSelectMonthEvent += DateRange_DoneSelectMonthEvent;
            popDateRange = new PoperContainer(dateRange);

            lblDateRange.Click += LblDateRange_Click;

        }

        private void DateRange_DoneSelectMonthEvent(object sender, int e)
        {

        }

        private void DateRange_DoneSelectDayEvent(object sender, EventArgs e)
        {
            this.popDateRange.Close();
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
