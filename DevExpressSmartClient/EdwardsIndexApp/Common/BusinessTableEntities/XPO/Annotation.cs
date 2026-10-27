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
	public class Annotation : XPLiteObject
	{
		public Annotation() : base()
		{
			// This constructor is used when an object is loaded from a persistent storage. 
			// Do not place any code here. 
		}
		
		public Annotation(Session session)
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
		/// The unique id of the Annotation.
		/// </summary>
        [Key(true)]
        public int ID
        {
            get { return _id; }
            set { SetPropertyValue<int>("ID", ref _id, value); }
        }

		
		/// <summary>
		/// Text of Annotation.
		/// </summary>
		
		[Persistent("Text")]
		[DisplayName("Text")]
		public string _Text { get; set; }
			
		/// <summary>
		/// DateCreated of Annotation.
		/// </summary>
		
		[Persistent("DateCreated")]
		[DisplayName("DateCreated")]
		public DateTime _DateCreated { get; set; }
			

		// Relation definitions (collection side) Annotation Id: 39

		// Relation definitions (single side) Annotation Id: 39
		private IndexMarker _IndexMarker;
		[Association("IndexMarker-Annotation_IndexMarker")]
		[DisplayName("IndexMarker")]
		public IndexMarker IndexMarker {
		get { return _IndexMarker; }
		set { SetPropertyValue("IndexMarker", ref _IndexMarker, value); }
		}
	
		private Theme _Theme;
		[Association("Theme-Annotation_Theme")]
		[DisplayName("Theme")]
		public Theme Theme {
		get { return _Theme; }
		set { SetPropertyValue("Theme", ref _Theme, value); }
		}
	
	}
}
