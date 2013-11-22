using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace UTCCatholic.Models
{
	[ComplexType]
	public class Misc
	{
		public String WhyCome { get; set; }
		public String PrayerRequests { get; set; }
		public String FavoriteBibleVerse { get; set; }
		public String Hobbies { get; set; }
		public Saint FavoriteSaint { get; set; }
	}
}
