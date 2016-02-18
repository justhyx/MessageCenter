namespace XXY.MessageCenter.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FailedMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MESSAGECENTER.FAILED_MESSAGE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MSG_ID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MSG_TYPE = c.Decimal(nullable: false, precision: 3, scale: 0),
                        LOG = c.String(maxLength: 1000),
                        CREATE_ON = c.DateTime(nullable: false),
                        CREATE_BY_USER_NAME = c.String(nullable: false, maxLength: 20),
                        CREATE_BY_USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MODIFY_ON = c.DateTime(),
                        MODIFY_BY_USER_NAME = c.String(maxLength: 20),
                        MODIFY_BY_USER_ID = c.Decimal(precision: 18, scale: 2),
                        IS_DELETED = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("MESSAGECENTER.FAILED_MESSAGE");
        }
    }
}
