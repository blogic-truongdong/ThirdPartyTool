using System;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public enum CustomCalendarType
    {
        Day,
        Month,
        Year
    }

    public partial class CustomCalendar : UserControl
    {
        public event EventHandler<int> DoneSelectMonthEvent;
        public event EventHandler DoneSelectDayEvent;

        private CustomCalendarType CurrentType = CustomCalendarType.Day;

        public int year = 0;
        public int month = 0;
        public int day = 0;

        private DateTime selectedArea;

        private DateTime dateTimeValues;

        public DateTime DateTimeValues
        {
            get => dateTimeValues;
            set
            {
                dateTimeValues = value;


                year = value.Year;
                month = value.Month;
                day = value.Day;

                selectedArea = new DateTime(year, month, 1);
            }
        }

        public CustomCalendar()
        {
            InitializeComponent();

            this.Load += CustomCalendar_Load;

            btnDateRange.Click += BtnDateRange_Click;

            this.VisibleChanged += CustomCalendar_VisibleChanged;

            btnDateRange.GotFocus += BtnDateRange_GotFocus;
            btnNext.GotFocus += BtnDateRange_GotFocus;
            btnPrevious.GotFocus += BtnDateRange_GotFocus;

            btnNext.Click += BtnNext_Click;
            btnPrevious.Click += BtnPrevious_Click;

            dayCalendar.DoneSelectDayEvent += DayCalendar_DoneSelectDayEvent;
            monthCalendar.DoneSelectMonthEvent += MonthCalendar_DoneSelectMonthEvent;
        }

        private void MonthCalendar_DoneSelectMonthEvent(object sender, int e)
        {
            month = e;
            selectedArea = new DateTime(year, month, 1);
            UpdateDayCalendar();
            dayCalendar.BringToFront();
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            month--;

            selectedArea = new DateTime(year, month, 1);
            UpdateDayCalendar();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            month++;

            selectedArea = new DateTime(year, month, 1);
            UpdateDayCalendar();
        }

        private void DayCalendar_DoneSelectDayEvent(object sender, EventArgs e)
        {
            DoneSelectDayEvent?.Invoke(sender, e);
        }

        private void BtnDateRange_GotFocus(object sender, EventArgs e)
        {
            txtFocus.Focus();
        }

        private void CustomCalendar_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
                return;

            CurrentType = CustomCalendarType.Day;

            UpdateCurrentDateRange();

            UpdateDayCalendar();
        }

        private void UpdateDayCalendar()
        {
            try
            {
                dayCalendar.UpdateDays(selectedArea);
            }
            catch (Exception ex)
            {

            }
        }

        private void BtnDateRange_Click(object sender, EventArgs e)
        {
            CurrentType = CurrentType == CustomCalendarType.Day ? CustomCalendarType.Month :
                          (CurrentType == CustomCalendarType.Month ? CustomCalendarType.Year : CustomCalendarType.Day);

            UpdateCurrentDateRange();
        }

        private void UpdateCurrentDateRange()
        {
            if (CurrentType == CustomCalendarType.Day)
            {
                btnDateRange.Text = selectedArea.ToString("MMMM yyyy");

                dayCalendar.BringToFront();
            }
            else if (CurrentType == CustomCalendarType.Month)
            {
                btnDateRange.Text = dateTimeValues.Year.ToString();

                monthCalendar.BringToFront();
            }
            else if (CurrentType == CustomCalendarType.Year)
            {
                //btnDateRange.Text = 
                yearCalendar.BringToFront();
            }
        }

        private void CustomCalendar_Load(object sender, EventArgs e)
        {

        }
    }
}
