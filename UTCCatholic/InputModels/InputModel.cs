using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UTCCatholic.Models;
using UTCCatholic.ViewModels;

namespace UTCCatholic.InputModels
{
	public abstract class InputModel
	{
		public Guid RID { get; set; }

		public InputModel()
		{

		}
		public InputModel( Guid RID )
		{
			this.RID = RID;
		}
	}

	public class UserInput : InputModel
	{
		public ICollection<RoleInput> Roles { get; set; }
		public Decimal Fee { get; set; }
		public Decimal FeePaid { get; set; }
		[Display( Name="Personal Information", Description="" )]
		public PersonalInput Personal { get; set; }
		[Display( Name="School Information", Description="" )]
		public SchoolInput School { get; set; }
		[Display( Name="Medical Information", Description="" )]
		public MedicalInput Medical { get; set; }
		[Display( Name="Religious Information", Description="" )]
		public ReligionInput Religion { get; set; }
		[Display( Name="Emergency Contact Information", Description="" )]
		public EmergencyInput Emergency { get; set; }
		[Display( Name="Transportation Information", Description="" )]
		public TransportationInput Transportation { get; set; }
		[Display( Name="Staffing Questions", Description="" )]
		public StaffInput Staff { get; set; }
		public Boolean Retreater { get; set; }
		public Boolean RetreaterPending { get; set; }
		public Boolean Staffer { get; set; }
		public Boolean StafferPending { get; set; }
		public String Position { get; set; }
		public FamilyInput Family { get; set; }
		public String Name { get { return this.Personal.FirstName + " " + this.Personal.LastName; } }
		public DateTime? ApplicationStamp { get; set; }

		public UserInput()
		{

		}
		public UserInput(UserView model)
		{
			this.Roles = new Collection<RoleInput>();
			foreach(var role in model.Roles)
			{	
				this.Roles.Add(new RoleInput(role));
			}
			this.Fee = model.Fee;
			this.FeePaid = model.FeePaid;
			this.Personal = new PersonalInput(model.Personal);
			this.School = new SchoolInput(model.School);
			this.Medical = new MedicalInput(model.Medical);
			this.Religion = new ReligionInput(model.Religion);
			this.Emergency = new EmergencyInput(model.Emergency);
			this.Transportation = new TransportationInput(model.Transportation);
			this.Staff = new StaffInput( model.Staff );
			this.Position = model.Position;
			if ( model.ApplicationStamp != null )
			this.ApplicationStamp = model.ApplicationStamp;
		}
		public UserInput( MyUser User )
			: base(Guid.Parse(User.Id))
		{
			this.Roles = new Collection<RoleInput>();
			foreach ( var role in User.Roles )
			{
				this.Roles.Add( new RoleInput( role ) );
			}
			this.Fee = User.Fee;
			this.FeePaid = User.FeePaid;
			this.Personal = new PersonalInput( User.Personal );
			this.School = new SchoolInput( User.School );
			this.Medical = new MedicalInput( User.Medical );
			this.Religion = new ReligionInput( User.Religion );
			this.Emergency = new EmergencyInput( User.Emergency );
			this.Transportation = new TransportationInput( User.Transportation );
			this.Staff = new StaffInput( User.Staff );
			this.Retreater = User.Retreater;
			this.RetreaterPending = User.RetreaterPending;
			this.Staffer = User.Staffer;
			this.StafferPending = User.StafferPending;
			this.Position = User.Position;
			if ( User.ApplicationStamp != null )
				this.ApplicationStamp = User.ApplicationStamp;
		}
	}
	public class PersonalInput : InputModel
	{
		[Required]
		[Display(Name="First Name", Description="")]
		public String FirstName { get; set; }
		[Required]
		[Display( Name="Last Name", Description="" )]
		public String LastName { get; set; }
		[Required]
		[Display( Name="Phone", Description="" )]
		public PhoneInput Phone { get; set; }
		[Required]
		[Display( Name="Email Address", Description="" )]
		[EmailAddress]
		public String Email { get; set; }
		[Required]
		[Display( Name="Tell Us About Yourself", Description="" )]
		public String Hobbies { get; set; }
		[Required]
		[Display( Name="T Shirt Size", Description="" )]
		public Guid TShirtSize { get; set; }

