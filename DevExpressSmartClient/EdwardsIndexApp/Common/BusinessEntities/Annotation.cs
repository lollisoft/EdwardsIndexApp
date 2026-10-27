
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
	public class Annotation
	{
		private readonly int id;

		public Annotation()
		{
			id = -1;
		}
		public Annotation(int _id
			, string _Text
			/*,unknown _DateCreated*/
			
			, int _IndexMarker
			, int _Theme)
		{
			id = _id;
		
            this._Text = _Text;
			
            //this._DateCreated = _DateCreated;
			
            this._IndexMarker = _IndexMarker;
    
            this._Theme = _Theme;
    
		}

		/// <summary>
		/// The unique id of the Annotation.
		/// </summary>
		public int ID
		{
			get { return id; }
		}

		
		/// <summary>
		/// Text of Annotation.
		/// </summary>
		public string _Text { get; set; }
			
		/// <summary>
		/// DateCreated of Annotation.
		/// </summary>
		//public _DateTimeDateCreated { get; set; }
			
		/// <summary>
		/// IndexMarker of Annotation.
		/// </summary>
		public int _IndexMarker { get; set; }
	
		/// <summary>
		/// Theme of Annotation.
		/// </summary>
		public int _Theme { get; set; }
	

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

			return string.Format(FormatStringValue
			, _Text
			, _DateCreated
			, _IndexMarker
			, _Theme).Substring(0, 20);

		}
	}
}
