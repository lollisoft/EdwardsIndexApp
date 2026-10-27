
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

//using Annotation=EdwardsIndexAppCommon.Annotation;

namespace EdwardsIndexAppModule.WorkItems.Annotation
{
	partial class AnnotationPerFormGridView
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
			//this.AnnotationBindingSource = new System.Windows.Forms.BindingSource(this.components);

			//this.IndexMarkerBindingSource = new System.Windows.Forms.BindingSource(this.components);
	
			//this.ThemeBindingSource = new System.Windows.Forms.BindingSource(this.components);
	
			this.gridAnnotation = new DevExpress.XtraGrid.GridControl();
			this.gridAnnotationView = new DevExpress.XtraGrid.Views.Grid.GridView();
			

            this.gridColumnText = new DevExpress.XtraGrid.Columns.GridColumn();
			
            this.gridColumnDateCreated = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riDateCreated = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
				
	
            //this.repositotyItemLookupEditIndexMarker = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            //this.gridColumnIndexMarker = new DevExpress.XtraGrid.Columns.GridColumn();



            this.repositoryItemSearchLookUpEditIndexMarker = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEditIndexMarkerView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnIndexMarker = new DevExpress.XtraGrid.Columns.GridColumn();


            this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag = new DevExpress.XtraGrid.Columns.GridColumn();
			
            this.gridColumnLookupIndexMarkerIndexMarkerDateCreated = new DevExpress.XtraGrid.Columns.GridColumn();
				
	
            //this.repositotyItemLookupEditTheme = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            //this.gridColumnTheme = new DevExpress.XtraGrid.Columns.GridColumn();



            this.repositoryItemSearchLookUpEditTheme = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEditThemeView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnTheme = new DevExpress.XtraGrid.Columns.GridColumn();


            this.gridColumnLookupThemeThemeName = new DevExpress.XtraGrid.Columns.GridColumn();
			
            this.gridColumnLookupThemeThemeCategory = new DevExpress.XtraGrid.Columns.GridColumn();
			

		// Relation definitions

            ((System.ComponentModel.ISupportInitialize)(this.riDateCreated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateCreated.CalendarTimeProperties)).BeginInit();
				
	
			//((System.ComponentModel.ISupportInitialize)(this.repositotyItemLookupEditIndexMarker)).BeginInit();


			
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditIndexMarker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditIndexMarkerView)).BeginInit();


            // Cast Exception
			//((System.ComponentModel.ISupportInitialize)(this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag)).BeginInit();
		
            // Cast Exception
			//((System.ComponentModel.ISupportInitialize)(this.gridColumnLookupIndexMarkerIndexMarkerDateCreated)).BeginInit();
			
	
			//((System.ComponentModel.ISupportInitialize)(this.repositotyItemLookupEditTheme)).BeginInit();


			
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditThemeView)).BeginInit();


            // Cast Exception
			//((System.ComponentModel.ISupportInitialize)(this.gridColumnLookupThemeThemeName)).BeginInit();
		
            // Cast Exception
			//((System.ComponentModel.ISupportInitialize)(this.gridColumnLookupThemeThemeCategory)).BeginInit();
		







		// Begin Init for relation definitions








			//((System.ComponentModel.ISupportInitialize)(this.AnnotationBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAnnotation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAnnotationView)).BeginInit();
			this.SuspendLayout();
			// 
			// AnnotationBindingSource
			// 


			//this.AnnotationBindingSource.DataSource = typeof(EdwardsIndexAppCommon.Annotation);
			// 
			// gridAnnotation
			// 
			
		// Relation definitions

			
			
			
			
			this.gridAnnotation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridAnnotation.EmbeddedNavigator.Name = "";
			this.gridAnnotation.Location = new System.Drawing.Point(0, 0);
			this.gridAnnotation.MainView = this.gridAnnotationView;
			this.gridAnnotation.Name = "gridAnnotation";
			this.gridAnnotation.Size = new System.Drawing.Size(445, 271);
			this.gridAnnotation.TabIndex = 2;
			this.gridAnnotation.ShowOnlyPredefinedDetails = true;
            this.gridAnnotation.UseEmbeddedNavigator = true;

			this.gridAnnotation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
			                                                                                        	this.gridAnnotationView});

																										

            this.gridAnnotation.RepositoryItems.Add(this.riDateCreated);
				
	
            //this.gridAnnotation.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            //this.repositotyItemLookupEditIndexMarker});


            this.gridAnnotation.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSearchLookUpEditIndexMarker
			});

						
			
	
            //this.gridAnnotation.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            //this.repositotyItemLookupEditTheme});


            this.gridAnnotation.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSearchLookUpEditTheme
			});

						
		

		// Relation definitions

	
			// 
			// gridAnnotationView
			// 

