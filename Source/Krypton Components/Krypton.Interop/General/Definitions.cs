#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Interop;

#region Enum ImageresIconID

/// <summary>
/// Image resource IDs found in imageres.dll
/// 
/// This enum provides access to the comprehensive collection of system icons stored in Windows' imageres.dll.
/// These icons are used throughout the Windows operating system and provide a consistent visual language
/// for user interfaces. The icons are organized into logical categories for easier navigation and usage.
/// 
/// Usage: Use with GraphicsExtensions.ExtractIconFromImageres() to extract icons at specific sizes.
/// Example: var shieldIcon = GraphicsExtensions.ExtractIconFromImageres(ImageresIconID.Shield, UACShieldIconSize.Medium);
/// </summary>
public enum ImageresIconID : int
{
    #region System Icons (Security, Users, Files, Folders)

    /// <summary>Standard UAC shield icon - represents elevated permissions or security features</summary>
    Shield = 78,
    /// <summary>Alternative UAC shield icon - variant of the standard shield</summary>
    ShieldAlt = 79,
    /// <summary>Lock icon - represents security, protection, or locked state</summary>
    Lock = 48,
    /// <summary>Unlock icon - represents unlocked state or removing security</summary>
    Unlock = 49,
    /// <summary>Key icon - represents authentication, access, or encryption</summary>
    Key = 50,
    /// <summary>Single user icon - represents individual user account or profile</summary>
    User = 51,
    /// <summary>Multiple users icon - represents group of users or team</summary>
    Users = 52,
    /// <summary>User group icon - represents user groups or organizations</summary>
    UserGroup = 53,
    /// <summary>Computer icon - represents desktop computer or workstation</summary>
    Computer = 54,
    /// <summary>Network icon - represents network connection or internet</summary>
    Network = 55,
    /// <summary>Network drive icon - represents mapped network drive</summary>
    NetworkDrive = 56,
    /// <summary>Network folder icon - represents shared network folder</summary>
    NetworkFolder = 57,
    /// <summary>Generic folder icon - represents directory or folder</summary>
    Folder = 58,
    /// <summary>Open folder icon - represents expanded folder view</summary>
    FolderOpen = 59,
    /// <summary>Closed folder icon - represents collapsed folder view</summary>
    FolderClosed = 60,
    /// <summary>Generic file icon - represents document or file</summary>
    File = 61,
    /// <summary>Text file icon - represents text document or source file</summary>
    FileText = 62,
    /// <summary>Image file icon - represents image file (JPG, PNG, etc.)</summary>
    FileImage = 63,
    /// <summary>Video file icon - represents video file (MP4, AVI, etc.)</summary>
    FileVideo = 64,
    /// <summary>Audio file icon - represents audio file (MP3, WAV, etc.)</summary>
    FileAudio = 65,
    /// <summary>Archive file icon - represents compressed file (ZIP, RAR, etc.)</summary>
    FileArchive = 66,
    /// <summary>Code file icon - represents source code file</summary>
    FileCode = 67,
    /// <summary>PDF file icon - represents Adobe PDF document</summary>
    FilePdf = 68,
    /// <summary>Word document icon - represents Microsoft Word document</summary>
    FileWord = 69,
    /// <summary>Excel spreadsheet icon - represents Microsoft Excel document</summary>
    FileExcel = 70,
    /// <summary>PowerPoint presentation icon - represents Microsoft PowerPoint document</summary>
    FilePowerpoint = 71,
    /// <summary>Access database icon - represents Microsoft Access database</summary>
    FileAccess = 72,
    /// <summary>Outlook icon - represents Microsoft Outlook email</summary>
    FileOutlook = 73,
    /// <summary>Publisher icon - represents Microsoft Publisher document</summary>
    FilePublisher = 74,
    /// <summary>Visio diagram icon - represents Microsoft Visio diagram</summary>
    FileVisio = 75,
    /// <summary>OneNote icon - represents Microsoft OneNote notebook</summary>
    FileOneNote = 76,
    /// <summary>Project icon - represents Microsoft Project document</summary>
    FileProject = 77,

    #endregion

    #region Application Icons (Software, Tools, Productivity)

