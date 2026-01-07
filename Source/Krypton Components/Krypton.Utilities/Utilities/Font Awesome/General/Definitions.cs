#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Utilities;

#region Enum FontAwesomeIcon

/// <summary>
/// Represents Font Awesome icon names. This enum contains commonly used Font Awesome icons.
/// For icons not in this enum, use the string-based methods in FontAwesomeHelper.
/// </summary>
public enum FontAwesomeIcon
{
    /// <summary>
    /// Represents a home icon, typically used for navigation to the main page or dashboard.
    /// </summary>
    Home,

    /// <summary>
    /// Represents a single user icon, typically used for user profiles or account settings.
    /// </summary>
    User,

    /// <summary>
    /// Represents multiple users icon, typically used for groups, teams, or user management.
    /// </summary>
    Users,

    /// <summary>
    /// Represents a settings icon, typically used for application or system configuration.
    /// </summary>
    Settings,

    /// <summary>
    /// Represents a cog/gear icon, typically used for settings or configuration options.
    /// </summary>
    Cog,

    /// <summary>
    /// Represents a search icon, typically used for search functionality or find operations.
    /// </summary>
    Search,

    /// <summary>
    /// Represents a close icon, typically used to close dialogs, windows, or dismiss notifications.
    /// </summary>
    Close,

    /// <summary>
    /// Represents a checkmark icon, typically used to indicate success, completion, or approval.
    /// </summary>
    Check,

    /// <summary>
    /// Represents a times/X icon, typically used to indicate cancellation, deletion, or close operations.
    /// </summary>
    Times,

    /// <summary>
    /// Represents a plus icon, typically used for add, create, or expand operations.
    /// </summary>
    Plus,

    /// <summary>
    /// Represents a minus icon, typically used for remove, subtract, or collapse operations.
    /// </summary>
    Minus,

    /// <summary>
    /// Represents an edit icon, typically used for editing or modifying content.
    /// </summary>
    Edit,

    /// <summary>
    /// Represents a trash/delete icon, typically used for deletion operations.
    /// </summary>
    Trash,

    /// <summary>
    /// Represents a save icon, typically used for saving data or files.
    /// </summary>
    Save,

    /// <summary>
    /// Represents a folder icon, typically used for directories or folder navigation.
    /// </summary>
    Folder,

    /// <summary>
    /// Represents a file icon, typically used for documents or file operations.
    /// </summary>
    File,

    /// <summary>
    /// Represents an image icon, typically used for pictures, photos, or image files.
    /// </summary>
    Image,

    /// <summary>
    /// Represents a download icon, typically used for downloading files or data.
    /// </summary>
    Download,

    /// <summary>
    /// Represents an upload icon, typically used for uploading files or data.
    /// </summary>
    Upload,

    /// <summary>
    /// Represents a print icon, typically used for printing documents or pages.
    /// </summary>
    Print,

    /// <summary>
    /// Represents a copy icon, typically used for copying content or data.
    /// </summary>
    Copy,

    /// <summary>
    /// Represents a cut icon, typically used for cutting content or data.
    /// </summary>
    Cut,

    /// <summary>
    /// Represents a paste icon, typically used for pasting content or data.
    /// </summary>
    Paste,

    /// <summary>
    /// Represents an undo icon, typically used for undoing the last action.
    /// </summary>
    Undo,

    /// <summary>
    /// Represents a redo icon, typically used for redoing the last undone action.
    /// </summary>
    Redo,

    /// <summary>
    /// Represents a refresh icon, typically used for refreshing or reloading content.
    /// </summary>
    Refresh,

    /// <summary>
    /// Represents a sync icon, typically used for synchronization operations.
    /// </summary>
    Sync,

    /// <summary>
    /// Represents a play icon, typically used for starting media playback or processes.
    /// </summary>
    Play,

    /// <summary>
    /// Represents a pause icon, typically used for pausing media playback or processes.
    /// </summary>
    Pause,

    /// <summary>
    /// Represents a stop icon, typically used for stopping media playback or processes.
    /// </summary>
    Stop,

    /// <summary>
    /// Represents a forward icon, typically used for moving forward or fast-forwarding.
    /// </summary>
    Forward,

    /// <summary>
    /// Represents a backward icon, typically used for moving backward or rewinding.
    /// </summary>
    Backward,

    /// <summary>
    /// Represents a next icon, typically used for navigating to the next item or page.
    /// </summary>
    Next,

    /// <summary>
    /// Represents a previous icon, typically used for navigating to the previous item or page.
    /// </summary>
    Previous,

