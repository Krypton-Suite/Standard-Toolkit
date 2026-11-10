#region Original MIT License

/* The MIT License (MIT)
 *
 * Copyright (c) 2016 - 2020 Soroush Falahati
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

#endregion

#region Modified License

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */

#endregion

namespace WinFormAnimation;

/// <summary>
///     Contains public extensions methods about Path class
/// </summary>
internal static class PathExtensions
{
    /// <summary>
    ///     Continue the last paths with a new one
    /// </summary>
    /// <param name="paths">Array of paths</param>
    /// <param name="end">Next point to follow</param>
    /// <param name="duration">Duration of the animation</param>
    /// <returns>An array of paths including the newly created one</returns>
    public static Path[] ContinueTo(this Path[] paths, float end, ulong duration)
    {
        return paths.Concat([new Path(paths.Last().End, end, duration)]).ToArray();
    }

    /// <summary>
    ///     Continue the last paths with a new one
    /// </summary>
    /// <param name="paths">Array of paths</param>
    /// <param name="end">Next point to follow</param>
    /// <param name="duration">Duration of the animation</param>
    /// <param name="function">Animation controller function</param>
    /// <returns>An array of paths including the newly created one</returns>
    public static Path[] ContinueTo(this Path[] paths, float end, ulong duration,
        AnimationFunctions.Function function)
    {
        return paths.Concat([new Path(paths.Last().End, end, duration, function)]).ToArray();
    }

    /// <summary>
    ///     Continue the last paths with a new one
    /// </summary>
    /// <param name="paths">Array of paths</param>
    /// <param name="end">Next point to follow</param>
    /// <param name="duration">Duration of the animation</param>
    /// <param name="delay">Starting delay</param>
    /// <returns>An array of paths including the newly created one</returns>
    public static Path[] ContinueTo(this Path[] paths, float end, ulong duration, ulong delay)
    {
        return paths.Concat([new Path(paths.Last().End, end, duration, delay)]).ToArray();
    }

    /// <summary>
    ///     Continue the last paths with a new one
    /// </summary>
    /// <param name="paths">Array of paths</param>
    /// <param name="end">Next point to follow</param>
    /// <param name="duration">Duration of the animation</param>
    /// <param name="delay">Starting delay</param>
    /// <param name="function">Animation controller function</param>
    /// <returns>An array of paths including the newly created one</returns>
    public static Path[] ContinueTo(this Path[] paths, float end, ulong duration, ulong delay,
        AnimationFunctions.Function function)
    {
        return paths.Concat([new Path(paths.Last().End, end, duration, delay, function)]).ToArray();
    }


    /// <summary>
    ///     Continue the path with a new one
    /// </summary>
    /// <param name="path">The path to continue</param>
    /// <param name="end">Next point to follow</param>
    /// <param name="duration">Duration of the animation</param>
    /// <returns>An array of paths including the newly created one</returns>
    public static Path[] ContinueTo(this Path path, float end, ulong duration)
    {
        return path.ToArray().ContinueTo(end, duration);
    }

    /// <summary>
    ///     Continue the path with a new one
    /// </summary>
    /// <param name="path">The path to continue</param>
    /// <param name="end">Next point to follow</param>
    /// <param name="duration">Duration of the animation</param>
    /// <param name="function">Animation controller function</param>
    /// <returns>An array of paths including the newly created one</returns>
    public static Path[] ContinueTo(this Path path, float end, ulong duration, AnimationFunctions.Function function)
    {
        return path.ToArray().ContinueTo(end, duration, function);
    }

    /// <summary>
    ///     Continue the path with a new one
    /// </summary>
    /// <param name="path">The path to continue</param>
    /// <param name="end">Next point to follow</param>
    /// <param name="duration">Duration of the animation</param>
    /// <param name="delay">Starting delay</param>
    /// <returns>An array of paths including the newly created one</returns>
    public static Path[] ContinueTo(this Path path, float end, ulong duration, ulong delay)
    {
        return path.ToArray().ContinueTo(end, duration, delay);
    }

    /// <summary>
    ///     Continue the path with a new one
    /// </summary>
    /// <param name="path">The path to continue</param>
    /// <param name="end">Next point to follow</param>
    /// <param name="duration">Duration of the animation</param>
    /// <param name="delay">Starting delay</param>
    /// <param name="function">Animation controller function</param>
    /// <returns>An array of paths including the newly created one</returns>
    public static Path[] ContinueTo(this Path path, float end, ulong duration, ulong delay,
        AnimationFunctions.Function function)
    {
        return path.ToArray().ContinueTo(end, duration, delay, function);
    }


    /// <summary>
    ///     Continue the path array with a new ones
    /// </summary>
    /// <param name="paths">Array of paths</param>
    /// <param name="newPaths">An array of new paths to adds</param>
    /// <returns>An array of paths including the new ones</returns>
    public static Path[] ContinueTo(this Path[] paths, params Path[] newPaths)
    {
        return paths.Concat(newPaths).ToArray();
    }

    /// <summary>
    ///     Continue the path with a new ones
    /// </summary>
    /// <param name="path">The path to continue</param>
    /// <param name="newPaths">An array of new paths to adds</param>
    /// <returns>An array of paths including the new ones</returns>
    public static Path[] ContinueTo(this Path path, params Path[] newPaths)
    {
        return path.ToArray().ContinueTo(newPaths);
    }

    /// <summary>
    ///     Contains a single path in an array
    /// </summary>
    /// <param name="path">The path to add to the array</param>
    /// <returns>An array of paths including the new ones</returns>
    public static Path[] ToArray(this Path path)
    {
        return [path];
    }
}