    /// <summary>Generic application icon - represents software program</summary>
    Application = 1,
    /// <summary>Alternative application icon - variant of generic app</summary>
    ApplicationAlt = 2,
    /// <summary>Generic application icon - represents any software</summary>
    ApplicationGeneric = 3,
    /// <summary>Settings icon - represents configuration or preferences</summary>
    ApplicationSettings = 4,
    /// <summary>Help icon - represents assistance or documentation</summary>
    ApplicationHelp = 5,
    /// <summary>Information icon - represents info or details</summary>
    ApplicationInfo = 6,
    /// <summary>Warning icon - represents caution or alert</summary>
    ApplicationWarning = 7,
    /// <summary>Error icon - represents problem or failure</summary>
    ApplicationError = 8,
    /// <summary>Question icon - represents inquiry or prompt</summary>
    ApplicationQuestion = 9,
    /// <summary>Security icon - represents protection or safety</summary>
    ApplicationSecurity = 10,
    /// <summary>Update icon - represents software update or refresh</summary>
    ApplicationUpdate = 11,
    /// <summary>Install icon - represents software installation</summary>
    ApplicationInstall = 12,
    /// <summary>Uninstall icon - represents software removal</summary>
    ApplicationUninstall = 13,
    /// <summary>Repair icon - represents fixing or maintenance</summary>
    ApplicationRepair = 14,
    /// <summary>Restore icon - represents recovery or rollback</summary>
    ApplicationRestore = 15,
    /// <summary>Backup icon - represents data backup or save</summary>
    ApplicationBackup = 16,
    /// <summary>Sync icon - represents synchronization</summary>
    ApplicationSync = 17,
    /// <summary>Share icon - represents sharing or collaboration</summary>
    ApplicationShare = 18,
    /// <summary>Print icon - represents printing or output</summary>
    ApplicationPrint = 19,
    /// <summary>Scan icon - represents scanning or input</summary>
    ApplicationScan = 20,
    /// <summary>Fax icon - represents faxing or communication</summary>
    ApplicationFax = 21,
    /// <summary>Email icon - represents electronic mail</summary>
    ApplicationEmail = 22,
    /// <summary>Calendar icon - represents scheduling or dates</summary>
    ApplicationCalendar = 23,
    /// <summary>Contacts icon - represents address book or people</summary>
    ApplicationContacts = 24,
    /// <summary>Tasks icon - represents to-do list or activities</summary>
    ApplicationTasks = 25,
    /// <summary>Notes icon - represents text notes or memos</summary>
    ApplicationNotes = 26,
    /// <summary>Journal icon - represents diary or log</summary>
    ApplicationJournal = 27,
    /// <summary>Calculator icon - represents mathematical calculations</summary>
    ApplicationCalculator = 28,
    /// <summary>Clock icon - represents time or duration</summary>
    ApplicationClock = 29,
    /// <summary>Alarm icon - represents alerts or notifications</summary>
    ApplicationAlarm = 30,
    /// <summary>Timer icon - represents countdown or stopwatch</summary>
    ApplicationTimer = 31,
    /// <summary>Stopwatch icon - represents timing or measurement</summary>
    ApplicationStopwatch = 32,
    /// <summary>Schedule icon - represents planning or timeline</summary>
    ApplicationSchedule = 34,
    /// <summary>Meeting icon - represents conference or appointment</summary>
    ApplicationMeeting = 35,
    /// <summary>Conference icon - represents group meeting or call</summary>
    ApplicationConference = 36,
    /// <summary>Presentation icon - represents slideshow or demo</summary>
    ApplicationPresentation = 37,
    /// <summary>Document icon - represents text document</summary>
    ApplicationDocument = 38,
    /// <summary>Spreadsheet icon - represents data table or grid</summary>
    ApplicationSpreadsheet = 39,
    /// <summary>Database icon - represents data storage or records</summary>
    ApplicationDatabase = 40,
    /// <summary>Project icon - represents project management</summary>
    ApplicationProject = 41,
    /// <summary>Diagram icon - represents visual diagram or chart</summary>
    ApplicationDiagram = 42,
    /// <summary>Chart icon - represents data visualization</summary>
    ApplicationChart = 43,
    /// <summary>Graph icon - represents mathematical graph</summary>
    ApplicationGraph = 44,
    /// <summary>Report icon - represents analysis or summary</summary>
    ApplicationReport = 45,
    /// <summary>Form icon - represents data entry or survey</summary>
    ApplicationForm = 46,
    /// <summary>Template icon - represents reusable format</summary>
    ApplicationTemplate = 47,

    #endregion

    #region Media Icons (Audio, Video, Photography, Storage)

