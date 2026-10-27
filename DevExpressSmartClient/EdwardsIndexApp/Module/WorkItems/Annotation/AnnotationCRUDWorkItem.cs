
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




namespace EdwardsIndexAppModule.WorkItems.Annotation
{
	public class AnnotationCRUDWorkItem : WorkItem
	{
		public static readonly string AnnotationDETAIL_TABWORKSPACE = "tabbedWorkspace1";

		
        ViewMode viewMode;

        internal ViewMode ViewMode
        {
            get { return viewMode; }
            set { viewMode = value; }
        }

		private WorkItem parentWorkItem;
        private Services.AnnotationService _AnnotationService;
        private EdwardsIndexAppCommon.XPO.Annotation currentAnnotation;
		private int index;

		// React upon CRUD events only when active
		bool EnableSharedCRUDEvents;

        [ServiceDependency]
        public Services.AnnotationService AnnotationService
        {
            set { _AnnotationService = value; index = 0;}
        }

		[ServiceDependency]
		public WorkItem ParentWorkItem
		{
			set { parentWorkItem = value; }
		}

		
		private BarItem editAnnotationMenuItem1;
		
		private EdwardsIndexAppModule.Forms.GenericDataLayoutCRUDFormView<EdwardsIndexAppCommon.XPO.Annotation> AnnotationGenericDataLayoutCRUDFormView;
		private AnnotationSimpleCRUDFormView AnnotationSimpleCRUDFormView;
		private AnnotationLayoutCRUDFormView AnnotationLayoutCRUDFormView;
        private AnnotationPerFormGridView AnnotationGridCRUDFormView;
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
            if (AnnotationGridCRUDFormView != null && AnnotationGridCRUDFormView.IsDisposed)
	    		AnnotationGridCRUDFormView = Items.AddNew<AnnotationPerFormGridView>();
            else if (AnnotationGridCRUDFormView == null)
                AnnotationGridCRUDFormView = Items.AddNew<AnnotationPerFormGridView>();
		}

		protected void ShowSimple()
		{
            if (AnnotationSimpleCRUDFormView != null && AnnotationSimpleCRUDFormView.IsDisposed)
	    		AnnotationSimpleCRUDFormView = Items.AddNew<AnnotationSimpleCRUDFormView>();
            else if (AnnotationSimpleCRUDFormView == null)
                AnnotationSimpleCRUDFormView = Items.AddNew<AnnotationSimpleCRUDFormView>();
		}

		protected void ShowLayout()
		{
            if (AnnotationLayoutCRUDFormView != null && AnnotationLayoutCRUDFormView.IsDisposed)
	    		AnnotationLayoutCRUDFormView = Items.AddNew<AnnotationLayoutCRUDFormView>();
            else if (AnnotationLayoutCRUDFormView == null)
                AnnotationLayoutCRUDFormView = Items.AddNew<AnnotationLayoutCRUDFormView>();
		}

		protected void ShowGenericDataLayout()
		{
            if (AnnotationGenericDataLayoutCRUDFormView != null && AnnotationGenericDataLayoutCRUDFormView.IsDisposed)
	    		AnnotationGenericDataLayoutCRUDFormView = Items.AddNew<EdwardsIndexAppModule.Forms.GenericDataLayoutCRUDFormView<EdwardsIndexAppCommon.XPO.Annotation>>();
            else if (AnnotationGenericDataLayoutCRUDFormView == null)
                AnnotationGenericDataLayoutCRUDFormView = Items.AddNew<EdwardsIndexAppModule.Forms.GenericDataLayoutCRUDFormView<EdwardsIndexAppCommon.XPO.Annotation>>();
		}
		
