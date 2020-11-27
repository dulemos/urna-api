namespace UrnaEletronicaAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UrnaEletronicaAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<UrnaEletronicaAPI.Models.UrnaEletronicaAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UrnaEletronicaAPI.Models.UrnaEletronicaAPIContext context)
        {
            context.Candidates.AddOrUpdate(x => x.Id,
                new Candidate() { Legenda = 50, NomeCompleto = "Eduardo Mendes", NomeVice = "Eduardo Lemos", DataRegistro = DateTime.Now, TipoCandidato = 1 },
                new Candidate() { Legenda = 50000, NomeCompleto = "Eduardo Santos", DataRegistro = DateTime.Now, TipoCandidato = 2 });
            context.Votes.AddOrUpdate(x => x.Id,
                new Vote() { LegendaCandidato = 50, DataRegistro = DateTime.Now, CodigoVoto = "97b4b4545f558b3d27cebf0aa8b8109e" });
        }
    }
}
