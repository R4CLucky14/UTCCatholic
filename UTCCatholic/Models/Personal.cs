using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	public class Personal : DatabaseObject
	{
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual Phone Phone { get; set; }
		//[EmailAddress]
		public virtual String Email { get; set; }
		public virtual TShirtSize TShirtSize { get; set; }
		public virtual String Hobbies { get; set; }

		public Personal()
			: base()
		{
		}
	}
}
