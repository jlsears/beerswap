namespace beerswap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteField2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Swaps", "SwapNote", c => c.String());
            DropColumn("dbo.Swaps", "Note");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Swaps", "Note", c => c.String());
            DropColumn("dbo.Swaps", "SwapNote");
        }
    }
}
