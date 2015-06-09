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
    public class SegmentController : ApiController
    {
        private ServiceContext db = new ServiceContext();

        // GET api/Segment
        public IQueryable<Segment> GetSegments()
        {
            return db.Segments;
        }

        // GET api/Segment/5
        [ResponseType(typeof(Segment))]
        public async Task<IHttpActionResult> GetSegment(int id)
        {
            Segment segment = await db.Segments.FindAsync(id);
            if (segment == null)
            {
                return NotFound();
            }

            return Ok(segment);
        }

        // PUT api/Segment/5
        public async Task<IHttpActionResult> PutSegment(int id, Segment segment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != segment.SegmentId)
            {
                return BadRequest();
            }

            db.Entry(segment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SegmentExists(id))
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

        // POST api/Segment
        [ResponseType(typeof(Segment))]
        public async Task<IHttpActionResult> PostSegment(Segment segment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Segments.Add(segment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = segment.SegmentId }, segment);
        }

        // DELETE api/Segment/5
        [ResponseType(typeof(Segment))]
        public async Task<IHttpActionResult> DeleteSegment(int id)
        {
            Segment segment = await db.Segments.FindAsync(id);
            if (segment == null)
            {
                return NotFound();
            }

            db.Segments.Remove(segment);
            await db.SaveChangesAsync();

            return Ok(segment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SegmentExists(int id)
        {
            return db.Segments.Count(e => e.SegmentId == id) > 0;
        }
    }
}