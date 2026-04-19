using System;
using System.Drawing;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public partial class CustomDayCalendar : UserControl
    {
        public event EventHandler DoneSelectDayEvent;

        private Color CurrentDayColor = Color.FromArgb(11, 87, 208);
        private Color GrayColor = Color.Gray;
        private Color BlackColor = Color.Black;

        private DateTime selectedArea;
        public CustomDayCalendar()
        {
            InitializeComponent();

            btn1.Click += BtnDay_Click;
            btn2.Click += BtnDay_Click;
            btn3.Click += BtnDay_Click;
            btn4.Click += BtnDay_Click;
            btn5.Click += BtnDay_Click;
            btn6.Click += BtnDay_Click;
            btn7.Click += BtnDay_Click;
            btn8.Click += BtnDay_Click;
            btn9.Click += BtnDay_Click;
            btn10.Click += BtnDay_Click;
            btn11.Click += BtnDay_Click;
            btn12.Click += BtnDay_Click;
            btn13.Click += BtnDay_Click;
            btn14.Click += BtnDay_Click;
            btn15.Click += BtnDay_Click;
            btn16.Click += BtnDay_Click;
            btn17.Click += BtnDay_Click;
            btn18.Click += BtnDay_Click;
            btn19.Click += BtnDay_Click;
            btn20.Click += BtnDay_Click;
            btn21.Click += BtnDay_Click;
            btn22.Click += BtnDay_Click;
            btn23.Click += BtnDay_Click;
            btn24.Click += BtnDay_Click;
            btn25.Click += BtnDay_Click;
            btn26.Click += BtnDay_Click;
            btn27.Click += BtnDay_Click;
            btn28.Click += BtnDay_Click;
            btn29.Click += BtnDay_Click;
            btn30.Click += BtnDay_Click;
            btn31.Click += BtnDay_Click;
            btn32.Click += BtnDay_Click;
            btn33.Click += BtnDay_Click;
            btn34.Click += BtnDay_Click;
            btn35.Click += BtnDay_Click;
            btn36.Click += BtnDay_Click;
            btn37.Click += BtnDay_Click;
            btn38.Click += BtnDay_Click;
            btn39.Click += BtnDay_Click;
            btn40.Click += BtnDay_Click;
            btn41.Click += BtnDay_Click;
            btn42.Click += BtnDay_Click;
        }

        private void BtnDay_Click(object sender, EventArgs e)
        {
            DoneSelectDayEvent?.Invoke(sender, e);
        }

        public void UpdateDays(DateTime _selectedArea)
        {
            selectedArea = _selectedArea;
            try
            {
                int startIndex = (int)selectedArea.DayOfWeek + 1;
                int lastIndex = startIndex + selectedArea.AddDays(-1).Day - 1;
                int currentIndex = startIndex + DateTime.Now.Day - 1;

                DateTime startDay = selectedArea.AddDays((int)selectedArea.DayOfWeek * -1);

                foreach (Button btn in pnlContainerDays.Controls)
                {
                    btn.Tag = startDay;
                    btn.Text = startDay.Day.ToString();
                    if (startDay.Month == selectedArea.Month)
                    {
                        if (startDay.Month == DateTime.Now.Month && startDay.Day == DateTime.Now.Day)
                        {
                            btn.FlatAppearance.BorderSize = 1;
                            btn.ForeColor = CurrentDayColor;
                        }
                        else
                        {
                            btn.ForeColor = BlackColor;
                            btn.FlatAppearance.BorderSize = 0;
                        }
                    }
                    else
                    {
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
