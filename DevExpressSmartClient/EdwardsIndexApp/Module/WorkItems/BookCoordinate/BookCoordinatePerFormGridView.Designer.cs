
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

//using BookCoordinate=EdwardsIndexAppCommon.BookCoordinate;

namespace EdwardsIndexAppModule.WorkItems.BookCoordinate
{
	partial class BookCoordinatePerFormGridView
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
			//this.BookCoordinateBindingSource = new System.Windows.Forms.BindingSource(this.components);

			//this.IndexMarkerBindingSource = new System.Windows.Forms.BindingSource(this.components);
	
			this.gridBookCoordinate = new DevExpress.XtraGrid.GridControl();
			this.gridBookCoordinateView = new DevExpress.XtraGrid.Views.Grid.GridView();
			

            this.gridColumnBookName = new DevExpress.XtraGrid.Columns.GridColumn();
			
            this.gridColumnPage = new DevExpress.XtraGrid.Columns.GridColumn();
			
            this.gridColumnSubject = new DevExpress.XtraGrid.Columns.GridColumn();
			
            this.gridColumnChapter = new DevExpress.XtraGrid.Columns.GridColumn();
			
            this.gridColumnVerse = new DevExpress.XtraGrid.Columns.GridColumn();
				
	
            //this.repositotyItemLookupEditIndexMarker = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            //this.gridColumnIndexMarker = new DevExpress.XtraGrid.Columns.GridColumn();



            this.repositoryItemSearchLookUpEditIndexMarker = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEditIndexMarkerView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnIndexMarker = new DevExpress.XtraGrid.Columns.GridColumn();


            this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag = new DevExpress.XtraGrid.Columns.GridColumn();
			
            this.gridColumnLookupIndexMarkerIndexMarkerDateCreated = new DevExpress.XtraGrid.Columns.GridColumn();
			

		// Relation definitions
	
	
			//((System.ComponentModel.ISupportInitialize)(this.repositotyItemLookupEditIndexMarker)).BeginInit();


			
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditIndexMarker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditIndexMarkerView)).BeginInit();


            // Cast Exception
			//((System.ComponentModel.ISupportInitialize)(this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag)).BeginInit();
		
            // Cast Exception
			//((System.ComponentModel.ISupportInitialize)(this.gridColumnLookupIndexMarkerIndexMarkerDateCreated)).BeginInit();
		







		// Begin Init for relation definitions








			//((System.ComponentModel.ISupportInitialize)(this.BookCoordinateBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridBookCoordinate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridBookCoordinateView)).BeginInit();
			this.SuspendLayout();
			// 
			// BookCoordinateBindingSource
			// 


			//this.BookCoordinateBindingSource.DataSource = typeof(EdwardsIndexAppCommon.BookCoordinate);
			// 
			// gridBookCoordinate
			// 
			
		// Relation definitions

			
			
			
			
			this.gridBookCoordinate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridBookCoordinate.EmbeddedNavigator.Name = "";
			this.gridBookCoordinate.Location = new System.Drawing.Point(0, 0);
			this.gridBookCoordinate.MainView = this.gridBookCoordinateView;
			this.gridBookCoordinate.Name = "gridBookCoordinate";
			this.gridBookCoordinate.Size = new System.Drawing.Size(445, 271);
			this.gridBookCoordinate.TabIndex = 2;
			this.gridBookCoordinate.ShowOnlyPredefinedDetails = true;
            this.gridBookCoordinate.UseEmbeddedNavigator = true;

			this.gridBookCoordinate.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
			                                                                                        	this.gridBookCoordinateView});

																										
	
	
            //this.gridBookCoordinate.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            //this.repositotyItemLookupEditIndexMarker});


            this.gridBookCoordinate.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSearchLookUpEditIndexMarker
			});

						
		

		// Relation definitions

	
			// 
			// gridBookCoordinateView
			// 

this.gridBookCoordinateView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnBookName});
			
