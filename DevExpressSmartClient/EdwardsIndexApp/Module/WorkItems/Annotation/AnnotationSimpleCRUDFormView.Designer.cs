
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
	partial class AnnotationSimpleCRUDFormView
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
			

			this.lblText = new System.Windows.Forms.Label();
			
			this.txtText = new DevExpress.XtraEditors.TextEdit();
	

			this.AnnotationBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.AnnotationBindingSource)).BeginInit();
			this.SuspendLayout();


			// 
			// lblText
			// 
			this.lblText.AutoSize = true;
			this.lblText.Location = new System.Drawing.Point(5, 6);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(100, 13);
			this.lblText.TabIndex = 0;
			this.lblText.Text = "Text:";
			
			// 
			// txtFirstName
			// 
			this.txtText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                 | System.Windows.Forms.AnchorStyles.Right)));
			this.txtText.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.AnnotationBindingSource, "_Text", true));
			this.txtText.Location = new System.Drawing.Point(110, 6);
			this.txtText.Name = "txtText";
			this.txtText.Size = new System.Drawing.Size(100, 20);
			this.txtText.TabIndex = 1;
			
			// 
			// AnnotationBindingSource
			// 
            this.AnnotationBindingSource.DataSource = typeof(DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.Annotation>);
			// 
			// AnnotationSimpleCRUDFormView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

			this.Controls.Add(this.lblText);
			this.Controls.Add(this.txtText);
	
			this.Name = "AnnotationSimpleCRUDFormView";

			
			
			this.Size = new System.Drawing.Size(215, 111);
			
			((System.ComponentModel.ISupportInitialize)(this.AnnotationBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion


            System.Windows.Forms.Label lblText;
			
			// Unknown typ DateTime
            //System.Windows.Forms.Label lblDateCreated;
			
            private DevExpress.XtraEditors.TextEdit txtText;
			
			// Unknown typ DateTime
            //private DevExpress.XtraEditors.TextEdit txtDateCreated;
			
            System.Windows.Forms.Label IndexMarkerLabel = new System.Windows.Forms.Label();
            private DevExpress.XtraEditors.TextEdit txtIndexMarker;
    
            System.Windows.Forms.Label ThemeLabel = new System.Windows.Forms.Label();
            private DevExpress.XtraEditors.TextEdit txtTheme;
    
		
		private System.Windows.Forms.BindingSource AnnotationBindingSource;
	}
}
