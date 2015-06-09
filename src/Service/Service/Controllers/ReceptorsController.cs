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
using Service.Models;

namespace Service.Controllers
{
    public class ReceptorsController : ApiController
    {
        private ServiceContext db = new ServiceContext();

        // GET api/Receptors
        public IQueryable<Receptor> GetReceptors()
        {
            return db.Receptors;
        }

        // GET api/Receptors/5
        [ResponseType(typeof(Receptor))]
        public async Task<IHttpActionResult> GetReceptor(int id)
        {
            Receptor receptor = await db.Receptors.FindAsync(id);
            if (receptor == null)
            {
                return NotFound();
            }

            return Ok(receptor);
        }

        // PUT api/Receptors/5
        public async Task<IHttpActionResult> PutReceptor(int id, Receptor receptor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != receptor.ReceptorId)
            {
                return BadRequest();
            }

            db.Entry(receptor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceptorExists(id))
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

        // POST api/Receptors
        [ResponseType(typeof(Receptor))]
        public async Task<IHttpActionResult> PostReceptor(Receptor receptor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Receptors.Add(receptor);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = receptor.ReceptorId }, receptor);
        }

        // DELETE api/Receptors/5
        [ResponseType(typeof(Receptor))]
        public async Task<IHttpActionResult> DeleteReceptor(int id)
        {
            Receptor receptor = await db.Receptors.FindAsync(id);
            if (receptor == null)
            {
                return NotFound();
            }

            db.Receptors.Remove(receptor);
            await db.SaveChangesAsync();

            return Ok(receptor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReceptorExists(int id)
        {
            return db.Receptors.Count(e => e.ReceptorId == id) > 0;
        }
    }
}