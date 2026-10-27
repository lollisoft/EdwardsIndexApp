
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
	partial class BookCoordinateLayoutCRUDFormView
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
            this.BookCoordinateBindingSource = new System.Windows.Forms.BindingSource(this.components);

            this.IndexMarkerBookCoordinateBindingSource = new System.Windows.Forms.BindingSource(this.components);
	

            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiComments = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();

            this.layoutControlGroupFormularFields = new DevExpress.XtraLayout.LayoutControlGroup();

            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			
					
            this.textBookName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemBookName = new DevExpress.XtraLayout.LayoutControlItem();
	
            this.textPage = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemPage = new DevExpress.XtraLayout.LayoutControlItem();
	
            this.textSubject = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemSubject = new DevExpress.XtraLayout.LayoutControlItem();
	
            this.textChapter = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemChapter = new DevExpress.XtraLayout.LayoutControlItem();
	
            this.textVerse = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemVerse = new DevExpress.XtraLayout.LayoutControlItem();
	
            this.gridLookUpEditIndexMarker = new DevExpress.XtraEditors.GridLookUpEdit();
            this.layoutControlItemIndexMarker = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridLookUpEditViewIndexMarker = new DevExpress.XtraGrid.Views.Grid.GridView();
			

            this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag = new DevExpress.XtraGrid.Columns.GridColumn();
			
            this.gridColumnLookupIndexMarkerIndexMarkerDateCreated = new DevExpress.XtraGrid.Columns.GridColumn();
			

			
			
            ((System.ComponentModel.ISupportInitialize)(this.BookCoordinateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFormularFields)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();

		
            ((System.ComponentModel.ISupportInitialize)(this.textBookName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBookName)).BeginInit();
	
            ((System.ComponentModel.ISupportInitialize)(this.textPage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPage)).BeginInit();
	
            ((System.ComponentModel.ISupportInitialize)(this.textSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSubject)).BeginInit();
	
            ((System.ComponentModel.ISupportInitialize)(this.textChapter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemChapter)).BeginInit();
	
            ((System.ComponentModel.ISupportInitialize)(this.textVerse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemVerse)).BeginInit();
	
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEditIndexMarker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIndexMarker)).BeginInit();
	
            this.SuspendLayout();
            // 
            // BookCoordinateBindingSource
            // 

            this.BookCoordinateBindingSource.DataSource = typeof(DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.BookCoordinate>);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiComments});
            this.barManager1.MaxItemId = 1;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 1";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiComments)});
            this.bar1.Text = "Custom 1";
            // 
            // bbiComments
            // 
            this.bbiComments.Caption = "Comments";
            this.bbiComments.Id = 0;
            this.bbiComments.Name = "bbiComments";
            this.bbiComments.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnShowComments);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(383, 33);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 289);
            this.barDockControlBottom.Size = new System.Drawing.Size(383, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 33);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 256);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(383, 33);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 256);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
			
			
            this.layoutControl1.Location = new System.Drawing.Point(0, 32);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignMode.AlignInGroups;
            this.layoutControl1.OptionsView.AllowHotTrack = true;
            this.layoutControl1.OptionsView.HighlightFocusedItem = true;
            this.layoutControl1.Root = this.layoutControlGroupFormularFields;
            this.layoutControl1.Size = new System.Drawing.Size(383, 256);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            this.layoutControl1.HideCustomization += new System.EventHandler(this.layoutControl1_HideCustomization);

		
            // 
            // textBookName
            // 
            this.textBookName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.BookCoordinateBindingSource, "_BookName", true));
            this.textBookName.Location = new System.Drawing.Point(64, 70);
            this.textBookName.Name = "textBookName";
            this.textBookName.Size = new System.Drawing.Size(122, 70);
            this.textBookName.StyleController = this.layoutControl1;
            this.textBookName.TabIndex = 1;
            // 
            // layoutControlItemBookName
            // 
            this.layoutControlItemBookName.Control = this.textBookName;
            this.layoutControlItemBookName.CustomizationFormText = "BookName";
            this.layoutControlItemBookName.Location = new System.Drawing.Point(64, 70);
            this.layoutControlItemBookName.Name = "BookName";
            this.layoutControlItemBookName.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItemBookName.Size = new System.Drawing.Size(130, 30);
            this.layoutControlItemBookName.Text = "BookName:";
            this.layoutControlItemBookName.TextSize = new System.Drawing.Size(60, 13);
	
            // 
            // textPage
            // 
            this.textPage.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.BookCoordinateBindingSource, "_Page", true));
            this.textPage.Location = new System.Drawing.Point(64, 100);
            this.textPage.Name = "textPage";
            this.textPage.Size = new System.Drawing.Size(122, 100);
            this.textPage.StyleController = this.layoutControl1;
            this.textPage.TabIndex = 2;
            // 
            // layoutControlItemPage
            // 
            this.layoutControlItemPage.Control = this.textPage;
            this.layoutControlItemPage.CustomizationFormText = "Page";
            this.layoutControlItemPage.Location = new System.Drawing.Point(64, 100);
            this.layoutControlItemPage.Name = "Page";
            this.layoutControlItemPage.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItemPage.Size = new System.Drawing.Size(130, 30);
            this.layoutControlItemPage.Text = "Page:";
            this.layoutControlItemPage.TextSize = new System.Drawing.Size(60, 13);
	
            // 
            // textSubject
            // 
            this.textSubject.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.BookCoordinateBindingSource, "_Subject", true));
            this.textSubject.Location = new System.Drawing.Point(64, 130);
            this.textSubject.Name = "textSubject";
            this.textSubject.Size = new System.Drawing.Size(122, 130);
            this.textSubject.StyleController = this.layoutControl1;
            this.textSubject.TabIndex = 3;
            // 
            // layoutControlItemSubject
            // 
            this.layoutControlItemSubject.Control = this.textSubject;
            this.layoutControlItemSubject.CustomizationFormText = "Subject";
            this.layoutControlItemSubject.Location = new System.Drawing.Point(64, 130);
            this.layoutControlItemSubject.Name = "Subject";
            this.layoutControlItemSubject.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItemSubject.Size = new System.Drawing.Size(130, 30);
            this.layoutControlItemSubject.Text = "Subject:";
            this.layoutControlItemSubject.TextSize = new System.Drawing.Size(60, 13);
	
            // 
            // textChapter
            // 
            this.textChapter.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.BookCoordinateBindingSource, "_Chapter", true));
            this.textChapter.Location = new System.Drawing.Point(64, 160);
            this.textChapter.Name = "textChapter";
            this.textChapter.Size = new System.Drawing.Size(122, 160);
            this.textChapter.StyleController = this.layoutControl1;
            this.textChapter.TabIndex = 4;
            // 
            // layoutControlItemChapter
            // 
            this.layoutControlItemChapter.Control = this.textChapter;
            this.layoutControlItemChapter.CustomizationFormText = "Chapter";
            this.layoutControlItemChapter.Location = new System.Drawing.Point(64, 160);
            this.layoutControlItemChapter.Name = "Chapter";
            this.layoutControlItemChapter.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItemChapter.Size = new System.Drawing.Size(130, 30);
            this.layoutControlItemChapter.Text = "Chapter:";
            this.layoutControlItemChapter.TextSize = new System.Drawing.Size(60, 13);
	
            // 
            // textVerse
            // 
            this.textVerse.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.BookCoordinateBindingSource, "_Verse", true));
            this.textVerse.Location = new System.Drawing.Point(64, 190);
            this.textVerse.Name = "textVerse";
            this.textVerse.Size = new System.Drawing.Size(122, 190);
            this.textVerse.StyleController = this.layoutControl1;
            this.textVerse.TabIndex = 5;
            // 
            // layoutControlItemVerse
            // 
            this.layoutControlItemVerse.Control = this.textVerse;
            this.layoutControlItemVerse.CustomizationFormText = "Verse";
            this.layoutControlItemVerse.Location = new System.Drawing.Point(64, 190);
            this.layoutControlItemVerse.Name = "Verse";
            this.layoutControlItemVerse.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItemVerse.Size = new System.Drawing.Size(130, 30);
            this.layoutControlItemVerse.Text = "Verse:";
            this.layoutControlItemVerse.TextSize = new System.Drawing.Size(60, 13);
	
            // 
            // textIndexMarker
            // 
            // 
            // gridLookUpEditIndexMarker
            // 
            this.gridLookUpEditIndexMarker.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.BookCoordinateBindingSource, "IndexMarker!", true));
            this.gridLookUpEditIndexMarker.Location = new System.Drawing.Point(75, 220);
            this.gridLookUpEditIndexMarker.MenuManager = this.barManager1;
            this.gridLookUpEditIndexMarker.Name = "gridLookUpEditIndexMarker";
            this.gridLookUpEditIndexMarker.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridLookUpEditIndexMarker.Properties.DataSource = this.IndexMarkerBookCoordinateBindingSource;
            this.gridLookUpEditIndexMarker.Properties.DisplayMember = "_ID";
            this.gridLookUpEditIndexMarker.Properties.ValueMember = "This";
            this.gridLookUpEditIndexMarker.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.gridLookUpEditIndexMarker.Properties.View = this.gridLookUpEditViewIndexMarker;
            this.gridLookUpEditIndexMarker.Size = new System.Drawing.Size(302, 220);
            this.gridLookUpEditIndexMarker.StyleController = this.layoutControl1;
            this.gridLookUpEditIndexMarker.TabIndex = 6;
            // 
            // layoutControlItemIndexMarker
            // 
            this.layoutControlItemIndexMarker.Control = this.gridLookUpEditIndexMarker;
            this.layoutControlItemIndexMarker.CustomizationFormText = "IndexMarker";
            this.layoutControlItemIndexMarker.Location = new System.Drawing.Point(64, 220);
            this.layoutControlItemIndexMarker.Name = "IndexMarker";
            this.layoutControlItemIndexMarker.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItemIndexMarker.Size = new System.Drawing.Size(130, 30);
            this.layoutControlItemIndexMarker.Text = "IndexMarker:";
            this.layoutControlItemIndexMarker.TextSize = new System.Drawing.Size(60, 13);
			

            this.gridLookUpEditViewIndexMarker.Columns.Add(gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag);

            // 
            // gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag
            // 
            this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag.Caption = "AlphanumericTag";
            this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag.Name = "gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag";
            this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag.FieldName = "_AlphanumericTag";
            this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag.Visible = true;
            //this.gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag.VisibleIndex = 0;
			
            this.gridLookUpEditViewIndexMarker.Columns.Add(gridColumnLookupIndexMarkerIndexMarkerDateCreated);

            // 
            // gridColumnLookupIndexMarkerIndexMarkerDateCreated
            // 
            this.gridColumnLookupIndexMarkerIndexMarkerDateCreated.Caption = "DateCreated";
            this.gridColumnLookupIndexMarkerIndexMarkerDateCreated.Name = "gridColumnLookupIndexMarkerIndexMarkerDateCreated";
            this.gridColumnLookupIndexMarkerIndexMarkerDateCreated.FieldName = "_DateCreated";
            this.gridColumnLookupIndexMarkerIndexMarkerDateCreated.Visible = true;
            //this.gridColumnLookupIndexMarkerIndexMarkerDateCreated.VisibleIndex = 1;
			

            // 
            // layoutControlGroupFormularFields
            // 
            this.layoutControlGroupFormularFields.CustomizationFormText = "layoutControlGroupFormularFields";
            this.layoutControlGroupFormularFields.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
		
			this.layoutControlItemBookName,
	
			this.layoutControlItemPage,
	
			this.layoutControlItemSubject,
	
			this.layoutControlItemChapter,
	
			this.layoutControlItemVerse,
	
			this.layoutControlItemIndexMarker,
	
			this.emptySpaceItem1
			});

					
            this.layoutControl1.Controls.Add(this.textBookName);
	
            this.layoutControl1.Controls.Add(this.textPage);
	
            this.layoutControl1.Controls.Add(this.textSubject);
	
            this.layoutControl1.Controls.Add(this.textChapter);
	
            this.layoutControl1.Controls.Add(this.textVerse);
	
            this.layoutControl1.Controls.Add(this.gridLookUpEditIndexMarker);
	


            this.layoutControlGroupFormularFields.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupFormularFields.Name = "layoutControlGroupFormularFields";
            this.layoutControlGroupFormularFields.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroupFormularFields.Size = new System.Drawing.Size(383, 256);
            this.layoutControlGroupFormularFields.Text = "layoutControlGroupFormularFields";
            this.layoutControlGroupFormularFields.TextVisible = false;

            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 60);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem1.Size = new System.Drawing.Size(375, 22);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);


            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 60);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem2.Size = new System.Drawing.Size(375, 22);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			
            // 
            // BookCoordinateDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BookCoordinateDetailView";
            this.Size = new System.Drawing.Size(383, 289);
            ((System.ComponentModel.ISupportInitialize)(this.BookCoordinateBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();

		
            ((System.ComponentModel.ISupportInitialize)(this.textBookName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBookName)).EndInit();
	
            ((System.ComponentModel.ISupportInitialize)(this.textPage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPage)).EndInit();
	
            ((System.ComponentModel.ISupportInitialize)(this.textSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSubject)).EndInit();
	
            ((System.ComponentModel.ISupportInitialize)(this.textChapter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemChapter)).EndInit();
	
            ((System.ComponentModel.ISupportInitialize)(this.textVerse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemVerse)).EndInit();
	
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEditIndexMarker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIndexMarker)).EndInit();
	
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFormularFields)).EndInit();

            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.BindingSource BookCoordinateBindingSource;

		private System.Windows.Forms.BindingSource IndexMarkerBookCoordinateBindingSource;
	
		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar1;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarButtonItem bbiComments;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;

		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupFormularFields;

		
            private DevExpress.XtraEditors.TextEdit textBookName;
			private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBookName;
			
            private DevExpress.XtraEditors.TextEdit textPage;
			private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPage;
			
            private DevExpress.XtraEditors.TextEdit textSubject;
			private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSubject;
			
            private DevExpress.XtraEditors.TextEdit textChapter;
			private DevExpress.XtraLayout.LayoutControlItem layoutControlItemChapter;
			
            private DevExpress.XtraEditors.TextEdit textVerse;
			private DevExpress.XtraLayout.LayoutControlItem layoutControlItemVerse;
			
            private DevExpress.XtraEditors.GridLookUpEdit gridLookUpEditIndexMarker;
            private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEditViewIndexMarker;
            private DevExpress.XtraLayout.LayoutControlItem layoutControlItemIndexMarker;
			

		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLookupIndexMarkerIndexMarkerAlphanumericTag;
			
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnLookupIndexMarkerIndexMarkerDateCreated;
			



	}
}
