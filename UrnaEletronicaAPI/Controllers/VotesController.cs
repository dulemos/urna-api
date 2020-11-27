using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using UrnaEletronicaAPI.Models;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http.Cors;
namespace UrnaEletronicaAPI.Controllers
{
    [RoutePrefix("api")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VotesController : ApiController
    {
        private UrnaEletronicaAPIContext db = new UrnaEletronicaAPIContext();
        [Route("GetVotes", Name = "GetVotes")]
        [HttpGet]
        // GET: api/Votes
        public IQueryable<VoteDTO> GetVotes()
        {
            //var votes = db.Candidates.SqlQuery(" SELECT COUNT(v.LegendaCandidato) as QuantidadeVotos, c.NomeCompleto, c.Legenda, c.TipoCandidato, c.Id, c.NomeVice FROM Candidates as c JOIN Votes as v ON c.Legenda = v.LegendaCandidato GROUP BY  c.NomeCompleto, c.Legenda, c.TipoCandidato, c.Id, c.NomeVice");
            //List<VoteDTO> countVotes = votes

            var votes = db.Candidates.Select(c =>
                new VoteDTO()
                {
                    QuantidadeVotos = db.Votes.Where(v => v.LegendaCandidato == c.Legenda).Count(),
                    NomeCompleto = c.NomeCompleto,
                    TipoCandidato = c.TipoCandidato,
                    Legenda = c.Legenda
                });
            

            return votes;
        }



        [Route("PostVotes", Name = "PostVotes")]
        [HttpPost]
        // POST: api/Votes
        [ResponseType(typeof(Vote))]
        public async Task<IHttpActionResult> PostVote([FromBody] PostVoteDTO body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vote = new Vote()
            {
                DataRegistro = DateTime.Now,
                LegendaCandidato = body.legenda,
                CodigoVoto = MD5Hash(body.ip + body.data)
            };


            db.Votes.Add(vote);
            await db.SaveChangesAsync();

            return CreatedAtRoute("PostVotes", new { id = vote.Id }, vote);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5 md5 = MD5.Create();

            byte[] bytes = md5.ComputeHash(new UTF8Encoding().GetBytes(input));
            for(int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}