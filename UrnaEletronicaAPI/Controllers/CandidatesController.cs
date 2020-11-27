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
using System.Web.Http.Cors;

namespace UrnaEletronicaAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class CandidatesController : ApiController
    {
        private UrnaEletronicaAPIContext db = new UrnaEletronicaAPIContext();

        [Route("GetCandidate", Name = "GetAll")]
        // GET: api/Candidates
        public IQueryable<Candidate> GetCandidates()
        {
            return db.Candidates;
        }
        [Route("GetCandidate/{legenda:int}", Name = "GetCandidateByLegenda")]
        // GET: api/Candidates/5
        [ResponseType(typeof(Candidate))]
        public async Task<IHttpActionResult> GetCandidate(int legenda)
        {
            var candidate = db.Candidates.Where(c => c.Legenda == legenda);
            if (candidate.Count() == 0)
            {
                return NotFound();
            }

            return Ok(candidate);
        }
        [Route("EditCandidate/{legenda:int}",Name = "EditCandidate")]
        [HttpPut]
        // PUT: api/Candidates/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCandidate(int legenda, Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (legenda != candidate.Legenda)
            {
                return BadRequest();
            }

            db.Entry(candidate).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(candidate.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Candidates
        [Route("PostCandidate", Name = "PostCandidate")]
        [HttpPost]
        [ResponseType(typeof(Candidate))]
        public async Task<IHttpActionResult> PostCandidate([FromBody] Candidate candidate)
        {
            candidate.DataRegistro = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var candidates = db.Candidates.Where(c => c.Legenda == candidate.Legenda);
            //verifica se legenda já foi utilizada
            if (candidates.Count() == 0)
            {
                
                db.Candidates.Add(candidate);
                await db.SaveChangesAsync();

                return CreatedAtRoute("PostCandidate", new { legenda = candidate.Legenda }, candidate);
            }

            return BadRequest("Legenda já foi utilizada");

        }

        // DELETE: api/Candidates/5
        [HttpDelete]
        [Route("DeleteCandidate/{id:int}", Name = "Delete Candidate")]
        [ResponseType(typeof(Candidate))]
        public async Task<IHttpActionResult> DeleteCandidate(int id)
        {
            Candidate candidate = await db.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            db.Candidates.Remove(candidate);
            await db.SaveChangesAsync();

            return Ok(candidate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CandidateExists(int id)
        {
            return db.Candidates.Count(e => e.Id == id) > 0;
        }
    }
}