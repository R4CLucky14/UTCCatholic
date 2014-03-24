using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UTCCatholic.Models
{
/*
	public class State : DatabaseObject
	{
		[StringLength( 2 )]
		public String Abbreviation { get; set; }
		public String Title { get; set; }
		public Boolean Allowed { get; set; }
		public override bool Removeable
		{
			get
			{
				return false;
			}
		}

		public State()
			: base()
		{
		}
		public State( String Title, String Abbreviation, Boolean Allowed = false )
			: base()
		{
			this.Title = Title;
			this.Abbreviation = Abbreviation;
			this.Allowed = Allowed;
		}
	}

	public interface IAddress
	{
		String Nick { get; set; }
		String Line1 { get; set; }
		String Line2 { get; set; }
		String City { get; set; }
		State State { get; set; }
		String ZIP { get; set; }	
	}

	public class CollegeAddress : DatabaseObject, IAddress
	{
		public College College { get; set; }
		[DataType( DataType.Text )]
		[Display( Name = "NickName" )]
		public String Nick { get; set; }
		[Required]
		[DataType( DataType.Text )]
		[Display( Name = "Address Line 1", Description = "Street Address, Company Name" )]
		public String Line1 { get; set; }
		[DataType( DataType.Text )]
		[Display( Name = "Address Line 2", Description = "Apartment, suite, unit, building, floor, etc." )]
		public String Line2 { get; set; }
		[Required]
		[Display( Name = "City" )]
		public String City { get; set; }
		[Required]
		[Display( Name = "State" )]
		public virtual State State { get; set; }
		[Required]
		[DataType( DataType.PostalCode )]
		[Display( Name = "ZIP Code" )]
		[StringLength( 5 )]
		public String ZIP { get; set; }

		public CollegeAddress()
			: base()
		{

		}

		public CollegeAddress( String Nick, String Line1, String Line2, String City, State State, String ZIP )
			: base()
		{
			this.Nick = Nick;
			this.Line1 = Line1;
			this.Line2 = Line2;
			this.City = City;
			this.State = State;
			this.ZIP = ZIP;
		}
		public CollegeAddress( College College, String Nick, String Line1, String Line2, String City, State State, String ZIP )
			: this(Nick, Line1, Line2, City, State, ZIP)
		{
			this.College = College;
		}
	}
 * */
}