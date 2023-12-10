
namespace Utils.Colors
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using UnityEngine;

    internal class ColorUtils 
    {

        internal static UnityEngine.Color HexToColor(string hexColor)
        {
            if (hexColor.IndexOf('#') != -1)
            {
                hexColor = hexColor.Replace("#", "");
            }

            float r = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier) / 255f;
            float g = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier) / 255f;
            float b = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier) / 255f;
            return new UnityEngine.Color(r, g, b);
        }

        internal static string ColorToHex(System.Drawing.Color color)
        {
            return ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(color.ToArgb()));
        }


        internal static string ColorToHex(UnityEngine.Color color)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        }

        internal static UnityEngine.Color ToUnityEngineColor(System.Drawing.Color color)
        {
            return HexToColor(ColorToHex(color));
        }

    }
}