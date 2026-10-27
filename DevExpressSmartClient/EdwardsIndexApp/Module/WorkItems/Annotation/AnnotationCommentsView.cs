
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

namespace EdwardsIndexAppModule.WorkItems.Annotation
{
	[SmartPart]
	public partial class AnnotationCommentsView : XtraUserControl
	{
		// The Annotation state is stored in our parent work item

		private EdwardsIndexAppCommon.Annotation _Annotation;

		[State]
		public EdwardsIndexAppCommon.Annotation Annotation
		{
			set { _Annotation = value; }
		}

		public AnnotationCommentsView()
		{
			InitializeComponent();
		}

		private void AnnotationCommentsView_Load(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				AnnotationBindingSource.Add(_Annotation);
			}
		}
	}
}
