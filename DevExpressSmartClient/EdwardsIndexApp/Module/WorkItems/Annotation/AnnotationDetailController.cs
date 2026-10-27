
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

namespace EdwardsIndexAppModule.WorkItems.Annotation
{
	public class AnnotationDetailController : Controller
	{
		public new AnnotationWorkItem WorkItem
		{
			get { return base.WorkItem as AnnotationWorkItem; }
		}

		public void ShowAnnotationComments()
		{
			WorkItem.ShowAnnotationComments();
		}
	}
}
