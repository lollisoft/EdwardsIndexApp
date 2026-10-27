
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
using DevExpress.Xpo;
using DevExpress.XtraReports.UserDesigner;
using System.IO;

namespace EdwardsIndexAppModule.Reports
{
    public class IndexMarkerReportService : ReportTypeService
    {
		private static int _counter = 0;
        #region ReportTypeService Member

        public DevExpress.XtraReports.UI.XtraReport GetDefaultReport()
        {
			_counter++;
			IndexMarkerReport r = new IndexMarkerReport();
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EdwardsIndexApp");
			r.Name = string.Format(Path.Combine(path, "IndexMarkerReport{0}"), _counter);
			r.DisplayName = r.Name;
			r.LoadDataSource();
			
			return r;
		}

        public Type GetType(Type reportType)
        {
            return reportType;
        }

        #endregion
    }
}