this.gridAnnotationView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnText});
			
this.gridAnnotationView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnDateCreated});
			
this.gridAnnotationView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnIndexMarker});
	
this.gridAnnotationView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnTheme});
	

		// Relation definitions

            // 
            // gridColumnText
            // 
            this.gridColumnText.Caption = "Text";
            this.gridColumnText.Name = "gridColumnText";
            this.gridColumnText.FieldName = "_Text";
            this.gridColumnText.Visible = true;
            //this.gridColumnText.VisibleIndex = 0;
			
            // 
            // gridColumnDateCreated
            // 
            this.gridColumnDateCreated.Caption = "DateCreated";
            this.gridColumnDateCreated.Name = "gridColumnDateCreated";
            this.gridColumnDateCreated.FieldName = "_DateCreated";
            this.gridColumnDateCreated.Visible = true;
            //this.gridColumnDateCreated.VisibleIndex = 1;
            this.gridColumnDateCreated.ColumnEdit = this.riDateCreated;
            this.gridColumnDateCreated.DisplayFormat.FormatString = "G";
            this.gridColumnDateCreated.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;

			// 
            // riDateCreated
            // 
            this.riDateCreated.AutoHeight = false;
            this.riDateCreated.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riDateCreated.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.riDateCreated.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riDateCreated.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.riDateCreated.Name = "riDateCreated";
            this.riDateCreated.Mask.EditMask = "G";

			
			

            // 
            // gridColumnIndexMarker
            // 
            this.gridColumnIndexMarker.Caption = "IndexMarker";
			
	

            //this.gridColumnIndexMarker.ColumnEdit = this.repositotyItemLookupEditIndexMarker;



            this.gridColumnIndexMarker.ColumnEdit = this.repositoryItemSearchLookUpEditIndexMarker;

            this.gridColumnIndexMarker.Name = "gridColumnIndexMarker";

            this.gridColumnIndexMarker.Visible = true;
            //this.gridColumnIndexMarker.VisibleIndex = 2;

	
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
			

            // 
            // gridColumnTheme
            // 
            this.gridColumnTheme.Caption = "Theme";
			
	

            //this.gridColumnTheme.ColumnEdit = this.repositotyItemLookupEditTheme;



            this.gridColumnTheme.ColumnEdit = this.repositoryItemSearchLookUpEditTheme;

            this.gridColumnTheme.Name = "gridColumnTheme";

            this.gridColumnTheme.Visible = true;
            //this.gridColumnTheme.VisibleIndex = 3;

	
