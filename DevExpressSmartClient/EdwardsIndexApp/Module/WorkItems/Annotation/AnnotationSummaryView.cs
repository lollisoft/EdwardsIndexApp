
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
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;

namespace EdwardsIndexAppModule.WorkItems.Annotation
{
    [SmartPart]
    public partial class AnnotationSummaryView : XtraUserControl
    {
        private AnnotationSummaryController controller;

        public AnnotationSummaryView()
        {
            InitializeComponent();
        }

       
        [CreateNew]
        public AnnotationSummaryController Controller
        {
            set { controller = value; }
        }

        private void OnSave(object sender, EventArgs e)
        {
            controller.Save();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!controller.WorkItem.UIExtensionSites.Contains("AnnotationContext"))
            controller.WorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.AnnotationContext, AnnotationContextMenu);
        }

        internal void FocusFirstTab()
        {
            if (tabbedWorkspace1.TabPages.Count > 0)
                tabbedWorkspace1.SelectedTabPage = tabbedWorkspace1.TabPages[0];
        }
    }
}
