
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
	partial class IndexMarkerCommentsView
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
			this.components = new System.ComponentModel.Container();
			this.IndexMarkerBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.commentsTextBox = new DevExpress.XtraEditors.MemoEdit();
			((System.ComponentModel.ISupportInitialize)(this.IndexMarkerBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// IndexMarkerBindingSource
			// 

			this.IndexMarkerBindingSource.DataSource = typeof(EdwardsIndexAppCommon.IndexMarker);
			// 
			// commentsTextBox
			// 
			this.commentsTextBox.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.IndexMarkerBindingSource, "Comments", true));
			this.commentsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.commentsTextBox.Location = new System.Drawing.Point(3, 3);
			this.commentsTextBox.Name = "commentsTextBox";
			this.commentsTextBox.Size = new System.Drawing.Size(368, 314);
			this.commentsTextBox.TabIndex = 1;
			// 
			// IndexMarkerCommentsView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.commentsTextBox);
			this.Name = "IndexMarkerCommentsView";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Size = new System.Drawing.Size(374, 320);
			this.Load += new System.EventHandler(this.IndexMarkerCommentsView_Load);
			((System.ComponentModel.ISupportInitialize)(this.IndexMarkerBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.BindingSource IndexMarkerBindingSource;
		private DevExpress.XtraEditors.MemoEdit commentsTextBox;
	}
}
