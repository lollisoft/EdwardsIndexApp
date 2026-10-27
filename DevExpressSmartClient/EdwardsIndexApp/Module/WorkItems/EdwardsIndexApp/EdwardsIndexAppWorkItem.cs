
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
using System.Windows.Forms;
using EdwardsIndexAppCommon;
using EdwardsIndexAppModule.Constants;
using EdwardsIndexAppModule.Properties;

using EdwardsIndexAppModule.WorkItems.IndexMarker;

using EdwardsIndexAppModule.WorkItems.BookCoordinate;

using EdwardsIndexAppModule.WorkItems.Annotation;

using EdwardsIndexAppModule.WorkItems.Theme;

using CABDevExpress.SmartPartInfos;
using CABDevExpress.Workspaces;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraReports.UserDesigner;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.CompositeUI.SmartParts;
using DevExpress.XtraEditors;
using EdwardsIndexAppModule.Reports;
using DevExpress.XtraReports.UI;
using System.IO;
using ShellCommon;

namespace EdwardsIndexAppModule.WorkItems.EdwardsIndexApp
{
    public enum ReportType
    {
IndexMarker,
BookCoordinate,
Annotation,
Theme,

		UnknownType
    }

	public class EdwardsIndexAppWorkItem : WorkItem, IShowInShell
	{
        private ViewMode viewMode = ViewMode.ViewGrid;
		private BarItem queueItem;
		private IWorkspace contentWorkspace;

		// The work item uses the state persistence service that's been registered
		// in the shell initialization
		public IStatePersistenceService PersistenceService
		{
			get { return Services.Get<IStatePersistenceService>(); }
		}

		// Here we populate the work item with some of our views and start showing
		// ourselves. The EdwardsIndexAppMainView has smart part placeholders named
		// UserInfo and CustomerList; these are filled in at runtime with the
		// smart parts that are registered with those names. We chose to put a
		// UserInfoView in the "UserInfo" placeholder, and a CustomerQueueView
		// in the "CustomerList" placeholder.
		//
		// Note that order is important here. When we create the EdwardsIndexAppMainView,
		// it is going to assume that the smart parts that it needs already exist
		// in the work item. Therefore, we create the smart parts first, and then
		// create the main view that contains them.
		public void Show(IWorkspace navbarWorkspace, IWorkspace content)
		{
			contentWorkspace = content;

			AddMenuItems();
			AddShowNavBarGroups(navbarWorkspace);
			Activate();
		}

		private void AddShowNavBarGroups(IWorkspace navbarWorkspace)
		{
			SmartParts.AddNew<UserInfoView>("UserInfo");	//named because it will be used in a placeholder
	
			var statisticsBarView = SmartParts.AddNew<StatisticsBarView>(SmartPartNames.Statistics);

	

			var statsInfo = new XtraNavBarGroupSmartPartInfo
			                	{
			                		Title = "Statistics",
			                		LargeImage = Resources.statsLarge,
			                		SmallImage = Resources.statsSmall
			                	};

	
			navbarWorkspace.Show(statisticsBarView, statsInfo);
		}

		private void AddMenuItems()
		{
			if (queueItem != null) return;

	
		}

		private bool ShowQueueMenu
		{
			set
			{
				//if (queueItem != null && queueItem.Visibility != value)
				if (queueItem != null && ((queueItem.Visibility == BarItemVisibility.Always ? true : false) != value))
				{
					queueItem.Visibility = value ? BarItemVisibility.Always : BarItemVisibility.Never;
				}
			}
		}

		protected override void OnActivated()
		{
			base.OnActivated();

			ShowQueueMenu = true;
		}