		public PersonalInput()
		{

		}
		public PersonalInput(PersonalView model)
		{
			this.FirstName = model.FirstName;
			this.LastName = model.LastName;
			this.Phone = new PhoneInput(model.Phone);
			this.Email = model.Email;
			this.Hobbies = model.Hobbies;
			if(model.TShirtSize != null)
				this.TShirtSize = model.TShirtSize.RID;
		}
		public PersonalInput( Personal model )
		{
			this.FirstName = model.FirstName;
			this.LastName = model.LastName;
			if(model.Phone != null)
				this.Phone = new PhoneInput( model.Phone );
			else
				this.Phone = new PhoneInput();
			this.Email = model.Email;
			this.Hobbies = model.Hobbies;
			if ( model.TShirtSize != null )
				this.TShirtSize = model.TShirtSize.RID;
		}

	}
	public class SchoolInput : InputModel
	{
		[Required]
		[Display( Name="School", Description="" )]
		public String College { get; set; }
		[Required]
		[Display( Name="Grade", Description="" )]
		public Guid Grade { get; set; }

		public SchoolInput()
		{

		}
		public SchoolInput(SchoolView model)
		{
			this.College = model.College;
			if(model.Grade != null)
				this.Grade = model.Grade.RID;
		}
		public SchoolInput( School model )
		{
			this.College = model.College;
			if ( model.Grade != null )
				this.Grade = model.Grade.RID;
		}
	}
	public class MedicalInput : InputModel
	{
		[Display( Name="Allergies", Description="" )]
		public String Allergies { get; set; }
		[Display( Name="Any Other Health Concerns?", Description="" )]
		public String HealthConditions { get; set; }

		public MedicalInput()
		{

		}
		public MedicalInput(MedicalView model)
		{
			this.Allergies = model.Allergies;
			this.HealthConditions = model.HealthConditions;
		}
		public MedicalInput( Medical model )
		{
			this.Allergies = model.Allergies;
			this.HealthConditions = model.HealthConditions;
		}
	}
	public class ReligionInput : InputModel
	{
		[Required]
		[Display( Name="Favorite Saint", Description="" )]
		public String FavoriteSaint { get; set; }
		[Required]
		[Display( Name="Favorite Bible Verse", Description="" )]
		public String FavoriteBibleVerse { get; set; }
		[Required]
		[Display( Name="Who is your Role Model?", Description="" )]
		public String RoleModel { get; set; }
		[Required]
		[Display( Name="Why would you like to come to Awakening?", Description="" )]
		public String Reason { get; set; }
		[Required]
		[Display( Name="Do you have any Prayer Requests?", Description="" )]
		public String PrayerRequest { get; set; }

		public ReligionInput()
		{

		}
		public ReligionInput(ReligionView model)
		{
			this.FavoriteSaint = model.FavoriteSaint;
			this.FavoriteBibleVerse= model.FavoriteBibleVerse;
			this.RoleModel = model.RoleModel;
			this.Reason = model.Reason;
			this.PrayerRequest = model.PrayerRequest;
		}
		public ReligionInput( Religion model )
		{
			this.FavoriteSaint = model.FavoriteSaint;
			this.FavoriteBibleVerse= model.FavoriteBibleVerse;
			this.RoleModel = model.RoleModel;
			this.Reason = model.Reason;
			this.PrayerRequest = model.PrayerRequest;
		}
	}
	public class EmergencyInput : InputModel
	{
		[Required]
		[Display( Name="First Name", Description="" )]
		public String FirstName { get; set; }
		[Required]
		[Display( Name="Last Name", Description="" )]
		public String LastName { get; set; }
		[Required]
		[Display( Name="Email", Description="" )]
		[EmailAddress]
		public String Email { get; set; }
		[Required]
		public PhoneInput Phone { get; set; }

		public EmergencyInput()
		{

		}
		public EmergencyInput(EmergencyView model)
		{
			this.FirstName = model.FirstName;
			this.LastName = model.LastName;
			this.Email = model.Email;
			this.Phone = new PhoneInput(model.Phone);
		}
		public EmergencyInput( Emergency model )
		{
			this.FirstName = model.FirstName;
			this.LastName = model.LastName;
			this.Email = model.Email;
			if ( model.Phone != null )
				this.Phone = new PhoneInput( model.Phone );
			else
				this.Phone = new PhoneInput();
		}
	}
	public class TransportationInput : InputModel
	{
		[Required]
		[Display( Name="Are you able to transport yourself?", Description="If you are not able to transport yourself, we will make arrangements." )]
		public Boolean TransportSelf { get; set; }

