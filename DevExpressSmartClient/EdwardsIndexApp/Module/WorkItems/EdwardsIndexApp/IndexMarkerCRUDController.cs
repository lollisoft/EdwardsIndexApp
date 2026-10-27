

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
	public class IndexMarkerCRUDController : Controller
	{
		// We depend on the customer queue service to tell us which customer is next
		private IndexMarkerService _IndexMarkerService;
		private int index;

		public IndexMarkerCRUDController()
		{
			index = 0;
		}
		
		[ServiceDependency]
		public IndexMarkerService IndexMarkerCRUDService
		{
			set { _IndexMarkerService = value; }
		}

		public new IndexMarker.IndexMarkerCRUDWorkItem WorkItem
		{
			get { return base.WorkItem as IndexMarker.IndexMarkerCRUDWorkItem; }
		}

		public EdwardsIndexAppCommon.XPO.IndexMarker GetNextIndexMarker()
		{
			if (index < _IndexMarkerService.Count() - 1) 
				index++;
			return _IndexMarkerService.GetByIndex(index);
		}

		// A grid directly operates on a list of IndexMarker, thus it does not need to operate with
		// The navigation buttons. But when a navigarion button is pressed the grid may be notified about the navigation event.
		public void WorkWithIndexMarker(EdwardsIndexAppCommon.XPO.IndexMarker _IndexMarker)
		{
			WorkItem.WorkWithIndexMarker(_IndexMarker);
		}
	}
}
