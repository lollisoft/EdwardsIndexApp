
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

namespace EdwardsIndexAppModule.WorkItems.Annotation
{
	partial class AnnotationHeaderView
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
		
			this.lblAnnotationID = new System.Windows.Forms.Label();
			this.txtAnnotationID = new DevExpress.XtraEditors.TextEdit();

		

			this.AnnotationBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.AnnotationBindingSource)).BeginInit();
			this.SuspendLayout();
		
			// 
			// lblAnnotationID
			// 
			this.lblAnnotationID.AutoSize = true;
			this.lblAnnotationID.Location = new System.Drawing.Point(3, 9);
			this.lblAnnotationID.Name = "lblAnnotationID";
			this.lblAnnotationID.Size = new System.Drawing.Size(64, 13);
			this.lblAnnotationID.TabIndex = 2;
			this.lblAnnotationID.Text = "Annotation ID:";
			// 
			// txtAnnotationID
			// 
			this.txtAnnotationID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                  | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAnnotationID.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.AnnotationBindingSource, "ID", true));
			this.txtAnnotationID.Location = new System.Drawing.Point(73, 6);
			this.txtAnnotationID.Name = "txtAnnotationID";
			this.txtAnnotationID.Properties.ReadOnly = true;
			this.txtAnnotationID.Size = new System.Drawing.Size(176, 20);
			this.txtAnnotationID.TabIndex = 0;
		
			// 
			// AnnotationBindingSource
			// 

			this.AnnotationBindingSource.DataSource = typeof(EdwardsIndexAppCommon.Annotation);
			// 
			// AnnotationHeaderView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtAnnotationID);
			this.Controls.Add(this.lblAnnotationID);
		
			this.Name = "AnnotationHeaderView";

			
			
			this.Size = new System.Drawing.Size(256, 111);
			// Not calculated
			//this.Size = new System.Drawing.Size(256, 85);

			
			this.Load += new System.EventHandler(this.AnnotationHeaderView_Load);
			((System.ComponentModel.ISupportInitialize)(this.AnnotationBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		
		
		private System.Windows.Forms.Label lblAnnotationID;
		private DevExpress.XtraEditors.TextEdit txtAnnotationID;
		private System.Windows.Forms.BindingSource AnnotationBindingSource;
	}
}
