using StsService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;

namespace StsServiceHost
{
    public partial class StsService : ServiceBase
    {
        ServiceHost userProfileServiceHost;
        ServiceHost serviceHost;
        public StsService()
        {
            InitializeComponent();
        }


        protected static bool CreateDataStore(out bool forceCreateSchema)
        {
            forceCreateSchema = false;
            try
            {
                if (!Directory.Exists(@"C:\lbDMFManagerStsStore"))
                {
                    Directory.CreateDirectory(@"C:\lbDMFManagerStsStore");
                    forceCreateSchema = true;
                }

                DevExpress.Xpo.XpoDefault.DataLayer = DevExpress.Xpo.XpoDefault.GetDataLayer(

                //TODO: Fix Database file name.
                DevExpress.Xpo.DB.SQLiteConnectionProvider.GetConnectionString(Path.Combine(@"C:\lbDMFManagerStsStore", @"lbDMFManagerStsStore.db3")),

                DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            }
            catch (Exception e)
            {
                Trace.WriteLine("GetConnectionProvider could not get created: " + e.Message);
                return false;
            }

            return true;
        }

        protected override void OnStart(string[] args)
        {
            bool forceCreateSchema = false;
            if (!CreateDataStore(out forceCreateSchema))
            {
                Trace.WriteLine("Service could not get started.");
                throw new InvalidOperationException("Failed to open datastore");
            }

            if (forceCreateSchema)
            {

            }

            serviceHost = new ServiceHost(typeof(SecurityTokenService));
            serviceHost.Open();

            userProfileServiceHost = new ServiceHost(typeof(UserProfileService));
            userProfileServiceHost.Open();
		}

        protected override void OnStop()
        {
            if (serviceHost != null)
                serviceHost.Close();
				
            if (userProfileServiceHost != null)
                userProfileServiceHost.Close();
        }
    }
}
