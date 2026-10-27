
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

//using IndexMarker=EdwardsIndexAppCommon.IndexMarker;

namespace EdwardsIndexAppModule.WorkItems.IndexMarker
{
	partial class IndexMarkerPerFormGridView
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
			//this.IndexMarkerBindingSource = new System.Windows.Forms.BindingSource(this.components);

			this.gridIndexMarker = new DevExpress.XtraGrid.GridControl();
			this.gridIndexMarkerView = new DevExpress.XtraGrid.Views.Grid.GridView();
			

            this.gridColumnAlphanumericTag = new DevExpress.XtraGrid.Columns.GridColumn();
			
            this.gridColumnDateCreated = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riDateCreated = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			

		// Relation definitions

            ((System.ComponentModel.ISupportInitialize)(this.riDateCreated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateCreated.CalendarTimeProperties)).BeginInit();
			







		// Begin Init for relation definitions








			//((System.ComponentModel.ISupportInitialize)(this.IndexMarkerBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridIndexMarker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridIndexMarkerView)).BeginInit();
			this.SuspendLayout();
			// 
			// IndexMarkerBindingSource
			// 


			//this.IndexMarkerBindingSource.DataSource = typeof(EdwardsIndexAppCommon.IndexMarker);
			// 
			// gridIndexMarker
			// 
			
		// Relation definitions

			
			
			
			
			this.gridIndexMarker.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridIndexMarker.EmbeddedNavigator.Name = "";
			this.gridIndexMarker.Location = new System.Drawing.Point(0, 0);
			this.gridIndexMarker.MainView = this.gridIndexMarkerView;
			this.gridIndexMarker.Name = "gridIndexMarker";
			this.gridIndexMarker.Size = new System.Drawing.Size(445, 271);
			this.gridIndexMarker.TabIndex = 2;
			this.gridIndexMarker.ShowOnlyPredefinedDetails = true;
            this.gridIndexMarker.UseEmbeddedNavigator = true;

			this.gridIndexMarker.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
			                                                                                        	this.gridIndexMarkerView});

																										

            this.gridIndexMarker.RepositoryItems.Add(this.riDateCreated);
			

		// Relation definitions

	
			// 
			// gridIndexMarkerView
			// 

this.gridIndexMarkerView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnAlphanumericTag});
			
this.gridIndexMarkerView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnDateCreated});
			

		// Relation definitions

            // 
            // gridColumnAlphanumericTag
            // 
            this.gridColumnAlphanumericTag.Caption = "AlphanumericTag";
            this.gridColumnAlphanumericTag.Name = "gridColumnAlphanumericTag";
            this.gridColumnAlphanumericTag.FieldName = "_AlphanumericTag";
            this.gridColumnAlphanumericTag.Visible = true;
            //this.gridColumnAlphanumericTag.VisibleIndex = 0;
			
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

			
			

		// Relation settings for detail grid columns

	

			
			this.gridIndexMarkerView.GridControl = this.gridIndexMarker;
			this.gridIndexMarkerView.Name = "gridIndexMarkerView";
			this.gridIndexMarkerView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridIndexMarkerView.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridIndexMarkerView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridIndexMarkerView.OptionsView.ShowAutoFilterRow = true;
            this.gridIndexMarkerView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridIndexMarkerView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridIndexMarkerView_KeyUp);

			// 
			// IndexMarkerPerFormGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridIndexMarker);
			this.Name = "IndexMarkerPerFormGridView";
			this.Size = new System.Drawing.Size(445, 271);
			this.Load += new System.EventHandler(this.IndexMarkersView_Load);

			
            ((System.ComponentModel.ISupportInitialize)(this.riDateCreated.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateCreated)).EndInit();
			






		// End Init for relation definitions





			//((System.ComponentModel.ISupportInitialize)(this.IndexMarkerBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridIndexMarker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridIndexMarkerView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		//private System.Windows.Forms.BindingSource IndexMarkerBindingSource;
		private DevExpress.XtraGrid.GridControl gridIndexMarker;
		private DevExpress.XtraGrid.Views.Grid.GridView gridIndexMarkerView;

		// Relation definitions for detail grid and columns

	
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnAlphanumericTag;
			
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDateCreated;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riDateCreated;
			
	}
}
