using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UTCCatholic.Models
{
	public class Phone
	{
		[Required]
		public virtual AppUser Profile { get; set; }
		[Required]
		public virtual Carrier Carrier { get; set; }
		[StringLength( 3 )]
		[Required]
		public String AreaCode { get; set; }
		[StringLength( 3 )]
		[Required]
		public String Prefix { get; set; }
		[StringLength( 4 )]
		[Required]
		public String Line { get; set; }
		public String TextNumber { get { return AreaCode + Prefix + Line + "@" + Carrier.Email;  } private set;}
		public Phone()
			: base()
		{

		}
		public Phone( Carrier Carrier, String AreaCode, String Prefix, String Line )
			: base()
		{
			this.AreaCode = AreaCode;
			this.Prefix = Prefix;
			this.Line = Line;
		}
	}
}