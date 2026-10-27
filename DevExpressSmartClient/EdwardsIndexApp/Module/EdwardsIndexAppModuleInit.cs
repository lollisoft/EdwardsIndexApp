
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
using EdwardsIndexAppModule.Constants;
using EdwardsIndexAppModule.WorkItems.EdwardsIndexApp;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using EdwardsIndexAppModule.Properties;
using System.Drawing;
using System.IO;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.EnterpriseLibrary.Security;
using System.Security.Principal;
using System.Threading;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

using System.Security.Cryptography.X509Certificates;
using EdwardsIndexAppSecurity;
using System.Security;
using System.Runtime.InteropServices;
using System.ServiceModel.Description;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI.Services;
					
//TODO: Performance measurement at the right place?
					
using ShellCommon;

using System.Net;
using System.Net.Sockets;

namespace EdwardsIndexAppModule
{
	// This is the initialization class for the Module. Any classes in the assembly that
	// derive from Microsoft.Practices.CompositeUI.ModuleInit will automatically
	// be created and called for initialization.

	public class EdwardsIndexAppModuleInit : ModuleInit
	{
        IAuthorizationProvider authorizationProvider = null;
        IPrincipal principal = null;

		protected static bool _DALInitialized = false;
		
		protected static Binding CreateBinding(string security_mode)
        {
            Binding _binding;

            switch (security_mode)
            {

                case "Brokered_Auth":
                    CustomBinding bindingBA = new CustomBinding("UsernameBinding"); // Use the configuration
                    _binding = bindingBA;
                    break;
					
                case "Transport":
                    NetTcpBinding bindingT = new NetTcpBinding(SecurityMode.Transport);
                    bindingT.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
                    bindingT.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;

                    bindingT.MaxReceivedMessageSize = Int32.MaxValue;
                    bindingT.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
                    bindingT.ReaderQuotas.MaxDepth = Int32.MaxValue;
                    bindingT.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;
                    bindingT.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
                    _binding = bindingT;

                    break;
                case "Message":
                    NetTcpBinding bindingM = new NetTcpBinding(SecurityMode.Message);
                    bindingM.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
                    bindingM.Security.Message.ClientCredentialType = MessageCredentialType.None;
                    _binding = bindingM;
                    break;
                case "None":
                    NetTcpBinding bindingN = new NetTcpBinding(SecurityMode.None);
                    _binding = bindingN;

                    break;
                default:
                    NetTcpBinding bindingD = new NetTcpBinding();
                    _binding = bindingD;
                    break;
            }
            
            return _binding;
        }

        protected static X509CertificateValidationMode SetCertificateValidationMode(string X509CertificateValidationMode)
        {
            X509CertificateValidationMode mode = System.ServiceModel.Security.X509CertificateValidationMode.ChainTrust;

            switch (X509CertificateValidationMode)
            {
                case "None":
                    mode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                    break;
                case "PeerTrust":
                    mode = System.ServiceModel.Security.X509CertificateValidationMode.PeerTrust;
                    break;
                case "PeerOrChainTrust":
                    mode = System.ServiceModel.Security.X509CertificateValidationMode.PeerOrChainTrust;
                    break;
                case "ChainTrust":
                    mode = System.ServiceModel.Security.X509CertificateValidationMode.ChainTrust;
                    break;
            }

            return mode;
        }

		protected string convertToUNSecureString(SecureString secstrPassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secstrPassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static DateTime GetNetworkTime()
        {
            const string ntpServer = "pool.ntp.org";
            var ntpData = new byte[48];
            ntpData[0] = 0x1B; //LeapIndicator = 0 (no warning), VersionNum = 3 (IPv4 only), Mode = 3 (Client Mode)

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;

            foreach (var address in addresses) 
            {
                try
                {
                    var ipEndPoint = new IPEndPoint(address, 123);
                    var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    socket.ReceiveTimeout = 3000;
                    socket.Connect(ipEndPoint);
                    socket.Send(ntpData);
                    socket.Receive(ntpData);
                    socket.Close();

                    ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
                    ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

                    var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
                    var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);

                    return networkDateTime;
                }
                catch (Exception e) 
                {
                }
            }

            throw new InvalidDataException("Can't get network time from any server.");
        }
        private bool CheckTimeDiff()
        {
            try
            {
                DateTime serverTime = GetNetworkTime().ToLocalTime();
                DateTime clientTime = DateTime.Now;
                double totalMinutes = (clientTime - serverTime).TotalMinutes;
                return Math.Abs(totalMinutes) > 5.0;
            }
            catch (Exception e) 
            {
                return false;
            }
        }

