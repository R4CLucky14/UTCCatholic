using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UTCCatholic.Models;

namespace UTCCatholic.Models
{
	public abstract class DatabaseObject
	{
		[Key]
		public Guid RID { get; set; }
		public virtual bool Removeable { get { return true; } }

		public DatabaseObject()
		{
			this.RID = Guid.NewGuid();
		}
	}

	public class Connection : DatabaseObject
	{
		public virtual MyUser User { get; set; }
		public string UserAgent { get; set; }
		public bool Connected { get; set; }
		public override bool Removeable
		{
			get
			{
				return Connected;
			}
		}
		public Connection()
			: base()
		{

		}
		public Connection(Guid ID, String UserAgent, Boolean Connected)
		{
			this.RID = ID;
			this.UserAgent = UserAgent;
			this.Connected = Connected;
		}
	}

    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class MyUser : IdentityUser
    {
		public virtual DateTime? ApplicationStamp { get; set; }
		public virtual Family Family { get; set; }
		/// <summary>
		/// Short Cut Variable - Normally would be a data-entry, but for time-sakes, this will be a simple string.
		/// </summary>
		public virtual Decimal FeePaid { get; set; }
		public virtual Decimal Fee { get; set; }
		public virtual String Position { get; set; }
		public virtual ICollection<Connection> Connections { get; set; }
		public virtual Personal Personal { get; set; }
		public virtual School School { get; set; }
		public virtual Medical Medical { get; set; }
		public virtual Religion Religion { get; set; }
		public virtual Emergency Emergency { get; set; }
		public virtual Transportation Transportation { get; set; }
		public virtual Staff Staff { get; set; }
		public virtual Boolean IsFeePaid { get { if(this.Fee < this.FeePaid) return true; else return false; } }
		/// <summary>
		/// This is a temporary variable to allow faster roll-out.
		/// </summary>
		public virtual Boolean Retreater { get; set; }
		/// <summary>
		/// This is a temporary variable to allow faster roll-out.
		/// True if app is submitted and being reviewed. This will become false when retreater becomes true.
		/// </summary>
		public virtual Boolean RetreaterPending { get; set; }
		/// <summary>
		/// This is a temporary variable to allow faster roll-out.
		/// </summary>
		public virtual Boolean Staffer { get; set; }
		/// <summary>
		/// This is a temporary variable to allow faster roll-out.
		/// True if app is submitted and being reviewed. This will become false when staffer becomes true.
		/// </summary>
		public virtual Boolean StafferPending { get; set; }

		public MyUser( )
			: base( )
		{
			this.ApplicationStamp = null;
			this.Connections = new Collection<Connection>();
			this.Fee = 0M;
			this.FeePaid = 0M;
		}
		public MyUser( string userName )
			: base ( userName )
		{
			this.Fee = 0M;
			this.FeePaid = 0M;
			this.Personal = new Personal();
			this.School = new School();
			this.Medical = new Medical();
			this.School = new School();
			this.Religion = new Religion();
			this.Emergency = new Emergency();
			this.Transportation = new Transportation();
			this.Staff = new Staff();
			this.Retreater = false;
			this.RetreaterPending = false;
			this.Staffer = false;
			this.StafferPending = false;
			this.Connections = new Collection<Connection>();
			this.ApplicationStamp = null;
		}
    }

    public class AppDbContext : IdentityDbContext<MyUser>
    {
		public DbSet<DatabaseObject> DatabaseObjects { get; set; }
		public DbSet<Connection> Connections { get; set; }
		public DbSet<Carrier> Carriers { get; set; }
		public DbSet<Grade> Grades { get; set; }
		public DbSet<TShirtSize> TShirtSizes { get; set; }
		public DbSet<StaffChoice> StaffChoices { get; set; }
		public DbSet<Family> Families { get; set; }
		 
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			modelBuilder.Entity<IdentityUser>()
				.ToTable( "Users" );
			modelBuilder.Entity<MyUser>()
				.ToTable( "Users" );

			modelBuilder.Entity<DatabaseObject>().ToTable( "DatabaseObjects" ).
				Map<Family>( m =>
				{
					m.ToTable( "Families" );
				} ).
				Map<Carrier>( m =>
				{
					m.ToTable( "Carriers" );
				} ).
				Map<Grade>( m =>
				{
					m.ToTable( "Grades" );
				} ).
				Map<Staff>( m =>
				{
					m.ToTable( "Staffs" );
				} ).
				Map<Phone>( m =>
				{
					m.ToTable( "Phones" );
				} ).
				Map<Religion>( m =>
				{
					m.ToTable( "Religions" );
				} ).
				Map<Connection>( m =>
				{
					m.ToTable( "Connections" );
				} ).
				Map<Saint>( m =>
				{
					m.ToTable( "Saints" );
				} ).
				Map<School>( m =>
				{
					m.ToTable( "Schools" );
				} ).
				Map<TShirtSize>( m =>
				{
					m.ToTable( "TShirtSizes" );
				} ).
				Map<Personal>( m =>
				{
					m.ToTable( "Personals" );
				} ).
				Map<Emergency>( m =>
				{
					m.ToTable( "Emergencys" );
				} ).
				Map<Transportation>( m =>
				{
					m.ToTable( "Transportations" );
				} ).
				Map<StaffChoice>( m =>
				{
					m.ToTable( "StaffChoices" );
				} ).
				Map<Medical>( m =>
				{
					m.ToTable( "Medicals" );
				} );
		}
	}
}