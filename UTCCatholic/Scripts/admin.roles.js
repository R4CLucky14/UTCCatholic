function Roles()
{
	var self = this;

	self.list = ko.observableArray([]);

	self.loading = ko.observable(true);

	self.currentRole = ko.observable();
	self.currentRoleNotEmpty = ko.observable(false);
	self.setCurrentRole = function(role)
	{
		var index = self.list.indexOf(role);
		if (index >= 0)
		{
			var role = self.list()[index];
			self.currentRole(role);
			self.currentRoleNotEmpty(true);
		}
	}
	self.NewRoleTitle = ko.observable();

	self.hub = $.connection.adminHub;
	self.init = function ()
	{
		console.log("Loading init!");
		self.hub.server.rolesIndex();
	}
	self.hub.client.roleIndexBack = function (list)
	{
		$.map(list, function (role)
		{
			self.list.push(new RoleViewModel(role));
		});
		self.loading(false);
		console.log("Roles:");
		console.log(self.list());
	}
	self.createRole = function ()
	{
		if( self.NewRoleTitle() != null)
		{
			self.hub.server.createRole(self.NewRoleTitle());
		}
	}
	self.hub.client.createRoleBack = function (role)
	{
		self.list.push(new RoleViewModel(role));
		self.NewRoleTitle(null);
	}
	self.editRole = function (data)
	{
		if (self.currentRole() != null)
		{
			var role = self.currentRole();
			self.hub.server.editRole(role.Id(), data[0].value);
		}
	}
	self.hub.client.editRoleBack = function (editedRole)
	{
		if (editedRole != null)
		{
			var model = ko.utils.arrayFirst(self.list(), function (originalRole)
			{
				return originalRole.Id() == editedRole.Id;
			});

			var i = self.list.indexOf(model);
			if (i > 0)
			{
				self.list()[i].Name(editedRole.Name);
			}
		}
	}
	self.deleteRole = function ()
	{
		if (self.currentRole() != null)
		{
			self.hub.server.deleteRole(self.currentRole().Id());
		}
	}
	self.hub.client.deleteRoleBack = function (deletedRole)
	{
		//If Role is null, no Role was deleted, since users have already been assigned to the Role.
		if (deletedRole != null)
		{
			var model = ko.utils.arrayFirst(self.list(), function (originalRole)
			{
				return originalRole.Id() == deletedRole.Id;
			});

			var i = self.list.indexOf(model);
			if (i > 0)
			{
				self.list.remove(model);
			}
			self.currentRoleNotEmpty(false);
		}
	}
}