/*
            // 
            // repositotyItemLookupEditTheme
            // 
            this.repositotyItemLookupEditTheme.AutoHeight = false;
            this.repositotyItemLookupEditTheme.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositotyItemLookupEditTheme.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("_ID")});
            //this.repositotyItemLookupEditTheme.DataSource = this.ThemeBindingSource;
            this.repositotyItemLookupEditTheme.DisplayMember = "_ID";
            this.repositotyItemLookupEditTheme.Name = "repositotyItemLookupEditTheme";
            this.repositotyItemLookupEditTheme.ValueMember = "This";
*/


            // 
            // repositoryItemSearchLookUpEditTheme
            // 
            this.repositoryItemSearchLookUpEditTheme.AutoHeight = false;
            this.repositoryItemSearchLookUpEditTheme.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEditTheme.Name = "repositoryItemSearchLookUpEditTheme";
            this.repositoryItemSearchLookUpEditTheme.View = this.repositoryItemSearchLookUpEditThemeView;
            this.repositoryItemSearchLookUpEditTheme.DisplayMember = "_ID";
            this.repositoryItemSearchLookUpEditTheme.ValueMember = "This";
            this.repositoryItemSearchLookUpEditTheme.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;

            // 
            // repositoryItemSearchLookUpEditThemeView
            // 
            this.repositoryItemSearchLookUpEditThemeView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEditThemeView.Name = "repositoryItemSearchLookUpEditThemeView";
            this.repositoryItemSearchLookUpEditThemeView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEditThemeView.OptionsView.ShowGroupPanel = false;
			

            this.repositoryItemSearchLookUpEditThemeView.Columns.Add(gridColumnLookupThemeThemeName);
			
            // 
            // gridColumnLookupThemeThemeName
            // 
            this.gridColumnLookupThemeThemeName.Caption = "Name";
            this.gridColumnLookupThemeThemeName.Name = "gridColumnLookupThemeThemeName";
            this.gridColumnLookupThemeThemeName.FieldName = "_Name";
            this.gridColumnLookupThemeThemeName.Visible = true;
            //this.gridColumnLookupThemeThemeName.VisibleIndex = 0;
			
            this.repositoryItemSearchLookUpEditThemeView.Columns.Add(gridColumnLookupThemeThemeCategory);
			
            // 
            // gridColumnLookupThemeThemeCategory
            // 
            this.gridColumnLookupThemeThemeCategory.Caption = "Category";
            this.gridColumnLookupThemeThemeCategory.Name = "gridColumnLookupThemeThemeCategory";
            this.gridColumnLookupThemeThemeCategory.FieldName = "_Category";
            this.gridColumnLookupThemeThemeCategory.Visible = true;
            //this.gridColumnLookupThemeThemeCategory.VisibleIndex = 1;
			

		// Relation settings for detail grid columns

	

			
			this.gridAnnotationView.GridControl = this.gridAnnotation;
			this.gridAnnotationView.Name = "gridAnnotationView";
			this.gridAnnotationView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridAnnotationView.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridAnnotationView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridAnnotationView.OptionsView.ShowAutoFilterRow = true;
            this.gridAnnotationView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridAnnotationView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridAnnotationView_KeyUp);

			// 
			// AnnotationPerFormGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridAnnotation);
			this.Name = "AnnotationPerFormGridView";
			this.Size = new System.Drawing.Size(445, 271);
			this.Load += new System.EventHandler(this.AnnotationsView_Load);

			
            ((System.ComponentModel.ISupportInitialize)(this.riDateCreated.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateCreated)).EndInit();
				

			//((System.ComponentModel.ISupportInitialize)(this.repositotyItemLookupEditIndexMarker)).EndInit();



            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditIndexMarker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditIndexMarkerView)).EndInit();


            // Cast Exception
			//((System.ComponentModel.ISupportInitialize)(this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag)).EndInit();
		
            // Cast Exception
			//((System.ComponentModel.ISupportInitialize)(this.gridColumnLookupIndexMarkerIndexMarkerDateCreated)).EndInit();
		

			//((System.ComponentModel.ISupportInitialize)(this.IndexMarkerBindingSource)).EndInit();
		

			//((System.ComponentModel.ISupportInitialize)(this.repositotyItemLookupEditTheme)).EndInit();



            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditThemeView)).EndInit();


            // Cast Exception
			//((System.ComponentModel.ISupportInitialize)(this.gridColumnLookupThemeThemeName)).EndInit();
		
            // Cast Exception
			//((System.ComponentModel.ISupportInitialize)(this.gridColumnLookupThemeThemeCategory)).EndInit();
		

			//((System.ComponentModel.ISupportInitialize)(this.ThemeBindingSource)).EndInit();
	






		// End Init for relation definitions





			//((System.ComponentModel.ISupportInitialize)(this.AnnotationBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAnnotation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAnnotationView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		//private System.Windows.Forms.BindingSource AnnotationBindingSource;
		private DevExpress.XtraGrid.GridControl gridAnnotation;
		private DevExpress.XtraGrid.Views.Grid.GridView gridAnnotationView;

		// Relation definitions for detail grid and columns

	
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnText;
			
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDateCreated;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riDateCreated;
			
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIndexMarker;
		
	

		//private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositotyItemLookupEditIndexMarker;



        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditIndexMarker;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEditIndexMarkerView;



		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag;
		
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLookupIndexMarkerIndexMarkerDateCreated;
		
		
		//private System.Windows.Forms.BindingSource IndexMarkerBindingSource;
	
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTheme;
		
	

		//private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositotyItemLookupEditTheme;



        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditTheme;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEditThemeView;



		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLookupThemeThemeName;
		
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLookupThemeThemeCategory;
		
		
		//private System.Windows.Forms.BindingSource ThemeBindingSource;
	
	}
}
