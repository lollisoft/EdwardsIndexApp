
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Security;
using Microsoft.Practices.EnterpriseLibrary.Security.Configuration;
using System.Collections.Specialized;
using DevExpress.Xpo;

namespace EdwardsIndexAppSecurity.Authorization
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

        [Key(true)]
        public int ID
        {
            get { return _id; }
            set { SetPropertyValue<int>("ID", ref _id, value); }
        }

		[Persistent("name")]
		[DisplayName("Name")]
		public string _name { get; set; }
			
		[Persistent("vorname")]
		[DisplayName("Vorname")]
		public string _vorname { get; set; }
			
		[Persistent("userid")]
		[DisplayName("Userid")]
		public string _userid { get; set; }
			
		[Persistent("passwort")]
		[DisplayName("Passwort")]
		public string _passwort { get; set; }
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
            return collusers[index];;
		}

	}

	[ConfigurationElementType(typeof(CustomAuthorizationProviderData))]
	public class EdwardsIndexAppAuthorization : AuthorizationProvider
	{
        usersService usersService;

        public EdwardsIndexAppAuthorization(NameValueCollection attributes) 
        {
            usersService = new usersService();

            usersService.Load();
        }
		
        public override bool Authorize(System.Security.Principal.IPrincipal principal, string context)
        {
            bool authorized = false;
            string name = principal.Identity.Name;
            int count = usersService.Count();

            for (int i = 0; i < count; i++)
            {
				SecurityUsers user = usersService.GetByIndex(i);
				// Domain user must exist in users
				if (user._userid == name) 
                {
                    return true;
                }
            }

			return authorized;
        }
	}

}
