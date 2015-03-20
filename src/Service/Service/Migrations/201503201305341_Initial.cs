namespace Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Receptors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReadCount = c.Int(nullable: false),
                        Percentage = c.Int(nullable: false),
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
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Receptors");
        }
    }
}
