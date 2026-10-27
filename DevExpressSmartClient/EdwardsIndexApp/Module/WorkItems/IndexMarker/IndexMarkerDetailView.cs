
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

namespace EdwardsIndexAppModule.WorkItems.IndexMarker
{
	[SmartPart]
	public partial class IndexMarkerDetailView : XtraUserControl
	{
		private EdwardsIndexAppCommon.IndexMarker _IndexMarker;
		private WorkItem parentWorkItem;
		private IndexMarkerDetailController controller;

		public IndexMarkerDetailView()
		{
			InitializeComponent();
		}

		[ServiceDependency]
		public WorkItem ParentWorkItem
		{
			set { parentWorkItem = value; }
		}

		// The IndexMarker state is stored in our parent work item
		[State]
		public EdwardsIndexAppCommon.IndexMarker IndexMarker
		{
			set { _IndexMarker = value; }
		}

		// We use our controller so we can show the comments page
		[CreateNew]
		public IndexMarkerDetailController Controller
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


			if (_IndexMarker != null)
			{
				IndexMarkerBindingSource.Add(_IndexMarker);
			}
		}

		private void OnShowComments(object sender, ItemClickEventArgs e)
		{
			controller.ShowIndexMarkerComments();
		}

        private void layoutControl1_HideCustomization(object sender, EventArgs e)
        {
            layoutControl1.SaveLayoutToXml(GetLayoutFileName());
        }
	}
}
