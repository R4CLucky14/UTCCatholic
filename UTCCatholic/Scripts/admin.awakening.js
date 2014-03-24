function Users()
{
	var self = this;
	self.Retreaters = ko.observableArray();
	self.RetreatersPending = ko.observableArray();
	self.Staffers = ko.observableArray();
	self.StaffersPending = ko.observableArray();

	self.StaffersPendingEmails = ko.computed(function()
	{
		var href = "mailto:";
		for (i = 0; i < self.StaffersPending().length; i++)
		{
			href += self.StaffersPending()[i].Personal.Email() + ",";
		}
		href += "awakening@utccatholic.org";
		return href;
	})
	self.RetreatersPendingEmails = ko.computed(function ()
	{
		var href = "mailto:";
		for (i = 0; i < self.RetreatersPending().length; i++)
		{
			href += self.RetreatersPending()[i].Personal.Email() + ",";
		}
		href += "awakening@utccatholic.org";
		return href;
	})
	self.StaffersEmails = ko.computed(function ()
	{
		var href = "mailto:";
		for (i = 0; i < self.Staffers().length; i++)
		{
			href += self.Staffers()[i].Personal.Email() + ",";
		}
		href += "awakening@utccatholic.org";
		return href;
	})
	self.RetreatersEmails = ko.computed(function ()
	{
		var href = "mailto:";
		for (i = 0; i < self.Retreaters().length; i++)
		{
			href += self.Retreaters()[i].Personal.Email() + ",";
		}
		href += "awakening@utccatholic.org";
		return href;
	})

	self.TShirtSizes = new function()
	{
		var sizes = this;
		sizes.S =  ko.computed(function()
		{
			var x = 0;
			for( i = 0; i < self.Retreaters().length; i++)
			{
				if(self.Retreaters()[i].Personal.TShirtSize.Size() == "Small")
				{
					x++;
				}
			}
			for( i = 0; i < self.RetreatersPending().length; i++)
			{
				if(self.RetreatersPending()[i].Personal.TShirtSize.Size() == "Small")
				{
					x++;
				}
			}
			for( i = 0; i < self.Staffers().length; i++)
			{
				if(self.Staffers()[i].Personal.TShirtSize.Size() == "Small")
				{
					x++;
				}
			}
			for( i = 0; i < self.StaffersPending().length; i++)
			{
				if(self.StaffersPending()[i].Personal.TShirtSize.Size() == "Small")
				{
					x++;
				}
			}
			return x;
		});
		sizes.M =  ko.computed(function()
		{
			var x = 0;
			for( i = 0; i < self.Retreaters().length; i++)
			{
				if(self.Retreaters()[i].Personal.TShirtSize.Size() == "Medium")
				{
					x++;
				}
			}
			for( i = 0; i < self.RetreatersPending().length; i++)
			{
				if(self.RetreatersPending()[i].Personal.TShirtSize.Size() == "Medium")
				{
					x++;
				}
			}
			for( i = 0; i < self.Staffers().length; i++)
			{
				if(self.Staffers()[i].Personal.TShirtSize.Size() == "Medium")
				{
					x++;
				}
			}
			for( i = 0; i < self.StaffersPending().length; i++)
			{
				if(self.StaffersPending()[i].Personal.TShirtSize.Size() == "Medium")
				{
					x++;
				}
			}
			return x;
		});
		sizes.L =  ko.computed(function()
		{
			var x = 0;
			for( i = 0; i < self.Retreaters().length; i++)
			{
				if(self.Retreaters()[i].Personal.TShirtSize.Size() == "Large")
				{
					x++;
				}
			}
			for( i = 0; i < self.RetreatersPending().length; i++)
			{
				if(self.RetreatersPending()[i].Personal.TShirtSize.Size() == "Large")
				{
					x++;
				}
			}
			for( i = 0; i < self.Staffers().length; i++)
			{
				if(self.Staffers()[i].Personal.TShirtSize.Size() == "Large")
				{
					x++;
				}
			}
			for( i = 0; i < self.StaffersPending().length; i++)
			{
				if(self.StaffersPending()[i].Personal.TShirtSize.Size() == "Large")
				{
					x++;
				}
			}
			return x;
		});
		sizes.XL = ko.computed(function()
		{
			var x = 0;
			for( i = 0; i < self.Retreaters().length; i++)
			{
				if(self.Retreaters()[i].Personal.TShirtSize.Size() == "X-Large")
				{
					x++;
				}
			}
			for( i = 0; i < self.RetreatersPending().length; i++)
			{
				if(self.RetreatersPending()[i].Personal.TShirtSize.Size() == "X-Large")
				{
					x++;
				}
			}
			for( i = 0; i < self.Staffers().length; i++)
			{
				if(self.Staffers()[i].Personal.TShirtSize.Size() == "X-Large")
				{
					x++;
				}
			}
			for( i = 0; i < self.StaffersPending().length; i++)
			{
				if(self.StaffersPending()[i].Personal.TShirtSize.Size() == "X-Large")
				{
					x++;
				}
			}
			return x;
		});
		sizes.XL2 = ko.computed(function()
		{
			var x = 0;
			for( i = 0; i < self.Retreaters().length; i++)
			{
				if(self.Retreaters()[i].Personal.TShirtSize.Size() == "2X-Large")
				{
					x++;
				}
			}
			for( i = 0; i < self.RetreatersPending().length; i++)
			{
				if(self.RetreatersPending()[i].Personal.TShirtSize.Size() == "2X-Large")
				{
					x++;
				}
			}
			for( i = 0; i < self.Staffers().length; i++)
			{
				if(self.Staffers()[i].Personal.TShirtSize.Size() == "2X-Large")
				{
					x++;
				}
			}
			for( i = 0; i < self.StaffersPending().length; i++)
			{
				if(self.StaffersPending()[i].Personal.TShirtSize.Size() == "2X-Large")
				{
					x++;
				}
			}
			return x;
		});
	}
	self.Transport = new function ()
	{
		var trans = this;
		trans.True = ko.computed(function ()
		{
			var x = 0;
			for (i = 0; i < self.Retreaters().length; i++)
			{
				if (self.Retreaters()[i].Transportation.TransportSelf() == true)
				{
					x++;
				}
			}
			for (i = 0; i < self.RetreatersPending().length; i++)
			{
				if (self.RetreatersPending()[i].Transportation.TransportSelf() == true)
				{
					x++;
				}
			}
			for (i = 0; i < self.Staffers().length; i++)
			{
				if (self.Staffers()[i].Transportation.TransportSelf() == true)
				{
					x++;
				}
			}
			for (i = 0; i < self.StaffersPending().length; i++)
			{
				if (self.StaffersPending()[i].Transportation.TransportSelf() == true)
				{
					x++;
				}
			}
			console.log("TransportSelf, True:  " + x);
			return x;
		});
		trans.False = ko.computed(function ()
		{
			var x = 0;
			for (i = 0; i < self.Retreaters().length; i++)
			{
				if (self.Retreaters()[i].Transportation.TransportSelf() == false)
				{
					x++;
				}
			}
			for (i = 0; i < self.RetreatersPending().length; i++)
			{
				if (self.RetreatersPending()[i].Transportation.TransportSelf() == false)
				{
					x++;
				}
			}
			for (i = 0; i < self.Staffers().length; i++)
			{
				if (self.Staffers()[i].Transportation.TransportSelf() == false)
				{
					x++;
				}
			}
			for (i = 0; i < self.StaffersPending().length; i++)
			{
				if (self.StaffersPending()[i].Transportation.TransportSelf() == false)
				{
					x++;
				}
			}
			return x;
		});
	}
	self.HealthConditions = new function ()
	{
		var health = this;
		health.Allergies = ko.computed(function ()
		{
			var x = 0;
			for (i = 0; i < self.Retreaters().length; i++)
			{
				if (self.Retreaters()[i].Medical.Allergies() != null && self.Retreaters()[i].Medical.HealthConditions() == null)
				{
					x++;
				}
			}
			for (i = 0; i < self.RetreatersPending().length; i++)
			{
				if (self.RetreatersPending()[i].Medical.Allergies() != null && self.RetreatersPending()[i].Medical.HealthConditions() == null)
				{
					x++;
				}
			}
			for (i = 0; i < self.Staffers().length; i++)
			{
				if (self.Staffers()[i].Medical.Allergies() != null && self.Staffers()[i].Medical.HealthConditions() == null)
				{
					x++;
				}
			}
			for (i = 0; i < self.StaffersPending().length; i++)
			{
				if (self.StaffersPending()[i].Medical.Allergies() != null && self.StaffersPending()[i].Medical.HealthConditions() == null)
				{
					x++;
				}
			}
			console.log("TransportSelf, True:  " + x);
			return x;
		});
		health.Con = ko.computed(function ()
		{
			var x = 0;
			for (i = 0; i < self.Retreaters().length; i++)
			{
				if (self.Retreaters()[i].Medical.HealthConditions() != null && self.Retreaters()[i].Medical.Allergies() == null)
				{
					x++;
				}
			}
			for (i = 0; i < self.RetreatersPending().length; i++)
			{
				if (self.RetreatersPending()[i].Medical.HealthConditions() != null && self.RetreatersPending()[i].Medical.Allergies() == null)
				{
					x++;
				}
			}
			for (i = 0; i < self.Staffers().length; i++)
			{
				if (self.Staffers()[i].Medical.HealthConditions() != null && self.Staffers()[i].Medical.Allergies() == null)
				{
					x++;
				}
			}
			for (i = 0; i < self.StaffersPending().length; i++)
			{
				if (self.StaffersPending()[i].Medical.HealthConditions() != null && self.StaffersPending()[i].Medical.Allergies() == null)
				{
					x++;
				}
			}
			return x;
		});
		health.Both = ko.computed(function ()
		{
			var x = 0;
			for (i = 0; i < self.Retreaters().length; i++)
			{
				if (self.Retreaters()[i].Medical.HealthConditions() != null && self.Retreaters()[i].Medical.Allergies() != null)
				{
					x++;
				}
			}
			for (i = 0; i < self.RetreatersPending().length; i++)
			{
				if (self.RetreatersPending()[i].Medical.HealthConditions() != null && self.RetreatersPending()[i].Medical.Allergies() != null)
				{
					x++;
				}
			}
			for (i = 0; i < self.Staffers().length; i++)
			{
				if (self.Staffers()[i].Medical.HealthConditions() != null && self.Staffers()[i].Medical.Allergies() != null)
				{
					x++;
				}
			}
			for (i = 0; i < self.StaffersPending().length; i++)
			{
				if (self.StaffersPending()[i].Medical.HealthConditions() != null && self.StaffersPending()[i].Medical.Allergies() != null)
				{
					x++;
				}
			}
			return x;
		});
		health.Neither = ko.computed(function ()
		{
			var x = 0;
			for (i = 0; i < self.Retreaters().length; i++)
			{
				if (self.Retreaters()[i].Medical.HealthConditions() == null && self.Retreaters()[i].Medical.Allergies() == null)
				{
					x++;
				}
			}
			for (i = 0; i < self.RetreatersPending().length; i++)
			{
				if (self.RetreatersPending()[i].Medical.HealthConditions() == null && self.RetreatersPending()[i].Medical.Allergies() == null)
				{
					x++;
				}
			}
			for (i = 0; i < self.Staffers().length; i++)
			{
				if (self.Staffers()[i].Medical.HealthConditions() == null && self.Staffers()[i].Medical.Allergies() == null)
				{
					x++;
				}
			}
			for (i = 0; i < self.StaffersPending().length; i++)
			{
				if (self.StaffersPending()[i].Medical.HealthConditions() == null && self.StaffersPending()[i].Medical.Allergies() == null)
				{
					x++;
				}
			}
			return x;
		});
	}

	self.ShowEmailDialog = ko.observable(false);
	self.ShowRoleDialog = ko.observable(true);
	self.ShowQuickView = ko.observable(true);
	self.ShowStaffers = ko.observable(true);
	self.ShowIncomingStaffers = ko.observable(true);
	self.ShowRetreaters = ko.observable(true);

	self.hub = $.connection.adminHub;
	self.setCurrentUser = function (user)
	{
		console.log("Setting Current User!");
		if (user.Retreater())
		{
			console.log("It's a Retreater!");
			if (index >= 0)
			{
				var user = self.Retreaters()[index];
				self.currentUser(user);
				self.currentUserNotEmpty(true);
			}
		}
		else if(user.RetreaterPending())
		{
			var index = self.RetreatersPending.indexOf(user);
			if (index >= 0)
			{
				var user = self.RetreatersPending()[index];
				self.currentUser(user);
				self.currentUserNotEmpty(true);
			}
		}
		else if(user.Staffer())
		{
			var index = self.Staffers.indexOf(user);
			if (index >= 0)
			{
				var user = self.Staffers()[index];
				self.currentUser(user);
				self.currentUserNotEmpty(true);
			}
		}
		else if (user.StafferPending())
		{
			var index = self.StaffersPending.indexOf(user);
			if (index >= 0)
			{
				var user = self.StaffersPending()[index];
				self.currentUser(user);
				self.currentUserNotEmpty(true);
			}
		}
		else
		{
			self.currentUserNotEmpty(false);
		}
	}
	self.setCurrentUserFalse = function ()
	{
		self.currentUserNotEmpty(false);
	}
	self.currentUser = ko.observable();
	self.init = function ()
	{
		console.log("Loading init!");
		self.hub.server.awakeningIndex();
	}
	self.loading = ko.observable(true);
	self.currentUserNotEmpty = ko.observable(false);
	self.hub.client.awakeningRetreaters = function (UsersList)
	{
		$.map(UsersList, function (user)
		{
			self.Retreaters.push(new UserViewModel(user));
		});
		self.loading(false);
		console.log("Retreaters:");
		console.log(self.Retreaters());
	}
	self.hub.client.awakeningStaffers = function (UsersList)
	{
		$.map(UsersList, function (user)
		{
			self.Staffers.push(new UserViewModel(user));
		});
		self.loading(false);
		console.log("Staffers:");
		console.log(self.Staffers());
	}
	self.hub.client.awakeningRetreatersPending = function (UsersList)
	{
		$.map(UsersList, function (user)
		{
			self.RetreatersPending.push(new UserViewModel(user));
		});
		self.loading(false);
		console.log("Retreaters Pending:");
		console.log(self.RetreatersPending());
	}
	self.hub.client.awakeningStaffersPending = function (UsersList)
	{
		$.map(UsersList, function (user)
		{
			self.StaffersPending.push(new UserViewModel(user));
		});
		self.loading(false);
		console.log("Staffers Pending:");
		console.log(self.StaffersPending());
	}
	self.hub.client.errorBack = function (error)
	{
		console.log(error);
	}
	self.UpdateRole = function(data)
	{
		if(data != null)
		{
			var StafferID = data[1].value;
			for (i = 0; i < data[0].options.length; i++)
			{
				if (data[0][i].selected)
				{
					role = data[0][i].value;
					break;
				}
			}
			self.hub.server.acceptStaffer(StafferID, role);
		}
	}
	self.hub.client.acceptStafferBack = function (StafferID, RoleID)
	{
		if (StafferID != null && RoleID != null)
		{
			var success = ko.utils.arrayFirst(self.StaffersPending(), function (originalStaffer)
			{
				if (originalStaffer.RID() == StafferID)
				{
					self.StaffersPending.remove(originalStaffer);
					self.Staffers.push(originalStaffer);
					originalStaffer.Staffer(true);
					originalStaffer.StafferPending(false);
					self.currentUser(originalStaffer);
					
					var role = ko.utils.arrayFirst(Page.Roles.list(), function (role)
					{
						return RoleID == role.Id();
					});
					var user = ko.utils.arrayFirst(self.Staffers(), function (user)
					{
						return StafferID == user.RID();
					});
					if (role != null && user != null)
					{
						user.Roles.push(role);
					}

				}
			});
		}
	}

	self.AddUserRole = function(data)
	{
		var role;
		for(i = 0; i < data[0].options.length; i++)
		{
			if(data[0][i].selected)
			{
				role = data[0][i].value;
				break;
			}
		}
		self.hub.server.addUserRole(self.currentUser().RID(), role);
	}
	self.RemoveUserRole = function(data)
	{
		self.hub.server.removeUserRole(self.currentUser().RID(), data.Id());
	}
	self.hub.client.addUserRoleBack = function(userID, roleID)
	{
		var role = ko.utils.arrayFirst(Page.Roles.list(), function (role)
		{
			return roleID == role.Id();
		});
		var user = ko.utils.arrayFirst(self.Staffers(), function (user)
		{
			return userID == user.RID();
		});
		if (role != null && user != null)
		{
			user.Roles.push(role);
		}
	}
	self.hub.client.removeUserRoleBack = function(userID, roleID)
	{
		var role = ko.utils.arrayFirst(Page.Roles.list(), function (role)
		{
			return roleID == role.Id();
		});
		var user = ko.utils.arrayFirst(self.Staffers(), function (user)
		{
			return userID == user.RID();
		});
		if (role != null && user != null)
		{
			user.Roles.remove(role);
		}
	}

}