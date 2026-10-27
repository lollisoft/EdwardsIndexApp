
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




namespace EdwardsIndexAppModule.WorkItems.BookCoordinate
{
	public class BookCoordinateCRUDWorkItem : WorkItem
	{
		public static readonly string BookCoordinateDETAIL_TABWORKSPACE = "tabbedWorkspace1";

		
        ViewMode viewMode;

        internal ViewMode ViewMode
        {
            get { return viewMode; }
            set { viewMode = value; }
        }

		private WorkItem parentWorkItem;
        private Services.BookCoordinateService _BookCoordinateService;
        private EdwardsIndexAppCommon.XPO.BookCoordinate currentBookCoordinate;
		private int index;

		// React upon CRUD events only when active
		bool EnableSharedCRUDEvents;

        [ServiceDependency]
        public Services.BookCoordinateService BookCoordinateService
        {
            set { _BookCoordinateService = value; index = 0;}
        }

		[ServiceDependency]
		public WorkItem ParentWorkItem
		{
			set { parentWorkItem = value; }
		}

		
		private BarItem editBookCoordinateMenuItem1;
		
		private EdwardsIndexAppModule.Forms.GenericDataLayoutCRUDFormView<EdwardsIndexAppCommon.XPO.BookCoordinate> BookCoordinateGenericDataLayoutCRUDFormView;
		private BookCoordinateSimpleCRUDFormView BookCoordinateSimpleCRUDFormView;
		private BookCoordinateLayoutCRUDFormView BookCoordinateLayoutCRUDFormView;
        private BookCoordinatePerFormGridView BookCoordinateGridCRUDFormView;
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
            if (BookCoordinateGridCRUDFormView != null && BookCoordinateGridCRUDFormView.IsDisposed)
	    		BookCoordinateGridCRUDFormView = Items.AddNew<BookCoordinatePerFormGridView>();
            else if (BookCoordinateGridCRUDFormView == null)
                BookCoordinateGridCRUDFormView = Items.AddNew<BookCoordinatePerFormGridView>();
		}

		protected void ShowSimple()
		{
            if (BookCoordinateSimpleCRUDFormView != null && BookCoordinateSimpleCRUDFormView.IsDisposed)
	    		BookCoordinateSimpleCRUDFormView = Items.AddNew<BookCoordinateSimpleCRUDFormView>();
            else if (BookCoordinateSimpleCRUDFormView == null)
                BookCoordinateSimpleCRUDFormView = Items.AddNew<BookCoordinateSimpleCRUDFormView>();
		}

		protected void ShowLayout()
		{
            if (BookCoordinateLayoutCRUDFormView != null && BookCoordinateLayoutCRUDFormView.IsDisposed)
	    		BookCoordinateLayoutCRUDFormView = Items.AddNew<BookCoordinateLayoutCRUDFormView>();
            else if (BookCoordinateLayoutCRUDFormView == null)
                BookCoordinateLayoutCRUDFormView = Items.AddNew<BookCoordinateLayoutCRUDFormView>();
		}

		protected void ShowGenericDataLayout()
		{
            if (BookCoordinateGenericDataLayoutCRUDFormView != null && BookCoordinateGenericDataLayoutCRUDFormView.IsDisposed)
	    		BookCoordinateGenericDataLayoutCRUDFormView = Items.AddNew<EdwardsIndexAppModule.Forms.GenericDataLayoutCRUDFormView<EdwardsIndexAppCommon.XPO.BookCoordinate>>();
            else if (BookCoordinateGenericDataLayoutCRUDFormView == null)
                BookCoordinateGenericDataLayoutCRUDFormView = Items.AddNew<EdwardsIndexAppModule.Forms.GenericDataLayoutCRUDFormView<EdwardsIndexAppCommon.XPO.BookCoordinate>>();
		}
		
