
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
	public class Theme
	{
		private readonly int id;

		public Theme()
		{
			id = -1;
		}
		public Theme(int _id
			, string _Name
			, string _Category)
		{
			id = _id;
		
            this._Name = _Name;
			
            this._Category = _Category;
			
		}

		/// <summary>
		/// The unique id of the Theme.
		/// </summary>
		public int ID
		{
			get { return id; }
		}

		
		/// <summary>
		/// Name of Theme.
		/// </summary>
		public string _Name { get; set; }
			
		/// <summary>
		/// Category of Theme.
		/// </summary>
		public string _Category { get; set; }
			

		public override string ToString()
		{
			string FormatStringValue = "";
		
			FormatStringValue += "{0}";

			FormatStringValue += ", ";
			
			FormatStringValue += "{1}";

			return string.Format(FormatStringValue
			, _Name
			, _Category).Substring(0, 20);

		}
	}
}
