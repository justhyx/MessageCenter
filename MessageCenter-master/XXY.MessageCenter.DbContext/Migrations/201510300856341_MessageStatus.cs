namespace XXY.MessageCenter.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("MESSAGECENTER.E_MAIL_MESSAGE", "STATUS", c => c.Decimal(nullable: false, precision: 3, scale: 0));
            AddColumn("MESSAGECENTER.QQ_MESSAGE", "STATUS", c => c.Decimal(nullable: false, precision: 3, scale: 0));
            AddColumn("MESSAGECENTER.SMS_MESSAGE", "STATUS", c => c.Decimal(nullable: false, precision: 3, scale: 0));
            AddColumn("MESSAGECENTER.TXT_MESSAGE", "STATUS", c => c.Decimal(nullable: false, precision: 3, scale: 0));
            AddColumn("MESSAGECENTER.WE_CHAT_MESSAGE", "STATUS", c => c.Decimal(nullable: false, precision: 3, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("MESSAGECENTER.WE_CHAT_MESSAGE", "STATUS");
            DropColumn("MESSAGECENTER.TXT_MESSAGE", "STATUS");
            DropColumn("MESSAGECENTER.SMS_MESSAGE", "STATUS");
            DropColumn("MESSAGECENTER.QQ_MESSAGE", "STATUS");
            DropColumn("MESSAGECENTER.E_MAIL_MESSAGE", "STATUS");
        }
    }
}
