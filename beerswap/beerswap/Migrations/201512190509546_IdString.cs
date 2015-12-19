namespace beerswap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Swaps", "BeerPostingID", "dbo.BeerPostings");
            DropIndex("dbo.Swaps", new[] { "BeerPostingID" });
            DropPrimaryKey("dbo.BeerPostings");
            AddColumn("dbo.Swaps", "BeerPosting_BeerPostingID", c => c.String(maxLength: 128));
            AlterColumn("dbo.BeerPostings", "BeerPostingID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.BeerPostings", "BeerPostingID");
            CreateIndex("dbo.Swaps", "BeerPosting_BeerPostingID");
            AddForeignKey("dbo.Swaps", "BeerPosting_BeerPostingID", "dbo.BeerPostings", "BeerPostingID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Swaps", "BeerPosting_BeerPostingID", "dbo.BeerPostings");
            DropIndex("dbo.Swaps", new[] { "BeerPosting_BeerPostingID" });
            DropPrimaryKey("dbo.BeerPostings");
            AlterColumn("dbo.BeerPostings", "BeerPostingID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Swaps", "BeerPosting_BeerPostingID");
            AddPrimaryKey("dbo.BeerPostings", "BeerPostingID");
            CreateIndex("dbo.Swaps", "BeerPostingID");
            AddForeignKey("dbo.Swaps", "BeerPostingID", "dbo.BeerPostings", "BeerPostingID", cascadeDelete: true);
        }
    }
}
