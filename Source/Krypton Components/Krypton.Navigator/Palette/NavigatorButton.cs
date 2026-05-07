#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

// ReSharper disable VirtualMemberCallInConstructor
namespace Krypton.Navigator;

/// <summary>
/// Storage for button related properties.
/// </summary>
public class NavigatorButton : Storage
{
    #region Static Fields

    private const Keys DEFAULT_SHORTCUT_PREVIOUS = (Keys.Control | Keys.Shift | Keys.F6);
    private const Keys DEFAULT_SHORTCUT_NEXT = (Keys.Control | Keys.F6);
    private const Keys DEFAULT_SHORTCUT_CONTEXT = (Keys.Control | Keys.Alt | Keys.Down);
    private const Keys DEFAULT_SHORTCUT_CLOSE = (Keys.Control | Keys.F4);

    #endregion

    #region Instance Fields
    private readonly KryptonNavigator _navigator;
    private DirectionButtonAction _actionPrevious;
    private ButtonDisplay _displayPrevious;
    private DirectionButtonAction _actionNext;
    private ButtonDisplay _displayNext;
    private ContextButtonAction _actionContext;
    private ButtonDisplay _displayContext;
    private CloseButtonAction _actionClosed;
    private ButtonDisplay _displayClosed;
    private ButtonDisplayLogic _displayLogic;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the NavigatorButton class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public NavigatorButton([DisallowNull] KryptonNavigator navigator,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(navigator is not null);

        // Remember back reference
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create collection for use defined and fixed buttons
        ButtonSpecs = new NavigatorButtonSpecCollection(navigator);
        FixedSpecs = new NavFixedButtonSpecCollection(navigator);

        // Create the fixed buttons
        PreviousButton = new ButtonSpecNavPrevious(_navigator);
        NextButton = new ButtonSpecNavNext(_navigator);
        ContextButton = new ButtonSpecNavContext(_navigator);
        CloseButton = new ButtonSpecNavClose(_navigator);
        FormCloseButton = new ButtonSpecNavFormClose(_navigator);
        FormMaximizeButton = new ButtonSpecNavFormMaximize(_navigator);
        FormMinimizeButton = new ButtonSpecNavFormMinimize(_navigator);

        // Hook into the click events for the buttons
        PreviousButton.Click += OnPreviousClick;
        NextButton.Click += OnNextClick;
        ContextButton.Click += OnContextClick;
        CloseButton.Click += OnCloseClick;
        FormCloseButton.Click += OnCloseButtonClick;
        FormMinimizeButton.Click += OnMinimizeButtonClick;
        FormMaximizeButton.Click += OnMaximizeButtonClick;

        // Add fixed buttons into the display collection
        if (_navigator is { Owner: not null, ControlKryptonFormFeatures: false })
        {
            FixedSpecs.AddRange(new ButtonSpecNavFixed[] { PreviousButton, NextButton, ContextButton, CloseButton, FormMinimizeButton, FormMaximizeButton, FormCloseButton });
        }
        else
        {
            FixedSpecs.AddRange(new ButtonSpecNavFixed[] { PreviousButton, NextButton, ContextButton, CloseButton });
        }

        // Default fields
        _displayLogic = ButtonDisplayLogic.Context;
        ContextMenuMapText = MapKryptonPageText.TextTitle;
        ContextMenuMapImage = MapKryptonPageImage.Small;
        _actionClosed = CloseButtonAction.RemovePageAndDispose;
        _actionContext = ContextButtonAction.SelectPage;
        _actionPrevious = _actionNext = DirectionButtonAction.ModeAppropriateAction;
        _displayPrevious = _displayNext = _displayContext = _displayClosed = ButtonDisplay.Logic;
        CloseButtonShortcut = DEFAULT_SHORTCUT_CLOSE;
        ContextButtonShortcut = DEFAULT_SHORTCUT_CONTEXT;
        NextButtonShortcut = DEFAULT_SHORTCUT_NEXT;
        PreviousButtonShortcut = DEFAULT_SHORTCUT_PREVIOUS;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ((ButtonSpecs!.Count == 0) &&
                                       PreviousButton.IsDefault &&
                                       (PreviousButtonAction == DirectionButtonAction.ModeAppropriateAction) &&
                                       (PreviousButtonDisplay == ButtonDisplay.Logic) &&
                                       (PreviousButtonShortcut == DEFAULT_SHORTCUT_PREVIOUS) &&
                                       NextButton.IsDefault &&
                                       (NextButtonAction == DirectionButtonAction.ModeAppropriateAction) &&
                                       (NextButtonDisplay == ButtonDisplay.Logic) &&
                                       (NextButtonShortcut == DEFAULT_SHORTCUT_NEXT) &&
                                       ContextButton.IsDefault &&
                                       (ContextButtonDisplay == ButtonDisplay.Logic) &&
                                       (ContextButtonShortcut == DEFAULT_SHORTCUT_CONTEXT) &&
                                       (ContextMenuMapText == MapKryptonPageText.TextTitle) &&
                                       (ContextMenuMapImage == MapKryptonPageImage.Small) &&
                                       CloseButton.IsDefault &&
                                       (CloseButtonAction == CloseButtonAction.RemovePageAndDispose) &&
                                       (CloseButtonDisplay == ButtonDisplay.Logic) &&
                                       (CloseButtonShortcut == DEFAULT_SHORTCUT_CLOSE) &&
                                       (ButtonDisplayLogic == ButtonDisplayLogic.Context));

    #endregion

    #region ButtonSpecs
    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public NavigatorButtonSpecCollection? ButtonSpecs { get; }

    #endregion

    #region PreviousButton
    /// <summary>
    /// Gets access to the previous button specification.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Previous button specification.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ButtonSpecNavPrevious PreviousButton { get; }

    private bool ShouldSerializePreviousButton() => !PreviousButton.IsDefault;

    #endregion

    #region PreviousButtonAction
    /// <summary>
    /// Gets and sets the action to take when the previous button is clicked.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Action to take when the previous button is clicked.")]
    //[DefaultValue(typeof(DirectionButtonAction), "Mode Appropriate Action")]
    public DirectionButtonAction PreviousButtonAction
    {
        get => _actionPrevious;

        set
        {
            if (_actionPrevious != value)
            {
                _actionPrevious = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(PreviousButtonAction));
            }
        }
    }
    #endregion

