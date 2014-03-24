using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	public class Emergency : DatabaseObject
	{
		public virtual String FirstName { get; set; }
		public virtual String LastName { get; set; }
		//[EmailAddress]
		public virtual String Email { get; set; }
		public virtual Phone Phone { get; set; }

		public Emergency()
			: base()
		{
		}
	}
}
