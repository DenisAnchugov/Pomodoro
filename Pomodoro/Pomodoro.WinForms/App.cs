using System;
using System.Windows.Forms;

namespace Pomodoro.WinForms
{
    class App
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Pomodoro());            
        }
    }
}
