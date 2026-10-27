
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

using EdwardsIndexAppCommon.XPO;
using Microsoft.Practices.CompositeUI;
using System;



namespace EdwardsIndexAppModule.Services
{
	[Service]
	public class BookCoordinateQueueService
	{
		private readonly BookCoordinate[] BookCoordinateArray;
		private int idx;

		public BookCoordinateQueueService()
		{
		}

		public bool HasMore
		{
			get
			{
				return idx < BookCoordinateArray.Length;
			}
		}

		public BookCoordinate GetNext()
		{
			if (!HasMore)
				return null;

			return BookCoordinateArray[idx++];
		}
	}
}