    /// <summary>Play icon - represents media playback start</summary>
    MediaPlay = 80,
    /// <summary>Pause icon - represents media playback pause</summary>
    MediaPause = 81,
    /// <summary>Stop icon - represents media playback stop</summary>
    MediaStop = 82,
    /// <summary>Next icon - represents skip to next track</summary>
    MediaNext = 83,
    /// <summary>Previous icon - represents skip to previous track</summary>
    MediaPrevious = 84,
    /// <summary>Rewind icon - represents backward playback</summary>
    MediaRewind = 85,
    /// <summary>Fast forward icon - represents forward playback</summary>
    MediaFastForward = 86,
    /// <summary>Record icon - represents media recording</summary>
    MediaRecord = 87,
    /// <summary>Eject icon - represents ejecting media</summary>
    MediaEject = 88,
    /// <summary>Volume icon - represents audio volume control</summary>
    MediaVolume = 89,
    /// <summary>Volume mute icon - represents muted audio</summary>
    MediaVolumeMute = 90,
    /// <summary>Volume up icon - represents increasing volume</summary>
    MediaVolumeUp = 91,
    /// <summary>Volume down icon - represents decreasing volume</summary>
    MediaVolumeDown = 92,
    /// <summary>Microphone icon - represents audio input</summary>
    MediaMicrophone = 93,
    /// <summary>Microphone mute icon - represents muted microphone</summary>
    MediaMicrophoneMute = 94,
    /// <summary>Headphones icon - represents audio output device</summary>
    MediaHeadphones = 95,
    /// <summary>Speaker icon - represents audio output</summary>
    MediaSpeaker = 96,
    /// <summary>Camera icon - represents photo capture</summary>
    MediaCamera = 97,
    /// <summary>Video icon - represents video recording</summary>
    MediaVideo = 98,
    /// <summary>Photo icon - represents photograph</summary>
    MediaPhoto = 99,
    /// <summary>Picture icon - represents image or photo</summary>
    MediaPicture = 100,
    /// <summary>Gallery icon - represents photo collection</summary>
    MediaGallery = 101,
    /// <summary>Album icon - represents photo album</summary>
    MediaAlbum = 102,
    /// <summary>Playlist icon - represents media playlist</summary>
    MediaPlaylist = 103,
    /// <summary>Library icon - represents media library</summary>
    MediaLibrary = 104,
    /// <summary>Stream icon - represents media streaming</summary>
    MediaStream = 105,
    /// <summary>Broadcast icon - represents live broadcast</summary>
    MediaBroadcast = 106,
    /// <summary>Podcast icon - represents podcast content</summary>
    MediaPodcast = 107,
    /// <summary>Radio icon - represents radio broadcast</summary>
    MediaRadio = 108,
    /// <summary>TV icon - represents television</summary>
    MediaTv = 109,
    /// <summary>DVD icon - represents DVD media</summary>
    MediaDvd = 110,
    /// <summary>CD icon - represents compact disc</summary>
    MediaCd = 111,
    /// <summary>Blu-ray icon - represents Blu-ray disc</summary>
    MediaBluray = 112,
    /// <summary>USB icon - represents USB storage</summary>
    MediaUsb = 113,
    /// <summary>SD card icon - represents SD memory card</summary>
    MediaSd = 114,
    /// <summary>Hard drive icon - represents HDD storage</summary>
    MediaHdd = 115,
    /// <summary>Solid state drive icon - represents SSD storage</summary>
    MediaSsd = 116,
    /// <summary>Cloud icon - represents cloud storage</summary>
    MediaCloud = 117,
    /// <summary>Online icon - represents internet connection</summary>
    MediaOnline = 118,
    /// <summary>Offline icon - represents no connection</summary>
    MediaOffline = 119,
    /// <summary>Sync icon - represents media synchronization</summary>
    MediaSync = 120,
    /// <summary>Download icon - represents file download</summary>
    MediaDownload = 121,
    /// <summary>Upload icon - represents file upload</summary>
    MediaUpload = 122,
    /// <summary>Share icon - represents media sharing</summary>
    MediaShare = 123,
    /// <summary>Burn icon - represents disc burning</summary>
    MediaBurn = 124,
    /// <summary>Rip icon - represents media extraction</summary>
    MediaRip = 125,
    /// <summary>Convert icon - represents media conversion</summary>
    MediaConvert = 126,
    /// <summary>Edit icon - represents media editing</summary>
    MediaEdit = 127,
    /// <summary>Crop icon - represents image cropping</summary>
    MediaCrop = 128,
    /// <summary>Resize icon - represents image resizing</summary>
    MediaResize = 129,
    /// <summary>Rotate icon - represents image rotation</summary>
    MediaRotate = 130,
    /// <summary>Flip icon - represents image flipping</summary>
    MediaFlip = 131,
    /// <summary>Filter icon - represents image filtering</summary>
    MediaFilter = 132,
    /// <summary>Effect icon - represents media effects</summary>
    MediaEffect = 133,
    /// <summary>Overlay icon - represents image overlay</summary>
    MediaOverlay = 134,
    /// <summary>Watermark icon - represents image watermark</summary>
    MediaWatermark = 135,
    /// <summary>Sticker icon - represents digital sticker</summary>
    MediaSticker = 136,
    /// <summary>Emoji icon - represents emoji or emoticon</summary>
    MediaEmoji = 137,
    /// <summary>GIF icon - represents animated GIF</summary>
    MediaGif = 138,
    /// <summary>Meme icon - represents internet meme</summary>
    MediaMeme = 139,
    /// <summary>Viral icon - represents viral content</summary>
    MediaViral = 140,

    #endregion

    #region Communication Icons (Email, Messaging, Calls, Status)

