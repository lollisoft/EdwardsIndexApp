
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

namespace EdwardsIndexAppModule.WorkItems.Theme
{
    [SmartPart]
    public partial class ThemeSummaryView : XtraUserControl
    {
        private ThemeSummaryController controller;

        public ThemeSummaryView()
        {
            InitializeComponent();
        }

       
        [CreateNew]
        public ThemeSummaryController Controller
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
            if (!controller.WorkItem.UIExtensionSites.Contains("ThemeContext"))
            controller.WorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.ThemeContext, ThemeContextMenu);
        }

        internal void FocusFirstTab()
        {
            if (tabbedWorkspace1.TabPages.Count > 0)
                tabbedWorkspace1.SelectedTabPage = tabbedWorkspace1.TabPages[0];
        }
    }
}
