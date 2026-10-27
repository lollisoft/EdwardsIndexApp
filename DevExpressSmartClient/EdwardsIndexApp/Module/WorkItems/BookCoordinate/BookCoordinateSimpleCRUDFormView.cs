
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
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;



namespace EdwardsIndexAppModule.WorkItems.BookCoordinate
{
	[SmartPart]
	public partial class BookCoordinateSimpleCRUDFormView : XtraUserControl
	{
		// The BookCoordinate state is stored in our parent work item
		private EdwardsIndexAppCommon.XPO.BookCoordinate _BookCoordinate;

		private int position;
		
		public void Position(int _position) {
			position = _position;
		}

		[State]
		public EdwardsIndexAppCommon.XPO.BookCoordinate BookCoordinate
		{
			set 
            {
                if (_BookCoordinate != null)
                {
                    _BookCoordinate.Save();
                }

                _BookCoordinate = value;
            }
			get 
            { 
                return _BookCoordinate; 
            }
		}

		public BookCoordinateSimpleCRUDFormView()
		{
			InitializeComponent();
			
 
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			Refresh();
		}
		
		public void Refresh()
		{

			DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.BookCoordinate> coll = new DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.BookCoordinate>();
			BookCoordinateBindingSource.DataSource = coll;
			BookCoordinateBindingSource.Position = position;
		}

		public void EndEdit()
        {
            BookCoordinateBindingSource.EndEdit();
        }

		private void BookCoordinateSimpleCRUDFormView_Load(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				BookCoordinateBindingSource.DataSource = _BookCoordinate;
			}
		}
	}
}