    /// <summary>Communication icon - represents general communication</summary>
    Communication = 141,
    /// <summary>Email icon - represents electronic mail</summary>
    CommunicationEmail = 142,
    /// <summary>SMS icon - represents text messaging</summary>
    CommunicationSms = 143,
    /// <summary>MMS icon - represents multimedia messaging</summary>
    CommunicationMms = 144,
    /// <summary>Chat icon - represents instant messaging</summary>
    CommunicationChat = 145,
    /// <summary>Message icon - represents text message</summary>
    CommunicationMessage = 146,
    /// <summary>Inbox icon - represents incoming messages</summary>
    CommunicationInbox = 147,
    /// <summary>Outbox icon - represents outgoing messages</summary>
    CommunicationOutbox = 148,
    /// <summary>Sent icon - represents sent messages</summary>
    CommunicationSent = 149,
    /// <summary>Draft icon - represents unsent message</summary>
    CommunicationDraft = 150,
    /// <summary>Spam icon - represents unwanted messages</summary>
    CommunicationSpam = 151,
    /// <summary>Junk icon - represents junk mail</summary>
    CommunicationJunk = 152,
    /// <summary>Archive icon - represents archived messages</summary>
    CommunicationArchive = 153,
    /// <summary>Delete icon - represents message deletion</summary>
    CommunicationDelete = 154,
    /// <summary>Reply icon - represents message reply</summary>
    CommunicationReply = 155,
    /// <summary>Reply all icon - represents reply to all</summary>
    CommunicationReplyAll = 156,
    /// <summary>Forward icon - represents message forwarding</summary>
    CommunicationForward = 157,
    /// <summary>Redirect icon - represents message redirection</summary>
    CommunicationRedirect = 158,
    /// <summary>Send icon - represents sending message</summary>
    CommunicationSend = 159,
    /// <summary>Receive icon - represents receiving message</summary>
    CommunicationReceive = 160,
    /// <summary>Sync icon - represents message synchronization</summary>
    CommunicationSync = 161,
    /// <summary>Download icon - represents downloading messages</summary>
    CommunicationDownload = 162,
    /// <summary>Upload icon - represents uploading messages</summary>
    CommunicationUpload = 163,
    /// <summary>Attach icon - represents file attachment</summary>
    CommunicationAttach = 164,
    /// <summary>Detach icon - represents removing attachment</summary>
    CommunicationDetach = 165,
    /// <summary>Link icon - represents hyperlink</summary>
    CommunicationLink = 166,
    /// <summary>Unlink icon - represents removing link</summary>
    CommunicationUnlink = 167,
    /// <summary>Connect icon - represents establishing connection</summary>
    CommunicationConnect = 168,
    /// <summary>Disconnect icon - represents ending connection</summary>
    CommunicationDisconnect = 169,
    /// <summary>Join icon - represents joining conversation</summary>
    CommunicationJoin = 170,
    /// <summary>Leave icon - represents leaving conversation</summary>
    CommunicationLeave = 171,
    /// <summary>Invite icon - represents invitation</summary>
    CommunicationInvite = 172,
    /// <summary>Accept icon - represents accepting invitation</summary>
    CommunicationAccept = 173,
    /// <summary>Decline icon - represents declining invitation</summary>
    CommunicationDecline = 174,
    /// <summary>Block icon - represents blocking user</summary>
    CommunicationBlock = 175,
    /// <summary>Unblock icon - represents unblocking user</summary>
    CommunicationUnblock = 176,
    /// <summary>Mute icon - represents muting notifications</summary>
    CommunicationMute = 177,
    /// <summary>Unmute icon - represents enabling notifications</summary>
    CommunicationUnmute = 178,
    /// <summary>Call icon - represents phone call</summary>
    CommunicationCall = 179,
    /// <summary>End call icon - represents ending call</summary>
    CommunicationEndCall = 180,
    /// <summary>Answer icon - represents answering call</summary>
    CommunicationAnswer = 181,
    /// <summary>Reject icon - represents rejecting call</summary>
    CommunicationReject = 182,
    /// <summary>Hold icon - represents putting call on hold</summary>
    CommunicationHold = 183,
    /// <summary>Resume icon - represents resuming call</summary>
    CommunicationResume = 184,
    /// <summary>Transfer icon - represents call transfer</summary>
    CommunicationTransfer = 185,
    /// <summary>Conference icon - represents conference call</summary>
    CommunicationConference = 186,
    /// <summary>Voicemail icon - represents voice message</summary>
    CommunicationVoicemail = 187,
    /// <summary>Missed call icon - represents missed call</summary>
    CommunicationMissed = 188,
    /// <summary>Busy icon - represents busy status</summary>
    CommunicationBusy = 189,
    /// <summary>Available icon - represents available status</summary>
    CommunicationAvailable = 190,
    /// <summary>Away icon - represents away status</summary>
    CommunicationAway = 191,
    /// <summary>Offline icon - represents offline status</summary>
    CommunicationOffline = 192,
    /// <summary>Online icon - represents online status</summary>
    CommunicationOnline = 193,
    /// <summary>Idle icon - represents idle status</summary>
    CommunicationIdle = 194,
    /// <summary>Do not disturb icon - represents DND status</summary>
    CommunicationDnd = 195,
    /// <summary>Status icon - represents user status</summary>
    CommunicationStatus = 196,
    /// <summary>Presence icon - represents presence indicator</summary>
    CommunicationPresence = 197,
    /// <summary>Profile icon - represents user profile</summary>
    CommunicationProfile = 198,
    /// <summary>Avatar icon - represents user avatar</summary>
    CommunicationAvatar = 199,
    /// <summary>Contact icon - represents contact information</summary>
    CommunicationContact = 200,

