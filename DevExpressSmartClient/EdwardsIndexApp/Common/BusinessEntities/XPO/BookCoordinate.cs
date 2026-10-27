
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

// Template: XPOLightEntity.cs
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
		public string _BookName { get; set; }
			
		/// <summary>
		/// Page of BookCoordinate.
		/// </summary>
		[Persistent("Page")]
		public int _Page { get; set; }
			
		/// <summary>
		/// Subject of BookCoordinate.
		/// </summary>
		[Persistent("Subject")]
		public string _Subject { get; set; }
			
		/// <summary>
		/// Chapter of BookCoordinate.
		/// </summary>
		[Persistent("Chapter")]
		public int _Chapter { get; set; }
			
		/// <summary>
		/// Verse of BookCoordinate.
		/// </summary>
		[Persistent("Verse")]
		public int _Verse { get; set; }
			
		/// <summary>
		/// IndexMarker of BookCoordinate.
		/// </summary>
		//[Persistent("IndexMarker")]
		//public int _IndexMarker { get; set; }
	

		// Relation definitions
	
	
		public override string ToString()
		{
			string FormatStringValue = "";
		
			FormatStringValue += "{0}";

			FormatStringValue += ", ";
			
			FormatStringValue += "{1}";

			FormatStringValue += ", ";
			
			FormatStringValue += "{2}";

			FormatStringValue += ", ";
			
			FormatStringValue += "{3}";

			FormatStringValue += ", ";
			
			FormatStringValue += "{4}";

			return string.Format(FormatStringValue, _BookName, _Page, _Subject, _Chapter, _Verse).Substring(0, 20);

		}
	}
}
