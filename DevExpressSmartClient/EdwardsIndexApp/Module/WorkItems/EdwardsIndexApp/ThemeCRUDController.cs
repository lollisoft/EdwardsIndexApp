

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

using EdwardsIndexAppModule.Services;
using Microsoft.Practices.CompositeUI;



namespace EdwardsIndexAppModule.WorkItems.EdwardsIndexApp
{
	public class ThemeCRUDController : Controller
	{
		// We depend on the customer queue service to tell us which customer is next
		private ThemeService _ThemeService;
		private int index;

		public ThemeCRUDController()
		{
			index = 0;
		}
		
		[ServiceDependency]
		public ThemeService ThemeCRUDService
		{
			set { _ThemeService = value; }
		}

		public new Theme.ThemeCRUDWorkItem WorkItem
		{
			get { return base.WorkItem as Theme.ThemeCRUDWorkItem; }
		}

		public EdwardsIndexAppCommon.XPO.Theme GetNextTheme()
		{
			if (index < _ThemeService.Count() - 1) 
				index++;
			return _ThemeService.GetByIndex(index);
		}

		// A grid directly operates on a list of Theme, thus it does not need to operate with
		// The navigation buttons. But when a navigarion button is pressed the grid may be notified about the navigation event.
		public void WorkWithTheme(EdwardsIndexAppCommon.XPO.Theme _Theme)
		{
			WorkItem.WorkWithTheme(_Theme);
		}
	}
}