this.gridBookCoordinateView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnPage});
			
this.gridBookCoordinateView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnSubject});
			
this.gridBookCoordinateView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnChapter});
			
this.gridBookCoordinateView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnVerse});
			
this.gridBookCoordinateView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnIndexMarker});
	

		// Relation definitions

            // 
            // gridColumnBookName
            // 
            this.gridColumnBookName.Caption = "BookName";
            this.gridColumnBookName.Name = "gridColumnBookName";
            this.gridColumnBookName.FieldName = "_BookName";
            this.gridColumnBookName.Visible = true;
            //this.gridColumnBookName.VisibleIndex = 0;
			
            // 
            // gridColumnPage
            // 
            this.gridColumnPage.Caption = "Page";
            this.gridColumnPage.Name = "gridColumnPage";
            this.gridColumnPage.FieldName = "_Page";
            this.gridColumnPage.Visible = true;
            //this.gridColumnPage.VisibleIndex = 1;
			
            // 
            // gridColumnSubject
            // 
            this.gridColumnSubject.Caption = "Subject";
            this.gridColumnSubject.Name = "gridColumnSubject";
            this.gridColumnSubject.FieldName = "_Subject";
            this.gridColumnSubject.Visible = true;
            //this.gridColumnSubject.VisibleIndex = 2;
			
            // 
            // gridColumnChapter
            // 
            this.gridColumnChapter.Caption = "Chapter";
            this.gridColumnChapter.Name = "gridColumnChapter";
            this.gridColumnChapter.FieldName = "_Chapter";
            this.gridColumnChapter.Visible = true;
            //this.gridColumnChapter.VisibleIndex = 3;
			
            // 
            // gridColumnVerse
            // 
            this.gridColumnVerse.Caption = "Verse";
            this.gridColumnVerse.Name = "gridColumnVerse";
            this.gridColumnVerse.FieldName = "_Verse";
            this.gridColumnVerse.Visible = true;
            //this.gridColumnVerse.VisibleIndex = 4;
			

            // 
            // gridColumnIndexMarker
            // 
            this.gridColumnIndexMarker.Caption = "IndexMarker";
			
	

            //this.gridColumnIndexMarker.ColumnEdit = this.repositotyItemLookupEditIndexMarker;



            this.gridColumnIndexMarker.ColumnEdit = this.repositoryItemSearchLookUpEditIndexMarker;

            this.gridColumnIndexMarker.Name = "gridColumnIndexMarker";

            this.gridColumnIndexMarker.Visible = true;
            //this.gridColumnIndexMarker.VisibleIndex = 5;

	