        public bool InitDAL(string user, SecureString pass)
        {
			if (_DALInitialized)
				return true;
							
			try 
			{

                string host = ConfigurationManager.AppSettings.Get("DataStoreHost");
                string port = ConfigurationManager.AppSettings.Get("DataStorePort");
                string security_mode = ConfigurationManager.AppSettings.Get("DataStoreSecurityMode");
                string X509CertificateValidationMode = ConfigurationManager.AppSettings.Get("X509CertificateValidationMode");
                string LocalMachine_My_BySubject_ClientCertificate = ConfigurationManager.AppSettings.Get("LocalMachine_My_BySubject_ClientCertificate");
                string LocalMachine_My_BySubject_ServerCertificate = ConfigurationManager.AppSettings.Get("LocalMachine_My_BySubject_ServerCertificate");
                string DnsIdentity = ConfigurationManager.AppSettings.Get("DnsIdentity");

                string auxPath = "net.tcp://" + host + ":" + port + "/EdwardsIndexAppService";

				// Security mode tells also when to use brokered auth, if supported.
				
                Binding binding = CreateBinding(security_mode);

                EndpointAddress addr;

                if (DnsIdentity != null && DnsIdentity != "")
                {
                    addr = new EndpointAddress(new Uri(auxPath), EndpointIdentity.CreateDnsIdentity(DnsIdentity));
                }
                else
                {
                    addr = new EndpointAddress(new Uri(auxPath));
                }

                DevExpress.Xpo.DB.DataStoreClient dataLayer;

                // Auth with STS
                if (security_mode == "Brokered_Auth")
                {
                    dataLayer = new DevExpress.Xpo.DB.DataStoreClient("EdwardsIndexAppEndpoint");
                }
                else
                {
                    dataLayer = new DevExpress.Xpo.DB.DataStoreClient(binding, addr);
                }

				string password = convertToUNSecureString(pass);

                InMemoryIssuedTokenCache cache = new InMemoryIssuedTokenCache();

                dataLayer.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = SetCertificateValidationMode(X509CertificateValidationMode);
				

                // Auth with STS
				if (security_mode == "Brokered_Auth") {
					
					DurableIssuedTokenClientCredentials durableCreds = new DurableIssuedTokenClientCredentials();
                    durableCreds.IssuedTokenCache = cache;

                    dataLayer.ChannelFactory.Endpoint.Behaviors.Remove<ClientCredentials>();
                    dataLayer.ChannelFactory.Endpoint.Behaviors.Add(durableCreds);

                    dataLayer.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = SetCertificateValidationMode(X509CertificateValidationMode);
                    if (LocalMachine_My_BySubject_ServerCertificate != null && LocalMachine_My_BySubject_ServerCertificate != "")
                        dataLayer.ClientCredentials.ServiceCertificate.SetDefaultCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, LocalMachine_My_BySubject_ServerCertificate); //"cloud.lollisoft.de");
                    if (LocalMachine_My_BySubject_ClientCertificate != null && LocalMachine_My_BySubject_ClientCertificate != "")
                        dataLayer.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, LocalMachine_My_BySubject_ClientCertificate); //"cloud.lollisoft.de");

					dataLayer.ChannelFactory.Credentials.UserName.UserName = user;
					dataLayer.ChannelFactory.Credentials.UserName.Password = password;
				}
					

				// Performance measurement
				string measurement = ConfigurationManager.AppSettings.Get("EnablePerformanceMeasurement");
				if (measurement != null && measurement.ToUpper() == "TRUE") dataLayer.Endpoint.Behaviors.Add(new MessagePerformanceMonitoring());

                DevExpress.Xpo.SimpleDataLayer dtLayer = new DevExpress.Xpo.SimpleDataLayer(dataLayer);
                DevExpress.Xpo.XpoDefault.DataLayer = dtLayer;
				
