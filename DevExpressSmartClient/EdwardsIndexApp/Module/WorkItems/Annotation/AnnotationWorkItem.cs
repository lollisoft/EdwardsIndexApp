
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
using System.Drawing;
using System.Windows.Forms;
using EdwardsIndexAppModule.Constants;
using EdwardsIndexAppModule.Properties;
using CABDevExpress.SmartPartInfos;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.CompositeUI.WinForms;

namespace EdwardsIndexAppModule.WorkItems.Annotation
{
	public class AnnotationWorkItem : WorkItem
	{
		public static readonly string AnnotationDETAIL_TABWORKSPACE = "tabbedWorkspace1";

		private BarItem editAnnotationMenuItem1;
		private AnnotationSummaryView AnnotationSummaryView;
		private AnnotationCommentsView commentsView;
		private BarStaticItem addressLabel;

		// This event is published to indicate that the module would like to
		// "update status". The only subscriber to this event today is the shell
		// which updates the status bar. These two components don't know anything
		// about one another, because they communicate indirectly via the
		// EventBroker. In reality, you can have any number of publishers and
		// any number of subscribers; in fact, other modules will undoubtedly
		// also publish status update events.
		[EventPublication(EventNames.StatusUpdate, PublicationScope.Global)]
		public event EventHandler<DataEventArgs<string>> UpdateStatusTextEvent;

		public void Show(IWorkspace parentWorkspace)
		{
			AnnotationSummaryView = AnnotationSummaryView ?? Items.AddNew<AnnotationSummaryView>();

			AddMenuItems();

			var Annotation = (EdwardsIndexAppCommon.Annotation)State[WorkItemStates.Annotation];

			OnStatusTextUpdate(string.Format("Editing {0}", Annotation.ToString()));
			var smartPartInfo = new XtraWindowSmartPartInfo
			                    	{
			                    		Title = Annotation.ToString()
			                    	};
			//AnnotationSummaryView.Dock = DockStyle.Fill;
			parentWorkspace.Show(AnnotationSummaryView, smartPartInfo);

			UpdateUserAddressLabel(Annotation);

			Activate();
            parentWorkspace.SmartPartActivated += new EventHandler<WorkspaceEventArgs>(parentWorkspace_SmartPartActivated);

			// When activating, force focus on the first tab in the view.
			// Extensions may have added stuff at the end of the tab.
			AnnotationSummaryView.FocusFirstTab();
		}

        void parentWorkspace_SmartPartActivated(object sender, WorkspaceEventArgs e)
        {
        if ((e.SmartPart==AnnotationSummaryView)&& (this.Status==WorkItemStatus.Inactive))
            this.Activate();
        }

        

		private void UpdateUserAddressLabel(EdwardsIndexAppCommon.Annotation Annotation)
		{
			if (addressLabel != null) return;

			addressLabel = new BarStaticItem();
			UIExtensionSites[ExtensionSiteNames.MainStatus].Add(addressLabel);
			addressLabel.Caption = Annotation.ToString(); //Annotation.Address1;
		}

		private void AddMenuItems()
		{
			if (editAnnotationMenuItem1 != null) return;

            const string editCaption = "Edit";
            editAnnotationMenuItem1 = new BarButtonItem 
                                        {
                                            Caption = editCaption,
                                            Hint = editCaption,
                                            Glyph = Resources.Edit16,
                                            LargeGlyph = Resources.Edit32
                                        };
#if UseRibbonForm
            UIExtensionSites[ExtensionSiteNames.File].Add(editAnnotationMenuItem1);
#else
			UIExtensionSites[Resources.AnnotationMenuExtensionSite].Add(editAnnotationMenuItem1);
            UIExtensionSites[ExtensionSiteNames.ToolBar].Add(editAnnotationMenuItem1);
#endif
		}

		private void SetUIElementVisibility(bool visible)
		{
			if (editAnnotationMenuItem1 != null)
				editAnnotationMenuItem1.Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;

			if (addressLabel != null)
				addressLabel.Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
		}

		// We watch for when we are activated (i.e., shown to
		// be worked on), we want to fire a status update event and show ourselves
		// in the provided workspace.
		protected override void OnActivated()
		{
			base.OnActivated();

			SetUIElementVisibility(true);
		}

		protected override void OnDeactivated()
		{
			base.OnDeactivated();

			SetUIElementVisibility(false);
		}

		protected virtual void OnStatusTextUpdate(string newText)
		{
			if (UpdateStatusTextEvent != null)
			{
				UpdateStatusTextEvent(this, new DataEventArgs<string>(newText));
			}
		}

		// This is called by AnnotationDetailController when the user has indicated
		// that they want to show the comments. We dynamically create the comments
		// view and add it to our tab workspace.
		public void ShowAnnotationComments()
		{
			CreateCommentsView();

			IWorkspace ws = Workspaces[AnnotationDETAIL_TABWORKSPACE];
			if (ws != null)
			{
				ws.Show(commentsView);
			}
		}

		private void CreateCommentsView()
		{
			commentsView = commentsView ?? Items.AddNew<AnnotationCommentsView>();
			var info = new XtraTabSmartPartInfo
			           	{
			           		Title = "Title",
			           		Text = "Comments",
			           		PageHeaderFont = new Font("Comic Sans MS", 11.25F, FontStyle.Regular)
			           	};
			RegisterSmartPartInfo(commentsView, info);
		}

		//note let this string constant server to inform that it's not defined anywhere else in code
		[CommandHandler("AnnotationMouseOver")]
		public void OnAnnotationEdit(object sender, EventArgs args)
		{
			if (Status != WorkItemStatus.Active) return;

			Form form = AnnotationSummaryView.ParentForm;
			string tooltipText = "This is Annotation work item " + ID;
			var toolTip = new ToolTip {IsBalloon = true};
			if (form != null) 
				toolTip.Show(tooltipText, form, form.Size.Width - 30, 30, 3000);
		}
	}
}
