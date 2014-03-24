using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Collections.ObjectModel;
using UTCCatholic.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UTCCatholic.Hubs
{
	[Authorize]
	public class AdminHub : BaseHub
	{
		public async Task AwakeningIndex()
		{
			try
			{ 
				if((await this.User()).Roles.FirstOrDefault(c => c.Role.Name == "Admin") != null )
				{
					var list = await db.Users.ToListAsync();
					var Retreaters = list.Where( c => c.Retreater == true );
					var Staffers = list.Where( c => c.Staffer == true );
					var RetreatersPending = list.Where( c => c.RetreaterPending == true );
					var StaffersPending = list.Where( c => c.StafferPending == true );
					var RetreatersView = new Collection<UserView>();
					foreach ( var item in Retreaters )
					{
						RetreatersView.Add( new UserView( item ) );
					}
					var StaffersView = new Collection<UserView>();
					foreach ( var item in Staffers )
					{
						StaffersView.Add( new UserView( item ) );
					}
					var RetreatersPendingView = new Collection<UserView>();
					foreach ( var item in RetreatersPending )
					{
						RetreatersPendingView.Add( new UserView( item ) );
					}
					var StaffersPendingView = new Collection<UserView>();
					foreach ( var item in StaffersPending )
					{
						StaffersPendingView.Add( new UserView( item ) );
					}

					var SortedRetreaters = RetreatersView.OrderBy( m => m.Personal.FirstName );
					var SortedStaffers = StaffersView.OrderBy( m => m.Personal.FirstName );
					var SortedRetreatersPending = RetreatersPendingView.OrderBy( m => m.Personal.FirstName );
					var SortedStaffersPending = StaffersPendingView.OrderBy( m => m.Personal.FirstName );

					Clients.Caller.awakeningRetreaters( SortedRetreaters );
					Clients.Caller.awakeningStaffers( SortedStaffers );
					Clients.Caller.awakeningRetreatersPending( SortedRetreatersPending );
					Clients.Caller.awakeningStaffersPending( SortedStaffersPending );
				}		
				else
				{
					Clients.Caller.errorBack( "Unauthorized Access" );
				}	
			}
			catch(Exception e)
			{
				Clients.Caller.errorBack(e);			
			}
		}

		public async Task AcceptStaffer(String StafferID, String RoleID)
		{
			try
			{ 
				if((await this.User()).Roles.FirstOrDefault(c => c.Role.Name == "Admin") != null )
				{
					var user = await db.Users.FirstOrDefaultAsync(c => c.Id == StafferID);
					var role = await db.Roles.FirstOrDefaultAsync(c => c.Id == RoleID);
					if(user != null && role != null)
					{
						user.Staffer = true;
						user.StafferPending = false;
						user.Retreater = false;
						user.RetreaterPending = false;

						if(user.Roles.Any( c => c.RoleId == RoleID ))
						{
							Clients.Caller.errorBack( "User Already In Role" );
						}
						user.Roles.Add(new IdentityUserRole { Role = role, RoleId = RoleID, User = user, UserId = StafferID });
						db.Entry( user ).State = EntityState.Modified;
						await db.SaveChangesAsync();

						Clients.All.AcceptStafferBack(user.Id, role.Id);
					}
					else
					{
						Clients.Caller.errorBack( "Incorrect Form Information" );
					}
				}
				else
				{
					Clients.Caller.errorBack( "Unauthorized Access" );
				}
			}
			catch(Exception e)
			{
				Clients.Caller.errorBack(e);			
			}
		}

		public async Task AcceptRetreater(Guid RetreaterID)
		{
			try
			{
				if ( ( await this.User() ).Roles.FirstOrDefault( c => c.Role.Name == "Admin" ) != null )
				{

				}
				else
				{
					Clients.Caller.errorBack( "Unauthorized Access" );
				}
			}
			catch ( Exception e )
			{
				Clients.Caller.errorBack( e );
			}
		}

		public async Task StafferRole(Guid StafferID, Guid RoleID)
		{
			try
			{
				if ( ( await this.User() ).Roles.FirstOrDefault( c => c.Role.Name == "Admin" ) != null )
				{

				}
				else
				{
					Clients.Caller.errorBack( "Unauthorized Access" );
				}
			}
			catch ( Exception e )
			{
				Clients.Caller.errorBack( e );
			}
		}

		public async Task RolesIndex()
		{
			try
			{
				if ( ( await this.User() ).Roles.FirstOrDefault( c => c.Role.Name == "Admin" ) != null )
				{
					var Roles = db.Roles.ToList();

					var RolesView = new Collection<RoleView>();
					foreach ( var role in Roles )
					{
						RolesView.Add( new RoleView( role ) );
					}

					var SortedRoles = RolesView.OrderBy( c => c.Name );

					Clients.Caller.roleIndexBack( SortedRoles );
				}
				else
				{
					Clients.Caller.errorBack( "Unauthorized Access" );
				}
			}
			catch ( Exception e )
			{
				Clients.Caller.errorBack( e );
			}
		}

		public async Task CreateRole(String Title)
		{
			if(Title != null)
			{
				db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(Title));
				await db.SaveChangesAsync();

				var role = db.Roles.FirstOrDefault( c => c.Name == Title );

				Clients.All.createRoleBack(new RoleView(role));
			}
			else
			{
				Clients.Caller.errorBack( "Empty Role Title!" );			
			}
		}
		public async Task EditRole(String Id, String Name)
		{
			if ( Id != null && Name != null )
			{
				var model = db.Roles.FirstOrDefault( c => c.Id == Id );

				model.Name = Name;

				db.Entry(model).State = EntityState.Modified;

				await db.SaveChangesAsync();

				Clients.All.editRoleBack( model );

			}
		}
		public async Task DeleteRole( Guid RoleID )
		{
			if ( RoleID != null )
			{
				var id = RoleID.ToString();
				var model = db.Roles.FirstOrDefault( c => c.Id == id );

				try
				{
					db.Roles.Remove( model );
					await db.SaveChangesAsync();
					Clients.All.deleteRoleBack( model );
				}
				catch(Exception e)
				{
					Clients.Caller.errorBack(e);
				}
			}
		}

		public async Task AddUserRole( String UserID, String RoleID )
		{
			try
			{
				var name = Context.User.Identity.Name;
				if ( await this.User() == await db.Users.FirstOrDefaultAsync( c => c.UserName == name ) )
				{
					var user = await db.Users.FirstOrDefaultAsync( c => c.Id == UserID );
					if ( user != null )
					{
						var role = await db.Roles.FirstOrDefaultAsync( c => c.Id == RoleID );
						if ( role != null )
						{
							if ( user.Roles.Contains( new IdentityUserRole() { Role = role, RoleId = role.Id, User = user, UserId = user.Id } ) )
							{
								Clients.Caller.errorBack( "User Already in Role." );
							}
							else
							{
								user.Roles.Add( new IdentityUserRole() { Role = role, RoleId = role.Id, User = user, UserId = user.Id } );
								await db.SaveChangesAsync();
								Clients.All.AddUserRoleBack( UserID, RoleID );
							}
						}
						else
						{
							Clients.Caller.errorBack( "Incorrect Role ID." );
						}
					}
					else
					{
						Clients.Caller.errorBack( "Incorrect User ID." );
					}
				}
				else
				{
					Clients.Caller.errorBack( "Non-Authorized Access." );
				}
			}
			catch ( Exception error )
			{
				Clients.Caller.errorBack( error );
			}
		}
		public async Task RemoveUserRole( String UserID, String RoleID )
		{
			try
			{
				var name = Context.User.Identity.Name;
				if ( await this.User() == await db.Users.FirstOrDefaultAsync( c => c.UserName == name ) )
				{
					var user = await db.Users.FirstOrDefaultAsync( c => c.Id == UserID );
					if ( user != null )
					{
						var role = user.Roles.FirstOrDefault( c => c.RoleId == RoleID );
						if ( role != null )
						{
							user.Roles.Remove( role );
							await db.SaveChangesAsync();
							Clients.All.RemoveUserRoleBack( UserID, RoleID );
						}
						else
						{
							Clients.Caller.errorBack( "User not within Role." );
						}
					}
					else
					{
						Clients.Caller.errorBack( "Incorrect User ID." );
					}
				}
				else
				{
					Clients.Caller.errorBack( "Non-Authorized Access." );
				}
			}
			catch ( Exception error )
			{
				Clients.Caller.errorBack( error );
			}
		}

	}
}