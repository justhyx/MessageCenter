namespace XXY.MessageCenter.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MESSAGECENTER.E_MAIL_MESSAGE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        CC = c.String(),
                        BCC = c.String(),
                        SUBJECT = c.String(nullable: false, maxLength: 100),
                        PRI = c.Decimal(nullable: false, precision: 3, scale: 0),
                        CTX = c.String(nullable: false),
                        RECEIVER = c.String(nullable: false),
                        CREATE_ON = c.DateTime(nullable: false),
                        CREATE_BY_USER_NAME = c.String(nullable: false, maxLength: 20),
                        CREATE_BY_USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MODIFY_ON = c.DateTime(),
                        MODIFY_BY_USER_NAME = c.String(maxLength: 20),
                        MODIFY_BY_USER_ID = c.Decimal(precision: 18, scale: 2),
                        IS_DELETED = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "MESSAGECENTER.QQ_MESSAGE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        RECEIVER = c.String(nullable: false, maxLength: 20),
                        CTX = c.String(nullable: false, maxLength: 1000),
                        PRI = c.Decimal(nullable: false, precision: 3, scale: 0),
                        CREATE_ON = c.DateTime(nullable: false),
                        CREATE_BY_USER_NAME = c.String(nullable: false, maxLength: 20),
                        CREATE_BY_USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MODIFY_ON = c.DateTime(),
                        MODIFY_BY_USER_NAME = c.String(maxLength: 20),
                        MODIFY_BY_USER_ID = c.Decimal(precision: 18, scale: 2),
                        IS_DELETED = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "MESSAGECENTER.SMS_MESSAGE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        RECEIVER = c.String(nullable: false, maxLength: 20),
                        CTX = c.String(nullable: false, maxLength: 200),
                        PRI = c.Decimal(nullable: false, precision: 3, scale: 0),
                        CREATE_ON = c.DateTime(nullable: false),
                        CREATE_BY_USER_NAME = c.String(nullable: false, maxLength: 20),
                        CREATE_BY_USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MODIFY_ON = c.DateTime(),
                        MODIFY_BY_USER_NAME = c.String(maxLength: 20),
                        MODIFY_BY_USER_ID = c.Decimal(precision: 18, scale: 2),
                        IS_DELETED = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "MESSAGECENTER.TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        CODE = c.String(nullable: false, maxLength: 30),
                        MSG_TYPE = c.Decimal(nullable: false, precision: 3, scale: 0),
                        LANG = c.Decimal(nullable: false, precision: 3, scale: 0),
                        APP_CODE = c.String(nullable: false, maxLength: 30),
                        IS_DEFAULT = c.Decimal(nullable: false, precision: 1, scale: 0),
                        SUBJECT = c.String(nullable: false, maxLength: 200),
                        CTX = c.String(),
                        CREATE_ON = c.DateTime(nullable: false),
                        CREATE_BY_USER_NAME = c.String(nullable: false, maxLength: 20),
                        CREATE_BY_USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MODIFY_ON = c.DateTime(),
                        MODIFY_BY_USER_NAME = c.String(maxLength: 20),
                        MODIFY_BY_USER_ID = c.Decimal(precision: 18, scale: 2),
                        IS_DELETED = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.CODE)
                .Index(t => t.MSG_TYPE)
                .Index(t => t.LANG)
                .Index(t => t.APP_CODE);
            
            CreateTable(
                "MESSAGECENTER.TXT_MESSAGE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SUBJECT = c.String(nullable: false, maxLength: 100),
                        READED = c.Decimal(nullable: false, precision: 1, scale: 0),
                        SENDER = c.String(nullable: false, maxLength: 20),
                        SENDER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RECEIVER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RECEIVER = c.String(nullable: false, maxLength: 20),
                        CTX = c.String(nullable: false, maxLength: 1000),
                        PRI = c.Decimal(nullable: false, precision: 3, scale: 0),
                        CREATE_ON = c.DateTime(nullable: false),
                        CREATE_BY_USER_NAME = c.String(nullable: false, maxLength: 20),
                        CREATE_BY_USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MODIFY_ON = c.DateTime(),
                        MODIFY_BY_USER_NAME = c.String(maxLength: 20),
                        MODIFY_BY_USER_ID = c.Decimal(precision: 18, scale: 2),
                        IS_DELETED = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "MESSAGECENTER.WE_CHAT_MESSAGE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        RECEIVER = c.String(nullable: false, maxLength: 100),
                        PRI = c.Decimal(nullable: false, precision: 3, scale: 0),
                        CTX = c.String(nullable: false),
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
            DropIndex("MESSAGECENTER.TEMPLATE", new[] { "APP_CODE" });
            DropIndex("MESSAGECENTER.TEMPLATE", new[] { "LANG" });
            DropIndex("MESSAGECENTER.TEMPLATE", new[] { "MSG_TYPE" });
            DropIndex("MESSAGECENTER.TEMPLATE", new[] { "CODE" });
            DropTable("MESSAGECENTER.WE_CHAT_MESSAGE");
            DropTable("MESSAGECENTER.TXT_MESSAGE");
            DropTable("MESSAGECENTER.TEMPLATE");
            DropTable("MESSAGECENTER.SMS_MESSAGE");
            DropTable("MESSAGECENTER.QQ_MESSAGE");
            DropTable("MESSAGECENTER.E_MAIL_MESSAGE");
        }
    }
}