    // System status icons
    SystemStatus = 201,
    SystemStatusOk = 202,
    SystemStatusWarning = 203,
    SystemStatusError = 204,
    SystemStatusInfo = 205,
    SystemStatusQuestion = 206,
    SystemStatusCritical = 207,
    SystemStatusFatal = 208,
    SystemStatusUnknown = 209,
    SystemStatusPending = 210,
    SystemStatusProcessing = 211,
    SystemStatusComplete = 212,
    SystemStatusIncomplete = 213,
    SystemStatusPartial = 214,
    SystemStatusFailed = 215,
    SystemStatusSuccess = 216,
    SystemStatusCancel = 217,
    SystemStatusAbort = 218,
    SystemStatusRetry = 219,
    SystemStatusSkip = 220,
    SystemStatusIgnore = 221,
    SystemStatusContinue = 222,
    SystemStatusStop = 223,
    SystemStatusPause = 224,
    SystemStatusResume = 225,
    SystemStatusRestart = 226,
    SystemStatusShutdown = 227,
    SystemStatusSleep = 228,
    SystemStatusHibernate = 229,
    SystemStatusWake = 230,
    SystemStatusLock = 231,
    SystemStatusUnlock = 232,
    SystemStatusLogon = 233,
    SystemStatusLogoff = 234,
    SystemStatusSwitch = 235,
    SystemStatusFast = 236,
    SystemStatusSlow = 237,
    SystemStatusNormal = 238,
    SystemStatusHigh = 239,
    SystemStatusLow = 240,
    SystemStatusMedium = 241,
    SystemStatusMaximum = 242,
    SystemStatusMinimum = 243,
    SystemStatusOptimal = 244,
    SystemStatusPoor = 245,
    SystemStatusGood = 246,
    SystemStatusExcellent = 247,
    SystemStatusFair = 248,
    SystemStatusBad = 249,
    SystemStatusTerrible = 250,
    SystemStatusPerfect = 251,
    SystemStatusAcceptable = 252,
    SystemStatusUnacceptable = 253,
    SystemStatusSatisfactory = 254,
    SystemStatusUnsatisfactory = 255,

    // Action icons
    Action = 256,
    ActionAdd = 257,
    ActionRemove = 258,
    ActionDelete = 259,
    ActionEdit = 260,
    ActionModify = 261,
    ActionChange = 262,
    ActionUpdate = 263,
    ActionRefresh = 264,
    ActionReload = 265,
    ActionReset = 266,
    ActionRestore = 267,
    ActionUndo = 268,
    ActionRedo = 269,
    ActionCopy = 270,
    ActionCut = 271,
    ActionPaste = 272,
    ActionDuplicate = 273,
    ActionClone = 274,
    ActionMove = 275,
    ActionDrag = 276,
    ActionDrop = 277,
    ActionSort = 278,
    ActionFilter = 279,
    ActionSearch = 280,
    ActionFind = 281,
    ActionReplace = 282,
    ActionSelect = 283,
    ActionDeselect = 284,
    ActionSelectAll = 285,
    ActionClear = 286,
    ActionClearAll = 287,
    ActionEmpty = 288,
    ActionFill = 289,
    ActionLoad = 290,
    ActionSave = 291,
    ActionSaveAs = 292,
    ActionExport = 293,
    ActionImport = 294,
    ActionOpen = 295,
    ActionClose = 296,
    ActionExit = 297,
    ActionCancel = 298,
    ActionApply = 299,
    ActionOK = 300,
    ActionYes = 301,
    ActionNo = 302,
    ActionAccept = 303,
    ActionDecline = 304,
    ActionApprove = 305,
    ActionReject = 306,
    ActionSubmit = 307,
    ActionCommit = 308,
    ActionRollback = 309,
    ActionConfirm = 310,
    ActionVerify = 311,
    ActionValidate = 312,
    ActionCheck = 313,
    ActionUncheck = 314,
    ActionEnable = 315,
    ActionDisable = 316,
    ActionActivate = 317,
    ActionDeactivate = 318,
    ActionStart = 319,
    ActionStop = 320,
    ActionPause = 321,
    ActionResume = 322,
    ActionPlay = 323,
    ActionRecord = 324,
    ActionCapture = 325,
    ActionRelease = 326,
    ActionLock = 327,
    ActionUnlock = 328,
    ActionSecure = 329,
    ActionEncrypt = 330,
    ActionDecrypt = 331,
    ActionProtect = 332,
    ActionUnprotect = 333,
    ActionHide = 334,
    ActionShow = 335,
    ActionDisplay = 336,
    ActionMinimize = 338,
    ActionMaximize = 339,
    ActionRestoreWindow = 340,
    ActionResize = 341,
    ActionScale = 342,
    ActionZoom = 343,
    ActionZoomIn = 344,
    ActionZoomOut = 345,
    ActionFit = 346,
    ActionActual = 347,
    ActionRotate = 348,
    ActionFlip = 349,
    ActionMirror = 350,
    ActionCrop = 351,
    ActionTrim = 352,
    ActionSplit = 353,
    ActionMerge = 354,
    ActionCombine = 355,
    ActionSeparate = 356,
    ActionJoin = 357,
    ActionBreak = 358,
    ActionConnect = 359,
    ActionDisconnect = 360,
    ActionLink = 361,
    ActionUnlink = 362,
    ActionAttach = 363,
    ActionDetach = 364,
    ActionBind = 365,
    ActionUnbind = 366,
    ActionMount = 367,
    ActionUnmount = 368,
    ActionInstall = 369,
    ActionUninstall = 370,
    ActionSetup = 371,
    ActionConfigure = 372,
    ActionCustomize = 373,
    ActionPersonalize = 374,
    ActionOptimize = 375,
    ActionTune = 376,
    ActionCalibrate = 377,
    ActionAlign = 378,
    ActionCenter = 379,
    ActionJustify = 380,
    ActionDistribute = 381,
    ActionArrange = 382,
    ActionOrganize = 383,
    ActionGroup = 384,
    ActionUngroup = 385,
    ActionStack = 386,
    ActionUnstack = 387,
    ActionLayer = 388,
    ActionOverlay = 389,
    ActionBlend = 390,
    ActionMix = 391,
    ActionFade = 393,
    ActionTransition = 394,
    ActionAnimate = 395,
    ActionSlide = 396,
    ActionScroll = 397,
    ActionPan = 398,
    ActionNavigate = 399,
    ActionBrowse = 400,

