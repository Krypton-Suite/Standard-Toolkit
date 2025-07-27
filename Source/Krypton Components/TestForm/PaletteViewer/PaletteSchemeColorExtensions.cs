using System;
using System.Drawing;
using System.Reflection;
using Krypton.Toolkit;

namespace TestForm;

// TODO Experimental! Only works for PaletteMicrosoft365Base-derived palettes!
internal static class PaletteSchemeColorExtensions
{
    /// <summary>
    /// Retrieves a scheme colour from the supplied palette. Works for PaletteMicrosoft365Base and
    /// the additional *Base palettes under Extra Themes.
    /// </summary>
    public static Color GetSchemeColor(this PaletteBase palette, SchemeBaseColors colorEnum)
    {
        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        // Prefer native method if present
        MethodInfo? getter = palette.GetType().GetMethod("GetSchemeColor", BindingFlags.Public | BindingFlags.Instance);
        if (getter != null && getter.ReturnType == typeof(Color))
        {
            return (Color)getter.Invoke(palette, new object[] { colorEnum })!;
        }

        // Try BaseColors property mapping
        if (TryGetBaseScheme(palette, out var schemeObj))
        {
            string propName = colorEnum.ToString();
            PropertyInfo? pi = schemeObj!.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
            if (pi != null && pi.PropertyType == typeof(Color))
            {
                return (Color)pi.GetValue(schemeObj)!;
            }
        }

        // Fallback: access private _ribbonColors/_ribbonColors array
        Color[]? scheme = GetColorArray(palette);
        return scheme is { Length: > 0 } && (int)colorEnum < scheme.Length
            ? scheme[(int)colorEnum]
            : Color.Empty;
    }

    /// <summary>
    /// Updates a single scheme colour on the supplied palette. Supports both built-in and reflection paths.
    /// </summary>
    public static void SetSchemeColor(this PaletteBase palette, SchemeBaseColors colorEnum, Color newColor)
    {
        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        // Prefer native method if present
        MethodInfo? setter = palette.GetType().GetMethod("SetSchemeColor", BindingFlags.Public | BindingFlags.Instance);
        if (setter != null)
        {
            setter.Invoke(palette, new object[] { colorEnum, newColor });

            // Ensure BaseColors scheme stays in sync with the updated ribbon colour
            if (TryGetBaseScheme(palette, out var nativeScheme))
            {
                string propName = colorEnum.ToString();
                PropertyInfo? pi = nativeScheme!.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                if (pi != null && pi.CanWrite && pi.PropertyType == typeof(Color))
                {
                    pi.SetValue(nativeScheme, newColor);

                    // Verify the assignment succeeded – fall back to backing field if required
                    if (((Color)pi.GetValue(nativeScheme)!) != newColor)
                    {
                        var backingField = nativeScheme.GetType().GetField($"<{propName}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
                        backingField?.SetValue(nativeScheme, newColor);
                    }
                }
            }
        }
        else
        {
            // First, invalidate cached colour table to ensure it will be rebuilt
            FieldInfo? tableField = palette.GetType().GetField("Table", BindingFlags.NonPublic | BindingFlags.Instance);
            if (tableField != null)
            {
                tableField.SetValue(palette, null);
            }

            // Reflection fallback: mutate colour array in-place
            Color[]? scheme = GetColorArray(palette);
            if (scheme is { Length: > 0 } && (int)colorEnum < scheme.Length)
            {
                scheme[(int)colorEnum] = newColor;
            }

            // Attempt to update BaseColors scheme property if present
            if (TryGetBaseScheme(palette, out var baseScheme))
            {
                string propName = colorEnum.ToString();
                PropertyInfo? pi = baseScheme!.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                if (pi != null && pi.CanWrite && pi.PropertyType == typeof(Color))
                {
                    pi.SetValue(baseScheme, newColor);

                    // Verify the value was set
                    var verifyValue = (Color)pi.GetValue(baseScheme)!;
                    if (verifyValue != newColor)
                    {
                        // If direct set didn't work, try finding the backing field
                        var backingField = baseScheme.GetType().GetField($"<{propName}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (backingField != null)
                        {
                            backingField.SetValue(baseScheme, newColor);
                        }
                    }
                }
            }

            // Invalidate cached colour table again after updates
            if (tableField != null)
            {
                tableField.SetValue(palette, null);
            }
        }

        // Notify listeners (KryptonManager and controls) that color table has changed
        MethodInfo? raisePaint = typeof(PaletteBase).GetMethod("OnPalettePaint", BindingFlags.NonPublic | BindingFlags.Instance);
        if (raisePaint != null)
        {
            // NeedColorTable= true, NeedLayout = false (layout unaffected)
            raisePaint.Invoke(palette, new object[] { palette, new PaletteLayoutEventArgs(false, true) });
        }
    }

    private static Color[]? GetColorArray(PaletteBase palette)
    {
        FieldInfo? fi = palette.GetType().GetField("_ribbonColors", BindingFlags.NonPublic | BindingFlags.Instance);
        return fi?.GetValue(palette) as Color[];
    }

    private static bool TryGetBaseScheme(PaletteBase palette, out KryptonColorSchemeBase? scheme)
    {
        scheme = null;
        FieldInfo? fi = palette.GetType().GetField("BaseColors", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        if (fi != null)
        {
            scheme = fi.GetValue(palette) as KryptonColorSchemeBase;
        }
        return scheme != null;
    }
}