				if (security_mode == "Brokered_Auth")
                {
                    try
                    {
						// Make a server roundtrip to test login credentials are working.
						// This enables a retry without using specific select statements.
                        DevExpress.Xpo.UnitOfWork uow = new DevExpress.Xpo.UnitOfWork();

                        uow.ExplicitBeginTransaction();
                        uow.ExplicitRollbackTransaction();

                        password = "";
					}
                    catch (MessageSecurityException secex)
                    {
                        if (secex.InnerException != null)
                        {
                            if (CheckTimeDiff()) 
                            {
                                XtraMessageBox.Show("Please check your local time. It may be not in sync with the server. This probably causes the following error:\n\n" + secex.InnerException.Message, "Error");
                                return false;
                            }
                            XtraMessageBox.Show("Something went wrong while connecting to the database 'EdwardsIndexApp': " + secex.InnerException.Message, "Error");
                            return false;
                        }
                    }
                }
			
			}
			catch (Exception e)
			{
				XtraMessageBox.Show("Something went wrong while connecting to the database 'EdwardsIndexApp': " + e.Message, "Error");
				return false;
			}
			
			_DALInitialized = true;
			return true;
        }

		private readonly WorkItem workItem;
		
		[InjectionConstructor]
		public EdwardsIndexAppModuleInit([ServiceDependency] WorkItem workItem)
		{
			this.workItem = workItem;
			
			ISecureCredentials cred = workItem.Services.Get<ISecureCredentials>(true);

            int retry = 3;

            while (!InitDAL(cred.UserName, cred.Password))
            {
                if (retry == 0)
                {
                    return;
                }

                retry--;
                IAuthenticationService login = workItem.Services.Get<IAuthenticationService>(true);
                login.Authenticate();
            }

            if (retry == 0 && !InitDAL(cred.UserName, cred.Password))

            try
            {
                // Create a principal to use for new threads.
                IIdentity identity = new GenericIdentity(cred.UserName);
                IPrincipal principal = new GenericPrincipal(identity, null);
                AppDomain.CurrentDomain.SetThreadPrincipal(principal);
                Thread.CurrentPrincipal = principal;

                authorizationProvider = AuthorizationFactory.GetAuthorizationProvider("Custom Authorization Provider");
            }
            catch (Exception e)
            {

            }
            finally
            {
                principal = new GenericPrincipal(Thread.CurrentPrincipal.Identity, new string[] { "Manager" });
            }
		}

		private BarButtonItem editMenuItemSave;
        private BarButtonItem editMenuItemNew;
        private BarButtonItem editMenuItemDelete;
        private BarButtonItem editMenuItemFirst;
        private BarButtonItem editMenuItemPrevious;
        private BarButtonItem editMenuItemNext;
        private BarButtonItem editMenuItemLast;

        private BarButtonItem editMenuItemSimpleMode;
        private BarButtonItem editMenuItemDesignableMode;
        private BarButtonItem editMenuItemGenericDataLayoutMode;
        private BarButtonItem editMenuItemGridMode;
        private BarButtonItem editMenuItemRefreshReportList;
		
        private BarButtonItem editMenuItemChangePassword;

		// Create report
        private BarSubItem editMenuItemCreateReport;

		// Preview report
        private BarSubItem editMenuItemPreviewReport;
		
        private string EdwardsIndexAppRibbonCRUDReportGroup;


		private BarButtonItem createReportIndexMarker;

		private BarButtonItem createReportBookCoordinate;

		private BarButtonItem createReportAnnotation;

		private BarButtonItem createReportTheme;


		
		public override void Load()
		{
			AddCustomerMenuItem();

            AddModuleRibbonPage();

			//Retrieve well known workspaces
			IWorkspace navbarWorkspace = workItem.Workspaces[WorkspaceNames.ShellNavBarWorkspace];
			IWorkspace contentWorkspace = workItem.Workspaces[WorkspaceNames.ShellContentWorkspace];

            EdwardsIndexAppWorkItem EdwardsIndexAppWorkItem = workItem.WorkItems.AddNew<EdwardsIndexAppWorkItem>();
            EdwardsIndexAppWorkItem.Show(navbarWorkspace, contentWorkspace);

			IWorkspace dockWorkspace = workItem.Workspaces[WorkspaceNames.DockManagerWorkspace];
			EdwardsIndexAppWorkItem.Show(dockWorkspace);
		}
		
        private void AddModuleRibbonPage()
        {
            //RibbonPage ribbonPage = new RibbonPage(ExtensionSiteNames.RibbonLookAndFeel);
            string EdwardsIndexAppRibbon = "EdwardsIndexApp";
            string EdwardsIndexAppRibbonGroup = "EdwardsIndexApp Group";
            string EdwardsIndexAppRibbonCRUDGroup = "EdwardsIndexApp CRUD Group";
            string EdwardsIndexAppRibbonCRUDWindowGroup = "EdwardsIndexApp Window Group";
            EdwardsIndexAppRibbonCRUDReportGroup = "EdwardsIndexApp Report Group";

            try
            {


                //IndexMarker
                const string IndexMarker = "IndexMarker verwalten";
				var btnIndexMarker = new BarButtonItem
				{
					Caption = IndexMarker,
					Hint = "",
					Glyph = Resources.IndexMarker,
					LargeGlyph = Resources.IndexMarker,
					//ItemShortcut = new BarShortcut(Keys.Control | Keys.A)
				};

                if (authorizationProvider != null && authorizationProvider.Authorize(principal, IndexMarker)) 
                {
					createCommandButton(EdwardsIndexAppRibbonGroup, IndexMarker, btnIndexMarker, "", "");
				} else 
				{
					// Fallback bis es geht
					if (authorizationProvider == null) 
					{
						createCommandButton(EdwardsIndexAppRibbonGroup, IndexMarker, btnIndexMarker, "", "");
					}
				}


                //BookCoordinate
                const string BookCoordinate = "BookCoordinate verwalten";
				var btnBookCoordinate = new BarButtonItem
				{
					Caption = BookCoordinate,
					Hint = "",
					Glyph = Resources.BookCoordinate,
					LargeGlyph = Resources.BookCoordinate,
					//ItemShortcut = new BarShortcut(Keys.Control | Keys.A)
				};

                if (authorizationProvider != null && authorizationProvider.Authorize(principal, BookCoordinate)) 
                {
					createCommandButton(EdwardsIndexAppRibbonGroup, BookCoordinate, btnBookCoordinate, "", "");
				} else 
				{
					// Fallback bis es geht
					if (authorizationProvider == null) 
					{
						createCommandButton(EdwardsIndexAppRibbonGroup, BookCoordinate, btnBookCoordinate, "", "");
					}
				}


                //Annotation
                const string Annotation = "Annotation verwalten";
				var btnAnnotation = new BarButtonItem
				{
					Caption = Annotation,
					Hint = "",
					Glyph = Resources.Annotation,
					LargeGlyph = Resources.Annotation,
					//ItemShortcut = new BarShortcut(Keys.Control | Keys.A)
				};

                if (authorizationProvider != null && authorizationProvider.Authorize(principal, Annotation)) 
                {
					createCommandButton(EdwardsIndexAppRibbonGroup, Annotation, btnAnnotation, "", "");
				} else 
				{
					// Fallback bis es geht
					if (authorizationProvider == null) 
					{
						createCommandButton(EdwardsIndexAppRibbonGroup, Annotation, btnAnnotation, "", "");
					}
				}


                //Theme
                const string Theme = "Theme verwalten";
				var btnTheme = new BarButtonItem
				{
					Caption = Theme,
					Hint = "",
					Glyph = Resources.Theme,
					LargeGlyph = Resources.Theme,
					//ItemShortcut = new BarShortcut(Keys.Control | Keys.A)
				};

                if (authorizationProvider != null && authorizationProvider.Authorize(principal, Theme)) 
                {
					createCommandButton(EdwardsIndexAppRibbonGroup, Theme, btnTheme, "", "");
				} else 
				{
					// Fallback bis es geht
					if (authorizationProvider == null) 
					{
						createCommandButton(EdwardsIndexAppRibbonGroup, Theme, btnTheme, "", "");
					}
				}


            CreateBarButtonItem(EdwardsIndexAppRibbonCRUDGroup, "Save", Resources.CommitRow, ref editMenuItemSave, "Home", "File");
            CreateBarButtonItem(EdwardsIndexAppRibbonCRUDGroup, "Delete", Resources.DeleteRow, ref editMenuItemDelete, "Home", "File");
            CreateBarButtonItem(EdwardsIndexAppRibbonCRUDGroup, "New", Resources.NewRow, ref editMenuItemNew, "Home", "File");
            CreateBarButtonItem(EdwardsIndexAppRibbonCRUDGroup, "First", Resources.FirstRow, ref editMenuItemFirst, "Home", "Navigation");
            CreateBarButtonItem(EdwardsIndexAppRibbonCRUDGroup, "Previous", Resources.PreviousRow, ref editMenuItemPrevious, "Home", "Navigation");
            CreateBarButtonItem(EdwardsIndexAppRibbonCRUDGroup, "Next", Resources.NextRow, ref editMenuItemNext, "Home", "Navigation");
            CreateBarButtonItem(EdwardsIndexAppRibbonCRUDGroup, "Last", Resources.LastRow, ref editMenuItemLast, "Home", "Navigation");

            CreateBarButtonItem(EdwardsIndexAppRibbonCRUDWindowGroup, "ViewGrid", Resources.ViewGrid, ref editMenuItemGridMode, "Settings", "View");
            CreateBarButtonItem(EdwardsIndexAppRibbonCRUDWindowGroup, "ViewFixedForm", Resources.ViewFixedForm, ref editMenuItemSimpleMode, "Settings", "View");
            CreateBarButtonItem(EdwardsIndexAppRibbonCRUDWindowGroup, "ViewDesignableForm", Resources.ViewDesignableForm, ref editMenuItemDesignableMode, "Settings", "View");
            CreateBarButtonItem(EdwardsIndexAppRibbonCRUDWindowGroup, "ViewGenericDataLayoutForm", Resources.ViewDesignableForm, ref editMenuItemGenericDataLayoutMode, "Settings", "View");
            CreateBarButtonItem(EdwardsIndexAppRibbonCRUDWindowGroup, CommandNames.ChangePassword, Resources.Edit32, ref editMenuItemChangePassword, "Settings", "Usersettings");

            CreateBarMenuItem(EdwardsIndexAppRibbonCRUDReportGroup, "Create report", Resources.ViewGrid, ref editMenuItemCreateReport, "Home", "Reports");

            CreateBarMenuItem(EdwardsIndexAppRibbonCRUDReportGroup, "Preview report", Resources.ViewGrid, ref editMenuItemPreviewReport, "Home", "Reports");

            CreateBarButtonItem(EdwardsIndexAppRibbonCRUDReportGroup, "Refresh report menu", Resources.ViewDesignableForm, ref editMenuItemRefreshReportList, "Home", "Reports");
			
			
			

                //IndexMarker
                const string txtcreateReportIndexMarker = "For IndexMarker";
                var createReportIndexMarker = new BarButtonItem
                {
                    Caption = txtcreateReportIndexMarker,
                    Hint = "Report erstellen fuer IndexMarker",
                    Glyph = Resources.IndexMarker,
                    LargeGlyph = Resources.IndexMarker,
                    //ItemShortcut = new BarShortcut(Keys.Control | Keys.A)
                };
				editMenuItemCreateReport.AddItem(createReportIndexMarker);
				createBarSubButtonItem(EdwardsIndexAppRibbonCRUDReportGroup, "Create Report For IndexMarker", createReportIndexMarker);

                //BookCoordinate
                const string txtcreateReportBookCoordinate = "For BookCoordinate";
                var createReportBookCoordinate = new BarButtonItem
                {
                    Caption = txtcreateReportBookCoordinate,
                    Hint = "Report erstellen fuer BookCoordinate",
                    Glyph = Resources.BookCoordinate,
                    LargeGlyph = Resources.BookCoordinate,
                    //ItemShortcut = new BarShortcut(Keys.Control | Keys.A)
                };
				editMenuItemCreateReport.AddItem(createReportBookCoordinate);
				createBarSubButtonItem(EdwardsIndexAppRibbonCRUDReportGroup, "Create Report For BookCoordinate", createReportBookCoordinate);

                //Annotation
                const string txtcreateReportAnnotation = "For Annotation";
                var createReportAnnotation = new BarButtonItem
                {
                    Caption = txtcreateReportAnnotation,
                    Hint = "Report erstellen fuer Annotation",
                    Glyph = Resources.Annotation,
                    LargeGlyph = Resources.Annotation,
                    //ItemShortcut = new BarShortcut(Keys.Control | Keys.A)
                };
				editMenuItemCreateReport.AddItem(createReportAnnotation);
				createBarSubButtonItem(EdwardsIndexAppRibbonCRUDReportGroup, "Create Report For Annotation", createReportAnnotation);

                //Theme
                const string txtcreateReportTheme = "For Theme";
                var createReportTheme = new BarButtonItem
                {
                    Caption = txtcreateReportTheme,
                    Hint = "Report erstellen fuer Theme",
                    Glyph = Resources.Theme,
                    LargeGlyph = Resources.Theme,
                    //ItemShortcut = new BarShortcut(Keys.Control | Keys.A)
                };
				editMenuItemCreateReport.AddItem(createReportTheme);
				createBarSubButtonItem(EdwardsIndexAppRibbonCRUDReportGroup, "Create Report For Theme", createReportTheme);

				LoadReportMenu(EdwardsIndexAppRibbonCRUDReportGroup);
            }
            catch (Exception e)
            {
                XtraMessageBox.Show("Failed to init module ribbon page: " + e.Message, "Error");
            }
        }

        [CommandHandler("Refresh report menu")]
        public void OnPreviewReport(object sender, EventArgs e)
        {
			LoadReportMenu(EdwardsIndexAppRibbonCRUDReportGroup);
		}
		
        Image MapToolBarImage(string name)
        {
            switch (name)
            {

                case "IndexMarker":
                    return Resources.IndexMarker;
                    break;

                case "BookCoordinate":
                    return Resources.BookCoordinate;
                    break;

                case "Annotation":
                    return Resources.Annotation;
                    break;

                case "Theme":
                    return Resources.Theme;
                    break;
            
			}
            return null;
        }
		
		private void LoadReportMenu(string ribbongroup)
		{
            editMenuItemPreviewReport.ClearLinks();
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EdwardsIndexApp");
			
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

			foreach(string f in Directory.GetFiles(path, "*.repx"))
			{
				FileInfo fi = new FileInfo(f);
                string txtcreateReport = "For " + fi.Name.Replace(fi.Extension, "");
                var createReport = new BarButtonItem
                {
                    Caption = txtcreateReport,
                    Hint = "Report erstellen fuer " + f,
                    Glyph = MapToolBarImage(fi.Name.Replace(fi.Extension, "")),
                    LargeGlyph = MapToolBarImage(fi.Name.Replace(fi.Extension, "")),
					Tag = f
                };
				editMenuItemPreviewReport.AddItem(createReport);
                createBarSubButtonItem(ribbongroup, "Preview Report", createReport);

                createReport.ItemClick += new ItemClickEventHandler(createReport_ItemClick);
			}
		}

        void createReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarButtonItem item = e.Item as BarButtonItem;

            if (item != null)
            {
                workItem.RootWorkItem.State["Preview Report Tag"] = item.Tag;
                workItem.Commands["Preview Report"].Execute();
            }

        }
		
        private void CreateBarMenuItem(string CRMRibbonGroup, string Caption, Bitmap icon, ref BarSubItem item, string ribbonPageName, string ribbonGroupName)
        {
            if (item == null)
            {
                item = new BarSubItem
                {
                    Caption = Caption,
                    Hint = Caption,
                    Glyph = icon,
                    LargeGlyph = icon
                };
                createBarSubItem(CRMRibbonGroup, Caption, item, ribbonPageName, ribbonGroupName);
            }
        }

        private void createBarSubButtonItem(string CRMRibbonGroup, string acceptCustomerCaption, BarButtonItem command1)
        {
            workItem.Commands[acceptCustomerCaption].AddInvoker(command1, "ItemClick");
        }

        private void createBarSubItem(string EdwardsIndexAppRibbonGroup, string acceptCustomerCaption, BarSubItem command1, string ribbonPageName, string ribbonGroupName)
        {
#if UseRibbonForm
            if (ribbonGroupName == null || ribbonGroupName.Trim() == "")
            {
                ribbonPageName = "Application";
                ribbonGroupName = EdwardsIndexAppRibbonGroup;
            }

            string pageTitle = ribbonPageName;
            string groupTitle = ribbonGroupName;

            ribbonPageName = "Page_" + ribbonPageName;
            ribbonGroupName = ribbonPageName + "_Group_" + ribbonGroupName;

            if (!workItem.UIExtensionSites.Contains(ribbonGroupName))
            {
                if (ribbonPageName != null && ribbonPageName.Trim() != "")
                {
                    CreateRibbonPage(ribbonPageName, pageTitle);
                }
                CreateRibbonGroup(ribbonPageName, ribbonGroupName, groupTitle);
            }

            workItem.UIExtensionSites[ribbonGroupName].Add(command1);
#else
            string extensionSite;
            extensionSite = ExtensionSiteNames.File;
            queueItem = new BarSubItem { Caption = "Queue" };

			workItem.UIExtensionSites[ExtensionSiteNames.File].Add(queueItem);
			workItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.Queue, queueItem);
			extensionSite = ExtensionSiteNames.Queue;
			workItem.UIExtensionSites[ExtensionSiteNames.ToolBar].Add(command1);
