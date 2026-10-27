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

// Template: XPOTableLightEntity.cs
using System;
using DevExpress.Xpo;

namespace EdwardsIndexAppCommon.XPO
{
	public class Theme : XPLiteObject
	{
		public Theme() : base()
		{
			// This constructor is used when an object is loaded from a persistent storage. 
			// Do not place any code here. 
		}
		
		public Theme(Session session)
			: base(session)
		{
			// This constructor is used when an object is loaded from a persistent storage. 
			// Do not place any code here. 
		}

	    public override void AfterConstruction()
		{
			base.AfterConstruction();
			// Place here your initialization code. 
		}

        int _id;

		/// <summary>
		/// The unique id of the Theme.
		/// </summary>
        [Key(true)]
        public int ID
        {
            get { return _id; }
            set { SetPropertyValue<int>("ID", ref _id, value); }
        }

		
		/// <summary>
		/// Name of Theme.
		/// </summary>
		
		[Persistent("Name")]
		[DisplayName("Name")]
		public string _Name { get; set; }
			
		/// <summary>
		/// Category of Theme.
		/// </summary>
		
		[Persistent("Category")]
		[DisplayName("Category")]
		public string _Category { get; set; }
			

		// Relation definitions (collection side) Theme Id: 40
		[Association("Theme-Annotation_Theme")]
		[DisplayName("Annotation")]
		public XPCollection<Annotation> Annotation_Theme { get { return GetCollection<Annotation>("Annotation_Theme"); } }
		

		// Relation definitions (single side) Theme Id: 40
	}
}
