using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	public class Staff : DatabaseObject
	{
		public String PreviousAttended { get; set; }
		public String PreviousPositions { get; set; }
		public String PreviousTalk { get; set; }
		public String InterestedTalk { get; set; }
		public virtual StaffChoice FirstChoice { get; set; }
		public virtual StaffChoice SecondChoice { get; set; }
		public virtual StaffChoice ThirdChoice { get; set; }
		public String LeadershipPosition { get; set; }
		public String AdditionalComments { get; set; }
	}
}
