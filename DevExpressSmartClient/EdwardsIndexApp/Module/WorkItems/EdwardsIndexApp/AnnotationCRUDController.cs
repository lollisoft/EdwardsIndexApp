

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
	public class AnnotationCRUDController : Controller
	{
		// We depend on the customer queue service to tell us which customer is next
		private AnnotationService _AnnotationService;
		private int index;

		public AnnotationCRUDController()
		{
			index = 0;
		}
		
		[ServiceDependency]
		public AnnotationService AnnotationCRUDService
		{
			set { _AnnotationService = value; }
		}

		public new Annotation.AnnotationCRUDWorkItem WorkItem
		{
			get { return base.WorkItem as Annotation.AnnotationCRUDWorkItem; }
		}

		public EdwardsIndexAppCommon.XPO.Annotation GetNextAnnotation()
		{
			if (index < _AnnotationService.Count() - 1) 
				index++;
			return _AnnotationService.GetByIndex(index);
		}

		// A grid directly operates on a list of Annotation, thus it does not need to operate with
		// The navigation buttons. But when a navigarion button is pressed the grid may be notified about the navigation event.
		public void WorkWithAnnotation(EdwardsIndexAppCommon.XPO.Annotation _Annotation)
		{
			WorkItem.WorkWithAnnotation(_Annotation);
		}
	}
}
