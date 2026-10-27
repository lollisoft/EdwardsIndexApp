
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
	partial class BookCoordinateSimpleCRUDFormView
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
			

			this.lblBookName = new System.Windows.Forms.Label();
			
			this.lblPage = new System.Windows.Forms.Label();
			
			this.lblSubject = new System.Windows.Forms.Label();
			
			this.lblChapter = new System.Windows.Forms.Label();
			
			this.lblVerse = new System.Windows.Forms.Label();
			
			this.txtBookName = new DevExpress.XtraEditors.TextEdit();
	
			this.txtPage = new DevExpress.XtraEditors.TextEdit();
	
			this.txtSubject = new DevExpress.XtraEditors.TextEdit();
	
			this.txtChapter = new DevExpress.XtraEditors.TextEdit();
	
			this.txtVerse = new DevExpress.XtraEditors.TextEdit();
	

			this.BookCoordinateBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.BookCoordinateBindingSource)).BeginInit();
			this.SuspendLayout();


			// 
			// lblBookName
			// 
			this.lblBookName.AutoSize = true;
			this.lblBookName.Location = new System.Drawing.Point(5, 6);
			this.lblBookName.Name = "lblBookName";
			this.lblBookName.Size = new System.Drawing.Size(100, 13);
			this.lblBookName.TabIndex = 0;
			this.lblBookName.Text = "BookName:";
			
			// 
			// lblPage
			// 
			this.lblPage.AutoSize = true;
			this.lblPage.Location = new System.Drawing.Point(5, 32);
			this.lblPage.Name = "lblPage";
			this.lblPage.Size = new System.Drawing.Size(100, 13);
			this.lblPage.TabIndex = 0;
			this.lblPage.Text = "Page:";
			
			// 
			// lblSubject
			// 
			this.lblSubject.AutoSize = true;
			this.lblSubject.Location = new System.Drawing.Point(5, 58);
			this.lblSubject.Name = "lblSubject";
			this.lblSubject.Size = new System.Drawing.Size(100, 13);
			this.lblSubject.TabIndex = 0;
			this.lblSubject.Text = "Subject:";
			
			// 
			// lblChapter
			// 
			this.lblChapter.AutoSize = true;
			this.lblChapter.Location = new System.Drawing.Point(5, 84);
			this.lblChapter.Name = "lblChapter";
			this.lblChapter.Size = new System.Drawing.Size(100, 13);
			this.lblChapter.TabIndex = 0;
			this.lblChapter.Text = "Chapter:";
			
			// 
			// lblVerse
			// 
			this.lblVerse.AutoSize = true;
			this.lblVerse.Location = new System.Drawing.Point(5, 110);
			this.lblVerse.Name = "lblVerse";
			this.lblVerse.Size = new System.Drawing.Size(100, 13);
			this.lblVerse.TabIndex = 0;
			this.lblVerse.Text = "Verse:";
			
			// 
			// txtFirstName
			// 
			this.txtBookName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                 | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBookName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.BookCoordinateBindingSource, "_BookName", true));
			this.txtBookName.Location = new System.Drawing.Point(110, 6);
			this.txtBookName.Name = "txtBookName";
			this.txtBookName.Size = new System.Drawing.Size(100, 20);
			this.txtBookName.TabIndex = 1;
			
			// 
			// txtFirstName
			// 
			this.txtPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                 | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPage.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.BookCoordinateBindingSource, "_Page", true));
			this.txtPage.Location = new System.Drawing.Point(110, 32);
			this.txtPage.Name = "txtPage";
			this.txtPage.Size = new System.Drawing.Size(100, 20);
			this.txtPage.TabIndex = 2;
			
			// 
			// txtFirstName
			// 
			this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                 | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSubject.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.BookCoordinateBindingSource, "_Subject", true));
			this.txtSubject.Location = new System.Drawing.Point(110, 58);
			this.txtSubject.Name = "txtSubject";
			this.txtSubject.Size = new System.Drawing.Size(100, 20);
			this.txtSubject.TabIndex = 3;
			
			// 
			// txtFirstName
			// 
			this.txtChapter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                 | System.Windows.Forms.AnchorStyles.Right)));
			this.txtChapter.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.BookCoordinateBindingSource, "_Chapter", true));
			this.txtChapter.Location = new System.Drawing.Point(110, 84);
			this.txtChapter.Name = "txtChapter";
			this.txtChapter.Size = new System.Drawing.Size(100, 20);
			this.txtChapter.TabIndex = 4;
			
			// 
			// txtFirstName
			// 
			this.txtVerse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                 | System.Windows.Forms.AnchorStyles.Right)));
			this.txtVerse.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.BookCoordinateBindingSource, "_Verse", true));
			this.txtVerse.Location = new System.Drawing.Point(110, 110);
			this.txtVerse.Name = "txtVerse";
			this.txtVerse.Size = new System.Drawing.Size(100, 20);
			this.txtVerse.TabIndex = 5;
			
			// 
			// BookCoordinateBindingSource
			// 
            this.BookCoordinateBindingSource.DataSource = typeof(DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.BookCoordinate>);
			// 
			// BookCoordinateSimpleCRUDFormView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

			this.Controls.Add(this.lblBookName);
			this.Controls.Add(this.txtBookName);
	
			this.Controls.Add(this.lblPage);
			this.Controls.Add(this.txtPage);
	
			this.Controls.Add(this.lblSubject);
			this.Controls.Add(this.txtSubject);
	
			this.Controls.Add(this.lblChapter);
			this.Controls.Add(this.txtChapter);
	
			this.Controls.Add(this.lblVerse);
			this.Controls.Add(this.txtVerse);
	
			this.Name = "BookCoordinateSimpleCRUDFormView";

			
			
			this.Size = new System.Drawing.Size(215, 163);
			
			((System.ComponentModel.ISupportInitialize)(this.BookCoordinateBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion


            System.Windows.Forms.Label lblBookName;
			
            System.Windows.Forms.Label lblPage;
			
            System.Windows.Forms.Label lblSubject;
			
            System.Windows.Forms.Label lblChapter;
			
            System.Windows.Forms.Label lblVerse;
			
            private DevExpress.XtraEditors.TextEdit txtBookName;
			
            private DevExpress.XtraEditors.TextEdit txtPage;
			
            private DevExpress.XtraEditors.TextEdit txtSubject;
			
            private DevExpress.XtraEditors.TextEdit txtChapter;
			
            private DevExpress.XtraEditors.TextEdit txtVerse;
			
            System.Windows.Forms.Label IndexMarkerLabel = new System.Windows.Forms.Label();
            private DevExpress.XtraEditors.TextEdit txtIndexMarker;
    
		
		private System.Windows.Forms.BindingSource BookCoordinateBindingSource;
	}
}
