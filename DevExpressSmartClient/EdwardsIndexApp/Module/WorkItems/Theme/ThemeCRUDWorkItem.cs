
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




namespace EdwardsIndexAppModule.WorkItems.Theme
{
	public class ThemeCRUDWorkItem : WorkItem
	{
		public static readonly string ThemeDETAIL_TABWORKSPACE = "tabbedWorkspace1";

		
        ViewMode viewMode;

        internal ViewMode ViewMode
        {
            get { return viewMode; }
            set { viewMode = value; }
        }

		private WorkItem parentWorkItem;
        private Services.ThemeService _ThemeService;
        private EdwardsIndexAppCommon.XPO.Theme currentTheme;
		private int index;

		// React upon CRUD events only when active
		bool EnableSharedCRUDEvents;

        [ServiceDependency]
        public Services.ThemeService ThemeService
        {
            set { _ThemeService = value; index = 0;}
        }

		[ServiceDependency]
		public WorkItem ParentWorkItem
		{
			set { parentWorkItem = value; }
		}

		
		private BarItem editThemeMenuItem1;
		
		private EdwardsIndexAppModule.Forms.GenericDataLayoutCRUDFormView<EdwardsIndexAppCommon.XPO.Theme> ThemeGenericDataLayoutCRUDFormView;
		private ThemeSimpleCRUDFormView ThemeSimpleCRUDFormView;
		private ThemeLayoutCRUDFormView ThemeLayoutCRUDFormView;
        private ThemePerFormGridView ThemeGridCRUDFormView;
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
            if (ThemeGridCRUDFormView != null && ThemeGridCRUDFormView.IsDisposed)
	    		ThemeGridCRUDFormView = Items.AddNew<ThemePerFormGridView>();
            else if (ThemeGridCRUDFormView == null)
                ThemeGridCRUDFormView = Items.AddNew<ThemePerFormGridView>();
		}

		protected void ShowSimple()
		{
            if (ThemeSimpleCRUDFormView != null && ThemeSimpleCRUDFormView.IsDisposed)
	    		ThemeSimpleCRUDFormView = Items.AddNew<ThemeSimpleCRUDFormView>();
            else if (ThemeSimpleCRUDFormView == null)
                ThemeSimpleCRUDFormView = Items.AddNew<ThemeSimpleCRUDFormView>();
		}

		protected void ShowLayout()
		{
            if (ThemeLayoutCRUDFormView != null && ThemeLayoutCRUDFormView.IsDisposed)
	    		ThemeLayoutCRUDFormView = Items.AddNew<ThemeLayoutCRUDFormView>();
            else if (ThemeLayoutCRUDFormView == null)
                ThemeLayoutCRUDFormView = Items.AddNew<ThemeLayoutCRUDFormView>();
		}

		protected void ShowGenericDataLayout()
		{
            if (ThemeGenericDataLayoutCRUDFormView != null && ThemeGenericDataLayoutCRUDFormView.IsDisposed)
	    		ThemeGenericDataLayoutCRUDFormView = Items.AddNew<EdwardsIndexAppModule.Forms.GenericDataLayoutCRUDFormView<EdwardsIndexAppCommon.XPO.Theme>>();
            else if (ThemeGenericDataLayoutCRUDFormView == null)
                ThemeGenericDataLayoutCRUDFormView = Items.AddNew<EdwardsIndexAppModule.Forms.GenericDataLayoutCRUDFormView<EdwardsIndexAppCommon.XPO.Theme>>();
		}
		