        /// <summary>
        /// Usage sample for the CABDevExpress.Extension Kit XtraWindowWorkspace and XtraWindowSmartPartInfo
        /// the example shows an 'About Dialog'
        /// </summary>
        private void ShowHelpAbout()
        {
            if (!SmartParts.Contains(SmartPartNames.HelpAbout))
                SmartParts.AddNew<AboutEdwardsIndexAppView>(SmartPartNames.HelpAbout);

            var smartPartInfo = new XtraWindowSmartPartInfo
                                    {
                                        Modal = true,
                                        StartPosition = FormStartPosition.CenterParent,
                                        FormBorderStyle = FormBorderStyle.FixedDialog,
                                        MinimizeBox = false,
                                        MaximizeBox = false,
                                        Height = 150,
                                        Width = 350,
                                        Title = "About"
                                    };

            // the two properties added by CABDevExpress.ExtensionKit's XtraWindowSmartPartInfo
            var xtraWindow = new XtraWindowWorkspace();
            xtraWindow.Show(SmartParts[SmartPartNames.HelpAbout], smartPartInfo);
        }

        [CommandHandler("ViewGrid")]
        public void OnViewGrid(object sender, EventArgs e)
        {
			viewMode = ViewMode.ViewGrid;
        }

        [CommandHandler("ViewFixedForm")]
        public void OnViewFixedForm(object sender, EventArgs e)
        {
			viewMode = ViewMode.ViewFixedForm;
        }

        [CommandHandler("ViewDesignableForm")]
        public void OnViewDesignableForm(object sender, EventArgs e)
        {
			viewMode = ViewMode.ViewDesignableForm;
        }

        [CommandHandler("ViewGenericDataLayoutForm")]
        public void OnViewGenericDataLayoutForm(object sender, EventArgs e)
        {
			viewMode = ViewMode.ViewGenericDataLayoutForm;
        }

		private void LoadPreview(string url)
		{
			XtraReport r = XtraReport.FromFile(url, true);
			
			if (r is IReport)
				(r as IReport).LoadDataSource();
			r.ShowPreview();
		}
		
        [CommandHandler("Preview Report")]
        public void OnPreviewReport(object sender, EventArgs e)
        {
            string tag = RootWorkItem.State["Preview Report Tag"] as string;

            if (tag != null)
            {
                RootWorkItem.State["Preview Report Tag"] = null;
                LoadPreview(tag);
            }
		}

		private void LoadDesigner(ReportType type)
		{
			LoadDesigner(type, null);
		}

		private void LoadDesigner(ReportType type, string tag)
		{
            XRDesignRibbonForm form = new XRDesignRibbonForm();
			var controller = form.DesignMdiController;
			

			if (type == ReportType.IndexMarker)
			{
				controller.AddService(typeof(ReportTypeService), new IndexMarkerReportService());
			}

			if (type == ReportType.BookCoordinate)
			{
				controller.AddService(typeof(ReportTypeService), new BookCoordinateReportService());
			}

			if (type == ReportType.Annotation)
			{
				controller.AddService(typeof(ReportTypeService), new AnnotationReportService());
			}

			if (type == ReportType.Theme)
			{
				controller.AddService(typeof(ReportTypeService), new ThemeReportService());
			}

			
			if (tag == null || tag == "")
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EdwardsIndexApp");
                openFileDialog1.Filter = "Report files (*.repx)|*.repx|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    tag = openFileDialog1.FileName;
                }
            }

            if (tag != null && tag != "")
            {
                controller.OpenReport(XtraReport.FromFile(tag, true));
            }
            else 
            {
                controller.CreateNewReport();
            }

