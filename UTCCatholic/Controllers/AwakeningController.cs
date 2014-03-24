using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using UTCCatholic.Models;
using UTCCatholic.ViewModels;
using System.Collections.ObjectModel;
using UTCCatholic.InputModels;
using System.Data.Entity.Validation;
using System.Net.Mail;
using System.Net;

namespace UTCCatholic.Controllers
{
	/// <summary>
	/// Handles CRUD actions for the Awakening section.
	/// </summary>
	[RequireHttps]
    public class AwakeningController : Controller
    {
		public AppDbContext db = new AppDbContext();

		MailMessage Mail = new MailMessage();
		MailAddress From = new MailAddress( "awakening@utccatholic.org" );
		SmtpClient client = new SmtpClient( "mail.utccatholic.org" );
		NetworkCredential Credentials = new NetworkCredential( "awakening@utccatholic.org", "WVhmPOZr7Otz5b6F" );

		public AwakeningController()
			: base()
		{
		}
		public async Task<ViewResult> Index()
        {
			var name = HttpContext.User.Identity.Name;
			var User = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );
			if(User!=null)
			{
				var userView = new UserInput( User );
				return View( userView );
			}
			return View();
        }
		[Authorize]
		public async Task<ActionResult> Retreaters()
		{
			var name = HttpContext.User.Identity.Name;
			var User = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );
			var userView = new UserInput( User );

			var carriers = await db.Carriers.OrderBy( c => c.Title ).ToListAsync<Carrier>();
			var carriersView = new Collection<CarrierView>();
			foreach ( var carrier in carriers )
			{
				carriersView.Add( new CarrierView( carrier ) );
			}
			var tshirtsizes = await db.TShirtSizes.OrderBy( c => c.Number ).ToListAsync<TShirtSize>();
			var tshirtsizesView = new Collection<TShirtSizeView>();
			foreach ( var size in tshirtsizes )
			{
				tshirtsizesView.Add( new TShirtSizeView( size ) );
			}
			var grades = await db.Grades.OrderBy( c => c.Number ).ToListAsync<Grade>();
			var gradesView = new Collection<GradeView>();
			foreach ( var grade in grades )
			{
				gradesView.Add( new GradeView( grade ) );
			}
			var retreatersPending = await db.Users.CountAsync( c => c.RetreaterPending == true);
			var retreaters = await db.Users.CountAsync( c => c.Retreater == true);



