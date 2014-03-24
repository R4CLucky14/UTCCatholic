using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UTCCatholic.InputModels;

namespace UTCCatholic.Models
{
	public class Phone : DatabaseObject
	{
		[Required]
		public virtual Carrier Carrier { get; set; }
		[Required]
		[Phone]
		public virtual String Number { get; set; }
		public virtual String TextNumber { get { return Number + "@" + Carrier.Email; } }
		public Phone()
			: base()
		{
		}
	}
}