
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
	public class MockValueBookCoordinate {
		static Random r = new Random();
		
		public string StringValue { get { return "Mock value " + r.Next(10); } }
		public string BinaryValue { get { return "Mock value " + r.Next(10); } }
		public bool BitValue { get { return false; } }
		public int IntegerValue { get { return r.Next(10000); } }
	}
	[Service]
	public class BookCoordinateService
	{

		XPCollection<BookCoordinate> collBookCoordinate = new XPCollection<BookCoordinate>();
		public XPCollection<BookCoordinate> Load()
		{
			int AccountIDValue = 1;

			collBookCoordinate.DeleteObjectOnRemove = true;
            collBookCoordinate.Reload();
		
            if (collBookCoordinate.Count == 0)
			{
				//CreateMockData();
                collBookCoordinate.Reload();
                collBookCoordinate.Load();
			}

            Trace.TraceInformation(string.Format("BookCoordinateService.Load() returns {0} rows.", collBookCoordinate.Count));

			return collBookCoordinate;
		}



		public BookCoordinateService()
		{
			int AccountIDValue = 1;
			

            //collBookCoordinate.Load();
            //if (collBookCoordinate.Count == 0)
            //{

				//CreateMockData();

			//}
			
		}

		public void Add(BookCoordinate newBookCoordinate)
		{
		
		}

		public void Delete(BookCoordinate newBookCoordinate)
		{
		
		}

		public void Save(BookCoordinate newBookCoordinate)
		{
		
		}
		
		public int Count()
		{

            return collBookCoordinate.Count;
			
		}
		
		public BookCoordinate GetByIndex(int index)
		{

            return collBookCoordinate[index];;

		}
		

	}
}
