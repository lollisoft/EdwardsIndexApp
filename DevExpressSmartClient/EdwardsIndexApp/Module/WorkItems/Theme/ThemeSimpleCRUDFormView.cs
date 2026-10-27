
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



namespace EdwardsIndexAppModule.WorkItems.Theme
{
	[SmartPart]
	public partial class ThemeSimpleCRUDFormView : XtraUserControl
	{
		// The Theme state is stored in our parent work item
		private EdwardsIndexAppCommon.XPO.Theme _Theme;

		private int position;
		
		public void Position(int _position) {
			position = _position;
		}

		[State]
		public EdwardsIndexAppCommon.XPO.Theme Theme
		{
			set 
            {
                if (_Theme != null)
                {
                    _Theme.Save();
                }

                _Theme = value;
            }
			get 
            { 
                return _Theme; 
            }
		}

		public ThemeSimpleCRUDFormView()
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

			DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.Theme> coll = new DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.Theme>();
			ThemeBindingSource.DataSource = coll;
			ThemeBindingSource.Position = position;
		}

		public void EndEdit()
        {
            ThemeBindingSource.EndEdit();
        }

		private void ThemeSimpleCRUDFormView_Load(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				ThemeBindingSource.DataSource = _Theme;
			}
		}
	}
}
