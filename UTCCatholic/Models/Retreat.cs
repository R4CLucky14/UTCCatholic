using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	/// <summary>
	/// A base class for Retreats.
	/// As Awakening is a specific retreat that is rather popular, it will have it's own subclass.
	/// </summary>
	public class Retreat : DatabaseObject
	{
		public String Title { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public String Description { get; set; }
		public Decimal Fee { get; set; }
		public Boolean Free { get { if(Fee > 0M) return false; else return true; } private set; }
		public ICollection<AppUser> Retreaters { get; set; }
	}
}
