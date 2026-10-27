
using System;

namespace EdwardsIndexAppModule
{
	/// <summary>
	/// Provider for various custom types. This class must be extended per type used in UML.
	/// Sample: Create a type PhoneNumber in your UML model and add here the formatting information.
	/// Add custom XSLT template code per type as like those for int, string and others.
	/// </summary>
 	public class MaskedTextProvider
	{
        private const string _STR_EnUS = "en-US";

        public static string STR_EnUS
        {
            get { return _STR_EnUS; }
        } 

		/// <summary>
        /// The phone number is dependent on the address and thus the country based format.
        /// </summary>
        /// <returns></returns>
        public static string GetPhoneNumberFormat(string culture) 
        {
            switch (culture)
            {
                case MaskedTextProvider._STR_EnUS:
                    return "(\\d?\\d?\\d?)\\d\\d\\d-\\d\\d\\d\\d";
            }
            return "";
        }

        public static DevExpress.XtraEditors.Mask.MaskType GetPhoneNumberMaskType(string culture) 
        {
            switch (culture)
            {
                case MaskedTextProvider._STR_EnUS:
                    return DevExpress.XtraEditors.Mask.MaskType.Regular;
            }
            return DevExpress.XtraEditors.Mask.MaskType.None;
        }
	}
}
