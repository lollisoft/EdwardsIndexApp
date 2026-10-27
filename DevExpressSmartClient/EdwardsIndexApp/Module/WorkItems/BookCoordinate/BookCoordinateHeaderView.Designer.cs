
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

namespace EdwardsIndexAppModule.WorkItems.BookCoordinate
{
	partial class BookCoordinateHeaderView
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
		
			this.lblBookCoordinateID = new System.Windows.Forms.Label();
			this.txtBookCoordinateID = new DevExpress.XtraEditors.TextEdit();

		

			this.BookCoordinateBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.BookCoordinateBindingSource)).BeginInit();
			this.SuspendLayout();
		
			// 
			// lblBookCoordinateID
			// 
			this.lblBookCoordinateID.AutoSize = true;
			this.lblBookCoordinateID.Location = new System.Drawing.Point(3, 9);
			this.lblBookCoordinateID.Name = "lblBookCoordinateID";
			this.lblBookCoordinateID.Size = new System.Drawing.Size(64, 13);
			this.lblBookCoordinateID.TabIndex = 2;
			this.lblBookCoordinateID.Text = "BookCoordinate ID:";
			// 
			// txtBookCoordinateID
			// 
			this.txtBookCoordinateID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                  | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBookCoordinateID.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.BookCoordinateBindingSource, "ID", true));
			this.txtBookCoordinateID.Location = new System.Drawing.Point(73, 6);
			this.txtBookCoordinateID.Name = "txtBookCoordinateID";
			this.txtBookCoordinateID.Properties.ReadOnly = true;
			this.txtBookCoordinateID.Size = new System.Drawing.Size(176, 20);
			this.txtBookCoordinateID.TabIndex = 0;
		
			// 
			// BookCoordinateBindingSource
			// 

			this.BookCoordinateBindingSource.DataSource = typeof(EdwardsIndexAppCommon.BookCoordinate);
			// 
			// BookCoordinateHeaderView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtBookCoordinateID);
			this.Controls.Add(this.lblBookCoordinateID);
		
			this.Name = "BookCoordinateHeaderView";

			
			
			this.Size = new System.Drawing.Size(256, 163);
			// Not calculated
			//this.Size = new System.Drawing.Size(256, 85);

			
			this.Load += new System.EventHandler(this.BookCoordinateHeaderView_Load);
			((System.ComponentModel.ISupportInitialize)(this.BookCoordinateBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		
		
		private System.Windows.Forms.Label lblBookCoordinateID;
		private DevExpress.XtraEditors.TextEdit txtBookCoordinateID;
		private System.Windows.Forms.BindingSource BookCoordinateBindingSource;
	}
}
