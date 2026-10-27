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
	public class BookCoordinate : XPLiteObject
	{
		public BookCoordinate() : base()
		{
			// This constructor is used when an object is loaded from a persistent storage. 
			// Do not place any code here. 
		}
		
		public BookCoordinate(Session session)
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
		/// The unique id of the BookCoordinate.
		/// </summary>
        [Key(true)]
        public int ID
        {
            get { return _id; }
            set { SetPropertyValue<int>("ID", ref _id, value); }
        }

		
		/// <summary>
		/// BookName of BookCoordinate.
		/// </summary>
		
		[Persistent("BookName")]
		[DisplayName("BookName")]
		public string _BookName { get; set; }
					
		[Persistent("Page")]
		[DisplayName("Page")]
		public int _Page { get; set; }

		/// <summary>
		/// Subject of BookCoordinate.
		/// </summary>
		
		[Persistent("Subject")]
		[DisplayName("Subject")]
		public string _Subject { get; set; }
					
		[Persistent("Chapter")]
		[DisplayName("Chapter")]
		public int _Chapter { get; set; }
		
		[Persistent("Verse")]
		[DisplayName("Verse")]
		public int _Verse { get; set; }


		// Relation definitions (collection side) BookCoordinate Id: 38

		// Relation definitions (single side) BookCoordinate Id: 38
		private IndexMarker _IndexMarker;
		[Association("IndexMarker-BookCoordinate_IndexMarker")]
		[DisplayName("IndexMarker")]
		public IndexMarker IndexMarker {
		get { return _IndexMarker; }
		set { SetPropertyValue("IndexMarker", ref _IndexMarker, value); }
		}
	
	}
}
