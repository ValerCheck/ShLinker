using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace ShLinkerApp
{
    public class ShlinkerHook
    {
        private static IKeyboardMouseEvents globalHookEvents;
        
        public static Action<object,KeyEventArgs> KeyDownAction;

        public static void Subscribe()
        {
            globalHookEvents = Hook.GlobalEvents();
            globalHookEvents.KeyDown += GlobalHookKeyDownExt;
        }

        private static void GlobalHookKeyDownExt(object sender, KeyEventArgs e)
        {
            KeyDownAction.Invoke(sender,e);
        }

        public static void Unsubscribe()
        {
            globalHookEvents.KeyDown -= GlobalHookKeyDownExt;
            globalHookEvents.Dispose();
        }
    }
}
