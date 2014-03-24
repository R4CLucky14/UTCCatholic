using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	public class StaffChoice : DatabaseObject
	{
		public String Title { get; set; }

		public StaffChoice()
			: base()
		{

		}
		public StaffChoice(String Title)
			: base()
		{
			this.Title = Title;
		}
	}
}
