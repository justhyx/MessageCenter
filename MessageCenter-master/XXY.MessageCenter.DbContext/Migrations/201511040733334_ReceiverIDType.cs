namespace XXY.MessageCenter.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReceiverIDType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("MESSAGECENTER.TXT_MESSAGE", "SENDER_ID", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("MESSAGECENTER.TXT_MESSAGE", "RECEIVER_ID", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("MESSAGECENTER.TXT_MESSAGE", "RECEIVER_ID", c => c.Double(nullable: false));
            AlterColumn("MESSAGECENTER.TXT_MESSAGE", "SENDER_ID", c => c.Double(nullable: false));
        }
    }
}
