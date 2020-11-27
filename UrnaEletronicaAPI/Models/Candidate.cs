using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
namespace UrnaEletronicaAPI.Models
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }
        public int Legenda { get; set; }
        [Required]
        public string NomeCompleto { get; set; }
        public string NomeVice { get; set; }
        public DateTime DataRegistro { get; set; }
        public int TipoCandidato { get; set; }
    }
}