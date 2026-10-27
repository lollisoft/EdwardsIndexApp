
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
	public class MockValueAnnotation {
		static Random r = new Random();
		
		public string StringValue { get { return "Mock value " + r.Next(10); } }
		public string BinaryValue { get { return "Mock value " + r.Next(10); } }
		public bool BitValue { get { return false; } }
		public int IntegerValue { get { return r.Next(10000); } }
	}
	[Service]
	public class AnnotationService
	{

		XPCollection<Annotation> collAnnotation = new XPCollection<Annotation>();
		public XPCollection<Annotation> Load()
		{
			int AccountIDValue = 1;

			collAnnotation.DeleteObjectOnRemove = true;
            collAnnotation.Reload();
		
            if (collAnnotation.Count == 0)
			{
				//CreateMockData();
                collAnnotation.Reload();
                collAnnotation.Load();
			}

            Trace.TraceInformation(string.Format("AnnotationService.Load() returns {0} rows.", collAnnotation.Count));

			return collAnnotation;
		}



		public AnnotationService()
		{
			int AccountIDValue = 1;
			

            //collAnnotation.Load();
            //if (collAnnotation.Count == 0)
            //{

				//CreateMockData();

			//}
			
		}

		public void Add(Annotation newAnnotation)
		{
		
		}

		public void Delete(Annotation newAnnotation)
		{
		
		}

		public void Save(Annotation newAnnotation)
		{
		
		}
		
		public int Count()
		{

            return collAnnotation.Count;
			
		}
		
		public Annotation GetByIndex(int index)
		{

            return collAnnotation[index];;

		}
		

	}
}