		public TransportationInput()
		{

		}
		public TransportationInput(TransportationView model)
		{
			this.TransportSelf = model.TransportSelf;
		}
		public TransportationInput( Transportation model )
		{
			this.TransportSelf = model.TransportSelf;
		}
	}
	public class StaffInput : InputModel
	{
		[Required]
		[Display( Name="Previous Awakenings Attended" )]
		public String PreviousAttended { get; set; }
		[Required]
		[Display( Name="Previous Positions Staffed" )]
		public String PreviousPositions { get; set; }
		[Required]
		[Display( Name="Previous Talks Given" )]
		public String PreviousTalk { get; set; }
		[Required]
		[Display( Name="Are you interested in giving any talks? If so, which ones and why?" )]
		public String InterestedTalk { get; set; }
		[Required]
		[Display( Name="First Staff Choice" )]
		public Guid FirstChoice { get; set; }
		[Required]
		[Display( Name="Second Staff Choice" )]
		public Guid SecondChoice { get; set; }
		[Required]
		[Display( Name="Third Staff Choice" )]
		public Guid ThirdChoice { get; set; }
		[Display( Name="Would you want to staff any Leadership Positions?" )]
		public String LeadershipPosition { get; set; }
		[Display( Name="Any Additional Comments" )]
		public String AdditionalComments { get; set; }

		public StaffInput()
		{

		}
		public StaffInput(StaffView model)
		{
			this.PreviousAttended = model.PreviousAttended;
			this.PreviousPositions = model.PreviousPositions;
			this.PreviousTalk = model.PreviousTalk;
			this.InterestedTalk = model.InterestedTalk;
			if(model.FirstChoice != null)
				this.FirstChoice = model.FirstChoice.RID;
			if ( model.SecondChoice != null )
				this.SecondChoice = model.SecondChoice.RID;
			if ( model.ThirdChoice != null )
				this.ThirdChoice = model.ThirdChoice.RID;
			this.LeadershipPosition = model.LeadershipPosition;
			this.AdditionalComments = model.AdditionalComments;
		}
		public StaffInput( Staff model )
		{
			this.PreviousAttended = model.PreviousAttended;
			this.PreviousPositions = model.PreviousPositions;
			this.PreviousTalk = model.PreviousTalk;
			this.InterestedTalk = model.InterestedTalk;
			if ( model.FirstChoice != null )
				this.FirstChoice = model.FirstChoice.RID;
			if ( model.SecondChoice != null )
				this.SecondChoice = model.SecondChoice.RID;
			if ( model.ThirdChoice != null )
				this.ThirdChoice = model.ThirdChoice.RID;
			this.LeadershipPosition = model.LeadershipPosition;
			this.AdditionalComments = model.AdditionalComments;
		}
	}
	public class PhoneInput : InputModel
	{
		[Required]
		[Phone]
		[Display(Name="Number")]
		public String Number { get; set; }
		[Required]
		[Display( Name="Carrier", Description="We need your Carrier to send you text alerts/messages." )]
		public Guid Carrier { get; set; }

		public PhoneInput()
		{

		}
		public PhoneInput (PhoneView model)
		{
			this.Number = model.Number;
			this.Carrier = model.Carrier.RID;
		}
		public PhoneInput( Phone model )
		{
			this.Number = model.Number;
			this.Carrier = model.Carrier.RID;
		}
	}
	public class RoleInput : InputModel
	{
		public String Name { get; set; }

		public RoleInput()
			: base()
		{
		}
		public RoleInput(RoleView model)
			: base( model.RID )
		{
			this.Name = model.Name;
		}
		public RoleInput( IdentityRole Role )
			: base( Guid.Parse( Role.Id ) )
		{
			this.Name = Role.Name;
		}
		public RoleInput( IdentityUserRole Role )
			: base( Guid.Parse( Role.RoleId ) )
		{
			this.Name = Role.Role.Name;
		}

	}
	public class FamilyInput : InputModel
	{
		public String Name { get; set; }
		public UserInput Father { get; set; }
		public UserInput Mother { get; set; }
		public UserInput Gopher { get; set; }
		public ICollection<UserInput> Children { get; set; }
		public ICollection<UserInput> PSite { get; set; }

		public FamilyInput()
			: base()
		{
			this.Children = new Collection<UserInput>();
			this.PSite = new Collection<UserInput>();
		}
		public FamilyInput( Family Family )
			: base( Family.RID )
		{
			this.Name = Family.Name;
			if ( Family.Father != null )
				this.Father = new UserInput( Family.Father );
			if ( Family.Mother != null )
				this.Mother = new UserInput( Family.Mother );
			if ( Family.Gopher != null )
				this.Gopher = new UserInput( Family.Gopher );
			this.Children = new Collection<UserInput>();
			this.PSite = new Collection<UserInput>();
			foreach ( var child in Family.Children )
			{
				this.Children.Add( new UserInput( child ) );
			}
			foreach ( var psite in Family.PSite )
			{
				this.PSite.Add( new UserInput( psite ) );
			}
		}
	}
}