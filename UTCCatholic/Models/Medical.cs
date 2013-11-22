using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	[ComplexType]
	public class Medical
	{
		public ICollection<String> Allergies { get; set; }
		public String Concerns { get; set; }
	}
}
