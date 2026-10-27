using System;
using DevExpress.Xpo;

namespace lbDMFManagerSecurity.Authentication
{
	public class SecurityUsers_ApprovedAudiences : XPLiteObject
	{
		public SecurityUsers_ApprovedAudiences() : base()
		{
			// This constructor is used when an object is loaded from a persistent storage. 
			// Do not place any code here. 
		}
		
		public SecurityUsers_ApprovedAudiences(Session session)
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
		/// The unique id of the SecurityUsers_ApprovedAudiences.
		/// </summary>
        [Key(true)]
        public int ID
        {
            get { return _id; }
            set { SetPropertyValue<int>("ID", ref _id, value); }
        }

		
		/// <summary>
		/// name of SecurityUsers_ApprovedAudiences.
		/// </summary>
		[Persistent("name")]
		[DisplayName("Name")]
		public string _name { get; set; }
			

		// Relation definitions (collection side) SecurityUsers_ApprovedAudiences

		// Relation definitions (single side)

		private SecurityUsers _SecurityUsers;
		[Association("SecurityUsers-SecurityUsers_ApprovedAudiences_SecurityUsers")]
		[DisplayName("SecurityUsers")]
		public SecurityUsers SecurityUsers {
		get { return _SecurityUsers; }
		set { SetPropertyValue("SecurityUsers", ref _SecurityUsers, value); }
		}
	
		private ApprovedAudiences _ApprovedAudiences;
		[Association("ApprovedAudiences-SecurityUsers_ApprovedAudiences_ApprovedAudiences")]
		[DisplayName("ApprovedAudiences")]
		public ApprovedAudiences ApprovedAudiences {
		get { return _ApprovedAudiences; }
		set { SetPropertyValue("ApprovedAudiences", ref _ApprovedAudiences, value); }
		}
	
	}
}
