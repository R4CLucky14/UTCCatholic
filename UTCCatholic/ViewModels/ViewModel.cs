using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UTCCatholic.Models;
using UTCCatholic.InputModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.ObjectModel;

namespace UTCCatholic.ViewModels
{
	public abstract class ViewModel
	{
		public Guid RID { get; set; }

		public ViewModel()
		{

		}
		public ViewModel( Guid RID )
		{
			this.RID = RID;
		}
	}
	public class UserView : ViewModel
	{
		public ICollection<RoleView> Roles { get; set; }
		public Decimal Fee { get; set; }
		public Decimal FeePaid { get; set; }
		[Display( Name="Personal Information", Description="" )]
		public PersonalView Personal { get; set; }
		[Display( Name="School Information", Description="" )]
		public SchoolView School { get; set; }
		[Display( Name="Medical Information", Description="" )]
		public MedicalView Medical { get; set; }
		[Display( Name="Religious Information", Description="" )]
		public ReligionView Religion { get; set; }
		[Display( Name="Emergency Contact Information", Description="" )]
		public EmergencyView Emergency { get; set; }
		[Display( Name="Transportation Information", Description="" )]
		public TransportationView Transportation { get; set; }
		[Display( Name="Staffing Information", Description="" )]
		public StaffView Staff { get; set; }
		public Boolean Retreater { get; set; }
		public Boolean RetreaterPending { get; set; }
		public Boolean Staffer { get; set; }
		public Boolean StafferPending { get; set; }
		public String Position { get; set; }
		public DateTime? ApplicationStamp { get; set; }
		public String Name { get { return this.Personal.FirstName + " " + this.Personal.LastName; } }
		public Boolean IsFeePaid { get { if(Fee == FeePaid)  return true; else return false; } }

		public UserView()
			: base()
		{

		}
		public UserView(MyUser User)
			: base(Guid.Parse(User.Id))
		{
			this.Roles = new Collection<RoleView>();
			foreach(var role in User.Roles)
			{
				this.Roles.Add(new RoleView(role));
			}
			this.Fee = User.Fee;
			this.FeePaid = User.FeePaid;
			this.Personal = new PersonalView( User.Personal );
			this.School = new SchoolView( User.School );
			this.Medical = new MedicalView( User.Medical);
			this.Religion = new ReligionView( User.Religion);
			this.Emergency = new EmergencyView( User.Emergency);
			this.Transportation = new TransportationView( User.Transportation);
			this.Staff = new StaffView(User.Staff);
			this.Retreater = User.Retreater;
			this.RetreaterPending = User.RetreaterPending;
			this.Staffer = User.Staffer;
			this.StafferPending = User.StafferPending;
			this.Position = User.Position;
			if(User.ApplicationStamp != null)
				this.ApplicationStamp = User.ApplicationStamp;
		}
		public UserView( UserInput User )
			: base()
		{
			this.Roles = new Collection<RoleView>();
			foreach ( var role in User.Roles )
			{
				this.Roles.Add( new RoleView( role ) );
			}
			this.Fee = User.Fee;
			this.FeePaid = User.FeePaid;
			this.Personal = new PersonalView( User.Personal );
			this.School = new SchoolView( User.School );
			this.Medical = new MedicalView( User.Medical );
			this.Religion = new ReligionView( User.Religion );
			this.Emergency = new EmergencyView( User.Emergency );
			this.Transportation = new TransportationView( User.Transportation );
			this.Staff = new StaffView( User.Staff );
			this.Position = User.Position;
			if ( User.ApplicationStamp != null )
				this.ApplicationStamp = User.ApplicationStamp;
		}
	}
	public class PersonalView : ViewModel
	{
		[Required]
		[Display(Name="First Name", Description="")]
		public String FirstName { get; set; }
		[Required]
		[Display( Name="Last Name", Description="" )]
		public String LastName { get; set; }
		[Required]
		[Phone]
		[Display( Name="Phone", Description="" )]
		public PhoneView Phone { get; set; }
		[Required]
		[Display( Name="Email Address", Description="" )]
		[EmailAddress]
		public String Email { get; set; }
		[Required]
		[Display( Name="What are your Hobbies, etc?", Description="" )]
		public String Hobbies { get; set; }
		[Required]
		[Display( Name="T Shirt Size", Description="" )]
		public TShirtSizeView TShirtSize { get; set; }

