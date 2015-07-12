using Pomodoro.Core;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace Pomodoro.WinForms
{
    public partial class Pomodoro : Form, IPomodoroView
    {
        Presenter presenter;

        public Pomodoro()
        {
            InitializeComponent();
            presenter = new Presenter(this);
        }

        public void NotifyTimerElapsed()
        {
            throw new NotImplementedException();
        }

        public void UpdateTime(TimeSpan elapsedTime)
        {
            UpdateTimeLabel(elapsedTime);
            UpdateTrayIcon(elapsedTime);
        }

        void UpdateTrayIcon(TimeSpan elapsedTime)
        {
            this.TrayIcon.Icon = GetIcon(elapsedTime.Minutes.ToString());
        }

        Icon GetIcon(string minutes)
        {
            Bitmap bitmap = new Bitmap(36, 32);

            Font drawFont = new Font("Calibri", 20, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            graphics.DrawString(minutes, drawFont, drawBrush, 0, 2);

            Icon createdIcon = Icon.FromHandle(bitmap.GetHicon());

            drawFont.Dispose();
            drawBrush.Dispose();
            graphics.Dispose();
            bitmap.Dispose();

            return createdIcon;
        }

        void UpdateTimeLabel(TimeSpan elapsedTime)
        {
            this.Timer.Invoke((MethodInvoker)(() =>
            {
                this.Timer.Text = elapsedTime.ToString("mm':'ss"); ;
            }));
        }

        void StartButton_Click(object sender, EventArgs e)
        {
            if (presenter.SwitchTimerState())
            {
                this.Timer.Invoke((MethodInvoker)(() =>
                {
                    this.StartButton.Text = "Pause";
                }));
            }
            else
            {
                this.Timer.Invoke((MethodInvoker)(() =>
                {
                    this.StartButton.Text = "Start";
                }));
            }
        }

        void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (Visible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        protected override void OnLoad(EventArgs e)
        {
            var screen = Screen.FromPoint(this.Location);
            this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height);
            base.OnLoad(e);
        }
    }
}
