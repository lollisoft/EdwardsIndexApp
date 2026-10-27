
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


using Microsoft.Practices.CompositeUI;
namespace EdwardsIndexAppModule.WorkItems.BookCoordinate
{
	partial class BookCoordinateSummaryView
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
            if (controller.WorkItem.Status != WorkItemStatus.Terminated)
                controller.WorkItem.Terminate();
			if (disposing && (components != null))
			{
                components.Dispose();               
			}
			base.Dispose(disposing);        
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tabbedWorkspace1 = new CABDevExpress.Workspaces.XtraTabWorkspace();
			this.tabSummary = new DevExpress.XtraTab.XtraTabPage();
			this.BookCoordinateDetailView1 = new BookCoordinateDetailView();
			this.tabAccounts = new DevExpress.XtraTab.XtraTabPage();
			this.BookCoordinateAccountsView1 = new BookCoordinateAccount.BookCoordinateAccountsView();
			this.SaveButton = new DevExpress.XtraEditors.SimpleButton();
			this.BookCoordinateContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.BookCoordinateHeaderView1 = new BookCoordinateHeaderView();
			this.infoProvider = new Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider();
			((System.ComponentModel.ISupportInitialize)(this.tabbedWorkspace1)).BeginInit();
			this.tabbedWorkspace1.SuspendLayout();
			this.tabSummary.SuspendLayout();
			this.tabAccounts.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabbedWorkspace1
			// 
			this.tabbedWorkspace1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			                                                                      | System.Windows.Forms.AnchorStyles.Left)
			                                                                     | System.Windows.Forms.AnchorStyles.Right)));
			this.tabbedWorkspace1.Location = new System.Drawing.Point(4, 122);
			this.tabbedWorkspace1.Name = "tabbedWorkspace1";
			this.tabbedWorkspace1.SelectedTabPage = this.tabSummary;
			this.tabbedWorkspace1.Size = new System.Drawing.Size(539, 294);
			this.tabbedWorkspace1.TabIndex = 1;
			this.tabbedWorkspace1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
			                                                                             	this.tabSummary,
			                                                                             	this.tabAccounts});
			// 
			// tabSummary
			// 
			this.tabSummary.AutoScroll = true;
			this.tabSummary.Controls.Add(this.BookCoordinateDetailView1);
			this.tabSummary.Name = "tabSummary";
			this.tabSummary.Padding = new System.Windows.Forms.Padding(5);
			this.tabSummary.Size = new System.Drawing.Size(530, 289);
			this.tabSummary.Text = "Summary";
			// 
			// BookCoordinateDetailView1
			// 
			this.BookCoordinateDetailView1.AutoScroll = true;
			this.BookCoordinateDetailView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BookCoordinateDetailView1.Location = new System.Drawing.Point(5, 5);
			this.BookCoordinateDetailView1.MinimumSize = new System.Drawing.Size(445, 271);
			this.BookCoordinateDetailView1.Name = "BookCoordinateDetailView1";
			this.BookCoordinateDetailView1.Size = new System.Drawing.Size(520, 279);
			this.BookCoordinateDetailView1.TabIndex = 0;
			// 
			// tabAccounts
			// 
			this.tabAccounts.Controls.Add(this.BookCoordinateAccountsView1);
			this.tabAccounts.Name = "tabAccounts";
			this.tabAccounts.Padding = new System.Windows.Forms.Padding(5);
			this.tabAccounts.Size = new System.Drawing.Size(530, 289);
			this.tabAccounts.Text = "Accounts";
			// 
			// BookCoordinateAccountsView1
			// 
			this.BookCoordinateAccountsView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BookCoordinateAccountsView1.Location = new System.Drawing.Point(5, 5);
			this.BookCoordinateAccountsView1.Name = "BookCoordinateAccountsView1";
			this.BookCoordinateAccountsView1.Size = new System.Drawing.Size(520, 279);
			this.BookCoordinateAccountsView1.TabIndex = 0;
			// 
			// SaveButton
			// 
			this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.SaveButton.Location = new System.Drawing.Point(468, 422);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(75, 23);
			this.SaveButton.TabIndex = 2;
			this.SaveButton.Text = "Save";
			this.SaveButton.Click += new System.EventHandler(this.OnSave);
			// 
			// BookCoordinateContextMenu
			// 
			this.BookCoordinateContextMenu.Name = "BookCoordinateContextMenu";
			this.BookCoordinateContextMenu.Size = new System.Drawing.Size(61, 4);
			// 
			// BookCoordinateHeaderView1
			// 
			this.BookCoordinateHeaderView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                        | System.Windows.Forms.AnchorStyles.Right)));
			this.BookCoordinateHeaderView1.Location = new System.Drawing.Point(4, 4);
			this.BookCoordinateHeaderView1.Name = "BookCoordinateHeaderView1";
			this.BookCoordinateHeaderView1.Size = new System.Drawing.Size(535, 111);
			this.BookCoordinateHeaderView1.TabIndex = 0;
			// 
			// BookCoordinateSummaryView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ContextMenuStrip = this.BookCoordinateContextMenu;
			this.Controls.Add(this.tabbedWorkspace1);
			this.Controls.Add(this.SaveButton);
			this.Controls.Add(this.BookCoordinateHeaderView1);
			this.Name = "BookCoordinateSummaryView";
			this.Size = new System.Drawing.Size(546, 450);
			((System.ComponentModel.ISupportInitialize)(this.tabbedWorkspace1)).EndInit();
			this.tabbedWorkspace1.ResumeLayout(false);
			this.tabSummary.ResumeLayout(false);
			this.tabAccounts.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private CABDevExpress.Workspaces.XtraTabWorkspace tabbedWorkspace1;
		private DevExpress.XtraTab.XtraTabPage tabSummary;
		private BookCoordinateDetailView BookCoordinateDetailView1;
		private DevExpress.XtraTab.XtraTabPage tabAccounts;
		private BookCoordinateAccount.BookCoordinateAccountsView BookCoordinateAccountsView1;
		private BookCoordinateHeaderView BookCoordinateHeaderView1;
		private DevExpress.XtraEditors.SimpleButton SaveButton;
		private System.Windows.Forms.ContextMenuStrip BookCoordinateContextMenu;
		private Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider infoProvider;
	}
}
