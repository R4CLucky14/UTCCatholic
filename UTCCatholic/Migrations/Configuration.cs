namespace UTCCatholic.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
	using UTCCatholic.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<UTCCatholic.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UTCCatholic.Models.AppDbContext context)
        {
		/*
			context.Carriers.AddOrUpdate(
				p => p.Title,
				new Carrier("3 River Wireless", "sms.3rivers.net"),
				new Carrier("ACS Wireless", "paging.acswireless.com"),
				new Carrier("AT&T", "txt.att.net"),
				new Carrier("Bell Canada", "txt.bellmobility.ca"),
				new Carrier("Blue Sky Frog", "blueskyfrog.com"),
				new Carrier("Bluegrass Cellular", "sms.bluecell.com"),
				new Carrier("Boost Mobile", "myboostmobile.com"),
				new Carrier("BPL Mobile", "bplmobile.com"),
				new Carrier("Carolina West Wireless", "cwwsms.com"),
				new Carrier("Cellular One", "mobile.celloneusa.com"),
				new Carrier("Cellular South", "csouth1.com"),
				new Carrier("Centennial Wireless", "cwemail.com"),
				new Carrier("CenturyTel", "messaging.centurytel.net"),
				new Carrier("Clearnet", "msg.clearnet.com"),
				new Carrier("Comcast", "comcastpcs.textmsg.com"),
				new Carrier("Corr Wireless Communications", "corrwireless.net"),
				new Carrier("Dobson", "mobile.dobson.net"),
				new Carrier("Edge Wireless", "sms.edgewireless.com"),
				new Carrier("Fido", "fido.ca"),
				new Carrier("Golden Telecom", "sms.goldetele.com"),
				new Carrier("Helio", "messaging.sprintpcs.com"),
				new Carrier("Houston Cellular", "text.houstoncellular.net"),
				new Carrier("Idea Cellular", "ideacellular.net"),
				new Carrier("Illinois Valley Cellular", "ivctext.com"),
				new Carrier("Inland Cellular Telephone", "inlandlink.com"),
				new Carrier("MCI", "pagemci.com"),
				new Carrier("Metrocall", "page.metrocall.com"),
				new Carrier("Metro PCS", "mymetropcs.com"),
				new Carrier("Microcell", "fido.ca"),
				new Carrier("Midwest Wireless", "clearlydigital.com"),
				new Carrier("Mobilcomm", "mobilecomm.net"),
				new Carrier("MTS", "text.mtsmobility.com"),
				new Carrier("Nextel", "messaging.nextel.com"),
				new Carrier("OnlineBeep", "onlinebeep.net"),
				new Carrier("PCS One", "pcsone.net"),
				new Carrier("President's Choice", "txt.bell.ca"),
				new Carrier("Public Service Cellular", "sms.pscel.com"),
				new Carrier("Qwest", "qwestmp.com"),
				new Carrier("Southwestern Bell", "email.swbw.com"),
				new Carrier("Sprint", "messaging.sprintpcs.com"),
				new Carrier("Sumcom", "tms.suncom.com"),
				new Carrier("Surewest Communcations", "mobile.surewest.com"),
				new Carrier("T-Mobile", "tmomail.net"),
				new Carrier("Telus", "msg.telus.com"),
				new Carrier("Tracfone", "txt.att.net"),
				new Carrier("Triton", "tms.suncom.com"),
				new Carrier("Unicel", "utext.com"),
				new Carrier("US Cellular", "email.uscc.net"),
				new Carrier("US West", "uswestdatamail.com"),
				new Carrier("Verizon", "vtext.com"),
				new Carrier("Virgon Mobile", "vmobl.com"),
				new Carrier("Virgon Mobile Canada", "vmobile.ca"),
				new Carrier("West Central Wireless", "sms.wcc.net"),
				new Carrier("Western Wireless", "cellularonewest")
			);

			context.Grades.AddOrUpdate(
				p => p.Title,
				new Grade("Freshman", 1),
				new Grade("Sophomore", 2),
				new Grade("Junior", 3),
				new Grade("Senior", 4)
			);
			context.TShirtSizes.AddOrUpdate(
				p => p.Size,
				new TShirtSize( "Small", 1 ),
				new TShirtSize( "Medium", 2  ),
				new TShirtSize( "Large", 3 ),
				new TShirtSize( "X-Large", 4 ),
				new TShirtSize( "2X-Large", 5 )
			);
			context.StaffChoices.AddOrUpdate(
				p => p.Title,
				new StaffChoice( "Cook"),
				new StaffChoice( "Music"),
				new StaffChoice( "Prep"),
				new StaffChoice( "Table"),
				new StaffChoice( "Gopher")
			);
		 */
        }
    }
}
