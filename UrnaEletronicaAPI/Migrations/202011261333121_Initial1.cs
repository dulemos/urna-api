namespace UrnaEletronicaAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Votes", "Candidate_Id", "dbo.Candidates");
            DropIndex("dbo.Votes", new[] { "Candidate_Id" });
            DropColumn("dbo.Votes", "Candidate_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Votes", "Candidate_Id", c => c.Int());
            CreateIndex("dbo.Votes", "Candidate_Id");
            AddForeignKey("dbo.Votes", "Candidate_Id", "dbo.Candidates", "Id");
        }
    }
}