    /// <summary>
    /// Represents a first icon, typically used for navigating to the first item or page.
    /// </summary>
    First,

    /// <summary>
    /// Represents a last icon, typically used for navigating to the last item or page.
    /// </summary>
    Last,

    /// <summary>
    /// Represents an expand icon, typically used for expanding content or sections.
    /// </summary>
    Expand,

    /// <summary>
    /// Represents a collapse icon, typically used for collapsing content or sections.
    /// </summary>
    Collapse,

    /// <summary>
    /// Represents a chevron up icon, typically used for upward navigation or expanding upward.
    /// </summary>
    ChevronUp,

    /// <summary>
    /// Represents a chevron down icon, typically used for downward navigation or expanding downward.
    /// </summary>
    ChevronDown,

    /// <summary>
    /// Represents a chevron left icon, typically used for leftward navigation or expanding leftward.
    /// </summary>
    ChevronLeft,

    /// <summary>
    /// Represents a chevron right icon, typically used for rightward navigation or expanding rightward.
    /// </summary>
    ChevronRight,

    /// <summary>
    /// Represents an arrow up icon, typically used for upward movement or scrolling up.
    /// </summary>
    ArrowUp,

    /// <summary>
    /// Represents an arrow down icon, typically used for downward movement or scrolling down.
    /// </summary>
    ArrowDown,

    /// <summary>
    /// Represents an arrow left icon, typically used for leftward movement or navigation.
    /// </summary>
    ArrowLeft,

    /// <summary>
    /// Represents an arrow right icon, typically used for rightward movement or navigation.
    /// </summary>
    ArrowRight,

    /// <summary>
    /// Represents an info icon, typically used for displaying informational messages or tooltips.
    /// </summary>
    Info,

    /// <summary>
    /// Represents a warning icon, typically used for displaying warning messages or alerts.
    /// </summary>
    Warning,

    /// <summary>
    /// Represents an error icon, typically used for displaying error messages or critical alerts.
    /// </summary>
    Error,

    /// <summary>
    /// Represents a question icon, typically used for help, FAQ, or confirmation dialogs.
    /// </summary>
    Question,

    /// <summary>
    /// Represents a star icon, typically used for favorites, ratings, or featured items.
    /// </summary>
    Star,

    /// <summary>
    /// Represents a heart icon, typically used for favorites, likes, or love actions.
    /// </summary>
    Heart,

    /// <summary>
    /// Represents a bookmark icon, typically used for saving or marking items for later.
    /// </summary>
    Bookmark,

    /// <summary>
    /// Represents a calendar icon, typically used for date selection or scheduling.
    /// </summary>
    Calendar,

    /// <summary>
    /// Represents a clock icon, typically used for time display or time-related operations.
    /// </summary>
    Clock,

    /// <summary>
    /// Represents an envelope icon, typically used for email or messaging functionality.
    /// </summary>
    Envelope,

    /// <summary>
    /// Represents a phone icon, typically used for phone calls or contact information.
    /// </summary>
    Phone,

    /// <summary>
    /// Represents a globe icon, typically used for web, internet, or global operations.
    /// </summary>
    Globe,

    /// <summary>
    /// Represents a link icon, typically used for hyperlinks or URL connections.
    /// </summary>
    Link,

    /// <summary>
    /// Represents an unlink icon, typically used for removing links or breaking connections.
    /// </summary>
    Unlink,

    /// <summary>
    /// Represents a lock icon, typically used for security, protection, or locked states.
    /// </summary>
    Lock,

    /// <summary>
    /// Represents an unlock icon, typically used for unlocking or accessible states.
    /// </summary>
    Unlock,

    /// <summary>
    /// Represents an eye icon, typically used for viewing or showing content.
    /// </summary>
    Eye,

    /// <summary>
    /// Represents an eye-slash icon, typically used for hiding content or password visibility toggle.
    /// </summary>
    EyeSlash,

    /// <summary>
    /// Represents a filter icon, typically used for filtering or sorting data.
    /// </summary>
    Filter,

    /// <summary>
    /// Represents a sort icon, typically used for sorting data in ascending or descending order.
    /// </summary>
    Sort,

    /// <summary>
    /// Represents a sort up icon, typically used for sorting data in ascending order.
    /// </summary>
    SortUp,

    /// <summary>
    /// Represents a sort down icon, typically used for sorting data in descending order.
    /// </summary>
    SortDown,