    #region PreviousButtonDisplay
    /// <summary>
    /// Gets and set the logic used to decide how to show the previous button.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Logic used to decide if previous button is Displayed.")]
    //[DefaultValue(typeof(ButtonDisplay), "Logic")]
    public ButtonDisplay PreviousButtonDisplay
    {
        get => _displayPrevious;

        set
        {
            if (_displayPrevious != value)
            {
                _displayPrevious = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(PreviousButtonDisplay));
            }
        }
    }
    #endregion

    #region PreviousButtonShortcut
    /// <summary>
    /// Gets access to the shortcut for invoking the previous action.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Shortcut for invoking the previous action.")]
    //[DefaultValue(typeof(Keys), "F6, Shift, Control")]
    public Keys PreviousButtonShortcut { get; set; }

    private bool ShouldSerializePreviousButtonShortcut() => (PreviousButtonShortcut != DEFAULT_SHORTCUT_PREVIOUS);

    /// <summary>
    /// Resets the PreviousButtonShortcut property to its default value.
    /// </summary>
    public void ResetPreviousButtonShortcut() => PreviousButtonShortcut = DEFAULT_SHORTCUT_PREVIOUS;
    #endregion

    #region NextButton
    /// <summary>
    /// Gets access to the next button specification.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Next button specification.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ButtonSpecNavNext NextButton { get; }

    private bool ShouldSerializeNextButton() => !NextButton.IsDefault;

    #endregion

    #region NextButtonAction
    /// <summary>
    /// Gets and sets the action to take when the next button is clicked.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Action to take when the next button is clicked.")]
    //[DefaultValue(typeof(DirectionButtonAction), "Mode Appropriate Action")]
    public DirectionButtonAction NextButtonAction
    {
        get => _actionNext;

        set
        {
            if (_actionNext != value)
            {
                _actionNext = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(NextButtonAction));
            }
        }
    }
    #endregion

    #region NextButtonDisplay
    /// <summary>
    /// Gets and set the logic used to decide how to show the next button.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Logic used to decide if next button is Displayed.")]
    //[DefaultValue(typeof(ButtonDisplay), "Logic")]
    public ButtonDisplay NextButtonDisplay
    {
        get => _displayNext;

        set
        {
            if (_displayNext != value)
            {
                _displayNext = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(NextButtonDisplay));
            }
        }
    }
    #endregion

    #region NextButtonShortcut
    /// <summary>
    /// Gets access to the shortcut for invoking the next action.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Shortcut for invoking the next action.")]
    //[DefaultValue(typeof(Keys), "F6, Control")]
    public Keys NextButtonShortcut { get; set; }

    private bool ShouldSerializeNextButtonShortcut() => (NextButtonShortcut != DEFAULT_SHORTCUT_NEXT);

    /// <summary>
    /// Resets the NextButtonShortcut property to its default value.
    /// </summary>
    public void ResetNextButtonShortcut() => NextButtonShortcut = DEFAULT_SHORTCUT_NEXT;
    #endregion

    #region ContextButton
    /// <summary>
    /// Gets access to the context button specification.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Context button specification.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ButtonSpecNavContext ContextButton { get; }

    private bool ShouldSerializeContextButton() => !ContextButton.IsDefault;

    #endregion

    #region ContextButtonAction
    /// <summary>
    /// Gets and sets the action to take when the context button is clicked.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Action to take when the context button is clicked.")]
    //[DefaultValue(typeof(ContextButtonAction), "Select Page")]
    public ContextButtonAction ContextButtonAction
    {
        get => _actionContext;

        set
        {
            if (_actionContext != value)
            {
                _actionContext = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(ContextButtonAction));
            }
        }
    }
    #endregion

    #region ContextButtonDisplay
    /// <summary>
    /// Gets and set the logic used to decide how to show the context button.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Logic used to decide if context button is Displayed.")]
    //[DefaultValue(typeof(ButtonDisplay), "Logic")]
    public ButtonDisplay ContextButtonDisplay
    {
        get => _displayContext;

        set
        {
            if (_displayContext != value)
            {
                _displayContext = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(ContextButtonDisplay));
            }
        }
    }
    #endregion

    #region ContextButtonShortcut
    /// <summary>
    /// Gets access to the shortcut for invoking the context action.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Shortcut for invoking the context action.")]
    //[DefaultValue(typeof(Keys), "Down, Alt, Control")]
    public Keys ContextButtonShortcut { get; set; }

    private bool ShouldSerializeContextButtonShortcut() => (ContextButtonShortcut != DEFAULT_SHORTCUT_CONTEXT);

    /// <summary>
    /// Resets the ContextButtonShortcut property to its default value.
    /// </summary>
    public void ResetContextButtonShortcut() => ContextButtonShortcut = DEFAULT_SHORTCUT_CONTEXT;
    #endregion

    #region ContextMenuMapText
    /// <summary>
    /// Gets and set the mapping used to generate context menu item image.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Mapping used to generate context menu item image.")]
    //[DefaultValue(typeof(MapKryptonPageText), "Text - Title")]
    public MapKryptonPageText ContextMenuMapText { get; set; }

    #endregion

    #region ContextMenuMapImage
    /// <summary>
    /// Gets and set the mapping used to generate context menu item text.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Mapping used to generate context menu item text.")]
    //[DefaultValue(typeof(MapKryptonPageImage), "Small")]
    public MapKryptonPageImage ContextMenuMapImage { get; set; }

    #endregion

    #region CloseButton
    /// <summary>
    /// Gets access to the close button specification.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Close button specification.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ButtonSpecNavClose CloseButton { get; }

    private bool ShouldSerializeCloseButton() => !CloseButton.IsDefault;

    #endregion

    #region CloseButtonAction
    /// <summary>
    /// Gets and sets the action to take when the close button is clicked.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Action to take when the close button is clicked.")]
    //[DefaultValue(typeof(CloseButtonAction), "RemovePage & Dispose")]
    public CloseButtonAction CloseButtonAction
    {
        get => _actionClosed;

        set
        {
            if (_actionClosed != value)
            {
                _actionClosed = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(CloseButtonAction));
            }
        }
    }
    #endregion

    #region CloseButtonDisplay
    /// <summary>
    /// Gets and set the logic used to decide how to show the close button.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Logic used to decide if close button is Displayed.")]
    //[DefaultValue(typeof(ButtonDisplay), "Logic")]
    public ButtonDisplay CloseButtonDisplay
    {
        get => _displayClosed;

        set
        {
            if (_displayClosed != value)
            {
                _displayClosed = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(CloseButtonDisplay));
            }
        }
    }
    #endregion

    #region CloseButtonShortcut
    /// <summary>
    /// Gets access to the shortcut for invoking the close action.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Shortcut for invoking the close action.")]
    //[DefaultValue(typeof(Keys), "F4, Control")]
    public Keys CloseButtonShortcut { get; set; }

    private bool ShouldSerializeCloseButtonShortcut() => (CloseButtonShortcut != DEFAULT_SHORTCUT_CLOSE);

    /// <summary>
    /// Resets the CloseButtonShortcut property to its default value.
    /// </summary>
    public void ResetCloseButtonShortcut() => CloseButtonShortcut = DEFAULT_SHORTCUT_CLOSE;
    #endregion

    #region FormCloseButton

    /// <summary>
    /// Gets access to the form close button specification.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Form close button specification.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ButtonSpecNavFormClose FormCloseButton { get; }

    private bool ShouldSerializeFormCloseButton() => !FormCloseButton.IsDefault;

    #endregion

    #region FormMaximizeButton

    /// <summary>
    /// Gets access to the form maximize button specification.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Form maximize button specification.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ButtonSpecNavFormMaximize FormMaximizeButton { get; }

    private bool ShouldSerializeFormMaximizeButton() => !FormMaximizeButton.IsDefault;

    #endregion

    #region FormMinimizeButton

    /// <summary>
    /// Gets access to the form minimize button specification.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Form minimize button specification.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ButtonSpecNavFormMinimize FormMinimizeButton { get; }

    private bool ShouldSerializeFormMinimizeButton() => !FormMinimizeButton.IsDefault;

    #endregion

    #region ButtonDisplayLogic
    /// <summary>
    /// Gets and sets the logic used to control button display.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Define the logic used to control button display.")]
    //[DefaultValue(typeof(ButtonDisplayLogic), "Context")]
    public ButtonDisplayLogic ButtonDisplayLogic
    {
        get => _displayLogic;

        set
        {
            if (_displayLogic != value)
            {
                _displayLogic = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(ButtonDisplayLogic));
            }
        }
    }

    /// <summary>
    /// Resets the ButtonDisplayLogic property to its default value.
    /// </summary>
    public void ResetButtonDisplayLogic() => ButtonDisplayLogic = ButtonDisplayLogic.Context;
    #endregion

    #region Internal
    internal NavFixedButtonSpecCollection? FixedSpecs { get; }

    #endregion

    #region Implementation
    private void OnPreviousClick(object? sender, EventArgs e) => _navigator.PerformPreviousAction();

    private void OnNextClick(object? sender, EventArgs e) => _navigator.PerformNextAction();

    private void OnContextClick(object? sender, EventArgs e) => _navigator.PerformContextAction();

    private void OnCloseClick(object? sender, EventArgs e) => _navigator.PerformCloseAction();

    private void OnMaximizeButtonClick(object? sender, EventArgs e) => throw new NotImplementedException();

    private void OnMinimizeButtonClick(object? sender, EventArgs e) => throw new NotImplementedException();

    private void OnCloseButtonClick(object? sender, EventArgs e) => throw new NotImplementedException();
    #endregion
}