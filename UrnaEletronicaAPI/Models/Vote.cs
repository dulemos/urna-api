using System;
using System.ComponentModel.DataAnnotations;

namespace UrnaEletronicaAPI.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CodigoVoto { get; set; }
        public DateTime DataRegistro { get; set; }
        public int LegendaCandidato { get; set; }
    }
}

namespace UrnaEletronicaAPI.Models
{
    public class VoteDTO
    {
        public int QuantidadeVotos { get; set; }
        public string NomeCompleto { get; set; }
        public int TipoCandidato { get; set; }
        public int Legenda { get; set; }
    }
}

namespace UrnaEletronicaAPI.Models
{
    public class PostVoteDTO
    {
        public string ip { get; set; }
        public string data { get; set; }
        public int legenda { get; set; }
    }
}