using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace UserManagementUiDemo.Models.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string FullName { get; set; }
		public ICollection<IdentityUserClaim<string>> UserClaims { get; set; }
	}
}