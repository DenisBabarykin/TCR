namespace TCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DSegments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Alleles = c.String(nullable: false),
                        CDR3_Position = c.Int(nullable: false),
                        FullNucleoSequence = c.String(),
                        NucleoSequence = c.String(),
                        NucleoSequenceP = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Alleles, unique: true, name: "SegmentIndex");
            
            CreateTable(
                "dbo.Receptors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NucleoSequence = c.String(nullable: false),
                        AminoSequence = c.String(nullable: false),
                        LastVNucleoPos = c.Int(nullable: false),
                        FirstDNucleoPos = c.Int(nullable: false),
                        LastDNucleoPos = c.Int(nullable: false),
                        FirstJNucleoPos = c.Int(nullable: false),
                        VDInsertions = c.Int(nullable: false),
                        DJInsertions = c.Int(nullable: false),
                        TotalInsertions = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.NucleoSequence, unique: true, name: "ReceptorIndex");
            
            CreateTable(
                "dbo.JSegments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Alleles = c.String(nullable: false),
                        CDR3_Position = c.Int(nullable: false),
                        FullNucleoSequence = c.String(),
                        NucleoSequence = c.String(),
                        NucleoSequenceP = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Alleles, unique: true, name: "SegmentIndex");
            
            CreateTable(
                "dbo.PersonalReceptors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReadCount = c.Int(nullable: false),
                        Percentage = c.Double(nullable: false),
                        ReceptorId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Receptors", t => t.ReceptorId, cascadeDelete: true)
                .Index(t => t.ReceptorId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VSegments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Alleles = c.String(nullable: false),
                        CDR3_Position = c.Int(nullable: false),
                        FullNucleoSequence = c.String(),
                        NucleoSequence = c.String(),
                        NucleoSequenceP = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Alleles, unique: true, name: "SegmentIndex");
            
            CreateTable(
                "dbo.JSegmentsReceptors",
                c => new
                    {
                        SegmentId = c.Guid(nullable: false),
                        ReceptorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SegmentId, t.ReceptorId })
                .ForeignKey("dbo.JSegments", t => t.SegmentId, cascadeDelete: true)
                .ForeignKey("dbo.Receptors", t => t.ReceptorId, cascadeDelete: true)
                .Index(t => t.SegmentId)
                .Index(t => t.ReceptorId);
            
            CreateTable(
                "dbo.VSegmentsReceptors",
                c => new
                    {
                        SegmentId = c.Guid(nullable: false),
                        ReceptorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SegmentId, t.ReceptorId })
                .ForeignKey("dbo.VSegments", t => t.SegmentId, cascadeDelete: true)
                .ForeignKey("dbo.Receptors", t => t.ReceptorId, cascadeDelete: true)
                .Index(t => t.SegmentId)
                .Index(t => t.ReceptorId);
            
            CreateTable(
                "dbo.DSegmentsReceptors",
                c => new
                    {
                        SegmentId = c.Guid(nullable: false),
                        ReceptorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SegmentId, t.ReceptorId })
                .ForeignKey("dbo.DSegments", t => t.SegmentId, cascadeDelete: true)
                .ForeignKey("dbo.Receptors", t => t.ReceptorId, cascadeDelete: true)
                .Index(t => t.SegmentId)
                .Index(t => t.ReceptorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DSegmentsReceptors", "ReceptorId", "dbo.Receptors");
            DropForeignKey("dbo.DSegmentsReceptors", "SegmentId", "dbo.DSegments");
            DropForeignKey("dbo.VSegmentsReceptors", "ReceptorId", "dbo.Receptors");
            DropForeignKey("dbo.VSegmentsReceptors", "SegmentId", "dbo.VSegments");
            DropForeignKey("dbo.PersonalReceptors", "ReceptorId", "dbo.Receptors");
            DropForeignKey("dbo.PersonalReceptors", "PersonId", "dbo.People");
            DropForeignKey("dbo.JSegmentsReceptors", "ReceptorId", "dbo.Receptors");
            DropForeignKey("dbo.JSegmentsReceptors", "SegmentId", "dbo.JSegments");
            DropIndex("dbo.DSegmentsReceptors", new[] { "ReceptorId" });
            DropIndex("dbo.DSegmentsReceptors", new[] { "SegmentId" });
            DropIndex("dbo.VSegmentsReceptors", new[] { "ReceptorId" });
            DropIndex("dbo.VSegmentsReceptors", new[] { "SegmentId" });
            DropIndex("dbo.JSegmentsReceptors", new[] { "ReceptorId" });
            DropIndex("dbo.JSegmentsReceptors", new[] { "SegmentId" });
            DropIndex("dbo.VSegments", "SegmentIndex");
            DropIndex("dbo.PersonalReceptors", new[] { "PersonId" });
            DropIndex("dbo.PersonalReceptors", new[] { "ReceptorId" });
            DropIndex("dbo.JSegments", "SegmentIndex");
            DropIndex("dbo.Receptors", "ReceptorIndex");
            DropIndex("dbo.DSegments", "SegmentIndex");
            DropTable("dbo.DSegmentsReceptors");
            DropTable("dbo.VSegmentsReceptors");
            DropTable("dbo.JSegmentsReceptors");
            DropTable("dbo.VSegments");
            DropTable("dbo.People");
            DropTable("dbo.PersonalReceptors");
            DropTable("dbo.JSegments");
            DropTable("dbo.Receptors");
            DropTable("dbo.DSegments");
        }
    }
}