		public void Show(IWorkspace parentWorkspace)
		{
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) ShowLayout();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) ShowGenericDataLayout();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) ShowSimple();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) ShowGrid();
			
			AddMenuItems();

			var Theme = (EdwardsIndexAppCommon.XPO.Theme)State[WorkItemStates.Theme];

			//OnStatusTextUpdate(string.Format("Editing {0}", Theme.ToString()));
			var smartPartInfo = new XtraWindowSmartPartInfo
			                    	{
			                    		Title = "Theme",
                                        Icon = Icon.FromHandle(Resources.Theme.GetHicon())
			                    	};
									
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm)
			{
				ThemeLayoutCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(ThemeLayoutCRUDFormView, smartPartInfo);
			}
			
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm)
			{
				ThemeGenericDataLayoutCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(ThemeGenericDataLayoutCRUDFormView, smartPartInfo);
			}
			
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm)
            {
				ThemeSimpleCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(ThemeSimpleCRUDFormView, smartPartInfo);
			}
			
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid)
            {
				ThemeGridCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(ThemeGridCRUDFormView, smartPartInfo);
			}

			//UpdateUserAddressLabel(Theme);

			Activate();
            parentWorkspace.SmartPartActivated += new EventHandler<WorkspaceEventArgs>(parentWorkspace_SmartPartActivated);

			// When activating, force focus on the first tab in the view.
			// Extensions may have added stuff at the end of the tab.
			//ThemeSimpleCRUDFormView.FocusFirstTab();
		}

        void parentWorkspace_SmartPartActivated(object sender, WorkspaceEventArgs e)
        {
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm)
			{
				if ((e.SmartPart==ThemeLayoutCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}

			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm)
			{
				if ((e.SmartPart==ThemeGenericDataLayoutCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}

			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm)
			{
				if ((e.SmartPart==ThemeSimpleCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}

			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid)
			{
				if ((e.SmartPart==ThemeGridCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}
			
        }

		protected void Refresh()
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) ThemeLayoutCRUDFormView.Refresh();
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) ThemeGenericDataLayoutCRUDFormView.Refresh();
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) ThemeSimpleCRUDFormView.Refresh();
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) ThemeGridCRUDFormView.RefreshData(false);
		}
		
		public void WorkWithTheme(EdwardsIndexAppCommon.XPO.Theme _Theme) {
		
		}
		
		// Move to position
		protected void SetViewEntity(int index)
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) ThemeLayoutCRUDFormView.Position(index);
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) ThemeGenericDataLayoutCRUDFormView.Position(index);
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) ThemeSimpleCRUDFormView.Position(index);
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) ThemeGridCRUDFormView.Position(index);
			
			Refresh();
		}

		protected EdwardsIndexAppCommon.XPO.Theme GetViewEntity()
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) return ThemeLayoutCRUDFormView.Theme;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) return ThemeGenericDataLayoutCRUDFormView.TItem;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) return ThemeSimpleCRUDFormView.Theme;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) return ThemeGridCRUDFormView.Theme;
			throw new Exception("View mode not supported");
		}
		
		protected bool IsViewDisposed()
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm && ThemeLayoutCRUDFormView.IsDisposed) return true;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm && ThemeGenericDataLayoutCRUDFormView.IsDisposed) return true;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm && ThemeSimpleCRUDFormView.IsDisposed) return true;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid && ThemeGridCRUDFormView.IsDisposed) return true;
			return false;
		}

        [CommandHandler("Save")]
        public void OnSave(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
 
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) ThemeLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) ThemeGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) ThemeSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) ThemeGridCRUDFormView.EndEdit();

			_ThemeService.Save(currentTheme);
        }
		
        [CommandHandler("Delete")]
        public void OnDelete(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents ) return;
			if (IsViewDisposed()) return;
            _ThemeService.Delete(currentTheme);
			
			if (index > _ThemeService.Count() - 1) index--;
			if (_ThemeService.Count() == 0)
				currentTheme = null;
			SetViewEntity(index);
        }

        [CommandHandler("New")]
        public void OnNew(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            _ThemeService.Save(currentTheme);
            currentTheme = new EdwardsIndexAppCommon.XPO.Theme();
            _ThemeService.Add(currentTheme);
			SetViewEntity(index);
        }

        [CommandHandler("First")]
        public void OnFirst(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) ThemeLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) ThemeGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) ThemeSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) ThemeGridCRUDFormView.EndEdit();

            _ThemeService.Save(currentTheme);
            index = 0;
            currentTheme = _ThemeService.GetByIndex(index);
			SetViewEntity(index);
        }

        [CommandHandler("Previous")]
        public void OnPrevious(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            if (index > 0) index--;
			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) ThemeLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) ThemeGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) ThemeSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) ThemeGridCRUDFormView.EndEdit();

            _ThemeService.Save(currentTheme);
            currentTheme = _ThemeService.GetByIndex(index);
			SetViewEntity(index);
        }

        [CommandHandler("Next")]
        public void OnNext(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            if (index < _ThemeService.Count() - 1) index++;

			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) ThemeLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) ThemeGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) ThemeSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) ThemeGridCRUDFormView.EndEdit();

            _ThemeService.Save(currentTheme);
            currentTheme = _ThemeService.GetByIndex(index);
			SetViewEntity(index);
        }

        [CommandHandler("Last")]
        public void OnLast(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            index = _ThemeService.Count() - 1;
			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) ThemeLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) ThemeGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) ThemeSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) ThemeGridCRUDFormView.EndEdit();

            _ThemeService.Save(currentTheme);
            currentTheme = _ThemeService.GetByIndex(index);
			SetViewEntity(index);
        }
		
		private void UpdateUserAddressLabel(EdwardsIndexAppCommon.XPO.Theme Theme)
		{
			if (addressLabel != null) return;

			addressLabel = new BarStaticItem();
			UIExtensionSites[ExtensionSiteNames.MainStatus].Add(addressLabel);
			addressLabel.Caption = Theme.ToString(); //Theme.Address1;
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

			if (editThemeMenuItem1 != null)
				editThemeMenuItem1.Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;

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
		[CommandHandler("ThemeMouseOver")]
		public void OnThemeEdit(object sender, EventArgs args)
		{
			if (Status != WorkItemStatus.Active) return;

			Form form = ThemeSimpleCRUDFormView.ParentForm;
			string tooltipText = "This is Theme work item " + ID;
			var toolTip = new ToolTip {IsBalloon = true};
			if (form != null) 
				toolTip.Show(tooltipText, form, form.Size.Width - 30, 30, 3000);
		}
	}
}
