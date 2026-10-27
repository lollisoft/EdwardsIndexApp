
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

//using Theme=EdwardsIndexAppCommon.Theme;

namespace EdwardsIndexAppModule.WorkItems.Theme
{
	partial class ThemePerFormGridView
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
			//this.ThemeBindingSource = new System.Windows.Forms.BindingSource(this.components);

			this.gridTheme = new DevExpress.XtraGrid.GridControl();
			this.gridThemeView = new DevExpress.XtraGrid.Views.Grid.GridView();
			

            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
			
            this.gridColumnCategory = new DevExpress.XtraGrid.Columns.GridColumn();
			

		// Relation definitions








		// Begin Init for relation definitions








			//((System.ComponentModel.ISupportInitialize)(this.ThemeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridTheme)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridThemeView)).BeginInit();
			this.SuspendLayout();
			// 
			// ThemeBindingSource
			// 


			//this.ThemeBindingSource.DataSource = typeof(EdwardsIndexAppCommon.Theme);
			// 
			// gridTheme
			// 
			
		// Relation definitions

			
			
			
			
			this.gridTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridTheme.EmbeddedNavigator.Name = "";
			this.gridTheme.Location = new System.Drawing.Point(0, 0);
			this.gridTheme.MainView = this.gridThemeView;
			this.gridTheme.Name = "gridTheme";
			this.gridTheme.Size = new System.Drawing.Size(445, 271);
			this.gridTheme.TabIndex = 2;
			this.gridTheme.ShowOnlyPredefinedDetails = true;
            this.gridTheme.UseEmbeddedNavigator = true;

			this.gridTheme.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
			                                                                                        	this.gridThemeView});

																										


		// Relation definitions

	
			// 
			// gridThemeView
			// 

this.gridThemeView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnName});
			
this.gridThemeView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.gridColumnCategory});
			

		// Relation definitions

            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.FieldName = "_Name";
            this.gridColumnName.Visible = true;
            //this.gridColumnName.VisibleIndex = 0;
			
            // 
            // gridColumnCategory
            // 
            this.gridColumnCategory.Caption = "Category";
            this.gridColumnCategory.Name = "gridColumnCategory";
            this.gridColumnCategory.FieldName = "_Category";
            this.gridColumnCategory.Visible = true;
            //this.gridColumnCategory.VisibleIndex = 1;
			

		// Relation settings for detail grid columns

	

			
			this.gridThemeView.GridControl = this.gridTheme;
			this.gridThemeView.Name = "gridThemeView";
			this.gridThemeView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridThemeView.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridThemeView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridThemeView.OptionsView.ShowAutoFilterRow = true;
            this.gridThemeView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridThemeView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridThemeView_KeyUp);

			// 
			// ThemePerFormGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridTheme);
			this.Name = "ThemePerFormGridView";
			this.Size = new System.Drawing.Size(445, 271);
			this.Load += new System.EventHandler(this.ThemesView_Load);

			






		// End Init for relation definitions





			//((System.ComponentModel.ISupportInitialize)(this.ThemeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridTheme)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridThemeView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		//private System.Windows.Forms.BindingSource ThemeBindingSource;
		private DevExpress.XtraGrid.GridControl gridTheme;
		private DevExpress.XtraGrid.Views.Grid.GridView gridThemeView;

		// Relation definitions for detail grid and columns

	
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
			
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCategory;
			
	}
}
