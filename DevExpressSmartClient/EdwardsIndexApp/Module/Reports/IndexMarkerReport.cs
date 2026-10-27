
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
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using EdwardsIndexAppCommon.XPO;
using DevExpress.Xpo;



namespace EdwardsIndexAppModule.Reports
{
	public partial class IndexMarkerReport : XtraReport, IReport
	{
        public IndexMarkerReport()
        {
            InitializeComponent();
        }
		
		public void LoadDataSource()
		{
            DataSource = new XPCollection<EdwardsIndexAppCommon.XPO.IndexMarker>();
		}
	}
}
