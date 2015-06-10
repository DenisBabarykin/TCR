namespace Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Receptors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReadCount = c.Int(nullable: false),
                        Percentage = c.Double(nullable: false),
                        NucleoSequence = c.String(),
                        AminoSequence = c.String(),
                        VSegments = c.String(),
                        JSegments = c.String(),
                        DSegments = c.String(),
                        LastVNucleoPos = c.Int(nullable: false),
                        FirstDNucleoPos = c.Int(nullable: false),
                        LastDNucleoPos = c.Int(nullable: false),
                        FirstJNucleoPos = c.Int(nullable: false),
                        VDInsertions = c.Int(nullable: false),
                        DJInsertions = c.Int(nullable: false),
                        TotalInsertions = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receptors", "Id", "dbo.People");
            DropIndex("dbo.Receptors", new[] { "Id" });
            DropTable("dbo.Receptors");
            DropTable("dbo.People");
        }
    }
}