			ViewBag.Grades = new SelectList(gradesView, "RID", "Title");
			ViewBag.Sizes =  new SelectList(tshirtsizesView, "RID", "Size");
			ViewBag.Carriers = new SelectList( carriersView, "RID", "Title" );
			ViewBag.RetreatersCount = retreatersPending + retreaters;
			return View( "Retreaters", userView );
		}
		[Authorize]
		public async Task<ActionResult> Staffers()
		{
			var name = HttpContext.User.Identity.Name;
			var User = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );
			var userView = new UserInput( User );

			var carriers = await db.Carriers.OrderBy( c => c.Title ).ToListAsync<Carrier>();
			var carriersView = new Collection<CarrierView>();
			foreach ( var carrier in carriers )
			{
				carriersView.Add( new CarrierView( carrier ) );
			}
			var tshirtsizes = await db.TShirtSizes.OrderBy( c => c.Number ).ToListAsync<TShirtSize>();
			var tshirtsizesView = new Collection<TShirtSizeView>();
			foreach ( var size in tshirtsizes )
			{
				tshirtsizesView.Add( new TShirtSizeView( size ) );
			}
			var grades = await db.Grades.OrderBy( c => c.Number ).ToListAsync<Grade>();
			var gradesView = new Collection<GradeView>();
			foreach ( var grade in grades )
			{
				gradesView.Add( new GradeView( grade ) );
			}
			var choices = await db.StaffChoices.OrderBy( c => c.Title ).ToListAsync<StaffChoice>();
			var choicesView = new Collection<StaffChoiceView>();
			foreach ( var choice in choices )
			{
				choicesView.Add( new StaffChoiceView( choice ) );
			}


			ViewBag.Choices = new SelectList( choicesView, "RID", "Title" );
			ViewBag.Grades = new SelectList( gradesView, "RID", "Title" );
			ViewBag.Sizes =  new SelectList( tshirtsizesView, "RID", "Size" );
			ViewBag.Carriers = new SelectList( carriersView, "RID", "Title" );

			return View( "Staffers", userView );
		}
		/// <summary>
		/// Handles a Retreater Application Submittal.
		/// </summary>
		/// <param name="model"></param>
		/// <returns>Goes back to the main Retreaters View</returns>
		[Authorize]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<ActionResult> RetreatersPost( UserInput model )
		{
			var carriers = await db.Carriers.OrderBy( c => c.Title ).ToListAsync<Carrier>();
			var carriersView = new Collection<CarrierView>();
			foreach ( var carrier in carriers )
			{
				carriersView.Add( new CarrierView( carrier ) );
			}
			var tshirtsizes = await db.TShirtSizes.OrderBy( c => c.Number ).ToListAsync<TShirtSize>();
			var tshirtsizesView = new Collection<TShirtSizeView>();
			foreach ( var size in tshirtsizes )
			{
				tshirtsizesView.Add( new TShirtSizeView( size ) );
			}
			var grades = await db.Grades.OrderBy( c => c.Number ).ToListAsync<Grade>();
			var gradesView = new Collection<GradeView>();
			foreach ( var grade in grades )
			{
				gradesView.Add( new GradeView( grade ) );
			}


			ViewBag.Grades = new SelectList( gradesView, "RID", "Title" );
			ViewBag.Sizes =  new SelectList( tshirtsizesView, "RID", "Size" );
			ViewBag.Carriers = new SelectList( carriersView, "RID", "Title" );

			if ( ModelState.IsValid )
			{
				var name = HttpContext.User.Identity.Name;
				var User = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );
				if(User != null)
				{
					if ( User.Retreater )
					{
						return HttpNotFound();
					}
					else if ( User.Staffer )
					{
						return HttpNotFound();
					}
					else if ( User.RetreaterPending )
					{
						return HttpNotFound();
					}
					else if ( User.StafferPending )
					{
						return HttpNotFound();
					}
					User.RetreaterPending = true;
					if ( db.Users.Count( c => c.RetreaterPending == true ) + db.Users.Count( c => c.Retreater == true ) > 20 )
					{
						User.Fee = 30M;
					}
					else
					{
						User.Fee = 10M;
					}

					User.ApplicationStamp = DateTime.Now;
					User.Personal.FirstName = model.Personal.FirstName;
					User.Personal.LastName = model.Personal.LastName;
					User.Personal.Email = model.Personal.Email;
					User.Personal.Phone = new Models.Phone();
					User.Personal.Phone.Number = model.Personal.Phone.Number;
					User.Personal.Phone.Carrier = await db.Carriers.FindAsync( model.Personal.Phone.Carrier );
					User.Personal.Hobbies = model.Personal.Hobbies;
					User.Personal.TShirtSize = await db.TShirtSizes.FindAsync( model.Personal.TShirtSize );

					User.Religion.FavoriteBibleVerse = model.Religion.FavoriteBibleVerse;
					User.Religion.FavoriteSaint = model.Religion.FavoriteSaint;
					User.Religion.Reason = model.Religion.Reason;
					User.Religion.RoleModel = model.Religion.RoleModel;
					User.Religion.PrayerRequest = model.Religion.PrayerRequest;

					User.School.College = model.School.College;
					User.School.Grade = await db.Grades.FindAsync( model.School.Grade );

					User.Medical.Allergies = model.Medical.Allergies;
					User.Medical.HealthConditions = model.Medical.HealthConditions;

					User.Emergency.FirstName = model.Emergency.FirstName;
					User.Emergency.LastName = model.Emergency.LastName;
					User.Emergency.Email = model.Emergency.Email;
					User.Emergency.Phone = new Models.Phone();
					User.Emergency.Phone.Number = model.Emergency.Phone.Number;
					User.Emergency.Phone.Carrier = await db.Carriers.FindAsync( model.Emergency.Phone.Carrier );

					User.Transportation.TransportSelf = model.Transportation.TransportSelf;

					await db.SaveChangesAsync();
					var userView = new UserInput( User );

					this.Mail.From = this.From;
					this.Mail.Subject = "ChattAwakening Retreater Application Confirmation";
					this.Mail.Body = User.Personal.FirstName + ", <br /><br />We are emailing you to confirm your application, and that we have received it. If you would like to review, edit, or delete your application at any time, please go to <a href=\"http://utccatholic.org/Awakening/Retreaters\">http://utccatholic.org/Awakening/Retreaters</a>. <br /> Please place our address in your contact list so we are not placed in your spam folder! We will be emailing you as we get closer to March 28th!<br /> If you have any questions, comments, and/or concerns, please email us back! <br /> <br /> Thank you, <br />ChattAwakening - UTC Catholic";
					this.Mail.IsBodyHtml = true;
					this.Mail.To.Add( User.Personal.Email );
					this.Mail.Bcc.Add( "mcgraw.m201294@yahoo.com, mollygallagher26@gmail.com, james0308@outlook.com" );
					client.Credentials = Credentials;
					client.Send( this.Mail );


					return View( "Retreaters", userView );
				}
				else
				{
					return HttpNotFound();
				}
			}
			else
			{
				return View( "Retreaters", model );
			}
		}
		/// <summary>
		/// Handles a Staffer Application Submittal
		/// </summary>
		/// <param name="model"></param>
		/// <returns>Goes back to the main Staffers View</returns>
		[Authorize]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<ActionResult> StaffersPost( UserInput model )
		{
			var carriers = await db.Carriers.OrderBy( c => c.Title ).ToListAsync<Carrier>();
			var carriersView = new Collection<CarrierView>();
			foreach ( var carrier in carriers )
			{
				carriersView.Add( new CarrierView( carrier ) );
			}
			var tshirtsizes = await db.TShirtSizes.OrderBy( c => c.Number ).ToListAsync<TShirtSize>();
			var tshirtsizesView = new Collection<TShirtSizeView>();
			foreach ( var size in tshirtsizes )
			{
				tshirtsizesView.Add( new TShirtSizeView( size ) );
			}
			var grades = await db.Grades.OrderBy( c => c.Number ).ToListAsync<Grade>();
			var gradesView = new Collection<GradeView>();
			foreach ( var grade in grades )
			{
				gradesView.Add( new GradeView( grade ) );
			}
			var choices = await db.StaffChoices.OrderBy( c => c.Title ).ToListAsync<StaffChoice>();
			var choicesView = new Collection<StaffChoiceView>();
			foreach ( var choice in choices )
			{
				choicesView.Add( new StaffChoiceView( choice ) );
			}


			ViewBag.Choices = new SelectList( choicesView, "RID", "Title" );
			ViewBag.Grades = new SelectList( gradesView, "RID", "Title" );
			ViewBag.Sizes =  new SelectList( tshirtsizesView, "RID", "Size" );
			ViewBag.Carriers = new SelectList( carriersView, "RID", "Title" );

			if ( ModelState.IsValid )
			{
				var name = HttpContext.User.Identity.Name;
				var User = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );
				if ( User != null )
				{
					if ( User.Retreater )
					{
						return HttpNotFound();
					}
					else if ( User.Staffer )
					{
						return HttpNotFound();
					}
					else if ( User.RetreaterPending )
					{
						return HttpNotFound();
					}
					else if ( User.StafferPending )
					{
						return HttpNotFound();
					}
					User.StafferPending = true;
					User.Fee = 35M;

					User.ApplicationStamp = DateTime.Now;
					User.Personal.FirstName = model.Personal.FirstName;
					User.Personal.LastName = model.Personal.LastName;
					User.Personal.Email = model.Personal.Email;
					User.Personal.Phone = new Models.Phone();
					User.Personal.Phone.Number = model.Personal.Phone.Number;
					User.Personal.Phone.Carrier = await db.Carriers.FindAsync( model.Personal.Phone.Carrier );
					User.Personal.Hobbies = model.Personal.Hobbies;
					User.Personal.TShirtSize = await db.TShirtSizes.FindAsync( model.Personal.TShirtSize );

					User.Staff.PreviousAttended = model.Staff.PreviousAttended;
					User.Staff.PreviousPositions = model.Staff.PreviousPositions;
					User.Staff.PreviousTalk = model.Staff.PreviousTalk;
					User.Staff.InterestedTalk = model.Staff.InterestedTalk;
					User.Staff.LeadershipPosition = model.Staff.LeadershipPosition;
					User.Staff.FirstChoice = await db.StaffChoices.FindAsync( model.Staff.FirstChoice );
					User.Staff.SecondChoice = await db.StaffChoices.FindAsync( model.Staff.SecondChoice );
					User.Staff.ThirdChoice = await db.StaffChoices.FindAsync( model.Staff.ThirdChoice );
					User.Staff.AdditionalComments = model.Staff.AdditionalComments;

					User.School.College = model.School.College;
					User.School.Grade = await db.Grades.FindAsync( model.School.Grade );

					User.Medical.Allergies = model.Medical.Allergies;
					User.Medical.HealthConditions = model.Medical.HealthConditions;

					User.Emergency.FirstName = model.Emergency.FirstName;
					User.Emergency.LastName = model.Emergency.LastName;
					User.Emergency.Email = model.Emergency.Email;
					User.Emergency.Phone = new Models.Phone();
					User.Emergency.Phone.Number = model.Emergency.Phone.Number;
					User.Emergency.Phone.Carrier = await db.Carriers.FindAsync( model.Emergency.Phone.Carrier );

					User.Transportation.TransportSelf = model.Transportation.TransportSelf;

					await db.SaveChangesAsync();
					var userView = new UserInput( User );

					this.Mail.From = this.From;
					this.Mail.Subject = "ChattAwakening Staffer Application Confirmation";
					this.Mail.Body = User.Personal.FirstName + ", <br /><br />We are emailing you to confirm your application, and that we have received it. If you would like to review, edit, or delete your application at any time, please go to <a href=\"http://utccatholic.org/Awakening/Staffers\">http://utccatholic.org/Awakening/Staffers</a>. <br />Please place our address in your contact list so we are not placed in your spam folder! We will be emailing you as we get closer to March 28th! <br /> If you have any questions, comments, and/or concerns, please email us back!<br /> <br /> Thank you, <br />ChattAwakening - UTC Catholic";
					this.Mail.IsBodyHtml = true;
					this.Mail.To.Add( User.Personal.Email );
					this.Mail.Bcc.Add( "mcgraw.m201294@yahoo.com, mollygallagher26@gmail.com, james0308@outlook.com" );
					client.Credentials = Credentials;
					client.Send( this.Mail );


					return View( "Staffers", userView );
				}
				else
				{
					return HttpNotFound();
				}
			}
			else
			{
				return View( "Staffers", model );
			}
		}
		/// <summary>
		/// Handles a Retreater Application Edit
		/// </summary>
		/// <param name="model"></param>
		/// <returns>Goes back to the main Retreaters View</returns>
		[Authorize]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<ActionResult> RetreatersEdit( UserInput model )
		{
			var carriers = await db.Carriers.OrderBy( c => c.Title ).ToListAsync<Carrier>();
			var carriersView = new Collection<CarrierView>();
			foreach ( var carrier in carriers )
			{
				carriersView.Add( new CarrierView( carrier ) );
			}
			var tshirtsizes = await db.TShirtSizes.OrderBy( c => c.Number ).ToListAsync<TShirtSize>();
			var tshirtsizesView = new Collection<TShirtSizeView>();
			foreach ( var size in tshirtsizes )
			{
				tshirtsizesView.Add( new TShirtSizeView( size ) );
			}
			var grades = await db.Grades.OrderBy( c => c.Number ).ToListAsync<Grade>();
			var gradesView = new Collection<GradeView>();
			foreach ( var grade in grades )
			{
				gradesView.Add( new GradeView( grade ) );
			}


			ViewBag.Grades = new SelectList( gradesView, "RID", "Title" );
			ViewBag.Sizes =  new SelectList( tshirtsizesView, "RID", "Size" );
			ViewBag.Carriers = new SelectList( carriersView, "RID", "Title" );


			if ( ModelState.IsValid )
			{
				var name = HttpContext.User.Identity.Name;
				var User = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );
				if ( User != null )
				{
					if ( User.Staffer )
					{
						return HttpNotFound();
					}
					else if ( User.StafferPending )
					{
						return HttpNotFound();
					}
					User.RetreaterPending = true;

					User.Personal.FirstName = model.Personal.FirstName;
					User.Personal.LastName = model.Personal.LastName;
					User.Personal.Email = model.Personal.Email;
					User.Personal.Phone = new Models.Phone();
					User.Personal.Phone.Number = model.Personal.Phone.Number;
					User.Personal.Phone.Carrier = await db.Carriers.FindAsync( model.Personal.Phone.Carrier );
					User.Personal.Hobbies = model.Personal.Hobbies;
					User.Personal.TShirtSize = await db.TShirtSizes.FindAsync( model.Personal.TShirtSize );

					User.Religion.FavoriteBibleVerse = model.Religion.FavoriteBibleVerse;
					User.Religion.FavoriteSaint = model.Religion.FavoriteSaint;
					User.Religion.Reason = model.Religion.Reason;
					User.Religion.RoleModel = model.Religion.RoleModel;
					User.Religion.PrayerRequest = model.Religion.PrayerRequest;

					User.School.College = model.School.College;
					User.School.Grade = await db.Grades.FindAsync( model.School.Grade );

					User.Medical.Allergies = model.Medical.Allergies;
					User.Medical.HealthConditions = model.Medical.HealthConditions;

					User.Emergency.FirstName = model.Emergency.FirstName;
					User.Emergency.LastName = model.Emergency.LastName;
					User.Emergency.Email = model.Emergency.Email;
					User.Emergency.Phone = new Models.Phone();
					User.Emergency.Phone.Number = model.Emergency.Phone.Number;
					User.Emergency.Phone.Carrier = await db.Carriers.FindAsync( model.Emergency.Phone.Carrier );

					User.Transportation.TransportSelf = model.Transportation.TransportSelf;

					await db.SaveChangesAsync();
					var userView = new UserInput( User );

					this.Mail.From = this.From;
					this.Mail.Subject = "ChattAwakening Retreater Application Edit";
					this.Mail.Body = User.Personal.FirstName + ", <br /><br />We are emailing you to confirm that you have edited your application.  If you would like to furthur review, edit, or delete your application at any time, please go to<a href=\"http://utccatholic.org/Awakening/Retreaters\">http://utccatholic.org/Awakening/Retreaters</a>. <br />Please place our address in your contact list so we are not placed in your spam folder! We will be emailing you as we get closer to March 28th! <br /> If you have any questions, comments, and/or concerns, please email us back!<br /> <br /> Thank you, <br />ChattAwakening - UTC Catholic";
					this.Mail.IsBodyHtml = true;
					this.Mail.To.Add( User.Personal.Email );
					this.Mail.Bcc.Add( "mcgraw.m201294@yahoo.com, mollygallagher26@gmail.com, james0308@outlook.com" );
					client.Credentials = Credentials;
					client.Send( this.Mail );

					return View( "Retreaters", userView );
				}
				else
				{
					return HttpNotFound();
				}
			}
			else
			{
				return View( "Retreaters", model );
			}
		}
		/// <summary>
		/// Handles a Staffer Application Edit
		/// </summary>
		/// <param name="model"></param>
		/// <returns>Goes back to the main Staffers View</returns>
		[Authorize]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<ActionResult> StaffersEdit( UserInput model )
		{
			var carriers = await db.Carriers.OrderBy( c => c.Title ).ToListAsync<Carrier>();
			var carriersView = new Collection<CarrierView>();
			foreach ( var carrier in carriers )
			{
				carriersView.Add( new CarrierView( carrier ) );
			}
			var tshirtsizes = await db.TShirtSizes.OrderBy( c => c.Number ).ToListAsync<TShirtSize>();
			var tshirtsizesView = new Collection<TShirtSizeView>();
			foreach ( var size in tshirtsizes )
			{
				tshirtsizesView.Add( new TShirtSizeView( size ) );
			}
			var grades = await db.Grades.OrderBy( c => c.Number ).ToListAsync<Grade>();
			var gradesView = new Collection<GradeView>();
			foreach ( var grade in grades )
			{
				gradesView.Add( new GradeView( grade ) );
			}
			var choices = await db.StaffChoices.OrderBy( c => c.Title ).ToListAsync<StaffChoice>();
			var choicesView = new Collection<StaffChoiceView>();
			foreach ( var choice in choices )
			{
				choicesView.Add( new StaffChoiceView( choice ) );
			}


			ViewBag.Choices = new SelectList( choicesView, "RID", "Title" );
			ViewBag.Grades = new SelectList( gradesView, "RID", "Title" );
			ViewBag.Sizes =  new SelectList( tshirtsizesView, "RID", "Size" );
			ViewBag.Carriers = new SelectList( carriersView, "RID", "Title" );



			if ( ModelState.IsValid )
			{
				var name = HttpContext.User.Identity.Name;
				var User = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );
				if ( User != null )
				{
					if ( User.Retreater )
					{
						return HttpNotFound();
					}
					else if ( User.RetreaterPending )
					{
						return HttpNotFound();
					}
					User.StafferPending = true;

					User.Personal.FirstName = model.Personal.FirstName;
					User.Personal.LastName = model.Personal.LastName;
					User.Personal.Email = model.Personal.Email;
					User.Personal.Phone = new Models.Phone();
					User.Personal.Phone.Number = model.Personal.Phone.Number;
					User.Personal.Phone.Carrier = await db.Carriers.FindAsync( model.Personal.Phone.Carrier );
					User.Personal.Hobbies = model.Personal.Hobbies;
					User.Personal.TShirtSize = await db.TShirtSizes.FindAsync( model.Personal.TShirtSize );

					User.Staff.PreviousAttended = model.Staff.PreviousAttended;
					User.Staff.PreviousPositions = model.Staff.PreviousPositions;
					User.Staff.PreviousTalk = model.Staff.PreviousTalk;
					User.Staff.InterestedTalk = model.Staff.InterestedTalk;
					User.Staff.LeadershipPosition = model.Staff.LeadershipPosition;
					User.Staff.FirstChoice = await db.StaffChoices.FindAsync( model.Staff.FirstChoice );
					User.Staff.SecondChoice = await db.StaffChoices.FindAsync( model.Staff.SecondChoice );
					User.Staff.ThirdChoice = await db.StaffChoices.FindAsync( model.Staff.ThirdChoice );
					User.Staff.AdditionalComments = model.Staff.AdditionalComments;

					User.School.College = model.School.College;
					User.School.Grade = await db.Grades.FindAsync( model.School.Grade );

					User.Medical.Allergies = model.Medical.Allergies;
					User.Medical.HealthConditions = model.Medical.HealthConditions;

					User.Emergency.FirstName = model.Emergency.FirstName;
					User.Emergency.LastName = model.Emergency.LastName;
					User.Emergency.Email = model.Emergency.Email;
					User.Emergency.Phone = new Models.Phone();
					User.Emergency.Phone.Number = model.Emergency.Phone.Number;
					User.Emergency.Phone.Carrier = await db.Carriers.FindAsync( model.Emergency.Phone.Carrier );

					User.Transportation.TransportSelf = model.Transportation.TransportSelf;

					await db.SaveChangesAsync();
					var userView = new UserInput( User );

					this.Mail.From = this.From;
					this.Mail.Subject = "ChattAwakening Staffer Application Edit";
					this.Mail.Body = User.Personal.FirstName + ", <br /><br />We are emailing you to confirm that you have edited your application.  If you would like to furthur review, edit, or delete your application at any time, please go to <a href=\"http://utccatholic.org/Awakening/Staffers\">http://utccatholic.org/Awakening/Staffers</a>. <br />Please place our address in your contact list so we are not placed in your spam folder! We will be emailing you as we get closer to March 28th! <br /> If you have any questions, comments, and/or concerns, please email us back!<br /> <br /> Thank you, <br />ChattAwakening - UTC Catholic";
					this.Mail.IsBodyHtml = true;
					this.Mail.To.Add( User.Personal.Email );
					this.Mail.Bcc.Add( "mcgraw.m201294@yahoo.com, mollygallagher26@gmail.com, james0308@outlook.com" );
					client.Credentials = Credentials;
					client.Send( this.Mail );


					return View( "Staffers", userView );
				}
				else
				{
					return HttpNotFound();
				}
			}
			else
			{
				return View( "Staffers", model );
			}
		}
		/// <summary>
		/// Handles a Retreater Application Delete
		/// </summary>
		/// <param name="userID"></param>
		/// <returns>Goes back to the main Retreaters View</returns>
		[Authorize]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<ActionResult> RetreatersDelete( String userID )
		{
			var User = await this.db.Users.FirstOrDefaultAsync( c => c.Id == userID );
			if(User != null)
			{
				User.RetreaterPending = false;
				User.Retreater = false;
				await db.SaveChangesAsync();
				var userView = new UserInput(User);

				var carriers = await db.Carriers.OrderBy( c => c.Title ).ToListAsync<Carrier>();
				var carriersView = new Collection<CarrierView>();
				foreach ( var carrier in carriers )
				{
					carriersView.Add( new CarrierView( carrier ) );
				}
				var tshirtsizes = await db.TShirtSizes.OrderBy( c => c.Number ).ToListAsync<TShirtSize>();
				var tshirtsizesView = new Collection<TShirtSizeView>();
				foreach ( var size in tshirtsizes )
				{
					tshirtsizesView.Add( new TShirtSizeView( size ) );
				}
				var grades = await db.Grades.OrderBy( c => c.Number ).ToListAsync<Grade>();
				var gradesView = new Collection<GradeView>();
				foreach ( var grade in grades )
				{
					gradesView.Add( new GradeView( grade ) );
				}


				ViewBag.Grades = new SelectList( gradesView, "RID", "Title" );
				ViewBag.Sizes =  new SelectList( tshirtsizesView, "RID", "Size" );
				ViewBag.Carriers = new SelectList( carriersView, "RID", "Title" );

				this.Mail.From = this.From;
				this.Mail.Subject = "ChattAwakening Retreater Application Deletion";
				this.Mail.Body = User.Personal.FirstName + ", <br /><br />We are emailing you to confirm that we have deleted your application.  We are sorry that you can no longer attend ChattAwakening. If you would like to re-apply, please go to <a href=\"http://utccatholic.org/Awakening/Retreaters\">http://utccatholic.org/Awakening/Retreaters</a>. We have saved your information there for convenience. <br /> If you have any questions, comments, and/or concerns, please email us back!<br /> <br /> Thank you, <br />ChattAwakening - UTC Catholic";
				this.Mail.IsBodyHtml = true;
				this.Mail.To.Add( User.Personal.Email );
				this.Mail.Bcc.Add( "mcgraw.m201294@yahoo.com, mollygallagher26@gmail.com, james0308@outlook.com" );
				client.Credentials = Credentials;
				client.Send( this.Mail );

				return View( "Retreaters", userView );
			}
			else
			{
				return HttpNotFound();
			}
		}
		/// <summary>
		/// Handles a Staffer Application Delete
		/// </summary>
		/// <param name="userID"></param>
		/// <returns>Goes back to the main Staffers View</returns>
		[Authorize]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<ActionResult> StaffersDelete( String userID )
		{
			var User = await this.db.Users.FirstOrDefaultAsync( c => c.Id == userID );
			if ( User != null )
			{
				User.StafferPending = false;
				User.Staffer = false;
				await db.SaveChangesAsync();
				var userView = new UserInput( User );

				var carriers = await db.Carriers.OrderBy( c => c.Title ).ToListAsync<Carrier>();
				var carriersView = new Collection<CarrierView>();
				foreach ( var carrier in carriers )
				{
					carriersView.Add( new CarrierView( carrier ) );
				}
				var tshirtsizes = await db.TShirtSizes.OrderBy( c => c.Number ).ToListAsync<TShirtSize>();
				var tshirtsizesView = new Collection<TShirtSizeView>();
				foreach ( var size in tshirtsizes )
				{
					tshirtsizesView.Add( new TShirtSizeView( size ) );
				}
				var grades = await db.Grades.OrderBy( c => c.Number ).ToListAsync<Grade>();
				var gradesView = new Collection<GradeView>();
				foreach ( var grade in grades )
				{
					gradesView.Add( new GradeView( grade ) );
				}
				var choices = await db.StaffChoices.OrderBy( c => c.Title ).ToListAsync<StaffChoice>();
				var choicesView = new Collection<StaffChoiceView>();
				foreach ( var choice in choices )
				{
					choicesView.Add( new StaffChoiceView( choice ) );
				}


				ViewBag.Choices = new SelectList( choicesView, "RID", "Title" );
				ViewBag.Grades = new SelectList( gradesView, "RID", "Title" );
				ViewBag.Sizes =  new SelectList( tshirtsizesView, "RID", "Size" );
				ViewBag.Carriers = new SelectList( carriersView, "RID", "Title" );

				this.Mail.From = this.From;
				this.Mail.Subject = "ChattAwakening Staffer Application Deletion";
				this.Mail.Body = User.Personal.FirstName + ", <br /><br />We are emailing you to confirm that we have deleted your application.  We are sorry that you can no longer attend ChattAwakening. If you would like to re-apply, please go to <a href=\"http://utccatholic.org/Awakening/Staffers\">http://utccatholic.org/Awakening/Staffers</a>. We have saved your information there for convenience. <br /> If you have any questions, comments, and/or concerns, please email us back!<br /> <br /> Thank you, <br />ChattAwakening - UTC Catholic";
				this.Mail.IsBodyHtml = true;
				this.Mail.To.Add( User.Personal.Email );
				this.Mail.Bcc.Add(  "mcgraw.m201294@yahoo.com, mollygallagher26@gmail.com, james0308@outlook.com" );
				client.Credentials = Credentials;
				client.Send( this.Mail );

				return View( "Staffers", userView );
			}
			else
			{
				return HttpNotFound();
			}
		}

		public async Task<ActionResult> Profile(String Id)
		{

			var name = HttpContext.User.Identity.Name;
			var User = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );
			var child = await db.Users.FirstOrDefaultAsync(c => c.Id == Id);

			if(User.Family.Equals(child.Family))
			{
				var childView = new UserInput( child );

				return View( childView );
			}
			else
			{
				return HttpNotFound();
			}
		}
		[Authorize]
		public async Task<ActionResult> Family()
		{


			var name = HttpContext.User.Identity.Name;
			var User = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );

			var allowed = false;
			//Go through User Roles
			foreach(var role in User.Roles)
			{
				//Check to see if User is a member of Parent, Gophers, or P-Site
				if(role.Role.Name == "Parent" || role.Role.Name == "Gophers"  || role.Role.Name == "P-Site" )
				{
					allowed = true;
					ViewBag.IsInPSite = role.Role.Name == "P-Site";
					if(ViewBag.IsInPSite == true)
					{
						//If we succesfully found a P-Site Role, then we can leave.
						break;
					}
				}
			}
			if(!allowed)
			{
				return new HttpUnauthorizedResult();
			}

			FamilyInput familyView = null;
			if(User.Family != null)
			{
				familyView = new FamilyInput( User.Family );
			}

			var families = await db.Families.ToListAsync();
			ICollection<FamilyView> Families = new Collection<FamilyView>();
			foreach ( var family in families )
			{
				Families.Add( new FamilyView( family ) );
			}
			ViewBag.Families = Families;

			return View( familyView );
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> JoinFamily( Guid FamilyID )
		{

			var name = HttpContext.User.Identity.Name;
			var User = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );
			var allowed = false;
			ViewBag.IsInPSite = false;
			foreach ( var role in User.Roles )
			{
				//Check to see if User is a member of Parent, Gophers, or P-Site
				if ( role.Role.Name == "P-Site" )
				{
					allowed = true;
					ViewBag.IsInPSite = true;
				}
			}
			if(!allowed)
			{
				return new HttpUnauthorizedResult();
			}

			var family = await db.Families.FirstOrDefaultAsync( c => c.RID == FamilyID );

			if ( family != null )
			{
				User.Family = family;
				family.PSite.Add( User );
				await db.SaveChangesAsync();
			}

			var familyView = new FamilyInput( family );

			var families = await db.Families.ToListAsync();
/*			ICollection<FamilyView> Families = new Collection<FamilyView>();
			foreach ( var f in families )
			{
				Families.Add( new FamilyView( f ) );
			}
			ViewBag.Families = Families;
*/
			return View( "Family", familyView );
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> LeaveFamily(Guid FamilyID)
		{

			var name = HttpContext.User.Identity.Name;
			var User = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );
			var family = await db.Families.FirstOrDefaultAsync( c => c.RID == FamilyID);
			ViewBag.IsInPSite = false;

			if(User != null && family != null)
			{
				if(family.PSite.Contains(User))
				{
					User.Family = null;
					family.PSite.Remove( User );
					await db.SaveChangesAsync();
					ViewBag.IsInPSite = true;
				}
			}


			var families = await db.Families.ToListAsync();
			ICollection<FamilyView> Families = new Collection<FamilyView>();
			foreach ( var f in families )
			{
				Families.Add( new FamilyView( f ) );
			}
			ViewBag.Families = Families;

			return View( "Family" );
		}
		public async Task<ActionResult> Info()
		{

			var name = HttpContext.User.Identity.Name;
			var User = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );
			if(User != null)
			{
				ViewBag.Fee = User.Fee;
			}
			return View();
		}
	}
}