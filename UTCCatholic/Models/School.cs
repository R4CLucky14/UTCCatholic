using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UTCCatholic.Models
{
	[ComplexType]
	public class School
	{
		public College College { get; set; }
		public Grade Grade { get; set; }
	}
}