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
    public class VSegmentsController : ApiController
    {
        private ServiceContext db = new ServiceContext();

        // GET api/VSegments
        public IQueryable<VSegment> GetVSegments()
        {
            return db.VSegments;
        }

        // GET api/VSegments/5
        [ResponseType(typeof(VSegment))]
        public async Task<IHttpActionResult> GetVSegment(Guid id)
        {
            VSegment vsegment = await db.VSegments.FindAsync(id);
            if (vsegment == null)
            {
                return NotFound();
            }

            return Ok(vsegment);
        }

        // PUT api/VSegments/5
        public async Task<IHttpActionResult> PutVSegment(Guid id, VSegment vsegment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vsegment.Id)
            {
                return BadRequest();
            }

            db.Entry(vsegment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VSegmentExists(id))
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

        // POST api/VSegments
        [ResponseType(typeof(VSegment))]
        public async Task<IHttpActionResult> PostVSegment(VSegment vsegment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VSegments.Add(vsegment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = vsegment.Id }, vsegment);
        }

        // DELETE api/VSegments/5
        [ResponseType(typeof(VSegment))]
        public async Task<IHttpActionResult> DeleteVSegment(Guid id)
        {
            VSegment vsegment = await db.VSegments.FindAsync(id);
            if (vsegment == null)
            {
                return NotFound();
            }

            db.VSegments.Remove(vsegment);
            await db.SaveChangesAsync();

            return Ok(vsegment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VSegmentExists(Guid id)
        {
            return db.VSegments.Count(e => e.Id == id) > 0;
        }
    }
}