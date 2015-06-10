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
    public class DSegmentsController : ApiController
    {
        private ServiceContext db = new ServiceContext();

        // GET api/DSegments
        public IQueryable<DSegment> GetDSegments()
        {
            return db.DSegments;
        }

        // GET api/DSegments/5
        [ResponseType(typeof(DSegment))]
        public async Task<IHttpActionResult> GetDSegment(Guid id)
        {
            DSegment dsegment = await db.DSegments.FindAsync(id);
            if (dsegment == null)
            {
                return NotFound();
            }

            return Ok(dsegment);
        }

        // PUT api/DSegments/5
        public async Task<IHttpActionResult> PutDSegment(Guid id, DSegment dsegment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dsegment.Id)
            {
                return BadRequest();
            }

            db.Entry(dsegment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DSegmentExists(id))
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

        // POST api/DSegments
        [ResponseType(typeof(DSegment))]
        public async Task<IHttpActionResult> PostDSegment(DSegment dsegment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DSegments.Add(dsegment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dsegment.Id }, dsegment);
        }

        // DELETE api/DSegments/5
        [ResponseType(typeof(DSegment))]
        public async Task<IHttpActionResult> DeleteDSegment(Guid id)
        {
            DSegment dsegment = await db.DSegments.FindAsync(id);
            if (dsegment == null)
            {
                return NotFound();
            }

            db.DSegments.Remove(dsegment);
            await db.SaveChangesAsync();

            return Ok(dsegment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DSegmentExists(Guid id)
        {
            return db.DSegments.Count(e => e.Id == id) > 0;
        }
    }
}