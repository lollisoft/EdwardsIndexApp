
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
	public class BookCoordinate
	{
		private readonly int id;

		public BookCoordinate()
		{
			id = -1;
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
			
	}
}
