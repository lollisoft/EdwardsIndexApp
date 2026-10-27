
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

using EdwardsIndexAppModule.Services;
using Microsoft.Practices.CompositeUI;

namespace EdwardsIndexAppModule.WorkItems.EdwardsIndexApp
{
	public class CustomerQueueController : Controller
	{
		// We depend on the customer queue service to tell us which customer is next
		private CustomerQueueService customerQueueService;

		[ServiceDependency]
		public CustomerQueueService CustomerQueueService
		{
			set { customerQueueService = value; }
		}

		public new EdwardsIndexAppWorkItem WorkItem
		{
			get { return base.WorkItem as EdwardsIndexAppWorkItem; }
		}

		public EdwardsIndexAppCommon.Customer GetNextCustomerInQueue()
		{
			return customerQueueService.GetNext();
		}

		public void WorkWithCustomer(EdwardsIndexAppCommon.Customer customer)
		{
			WorkItem.WorkWithCustomer(customer);
		}
	}
}
