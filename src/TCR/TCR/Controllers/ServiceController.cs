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
using TCR.Models;

namespace TCR.Controllers
{
    [Authorize]
    public class ServiceController : ApiController
    {
        private TCRContext db = new TCRContext();

        [Route("api/Service/GetLengthDiv/{id}")]
        public IEnumerable<int[]> GetLengthDiv(int id)
        {
            var result = new List<int[]>();
            result.Add(new int[2] { 50, 400 });
            result.Add(new int[2] { 51, 460 });
            result.Add(new int[2] { 49, 1120 });
            result.Add(new int[2] { 52, 500 + id });

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}