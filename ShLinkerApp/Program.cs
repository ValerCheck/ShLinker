using System;
using System.Windows.Forms;

namespace ShLinkerApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ShlinkerHook.KeyDownAction = GeneralMethods.KeyDown;
            ShlinkerHook.Subscribe();
            Application.Run();
        }
    }
}