		public PersonalView()
			: base()
		{

		}
		public PersonalView(Personal Personal)
			: base(Personal.RID)
		{
			this.FirstName = Personal.FirstName;
			this.LastName = Personal.LastName;
			if(Personal.Phone != null)
				this.Phone = new PhoneView(Personal.Phone);
			else
				this.Phone = new PhoneView( );
			this.Email = Personal.Email;
			this.Hobbies = Personal.Hobbies;
			if(Personal.TShirtSize != null)
				this.TShirtSize = new TShirtSizeView( Personal.TShirtSize );
			else
				this.TShirtSize = new TShirtSizeView( );
		}

		public PersonalView(PersonalInput Personal)
		{
			if(Personal.FirstName != null)
				this.FirstName = Personal.FirstName;
			if ( Personal.LastName != null )
			this.LastName = Personal.LastName;
			if ( Personal.Phone != null )
				this.Phone = new PhoneView( Personal.Phone );
			else
				this.Phone = new PhoneView();
			if ( Personal.Email != null )
				this.Email = Personal.Email;
			if ( Personal.Hobbies != null)
				this.Hobbies = Personal.Hobbies;
			if ( Personal.TShirtSize != null )
				this.TShirtSize = new TShirtSizeView( Personal.TShirtSize );
			else
				this.TShirtSize = new TShirtSizeView();
		}

	}
	public class SchoolView : ViewModel
	{
		[Required]
		[Display( Name="School", Description="" )]
		public String College { get; set; }
		[Required]
		[Display( Name="Grade", Description="" )]
		public GradeView Grade { get; set; }

		public SchoolView()
			: base()
		{
			
		}
		public SchoolView(School School)
			: base(School.RID)
		{
			this.College = School.College;
			if ( School.Grade != null )
				this.Grade = new GradeView(School.Grade);
			else
				this.Grade = new GradeView();
		}
		public SchoolView( SchoolInput School )
			: base( School.RID )
		{
			if(School.College != null)
				this.College = School.College;
			if ( School.Grade != null )
				this.Grade = new GradeView( School.Grade );
		}
	}
	public class MedicalView : ViewModel
	{
		[Display( Name="Allergies", Description="" )]
		public String Allergies { get; set; }
		[Display( Name="Any Other Health Concerns", Description="" )]
		public String HealthConditions { get; set; }

		public MedicalView()
			: base()
		{

		}
		public MedicalView(Medical Medical)
			: base(Medical.RID)
		{
			this.Allergies = Medical.Allergies;
			this.HealthConditions = Medical.HealthConditions;
		}
		public MedicalView( MedicalInput Medical )
			: base( Medical.RID )
		{
			if(Medical.Allergies != null)
				this.Allergies = Medical.Allergies;
			if(HealthConditions != null)
				this.HealthConditions = Medical.HealthConditions;
		}
	}
	public class ReligionView : ViewModel
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

