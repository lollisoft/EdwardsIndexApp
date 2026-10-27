using System;
using DevExpress.Xpo;

namespace lbDMFManagerSecurity.Authentication
{
	public class Applications_ApprovedAudiences : XPLiteObject
	{
		public Applications_ApprovedAudiences() : base()
		{
			// This constructor is used when an object is loaded from a persistent storage. 
			// Do not place any code here. 
		}
		
		public Applications_ApprovedAudiences(Session session)
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
		/// The unique id of the Applications_ApprovedAudiences.
		/// </summary>
        [Key(true)]
        public int ID
        {
            get { return _id; }
            set { SetPropertyValue<int>("ID", ref _id, value); }
        }

		
		/// <summary>
		/// name of Applications_ApprovedAudiences.
		/// </summary>
		[Persistent("name")]
		[DisplayName("Name")]
		public string _name { get; set; }
			

		// Relation definitions (collection side) Applications_ApprovedAudiences

		// Relation definitions (single side)

		private ApprovedAudiences _ApprovedAudiences;
		[Association("ApprovedAudiences-Applications_ApprovedAudiences_ApprovedAudiences")]
		[DisplayName("ApprovedAudiences")]
		public ApprovedAudiences ApprovedAudiences {
		get { return _ApprovedAudiences; }
		set { SetPropertyValue("ApprovedAudiences", ref _ApprovedAudiences, value); }
		}
	
		private Applications _Applications;
		[Association("Applications-Applications_ApprovedAudiences_Applications")]
		[DisplayName("Applications")]
		public Applications Applications {
		get { return _Applications; }
		set { SetPropertyValue("Applications", ref _Applications, value); }
		}
	
	}
}
