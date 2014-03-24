using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTCCatholic.Models;
using System.Collections.ObjectModel;
using UTCCatholic.ViewModels;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;

namespace UTCCatholic.Controllers
{
	[RequireHttps]
	[Authorize]
    public class AdminController : Controller
    {
		AppDbContext db = new AppDbContext();

		// GET: /Admin/Carriers
		public async Task<ActionResult> Index()
		{
			if(User.IsInRole("Admin"))
			{
			/*
				var address = new MailAddress("awakening@utccatholic.org");
				var mail = new MailMessage();
				mail.From = address;
				mail.To.Add( "james0308@outlook.com" );


				mail.Subject = "This is an email";
				mail.Body = "This is from system.net.mail using C sharp with smtp authentication.";

				var client = new SmtpClient( "mail.utccatholic.org" );
				NetworkCredential Credentials = new NetworkCredential( "awakening@utccatholic.org", "WVhmPOZr7Otz5b6F" );
				client.Credentials = Credentials;
				client.Send( mail );
				return View();
		*/
				return View();
			}
			else
			{
				return HttpNotFound();
			}
		}
        // GET: /Admin/Carriers
        public async Task<ActionResult> Carriers()
        {
			if ( User.IsInRole( "Admin" ) )
			{
				return View();
			}
			else
			{
				return HttpNotFound();
			}
        }
		// GET: /Admin/Grades
		public async Task<ActionResult> Grades()
		{
			if ( User.IsInRole( "Admin" ) )
			{
				return View();
			}
			else
			{
				return HttpNotFound();
			}
		}
		// GET: /Admin/StaffChoices
		public async Task<ActionResult> StaffChoices()
		{
			if ( User.IsInRole( "Admin" ) )
			{
				return View();
			}
			else
			{
				return HttpNotFound();
			}
		}
		// GET: /Admin/LeadershipPositions
		public async Task<ActionResult> LeadershipPositions()
		{
			if ( User.IsInRole( "Admin" ) )
			{
				return View();
			}
			else
			{
				return HttpNotFound();
			}
		}
		// GET: /Admin/Users
		public async Task<ActionResult> Users()
		{
			if ( User.IsInRole( "Admin" ) )
			{
				return View();
			}
			else
			{
				return HttpNotFound();
			}
		}
		// GET: /Admin/Roles
		public async Task<ActionResult> Roles()
		{
			if ( User.IsInRole( "Admin" ) )
			{
				return View();
			}
			else
			{
				return HttpNotFound();
			}
		}
		// GET: /Admin/UserRoles
		public async Task<ActionResult> UserRoles()
		{
			if ( User.IsInRole( "Admin" ) )
			{
				return View();
			}
			else
			{
				return HttpNotFound();
			}
		}
		// GET: /Admin/UserRoles
		public async Task<ActionResult> Awakening()
		{
			if ( User.IsInRole( "Admin" ) )
			{
				Decimal expectedFees  = 0M;
				Decimal paidFees  = 0M;

				var users = await db.Users.ToListAsync();
				var usersView = new Collection<UserView>();
				foreach ( var user in users )
				{
					expectedFees += user.Fee;
					paidFees += user.FeePaid;
					usersView.Add( new UserView( user ) );
				}
				ViewBag.Staffers = usersView.Where( c => c.Staffer == true );
				ViewBag.StaffersPending = usersView.Where( c => c.StafferPending == true );
				ViewBag.Retreaters = usersView.Where( c => c.Retreater == true );
				ViewBag.RetreatersPending = usersView.Where( c => c.RetreaterPending == true );
				ViewBag.ExpectedFees = expectedFees;
				ViewBag.PaidFees = paidFees;
				return View();
			}
			else
			{
				return HttpNotFound();
			}
		}
		[HttpGet]
		public async Task<ActionResult> Families()
		{
			var families = await db.Families.ToListAsync();
			ICollection<FamilyView> Families = new Collection<FamilyView>();
			foreach(var family in families)
			{
				Families.Add(new FamilyView(family));
			}

			var users = await db.Users.ToListAsync();
			var Retreaters = new Collection<UserView>();
			foreach ( var user in users )
			{
				Boolean result;
				result = ( user.Retreater || user.RetreaterPending ) && user.Family == null;
				if ( result )
				{
					Retreaters.Add( new UserView( user ) );
				}
			}
			ViewBag.Retreaters = new SelectList( Retreaters, "RID", "Name" );

			return View(Families);
		}
		[HttpGet]
		public async Task<ActionResult> FamilyCreate( )
		{
			var users = await db.Users.ToListAsync();

			var Parents = new Collection<UserView>();
			var Gophers = new Collection<UserView>();

			foreach(var user in users)
			{
				Boolean result;
				result = user.Roles.Any( c => c.Role.Name.Equals( "Parent" ) );
				if ( result )
				{
					Parents.Add( new UserView(user) );
				}
				result = user.Roles.Any( c => c.Role.Name.Equals( "Gophers" ) );
				if ( result )
				{
					Gophers.Add( new UserView( user ) );
				}
			}

			ViewBag.Parents = new SelectList( Parents, "RID", "Name" );
			ViewBag.Gophers = new SelectList( Gophers, "RID", "Name" );
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> FamilyCreate(FamilyView Family)
		{
			if(!ModelState.IsValid)
			{
				return View(Family);
			}
			var fatherID = Family.Father.RID.ToString();
			var motherID = Family.Mother.RID.ToString();
			var gopherID = Family.Gopher.RID.ToString();
			var father = await db.Users.FirstOrDefaultAsync( c => c.Id == fatherID);
			var mother = await db.Users.FirstOrDefaultAsync( c => c.Id == motherID);
			var gopher = await db.Users.FirstOrDefaultAsync( c => c.Id == gopherID);
			if(father != null)
			{
				if(mother != null)
				{
					if(gopher != null)
					{
						var family = new Family(Family.Name, father, mother, gopher);
						father.Family = family;
						mother.Family = family;
						gopher.Family = family;
						db.Families.Add(family);
						await db.SaveChangesAsync();
					}
				}
			}

			var users = await db.Users.ToListAsync();
			var Retreaters = new Collection<UserView>();
			foreach ( var user in users )
			{
				Boolean result;
				result = ( user.Retreater || user.RetreaterPending ) && user.Family == null;
				if ( result )
				{
					Retreaters.Add( new UserView( user ) );
				}
			}
			ViewBag.Retreaters = new SelectList( Retreaters, "RID", "Name" );
			var families = await db.Families.ToListAsync();
			ICollection<FamilyView> Families = new Collection<FamilyView>();
			foreach ( var family in families )
			{
				Families.Add( new FamilyView( family ) );
			}
			return View( "Families", Families );
		}
		[HttpGet]
		public async Task<ActionResult> FamilyEdit( Guid FamilyID )
		{
			//Update Family
			var family = await db.Families.FirstOrDefaultAsync( c => c.RID == FamilyID );
			if ( family != null )
			{
				var users = await db.Users.ToListAsync();

				var Parents = new Collection<UserView>();
				var Gophers = new Collection<UserView>();

				foreach ( var user in users )
				{
					Boolean result;
					result = user.Roles.Any( c => c.Role.Name.Equals( "Parent" ) );
					if ( result )
					{
						Parents.Add( new UserView( user ) );
					}
					result = user.Roles.Any( c => c.Role.Name.Equals( "Gophers" ) );
					if ( result )
					{
						Gophers.Add( new UserView( user ) );
					}
				}

				ViewBag.Parents = new SelectList( Parents, "RID", "Name" );
				ViewBag.Gophers = new SelectList( Gophers, "RID", "Name" );
				return View( new FamilyView(family) );
			}
			else
			{
				return HttpNotFound();
			}
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> FamilyEdit( FamilyView Family )
		{
			if(!ModelState.IsValid)
			{
				return View(Family);
			}
			//Update Family
			var family = await db.Families.FirstOrDefaultAsync( c => c.RID == Family.RID );
			if ( family != null )
			{
				var fatherID = Family.Father.RID.ToString();
				var motherID = Family.Mother.RID.ToString();
				var gopherID = Family.Gopher.RID.ToString();
				var father = await db.Users.FirstOrDefaultAsync( c => c.Id == fatherID );
				var mother = await db.Users.FirstOrDefaultAsync( c => c.Id == motherID );
				var gopher = await db.Users.FirstOrDefaultAsync( c => c.Id == gopherID );
				if ( father != null )
				{
					if ( mother != null )
					{
						if ( gopher != null )
						{
							family.Father.Family = null;
							family.Mother.Family = null;
							family.Gopher.Family = null;
							
							family.Father = father;
							family.Mother = mother;
							family.Gopher = gopher;

							father.Family = family;
							mother.Family = family;
							gopher.Family = family;

							family.Name = Family.Name;
							db.Entry(family).State = EntityState.Modified;
							await db.SaveChangesAsync();

							var families = await db.Families.ToListAsync();
							ICollection<FamilyView> Families = new Collection<FamilyView>();
							foreach ( var f in families )
							{
								Families.Add( new FamilyView( f ) );
							}
							var users = await db.Users.ToListAsync();
							var Retreaters = new Collection<UserView>();
							foreach ( var user in users )
							{
								Boolean result;
								result = ( user.Retreater || user.RetreaterPending ) && user.Family == null;
								if ( result )
								{
									Retreaters.Add( new UserView( user ) );
								}
							}
							ViewBag.Retreaters = new SelectList( Retreaters, "RID", "Name" );
							return View( "Families", Families );
						}
					}
				}
			}
			return HttpNotFound();
		}
		/*
		[HttpGet]
		public async Task<ActionResult> AddChild( Guid FamilyID )
		{
			//Update Family
			var family = await db.Families.FirstOrDefaultAsync( c => c.RID == FamilyID );
			if ( family != null )
			{
				var users = await db.Users.ToListAsync();
				var Retreaters = new Collection<UserView>();
				foreach ( var user in users )
				{
					Boolean result;
					result = (user.Retreater || user.RetreaterPending) && user.Family == null;
					if ( result )
					{
						Retreaters.Add( new UserView( user ) );
					}
				}
				ViewBag.Retreaters = new SelectList( Retreaters, "RID", "Name" );
				return View( family );
			}
			return HttpNotFound();
		}
		*/
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddChild(Guid FamilyID, String ChildID)
		{
			//Add Child to Family
			var family = await db.Families.FirstOrDefaultAsync( c => c.RID == FamilyID );
			if(family != null)
			{
				var child = await db.Users.FirstOrDefaultAsync( c => c.Id == ChildID );
				if(child != null)
				{
					child.Family = family;
					family.Children.Add( child );
					await db.SaveChangesAsync();
				}
			}

			//Load All Families And Return To Index Page
			var families = db.Families.ToList();
			ICollection<FamilyView> Families = new Collection<FamilyView>();
			foreach ( var f in families )
			{
				Families.Add( new FamilyView( f ) );
			}

			var users = await db.Users.ToListAsync();
			var Retreaters = new Collection<UserView>();
			foreach ( var user in users )
			{
				Boolean result;
				result = ( user.Retreater || user.RetreaterPending ) && user.Family == null;
				if ( result )
				{
					Retreaters.Add( new UserView( user ) );
				}
			}
			ViewBag.Retreaters = new SelectList( Retreaters, "RID", "Name" );

			return View( "Families", Families );
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> RemoveChild( Guid FamilyID, String ChildID )
		{
			//Remove Child to Family
			var family = await db.Families.FirstOrDefaultAsync( c => c.RID == FamilyID );
			if ( family != null )
			{
				var child = await db.Users.FirstOrDefaultAsync( c => c.Id == ChildID );
				if ( child != null )
				{
					child.Family = null;
					family.Children.Remove(child);
					await db.SaveChangesAsync();
				}
			}

			//Load All Families And Return To Index Page
			var families = db.Families.ToList();
			ICollection<FamilyView> Families = new Collection<FamilyView>();
			foreach ( var f in families )
			{
				Families.Add( new FamilyView( f ) );
			}

			var users = await db.Users.ToListAsync();
			var Retreaters = new Collection<UserView>();
			foreach ( var user in users )
			{
				Boolean result;
				result = ( user.Retreater || user.RetreaterPending ) && user.Family == null;
				if ( result )
				{
					Retreaters.Add( new UserView( user ) );
				}
			}
			ViewBag.Retreaters = new SelectList( Retreaters, "RID", "Name" );

			return View( "Families", Families );
		}

		public async Task<ActionResult> RetreaterFees()
		{
			var users = await db.Users.ToListAsync();
			ICollection<UserView> Users = new Collection<UserView>();

			foreach(var user in users)
			{
				if(user.RetreaterPending || user.Retreater)
					Users.Add(new UserView(user));
			}
			var OrderedUsers = Users.OrderBy(c=>c.ApplicationStamp);

			return View( OrderedUsers );
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<ActionResult> CheckFees(String ChildID)
		{
			
			var user = await db.Users.FirstOrDefaultAsync( c => c.Id == ChildID);
			if(user != null )
			{
				return HttpNotFound();
			}
			else
			{
				user.FeePaid = user.Fee;
				db.Entry(user).State = EntityState.Modified;
				await db.SaveChangesAsync();
				ViewBag.Message = user.Personal.FirstName + " " + user.Personal.LastName + "'s fee has been paid.";

				var users = await db.Users.ToListAsync();
				ICollection<UserView> Users = new Collection<UserView>();

				foreach ( var u in users )
				{
					if ( user.RetreaterPending || user.Retreater )
						Users.Add( new UserView( u ) );
				}
				var OrderedUsers = Users.OrderBy( c => c.ApplicationStamp );


				return View( "RetreaterFees", OrderedUsers );
			}
		}
    }

}