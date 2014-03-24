using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	public class Grade : DatabaseObject
	{
		public String Title { get; set; }
		public int Number { get; set; }

		public Grade()
			: base()
		{

		}
		public Grade( String Title, int Number )
			: base()
		{
			this.Title = Title;
			this.Number = Number;
		}
	}
}
