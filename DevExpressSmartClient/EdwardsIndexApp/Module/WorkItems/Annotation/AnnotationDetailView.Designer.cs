
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
	partial class AnnotationDetailView
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
            this.AnnotationBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
			
					

			
			
            ((System.ComponentModel.ISupportInitialize)(this.AnnotationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFormularFields)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();

		
            this.SuspendLayout();
            // 
            // AnnotationBindingSource
            // 

            this.AnnotationBindingSource.DataSource = typeof(EdwardsIndexAppCommon.Annotation);
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
            // layoutControlGroupFormularFields
            // 
            this.layoutControlGroupFormularFields.CustomizationFormText = "layoutControlGroupFormularFields";
            this.layoutControlGroupFormularFields.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
		
			this.emptySpaceItem1
			});

					


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
            // AnnotationDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "AnnotationDetailView";
            this.Size = new System.Drawing.Size(383, 289);
            ((System.ComponentModel.ISupportInitialize)(this.AnnotationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();

		
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFormularFields)).EndInit();

            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.BindingSource AnnotationBindingSource;
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

		



	}
}