    /// <summary>
    /// Represents a list icon, typically used for list view or displaying items in a list format.
    /// </summary>
    List,

    /// <summary>
    /// Represents a grid icon, typically used for grid view or displaying items in a grid format.
    /// </summary>
    Grid,

    /// <summary>
    /// Represents bars icon, typically used for menus, navigation, or bar charts.
    /// </summary>
    Bars,

    /// <summary>
    /// Represents a table/grid icon, typically used for table view or grid layouts.
    /// </summary>
    Th,

    /// <summary>
    /// Represents a list view icon, typically used for displaying items in a list format.
    /// </summary>
    ThList,

    /// <summary>
    /// Represents align left icon, typically used for left text alignment.
    /// </summary>
    AlignLeft,

    /// <summary>
    /// Represents align center icon, typically used for center text alignment.
    /// </summary>
    AlignCenter,

    /// <summary>
    /// Represents align right icon, typically used for right text alignment.
    /// </summary>
    AlignRight,

    /// <summary>
    /// Represents a bold icon, typically used for bold text formatting.
    /// </summary>
    Bold,

    /// <summary>
    /// Represents an italic icon, typically used for italic text formatting.
    /// </summary>
    Italic,

    /// <summary>
    /// Represents an underline icon, typically used for underlined text formatting.
    /// </summary>
    Underline,

    /// <summary>
    /// Represents a code icon, typically used for code blocks, programming, or technical content.
    /// </summary>
    Code,

    /// <summary>
    /// Represents a terminal icon, typically used for command line interfaces or terminal access.
    /// </summary>
    Terminal,

    /// <summary>
    /// Represents a database icon, typically used for database operations or data storage.
    /// </summary>
    Database,

    /// <summary>
    /// Represents a server icon, typically used for server operations or hosting.
    /// </summary>
    Server,

    /// <summary>
    /// Represents a cloud icon, typically used for cloud storage or cloud services.
    /// </summary>
    Cloud,

    /// <summary>
    /// Represents a Wi-Fi icon, typically used for wireless network connectivity.
    /// </summary>
    Wifi,

    /// <summary>
    /// Represents a Bluetooth icon, typically used for Bluetooth connectivity.
    /// </summary>
    Bluetooth,

    /// <summary>
    /// Represents a full battery icon, typically used for battery status indicators.
    /// </summary>
    BatteryFull,

    /// <summary>
    /// Represents a half battery icon, typically used for battery status indicators.
    /// </summary>
    BatteryHalf,

    /// <summary>
    /// Represents an empty battery icon, typically used for low battery status indicators.
    /// </summary>
    BatteryEmpty,

    /// <summary>
    /// Represents a signal icon, typically used for signal strength or connectivity indicators.
    /// </summary>
    Signal,

    /// <summary>
    /// Represents a volume up icon, typically used for increasing volume or unmuting audio.
    /// </summary>
    VolumeUp,

    /// <summary>
    /// Represents a volume down icon, typically used for decreasing volume.
    /// </summary>
    VolumeDown,

    /// <summary>
    /// Represents a volume mute icon, typically used for muting audio.
    /// </summary>
    VolumeMute,

    /// <summary>
    /// Represents a music icon, typically used for music playback or audio content.
    /// </summary>
    Music,

    /// <summary>
    /// Represents a video icon, typically used for video playback or video content.
    /// </summary>
    Video,

    /// <summary>
    /// Represents a camera icon, typically used for photography or camera functionality.
    /// </summary>
    Camera,

    /// <summary>
    /// Represents a microphone icon, typically used for audio recording or voice input.
    /// </summary>
    Microphone,

    /// <summary>
    /// Represents headphones icon, typically used for audio output or listening devices.
    /// </summary>
    Headphones,

    /// <summary>
    /// Represents a desktop icon, typically used for desktop computers or desktop applications.
    /// </summary>
    Desktop,

    /// <summary>
    /// Represents a laptop icon, typically used for laptop computers or portable devices.
    /// </summary>
    Laptop,

    /// <summary>
    /// Represents a tablet icon, typically used for tablet devices.
    /// </summary>
    Tablet,

    /// <summary>
    /// Represents a mobile icon, typically used for mobile phones or mobile devices.
    /// </summary>
    Mobile,

    /// <summary>
    /// Represents a keyboard icon, typically used for keyboard input or typing.
    /// </summary>
    Keyboard,

    /// <summary>
    /// Represents a mouse icon, typically used for mouse input or pointing devices.
    /// </summary>
    Mouse,

