
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

namespace EdwardsIndexAppModule.WorkItems.IndexMarker
{
	[SmartPart]
	public partial class IndexMarkerCommentsView : XtraUserControl
	{
		// The IndexMarker state is stored in our parent work item

		private EdwardsIndexAppCommon.IndexMarker _IndexMarker;

		[State]
		public EdwardsIndexAppCommon.IndexMarker IndexMarker
		{
			set { _IndexMarker = value; }
		}

		public IndexMarkerCommentsView()
		{
			InitializeComponent();
		}

		private void IndexMarkerCommentsView_Load(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				IndexMarkerBindingSource.Add(_IndexMarker);
			}
		}
	}
}
