using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lbDMFManagerSecurity.Authentication
{
    public class SecurityUsers : XPLiteObject
	{
		public SecurityUsers() : base()
		{
			// This constructor is used when an object is loaded from a persistent storage. 
			// Do not place any code here. 
		}
		
		public SecurityUsers(Session session)
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
		/// The unique id of the SecurityUsers.
		/// </summary>
        [Key(true)]
        public int ID
        {
            get { return _id; }
            set { SetPropertyValue<int>("ID", ref _id, value); }
        }

		
		/// <summary>
		/// name of SecurityUsers.
		/// </summary>
		[Persistent("name")]
		[DisplayName("Name")]
		public string _name { get; set; }
			
		/// <summary>
		/// vorname of SecurityUsers.
		/// </summary>
		[Persistent("vorname")]
		[DisplayName("Vorname")]
		public string _vorname { get; set; }

        /// <summary>
        /// userid of SecurityUsers.
        /// </summary>
        [Persistent("userid")]
        [DisplayName("Userid")]
        public string _userid { get; set; }

        /// <summary>
        /// userid of SecurityUsers.
        /// </summary>
        [Persistent("email")]
        [DisplayName("Email")]
        public string _email { get; set; }

        /// <summary>
        /// userid of SecurityUsers.
        /// </summary>
        [Persistent("passwordreset")]
        [DisplayName("Passwordreset")]
        public DateTime _passwordreset
        {
            get;
            set;
        }

        /// <summary>
        /// userid of SecurityUsers.
        /// </summary>
        [Persistent("passwordresettoken")]
        [DisplayName("Passwordresettoken")]
        public string _passwordresettoken
        {
            get;
            set;
        }

        /// <summary>
		/// passwort of SecurityUsers.
		/// </summary>
		[Persistent("passwort")]
		[DisplayName("Passwort")]
		public string _passwort { get; set; }
			

		// Relation definitions (collection side) SecurityUsers
		[Association("SecurityUsers-SecurityUsers_ApprovedAudiences_SecurityUsers")]
		[DisplayName("SecurityUsers_ApprovedAudiences")]
		public XPCollection<SecurityUsers_ApprovedAudiences> SecurityUsers_ApprovedAudiences_SecurityUsers { get { return GetCollection<SecurityUsers_ApprovedAudiences>("SecurityUsers_ApprovedAudiences_SecurityUsers"); } }
		

		// Relation definitions (single side)

	}


    public class usersService
    {
        XPCollection<SecurityUsers> collusers = new XPCollection<SecurityUsers>();
        public XPCollection<SecurityUsers> Load()
        {
            int AccountIDValue = 1;

            collusers.DeleteObjectOnRemove = true;
            collusers.Reload();

            if (collusers.Count == 0)
            {
                SecurityUsers defaultAdminUser = new SecurityUsers();
                defaultAdminUser._name = "System";
                defaultAdminUser._vorname = "Administrator";
                defaultAdminUser._userid = "admin";
                //TODO: Define your products secret default password.
                defaultAdminUser._passwort = "";

                defaultAdminUser.Save();

                collusers.Reload();
                collusers.Load();
            }

            return collusers;
        }

        public usersService()
        {
            int AccountIDValue = 1;

            collusers.Load();
        }

        public int Count()
        {
            return collusers.Count;
        }

        public SecurityUsers GetByIndex(int index)
        {
            return collusers[index];
            ;
        }

    }
}