    // Navigation icons
    Navigation = 401,
    NavigationHome = 402,
    NavigationBack = 403,
    NavigationForward = 404,
    NavigationUp = 405,
    NavigationDown = 406,
    NavigationLeft = 407,
    NavigationRight = 408,
    NavigationTop = 409,
    NavigationBottom = 410,
    NavigationFirst = 411,
    NavigationLast = 412,
    NavigationPrevious = 413,
    NavigationNext = 414,
    NavigationJump = 415,
    NavigationSkip = 416,
    NavigationStep = 417,
    NavigationPage = 418,
    NavigationPageUp = 419,
    NavigationPageDown = 420,
    NavigationPageFirst = 421,
    NavigationPageLast = 422,
    NavigationPagePrevious = 423,
    NavigationPageNext = 424,
    NavigationTab = 425,
    NavigationTabNext = 426,
    NavigationTabPrevious = 427,
    NavigationTabFirst = 428,
    NavigationTabLast = 429,
    NavigationMenu = 430,
    NavigationSubmenu = 431,
    NavigationDropdown = 432,
    NavigationCombo = 433,
    NavigationList = 434,
    NavigationTree = 435,
    NavigationGrid = 436,
    NavigationTable = 437,
    NavigationChart = 438,
    NavigationGraph = 439,
    NavigationDiagram = 440,
    NavigationMap = 441,
    NavigationGlobe = 442,
    NavigationCompass = 443,
    NavigationLocation = 444,
    NavigationPin = 445,
    NavigationMarker = 446,
    NavigationFlag = 447,
    NavigationBookmark = 448,
    NavigationFavorite = 449,
    NavigationStar = 450,
    NavigationHeart = 451,
    NavigationLike = 452,
    NavigationDislike = 453,
    NavigationThumbsUp = 454,
    NavigationThumbsDown = 455,
    NavigationCheck = 456,
    NavigationCross = 457,
    NavigationPlus = 458,
    NavigationMinus = 459,
    NavigationMultiply = 460,
    NavigationDivide = 461,
    NavigationEquals = 462,
    NavigationPercent = 463,
    NavigationInfinity = 464,
    NavigationPi = 465,
    NavigationSigma = 466,
    NavigationDelta = 467,
    NavigationOmega = 468,
    NavigationAlpha = 469,
    NavigationBeta = 470,
    NavigationGamma = 471,
    NavigationTheta = 472,
    NavigationPhi = 473,
    NavigationPsi = 474,
    NavigationChi = 475,
    NavigationRho = 476,
    NavigationTau = 477,
    NavigationEpsilon = 478,
    NavigationZeta = 479,
    NavigationEta = 480,
    NavigationIota = 481,
    NavigationKappa = 482,
    NavigationLambda = 483,
    NavigationMu = 484,
    NavigationNu = 485,
    NavigationXi = 486,
    NavigationOmicron = 487,
    NavigationUpsilon = 488,
    NavigationPhiAlt = 489,
    NavigationChiAlt = 490,
    NavigationPsiAlt = 491,
    NavigationOmegaAlt = 492,

