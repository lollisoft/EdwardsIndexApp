
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
using EdwardsIndexAppModule.Constants;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.WinForms;

namespace EdwardsIndexAppModule.WorkItems.IndexMarker
{
	public class IndexMarkerSummaryController : Controller
	{
		// The IndexMarkerSummaryController is the controller used by the IndexMarkerSummaryView.
		// The summary view contains the pieces of the other views to display a IndexMarker,
		// and includes the Save button for the user to save their changes. The save
		// request is forwarded up to the work item.

		public new IndexMarkerWorkItem WorkItem
		{
			get { return base.WorkItem as IndexMarkerWorkItem; }
		}

		public void Save()
		{
			WorkItem.Save();
		}

		[CommandHandler(CommandNames.EditIndexMarker)]
		public void OnIndexMarkerEdit(object sender, EventArgs args)
		{
			if (WorkItem.Status != WorkItemStatus.Active) return;

			var tabWorkspace = WorkItem.Workspaces[IndexMarkerWorkItem.IndexMarkerDETAIL_TABWORKSPACE] as TabWorkspace;
			if (tabWorkspace != null)
				tabWorkspace.SelectedIndex = 0;
		}
		
	}
}
