
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

namespace EdwardsIndexAppModule.WorkItems.BookCoordinate
{
	[SmartPart]
	public partial class BookCoordinateCommentsView : XtraUserControl
	{
		// The BookCoordinate state is stored in our parent work item

		private EdwardsIndexAppCommon.BookCoordinate _BookCoordinate;

		[State]
		public EdwardsIndexAppCommon.BookCoordinate BookCoordinate
		{
			set { _BookCoordinate = value; }
		}

		public BookCoordinateCommentsView()
		{
			InitializeComponent();
		}

		private void BookCoordinateCommentsView_Load(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				BookCoordinateBindingSource.Add(_BookCoordinate);
			}
		}
	}
}
