using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	public class Saint : DatabaseObject
	{
		public String Name { get; set; }

		public Saint()
			: base()
		{

		}
		public Saint( String Name )
			: base()
		{
			this.Name = Name;
		}
	}
}
