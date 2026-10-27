
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

using System.Threading;
using DevExpress.XtraEditors;

namespace EdwardsIndexAppModule.WorkItems.EdwardsIndexApp
{
	public partial class UserInfoView : XtraUserControl
	{
		public UserInfoView()
		{
			InitializeComponent();

			Principal.Text = Thread.CurrentPrincipal.Identity.Name;
		}
	}
}
