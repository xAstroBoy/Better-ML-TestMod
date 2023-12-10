
namespace Utils.Extensions
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using MelonLoader;
    using UnityEngine;

    public static class MiscUtils
    {
        public static void Start(this IEnumerator e) => MelonCoroutines.Start(e);

        public static void ForceQuit()
        {
            try
            {
                Process.GetCurrentProcess().Kill();
            }
            catch { }

            try
            {
                Environment.Exit(0);
            }
            catch { }

            try
            {
                Application.Quit();
            }
            catch { }
        }

        public static void DelayFunction(float del, Action action)
        {
            if (action == null)
            {
                MelonLogger.Error("DelayFunction: action was null");
                return;
            }
            MelonCoroutines.Start(Delay(del, action));
        }
        public static void ActionAsCoroutine(Action action)
        {
            if (action == null)
            {
                MelonLogger.Error("ActionAsCoroutine: action was null");
                return;
            }
            MelonCoroutines.Start(Action(action));
        }

        private static IEnumerator Action( Action action) {
            action.Invoke();
            yield break;
        }


        private static IEnumerator Delay(float del, Action action)
        {
            yield return new WaitForSeconds(del);
            action.Invoke();
            yield break;
        }


        public static void CopyToClipboard(this string copytext)
        {
            TextEditor textEditor = new TextEditor
            {
                text = copytext
            };
            textEditor.SelectAll();
            textEditor.Copy();
        }
    }
}
