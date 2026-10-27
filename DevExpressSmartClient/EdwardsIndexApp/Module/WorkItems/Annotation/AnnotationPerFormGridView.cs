
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
using System.Windows.Forms;
using EdwardsIndexAppModule.Constants;
using EdwardsIndexAppModule.Services;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;



namespace EdwardsIndexAppModule.WorkItems.Annotation
{
	[SmartPart]
	public partial class AnnotationPerFormGridView : XtraUserControl
	{

		private EdwardsIndexAppCommon.XPO.Annotation _Annotation;
		public EdwardsIndexAppCommon.XPO.Annotation Annotation
        {
            get { return _Annotation; }
            set { _Annotation = value; }
        }

		private int position;
		
		public void Position(int _position) {
			position = _position;
		}

		
		//private AnnotationService _AnnotationService;


		private System.Windows.Forms.BindingSource IndexMarkerBindingSource = new System.Windows.Forms.BindingSource();

		private System.Windows.Forms.BindingSource BookCoordinateBindingSource = new System.Windows.Forms.BindingSource();

		private System.Windows.Forms.BindingSource AnnotationBindingSource = new System.Windows.Forms.BindingSource();

		private System.Windows.Forms.BindingSource ThemeBindingSource = new System.Windows.Forms.BindingSource();

		private IndexMarkerService _IndexMarkerService;

		private BookCoordinateService _BookCoordinateService;

		private AnnotationService _AnnotationService;

		private ThemeService _ThemeService;


		private readonly DXMenuItem popupMenuItem = new DXMenuItem("Say Hello...");


		// Make sure our required IndexMarkerService is available
		[ServiceDependency]
		public IndexMarkerService IndexMarkerService
		{
			set { _IndexMarkerService = value; }
		}

		// Make sure our required BookCoordinateService is available
		[ServiceDependency]
		public BookCoordinateService BookCoordinateService
		{
			set { _BookCoordinateService = value; }
		}

		// Make sure our required AnnotationService is available
		[ServiceDependency]
		public AnnotationService AnnotationService
		{
			set { _AnnotationService = value; }
		}

		// Make sure our required ThemeService is available
		[ServiceDependency]
		public ThemeService ThemeService
		{
			set { _ThemeService = value; }
		}



		
		public void SelectAnnotation(EdwardsIndexAppCommon.XPO.Annotation _Annotation)
		{
		
		}
		
		[ServiceDependency]
		public WorkItem ParentWorkItem { protected get; set; }

		public AnnotationPerFormGridView()
		{
			InitializeComponent();
			
			// If the grid must be readonly, apply this
			//this.gridAnnotationView.OptionsBehavior.Editable = false;

			gridAnnotationView.MouseDown += grid_MouseDown;
			

			
		}

		void grid_MouseDown(object sender, MouseEventArgs e)
		{
			bool RightMouseClicked = (e.Button & MouseButtons.Right) != 0;
			if (RightMouseClicked)
			{
				var view = sender as GridView;
				var mouseEventArgs = e as DXMouseEventArgs;
				if (view == null || mouseEventArgs == null)
					return;

				GridHitInfo hitInfo = view.CalcHitInfo(mouseEventArgs.Location);
				view.FocusedRowHandle = hitInfo.RowHandle;

				CreateAndShowPopupMenu(hitInfo, view);
			}
		}

		protected void CreateAndShowPopupMenu(GridHitInfo hitInfo, GridView view)
		{
			var menu = new ViewMenu(view);
			menu.Items.Add(popupMenuItem);
			menu.Show(hitInfo.HitPoint);
		}

		private void AnnotationsView_Load(object sender, EventArgs e)
		{
			HookupCabEvents();

			RefreshData(true);
		}

        private void gridAnnotationView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                RefreshData(false);
            }
        }

		public void RefreshData(bool initial) 
		{
			try
            {
                if (initial)
                {
					AnnotationBindingSource.DataSource = _AnnotationService.Load();
					gridAnnotation.DataSource = AnnotationBindingSource;
					AnnotationBindingSource.Position = position;
					gridAnnotationView.SelectRow(gridAnnotationView.GetRowHandle(position));
					IndexMarkerBindingSource.DataSource = _IndexMarkerService.Load();
					repositoryItemSearchLookUpEditIndexMarker.DataSource = IndexMarkerBindingSource;

					ThemeBindingSource.DataSource = _ThemeService.Load();
					repositoryItemSearchLookUpEditTheme.DataSource = ThemeBindingSource;

					
				}
                else 
                {
					int _pos = AnnotationBindingSource.Position;
					gridAnnotationView.BeginInit();
			
					repositoryItemSearchLookUpEditIndexMarker.BeginUpdate();
					repositoryItemSearchLookUpEditTheme.BeginUpdate();
					AnnotationBindingSource.DataSource = null;
					IndexMarkerBindingSource.DataSource = null;
					ThemeBindingSource.DataSource = null;
					AnnotationBindingSource.DataSource = _AnnotationService.Load();
					IndexMarkerBindingSource.DataSource = _IndexMarkerService.Load();
					ThemeBindingSource.DataSource = _ThemeService.Load();
					repositoryItemSearchLookUpEditIndexMarkerView.RefreshEditor(true);
					repositoryItemSearchLookUpEditThemeView.RefreshEditor(true);
					repositoryItemSearchLookUpEditIndexMarker.EndUpdate();
					repositoryItemSearchLookUpEditTheme.EndUpdate();
					

					gridAnnotationView.EndInit();
					AnnotationBindingSource.Position = _pos;
				}
			}
			catch (Exception e)
            {
                XtraMessageBox.Show("Daten konnten nicht geladen werden. Bitte versuchen Sie es nochmal.", "Fehler", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
		}
		
		
		public void EndEdit()
        {
            AnnotationBindingSource.EndEdit();
        }

		private void HookupCabEvents()
		{
			//ParentWorkItem.Commands[CommandNames.DxMenuSamplePopup].AddInvoker(popupMenuItem, "Click");
		}
	}
}
