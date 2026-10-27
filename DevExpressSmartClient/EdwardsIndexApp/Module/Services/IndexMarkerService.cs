
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

// The example companies, organizations, products, domain names, e-mail addresses,
// logos, people, places, and events depicted herein are fictitious.  No association
// with any real company, organization, product, domain name, email address, logo,
// person, places, or events is intended or should be inferred.

using System.Collections;
using EdwardsIndexAppCommon.XPO;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Diagnostics;
using System.Collections.Generic;

using DevExpress.Xpo;


namespace EdwardsIndexAppModule.Services
{
	public class MockValueIndexMarker {
		static Random r = new Random();
		
		public string StringValue { get { return "Mock value " + r.Next(10); } }
		public string BinaryValue { get { return "Mock value " + r.Next(10); } }
		public bool BitValue { get { return false; } }
		public int IntegerValue { get { return r.Next(10000); } }
	}
	[Service]
	public class IndexMarkerService
	{

		XPCollection<IndexMarker> collIndexMarker = new XPCollection<IndexMarker>();
		public XPCollection<IndexMarker> Load()
		{
			int AccountIDValue = 1;

			collIndexMarker.DeleteObjectOnRemove = true;
            collIndexMarker.Reload();
		
            if (collIndexMarker.Count == 0)
			{
				//CreateMockData();
                collIndexMarker.Reload();
                collIndexMarker.Load();
			}

            Trace.TraceInformation(string.Format("IndexMarkerService.Load() returns {0} rows.", collIndexMarker.Count));

			return collIndexMarker;
		}



		public IndexMarkerService()
		{
			int AccountIDValue = 1;
			

            //collIndexMarker.Load();
            //if (collIndexMarker.Count == 0)
            //{

				//CreateMockData();

			//}
			
		}

		public void Add(IndexMarker newIndexMarker)
		{
		
		}

		public void Delete(IndexMarker newIndexMarker)
		{
		
		}

		public void Save(IndexMarker newIndexMarker)
		{
		
		}
		
		public int Count()
		{

            return collIndexMarker.Count;
			
		}
		
		public IndexMarker GetByIndex(int index)
		{

            return collIndexMarker[index];;

		}
		

	}
}