			form.DesignDockManager[DesignDockPanelType.FieldList].Show();
			form.ShowDialog();
		}


        [CommandHandler("Create Report For IndexMarker")]
        public void OnCreateReportIndexMarker(object sender, EventArgs e)
        {
			LoadDesigner(ReportType.IndexMarker);
		}
		
        [CommandHandler("IndexMarker verwalten")]
        public void OnIndexMarker(object sender, EventArgs e)
        {
            var workItem = WorkItems.Get<IndexMarkerCRUDWorkItem>("IndexMarker_" + viewMode);

            if (workItem == null)
            {
                workItem = WorkItems.AddNew<IndexMarkerCRUDWorkItem>("IndexMarker_" + viewMode);
            }

			workItem.ViewMode = viewMode;
            workItem.Show(contentWorkspace);
        }

        [CommandHandler("Create Report For BookCoordinate")]
        public void OnCreateReportBookCoordinate(object sender, EventArgs e)
        {
			LoadDesigner(ReportType.BookCoordinate);
		}
		
        [CommandHandler("BookCoordinate verwalten")]
        public void OnBookCoordinate(object sender, EventArgs e)
        {
            var workItem = WorkItems.Get<BookCoordinateCRUDWorkItem>("BookCoordinate_" + viewMode);

            if (workItem == null)
            {
                workItem = WorkItems.AddNew<BookCoordinateCRUDWorkItem>("BookCoordinate_" + viewMode);
            }

			workItem.ViewMode = viewMode;
            workItem.Show(contentWorkspace);
        }

        [CommandHandler("Create Report For Annotation")]
        public void OnCreateReportAnnotation(object sender, EventArgs e)
        {
			LoadDesigner(ReportType.Annotation);
		}
		
        [CommandHandler("Annotation verwalten")]
        public void OnAnnotation(object sender, EventArgs e)
        {
            var workItem = WorkItems.Get<AnnotationCRUDWorkItem>("Annotation_" + viewMode);

            if (workItem == null)
            {
                workItem = WorkItems.AddNew<AnnotationCRUDWorkItem>("Annotation_" + viewMode);
            }

			workItem.ViewMode = viewMode;
            workItem.Show(contentWorkspace);
        }

        [CommandHandler("Create Report For Theme")]
        public void OnCreateReportTheme(object sender, EventArgs e)
        {
			LoadDesigner(ReportType.Theme);
		}
		
        [CommandHandler("Theme verwalten")]
        public void OnTheme(object sender, EventArgs e)
        {
            var workItem = WorkItems.Get<ThemeCRUDWorkItem>("Theme_" + viewMode);

            if (workItem == null)
            {
                workItem = WorkItems.AddNew<ThemeCRUDWorkItem>("Theme_" + viewMode);
            }

			workItem.ViewMode = viewMode;
            workItem.Show(contentWorkspace);
        }


        [CommandHandler(CommandNames.ChangePassword)]
        public void OnChangePassword(object sender, EventArgs e)
        {
            IChangePasswordService changePasswordService = RootWorkItem.Services.Get<IChangePasswordService>(true);
            if (!changePasswordService.ChangePassword())
            {
                XtraMessageBox.Show("Your password was not changed. Maybe your provided old one was not correct.", "Error");
            }
        }

		[CommandHandler(CommandNames.HelpAbout)]
		public void OnHelpAbout(object sender, EventArgs e)
		{
            ShowHelpAbout();
        }

        [EventSubscription(EventNames.HelpAbout, ThreadOption.UserInterface)]
        public void OnHelpAboutEvent(object sender, EventArgs e)
        {
            ShowHelpAbout();
        }

		const string DockableStatisticsView = "DockableStatisticsView";

		/// <summary>
		/// Usage sample for the CABDevExpress.Extension Kit DockManagerWorkspace
		/// </summary>
		/// <param name="dockWorkspace"></param>
		public void Show(IWorkspace dockWorkspace)
		{
			var info = new DockManagerSmartPartInfo {Name = "Statistics", Dock = DockingStyle.Right};

			SmartParts.AddNew<StatisticsBarView>(DockableStatisticsView);
			dockWorkspace.Show(SmartParts[DockableStatisticsView], info);
		}
	}
	
	public enum ViewMode {
		ViewGrid,
		ViewFixedForm,
		ViewDesignableForm,
		ViewGenericDataLayoutForm
	}
}
