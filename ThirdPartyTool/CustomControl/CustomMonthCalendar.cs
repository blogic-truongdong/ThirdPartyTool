using BLogicSystems.XPos.Utilites;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public partial class CustomMonthCalendar : UserControl
    {

        public event EventHandler<int> DoneSelectMonthEvent;

        public CustomMonthCalendar()
        {
            InitializeComponent();

            btn1.Click += BtnMonth_Click;
            btn2.Click += BtnMonth_Click;
            btn3.Click += BtnMonth_Click;
            btn4.Click += BtnMonth_Click;
            btn5.Click += BtnMonth_Click;
            btn6.Click += BtnMonth_Click;
            btn7.Click += BtnMonth_Click;
            btn8.Click += BtnMonth_Click;
            btn9.Click += BtnMonth_Click;
            btn10.Click += BtnMonth_Click;
            btn11.Click += BtnMonth_Click;
            btn12.Click += BtnMonth_Click;
        }

        private void BtnMonth_Click(object sender, EventArgs e)
        {
            DoneSelectMonthEvent?.Invoke(sender, Utility.GetIntFromObject((sender as Control).Tag));
        }

       
    }
}