    /// <summary>
    /// Represents a gamepad icon, typically used for gaming controllers or game-related functionality.
    /// </summary>
    Gamepad,

    /// <summary>
    /// Represents a TV icon, typically used for television or display devices.
    /// </summary>
    Tv,

    /// <summary>
    /// Represents a radio icon, typically used for radio or audio broadcasting.
    /// </summary>
    Radio,

    /// <summary>
    /// Represents a lightbulb icon, typically used for ideas, suggestions, or lighting.
    /// </summary>
    Lightbulb,

    /// <summary>
    /// Represents a flash icon, typically used for lightning, speed, or flash functionality.
    /// </summary>
    Flash,

    /// <summary>
    /// Represents a fire icon, typically used for fire, heat, or trending content.
    /// </summary>
    Fire,

    /// <summary>
    /// Represents a water icon, typically used for water, liquid, or fluid-related content.
    /// </summary>
    Water,

    /// <summary>
    /// Represents a snowflake icon, typically used for winter, cold, or weather-related content.
    /// </summary>
    Snowflake,

    /// <summary>
    /// Represents a sun icon, typically used for sunny weather, day time, or brightness.
    /// </summary>
    Sun,

    /// <summary>
    /// Represents a moon icon, typically used for night time, dark mode, or nighttime operations.
    /// </summary>
    Moon,

    /// <summary>
    /// Represents a cloud with sun icon, typically used for partly cloudy weather.
    /// </summary>
    CloudSun,

    /// <summary>
    /// Represents a cloud with rain icon, typically used for rainy weather.
    /// </summary>
    CloudRain,

    /// <summary>
    /// Represents an umbrella icon, typically used for rain protection or weather-related content.
    /// </summary>
    Umbrella,

    /// <summary>
    /// Represents a shield icon, typically used for security, protection, or defense.
    /// </summary>
    Shield,

    /// <summary>
    /// Represents a flag icon, typically used for flags, markers, or country indicators.
    /// </summary>
    Flag,

    /// <summary>
    /// Represents a trophy icon, typically used for achievements, awards, or competitions.
    /// </summary>
    Trophy,

    /// <summary>
    /// Represents a medal icon, typically used for achievements, awards, or recognition.
    /// </summary>
    Medal,

    /// <summary>
    /// Represents a gift icon, typically used for gifts, presents, or special offers.
    /// </summary>
    Gift,

    /// <summary>
    /// Represents a shopping cart icon, typically used for e-commerce or shopping functionality.
    /// </summary>
    ShoppingCart,

    /// <summary>
    /// Represents a credit card icon, typically used for payment or financial transactions.
    /// </summary>
    CreditCard,

    /// <summary>
    /// Represents a money icon, typically used for currency, payment, or financial content.
    /// </summary>
    Money,

    /// <summary>
    /// Represents a dollar icon, typically used for US dollar currency or dollar amounts.
    /// </summary>
    Dollar,

    /// <summary>
    /// Represents a euro icon, typically used for Euro currency or euro amounts.
    /// </summary>
    Euro,

    /// <summary>
    /// Represents a pound icon, typically used for British pound currency or pound amounts.
    /// </summary>
    Pound,

    /// <summary>
    /// Represents a Bitcoin icon, typically used for Bitcoin cryptocurrency.
    /// </summary>
    Bitcoin,

    /// <summary>
    /// Represents a PayPal icon, typically used for PayPal payment service.
    /// </summary>
    Paypal,

    /// <summary>
    /// Represents an Amazon icon, typically used for Amazon services or branding.
    /// </summary>
    Amazon,

    /// <summary>
    /// Represents an Apple icon, typically used for Apple products or branding.
    /// </summary>
    Apple,

    /// <summary>
    /// Represents a Windows icon, typically used for Microsoft Windows operating system or branding.
    /// </summary>
    Windows,

    /// <summary>
    /// Represents a Linux icon, typically used for Linux operating system or branding.
    /// </summary>
    Linux,

    /// <summary>
    /// Represents an Android icon, typically used for Android operating system or branding.
    /// </summary>
    Android,

    /// <summary>
    /// Represents a Chrome icon, typically used for Google Chrome browser or branding.
    /// </summary>
    Chrome,

    /// <summary>
    /// Represents a Firefox icon, typically used for Mozilla Firefox browser or branding.
    /// </summary>
    Firefox,

    /// <summary>
    /// Represents an Edge icon, typically used for Microsoft Edge browser or branding.
    /// </summary>
    Edge,

