using System;
using DevExpress.Xpo;

namespace lbDMFManagerSecurity.Authentication
{
	public class Applications : XPLiteObject
	{
		public Applications() : base()
		{
			// This constructor is used when an object is loaded from a persistent storage. 
			// Do not place any code here. 
		}
		
		public Applications(Session session)
			: base(session)
		{
			// This constructor is used when an object is loaded from a persistent storage. 
			// Do not place any code here. 
		}

	    public override void AfterConstruction()
		{
			base.AfterConstruction();
			// Place here your initialization code. 
		}

        int _id;

		/// <summary>
		/// The unique id of the Applications.
		/// </summary>
        [Key(true)]
        public int ID
        {
            get { return _id; }
            set { SetPropertyValue<int>("ID", ref _id, value); }
        }

		
		/// <summary>
		/// name of Applications.
		/// </summary>
		[Persistent("name")]
		[DisplayName("Name")]
		public string _name { get; set; }
			
		/// <summary>
		/// applicationid of Applications.
		/// </summary>
		[Persistent("applicationid")]
		[DisplayName("Applicationid")]
		public string _applicationid { get; set; }
			
		/// <summary>
		/// enablerequestaccount of Applications.
		/// </summary>
		[Persistent("enablerequestaccount")]
		[DisplayName("Enablerequestaccount")]
		public bool _enablerequestaccount { get; set; }
			

		// Relation definitions (collection side) Applications
		[Association("Applications-Applications_ApprovedAudiences_Applications")]
		[DisplayName("Applications_ApprovedAudiences")]
		public XPCollection<Applications_ApprovedAudiences> Applications_ApprovedAudiences_Applications { get { return GetCollection<Applications_ApprovedAudiences>("Applications_ApprovedAudiences_Applications"); } }
		

		// Relation definitions (single side)

	}
	
	public class ApplicationsService
    {

        XPCollection<Applications> collApplications = new XPCollection<Applications>();
        public XPCollection<Applications> Load()
        {
            int AccountIDValue = 1;

            collApplications.DeleteObjectOnRemove = true;
            collApplications.Reload();

            if (collApplications.Count == 0)
            {
                //CreateMockData();
                collApplications.Reload();
                collApplications.Load();
            }

            return collApplications;
        }



        public ApplicationsService()
        {
            int AccountIDValue = 1;


            //collApplications.Load();
            //if (collApplications.Count == 0)
            //{

            //CreateMockData();

            //}

        }

        public void Add(Applications newApplications)
        {

        }

        public void Delete(Applications newApplications)
        {

        }

        public void Save(Applications newApplications)
        {

        }

        public int Count()
        {

            return collApplications.Count;

        }

        public Applications GetByIndex(int index)
        {

            return collApplications[index];
            ;

        }


    }

}
