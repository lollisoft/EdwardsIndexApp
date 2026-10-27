
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

namespace EdwardsIndexAppModule.WorkItems.Theme
{
	public class ThemeDetailController : Controller
	{
		public new ThemeWorkItem WorkItem
		{
			get { return base.WorkItem as ThemeWorkItem; }
		}

		public void ShowThemeComments()
		{
			WorkItem.ShowThemeComments();
		}
	}
}
