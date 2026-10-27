
namespace EdwardsIndexAppModule.Constants
{
	public class CommandNames
	{
		public const string Exit = "ExitCommand";				// string is repeated in config
		public const string HelpAbout = "HelpAboutCommand";		// string is repeated in config
        public const string ChangePassword = "Change password"; // string is repeated in config
        public const string RequestAccount = "Request account"; // string is repeated in config
        public const string ResetPassword = "Reset password"; // string is repeated in config

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
