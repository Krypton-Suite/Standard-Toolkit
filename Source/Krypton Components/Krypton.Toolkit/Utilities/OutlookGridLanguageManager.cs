#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */

//--------------------------------------------------------------------------------
// Copyright (C) 2013-2021 JDH Software - <support@jdhsoftware.com>
//
// This program is provided to you under the terms of the Microsoft Public
// License (Ms-PL) as published at https://github.com/Cocotteseb/Krypton-OutlookGrid/blob/master/LICENSE.md
//
// Visit https://www.jdhsoftware.com and follow @jdhsoftware on Twitter
//
//--------------------------------------------------------------------------------

#endregion

namespace Krypton.Toolkit
{
    internal class OutlookGridLanguageManager
    {
        #region Instance Fields

        private static OutlookGridLanguageManager? _mInstance = null;

        private static readonly object _myLock = new();
        private ResourceManager _rm;

        private CultureInfo _ci;
        //Used for blocking critical sections on updates
        private object _locker = new();

        #endregion

        #region Identity

        public OutlookGridLanguageManager()
        {
            _rm = new ResourceManager("Krypton.Toolkit.ResourceFiles.OutlookGrid.Strings.OutlookGridStringResources", Assembly.GetExecutingAssembly());
            _ci = Thread.CurrentThread.CurrentCulture; //CultureInfo.CurrentCulture;
        }

        #endregion

        #region Public

        /// <summary>Gets or sets the P locker.</summary>
        /// <value>The P locker.</value>
        public object PLocker
        {
            get => _locker; 
            
            set => _locker = value;
        }

        /// <summary>Gets the instance of the singleton.</summary>
        public static OutlookGridLanguageManager Instance
        {
            get
            {
                if (_mInstance == null)
                {
                    lock (_myLock)
                    {
                        if (_mInstance == null)
                        {
                            _mInstance = new OutlookGridLanguageManager();
                        }
                    }
                }

                return _mInstance;
            }
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Get localized string
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetString(string name) => _rm.GetString(name, _ci)!;

        #endregion
    }
}