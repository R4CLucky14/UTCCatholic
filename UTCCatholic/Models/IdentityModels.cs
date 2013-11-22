using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UTCCatholic.Models
{
	public abstract class DatabaseObject
	{
		[Key]
		public Guid RID { get; set; }
		public virtual bool Removeable { get { return true; } }

		public DatabaseObject()
		{
			this.RID = Guid.NewGuid();
		}
	}

    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppUser : IdentityUser
    {
		/*
		 * A User can have many things in this case. But let's limit it down. Start with a basic profile.
		 * 
		 * 
		 */

		//Arrange Information into: Personal, School, Medical, Religious, Misc.
		public Personal Personal { get; set; }
		public School School { get; set; }
		public Medical Medical { get; set; }
		public Religion Religion { get; set; }
		public Misc Misc { get; set; }
		public ICollection<Retreat> Retreats { get; set; }

		public AppUser( string userName )
			: base ( userName )
		{

		}
    }

    public class AppDbContext : IdentityDbContext<AppUser>
    {

    }
}