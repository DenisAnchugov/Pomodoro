using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

namespace Pomodoro
{
    class App : Form
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new App());
        }

        int workPeriodInMinutes = 25;
        int shortRestPeriodInMinutes = 3;
        int longRestPeriodInMinutes = 15;

        System.Timers.Timer minuteTimer;
        NotifyIcon trayIcon;

        public App()
        {
            trayIcon = new NotifyIcon();
            trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);

            
            minuteTimer = new System.Timers.Timer(60000);
            minuteTimer.AutoReset = true;
            minuteTimer.Elapsed += TimerElapsed;
            

            trayIcon.Icon = GetIcon(workPeriodInMinutes.ToString());
            trayIcon.Visible = true;
            minuteTimer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            workPeriodInMinutes--;
            trayIcon.Icon = GetIcon(workPeriodInMinutes.ToString());
            if (workPeriodInMinutes == 0)
            {
                trayIcon.BalloonTipText = "Period ended!";
                trayIcon.ShowBalloonTip(1000);
                minuteTimer.Stop();
            }                
        }

        public static Icon GetIcon(string text)
        {
            Bitmap bitmap = new Bitmap(36, 32);

            Font drawFont = new Font("Calibri", 20, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            graphics.DrawString(text, drawFont, drawBrush, 0, 2);

            Icon createdIcon = Icon.FromHandle(bitmap.GetHicon());

            drawFont.Dispose();
            drawBrush.Dispose();
            graphics.Dispose();
            bitmap.Dispose();

            return createdIcon;
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            base.OnLoad(e);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                trayIcon.Dispose();
            }
            base.Dispose(isDisposing);
        }
    }
}