		public void Show(IWorkspace parentWorkspace)
		{
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) ShowLayout();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) ShowGenericDataLayout();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) ShowSimple();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) ShowGrid();
			
			AddMenuItems();

			var Annotation = (EdwardsIndexAppCommon.XPO.Annotation)State[WorkItemStates.Annotation];

			//OnStatusTextUpdate(string.Format("Editing {0}", Annotation.ToString()));
			var smartPartInfo = new XtraWindowSmartPartInfo
			                    	{
			                    		Title = "Annotation",
                                        Icon = Icon.FromHandle(Resources.Annotation.GetHicon())
			                    	};
									
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm)
			{
				AnnotationLayoutCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(AnnotationLayoutCRUDFormView, smartPartInfo);
			}
			
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm)
			{
				AnnotationGenericDataLayoutCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(AnnotationGenericDataLayoutCRUDFormView, smartPartInfo);
			}
			
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm)
            {
				AnnotationSimpleCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(AnnotationSimpleCRUDFormView, smartPartInfo);
			}
			
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid)
            {
				AnnotationGridCRUDFormView.Dock = DockStyle.Fill;
				parentWorkspace.Show(AnnotationGridCRUDFormView, smartPartInfo);
			}

			//UpdateUserAddressLabel(Annotation);

			Activate();
            parentWorkspace.SmartPartActivated += new EventHandler<WorkspaceEventArgs>(parentWorkspace_SmartPartActivated);

			// When activating, force focus on the first tab in the view.
			// Extensions may have added stuff at the end of the tab.
			//AnnotationSimpleCRUDFormView.FocusFirstTab();
		}

        void parentWorkspace_SmartPartActivated(object sender, WorkspaceEventArgs e)
        {
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm)
			{
				if ((e.SmartPart==AnnotationLayoutCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}

			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm)
			{
				if ((e.SmartPart==AnnotationGenericDataLayoutCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}

			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm)
			{
				if ((e.SmartPart==AnnotationSimpleCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}

			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid)
			{
				if ((e.SmartPart==AnnotationGridCRUDFormView)&& (this.Status==WorkItemStatus.Inactive))
					this.Activate();
			}
			
        }

		protected void Refresh()
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) AnnotationLayoutCRUDFormView.Refresh();
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) AnnotationGenericDataLayoutCRUDFormView.Refresh();
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) AnnotationSimpleCRUDFormView.Refresh();
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) AnnotationGridCRUDFormView.RefreshData(false);
		}
		
		public void WorkWithAnnotation(EdwardsIndexAppCommon.XPO.Annotation _Annotation) {
		
		}
		
		// Move to position
		protected void SetViewEntity(int index)
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) AnnotationLayoutCRUDFormView.Position(index);
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) AnnotationGenericDataLayoutCRUDFormView.Position(index);
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) AnnotationSimpleCRUDFormView.Position(index);
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) AnnotationGridCRUDFormView.Position(index);
			
			Refresh();
		}

		protected EdwardsIndexAppCommon.XPO.Annotation GetViewEntity()
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) return AnnotationLayoutCRUDFormView.Annotation;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) return AnnotationGenericDataLayoutCRUDFormView.TItem;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) return AnnotationSimpleCRUDFormView.Annotation;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) return AnnotationGridCRUDFormView.Annotation;
			throw new Exception("View mode not supported");
		}
		
		protected bool IsViewDisposed()
		{
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm && AnnotationLayoutCRUDFormView.IsDisposed) return true;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm && AnnotationGenericDataLayoutCRUDFormView.IsDisposed) return true;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm && AnnotationSimpleCRUDFormView.IsDisposed) return true;
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid && AnnotationGridCRUDFormView.IsDisposed) return true;
			return false;
		}

        [CommandHandler("Save")]
        public void OnSave(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
 
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) AnnotationLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) AnnotationGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) AnnotationSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) AnnotationGridCRUDFormView.EndEdit();

			_AnnotationService.Save(currentAnnotation);
        }
		
        [CommandHandler("Delete")]
        public void OnDelete(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents ) return;
			if (IsViewDisposed()) return;
            _AnnotationService.Delete(currentAnnotation);
			
			if (index > _AnnotationService.Count() - 1) index--;
			if (_AnnotationService.Count() == 0)
				currentAnnotation = null;
			SetViewEntity(index);
        }

        [CommandHandler("New")]
        public void OnNew(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            _AnnotationService.Save(currentAnnotation);
            currentAnnotation = new EdwardsIndexAppCommon.XPO.Annotation();
            _AnnotationService.Add(currentAnnotation);
			SetViewEntity(index);
        }

        [CommandHandler("First")]
        public void OnFirst(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) AnnotationLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) AnnotationGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) AnnotationSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) AnnotationGridCRUDFormView.EndEdit();

            _AnnotationService.Save(currentAnnotation);
            index = 0;
            currentAnnotation = _AnnotationService.GetByIndex(index);
			SetViewEntity(index);
        }

        [CommandHandler("Previous")]
        public void OnPrevious(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            if (index > 0) index--;
			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) AnnotationLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) AnnotationGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) AnnotationSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) AnnotationGridCRUDFormView.EndEdit();

            _AnnotationService.Save(currentAnnotation);
            currentAnnotation = _AnnotationService.GetByIndex(index);
			SetViewEntity(index);
        }

        [CommandHandler("Next")]
        public void OnNext(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            if (index < _AnnotationService.Count() - 1) index++;

			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) AnnotationLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) AnnotationGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) AnnotationSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) AnnotationGridCRUDFormView.EndEdit();

            _AnnotationService.Save(currentAnnotation);
            currentAnnotation = _AnnotationService.GetByIndex(index);
			SetViewEntity(index);
        }

        [CommandHandler("Last")]
        public void OnLast(object sender, EventArgs e)
        {
			if (!EnableSharedCRUDEvents) return;
			if (IsViewDisposed()) return;
            index = _AnnotationService.Count() - 1;
			
			if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewDesignableForm) AnnotationLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGenericDataLayoutForm) AnnotationGenericDataLayoutCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewFixedForm) AnnotationSimpleCRUDFormView.EndEdit();
            if (viewMode == EdwardsIndexAppModule.WorkItems.EdwardsIndexApp.ViewMode.ViewGrid) AnnotationGridCRUDFormView.EndEdit();

            _AnnotationService.Save(currentAnnotation);
            currentAnnotation = _AnnotationService.GetByIndex(index);
			SetViewEntity(index);
        }
		
		private void UpdateUserAddressLabel(EdwardsIndexAppCommon.XPO.Annotation Annotation)
		{
			if (addressLabel != null) return;

			addressLabel = new BarStaticItem();
			UIExtensionSites[ExtensionSiteNames.MainStatus].Add(addressLabel);
			addressLabel.Caption = Annotation.ToString(); //Annotation.Address1;
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

		//note let this string constant server to inform that it's not defined anywhere else in code
		[CommandHandler("AnnotationMouseOver")]
		public void OnAnnotationEdit(object sender, EventArgs args)
		{
			if (Status != WorkItemStatus.Active) return;

			Form form = AnnotationSimpleCRUDFormView.ParentForm;
			string tooltipText = "This is Annotation work item " + ID;
			var toolTip = new ToolTip {IsBalloon = true};
			if (form != null) 
				toolTip.Show(tooltipText, form, form.Size.Width - 30, 30, 3000);
		}
	}
}
