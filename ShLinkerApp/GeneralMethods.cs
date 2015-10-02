using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ShLinkerApp
{
    public class GeneralMethods
    {
        private static bool _enabled;

        public static Action<object,KeyEventArgs> KeyDown { get; private set; }

        static GeneralMethods()
        {
            KeyDown = (o, e) =>
            {
                if (e.Alt && e.Control && e.KeyCode == Keys.C)
                {
                    var t = Clipboard.GetText();
                    Debug.WriteLine(t);
                }
                if (e.Control && e.KeyCode == Keys.D1)
                {
                    _enabled = !_enabled;
                }
                if (_enabled)
                {
                    Clipboard.SetText("My app is enabled. You cannot use your clipboard. Ha ha ha.");
                }
            };

        }
    }
}
