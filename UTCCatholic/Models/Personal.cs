using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	[ComplexType]
	public class Personal
	{
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public Phone PhoneNumber { get; set; }
		[Required]
		[EmailAddress]
		public String Email { get; set; }
		[Required]
		public TShirtSize TShirtSize { get; set; }
	}
}
