
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

// Template: TableEntity.cs
using System;

namespace EdwardsIndexAppCommon
{
	[Serializable]
	public class IndexMarker
	{
		private readonly int id;

		public IndexMarker()
		{
			id = -1;
		}

		/// <summary>
		/// The unique id of the IndexMarker.
		/// </summary>
		public int ID
		{
			get { return id; }
		}

		
		/// <summary>
		/// AlphanumericTag of IndexMarker.
		/// </summary>
		public string _AlphanumericTag { get; set; }
			
		/// <summary>
		/// DateCreated of IndexMarker.
		/// </summary>
		public DateTime _DateCreated { get; set; }
			
	}
}
