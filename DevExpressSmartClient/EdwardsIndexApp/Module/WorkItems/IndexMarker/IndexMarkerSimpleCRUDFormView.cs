
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



namespace EdwardsIndexAppModule.WorkItems.IndexMarker
{
	[SmartPart]
	public partial class IndexMarkerSimpleCRUDFormView : XtraUserControl
	{
		// The IndexMarker state is stored in our parent work item
		private EdwardsIndexAppCommon.XPO.IndexMarker _IndexMarker;

		private int position;
		
		public void Position(int _position) {
			position = _position;
		}

		[State]
		public EdwardsIndexAppCommon.XPO.IndexMarker IndexMarker
		{
			set 
            {
                if (_IndexMarker != null)
                {
                    _IndexMarker.Save();
                }

                _IndexMarker = value;
            }
			get 
            { 
                return _IndexMarker; 
            }
		}

		public IndexMarkerSimpleCRUDFormView()
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

			DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.IndexMarker> coll = new DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.IndexMarker>();
			IndexMarkerBindingSource.DataSource = coll;
			IndexMarkerBindingSource.Position = position;
		}

		public void EndEdit()
        {
            IndexMarkerBindingSource.EndEdit();
        }

		private void IndexMarkerSimpleCRUDFormView_Load(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				IndexMarkerBindingSource.DataSource = _IndexMarker;
			}
		}
	}
}
