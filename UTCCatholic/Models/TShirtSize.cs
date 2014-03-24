using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	public class TShirtSize : DatabaseObject
	{
		public String Size { get; set; }
		public int Number { get; set; }

		public TShirtSize()
			: base()
		{

		}
		public TShirtSize( String Size, int Number)
			: base()
		{
			this.Size = Size;
			this.Number = Number;
		}
	}
}
