
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



namespace EdwardsIndexAppModule.WorkItems.BookCoordinate
{
	[SmartPart]
	public partial class BookCoordinatePerFormGridView : XtraUserControl
	{

		private EdwardsIndexAppCommon.XPO.BookCoordinate _BookCoordinate;
		public EdwardsIndexAppCommon.XPO.BookCoordinate BookCoordinate
        {
            get { return _BookCoordinate; }
            set { _BookCoordinate = value; }
        }

		private int position;
		
		public void Position(int _position) {
			position = _position;
		}

		
		//private BookCoordinateService _BookCoordinateService;


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



		
		public void SelectBookCoordinate(EdwardsIndexAppCommon.XPO.BookCoordinate _BookCoordinate)
		{
		
		}
		
		[ServiceDependency]
		public WorkItem ParentWorkItem { protected get; set; }

		public BookCoordinatePerFormGridView()
		{
			InitializeComponent();
			
			// If the grid must be readonly, apply this
			//this.gridBookCoordinateView.OptionsBehavior.Editable = false;

			gridBookCoordinateView.MouseDown += grid_MouseDown;
			

			
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

		private void BookCoordinatesView_Load(object sender, EventArgs e)
		{
			HookupCabEvents();

			RefreshData(true);
		}

        private void gridBookCoordinateView_KeyUp(object sender, KeyEventArgs e)
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
					BookCoordinateBindingSource.DataSource = _BookCoordinateService.Load();
					gridBookCoordinate.DataSource = BookCoordinateBindingSource;
					BookCoordinateBindingSource.Position = position;
					gridBookCoordinateView.SelectRow(gridBookCoordinateView.GetRowHandle(position));
					IndexMarkerBindingSource.DataSource = _IndexMarkerService.Load();
					repositoryItemSearchLookUpEditIndexMarker.DataSource = IndexMarkerBindingSource;

					
				}
                else 
                {
					int _pos = BookCoordinateBindingSource.Position;
					gridBookCoordinateView.BeginInit();
			
					repositoryItemSearchLookUpEditIndexMarker.BeginUpdate();
					BookCoordinateBindingSource.DataSource = null;
					IndexMarkerBindingSource.DataSource = null;
					BookCoordinateBindingSource.DataSource = _BookCoordinateService.Load();
					IndexMarkerBindingSource.DataSource = _IndexMarkerService.Load();
					repositoryItemSearchLookUpEditIndexMarkerView.RefreshEditor(true);
					repositoryItemSearchLookUpEditIndexMarker.EndUpdate();
					

					gridBookCoordinateView.EndInit();
					BookCoordinateBindingSource.Position = _pos;
				}
			}
			catch (Exception e)
            {
                XtraMessageBox.Show("Daten konnten nicht geladen werden. Bitte versuchen Sie es nochmal.", "Fehler", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
		}
		
		
		public void EndEdit()
        {
            BookCoordinateBindingSource.EndEdit();
        }

		private void HookupCabEvents()
		{
			//ParentWorkItem.Commands[CommandNames.DxMenuSamplePopup].AddInvoker(popupMenuItem, "Click");
		}
	}
}
