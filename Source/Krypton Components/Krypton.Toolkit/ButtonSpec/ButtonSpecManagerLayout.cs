﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Manage a collection of button specs for placing within a collection of ViewLayoutDocker instances.
    /// </summary>
    public class ButtonSpecManagerLayout : ButtonSpecManagerBase
    {
        #region Instance Fields
        private readonly ViewLayoutDocker[] _viewDockers;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecManagerLayout class.
        /// </summary>
        /// <param name="control">Control that owns the button manager.</param>
        /// <param name="redirector">Palette redirector.</param>
        /// <param name="variableSpecs">Variable set of button specifications.</param>
        /// <param name="fixedSpecs">Fixed set of button specifications.</param>
        /// <param name="viewDockers">Array of target view dockers.</param>
        /// <param name="viewMetrics">Array of target metric providers.</param>
        /// <param name="viewMetricInt">Array of target metrics for outside/inside spacer size.</param>
        /// <param name="viewMetricPaddings">Array of target metrics for button padding.</param>
        /// <param name="getRenderer">Delegate for returning a tool strip renderer.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ButtonSpecManagerLayout(Control control,
            [DisallowNull] PaletteRedirect? redirector,
                                       ButtonSpecCollectionBase? variableSpecs,
                                       ButtonSpecCollectionBase? fixedSpecs,
                                       ViewLayoutDocker[] viewDockers,
                                       IPaletteMetric[] viewMetrics,
                                       PaletteMetricInt[] viewMetricInt,
                                       PaletteMetricPadding[] viewMetricPaddings,
                                       GetToolStripRenderer getRenderer,
                                       NeedPaintHandler needPaint)
            : this(control, redirector, variableSpecs, fixedSpecs,
                   viewDockers, viewMetrics, viewMetricInt, viewMetricInt,
                   viewMetricPaddings, getRenderer, needPaint)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ButtonSpecManagerLayout class.
        /// </summary>
        /// <param name="control">Control that owns the button manager.</param>
        /// <param name="redirector">Palette redirector.</param>
        /// <param name="variableSpecs">Variable set of button specifications.</param>
        /// <param name="fixedSpecs">Fixed set of button specifications.</param>
        /// <param name="viewDockers">Array of target view dockers.</param>
        /// <param name="viewMetrics">Array of target metric providers.</param>
        /// <param name="viewMetricIntOutside">Array of target metrics for outside spacer size.</param>
        /// <param name="viewMetricIntInside">Array of target metrics for inside spacer size.</param>
        /// <param name="viewMetricPaddings">Array of target metrics for button padding.</param>
        /// <param name="getRenderer">Delegate for returning a tool strip renderer.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ButtonSpecManagerLayout(Control control,
            [DisallowNull] PaletteRedirect? redirector,
                                       ButtonSpecCollectionBase? variableSpecs,
                                       ButtonSpecCollectionBase? fixedSpecs,
                                       ViewLayoutDocker[] viewDockers,
                                       IPaletteMetric[] viewMetrics,
                                       PaletteMetricInt[] viewMetricIntOutside,
                                       PaletteMetricInt[] viewMetricIntInside,
                                       PaletteMetricPadding[] viewMetricPaddings,
                                       GetToolStripRenderer getRenderer,
                                       NeedPaintHandler needPaint)
            : base(control, redirector, variableSpecs, fixedSpecs,
                   viewMetrics, viewMetricIntOutside, viewMetricIntInside,
                   viewMetricPaddings, getRenderer, needPaint)
        {
            // Remember references
            _viewDockers = viewDockers;

            Construct();
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Gets the number of dockers.
        /// </summary>
        /// <returns>Number of dockers.</returns>
        protected override int DockerCount => _viewDockers.Length;

        /// <summary>
        /// Gets the index of the provided docker.
        /// </summary>
        /// <param name="viewDocker">View docker reference.</param>
        /// <returns>Index of docker; otherwise -1.</returns>
        protected override int DockerIndex(ViewBase viewDocker)
        {
            for (var i = 0; i < _viewDockers.Length; i++)
            {
                if (_viewDockers[i] == viewDocker)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets the docker at the specified index.
        /// </summary>
        /// <param name="i">Index.</param>
        /// <returns>View docker reference; otherwise null.</returns>
        protected override ViewBase? IndexDocker(int i) => _viewDockers.Length > i ? _viewDockers[i] : null;

        /// <summary>
        /// Gets the orientation of the docker at the specified index.
        /// </summary>
        /// <param name="i">Index.</param>
        /// <returns>VisualOrientation value.</returns>
        protected override VisualOrientation DockerOrientation(int i) =>
            _viewDockers.Length > i ? _viewDockers[i].Orientation : VisualOrientation.Top;

        /// <summary>
        /// Gets the element that represents the foreground color.
        /// </summary>
        /// <param name="i">Index.</param>
        /// <returns>View content instance.</returns>
        protected override ViewDrawContent? GetDockerForeground(int i) => null;

        /// <summary>
        /// Add a view element to a docker.
        /// </summary>
        /// <param name="i">Index of view docker.</param>
        /// <param name="dockStyle">Dock style for placement.</param>
        /// <param name="view">Actual view to add.</param>
        /// <param name="usingSpacers">Are view spacers being used.</param>
        protected override void AddViewToDocker(int i,
                                                ViewDockStyle dockStyle,
                                                ViewBase view,
                                                bool usingSpacers)
        {
            // Get the indexed docker
            ViewLayoutDocker viewDocker = _viewDockers[i];

            // By default add to the end of the children
            var insertIndex = viewDocker.Count;

            // If using spacers, then insert before the first spacer
            if (usingSpacers)
            {
                for (var j = 0; j < insertIndex; j++)
                {
                    if (viewDocker[j] is ViewLayoutMetricSpacer)
                    {
                        insertIndex = j;
                        break;
                    }
                }
            }

            viewDocker.Insert(insertIndex, view);
            viewDocker.SetDock(view, dockStyle);
        }

        /// <summary>
        /// Add the spacing views into the indexed docker.
        /// </summary>
        /// <param name="i">Index of docker.</param>
        /// <param name="spacerL">Spacer for the left side.</param>
        /// <param name="spacerR">Spacer for the right side.</param>
        protected override void AddSpacersToDocker(int i,
                                                   ViewLayoutMetricSpacer spacerL,
                                                   ViewLayoutMetricSpacer spacerR)
        {
            // Get the indexed instance
            ViewLayoutDocker viewDocker = _viewDockers[i];

            // Add them into the view docker instance
            viewDocker.Add(spacerL, ViewDockStyle.Left);
            viewDocker.Add(spacerR, ViewDockStyle.Right);
        }
        #endregion
    }
}
