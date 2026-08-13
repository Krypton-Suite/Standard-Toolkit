namespace Krypton.Toolkit;

/// <summary>
/// Opacity-step units for form fading. Applied as <c>units / 1000</c> on a 10 ms timer.
/// </summary>
internal static class KryptonFormFadeSpeed
{
    public const float DEFAULT_SLOWEST = 1;
    public const float DEFAULT_SLOWER = 10;
    public const float DEFAULT_SLOW = 25;
    public const float DEFAULT_NORMAL = 50;
    public const float DEFAULT_FAST = 60;
    public const float DEFAULT_FASTER = 75;
    public const float DEFAULT_FASTEST = 100;

    /// <summary>
    /// Maps a <see cref="FadeSpeedChoice"/> to fade-speed units.
    /// </summary>
    /// <param name="fadeSpeedChoice">The preset or <see cref="FadeSpeedChoice.Custom"/>.</param>
    /// <param name="customFadeSpeed">Used when <paramref name="fadeSpeedChoice"/> is <see cref="FadeSpeedChoice.Custom"/>.</param>
    /// <returns>Units compatible with <see cref="KryptonFormFadeController"/> (<c>opacity += units / 1000</c>).</returns>
    internal static float Resolve(FadeSpeedChoice fadeSpeedChoice, float? customFadeSpeed)
    {
        switch (fadeSpeedChoice)
        {
            case FadeSpeedChoice.Slowest:
                return DEFAULT_SLOWEST;
            case FadeSpeedChoice.Slower:
                return DEFAULT_SLOWER;
            case FadeSpeedChoice.Slow:
                return DEFAULT_SLOW;
            case FadeSpeedChoice.Normal:
                return DEFAULT_NORMAL;
            case FadeSpeedChoice.Fast:
                return DEFAULT_FAST;
            case FadeSpeedChoice.Faster:
                return DEFAULT_FASTER;
            case FadeSpeedChoice.Fastest:
                return DEFAULT_FASTEST;
            case FadeSpeedChoice.Custom:
                return customFadeSpeed.GetValueOrDefault() > 0
                    ? customFadeSpeed.GetValueOrDefault()
                    : DEFAULT_NORMAL;
            default:
                return customFadeSpeed.GetValueOrDefault() > 0
                    ? customFadeSpeed.GetValueOrDefault()
                    : DEFAULT_NORMAL;
        }
    }
}