    // Tool icons
    Tool = 493,
    ToolHammer = 494,
    ToolWrench = 495,
    ToolScrewdriver = 496,
    ToolPliers = 497,
    ToolSaw = 498,
    ToolDrill = 499,
    ToolChisel = 500,
    ToolFile = 501,
    ToolSandpaper = 502,
    ToolBrush = 503,
    ToolRoller = 504,
    ToolSpray = 505,
    ToolGlue = 506,
    ToolTape = 507,
    ToolRope = 508,
    ToolChain = 509,
    ToolCable = 510,
    ToolWire = 511,
    ToolPipe = 512,
    ToolTube = 513,
    ToolValve = 514,
    ToolPump = 515,
    ToolMotor = 516,
    ToolEngine = 517,
    ToolGear = 518,
    ToolCog = 519,
    ToolWheel = 520,
    ToolAxle = 521,
    ToolBearing = 522,
    ToolSpring = 523,
    ToolScrew = 524,
    ToolBolt = 525,
    ToolNut = 526,
    ToolWasher = 527,
    ToolPin = 528,
    ToolClip = 529,
    ToolClamp = 530,
    ToolVise = 531,
    ToolBench = 532,
    ToolTable = 533,
    ToolShelf = 534,
    ToolCabinet = 535,
    ToolDrawer = 536,
    ToolBox = 537,
    ToolCase = 538,
    ToolBag = 539,
    ToolPouch = 540,
    ToolBelt = 541,
    ToolHolster = 542,
    ToolSheath = 543,
    ToolScabbard = 544,
    ToolRack = 545,
    ToolStand = 546,
    ToolMount = 547,
    ToolBracket = 548,
    ToolHanger = 549,
    ToolHook = 550,
    ToolRing = 551,
    ToolLoop = 552,
    ToolEye = 553,
    ToolGrommet = 554,
    ToolGasket = 555,
    ToolSeal = 556,
    ToolGland = 557,
    ToolCoupling = 558,
    ToolAdapter = 559,
    ToolReducer = 560,
    ToolExpander = 561,
    ToolElbow = 562,
    ToolTee = 563,
    ToolCross = 564,
    ToolUnion = 565,
    ToolCap = 566,
    ToolPlug = 567,
    ToolStopper = 568,
    ToolCork = 569,
    ToolLid = 570,
    ToolCover = 571,
    ToolShield = 572,
    ToolGuard = 573,
    ToolFence = 574,
    ToolBarrier = 575,
    ToolGate = 576,
    ToolDoor = 577,
    ToolWindow = 578,
    ToolPanel = 579,
    ToolScreen = 580,
    ToolFilter = 581,
    ToolStrainer = 582,
    ToolSieve = 583,
    ToolMesh = 584,
    ToolNet = 585,
    ToolWeb = 586,
    ToolFabric = 587,
    ToolCloth = 588,
    ToolPaper = 589,
    ToolCard = 590,
    ToolBoard = 591,
    ToolSheet = 592,
    ToolPlate = 593,
    ToolStrip = 594,
    ToolBar = 595,
    ToolRod = 596,
    ToolBeam = 597,
    ToolColumn = 598,
    ToolPillar = 599,
    ToolPost = 600,

