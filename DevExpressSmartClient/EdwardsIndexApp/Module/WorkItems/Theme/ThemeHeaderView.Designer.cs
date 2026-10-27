
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

namespace EdwardsIndexAppModule.WorkItems.Theme
{
	partial class ThemeHeaderView
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
			int tabIndex = 0;
			int yPosStart = 40;
			int yPos = yPosStart;

			



			this.components = new System.ComponentModel.Container();
		
			this.lblThemeID = new System.Windows.Forms.Label();
			this.txtThemeID = new DevExpress.XtraEditors.TextEdit();

		

			this.ThemeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.ThemeBindingSource)).BeginInit();
			this.SuspendLayout();
		
			// 
			// lblThemeID
			// 
			this.lblThemeID.AutoSize = true;
			this.lblThemeID.Location = new System.Drawing.Point(3, 9);
			this.lblThemeID.Name = "lblThemeID";
			this.lblThemeID.Size = new System.Drawing.Size(64, 13);
			this.lblThemeID.TabIndex = 2;
			this.lblThemeID.Text = "Theme ID:";
			// 
			// txtThemeID
			// 
			this.txtThemeID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                  | System.Windows.Forms.AnchorStyles.Right)));
			this.txtThemeID.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ThemeBindingSource, "ID", true));
			this.txtThemeID.Location = new System.Drawing.Point(73, 6);
			this.txtThemeID.Name = "txtThemeID";
			this.txtThemeID.Properties.ReadOnly = true;
			this.txtThemeID.Size = new System.Drawing.Size(176, 20);
			this.txtThemeID.TabIndex = 0;
		
			// 
			// ThemeBindingSource
			// 

			this.ThemeBindingSource.DataSource = typeof(EdwardsIndexAppCommon.Theme);
			// 
			// ThemeHeaderView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtThemeID);
			this.Controls.Add(this.lblThemeID);
		
			this.Name = "ThemeHeaderView";

			
			
			this.Size = new System.Drawing.Size(256, 59);
			// Not calculated
			//this.Size = new System.Drawing.Size(256, 85);

			
			this.Load += new System.EventHandler(this.ThemeHeaderView_Load);
			((System.ComponentModel.ISupportInitialize)(this.ThemeBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		
		
		private System.Windows.Forms.Label lblThemeID;
		private DevExpress.XtraEditors.TextEdit txtThemeID;
		private System.Windows.Forms.BindingSource ThemeBindingSource;
	}
}