		public ReligionView()
			: base()
		{

		}
		public ReligionView(Religion Religion)
			: base(Religion.RID)
		{
			this.FavoriteSaint = Religion.FavoriteSaint;
			this.FavoriteBibleVerse = Religion.FavoriteBibleVerse;
			this.RoleModel = Religion.RoleModel;
			this.Reason = Religion.Reason;
			this.PrayerRequest = Religion.PrayerRequest;
		}
		public ReligionView( ReligionInput Religion )
			: base( Religion.RID )
		{
			if(Religion.FavoriteSaint != null)
				this.FavoriteSaint = Religion.FavoriteSaint;
			if(Religion.FavoriteBibleVerse != null)
				this.FavoriteBibleVerse = Religion.FavoriteBibleVerse;
			if(Religion.RoleModel != null)
				this.RoleModel = Religion.RoleModel;
			if(Religion.Reason != null)
				this.Reason = Religion.Reason;
			if(Religion.PrayerRequest != null)
				this.PrayerRequest = Religion.PrayerRequest;
		}
	}
	public class EmergencyView : ViewModel
	{
		[Required]
		[Display( Name="First Name", Description="" )]
		public String FirstName { get; set; }
		[Required]
		[Display( Name="Last Name", Description="" )]
		public String LastName { get; set; }
		[Required]
		[Display( Name="Email", Description="" )]
		public String Email { get; set; }
		[Required]
		public PhoneView Phone { get; set; }

		public EmergencyView()
			: base()
		{

		}
		public EmergencyView(Emergency Emergency)
			: base(Emergency.RID)
		{
			this.FirstName = Emergency.FirstName;
			this.LastName = Emergency.LastName;
			this.Email = Emergency.Email;
			if ( Emergency.Phone != null )
				this.Phone = new PhoneView(Emergency.Phone);
			else
				this.Phone = new PhoneView( );
		}
		public EmergencyView( EmergencyInput Emergency )
			: base( Emergency.RID )
		{
			if(Emergency.FirstName != null)
				this.FirstName = Emergency.FirstName;
			if(Emergency.LastName != null)
				this.LastName = Emergency.LastName;
			if(Emergency.Email != null)
				this.Email = Emergency.Email;
			if ( Emergency.Phone != null )
				this.Phone = new PhoneView( Emergency.Phone );
			else
				this.Phone = new PhoneView();
		}
	}
	public class TransportationView : ViewModel
	{
		[Required]
		[Display( Name="Are you able to transport yourself?", Description="If you are not able to transport yourself, we will make arrangements." )]
		public Boolean TransportSelf { get; set; }

		public TransportationView()
			: base()
		{

		}
		public TransportationView(Transportation Transportation)
			: base(Transportation.RID)
		{
			this.TransportSelf = Transportation.TransportSelf;
		}
		public TransportationView( TransportationInput Transportation )
			: base( Transportation.RID )
		{
			if(Transportation.TransportSelf != null)
				this.TransportSelf = Transportation.TransportSelf;
		}

	}
	public class StaffView : ViewModel
	{
		[Required]
		[Display(Name="Previous Awakenings Attended")]
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
		public StaffChoiceView FirstChoice { get; set; }
		[Required]
		[Display( Name="Second Staff Choice" )]
		public StaffChoiceView SecondChoice { get; set; }
		[Required]
		[Display( Name="Third Staff Choice" )]
		public StaffChoiceView ThirdChoice { get; set; }
		[Display( Name="Would you want to staff any Leadership Positions?" )]
		public String LeadershipPosition { get; set; }
		[Display( Name="Any Additional Comments" )]
		public String AdditionalComments { get; set; }

		public StaffView()
			: base()
		{

		}
		public StaffView(Staff Staff)
			: base(Staff.RID)
		{
			this.PreviousAttended = Staff.PreviousAttended;
			this.PreviousPositions = Staff.PreviousPositions;
			this.PreviousTalk = Staff.PreviousTalk;
			this.LeadershipPosition = Staff.LeadershipPosition;
			this.InterestedTalk = Staff.InterestedTalk;
			if ( Staff.FirstChoice != null )
				this.FirstChoice = new StaffChoiceView( Staff.FirstChoice );
			else
				this.FirstChoice = new StaffChoiceView();
			if ( Staff.SecondChoice != null )
				this.SecondChoice = new StaffChoiceView( Staff.SecondChoice );
			else
				this.SecondChoice = new StaffChoiceView();
			if ( Staff.ThirdChoice != null )
				this.ThirdChoice = new StaffChoiceView( Staff.ThirdChoice );
			else
				this.ThirdChoice = new StaffChoiceView();
			this.AdditionalComments = Staff.AdditionalComments;
		}
		public StaffView( StaffInput Staff )
			: base( Staff.RID )
		{
			if(Staff.PreviousAttended != null)
				this.PreviousAttended = Staff.PreviousAttended;
			if(Staff.PreviousPositions != null)
				this.PreviousPositions = Staff.PreviousPositions;
			if(Staff.PreviousTalk != null)
				this.PreviousTalk = Staff.PreviousTalk;
			if(Staff.LeadershipPosition != null)
				this.LeadershipPosition = Staff.LeadershipPosition;
			if(InterestedTalk != null)
				this.InterestedTalk = Staff.InterestedTalk;
			if ( Staff.FirstChoice != null )
				this.FirstChoice = new StaffChoiceView( Staff.FirstChoice );
			else
				this.FirstChoice = new StaffChoiceView();
			if ( Staff.SecondChoice != null )
				this.SecondChoice = new StaffChoiceView( Staff.SecondChoice );
			else
				this.FirstChoice = new StaffChoiceView();
			if ( Staff.ThirdChoice != null )
				this.ThirdChoice = new StaffChoiceView( Staff.ThirdChoice );
			else
				this.FirstChoice = new StaffChoiceView();
			if ( Staff.AdditionalComments != null )
				this.AdditionalComments = Staff.AdditionalComments;
		}
	}
	public class PhoneView : ViewModel
	{
		[Required]
		[Phone]
		[Display(Name="Number")]
		public String Number { get; set; }
		[Required]
		[Display( Name="Carrier", Description="We need your Carrier to send you text alerts/messages." )]
		public CarrierView Carrier { get; set; }
		
		public PhoneView()
			: base()
		{
			this.Carrier = new CarrierView();
		}
		public PhoneView(Phone Phone)
			: base(Phone.RID)
		{
			this.Number = Phone.Number;
			if(Phone.Carrier != null)
				this.Carrier = new CarrierView(Phone.Carrier);
			else
				this.Carrier = new CarrierView();
		}
		public PhoneView( PhoneInput Phone )
			: base( Phone.RID )
		{
			if(Phone.Number != null)
				this.Number = Phone.Number;
			if ( Phone.Carrier != null )
				this.Carrier = new CarrierView( Phone.Carrier );
			else
				this.Carrier = new CarrierView();
		}
	}
	public class CarrierView : ViewModel
	{
		public String Title { get; set; }
		public String Email { get; set; }

		public CarrierView()
			: base()
		{

		}
		public CarrierView(Carrier Carrier)
			: base(Carrier.RID)
		{
			this.Title = Carrier.Title;
			this.Email = Carrier.Email;
		}
		public CarrierView( Guid CarrierID )
			: base( CarrierID )
		{

		}
	}
	public class TShirtSizeView : ViewModel
	{
		public String Size { get; set; }

		public TShirtSizeView()
			: base()
		{

		}
		public TShirtSizeView(TShirtSize TShirtSize)
			: base(TShirtSize.RID)
		{
			this.Size = TShirtSize.Size;
		}
		public TShirtSizeView( Guid TShirtSizeID )
			: base( TShirtSizeID )
		{
		}
	}
	public class GradeView : ViewModel
	{
		public String Title { get; set; }

		public GradeView()
			: base()
		{

		}
		public GradeView( Grade Grade )
			: base( Grade.RID )
		{
			this.Title = Grade.Title;
		}
		public GradeView( Guid GradeID )
			: base( GradeID )
		{
		}
	}
	public class StaffChoiceView : ViewModel
	{
		public String Title { get; set; }

		public StaffChoiceView()
			: base()
		{

		}
		public StaffChoiceView( StaffChoice StaffChoice )
			: base( StaffChoice.RID )
		{
			this.Title = StaffChoice.Title;
		}
		public StaffChoiceView( Guid StaffChoiceID )
			: base( StaffChoiceID )
		{
		}
	}
	public class RoleView : ViewModel
	{
		public String Name { get; set; }

		public RoleView( )
			: base(  )
		{
		}
		public RoleView( RoleInput Role )
			: base( Role.RID )
		{
			this.Name = Role.Name;
		}
		
		public RoleView(IdentityRole Role)
			 : base(Guid.Parse(Role.Id))
		{
			this.Name = Role.Name;
		}

		public RoleView( IdentityUserRole Role )
			: base( Guid.Parse( Role.RoleId ) )
		{
			this.Name = Role.Role.Name;
		}
		
	}
	public class FamilyView : ViewModel
	{
		public String Name { get; set; }
		public UserView Father { get; set; }
		public UserView Mother { get; set; }
		public UserView Gopher { get; set; }
		public ICollection<UserView> Children { get; set; }
		public ICollection<UserView> PSite { get; set; }

		public FamilyView()
			: base()
		{
			this.Children = new Collection<UserView>();
			this.PSite = new Collection<UserView>();
		}
		public FamilyView( Family Family )
			: base(Family.RID)
		{
			this.Name = Family.Name;
			if(Family.Father != null)
				this.Father = new UserView( Family.Father );
			if ( Family.Mother != null )
				this.Mother = new UserView( Family.Mother );
			if ( Family.Gopher != null )
			this.Gopher = new UserView( Family.Gopher );
			this.Children = new Collection<UserView>();
			this.PSite = new Collection<UserView>();
			foreach(var child in Family.Children)
			{
				this.Children.Add(new UserView(child));
			}
			foreach ( var psite in Family.PSite )
			{
				this.Children.Add( new UserView( psite ) );
			}
		}
	}
}