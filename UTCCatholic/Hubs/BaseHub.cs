using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Security;
using UTCCatholic.Models;


namespace UTCCatholic.Hubs
{
	public class BaseHub : Hub
	{
		protected AppDbContext db = new AppDbContext();

		public override Task OnConnected()
		{
			var name = Context.User.Identity.Name;
			var User = db.Users.FirstOrDefault( c => c.UserName == name );
			if ( User != null )
			{
				var connection = User.Connections.FirstOrDefault( c => c.RID == Guid.Parse( Context.ConnectionId ) );
				if ( connection == null )
				{
					User.Connections.Add( new Connection( Guid.Parse( Context.ConnectionId ), Context.Request.Headers["User-Agent"], true ) );
				}
			}
			db.SaveChanges();
			return base.OnConnected();
		}
		public override Task OnDisconnected()
		{
			var id = Guid.Parse(this.Context.ConnectionId);
			var connection = db.Connections.FirstOrDefault( c => c.RID == id );
			if ( connection != null )
			{
				connection.Connected = false;
				db.SaveChanges();
			}
			return base.OnDisconnected();
		}
		protected async Task<ICollection<String>> ConnectionIds()
		{
			var Account = await this.User();
			ICollection<String> ConnectionIds = new Collection<String>();
			foreach ( var item in Account.Connections )
			{
				ConnectionIds.Add( item.RID.ToString() );
			}
			//todo: Add Admins and Managers to the ConnecionIds List.
			return ConnectionIds;
		}
		[Authorize]
		protected async Task<MyUser> User()
		{
			var name = Context.User.Identity.Name;
			var user = await db.Users.FirstOrDefaultAsync( c => c.UserName == name );
			return user;
		}
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				db.Dispose();
			}
			base.Dispose( disposing );
		}

	}
}