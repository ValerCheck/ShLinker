﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Automation.Text;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using System.Windows.Automation;

namespace ShLinkerApp
{
    static class Program
    {
        private static IKeyboardMouseEvents GlobalHook;
        private static string ToPaste { get; set; }
        private static string ToCopy { get; set; }

        public static void Subscribe()
        {
            GlobalHook = Hook.GlobalEvents();
            GlobalHook.KeyDown += GlobalHookKeyDownExt;
        }

        [STAThread]
        static void Main()
        {
            Subscribe();
            ToPaste = "";
            Application.Run();
        }

        private static void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            Process[] plist = Process.GetProcesses();
            foreach (Process p in plist)
            {
                if (p.ProcessName == "notepad")
                {
                    AutomationElement ae = AutomationElement.FromHandle(p.MainWindowHandle);
                    AutomationElement npEdit = ae.FindFirst(TreeScope.Descendants,
                        new PropertyCondition(AutomationElement.ClassNameProperty, "Edit"));

                    TextPattern tp = npEdit.GetCurrentPattern(TextPattern.Pattern) as TextPattern;
                    TextPatternRange[] trs;

                    if (tp.SupportedTextSelection == SupportedTextSelection.None)
                    {
                        return;
                    }
                    trs = tp.GetSelection();
                    Debug.WriteLine(trs[0].GetText(-1));
                }
            }
        }
        
        private static void GlobalHookKeyDownExt(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.Control && e.KeyCode == Keys.C)
            {
                ToCopy = Clipboard.GetText();
                Debug.WriteLine(ToCopy);
            }
            if (e.Alt && e.Control && e.KeyCode == Keys.V)
            {}
        }

        public static void Unsubscribe()
        {
            GlobalHook.KeyDown -= GlobalHookKeyDownExt;
            GlobalHook.Dispose();
        }
    }
}
