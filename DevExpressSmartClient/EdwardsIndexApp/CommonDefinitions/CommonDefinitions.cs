
namespace EdwardsIndexAppShell
{
    public class WorkspaceNames
    {
        public const string ShellNavBarWorkspace = "ShellNavBarWorkspace";
        public const string ShellContentWorkspace = "ShellContentWorkspace";
        public const string DockManagerWorkspace = "DockManagerWorkspace";
    }
    public class ExtensionSiteNames
    {

        public const string IndexMarkerContext = "IndexMarkerContext";
        public const string IndexMarker = "IndexMarker";

        public const string BookCoordinateContext = "BookCoordinateContext";
        public const string BookCoordinate = "BookCoordinate";

        public const string AnnotationContext = "AnnotationContext";
        public const string Annotation = "Annotation";

        public const string ThemeContext = "ThemeContext";
        public const string Theme = "Theme";

        public const string Queue = "Queue";
        public const string File = "Page_Home_Group_File";
        public const string MainMenu = "Page_Home";
        public const string FileDropDown = "FileDropDown";
        public const string MainStatus = "MainStatus";
        public const string NavBar = "NavBar";
        public const string SkinsDropDown = "SkinsDropDown";
        public const string SkinsToolBar = "SkinsToolBar";
        public const string ToolBar = "ToolBar";
        public const string WindowToolBar = "WindowToolBar";

        // UseRibbonForm
        public const string Ribbon = "Ribbon";
        public const string RibbonLookAndFeel = "Look & Feel";
        public const string RibbonSkins = "Skins";
        public const string RibbonSkinGallery = "Skin Gallery";
        public const string RibbonSkinGalleryGroup = "Skin Gallery Group";
        public const string RibbonWindows = "Windows";
        public const string RibbonPageHeader = "RibbonPageHeader";
        public const string RibbonQuickAccessToolbar = "RibbonQuickAccessToolbar";
        public const string RibbonApplicationMenu = "RibbonApplicationMenu";
        public const string RibbonAppMenuBottomPane = "RibbonAppMenuBottomPane";
    }
    public class EventNames
    {
        public const string StatusUpdate = "topic://BankShell/StatusUpdate";
        public const string SkinPopup = "topic://BankShell/SkinPopup";
        public const string SwitchSkin = "topic://BankShell/SwitchSkin";

        public const string IndexMarkerClosed = "topic://BankShell/IndexMarkerClosed";

        public const string BookCoordinateClosed = "topic://BankShell/BookCoordinateClosed";

        public const string AnnotationClosed = "topic://BankShell/AnnotationClosed";

        public const string ThemeClosed = "topic://BankShell/ThemeClosed";


        // UseRibbonForm
        public const string RibbonSkinChange = "topic://BankShell/RibbonSkinChange";

        public const string Exit = "ExitCommandEvent";				// string is repeated in config
        public const string HelpAbout = "HelpAboutCommandEvent";		// string is repeated in config
    }
    public class CommandNames
    {
        public const string Exit = "ExitCommand";				// string is repeated in config
        public const string HelpAbout = "HelpAboutCommand";		// string is repeated in config

        public const string AcceptIndexMarker = "QueueAcceptIndexMarker";
        public const string EditIndexMarker = "EditIndexMarker";

        public const string AcceptBookCoordinate = "QueueAcceptBookCoordinate";
        public const string EditBookCoordinate = "EditBookCoordinate";

        public const string AcceptAnnotation = "QueueAcceptAnnotation";
        public const string EditAnnotation = "EditAnnotation";

        public const string AcceptTheme = "QueueAcceptTheme";
        public const string EditTheme = "EditTheme";

        public const string DxMenuSamplePopup = "DxMenuSamplePopup";

        // UseRibbonForm
        public const string RibbonCascadeWindows = "RibbonCascadeWindows";
        public const string RibbonTileWindowsHoriz = "RibbonTileWindowsHoriz";
        public const string RibbontileWindowsVert = "RibbontileWindowsVert";
    }
}