    /// <summary>
    /// Represents a Safari icon, typically used for Apple Safari browser or branding.
    /// </summary>
    Safari,

    /// <summary>
    /// Represents a GitHub icon, typically used for GitHub service or branding.
    /// </summary>
    Github,

    /// <summary>
    /// Represents a GitLab icon, typically used for GitLab service or branding.
    /// </summary>
    Gitlab,

    /// <summary>
    /// Represents a Bitbucket icon, typically used for Bitbucket service or branding.
    /// </summary>
    Bitbucket,

    /// <summary>
    /// Represents a Stack Overflow icon, typically used for Stack Overflow service or branding.
    /// </summary>
    StackOverflow,

    /// <summary>
    /// Represents a Twitter icon, typically used for Twitter/X social media or branding.
    /// </summary>
    Twitter,

    /// <summary>
    /// Represents a Facebook icon, typically used for Facebook social media or branding.
    /// </summary>
    Facebook,

    /// <summary>
    /// Represents an Instagram icon, typically used for Instagram social media or branding.
    /// </summary>
    Instagram,

    /// <summary>
    /// Represents a LinkedIn icon, typically used for LinkedIn professional network or branding.
    /// </summary>
    Linkedin,

    /// <summary>
    /// Represents a YouTube icon, typically used for YouTube video platform or branding.
    /// </summary>
    Youtube,

    /// <summary>
    /// Represents a Twitch icon, typically used for Twitch streaming platform or branding.
    /// </summary>
    Twitch,

    /// <summary>
    /// Represents a Discord icon, typically used for Discord communication platform or branding.
    /// </summary>
    Discord,

    /// <summary>
    /// Represents a Steam icon, typically used for Steam gaming platform or branding.
    /// </summary>
    Steam,

    /// <summary>
    /// Represents a Spotify icon, typically used for Spotify music streaming service or branding.
    /// </summary>
    Spotify,

    /// <summary>
    /// Represents a Reddit icon, typically used for Reddit social platform or branding.
    /// </summary>
    Reddit,

    /// <summary>
    /// Represents a Pinterest icon, typically used for Pinterest social platform or branding.
    /// </summary>
    Pinterest,

    /// <summary>
    /// Represents a Snapchat icon, typically used for Snapchat social media or branding.
    /// </summary>
    Snapchat,

    /// <summary>
    /// Represents a TikTok icon, typically used for TikTok social media platform or branding.
    /// </summary>
    Tiktok,

    /// <summary>
    /// Represents a WhatsApp icon, typically used for WhatsApp messaging service or branding.
    /// </summary>
    Whatsapp,

    /// <summary>
    /// Represents a Telegram icon, typically used for Telegram messaging service or branding.
    /// </summary>
    Telegram,

    /// <summary>
    /// Represents a Skype icon, typically used for Skype communication service or branding.
    /// </summary>
    Skype,

    /// <summary>
    /// Represents a Zoom icon, typically used for Zoom video conferencing service or branding.
    /// </summary>
    Zoom,

    /// <summary>
    /// Represents a Microsoft icon, typically used for Microsoft services or branding.
    /// </summary>
    Microsoft,

    /// <summary>
    /// Represents a Google icon, typically used for Google services or branding.
    /// </summary>
    Google,

    /// <summary>
    /// Represents a Dropbox icon, typically used for Dropbox cloud storage service or branding.
    /// </summary>
    Dropbox,

    /// <summary>
    /// Represents a OneDrive icon, typically used for Microsoft OneDrive cloud storage service or branding.
    /// </summary>
    Onedrive,

    /// <summary>
    /// Represents a Google Drive icon, typically used for Google Drive cloud storage service or branding.
    /// </summary>
    GoogleDrive
}

#endregion

#region Enum FontAwesomeStyle

/// <summary>
/// Represents the different Font Awesome icon styles.
/// </summary>
public enum FontAwesomeStyle
{
    /// <summary>
    /// Font Awesome Solid style (fas).
    /// </summary>
    Solid,

    /// <summary>
    /// Font Awesome Regular style (far).
    /// </summary>
    Regular,

    /// <summary>
    /// Font Awesome Brands style (fab).
    /// </summary>
    Brands,

    /// <summary>
    /// Font Awesome Light style (fal) - Pro only.
    /// </summary>
    Light,

    /// <summary>
    /// Font Awesome Thin style (fat) - Pro only.
    /// </summary>
    Thin,

    /// <summary>
    /// Font Awesome Duotone style (fad) - Pro only.
    /// </summary>
    Duotone
}

#endregion
