
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



namespace EdwardsIndexAppModule.WorkItems.Annotation
{
	[SmartPart]
	public partial class AnnotationSimpleCRUDFormView : XtraUserControl
	{
		// The Annotation state is stored in our parent work item
		private EdwardsIndexAppCommon.XPO.Annotation _Annotation;

		private int position;
		
		public void Position(int _position) {
			position = _position;
		}

		[State]
		public EdwardsIndexAppCommon.XPO.Annotation Annotation
		{
			set 
            {
                if (_Annotation != null)
                {
                    _Annotation.Save();
                }

                _Annotation = value;
            }
			get 
            { 
                return _Annotation; 
            }
		}

		public AnnotationSimpleCRUDFormView()
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

			DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.Annotation> coll = new DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.Annotation>();
			AnnotationBindingSource.DataSource = coll;
			AnnotationBindingSource.Position = position;
		}

		public void EndEdit()
        {
            AnnotationBindingSource.EndEdit();
        }

		private void AnnotationSimpleCRUDFormView_Load(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				AnnotationBindingSource.DataSource = _Annotation;
			}
		}
	}
}
