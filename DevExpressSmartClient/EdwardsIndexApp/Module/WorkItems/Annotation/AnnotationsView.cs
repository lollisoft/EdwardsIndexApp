
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
	public partial class AnnotationsView : XtraUserControl
	{
		
		private AnnotationService _AnnotationService;

		private readonly DXMenuItem popupMenuItem = new DXMenuItem("Say Hello...");

		private int position;
		
		public void Position(int _position) {
			position = _position;
		}



		// Make sure our required AnnotationService is available
		[ServiceDependency]
		public AnnotationService AnnotationService
		{
			set { _AnnotationService = value; }
		}

		[ServiceDependency]
		public WorkItem ParentWorkItem { protected get; set; }

		public AnnotationsView()
		{
			InitializeComponent();
			
			// If the grid must be readonly, apply this
			//this.gridAccountsView.OptionsBehavior.Editable = false;

			gridAccountsView.MouseDown += grid_MouseDown;
			
 
			
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

		}

		private void HookupCabEvents()
		{
			ParentWorkItem.Commands[CommandNames.DxMenuSamplePopup].AddInvoker(popupMenuItem, "Click");
		}
	}
}