    // Device icons
    Device = 601,
    DeviceComputer = 602,
    DeviceLaptop = 603,
    DeviceTablet = 604,
    DevicePhone = 605,
    DeviceMobile = 606,
    DeviceSmartphone = 607,
    DevicePda = 608,
    DeviceWatch = 609,
    DeviceSmartwatch = 610,
    DeviceFitness = 611,
    DeviceHeadset = 612,
    DeviceHeadphones = 613,
    DeviceEarphones = 614,
    DeviceSpeaker = 615,
    DeviceMicrophone = 616,
    DeviceCamera = 617,
    DeviceWebcam = 618,
    DeviceVideo = 619,
    DeviceCamcorder = 620,
    DeviceProjector = 621,
    DeviceMonitor = 622,
    DeviceDisplay = 623,
    DeviceScreen = 624,
    DeviceTv = 625,
    DeviceRadio = 626,
    DeviceStereo = 627,
    DeviceAmplifier = 628,
    DeviceReceiver = 629,
    DeviceTuner = 630,
    DevicePlayer = 631,
    DeviceRecorder = 632,
    DevicePrinter = 633,
    DeviceScanner = 634,
    DeviceFax = 635,
    DeviceCopier = 636,
    DevicePlotter = 637,
    DeviceLabeler = 638,
    DeviceBarcode = 639,
    DeviceQr = 640,
    DeviceRfid = 641,
    DeviceNfc = 642,
    DeviceBluetooth = 643,
    DeviceWifi = 644,
    DeviceEthernet = 645,
    DeviceModem = 646,
    DeviceRouter = 647,
    DeviceSwitch = 648,
    DeviceHub = 649,
    DeviceBridge = 650,
    DeviceGateway = 651,
    DeviceFirewall = 652,
    DeviceProxy = 653,
    DeviceServer = 654,
    DeviceWorkstation = 655,
    DeviceTerminal = 656,
    DeviceConsole = 657,
    DeviceKeyboard = 658,
    DeviceMouse = 659,
    DeviceTrackball = 660,
    DeviceTouchpad = 661,
    DeviceJoystick = 662,
    DeviceGamepad = 663,
    DeviceController = 664,
    DeviceRemote = 665,
    DeviceWand = 666,
    DevicePen = 667,
    DeviceStylus = 668,
    DeviceTouch = 669,
    DeviceGesture = 670,
    DeviceVoice = 671,
    DeviceSpeech = 672,
    DeviceHand = 673,
    DeviceFinger = 674,
    DeviceEye = 675,
    DeviceFace = 676,
    DeviceBody = 677,
    DeviceHeart = 678,
    DeviceBrain = 679,
    DeviceChip = 680,
    DeviceProcessor = 681,
    DeviceMemory = 682,
    DeviceStorage = 683,
    DeviceDisk = 684,
    DeviceDrive = 685,
    DeviceCard = 686,
    DeviceModule = 687,
    DeviceBoard = 688,
    DeviceCircuit = 689,
    DeviceTransistor = 691,
    DeviceResistor = 692,
    DeviceCapacitor = 693,
    DeviceInductor = 694,
    DeviceDiode = 695,
    DeviceLed = 696,
    DeviceLaser = 697,
    DeviceSensor = 698,
    DeviceDetector = 699,
    DeviceMeter = 700,
    DeviceGauge = 701,
    DeviceScale = 702,
    DeviceThermometer = 703,
    DeviceBarometer = 704,
    DeviceHygrometer = 705,
    DeviceAnemometer = 706,
    DeviceCompass = 707,
    DeviceGyroscope = 708,
    DeviceAccelerometer = 709,
    DeviceMagnetometer = 710,
    DeviceGps = 711,
    DeviceSatellite = 712,
    DeviceAntenna = 713,
    DeviceTower = 714,
    DeviceMast = 715,
    DevicePole = 716,
    DeviceWire = 717,
    DeviceCable = 718,
    DeviceFiber = 719,
    DeviceCoax = 720,
    DeviceTwisted = 721,
    DeviceShielded = 722,
    DeviceGround = 723,
    DeviceNeutral = 724,
    DeviceHot = 725,
    DeviceLive = 726,
    DeviceDead = 727,
    DeviceOn = 728,
    DeviceOff = 729,
    DeviceStandby = 730,
    DeviceSleep = 731,
    DeviceWake = 732,
    DeviceBoot = 733,
    DeviceShutdown = 734,
    DeviceRestart = 735,
    DeviceReset = 736,
    DevicePower = 737,
    DeviceBattery = 738,
    DeviceCharger = 739,
    DeviceSolar = 740,
    DeviceWind = 741,
    DeviceHydro = 742,
    DeviceNuclear = 743,
    DeviceFossil = 744,
    DeviceGas = 745,
    DeviceOil = 746,
    DeviceCoal = 747,
    DeviceBiomass = 748,
    DeviceGeothermal = 749,
    DeviceTidal = 750,
    DeviceWave = 751,
    DeviceFusion = 752,
    DeviceFission = 753,
    DeviceReactor = 754,
    DeviceTurbine = 755,
    DeviceGenerator = 756,
    DeviceMotor = 757,
    DeviceEngine = 758,
    DevicePump = 759,
    DeviceCompressor = 760,
    DeviceFan = 761,
    DeviceBlower = 762,
    DeviceVentilator = 763,
    DeviceAir = 764,
    DeviceWater = 765,
    DeviceSteam = 766,
    DeviceLiquid = 768,
    DeviceSolid = 769,
    DevicePlasma = 770,
    DeviceVacuum = 771,
    DevicePressure = 772,
    DeviceFlow = 773,
    DeviceLevel = 774,
    DeviceVolume = 775,
    DeviceMass = 776,
    DeviceWeight = 777,
    DeviceForce = 778,
    DeviceTorque = 779,
    DeviceSpeed = 780,
    DeviceVelocity = 781,
    DeviceAcceleration = 782,
    DeviceDistance = 783,
    DeviceLength = 784,
    DeviceWidth = 785,
    DeviceHeight = 786,
    DeviceDepth = 787,
    DeviceArea = 788,
    DeviceSurface = 789,
    DeviceCapacity = 791,
    DeviceDensity = 792,
    DeviceViscosity = 793,
    DeviceElasticity = 794,
    DevicePlasticity = 795,
    DeviceHardness = 796,
    DeviceToughness = 797,
    DeviceStrength = 798,
    DeviceStiffness = 799,
    DeviceFlexibility = 800,

    #endregion

    // Additional icon categories include:
    // - System Status Icons (201-255): Status indicators, system states, performance levels
    // - Action Icons (256-400): Common actions like add, remove, edit, copy, paste, etc.
    // - Navigation Icons (401-492): Navigation controls, directions, mathematical symbols
    // - Tool Icons (493-600): Hardware tools, construction equipment, mechanical parts
    // - Device Icons (601-800): Computing devices, peripherals, sensors, power systems

    // Note: This is a comprehensive list but imageres.dll contains many more icons.
    // For complete reference, see Microsoft's documentation or use resource extraction tools.
    // 
    // Usage Examples:
    // var shieldIcon = GraphicsExtensions.ExtractIconFromImageres(ImageresIconID.Shield, UACShieldIconSize.Medium);
    // var lockIcon = GraphicsExtensions.ExtractIconFromImageres(ImageresIconID.Lock, UACShieldIconSize.Small);
    // var userIcon = GraphicsExtensions.ExtractIconFromImageres(ImageresIconID.User, UACShieldIconSize.Large);
}

#endregion
