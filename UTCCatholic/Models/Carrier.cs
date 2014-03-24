﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	public class Carrier : DatabaseObject
	{
		public string Title { get; set; }
		public string Email { get; set; }
		public virtual ICollection<Phone> PhoneNumbers { get; set; }

		public override bool Removeable
		{
			get
			{
				if ( PhoneNumbers.Count > 0 )
					return false;
				else
					return base.Removeable;
			}
		}

		public Carrier()
			: base()
		{
			this.PhoneNumbers = new Collection<Phone>();
		}
		public Carrier( String Title, String Email )
			: this()
		{
			this.Title = Title;
			this.Email = Email;
		}
	}
}
