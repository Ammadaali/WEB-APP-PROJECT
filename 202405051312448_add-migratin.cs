namespace Web_Project_Tour_and_Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigratin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TblLogin",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TblPackage",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Offer = c.String(nullable: false, maxLength: 40),
                        Image = c.String(),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TblPayment",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        paymentMode = c.String(),
                        RegId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.TblReg", t => t.RegId, cascadeDelete: true)
                .Index(t => t.RegId);
            
            CreateTable(
                "dbo.TblReg",
                c => new
                    {
                        RegId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Email = c.String(nullable: false, maxLength: 20),
                        Offers = c.String(nullable: false, maxLength: 40),
                        Service = c.String(nullable: false, maxLength: 40),
                        cellNo = c.String(nullable: false, maxLength: 11),
                        NIC = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.RegId);
            
            CreateTable(
                "dbo.TblServices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Service = c.String(nullable: false, maxLength: 40),
                        Image = c.String(),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TblPayment", "RegId", "dbo.TblReg");
            DropIndex("dbo.TblPayment", new[] { "RegId" });
            DropTable("dbo.TblServices");
            DropTable("dbo.TblReg");
            DropTable("dbo.TblPayment");
            DropTable("dbo.TblPackage");
            DropTable("dbo.TblLogin");
        }
    }
}
