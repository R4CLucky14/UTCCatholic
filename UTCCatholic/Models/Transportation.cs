using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	public class Transportation : DatabaseObject
	{
		public virtual Boolean TransportSelf { get; set; }
	}
}
