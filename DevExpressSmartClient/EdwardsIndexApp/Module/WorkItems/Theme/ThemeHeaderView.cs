
//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace EdwardsIndexAppModule.WorkItems.Theme
{
	[SmartPart]
	public partial class ThemeHeaderView : XtraUserControl
	{
		// The Theme state is stored in our parent work item

		[State]
		public EdwardsIndexAppCommon.Theme Theme
		{
			set { _Theme = value; }
		}

		private EdwardsIndexAppCommon.Theme _Theme;

		public ThemeHeaderView()
		{
			InitializeComponent();
		}

		private void ThemeHeaderView_Load(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				ThemeBindingSource.Add(_Theme);
			}
		}
	}
}
