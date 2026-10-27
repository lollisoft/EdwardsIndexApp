
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
	public class Annotation
	{
		private readonly int id;

		public Annotation()
		{
			id = -1;
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
		public DateTime _DateCreated { get; set; }
			
	}
}
