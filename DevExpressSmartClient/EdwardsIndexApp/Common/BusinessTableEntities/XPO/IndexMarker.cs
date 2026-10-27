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
	public class IndexMarker : XPLiteObject
	{
		public IndexMarker() : base()
		{
			// This constructor is used when an object is loaded from a persistent storage. 
			// Do not place any code here. 
		}
		
		public IndexMarker(Session session)
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
		/// The unique id of the IndexMarker.
		/// </summary>
        [Key(true)]
        public int ID
        {
            get { return _id; }
            set { SetPropertyValue<int>("ID", ref _id, value); }
        }

		
		/// <summary>
		/// AlphanumericTag of IndexMarker.
		/// </summary>
		
		[Persistent("AlphanumericTag")]
		[DisplayName("AlphanumericTag")]
		public string _AlphanumericTag { get; set; }
			
		/// <summary>
		/// DateCreated of IndexMarker.
		/// </summary>
		
		[Persistent("DateCreated")]
		[DisplayName("DateCreated")]
		public DateTime _DateCreated { get; set; }
			

		// Relation definitions (collection side) IndexMarker Id: 37
		[Association("IndexMarker-BookCoordinate_IndexMarker")]
		[DisplayName("BookCoordinate")]
		public XPCollection<BookCoordinate> BookCoordinate_IndexMarker { get { return GetCollection<BookCoordinate>("BookCoordinate_IndexMarker"); } }
		
		[Association("IndexMarker-Annotation_IndexMarker")]
		[DisplayName("Annotation")]
		public XPCollection<Annotation> Annotation_IndexMarker { get { return GetCollection<Annotation>("Annotation_IndexMarker"); } }
		

		// Relation definitions (single side) IndexMarker Id: 37
	}
}
