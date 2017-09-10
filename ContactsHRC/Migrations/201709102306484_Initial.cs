namespace ContactsHRC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.EmailAddresses",
                c => new
                    {
                        EmailAddressId = c.Int(nullable: false, identity: true),
                        EmailAddressValue = c.String(),
                        Contact_ContactId = c.Int(),
                    })
                .PrimaryKey(t => t.EmailAddressId)
                .ForeignKey("dbo.Contacts", t => t.Contact_ContactId)
                .Index(t => t.Contact_ContactId);
            
            CreateTable(
                "dbo.PhoneNumbers",
                c => new
                    {
                        PhoneNumberId = c.Int(nullable: false, identity: true),
                        PhoneNumberValue = c.String(),
                        Contact_ContactId = c.Int(),
                    })
                .PrimaryKey(t => t.PhoneNumberId)
                .ForeignKey("dbo.Contacts", t => t.Contact_ContactId)
                .Index(t => t.Contact_ContactId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.TagContacts",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Contact_ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Contact_ContactId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Contacts", t => t.Contact_ContactId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Contact_ContactId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagContacts", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.TagContacts", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.PhoneNumbers", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.EmailAddresses", "Contact_ContactId", "dbo.Contacts");
            DropIndex("dbo.TagContacts", new[] { "Contact_ContactId" });
            DropIndex("dbo.TagContacts", new[] { "Tag_TagId" });
            DropIndex("dbo.PhoneNumbers", new[] { "Contact_ContactId" });
            DropIndex("dbo.EmailAddresses", new[] { "Contact_ContactId" });
            DropTable("dbo.TagContacts");
            DropTable("dbo.Tags");
            DropTable("dbo.PhoneNumbers");
            DropTable("dbo.EmailAddresses");
            DropTable("dbo.Contacts");
        }
    }
}
