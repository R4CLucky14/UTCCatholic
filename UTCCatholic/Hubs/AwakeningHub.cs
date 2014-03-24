using Microsoft.AspNet.SignalR;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using UTCCatholic.InputModels;
using UTCCatholic.ViewModels;
using System.Net.Mail;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UTCCatholic.Hubs
{
	[Authorize]
	public class AwakeningHub : BaseHub
	{
		public async Task Index()
		{
			var name = Context.User.Identity.Name;
			var user = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );
			var UserViewModel = new UserView( user );
			Clients.Caller.indexBack( UserViewModel );
		}
		public async Task RetreaterAppSubmit(UserInput model)
		{
			var User = await this.User();

			if ( User.Retreater)
			{
				Clients.Caller.errorBack("You are already attending this retreat.");
			}
			else if(User.Staffer )
			{
				Clients.Caller.errorBack("You are already staffing this retreat.");
			}
			else if(User.RetreaterPending)
			{
				Clients.Caller.errorBack("You have already submitted your retreat application.");
			}
			else if(User.StafferPending)
			{
				Clients.Caller.errorBack("You have already submitted your staffer application.");
			}
			else
			{
				User.RetreaterPending = true;

				User.Personal.FirstName = model.Personal.FirstName;
				User.Personal.LastName = model.Personal.LastName;
				User.Personal.Email = model.Personal.Email;
				User.Personal.Phone = new Models.Phone();
				User.Personal.Phone.Number = model.Personal.Phone.Number;
				User.Personal.Phone.Carrier = await db.Carriers.FindAsync(model.Personal.Phone.Carrier);
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
				var userView = new UserView(User);
				Clients.Caller.submitRetreatBack( userView );
			}
		}
		[Authorize(Roles="Admin")]
		public async Task RetreaterAppVerify( Guid UserID )
		{
			var id = UserID.ToString();
			var User = await db.Users.FirstOrDefaultAsync( c => c.Id == id);

			if ( User.Retreater )
			{
				Clients.Caller.errorBack( "User is already attending this retreat." );
			}
			else if ( User.Staffer )
			{
				Clients.Caller.errorBack( "User is already staffing this retreat." );
			}
			else if ( User.StafferPending )
			{
				Clients.Caller.errorBack( "User has submitted a staffer application." );
			}
			else if ( !User.RetreaterPending )
			{
				Clients.Caller.errorBack( "User has not submitted a retreater application. " );
			}
			else
			{
				User.RetreaterPending = false;
				User.Retreater = true;
				Clients.Client( Context.ConnectionId ).verifyBack( "Changes made. Please refresh." );
			}
		}
		public async Task StafferAppSubmit(UserInput model)
		{
			var User = await this.User();
			if ( User.Retreater )
			{
				Clients.Caller.errorBack( "You are already attending this retreat." );
			}
			else if ( User.Staffer )
			{
				Clients.Caller.errorBack( "You are already staffing this retreat." );
			}
			else if ( User.RetreaterPending )
			{
				Clients.Caller.errorBack( "You have already submitted your retreat application." );
			}
			else if ( User.StafferPending )
			{
				Clients.Caller.errorBack( "You have already submitted your staffer application." );
			}
			else
			{
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
				Clients.Caller.submitStaffBack( userView );

				var message = new MailMessage( "awakening@utccatholic.org", "james0308@outlook.com" );
				message.Body = "We have received a new application for ChattAwakening. The new applicant is a Staffer by the name of " + User.Personal.FirstName + " " + User.Personal.LastName + ". For more details, please go to: http://utccatholic.org/awakening/admin";
				var client = new SmtpClient( "mail.utccatholic.org", 465 );
				client.EnableSsl = true;
				client.Send( message );
			}
		}
		[Authorize( Roles="Admin" )]
		public async Task StafferAppVerify( Guid UserID )
		{
			var id = UserID.ToString();
			var User = await db.Users.FirstOrDefaultAsync( c => c.Id == id );

			if ( User.Retreater )
			{
				Clients.Caller.errorBack( "User is already attending this retreat." );
			}
			else if ( User.Staffer )
			{
				Clients.Caller.errorBack( "User is already staffing this retreat." );
			}
			else if ( !User.StafferPending )
			{
				Clients.Caller.errorBack( "User has submitted a staffer application." );
			}
			else if ( User.RetreaterPending )
			{
				Clients.Caller.errorBack( "User has not submitted a retreater application. " );
			}
			else
			{
				User.StafferPending = false;
				User.Staffer = true;
				Clients.Client( Context.ConnectionId ).verifyBack( "Changes made. Please refresh." );
			}
		}

	}
}

