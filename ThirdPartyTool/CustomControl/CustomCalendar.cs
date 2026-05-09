using BLogicSystems.XPos.Utilites;
using System;
using System.Drawing;
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
        private Color SelectedBackColor = Color.FromArgb(11, 87, 208);
        private Color SelectedForeColor = Color.White;
        private Color DefaultBackColor = Color.White;
        private Color DefaultForeColor = Color.Black;

        private Color GrayColor = Color.Gray;
        private Color BlackColor = Color.Black;

        public event EventHandler<DateTime> DoneDateTimeValuesChanged;
        public event EventHandler<DateTime> DateTimeValuesChanged;

        private CustomCalendarType CurrentType = CustomCalendarType.Day;

        public int year = 0;
        public int month = 0;
        public int day = 0;

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
            }
        }

        public CustomCalendar()
        {
            InitializeComponent();

            this.Load += CustomCalendar_Load;

            this.VisibleChanged += CustomCalendar_VisibleChanged;

            btnDateRange.Click += BtnDateRange_Click;
            btnDateRange.GotFocus += BtnDateRange_GotFocus;

            btnNext.GotFocus += BtnDateRange_GotFocus;
            btnPrevious.GotFocus += BtnDateRange_GotFocus;

            btnNext.Click += BtnNext_Click;
            btnPrevious.Click += BtnPrevious_Click;

            dayCalendar.DoneSelectDayEvent += DayCalendar_DoneSelectDayEvent;
            monthCalendar.DoneSelectMonthEvent += MonthCalendar_DoneSelectMonthEvent;
            yearCalendar.DoneSelectYearEvent += YearCalendar_DoneSelectYearEvent;
        }

        private void YearCalendar_DoneSelectYearEvent(object sender, int e)
        {
            year = e;

            CurrentType = CustomCalendarType.Month;

            UpdateCalendar();

            DateTimeValuesChanged?.Invoke(null, new DateTime(year, month, day));
        }

        private void MonthCalendar_DoneSelectMonthEvent(object sender, int e)
        {
            DateTimeValues = new DateTime(year, e, 1);

            CurrentType = CustomCalendarType.Day;

            UpdateCalendar();

            DateTimeValuesChanged?.Invoke(null, new DateTime(year, month, day));
        }

        private void DayCalendar_DoneSelectDayEvent(object sender, DateTime e)
        {
            DoneDateTimeValuesChanged?.Invoke(null, e);
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentType == CustomCalendarType.Day)
            {
                month--;

                day = 1;

                if (month == 0)
                {
                    month = 12;
                    year--;
                }

                UpdateCalendar();
            }
            else if (CurrentType == CustomCalendarType.Month)
            {
                year--;

                month = 1;

                UpdateCalendar();

            }
            else if (CurrentType == CustomCalendarType.Year)
            {
                year -= 12;

                UpdateCalendar();
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (CurrentType == CustomCalendarType.Day)
            {
                month++;

                day = 1;

                if (month == 13)
                {
                    month = 1;
                    year++;
                }

                UpdateCalendar();
            }
            else if (CurrentType == CustomCalendarType.Month)
            {
                year++;

                month = 1;

                UpdateCalendar();

            }
            else if (CurrentType == CustomCalendarType.Year)
            {
                year += 12;

                UpdateCalendar();
            }
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

            UpdateCalendar();
        }

        private void BtnDateRange_Click(object sender, EventArgs e)
        {
            CurrentType = CurrentType == CustomCalendarType.Day ? CustomCalendarType.Month :
                          (CurrentType == CustomCalendarType.Month ? CustomCalendarType.Year : CustomCalendarType.Year);

            UpdateCalendar();
        }

        private void UpdateCalendar()
        {
            if (CurrentType == CustomCalendarType.Day)
            {
                btnDateRange.Text = new DateTime(year, month, day).ToString("MMMM yyyy");

                UpdateDays();

                dayCalendar.BringToFront();
            }
            else if (CurrentType == CustomCalendarType.Month)
            {
                btnDateRange.Text = year.ToString();

                UpdateMonths();

                monthCalendar.BringToFront();
            }
            else if (CurrentType == CustomCalendarType.Year)
            {
                btnDateRange.Text = $"{year - 5} - {year + 6}";

                UpdateYears();

                yearCalendar.BringToFront();
            }
        }

        private void CustomCalendar_Load(object sender, EventArgs e)
        {

        }

        public void UpdateYears()
        {
            try
            {
                int tempYear = year - 5;
                foreach (Button btn in this.yearCalendar.pnlContainer.Controls)
                {
                    btn.Text = tempYear.ToString();
                    btn.Tag = tempYear;

                    if (tempYear == year)
                    {
                        btn.BackColor = SelectedBackColor;
                        btn.ForeColor = SelectedForeColor;
                    }
                    else
                    {
                        btn.BackColor = DefaultBackColor;
                        btn.ForeColor = DefaultForeColor;
                    }

                    tempYear++;
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("UpdateYear in CustomYearCalendar ", ex);
            }

        }

        public void UpdateMonths()
        {
            try
            {
                foreach (Button btn in this.monthCalendar.pnlContainer.Controls)
                {
                    if (Utility.GetIntFromObject(btn.Tag) == month)
                    {
                        btn.BackColor = SelectedBackColor;
                        btn.ForeColor = SelectedForeColor;
                    }
                    else
                    {
                        btn.BackColor = DefaultBackColor;
                        btn.ForeColor = DefaultForeColor;
                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("UpdateYear in CustomYearCalendar ", ex);
            }
        }

        public void UpdateDays()
        {
            try
            {
                DateTime selectedArea = new DateTime(year, month, 1);

                int startIndex = (int)selectedArea.DayOfWeek + 1;
                int lastIndex = startIndex + selectedArea.AddDays(-1).Day - 1;
                int currentIndex = startIndex + DateTime.Now.Day - 1;

                DateTime startDay = selectedArea.AddDays((int)selectedArea.DayOfWeek * -1);

                foreach (Button btn in this.dayCalendar.pnlContainer.Controls)
                {
                    btn.Tag = startDay;
                    btn.Text = startDay.Day.ToString();

                    if (startDay.Month == selectedArea.Month)
                    {
                        if (startDay.Month == DateTime.Now.Month && startDay.Day == DateTime.Now.Day)
                        {
                            //btn.FlatAppearance.BorderSize = 1;
                            //btn.ForeColor = SelectedBackColor;
                            btn.BackColor = SelectedBackColor;
                            btn.ForeColor = SelectedForeColor;
                        }
                        else
                        {
                            btn.FlatAppearance.BorderSize = 0;

                            if (startDay == dateTimeValues)
                            {
                                btn.BackColor = SelectedBackColor;
                                btn.ForeColor = SelectedForeColor;
                            }
                            else
                            {
                                btn.BackColor = DefaultBackColor;
                                btn.ForeColor = DefaultForeColor;
                            }
                        }
                    }
                    else
                    {
                        btn.BackColor = DefaultBackColor;
                        btn.ForeColor = GrayColor;
                        btn.FlatAppearance.BorderSize = 0;
                    }

                    startDay = startDay.AddDays(1);

                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("CustomDayCalendar.UpdateDays()", ex);
            }

        }
    }
}
