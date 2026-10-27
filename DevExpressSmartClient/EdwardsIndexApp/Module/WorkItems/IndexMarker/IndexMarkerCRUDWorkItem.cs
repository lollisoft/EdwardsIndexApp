
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
using DevExpress.XtraEditors;
using EdwardsIndexAppModule.WorkItems.EdwardsIndexApp;




namespace EdwardsIndexAppModule.WorkItems.IndexMarker
{
	public class IndexMarkerCRUDWorkItem : WorkItem
	{
		public static readonly string IndexMarkerDETAIL_TABWORKSPACE = "tabbedWorkspace1";

		
        ViewMode viewMode;

        internal ViewMode ViewMode
        {
            get { return viewMode; }
            set { viewMode = value; }
        }

		private WorkItem parentWorkItem;
        private Services.IndexMarkerService _IndexMarkerService;
        private EdwardsIndexAppCommon.XPO.IndexMarker currentIndexMarker;
		private int index;

		// React upon CRUD events only when active
		bool EnableSharedCRUDEvents;

        [ServiceDependency]
        public Services.IndexMarkerService IndexMarkerService
        {
            set { _IndexMarkerService = value; index = 0;}
        }

		[ServiceDependency]
		public WorkItem ParentWorkItem
		{
			set { parentWorkItem = value; }
		}

		
		private BarItem editIndexMarkerMenuItem1;
		
		private EdwardsIndexAppModule.Forms.GenericDataLayoutCRUDFormView<EdwardsIndexAppCommon.XPO.IndexMarker> IndexMarkerGenericDataLayoutCRUDFormView;
		private IndexMarkerSimpleCRUDFormView IndexMarkerSimpleCRUDFormView;
		private IndexMarkerLayoutCRUDFormView IndexMarkerLayoutCRUDFormView;
        private IndexMarkerPerFormGridView IndexMarkerGridCRUDFormView;
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

		protected void ShowGrid()
		{
            if (IndexMarkerGridCRUDFormView != null && IndexMarkerGridCRUDFormView.IsDisposed)
	    		IndexMarkerGridCRUDFormView = Items.AddNew<IndexMarkerPerFormGridView>();
            else if (IndexMarkerGridCRUDFormView == null)
                IndexMarkerGridCRUDFormView = Items.AddNew<IndexMarkerPerFormGridView>();
		}

		protected void ShowSimple()
		{
            if (IndexMarkerSimpleCRUDFormView != null && IndexMarkerSimpleCRUDFormView.IsDisposed)
	    		IndexMarkerSimpleCRUDFormView = Items.AddNew<IndexMarkerSimpleCRUDFormView>();
            else if (IndexMarkerSimpleCRUDFormView == null)
                IndexMarkerSimpleCRUDFormView = Items.AddNew<IndexMarkerSimpleCRUDFormView>();
		}

		protected void ShowLayout()
		{
            if (IndexMarkerLayoutCRUDFormView != null && IndexMarkerLayoutCRUDFormView.IsDisposed)
	    		IndexMarkerLayoutCRUDFormView = Items.AddNew<IndexMarkerLayoutCRUDFormView>();
            else if (IndexMarkerLayoutCRUDFormView == null)
                IndexMarkerLayoutCRUDFormView = Items.AddNew<IndexMarkerLayoutCRUDFormView>();
		}

		protected void ShowGenericDataLayout()
		{
            if (IndexMarkerGenericDataLayoutCRUDFormView != null && IndexMarkerGenericDataLayoutCRUDFormView.IsDisposed)
	    		IndexMarkerGenericDataLayoutCRUDFormView = Items.AddNew<EdwardsIndexAppModule.Forms.GenericDataLayoutCRUDFormView<EdwardsIndexAppCommon.XPO.IndexMarker>>();
            else if (IndexMarkerGenericDataLayoutCRUDFormView == null)
                IndexMarkerGenericDataLayoutCRUDFormView = Items.AddNew<EdwardsIndexAppModule.Forms.GenericDataLayoutCRUDFormView<EdwardsIndexAppCommon.XPO.IndexMarker>>();
		}
		
