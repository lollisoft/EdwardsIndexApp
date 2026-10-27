
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

namespace EdwardsIndexAppModule.WorkItems.IndexMarker
{
	partial class IndexMarkerHeaderView
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
		
			this.lblIndexMarkerID = new System.Windows.Forms.Label();
			this.txtIndexMarkerID = new DevExpress.XtraEditors.TextEdit();

		

			this.IndexMarkerBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.IndexMarkerBindingSource)).BeginInit();
			this.SuspendLayout();
		
			// 
			// lblIndexMarkerID
			// 
			this.lblIndexMarkerID.AutoSize = true;
			this.lblIndexMarkerID.Location = new System.Drawing.Point(3, 9);
			this.lblIndexMarkerID.Name = "lblIndexMarkerID";
			this.lblIndexMarkerID.Size = new System.Drawing.Size(64, 13);
			this.lblIndexMarkerID.TabIndex = 2;
			this.lblIndexMarkerID.Text = "IndexMarker ID:";
			// 
			// txtIndexMarkerID
			// 
			this.txtIndexMarkerID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                  | System.Windows.Forms.AnchorStyles.Right)));
			this.txtIndexMarkerID.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.IndexMarkerBindingSource, "ID", true));
			this.txtIndexMarkerID.Location = new System.Drawing.Point(73, 6);
			this.txtIndexMarkerID.Name = "txtIndexMarkerID";
			this.txtIndexMarkerID.Properties.ReadOnly = true;
			this.txtIndexMarkerID.Size = new System.Drawing.Size(176, 20);
			this.txtIndexMarkerID.TabIndex = 0;
		
			// 
			// IndexMarkerBindingSource
			// 

			this.IndexMarkerBindingSource.DataSource = typeof(EdwardsIndexAppCommon.IndexMarker);
			// 
			// IndexMarkerHeaderView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtIndexMarkerID);
			this.Controls.Add(this.lblIndexMarkerID);
		
			this.Name = "IndexMarkerHeaderView";

			
			
			this.Size = new System.Drawing.Size(256, 59);
			// Not calculated
			//this.Size = new System.Drawing.Size(256, 85);

			
			this.Load += new System.EventHandler(this.IndexMarkerHeaderView_Load);
			((System.ComponentModel.ISupportInitialize)(this.IndexMarkerBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		
		
		private System.Windows.Forms.Label lblIndexMarkerID;
		private DevExpress.XtraEditors.TextEdit txtIndexMarkerID;
		private System.Windows.Forms.BindingSource IndexMarkerBindingSource;
	}
}