/*
            // 
            // repositotyItemLookupEditIndexMarker
            // 
            this.repositotyItemLookupEditIndexMarker.AutoHeight = false;
            this.repositotyItemLookupEditIndexMarker.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositotyItemLookupEditIndexMarker.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("_ID")});
            //this.repositotyItemLookupEditIndexMarker.DataSource = this.IndexMarkerBindingSource;
            this.repositotyItemLookupEditIndexMarker.DisplayMember = "_ID";
            this.repositotyItemLookupEditIndexMarker.Name = "repositotyItemLookupEditIndexMarker";
            this.repositotyItemLookupEditIndexMarker.ValueMember = "This";
*/


            // 
            // repositoryItemSearchLookUpEditIndexMarker
            // 
            this.repositoryItemSearchLookUpEditIndexMarker.AutoHeight = false;
            this.repositoryItemSearchLookUpEditIndexMarker.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEditIndexMarker.Name = "repositoryItemSearchLookUpEditIndexMarker";
            this.repositoryItemSearchLookUpEditIndexMarker.View = this.repositoryItemSearchLookUpEditIndexMarkerView;
            this.repositoryItemSearchLookUpEditIndexMarker.DisplayMember = "_ID";
            this.repositoryItemSearchLookUpEditIndexMarker.ValueMember = "This";
            this.repositoryItemSearchLookUpEditIndexMarker.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;

            // 
            // repositoryItemSearchLookUpEditIndexMarkerView
            // 
            this.repositoryItemSearchLookUpEditIndexMarkerView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEditIndexMarkerView.Name = "repositoryItemSearchLookUpEditIndexMarkerView";
            this.repositoryItemSearchLookUpEditIndexMarkerView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEditIndexMarkerView.OptionsView.ShowGroupPanel = false;
			

            this.repositoryItemSearchLookUpEditIndexMarkerView.Columns.Add(gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag);
			
            // 
            // gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag
            // 
            this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag.Caption = "AlphanumericTag";
            this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag.Name = "gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag";
            this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag.FieldName = "_AlphanumericTag";
            this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag.Visible = true;
            //this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag.VisibleIndex = 0;
			
            this.repositoryItemSearchLookUpEditIndexMarkerView.Columns.Add(gridColumnLookupIndexMarkerIndexMarkerDateCreated);
			
            // 
            // gridColumnLookupIndexMarkerIndexMarkerDateCreated
            // 
            this.gridColumnLookupIndexMarkerIndexMarkerDateCreated.Caption = "DateCreated";
            this.gridColumnLookupIndexMarkerIndexMarkerDateCreated.Name = "gridColumnLookupIndexMarkerIndexMarkerDateCreated";
            this.gridColumnLookupIndexMarkerIndexMarkerDateCreated.FieldName = "_DateCreated";
            this.gridColumnLookupIndexMarkerIndexMarkerDateCreated.Visible = true;
            //this.gridColumnLookupIndexMarkerIndexMarkerDateCreated.VisibleIndex = 1;
			

		// Relation settings for detail grid columns

	

			
			this.gridBookCoordinateView.GridControl = this.gridBookCoordinate;
			this.gridBookCoordinateView.Name = "gridBookCoordinateView";
			this.gridBookCoordinateView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridBookCoordinateView.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridBookCoordinateView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridBookCoordinateView.OptionsView.ShowAutoFilterRow = true;
            this.gridBookCoordinateView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridBookCoordinateView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridBookCoordinateView_KeyUp);

			// 
			// BookCoordinatePerFormGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridBookCoordinate);
			this.Name = "BookCoordinatePerFormGridView";
			this.Size = new System.Drawing.Size(445, 271);
			this.Load += new System.EventHandler(this.BookCoordinatesView_Load);

				

			//((System.ComponentModel.ISupportInitialize)(this.repositotyItemLookupEditIndexMarker)).EndInit();



            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditIndexMarker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditIndexMarkerView)).EndInit();


            // Cast Exception
			//((System.ComponentModel.ISupportInitialize)(this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag)).EndInit();
		
            // Cast Exception
			//((System.ComponentModel.ISupportInitialize)(this.gridColumnLookupIndexMarkerIndexMarkerDateCreated)).EndInit();
		

			//((System.ComponentModel.ISupportInitialize)(this.IndexMarkerBindingSource)).EndInit();
	






		// End Init for relation definitions





			//((System.ComponentModel.ISupportInitialize)(this.BookCoordinateBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridBookCoordinate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridBookCoordinateView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		//private System.Windows.Forms.BindingSource BookCoordinateBindingSource;
		private DevExpress.XtraGrid.GridControl gridBookCoordinate;
		private DevExpress.XtraGrid.Views.Grid.GridView gridBookCoordinateView;

		// Relation definitions for detail grid and columns

	
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnBookName;
			
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPage;
			
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSubject;
			
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnChapter;
			
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVerse;
			
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIndexMarker;
		
	

		//private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositotyItemLookupEditIndexMarker;



        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditIndexMarker;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEditIndexMarkerView;



		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag;
		
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLookupIndexMarkerIndexMarkerDateCreated;
		
		
		//private System.Windows.Forms.BindingSource IndexMarkerBindingSource;
	
	}
}
