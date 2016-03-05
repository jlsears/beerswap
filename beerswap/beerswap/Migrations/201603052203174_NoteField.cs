namespace beerswap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Swaps", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Swaps", "Note");
        }
    }
}
