
namespace EdwardsIndexAppModule.Constants
{
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
}
