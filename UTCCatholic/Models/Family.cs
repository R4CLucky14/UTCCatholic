using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UTCCatholic.ViewModels;

namespace UTCCatholic.Models
{
	public class Family : DatabaseObject
	{
		[Required]
		public virtual String Name { get; set; }
		public virtual MyUser Father { get; set; }
		public virtual MyUser Mother { get; set; }
		public virtual ICollection<MyUser> Children { get; set; }
		public virtual MyUser Gopher { get; set; }
		public virtual ICollection<MyUser> PSite { get; set; }

		public Family()
			: base()
		{
			this.Children = new Collection<MyUser>();
			this.PSite = new Collection<MyUser>();
		}
		public Family(String Name)
			: base()
		{
			this.Name = Name;
			this.Children = new Collection<MyUser>();
			this.PSite = new Collection<MyUser>();
		}
		public Family( String Name, MyUser Father, MyUser Mother, MyUser Gopher )
			: base()
		{
			this.Name = Name;
			this.Father = Father;
			this.Mother = Mother;
			this.Gopher = Gopher;
			this.Children = new Collection<MyUser>();
			this.PSite = new Collection<MyUser>();
		}
	}
}