		public void Show(IWorkspace parentWorkspace)
		{
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) ShowLayout();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) ShowGenericDataLayout();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) ShowSimple();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) ShowGrid();
			
			AddMenuItems();

			var IndexMarker = (EdwardsIndexAppCommon.XPO.IndexMarker)State[WorkItemStates.IndexMarker];

			//OnStatusTextUpdate(string.Format("Editing {0}", IndexMarker.ToString()));
			var smartPartInfo = new XtraWindowSmartPartInfo
			                    	{
			                    		Title = "IndexMarker",
                                        Icon = Icon.FromHandle(Resources.IndexMarker.GetHicon())
			                    	};
									
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm)
			{
				IndexMarkerLayoutCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(IndexMarkerLayoutCRUDFormView, smartPartInfo);
			}
			
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm)
			{
				IndexMarkerGenericDataLayoutCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(IndexMarkerGenericDataLayoutCRUDFormView, smartPartInfo);
			}
			
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm)
            {
				IndexMarkerSimpleCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(IndexMarkerSimpleCRUDFormView, smartPartInfo);
			}
			
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid)
            {
				IndexMarkerGridCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(IndexMarkerGridCRUDFormView, smartPartInfo);
			}

			//UpdateUserAddressLabel(IndexMarker);

			Activate();
            parentWorkspace.SmartPartActivated += new EventHandler<WorkspaceEventArgs>(parentWorkspace_SmartPartActivated);

			// When activating, force focus on the first tab in the view.
			// Extensions may have added stuff at the end of the tab.
			//IndexMarkerSimpleCRUDFormView.FocusFirstTab();
		}

        void parentWorkspace_SmartPartActivated(object sender, WorkspaceEventArgs e)
        {
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm)
			{
				if ((e.SmartPart==IndexMarkerLayoutCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}

			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm)
			{
				if ((e.SmartPart==IndexMarkerGenericDataLayoutCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}

			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm)
			{
				if ((e.SmartPart==IndexMarkerSimpleCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}

			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid)
			{
				if ((e.SmartPart==IndexMarkerGridCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}
			
        }

		protected void Refresh()
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) IndexMarkerLayoutCRUDFormView.Refresh();
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) IndexMarkerGenericDataLayoutCRUDFormView.Refresh();
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) IndexMarkerSimpleCRUDFormView.Refresh();
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) IndexMarkerGridCRUDFormView.RefreshData(false);
		}
		
		public void WorkWithIndexMarker(EdwardsIndexAppCommon.XPO.IndexMarker _IndexMarker) {
		
		}
		
		// Move to position
		protected void SetViewEntity(int index)
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) IndexMarkerLayoutCRUDFormView.Position(index);
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) IndexMarkerGenericDataLayoutCRUDFormView.Position(index);
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) IndexMarkerSimpleCRUDFormView.Position(index);
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) IndexMarkerGridCRUDFormView.Position(index);
			
			Refresh();
		}

		protected EdwardsIndexAppCommon.XPO.IndexMarker GetViewEntity()
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) return IndexMarkerLayoutCRUDFormView.IndexMarker;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) return IndexMarkerGenericDataLayoutCRUDFormView.TItem;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) return IndexMarkerSimpleCRUDFormView.IndexMarker;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) return IndexMarkerGridCRUDFormView.IndexMarker;
			throw new Exception("View mode not supported");
		}
		
		protected bool IsViewDisposed()
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm && IndexMarkerLayoutCRUDFormView.IsDisposed) return true;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm && IndexMarkerGenericDataLayoutCRUDFormView.IsDisposed) return true;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm && IndexMarkerSimpleCRUDFormView.IsDisposed) return true;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid && IndexMarkerGridCRUDFormView.IsDisposed) return true;
			return false;
		}

        [CommandHandler("Save")]
        public void OnSave(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
 
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) IndexMarkerLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) IndexMarkerGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) IndexMarkerSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) IndexMarkerGridCRUDFormView.EndEdit();

			_IndexMarkerService.Save(currentIndexMarker);
        }
		
        [CommandHandler("Delete")]
        public void OnDelete(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents ) return;
			if (IsViewDisposed()) return;
            _IndexMarkerService.Delete(currentIndexMarker);
			
			if (index > _IndexMarkerService.Count() - 1) index--;
			if (_IndexMarkerService.Count() == 0)
				currentIndexMarker = null;
			SetViewEntity(index);
        }

        [CommandHandler("New")]
        public void OnNew(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            _IndexMarkerService.Save(currentIndexMarker);
            currentIndexMarker = new EdwardsIndexAppCommon.XPO.IndexMarker();
            _IndexMarkerService.Add(currentIndexMarker);
			SetViewEntity(index);
        }

        [CommandHandler("First")]
        public void OnFirst(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) IndexMarkerLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) IndexMarkerGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) IndexMarkerSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) IndexMarkerGridCRUDFormView.EndEdit();

            _IndexMarkerService.Save(currentIndexMarker);
            index = 0;
            currentIndexMarker = _IndexMarkerService.GetByIndex(index);
			SetViewEntity(index);
        }

        [CommandHandler("Previous")]
        public void OnPrevious(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            if (index > 0) index--;
			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) IndexMarkerLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) IndexMarkerGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) IndexMarkerSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) IndexMarkerGridCRUDFormView.EndEdit();

            _IndexMarkerService.Save(currentIndexMarker);
            currentIndexMarker = _IndexMarkerService.GetByIndex(index);
			SetViewEntity(index);
        }

        [CommandHandler("Next")]
        public void OnNext(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            if (index < _IndexMarkerService.Count() - 1) index++;

			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) IndexMarkerLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) IndexMarkerGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) IndexMarkerSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) IndexMarkerGridCRUDFormView.EndEdit();

            _IndexMarkerService.Save(currentIndexMarker);
            currentIndexMarker = _IndexMarkerService.GetByIndex(index);
			SetViewEntity(index);
        }

        [CommandHandler("Last")]
        public void OnLast(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            index = _IndexMarkerService.Count() - 1;
			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) IndexMarkerLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) IndexMarkerGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) IndexMarkerSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) IndexMarkerGridCRUDFormView.EndEdit();

            _IndexMarkerService.Save(currentIndexMarker);
            currentIndexMarker = _IndexMarkerService.GetByIndex(index);
			SetViewEntity(index);
        }
		
		private void UpdateUserAddressLabel(EdwardsIndexAppCommon.XPO.IndexMarker IndexMarker)
		{
			if (addressLabel != null) return;

			addressLabel = new BarStaticItem();
			UIExtensionSites[ExtensionSiteNames.MainStatus].Add(addressLabel);
			addressLabel.Caption = IndexMarker.ToString(); //IndexMarker.Address1;
		}

        private void createCommand(string EdwardsIndexAppRibbonGroup, string Caption, BarItem command1, string extensionSite)
        {
            //extensionSite = ExtensionSiteNames.File;
            // add also the the Quick Access Toolbar and to the ApplicationMenu
            parentWorkItem.UIExtensionSites[ExtensionSiteNames.RibbonQuickAccessToolbar].Add(command1);
            parentWorkItem.UIExtensionSites[EdwardsIndexAppRibbonGroup].Add(command1);

            parentWorkItem.Commands[Caption].AddInvoker(command1, "ItemClick");
        }

        private void CreateBarButtonItem(string EdwardsIndexAppRibbonGroup, string extensionSite, string Caption, Bitmap icon, ref BarItem item)
        {
            if (item == null)
            {
                item = new BarButtonItem
                {
                    Caption = Caption,
                    Hint = Caption,
                    Glyph = icon,
                    LargeGlyph = icon
                };
                //UIExtensionSites[ExtensionSiteNames.File].Add(item);
				createCommand(EdwardsIndexAppRibbonGroup, Caption, item, extensionSite);
            }
        }

		private void AddMenuItems()
		{
		}

		private void SetUIElementVisibility(bool visible)
		{
			EnableSharedCRUDEvents = visible;

			if (editIndexMarkerMenuItem1 != null)
				editIndexMarkerMenuItem1.Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;

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

		//note let this string constant server to inform that it's not defined anywhere else in code
		[CommandHandler("IndexMarkerMouseOver")]
		public void OnIndexMarkerEdit(object sender, EventArgs args)
		{
			if (Status != WorkItemStatus.Active) return;

			Form form = IndexMarkerSimpleCRUDFormView.ParentForm;
			string tooltipText = "This is IndexMarker work item " + ID;
			var toolTip = new ToolTip {IsBalloon = true};
			if (form != null) 
				toolTip.Show(tooltipText, form, form.Size.Width - 30, 30, 3000);
		}
	}
}
