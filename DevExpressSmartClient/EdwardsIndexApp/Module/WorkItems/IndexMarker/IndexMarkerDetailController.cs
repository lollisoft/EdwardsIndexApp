
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

using Microsoft.Practices.CompositeUI;

namespace EdwardsIndexAppModule.WorkItems.IndexMarker
{
	public class IndexMarkerDetailController : Controller
	{
		public new IndexMarkerWorkItem WorkItem
		{
			get { return base.WorkItem as IndexMarkerWorkItem; }
		}

		public void ShowIndexMarkerComments()
		{
			WorkItem.ShowIndexMarkerComments();
		}
	}
}
