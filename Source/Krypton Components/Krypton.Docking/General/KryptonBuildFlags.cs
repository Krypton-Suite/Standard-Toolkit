#region BSD License
/*
 *
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

#if NIGHTLY || CANARY
#define PREVIEW_BUILD
#endif

#if RELEASE
#define STABLE_BUILD
#endif

#if DEBUG
#define INTERNAL_BUILD
#endif