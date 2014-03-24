using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Collections.ObjectModel;
using UTCCatholic.ViewModels;
using UTCCatholic.Models;
using System.Net.Mail;
using System.Net;

namespace UTCCatholic.Hubs
{
	/// <summary>
	/// This Hub is designed to offer real-time support for sending email to certain user groups.
	/// </summary>
	public class EmailHub : BaseHub
	{
	//	AppDbContext db = new AppDbContext();
		
		MailAddress From = new MailAddress( "awakening@utccatholic.org" );
		SmtpClient client = new SmtpClient( "mail.utccatholic.org" );
		NetworkCredential Credentials = new NetworkCredential( "awakening@utccatholic.org", "WVhmPOZr7Otz5b6F" );


		public async Task StaffersPending( String Subject, String Body )
		{
			try
			{
				if ( ( await this.User() ).Roles.FirstOrDefault( c => c.Role.Name == "Admin" ) != null )
				{
					client.Credentials = Credentials;

					var to = db.Users.Where( c => c.StafferPending == true );
					foreach ( var item in to )
					{
						MailMessage Mail = new MailMessage();
						Mail.From = this.From;
						Mail.Subject = Subject;
						Mail.IsBodyHtml = true;
						Mail.Body = item.Personal.FirstName + ",<br /><br />" + Body + "<br /> If you have any questions, comments, and/or concerns, please email us back!<br /> <br /> Thank you, <br />ChattAwakening - UTC Catholic";
						Mail.To.Add( item.Personal.Email );
						client.Send( Mail );
						Clients.All.emailConfirm( "Incoming Staffer Email to " + item.Personal.FirstName +" "+ item.Personal.LastName +" has been sent." );
					}
					Clients.All.emailConfirm( "All Incoming Staffers Emails have been sent." );
				}
				else
				{
					Clients.Caller.errorBack( "Unauthorized Access" );
				}
			}
			catch(Exception e)
			{
				Clients.Caller.emailConfirm( "Something went wrong!!! This came from the server: " + e.Message );
			}
		}
		public async Task Staffers(String Subject, String Body)
		{
			try
			{
				if ( ( await this.User() ).Roles.FirstOrDefault( c => c.Role.Name == "Admin" ) != null )
				{
					client.Credentials = Credentials;

					var to = db.Users.Where( c => c.Staffer == true );
					foreach ( var item in to )
					{
						MailMessage Mail = new MailMessage();
						Mail.From = this.From;
						Mail.Subject = Subject;
						Mail.IsBodyHtml = true;
						Mail.Body = item.Personal.FirstName + ",<br /><br />" + Body + "<br /> If you have any questions, comments, and/or concerns, please email us back!<br /> <br /> Thank you, <br />ChattAwakening - UTC Catholic";
						Mail.To.Add( item.Personal.Email );
						client.Send( Mail );
						Clients.All.emailConfirm( "Staffer Email to " + item.Personal.FirstName +" "+ item.Personal.LastName +" has been sent." );

					}
					Clients.All.emailConfirm( "All Staffer Email have been sent." );
				}
				else
				{
					Clients.Caller.errorBack( "Unauthorized Access" );
				}			
			}
			catch(Exception e)
			{
				Clients.Caller.emailConfirm( "Something went wrong!!! This came from the server: " + e.Message );
			}
		}
		public async Task RetreatersPending( String Subject, String Body )
		{
			try
			{
				if ( ( await this.User() ).Roles.FirstOrDefault( c => c.Role.Name == "Admin" ) != null )
				{
					client.Credentials = Credentials;

					var to = db.Users.Where( c => c.RetreaterPending == true );
					foreach ( var item in to )
					{
						MailMessage Mail = new MailMessage();
						Mail.From = this.From;
						Mail.Subject = Subject;
						Mail.IsBodyHtml = true;
						Mail.Body = item.Personal.FirstName + ",<br /><br />" + Body + "<br /> If you have any questions, comments, and/or concerns, please email us back!<br /> <br /> Thank you, <br />ChattAwakening - UTC Catholic";
						Mail.To.Add( item.Personal.Email );
						client.Send( Mail );
						Clients.All.emailConfirm( "Incoming Retreaters Email to " + item.Personal.FirstName +" "+ item.Personal.LastName +" has been sent." );

					}
					Clients.All.emailConfirm( "All Incoming Retreaters Emails have been sent." );
				}
				else
				{
					Clients.Caller.errorBack( "Unauthorized Access" );
				}
			}
			catch ( Exception e )
			{
				Clients.Caller.emailConfirm( "Something went wrong!!! This came from the server: " + e.Message );
			}

		}
		public async Task Retreaters( String Subject, String Body )
		{
			try
			{
				if ( ( await this.User() ).Roles.FirstOrDefault( c => c.Role.Name == "Admin" ) != null )
				{
					client.Credentials = Credentials;

					var to = db.Users.Where( c => c.Retreater == true );

					foreach ( var item in to )
					{
						MailMessage Mail = new MailMessage();
						Mail.From = this.From;
						Mail.Subject = Subject;
						Mail.IsBodyHtml = true;
						Mail.Body = item.Personal.FirstName + ",<br /><br />" + Body + "<br /> If you have any questions, comments, and/or concerns, please email us back!<br /> <br /> Thank you, <br />ChattAwakening - UTC Catholic";
						Mail.To.Add( item.Personal.Email );
						client.Send( Mail );
						Clients.All.emailConfirm( "Retreaters Email to " + item.Personal.FirstName +" "+ item.Personal.LastName +" has been sent." );

					}
					Clients.All.emailConfirm( "All Retreater Emails have been sent." );
				}
				else
				{
					Clients.Caller.errorBack( "Unauthorized Access" );
				}
			}
			catch ( Exception e )
			{
				Clients.Caller.emailConfirm( "Something went wrong!!! This came from the server: " + e.Message );
			}
		}

		public async Task EmailUser(String Subject, String Body, String UserID )
		{
			try
			{
				if ( ( await this.User() ).Roles.FirstOrDefault( c => c.Role.Name == "Admin" ) != null )
				{
					client.Credentials = Credentials;

					var to = await db.Users.FirstOrDefaultAsync( c => c.Id == UserID );
					MailMessage Mail = new MailMessage();
					Mail.From = this.From;
					Mail.Subject = Subject;
					Mail.IsBodyHtml = true;
					Mail.Body = to.Personal.FirstName + ",<br /><br />" + Body + "<br /> If you have any questions, comments, and/or concerns, please email us back!<br /> <br /> Thank you, <br />ChattAwakening - UTC Catholic";
					Mail.To.Add( to.Personal.Email );
					client.Send( Mail );

					Clients.All.emailConfirm( "Email has been sent to " + to.Personal.FirstName + " " + to.Personal.LastName );
				}
				else
				{
					Clients.Caller.errorBack( "Unauthorized Access" );
				}
			}
			catch ( Exception e )
			{
				Clients.Caller.emailConfirm( "Something went wrong!!! This came from the server: " + e.Message );
			}

		}
	}
}