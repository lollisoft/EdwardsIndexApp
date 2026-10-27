

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
	public class BookCoordinateCRUDController : Controller
	{
		// We depend on the customer queue service to tell us which customer is next
		private BookCoordinateService _BookCoordinateService;
		private int index;

		public BookCoordinateCRUDController()
		{
			index = 0;
		}
		
		[ServiceDependency]
		public BookCoordinateService BookCoordinateCRUDService
		{
			set { _BookCoordinateService = value; }
		}

		public new BookCoordinate.BookCoordinateCRUDWorkItem WorkItem
		{
			get { return base.WorkItem as BookCoordinate.BookCoordinateCRUDWorkItem; }
		}

		public EdwardsIndexAppCommon.XPO.BookCoordinate GetNextBookCoordinate()
		{
			if (index < _BookCoordinateService.Count() - 1) 
				index++;
			return _BookCoordinateService.GetByIndex(index);
		}

		// A grid directly operates on a list of BookCoordinate, thus it does not need to operate with
		// The navigation buttons. But when a navigarion button is pressed the grid may be notified about the navigation event.
		public void WorkWithBookCoordinate(EdwardsIndexAppCommon.XPO.BookCoordinate _BookCoordinate)
		{
			WorkItem.WorkWithBookCoordinate(_BookCoordinate);
		}
	}
}
