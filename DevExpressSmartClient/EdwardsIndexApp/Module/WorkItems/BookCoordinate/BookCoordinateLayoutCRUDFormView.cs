
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

using System;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.IO;
using EdwardsIndexAppModule.Services;
using ShellCommon;



namespace EdwardsIndexAppModule.WorkItems.BookCoordinate
{
	[SmartPart]
	public partial class BookCoordinateLayoutCRUDFormView : XtraUserControl
	{
		private EdwardsIndexAppCommon.XPO.BookCoordinate _BookCoordinate;

		private int position;
		
		public void Position(int _position) {
			position = _position;
		}
		

		private IndexMarkerService _IndexMarkerBookCoordinateService;

		// Make sure our required anwendungenService is available
		[ServiceDependency]
		public IndexMarkerService IndexMarkerBookCoordinateService
		{
			set 
			{ 
				_IndexMarkerBookCoordinateService = value;
				IndexMarkerBookCoordinateBindingSource.DataSource = _IndexMarkerBookCoordinateService.Load();
			}
		}
     

		// The BookCoordinate state is stored in our parent work item
		[State]
		public EdwardsIndexAppCommon.XPO.BookCoordinate BookCoordinate
		{
			set 
            {
                if (_BookCoordinate != null)
                {
                    _BookCoordinate.Save();
                }

                _BookCoordinate = value;
            }
			get 
            { 
                return _BookCoordinate; 
            }
		}

		private WorkItem parentWorkItem;
		//private BookCoordinateDetailController controller;

		public BookCoordinateLayoutCRUDFormView()
		{
			InitializeComponent();

 
		}

		[ServiceDependency]
		public WorkItem ParentWorkItem
		{
			set { parentWorkItem = value; }
		}

		//// We use our controller so we can show the comments page
		//[CreateNew]
		//public BookCoordinateDetailController Controller
		//{
		//	set { controller = value; }
		//}

        private string GetLayoutFileName()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), GetType().FullName + ".Layout.xml");
        }

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
            if (!File.Exists(GetLayoutFileName()))
                layoutControl1.SaveLayoutToXml(GetLayoutFileName());
            layoutControl1.RestoreLayoutFromXml(GetLayoutFileName());
            layoutControl1.OptionsCustomizationForm.ShowLoadButton = false;
            layoutControl1.OptionsCustomizationForm.ShowSaveButton = false;

			Refresh();
		}

		public void Refresh()
		{

			DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.BookCoordinate> coll = new DevExpress.Xpo.XPCollection<EdwardsIndexAppCommon.XPO.BookCoordinate>();
			BookCoordinateBindingSource.DataSource = coll;
			BookCoordinateBindingSource.Position = position;
		}
		
		public void EndEdit()
        {
            BookCoordinateBindingSource.EndEdit();
        }

		private void OnShowComments(object sender, ItemClickEventArgs e)
		{
			//controller.ShowBookCoordinateComments();
		}

		IDesignCollaboration collaborationClient;
		
        private void layoutControl1_HideCustomization(object sender, EventArgs e)
        {
			// Save layout local
            layoutControl1.SaveLayoutToXml(GetLayoutFileName());

			collaborationClient = parentWorkItem.Services.Get<IDesignCollaboration>(true);

			if (collaborationClient.IsCollaborationEnabled())
            {
                MemoryStream mStream = new MemoryStream();
                layoutControl1.SaveLayoutToStream(mStream);

                var buffer = mStream.GetBuffer();

				collaborationClient.SetScope("EdwardsIndexApp", "BookCoordinate");
				collaborationClient.SetFormDesign(buffer);
				
				IScreenCaptureService capture = parentWorkItem.Services.Get<IScreenCaptureService>(true);

				capture.ImageCaptured += capture_ImageCaptured;

				capture.CaptureWindow(this.Handle);
			}
        }

		void capture_ImageCaptured(byte[] image)
        {
			collaborationClient.SetFormDesignImage(image);
			
			// Depending on the implementation and the user rights, this interacts is a service or uses for sample a shared web folder (Dropbox)
			collaborationClient.ShareDesign();
        }

	}
}
