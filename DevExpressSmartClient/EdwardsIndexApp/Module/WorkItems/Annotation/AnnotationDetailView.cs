
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
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.IO;

namespace EdwardsIndexAppModule.WorkItems.Annotation
{
	[SmartPart]
	public partial class AnnotationDetailView : XtraUserControl
	{
		private EdwardsIndexAppCommon.Annotation _Annotation;
		private WorkItem parentWorkItem;
		private AnnotationDetailController controller;

		public AnnotationDetailView()
		{
			InitializeComponent();
		}

		[ServiceDependency]
		public WorkItem ParentWorkItem
		{
			set { parentWorkItem = value; }
		}

		// The Annotation state is stored in our parent work item
		[State]
		public EdwardsIndexAppCommon.Annotation Annotation
		{
			set { _Annotation = value; }
		}

		// We use our controller so we can show the comments page
		[CreateNew]
		public AnnotationDetailController Controller
		{
			set { controller = value; }
		}

        private string GetLayoutFileName()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), GetType().FullName + ".Layout.xml");
        }

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
            if (!File.Exists(GetLayoutFileName()))
                layoutControl1.SaveLayoutToXml(GetLayoutFileName());
            layoutControl1.RestoreLayoutFromXml(GetLayoutFileName());
            layoutControl1.OptionsCustomizationForm.ShowLoadButton = false;
            layoutControl1.OptionsCustomizationForm.ShowSaveButton = false;


			if (_Annotation != null)
			{
				AnnotationBindingSource.Add(_Annotation);
			}
		}

		private void OnShowComments(object sender, ItemClickEventArgs e)
		{
			controller.ShowAnnotationComments();
		}

        private void layoutControl1_HideCustomization(object sender, EventArgs e)
        {
            layoutControl1.SaveLayoutToXml(GetLayoutFileName());
        }
	}
}
