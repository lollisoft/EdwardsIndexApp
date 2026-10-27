
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

namespace EdwardsIndexAppCommon
{
	[Serializable]
	public class BookCoordinate
	{
		private readonly int id;

		public BookCoordinate()
		{
			id = -1;
		}
		public BookCoordinate(int _id
			, string _BookName
			, int _Page
			, string _Subject
			, int _Chapter
			, int _Verse
			, int _IndexMarker)
		{
			id = _id;
		
            this._BookName = _BookName;
			
            this._Page = _Page;
			
            this._Subject = _Subject;
			
            this._Chapter = _Chapter;
			
            this._Verse = _Verse;
			
            this._IndexMarker = _IndexMarker;
    
		}

		/// <summary>
		/// The unique id of the BookCoordinate.
		/// </summary>
		public int ID
		{
			get { return id; }
		}

		
		/// <summary>
		/// BookName of BookCoordinate.
		/// </summary>
		public string _BookName { get; set; }
			
		/// <summary>
		/// Page of BookCoordinate.
		/// </summary>
		public int _Page { get; set; }
			
		/// <summary>
		/// Subject of BookCoordinate.
		/// </summary>
		public string _Subject { get; set; }
			
		/// <summary>
		/// Chapter of BookCoordinate.
		/// </summary>
		public int _Chapter { get; set; }
			
		/// <summary>
		/// Verse of BookCoordinate.
		/// </summary>
		public int _Verse { get; set; }
			
		/// <summary>
		/// IndexMarker of BookCoordinate.
		/// </summary>
		public int _IndexMarker { get; set; }
	

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

			FormatStringValue += ", ";
			
			FormatStringValue += "{5}";

			return string.Format(FormatStringValue
			, _BookName
			, _Page
			, _Subject
			, _Chapter
			, _Verse
			, _IndexMarker).Substring(0, 20);

		}
	}
}
