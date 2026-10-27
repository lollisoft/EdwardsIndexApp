using System;
using DevExpress.Xpo;

namespace lbDMFManagerSecurity.Authentication
{
	public class ApprovedAudiences : XPLiteObject
	{
		public ApprovedAudiences() : base()
		{
			// This constructor is used when an object is loaded from a persistent storage. 
			// Do not place any code here. 
		}
		
		public ApprovedAudiences(Session session)
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
		/// The unique id of the ApprovedAudiences.
		/// </summary>
        [Key(true)]
        public int ID
        {
            get { return _id; }
            set { SetPropertyValue<int>("ID", ref _id, value); }
        }

		
		/// <summary>
		/// uri of ApprovedAudiences.
		/// </summary>
		[Persistent("uri")]
		[DisplayName("Uri")]
		public string _uri { get; set; }
			
		/// <summary>
		/// description of ApprovedAudiences.
		/// </summary>
		[Persistent("description")]
		[DisplayName("Description")]
		public string _description { get; set; }
			
		/// <summary>
		/// certificate_store_name of ApprovedAudiences.
		/// </summary>
		[Persistent("certificate_store_name")]
		[DisplayName("Certificate_store_name")]
		public string _certificate_store_name { get; set; }
			
		/// <summary>
		/// certificate_store_location of ApprovedAudiences.
		/// </summary>
		[Persistent("certificate_store_location")]
		[DisplayName("Certificate_store_location")]
		public string _certificate_store_location { get; set; }
			
		/// <summary>
		/// certificate_find_type of ApprovedAudiences.
		/// </summary>
		[Persistent("certificate_find_type")]
		[DisplayName("Certificate_find_type")]
		public string _certificate_find_type { get; set; }
			
		/// <summary>
		/// certificate_find_value of ApprovedAudiences.
		/// </summary>
		[Persistent("certificate_find_value")]
		[DisplayName("Certificate_find_value")]
		public string _certificate_find_value { get; set; }
			

		// Relation definitions (collection side) ApprovedAudiences
		[Association("ApprovedAudiences-SecurityUsers_ApprovedAudiences_ApprovedAudiences")]
		[DisplayName("SecurityUsers_ApprovedAudiences")]
		public XPCollection<SecurityUsers_ApprovedAudiences> SecurityUsers_ApprovedAudiences_ApprovedAudiences { get { return GetCollection<SecurityUsers_ApprovedAudiences>("SecurityUsers_ApprovedAudiences_ApprovedAudiences"); } }
		
		[Association("ApprovedAudiences-Applications_ApprovedAudiences_ApprovedAudiences")]
		[DisplayName("Applications_ApprovedAudiences")]
		public XPCollection<Applications_ApprovedAudiences> Applications_ApprovedAudiences_ApprovedAudiences { get { return GetCollection<Applications_ApprovedAudiences>("Applications_ApprovedAudiences_ApprovedAudiences"); } }
		

		// Relation definitions (single side)

	}
}
