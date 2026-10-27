
using System;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Commands;

namespace EdwardsIndexAppModule.WorkItems.EdwardsIndexApp
{
	[SmartPart]
	public partial class ThemeView : XtraUserControl, ISmartPartInfoProvider
	{
		public ThemeView()
		{
			InitializeComponent();
		}

		public ISmartPartInfo GetSmartPartInfo(Type smartPartInfoType)
		{
			return infoProvider.GetSmartPartInfo(smartPartInfoType);
		}
	}
}
