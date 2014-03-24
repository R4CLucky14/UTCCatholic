
function RoleViewModel(role)
{
	var self = this;
	if(role != null)
	{
		self.Id = ko.observable(role.RID);
		self.Name = ko.observable(role.Name);
	}
	else
	{
		self.Id = ko.observable();
		self.Name = ko.observable();
	}
	self.Emails = ko.computed(function()
	{
		var href = "mailto:";
		for (i = 0; i < Page.Users.Staffers().length; i++)
		{
			if (Page.Users.Staffers()[i].IsInRole(self))
			{
				href += Page.Users.Staffers()[i].Personal.Email() + ",";
			}
		}
		href += "awakening@utccatholic.org";
		return href;
	})
}

function PersonalViewModel(personal)
{
	var self = this;
	if(personal != null)
	{
		self.FirstName = ko.observable(personal.FirstName);
		self.LastName = ko.observable(personal.LastName);
		self.Phone = new PhoneViewModel(personal.Phone);
		self.Email = ko.observable(personal.Email);
		self.Hobbies = ko.observable(personal.Hobbies);
		self.TShirtSize = new TShirtViewModel(personal.TShirtSize);
	}
	else
	{
		self.FirstName = ko.observable();
		self.LastName = ko.observable();
		self.Phone = new PhoneViewModel();
		self.Email = ko.observable();
		self.Hobbies = ko.observable();
		self.TShirtSize = new TShirtViewModel();
	}
}
function SchoolViewModel(school)
{
	var self = this;
	if(school != null)
	{
		self.College = ko.observable(school.College);
		self.Grade = new GradeViewModel(school.Grade);
	}
	else
	{
		self.College = ko.observable();
		self.Grade = new GradeViewModel();
	}
}
function GradeViewModel(grade)
{
	var self = this;
	if(grade != null)
	{
		self.RID = ko.observable(grade.RID);
		self.Title = ko.observable(grade.Title);
	}
	else
	{
		self.RID = ko.observable();
		self.Title = ko.observable();
	}
}
function MedicalViewModel(medical)
{
	var self = this;
	if(medical != null)
	{
		self.Allergies = ko.observable(medical.Allergies);
		self.HealthConditions = ko.observable(medical.HealthConditions);
	}
	else
	{
		self.Allergies = ko.observable();
		self.HealthConditions = ko.observable();
	}
}
function ReligionViewModel(religion)
{
	var self = this;
	if(religion != null)
	{
		self.FavoriteSaint = ko.observable(religion.FavoriteSaint);
		self.FavoriteBibleVerse = ko.observable(religion.FavoriteBibleVerse);
		self.RoleModel = ko.observable(religion.RoleModel);
		self.Reason = ko.observable(religion.Reason);
		self.PrayerRequest = ko.observable(religion.PrayerRequest);
	}
	else
	{
		self.FavoriteSaint = ko.observable();
		self.FavoriteBibleVerse = ko.observable();
		self.RoleModel = ko.observable();
		self.Reason = ko.observable();
		self.PrayerRequest = ko.observable();
	}

}
function EmergencyViewModel(emergency)
{
	var self = this;
	if(emergency != null)
	{
		self.FirstName = ko.observable(emergency.FirstName);
		self.LastName = ko.observable(emergency.LastName);
		self.Email = ko.observable(emergency.Email);
		self.Phone = new PhoneViewModel(emergency.Phone);
	}
	else
	{
		self.FirstName = ko.observable();
		self.LastName = ko.observable();
		self.Email = ko.observable();
		self.Phone = new PhoneViewModel();
	}

}
function TransportationViewModel(transportation)
{
	var self = this;
	if(transportation != null)
	{
		self.TransportSelf = ko.observable(transportation.TransportSelf);
	}
	else
	{
		self.TransportSelf = ko.observable();
	}

}
function StaffViewModel(staff)
{
	var self = this;
	if(staff != null)
	{
		self.PreviousAttended = ko.observable(staff.PreviousAttended);
		self.PreviousPositions = ko.observable(staff.PreviousPositions);
		self.PreviousTalk = ko.observable(staff.PreviousTalk);
		self.InterestedTalk = ko.observable(staff.InterestedTalk);
		self.FirstChoice = new StaffChoiceViewModel(staff.FirstChoice);
		self.SecondChoice = new StaffChoiceViewModel(staff.SecondChoice);
		self.ThirdChoice = new StaffChoiceViewModel(staff.ThirdChoice);
		self.LeadershipPosition = ko.observable(staff.LeadershipPosition);
		self.AdditionalComments = ko.observable(staff.AdditionalComments);
	}
	else
	{
		self.PreviousAttended = ko.observable();
		self.PreviousPositions = ko.observable();
		self.PreviousTalk = ko.observable();
		self.InterestedTalk = ko.observable();
		self.FirstChoice = new StaffChoiceViewModel();
		self.SecondChoice = new StaffChoiceViewModel();
		self.ThirdChoice = new StaffChoiceViewModel();
		self.LeadershipPosition = ko.observable();
		self.AdditionalComments = ko.observable();
	}
}
function StaffChoiceViewModel(staffchoice)
{
	var self = this;
	if (staffchoice != null)
	{
		self.RID = ko.observable(staffchoice.RID);
		self.Title = ko.observable(staffchoice.Title);
	}
	else
	{
		self.RID = ko.observable();
		self.Title = ko.observable();
	}
}
function PhoneViewModel(phone)
{
	var self = this;
	if (phone != null)
	{
		self.Number = ko.observable(phone.Number);
		self.Carrier = new CarrierViewModel(phone.Carrier);
	}
	else
	{
		self.Number = ko.observable();
		self.Carrier = new CarrierViewModel();
	}
}
function CarrierViewModel(carrier)
{
	var self = this;
	if(carrier != null)
	{
		self.RID = ko.observable(carrier.RID);
		self.Title = ko.observable(carrier.Title);
		self.Email = ko.observable(carrier.Email);
	}
	else
	{
		self.RID = ko.observable();
		self.Title = ko.observable();
		self.Email = ko.observable();
	}
}
function TShirtViewModel(tshirt)
{
	var self = this;
	if (tshirt != null)
	{
		self.RID = ko.observable(tshirt.RID);
		self.Size = ko.observable(tshirt.Size);
	}
	else
	{
		self.RID = ko.observable();
		self.Size = ko.observable();
	}
}
function UserViewModel(user)
{
	var self = this;
	if (user != null)
	{
		self.RID = ko.observable(user.RID);
		self.Personal = new PersonalViewModel(user.Personal);
		self.School = new SchoolViewModel(user.School);
		self.Medical = new MedicalViewModel(user.Medical);
		self.Religion = new ReligionViewModel(user.Religion);
		self.Emergency = new EmergencyViewModel(user.Emergency);
		self.Transportation = new TransportationViewModel(user.Transportation);
		self.Staff = new StaffViewModel(user.Staff);

		self.Retreater = ko.observable(user.Retreater);
		self.RetreaterPending = ko.observable(user.RetreaterPending);
		self.Staffer = ko.observable(user.Staffer);
		self.StafferPending = ko.observable(user.StafferPending);
		self.Roles = ko.observableArray([]);
		$.map(user.Roles, function (role)
		{
			self.Roles.push(new RoleViewModel(role));
		});
	}
	else
	{
		self.RID = ko.observable();
		self.Personal = new PersonalViewModel();
		self.School = new SchoolViewModel();
		self.Medical = new MedicalViewModel();
		self.Religion = new ReligionViewModel();
		self.Emergency = new EmergencyViewModel();
		self.Transportation = new TransportationViewModel();
		self.Staff = new StaffViewModel();

		self.Retreater = ko.observable(false);
		self.RetreaterPending = ko.observable(false);
		self.Staffer = ko.observable(false);
		self.StafferPending = ko.observable(false);
		self.Roles = ko.observableArray([]);
	}
	self.ShowApp = ko.computed(function ()
	{
		if (self.Retreater())
		{
			return false;
		}
		else if (self.RetreaterPending())
		{
			return false;
		}
		else if (self.Staffer())
		{
			return false;
		}
		else if (self.StafferPending())
		{
			return false;
		}
		else
		{
			return true;
		}
	});

	self.IsInAnyRole = function()
	{
		if (self.Roles().length > 0)
		{
			return true;
		}
		else return false;
	}
	self.IsInRole = function (currentRole)
	{
		if (self.IsInAnyRole())
		{
			if (currentRole != null)
			{
				var match = ko.utils.arrayFirst(self.Roles(), function (role)
				{
					return currentRole.Name() == role.Name();
				});
				if (match != null)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}
	self.AnyStaffer = ko.computed(function ()
	{
		if (self.Staffer())
		{
			if (self.StafferPending())
			{
				return true;
			}
		}
		return false;
	});
	self.AnyRetreater = ko.computed(function ()
	{
		if (self.Retreater())
		{
			if (self.RetreaterPending())
			{
				return true;
			}
		}
		return false;
	});
}

function PersonalUser()
{
	var self = this;
	self.User = new UserViewModel();
	self.hub = $.connection.awakeningHub;
	self.init = function ()
	{
		console.log("Loading init!");
		self.hub.server.index();
	}
	self.loading = ko.observable(true);
	self.hub.client.indexBack = function (UserView)
	{
		console.log("We are back!!!!");
		console.log(UserView);
		self.setUser(UserView);
		console.log(self.User);
		self.loading(false);
	}


	self.submitRetreat = function (retreatForm)
	{
		if($(retreatForm).valid()) //true false 
		{
			self.loading(true);
			console.log(retreatForm);
			var UserInput =
			{
				RID: self.User.RID(),
				Personal:
				{
					FirstName: retreatForm.Personal_FirstName.value,
					LastName: retreatForm.Personal_LastName.value,
					Email: retreatForm.Personal_Email.value,
					Phone: 
						{
							Number: retreatForm.Personal_Phone_Number.value,
							Carrier: retreatForm.Personal_Phone_Carrier.value
						},
					Hobbies: retreatForm.Personal_Hobbies.value,
					TShirtSize: retreatForm.Personal_TShirtSize.value
				},
				Religion:
				{
					FavoriteBibleVerse: retreatForm.Religion_FavoriteBibleVerse.value,
					FavoriteSaint: retreatForm.Religion_FavoriteSaint.value,
					RoleModel: retreatForm.Religion_RoleModel.value,
					Reason: retreatForm.Religion_Reason.value,
					PrayerRequest: retreatForm.Religion_PrayerRequest.value				
				},
				School:
				{
					College: retreatForm.School_College.value,
					Grade: retreatForm.School_Grade.value		
				},
				Medical:
				{
					Allergies: retreatForm.Medical_Allergies.value,
					HealthConditions: retreatForm.Medical_HealthConditions.value	
				},
				Emergency:
				{
					FirstName: retreatForm.Emergency_FirstName.value,
					LastName: retreatForm.Emergency_LastName.value,
					Email: retreatForm.Emergency_Email.value,
					Phone:
					{
						Number: retreatForm.Personal_Phone_Number.value,
						Carrier: retreatForm.Personal_Phone_Carrier.value
					}				
				},
				Transportation:
				{
					TransportSelf: retreatForm.Transportation_TransportSelf.value
				}
			}
			console.log(UserInput);
			self.hub.server.retreaterAppSubmit(UserInput);
		}
	}
	self.hub.client.submitRetreatBack = function (UserView)
	{
		console.log(UserView);
		self.setUser(UserView);
		self.loading(false);
	}

	self.submitStaff = function (staffForm)
	{
		if($(staffForm).valid()) //true false 
		{
			self.loading(true);
			console.log(staffForm);
			var UserInput =
			{
				RID: self.User.RID(),
				Personal:
				{
					FirstName: staffForm.Personal_FirstName.value,
					LastName: staffForm.Personal_LastName.value,
					Email: staffForm.Personal_Email.value,
					Phone:
						{
							Number: staffForm.Personal_Phone_Number.value,
							Carrier: staffForm.Personal_Phone_Carrier.value
						},
					Hobbies: staffForm.Personal_Hobbies.value,
					TShirtSize: staffForm.Personal_TShirtSize.value
				},
				Staff:
				{
					PreviousAttended: staffForm.Staff_PreviousAttended.value,
					PreviousPositions: staffForm.Staff_PreviousPositions.value,
					PreviousTalk: staffForm.Staff_PreviousTalk.value,
					InterestedTalk: staffForm.Staff_InterestedTalk.value,
					FirstChoice: staffForm.Staff_FirstChoice.value,
					SecondChoice: staffForm.Staff_SecondChoice.value,
					ThirdChoice: staffForm.Staff_ThirdChoice.value,
					LeadershipPosition: staffForm.Staff_LeadershipPosition.value,
					AdditionalComments: staffForm.Staff_AdditionalComments.value
				},
				School:
				{
					College: staffForm.School_College.value,
					Grade: staffForm.School_Grade.value
				},
				Medical:
				{
					Allergies: staffForm.Medical_Allergies.value,
					HealthConditions: staffForm.Medical_HealthConditions.value
				},
				Emergency:
				{
					FirstName: staffForm.Emergency_FirstName.value,
					LastName: staffForm.Emergency_LastName.value,
					Email: staffForm.Emergency_Email.value,
					Phone:
					{
						Number: staffForm.Personal_Phone_Number.value,
						Carrier: staffForm.Personal_Phone_Carrier.value
					}
				},
				Transportation:
				{
					TransportSelf: staffForm.Transportation_TransportSelf.value
				}
			}
			console.log(UserInput);
			self.hub.server.stafferAppSubmit(UserInput);
		}
	}
	self.hub.client.submitStaffBack = function (UserView)
	{
		console.log(UserView);
		self.setUser(UserView);
		self.loading(false);
	}
	self.setUser = function (UserView)
	{
		self.User.Personal.FirstName(UserView.Personal.FirstName);
		self.User.Personal.LastName(UserView.Personal.LastName);
		self.User.Personal.Email(UserView.Personal.Email);
		self.User.Personal.Phone.Number(UserView.Personal.Phone.Number);
		self.User.Personal.Phone.Carrier.RID(UserView.Personal.Phone.Carrier.RID);
		self.User.Personal.Phone.Carrier.Title(UserView.Personal.Phone.Carrier.Title);
		self.User.Personal.Phone.Carrier.Email(UserView.Personal.Phone.Carrier.Email);
		self.User.Personal.Hobbies(UserView.Personal.Hobbies);
		self.User.Personal.TShirtSize.RID(UserView.Personal.TShirtSize.RID);
		self.User.Personal.TShirtSize.Size(UserView.Personal.TShirtSize.Size);

		self.User.Religion.FavoriteBibleVerse(UserView.Religion.FavoriteBibleVerse);
		self.User.Religion.FavoriteSaint(UserView.Religion.FavoriteSaint);
		self.User.Religion.RoleModel(UserView.Religion.RoleModel);
		self.User.Religion.Reason(UserView.Religion.Reason);
		self.User.Religion.PrayerRequest(UserView.Religion.PrayerRequest);

		self.User.School.College(UserView.School.College);
		self.User.School.Grade.RID(UserView.School.Grade.RID);
		self.User.School.Grade.Title(UserView.School.Grade.Title);

		self.User.Medical.Allergies(UserView.Medical.Allergies);
		self.User.Medical.HealthConditions(UserView.Medical.HealthConditions);

		self.User.Emergency.FirstName(UserView.Emergency.FirstName);
		self.User.Emergency.LastName(UserView.Emergency.LastName);
		self.User.Emergency.Email(UserView.Emergency.Email);
		self.User.Emergency.Phone.Number(UserView.Emergency.Phone.Number);
		self.User.Emergency.Phone.Carrier.RID(UserView.Emergency.Phone.Carrier.RID);
		self.User.Emergency.Phone.Carrier.Title(UserView.Emergency.Phone.Carrier.Title);
		self.User.Emergency.Phone.Carrier.Email(UserView.Emergency.Phone.Carrier.Email);

		self.User.Transportation.TransportSelf(UserView.Transportation.TransportSelf);

		self.User.Staff.PreviousAttended(UserView.Staff.PreviousAttended);
		self.User.Staff.PreviousPositions(UserView.Staff.PreviousPositions);
		self.User.Staff.PreviousTalk(UserView.Staff.PreviousTalk);
		self.User.Staff.InterestedTalk(UserView.Staff.InterestedTalk);
		self.User.Staff.FirstChoice.RID(UserView.Staff.FirstChoice.RID);
		self.User.Staff.FirstChoice.Title(UserView.Staff.FirstChoice.Title);
		self.User.Staff.SecondChoice.RID(UserView.Staff.SecondChoice.RID);
		self.User.Staff.SecondChoice.Title(UserView.Staff.SecondChoice.Title);
		self.User.Staff.ThirdChoice.RID(UserView.Staff.ThirdChoice.RID);
		self.User.Staff.ThirdChoice.Title(UserView.Staff.ThirdChoice.Title);
		self.User.Staff.LeadershipPosition(UserView.Staff.LeadershipPosition);
		self.User.Staff.AdditionalComments(UserView.Staff.AdditionalComments);

		self.User.Retreater(UserView.Retreater);
		self.User.RetreaterPending(UserView.RetreaterPending);
		self.User.Staffer(UserView.Staffer);
		self.User.StafferPending(UserView.StafferPending);
	}
}
$(function ()
{
	$.connection.hub.starting(function ()
	{
		$('.updated').hide();
		$('.updating').show();
	});
	$.connection.hub.received(function ()
	{
		$('.updating').hide();
		$('.updated').show();
	});
	$.connection.hub.reconnecting(function ()
	{
		$('.updated').hide();
		$('.updating').show();
	});
	$.connection.hub.reconnected(function ()
	{
		$('.updating').hide();
		$('.updated').show();
	});
	$.connection.hub.disconnected(function ()
	{
		setTimeout(function ()
		{
			$.connection.hub.start();
		}, 5000); // Restart connection after 5 seconds.
	});
});

