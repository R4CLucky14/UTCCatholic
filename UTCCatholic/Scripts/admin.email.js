function Email()
{
	var self = this;

	self.Messages = ko.observableArray([]);

	self.AvailableTo = ko.observableArray(["Staffers", "Retreaters", "Both"]);
	self.SelectedTo = ko.observable();
	self.Subject = ko.observable();
	self.Body = ko.observable();

	self.AnyMessages = ko.computed(function ()
	{
		if(self.Messages().length > 0)
		{
			return true;
		}
		return false;
	});

	self.hub = $.connection.emailHub;
	self.hub.client.errorBack = function (message)
	{
		self.Messages.unshift({ m: message });
		if (self.Messages().length > 5)
		{
			myObservableArray.shift();
		}
		console.log(message);
	}
	self.hub.client.emailConfirm = function (message)
	{
		self.Messages.unshift({ m: message });
		if (self.Messages().length > 5)
		{
			myObservableArray.shift();
		}
		console.log(message);
	}

	self.SendEmail = function()
	{
		console.log("Send Email!!!");
		console.log();
		
		if (self.SelectedTo() == "Retreaters")
		{
			self.hub.server.retreaters(self.Subject(), self.Body());
			self.hub.server.retreatersPending(self.Subject(), self.Body());
		}
		else if (self.SelectedTo() == "Staffers")
		{
			self.hub.server.staffersPending(self.Subject(), self.Body());
			self.hub.server.staffers(self.Subject(), self.Body());
		}
		else if (self.SelectedTo() == "Both")
		{
			self.hub.server.retreaters(self.Subject(), self.Body());
			self.hub.server.retreatersPending(self.Subject(), self.Body());
			self.hub.server.staffersPending(self.Subject(), self.Body());
			self.hub.server.staffers(self.Subject(), self.Body());
		}
		else
		{
			self.Messages.unshift({ m: "To field was incomplete" });
			if (self.Messages().length > 5)
			{
				myObservableArray.shift();
			}
			console.log(message);
		}
	}
	self.EmailUser = function (data)
	{
		if (data[0] != null)
		{
			self.hub.server.emailUser(data[1].value, data[3].value, data[0].value);

		}
		else
		{
			self.Messages.unshift({ m: "To field was incomplete" });
			if (self.Messages().length > 5)
			{
				myObservableArray.shift();
			}
		}
	}
}