		public void Show(IWorkspace parentWorkspace)
		{
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) ShowLayout();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) ShowGenericDataLayout();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) ShowSimple();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) ShowGrid();
			
			AddMenuItems();

			var BookCoordinate = (EdwardsIndexAppCommon.XPO.BookCoordinate)State[WorkItemStates.BookCoordinate];

			//OnStatusTextUpdate(string.Format("Editing {0}", BookCoordinate.ToString()));
			var smartPartInfo = new XtraWindowSmartPartInfo
			                    	{
			                    		Title = "BookCoordinate",
                                        Icon = Icon.FromHandle(Resources.BookCoordinate.GetHicon())
			                    	};
									
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm)
			{
				BookCoordinateLayoutCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(BookCoordinateLayoutCRUDFormView, smartPartInfo);
			}
			
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm)
			{
				BookCoordinateGenericDataLayoutCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(BookCoordinateGenericDataLayoutCRUDFormView, smartPartInfo);
			}
			
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm)
            {
				BookCoordinateSimpleCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(BookCoordinateSimpleCRUDFormView, smartPartInfo);
			}
			
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid)
            {
				BookCoordinateGridCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(BookCoordinateGridCRUDFormView, smartPartInfo);
			}

			//UpdateUserAddressLabel(BookCoordinate);

			Activate();
            parentWorkspace.SmartPartActivated += new EventHandler<WorkspaceEventArgs>(parentWorkspace_SmartPartActivated);

			// When activating, force focus on the first tab in the view.
			// Extensions may have added stuff at the end of the tab.
			//BookCoordinateSimpleCRUDFormView.FocusFirstTab();
		}

        void parentWorkspace_SmartPartActivated(object sender, WorkspaceEventArgs e)
        {
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm)
			{
				if ((e.SmartPart==BookCoordinateLayoutCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}

			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm)
			{
				if ((e.SmartPart==BookCoordinateGenericDataLayoutCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}

			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm)
			{
				if ((e.SmartPart==BookCoordinateSimpleCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}

			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid)
			{
				if ((e.SmartPart==BookCoordinateGridCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}
			
        }

		protected void Refresh()
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) BookCoordinateLayoutCRUDFormView.Refresh();
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) BookCoordinateGenericDataLayoutCRUDFormView.Refresh();
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) BookCoordinateSimpleCRUDFormView.Refresh();
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) BookCoordinateGridCRUDFormView.RefreshData(false);
		}
		
		public void WorkWithBookCoordinate(EdwardsIndexAppCommon.XPO.BookCoordinate _BookCoordinate) {
		
		}
		
		// Move to position
		protected void SetViewEntity(int index)
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) BookCoordinateLayoutCRUDFormView.Position(index);
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) BookCoordinateGenericDataLayoutCRUDFormView.Position(index);
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) BookCoordinateSimpleCRUDFormView.Position(index);
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) BookCoordinateGridCRUDFormView.Position(index);
			
			Refresh();
		}

		protected EdwardsIndexAppCommon.XPO.BookCoordinate GetViewEntity()
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) return BookCoordinateLayoutCRUDFormView.BookCoordinate;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) return BookCoordinateGenericDataLayoutCRUDFormView.TItem;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) return BookCoordinateSimpleCRUDFormView.BookCoordinate;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) return BookCoordinateGridCRUDFormView.BookCoordinate;
			throw new Exception("View mode not supported");
		}
		
		protected bool IsViewDisposed()
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm && BookCoordinateLayoutCRUDFormView.IsDisposed) return true;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm && BookCoordinateGenericDataLayoutCRUDFormView.IsDisposed) return true;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm && BookCoordinateSimpleCRUDFormView.IsDisposed) return true;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid && BookCoordinateGridCRUDFormView.IsDisposed) return true;
			return false;
		}

        [CommandHandler("Save")]
        public void OnSave(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
 
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) BookCoordinateLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) BookCoordinateGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) BookCoordinateSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) BookCoordinateGridCRUDFormView.EndEdit();

			_BookCoordinateService.Save(currentBookCoordinate);
        }
		
        [CommandHandler("Delete")]
        public void OnDelete(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents ) return;
			if (IsViewDisposed()) return;
            _BookCoordinateService.Delete(currentBookCoordinate);
			
			if (index > _BookCoordinateService.Count() - 1) index--;
			if (_BookCoordinateService.Count() == 0)
				currentBookCoordinate = null;
			SetViewEntity(index);
        }

        [CommandHandler("New")]
        public void OnNew(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            _BookCoordinateService.Save(currentBookCoordinate);
            currentBookCoordinate = new EdwardsIndexAppCommon.XPO.BookCoordinate();
            _BookCoordinateService.Add(currentBookCoordinate);
			SetViewEntity(index);
        }

        [CommandHandler("First")]
        public void OnFirst(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) BookCoordinateLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) BookCoordinateGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) BookCoordinateSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) BookCoordinateGridCRUDFormView.EndEdit();

            _BookCoordinateService.Save(currentBookCoordinate);
            index = 0;
            currentBookCoordinate = _BookCoordinateService.GetByIndex(index);
			SetViewEntity(index);
        }

        [CommandHandler("Previous")]
        public void OnPrevious(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            if (index > 0) index--;
			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) BookCoordinateLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) BookCoordinateGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) BookCoordinateSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) BookCoordinateGridCRUDFormView.EndEdit();

            _BookCoordinateService.Save(currentBookCoordinate);
            currentBookCoordinate = _BookCoordinateService.GetByIndex(index);
			SetViewEntity(index);
        }

        [CommandHandler("Next")]
        public void OnNext(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            if (index < _BookCoordinateService.Count() - 1) index++;

			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) BookCoordinateLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) BookCoordinateGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) BookCoordinateSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) BookCoordinateGridCRUDFormView.EndEdit();

            _BookCoordinateService.Save(currentBookCoordinate);
            currentBookCoordinate = _BookCoordinateService.GetByIndex(index);
			SetViewEntity(index);
        }

        [CommandHandler("Last")]
        public void OnLast(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            index = _BookCoordinateService.Count() - 1;
			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) BookCoordinateLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) BookCoordinateGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) BookCoordinateSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) BookCoordinateGridCRUDFormView.EndEdit();

            _BookCoordinateService.Save(currentBookCoordinate);
            currentBookCoordinate = _BookCoordinateService.GetByIndex(index);
			SetViewEntity(index);
        }
		
		private void UpdateUserAddressLabel(EdwardsIndexAppCommon.XPO.BookCoordinate BookCoordinate)
		{
			if (addressLabel != null) return;

			addressLabel = new BarStaticItem();
			UIExtensionSites[ExtensionSiteNames.MainStatus].Add(addressLabel);
			addressLabel.Caption = BookCoordinate.ToString(); //BookCoordinate.Address1;
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

			if (editBookCoordinateMenuItem1 != null)
				editBookCoordinateMenuItem1.Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;

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
		[CommandHandler("BookCoordinateMouseOver")]
		public void OnBookCoordinateEdit(object sender, EventArgs args)
		{
			if (Status != WorkItemStatus.Active) return;

			Form form = BookCoordinateSimpleCRUDFormView.ParentForm;
			string tooltipText = "This is BookCoordinate work item " + ID;
			var toolTip = new ToolTip {IsBalloon = true};
			if (form != null) 
				toolTip.Show(tooltipText, form, form.Size.Width - 30, 30, 3000);
		}
	}
}
