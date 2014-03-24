using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	public class Medical : DatabaseObject
	{
		public virtual String Allergies { get; set; }
		public virtual String HealthConditions { get; set; }

		public Medical()
			: base()
		{
		}
	}
}
