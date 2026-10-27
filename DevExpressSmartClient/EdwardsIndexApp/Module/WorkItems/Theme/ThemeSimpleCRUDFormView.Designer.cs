
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
	partial class ThemeSimpleCRUDFormView
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
			

			this.lblName = new System.Windows.Forms.Label();
			
			this.lblCategory = new System.Windows.Forms.Label();
			
			this.txtName = new DevExpress.XtraEditors.TextEdit();
	
			this.txtCategory = new DevExpress.XtraEditors.TextEdit();
	

			this.ThemeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.ThemeBindingSource)).BeginInit();
			this.SuspendLayout();


			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(5, 6);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(100, 13);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Name:";
			
			// 
			// lblCategory
			// 
			this.lblCategory.AutoSize = true;
			this.lblCategory.Location = new System.Drawing.Point(5, 32);
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.Size = new System.Drawing.Size(100, 13);
			this.lblCategory.TabIndex = 0;
			this.lblCategory.Text = "Category:";
			
			// 
			// txtFirstName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                 | System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ThemeBindingSource, "_Name", true));
			this.txtName.Location = new System.Drawing.Point(110, 6);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(100, 20);
			this.txtName.TabIndex = 1;
			
			// 
			// txtFirstName
			// 
			this.txtCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                 | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCategory.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ThemeBindingSource, "_Category", true));
			this.txtCategory.Location = new System.Drawing.Point(110, 32);
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.Size = new System.Drawing.Size(100, 20);
			this.txtCategory.TabIndex = 2;
			
			// 
			// ThemeBindingSource
			// 
            this.ThemeBindingSource.DataSource = typeof(DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.Theme>);
			// 
			// ThemeSimpleCRUDFormView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

			this.Controls.Add(this.lblName);
			this.Controls.Add(this.txtName);
	
			this.Controls.Add(this.lblCategory);
			this.Controls.Add(this.txtCategory);
	
			this.Name = "ThemeSimpleCRUDFormView";

			
			
			this.Size = new System.Drawing.Size(215, 59);
			
			((System.ComponentModel.ISupportInitialize)(this.ThemeBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion


            System.Windows.Forms.Label lblName;
			
            System.Windows.Forms.Label lblCategory;
			
            private DevExpress.XtraEditors.TextEdit txtName;
			
            private DevExpress.XtraEditors.TextEdit txtCategory;
			
		
		private System.Windows.Forms.BindingSource ThemeBindingSource;
	}
}
