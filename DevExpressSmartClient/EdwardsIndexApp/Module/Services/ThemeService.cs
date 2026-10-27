
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
	public class MockValueTheme {
		static Random r = new Random();
		
		public string StringValue { get { return "Mock value " + r.Next(10); } }
		public string BinaryValue { get { return "Mock value " + r.Next(10); } }
		public bool BitValue { get { return false; } }
		public int IntegerValue { get { return r.Next(10000); } }
	}
	[Service]
	public class ThemeService
	{

		XPCollection<Theme> collTheme = new XPCollection<Theme>();
		public XPCollection<Theme> Load()
		{
			int AccountIDValue = 1;

			collTheme.DeleteObjectOnRemove = true;
            collTheme.Reload();
		
            if (collTheme.Count == 0)
			{
				//CreateMockData();
                collTheme.Reload();
                collTheme.Load();
			}

            Trace.TraceInformation(string.Format("ThemeService.Load() returns {0} rows.", collTheme.Count));

			return collTheme;
		}



		public ThemeService()
		{
			int AccountIDValue = 1;
			

            //collTheme.Load();
            //if (collTheme.Count == 0)
            //{

				//CreateMockData();

			//}
			
		}

		public void Add(Theme newTheme)
		{
		
		}

		public void Delete(Theme newTheme)
		{
		
		}

		public void Save(Theme newTheme)
		{
		
		}
		
		public int Count()
		{

            return collTheme.Count;
			
		}
		
		public Theme GetByIndex(int index)
		{

            return collTheme[index];;

		}
		

	}
}
