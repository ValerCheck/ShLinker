using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ShLinkerApp
{
    public class GeneralMethods
    {
        private static bool _enabled;
        private static ShortenerProvider _provider;
        private static string textFromClipboard;

        public GeneralMethods()
        {
            _provider = new BitlyShortenerProvider();
        }

        public static Action<object,KeyEventArgs> KeyDown { get; private set; }

        static GeneralMethods()
        {
            _provider = new BitlyShortenerProvider();
            KeyDown = (o, e) =>
            {
                if (e.Alt && e.Control && e.KeyCode == Keys.C)
                {
                    textFromClipboard = Clipboard.GetText();
                    //Debug.WriteLine(t);
                }
                if (e.Control && e.KeyCode == Keys.D1)
                {
                    _enabled = !_enabled;
                }
                if (_enabled)
                {
                    if (!String.IsNullOrEmpty(textFromClipboard))
                    {
                        var resetText = _provider.GetShortUrl(textFromClipboard);

                        Clipboard.SetText(resetText);
                        textFromClipboard = "";
                    }
                }
            };

        }
    }
}
