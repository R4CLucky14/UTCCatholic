using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UTCCatholic.Models
{
	public class School : DatabaseObject
	{
		public virtual String College { get; set; }
		public virtual Grade Grade { get; set; }

		public School()
			: base()
		{
		}
	}
}