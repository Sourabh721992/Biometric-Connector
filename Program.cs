using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UserInfo
{
    static class Program
    {
        /// <summary>
        /// The application's main entry point.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TFT());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}