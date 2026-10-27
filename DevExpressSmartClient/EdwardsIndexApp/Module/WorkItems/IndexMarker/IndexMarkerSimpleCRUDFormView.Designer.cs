
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
	partial class IndexMarkerSimpleCRUDFormView
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
			//int tabIndex = 0;
			//int yPosStart = 40;
			//int yPos = yPosStart;

			



			this.components = new System.ComponentModel.Container();
			

			this.lblAlphanumericTag = new System.Windows.Forms.Label();
			
			this.txtAlphanumericTag = new DevExpress.XtraEditors.TextEdit();
	

			this.IndexMarkerBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.IndexMarkerBindingSource)).BeginInit();
			this.SuspendLayout();


			// 
			// lblAlphanumericTag
			// 
			this.lblAlphanumericTag.AutoSize = true;
			this.lblAlphanumericTag.Location = new System.Drawing.Point(5, 6);
			this.lblAlphanumericTag.Name = "lblAlphanumericTag";
			this.lblAlphanumericTag.Size = new System.Drawing.Size(100, 13);
			this.lblAlphanumericTag.TabIndex = 0;
			this.lblAlphanumericTag.Text = "AlphanumericTag:";
			
			// 
			// txtFirstName
			// 
			this.txtAlphanumericTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                 | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAlphanumericTag.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.IndexMarkerBindingSource, "_AlphanumericTag", true));
			this.txtAlphanumericTag.Location = new System.Drawing.Point(110, 6);
			this.txtAlphanumericTag.Name = "txtAlphanumericTag";
			this.txtAlphanumericTag.Size = new System.Drawing.Size(100, 20);
			this.txtAlphanumericTag.TabIndex = 1;
			
			// 
			// IndexMarkerBindingSource
			// 
            this.IndexMarkerBindingSource.DataSource = typeof(DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.IndexMarker>);
			// 
			// IndexMarkerSimpleCRUDFormView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

			this.Controls.Add(this.lblAlphanumericTag);
			this.Controls.Add(this.txtAlphanumericTag);
	
			this.Name = "IndexMarkerSimpleCRUDFormView";

			
			
			this.Size = new System.Drawing.Size(215, 59);
			
			((System.ComponentModel.ISupportInitialize)(this.IndexMarkerBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion


            System.Windows.Forms.Label lblAlphanumericTag;
			
			// Unknown typ DateTime
            //System.Windows.Forms.Label lblDateCreated;
			
            private DevExpress.XtraEditors.TextEdit txtAlphanumericTag;
			
			// Unknown typ DateTime
            //private DevExpress.XtraEditors.TextEdit txtDateCreated;
			
		
		private System.Windows.Forms.BindingSource IndexMarkerBindingSource;
	}
}
