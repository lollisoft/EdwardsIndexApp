
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
namespace EdwardsIndexAppModule.WorkItems.Annotation
{
	partial class AnnotationSummaryView
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
			this.AnnotationDetailView1 = new AnnotationDetailView();
			this.tabAccounts = new DevExpress.XtraTab.XtraTabPage();
			this.AnnotationAccountsView1 = new AnnotationAccount.AnnotationAccountsView();
			this.SaveButton = new DevExpress.XtraEditors.SimpleButton();
			this.AnnotationContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.AnnotationHeaderView1 = new AnnotationHeaderView();
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
			this.tabSummary.Controls.Add(this.AnnotationDetailView1);
			this.tabSummary.Name = "tabSummary";
			this.tabSummary.Padding = new System.Windows.Forms.Padding(5);
			this.tabSummary.Size = new System.Drawing.Size(530, 289);
			this.tabSummary.Text = "Summary";
			// 
			// AnnotationDetailView1
			// 
			this.AnnotationDetailView1.AutoScroll = true;
			this.AnnotationDetailView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AnnotationDetailView1.Location = new System.Drawing.Point(5, 5);
			this.AnnotationDetailView1.MinimumSize = new System.Drawing.Size(445, 271);
			this.AnnotationDetailView1.Name = "AnnotationDetailView1";
			this.AnnotationDetailView1.Size = new System.Drawing.Size(520, 279);
			this.AnnotationDetailView1.TabIndex = 0;
			// 
			// tabAccounts
			// 
			this.tabAccounts.Controls.Add(this.AnnotationAccountsView1);
			this.tabAccounts.Name = "tabAccounts";
			this.tabAccounts.Padding = new System.Windows.Forms.Padding(5);
			this.tabAccounts.Size = new System.Drawing.Size(530, 289);
			this.tabAccounts.Text = "Accounts";
			// 
			// AnnotationAccountsView1
			// 
			this.AnnotationAccountsView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AnnotationAccountsView1.Location = new System.Drawing.Point(5, 5);
			this.AnnotationAccountsView1.Name = "AnnotationAccountsView1";
			this.AnnotationAccountsView1.Size = new System.Drawing.Size(520, 279);
			this.AnnotationAccountsView1.TabIndex = 0;
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
			// AnnotationContextMenu
			// 
			this.AnnotationContextMenu.Name = "AnnotationContextMenu";
			this.AnnotationContextMenu.Size = new System.Drawing.Size(61, 4);
			// 
			// AnnotationHeaderView1
			// 
			this.AnnotationHeaderView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                        | System.Windows.Forms.AnchorStyles.Right)));
			this.AnnotationHeaderView1.Location = new System.Drawing.Point(4, 4);
			this.AnnotationHeaderView1.Name = "AnnotationHeaderView1";
			this.AnnotationHeaderView1.Size = new System.Drawing.Size(535, 111);
			this.AnnotationHeaderView1.TabIndex = 0;
			// 
			// AnnotationSummaryView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ContextMenuStrip = this.AnnotationContextMenu;
			this.Controls.Add(this.tabbedWorkspace1);
			this.Controls.Add(this.SaveButton);
			this.Controls.Add(this.AnnotationHeaderView1);
			this.Name = "AnnotationSummaryView";
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
		private AnnotationDetailView AnnotationDetailView1;
		private DevExpress.XtraTab.XtraTabPage tabAccounts;
		private AnnotationAccount.AnnotationAccountsView AnnotationAccountsView1;
		private AnnotationHeaderView AnnotationHeaderView1;
		private DevExpress.XtraEditors.SimpleButton SaveButton;
		private System.Windows.Forms.ContextMenuStrip AnnotationContextMenu;
		private Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider infoProvider;
	}
}
