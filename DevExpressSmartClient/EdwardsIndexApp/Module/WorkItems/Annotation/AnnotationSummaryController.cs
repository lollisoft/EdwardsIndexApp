
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

namespace EdwardsIndexAppModule.WorkItems.Annotation
{
	public class AnnotationSummaryController : Controller
	{
		// The AnnotationSummaryController is the controller used by the AnnotationSummaryView.
		// The summary view contains the pieces of the other views to display a Annotation,
		// and includes the Save button for the user to save their changes. The save
		// request is forwarded up to the work item.

		public new AnnotationWorkItem WorkItem
		{
			get { return base.WorkItem as AnnotationWorkItem; }
		}

		public void Save()
		{
			WorkItem.Save();
		}

		[CommandHandler(CommandNames.EditAnnotation)]
		public void OnAnnotationEdit(object sender, EventArgs args)
		{
			if (WorkItem.Status != WorkItemStatus.Active) return;

			var tabWorkspace = WorkItem.Workspaces[AnnotationWorkItem.AnnotationDETAIL_TABWORKSPACE] as TabWorkspace;
			if (tabWorkspace != null)
				tabWorkspace.SelectedIndex = 0;
		}
		
	}
}