#endif
            workItem.Commands[acceptCustomerCaption].AddInvoker(command1, "ItemClick");
        }

        private void CreateRibbonGroup(string pageName, string groupName, string groupTitle)
        {
            if (!workItem.UIExtensionSites.Contains(groupName))
            {
                RibbonPageGroup ribbonGroup = new RibbonPageGroup(groupTitle);
                workItem.UIExtensionSites[pageName].Add<RibbonPageGroup>(ribbonGroup);
                workItem.UIExtensionSites.RegisterSite(groupName, ribbonGroup);
            }
        }

        private void CreateRibbonPage(string pageName, string pageTitle)
        {
            if (!workItem.UIExtensionSites.Contains(pageName))
            {
                RibbonPage ribbonPage = new RibbonPage(pageTitle);
                workItem.UIExtensionSites[ExtensionSiteNames.Ribbon].Add<RibbonPage>(ribbonPage);
                workItem.UIExtensionSites.RegisterSite(pageName, ribbonPage);
            }
        }

        private void createCommandButton(string EdwardsIndexAppRibbonGroup, string acceptCustomerCaption, BarButtonItem command1, string ribbonPageName, string ribbonGroupName)
        {
#if UseRibbonForm

            if (ribbonGroupName == null || ribbonGroupName.Trim() == "")
            {
                ribbonPageName = "Application";
                ribbonGroupName = EdwardsIndexAppRibbonGroup;
            }
			
			string pageTitle = ribbonPageName;
            string groupTitle = ribbonGroupName;

            ribbonPageName = "Page_" + ribbonPageName;
            ribbonGroupName = ribbonPageName + "_Group_" + ribbonGroupName;

            if (!workItem.UIExtensionSites.Contains(ribbonGroupName))
            {
                if (ribbonPageName != null && ribbonPageName.Trim() != "")
                {
                    CreateRibbonPage(ribbonPageName, pageTitle);
                }
                CreateRibbonGroup(ribbonPageName, ribbonGroupName, groupTitle);
            }

            workItem.UIExtensionSites[ribbonGroupName].Add(command1);

#else
            string extensionSite;
            extensionSite = ExtensionSiteNames.File;
            queueItem = new BarSubItem { Caption = "Queue" };

			workItem.UIExtensionSites[ExtensionSiteNames.File].Add(queueItem);
			workItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.Queue, queueItem);
			extensionSite = ExtensionSiteNames.Queue;
			workItem.UIExtensionSites[ExtensionSiteNames.ToolBar].Add(command1);
#endif
            workItem.Commands[acceptCustomerCaption].AddInvoker(command1, "ItemClick");
        }

        private void CreateBarButtonItem(string EdwardsIndexAppRibbonGroup, string Caption, Bitmap icon, ref BarButtonItem item, string ribbonPageName, string ribbonGroupName)
        {
            if (item == null)
            {
                item = new BarButtonItem
                {
                    Caption = Caption,
                    Hint = Caption,
                    Glyph = icon,
                    LargeGlyph = icon
                };
                //UIExtensionSites[ExtensionSiteNames.File].Add(item);
				createCommandButton(EdwardsIndexAppRibbonGroup, Caption, item, ribbonPageName, ribbonGroupName);
            }
        }

		private void AddCustomerMenuItem()
		{
#if !UseRibbonForm
			BarItem customerItem = new BarSubItem();
            customerItem.Caption = "Customer";
			workItem.UIExtensionSites[ExtensionSiteNames.File].Add(customerItem);
			workItem.UIExtensionSites.RegisterSite(Resources.CustomerMenuExtensionSite, customerItem);
#endif
		}
	}
}
