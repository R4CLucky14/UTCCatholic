using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UTCCatholic.InputModels;

namespace UTCCatholic.Models
{
	public class Religion : DatabaseObject
	{
		public virtual String FavoriteSaint { get; set; }
		public virtual string FavoriteBibleVerse { get; set; }
		public virtual String RoleModel { get; set; }
		public virtual string Reason { get; set; }
		public virtual string PrayerRequest { get; set; }

		public Religion()
			: base()
		{

		}
		public Religion(ReligionInput ReligionInput)
			: base()
		{
			this.FavoriteSaint = ReligionInput.FavoriteSaint;
			this.FavoriteBibleVerse = ReligionInput.FavoriteBibleVerse;
			this.FavoriteSaint = ReligionInput.RoleModel;
			this.FavoriteBibleVerse = ReligionInput.Reason;
			this.PrayerRequest = ReligionInput.PrayerRequest;
		}
	}
}
