namespace UrnaEletronicaAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Legenda = c.Int(nullable: false),
                        NomeCompleto = c.String(nullable: false),
                        NomeVice = c.String(),
                        DataRegistro = c.DateTime(nullable: false),
                        TipoCandidato = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodigoVoto = c.String(nullable: false),
                        DataRegistro = c.DateTime(nullable: false),
                        LegendaCandidato = c.Int(nullable: false),
                        Candidate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Candidates", t => t.Candidate_Id)
                .Index(t => t.Candidate_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "Candidate_Id", "dbo.Candidates");
            DropIndex("dbo.Votes", new[] { "Candidate_Id" });
            DropTable("dbo.Votes");
            DropTable("dbo.Candidates");
        }
    }
}
