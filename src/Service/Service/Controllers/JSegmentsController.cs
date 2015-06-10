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
    public class JSegmentsController : ApiController
    {
        private ServiceContext db = new ServiceContext();

        // GET api/JSegments
        public IQueryable<JSegment> GetJSegments()
        {
            return db.JSegments;
        }

        // GET api/JSegments/5
        [ResponseType(typeof(JSegment))]
        public async Task<IHttpActionResult> GetJSegment(Guid id)
        {
            JSegment jsegment = await db.JSegments.FindAsync(id);
            if (jsegment == null)
            {
                return NotFound();
            }

            return Ok(jsegment);
        }

        // PUT api/JSegments/5
        public async Task<IHttpActionResult> PutJSegment(Guid id, JSegment jsegment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jsegment.Id)
            {
                return BadRequest();
            }

            db.Entry(jsegment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JSegmentExists(id))
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

        // POST api/JSegments
        [ResponseType(typeof(JSegment))]
        public async Task<IHttpActionResult> PostJSegment(JSegment jsegment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JSegments.Add(jsegment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = jsegment.Id }, jsegment);
        }

        // DELETE api/JSegments/5
        [ResponseType(typeof(JSegment))]
        public async Task<IHttpActionResult> DeleteJSegment(Guid id)
        {
            JSegment jsegment = await db.JSegments.FindAsync(id);
            if (jsegment == null)
            {
                return NotFound();
            }

            db.JSegments.Remove(jsegment);
            await db.SaveChangesAsync();

            return Ok(jsegment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JSegmentExists(Guid id)
        {
            return db.JSegments.Count(e => e.Id == id) > 0;
        }
    }
}