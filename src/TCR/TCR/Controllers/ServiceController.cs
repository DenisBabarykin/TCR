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
    //[Authorize]
    public class ServiceController : ApiController
    {
        private TCRContext db = new TCRContext();

        [Route("api/Service/GetLengthDiv/{id}")]
        public IEnumerable<int[]> GetLengthDiv(int id)
        {
            var personalReceptors = db.PersonalReceptors.Where(rec => rec.PersonId == id).Include(p => p.Receptor);
            int[] len = new int[100];
            foreach (var pr in personalReceptors)
            {
                len[pr.Receptor.NucleoSequence.Length] += pr.ReadCount;
            }

            var result = new List<int[]>();
            for (int i = 1; i < 100; ++i)
                if (len[i] > 0)
                    result.Add(new int[2] { i, len[i] });
            return result;
        }

        [Route("api/Service/GetVsegDiv/{id}")]
        public IEnumerable<KeyValuePair<string, int>> GetVsegDiv(int id)
        {
            var receptors = db.PersonalReceptors
                .Where(rec => rec.PersonId == id)
                .Include(p => p.Receptor)
                .Select(p => p.Receptor)
                .Include(p => p.VSegments);
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var rec in receptors)
            {
                foreach (var vseg in rec.VSegments)
                    if (dic.ContainsKey(vseg.Alleles))
                        dic[vseg.Alleles] += 1;
                    else
                        dic.Add(vseg.Alleles, 1);
            }
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int> >();
            foreach (KeyValuePair<string, int> kv in dic)
                result.Add(kv);

            return result;
        }

        [Route("api/Service/GetDsegDiv/{id}")]
        public IEnumerable<KeyValuePair<string, int>> GetDsegDiv(int id)
        {
            var receptors = db.PersonalReceptors
                .Where(rec => rec.PersonId == id)
                .Include(p => p.Receptor)
                .Select(p => p.Receptor)
                .Include(p => p.DSegments);
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var rec in receptors)
            {
                foreach (var dseg in rec.DSegments)
                    if (dic.ContainsKey(dseg.Alleles))
                        dic[dseg.Alleles] += 1;
                    else
                        dic.Add(dseg.Alleles, 1);
            }
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
            foreach (KeyValuePair<string, int> kv in dic)
                result.Add(kv);

            return result;
        }

        [Route("api/Service/GetJsegDiv/{id}")]
        public IEnumerable<KeyValuePair<string, int>> GetJsegDiv(int id)
        {
            var receptors = db.PersonalReceptors
                .Where(rec => rec.PersonId == id)
                .Include(p => p.Receptor)
                .Select(p => p.Receptor)
                .Include(p => p.JSegments);
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var rec in receptors)
            {
                foreach (var jseg in rec.JSegments)
                    if (dic.ContainsKey(jseg.Alleles))
                        dic[jseg.Alleles] += 1;
                    else
                        dic.Add(jseg.Alleles, 1);
            }
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
            foreach (KeyValuePair<string, int> kv in dic)
                result.Add(kv);

            return result;
        }

        [Route("api/Service/GetClones")]
        public HeatmapDataContainer GetClones()
        {
            HeatmapDataContainer res = new HeatmapDataContainer();
            int count = db.People.Count();
            for (int i = 1; i <= count; ++i)
                res.Repertoires.Add("Rep. " + i.ToString());

            int[] amount = new int[count];
            for (int i = 0; i < count; ++i)
                amount[i] = db.PersonalReceptors.Count(p => p.PersonId == i + 1);

            for (int i = 0; i < count - 1; ++i)
                for (int j = i + 1; j < count; ++j)
                {
                    double intersections = db.PersonalReceptors
                        .Count(prec => prec.PersonId == i + 1 && db.PersonalReceptors
                            .Any(prec2 => prec.ReceptorId == prec2.ReceptorId && prec2.PersonId == j + 1));
                    intersections /= amount[i];
                    intersections /= amount[j];
                    intersections *= 10e6;
                    intersections = Math.Round(intersections, 3);
                    res.Data.Add(new double[3] { i, j, intersections });
                    res.Data.Add(new double[3] { j, i, intersections });
                }

            return res;
        }

        [Route("api/Service/GetCountClones")]
        public HeatmapDataContainer GetCountClones()
        {
            HeatmapDataContainer res = new HeatmapDataContainer();
            int count = db.People.Count();
            for (int i = 1; i <= count; ++i)
                res.Repertoires.Add("Rep. " + i.ToString());

            for (int i = 0; i < count - 1; ++i)
                for (int j = i + 1; j < count; ++j)
                {
                    double intersections = db.PersonalReceptors
                        .Count(prec => prec.PersonId == i + 1 && db.PersonalReceptors
                            .Any(prec2 => prec.ReceptorId == prec2.ReceptorId && prec2.PersonId == j + 1));
                    intersections = Math.Round(intersections, 3);
                    res.Data.Add(new double[3] { i, j, intersections });
                    res.Data.Add(new double[3] { j, i, intersections });
                }

            return res;
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

    public class HeatmapDataContainer
    {
        public List<string> Repertoires { get; set; }

        public List<double[]> Data { get; set; }

        public HeatmapDataContainer()
        {
            Repertoires = new List<string>();
            Data = new List<double[]>();
        }
    };

    class PersonalReceptorComparer : IEqualityComparer<PersonalReceptor>
    {
        public bool Equals(PersonalReceptor x, PersonalReceptor y)
        {

            if (x.ReceptorId == y.ReceptorId)
                return true;
            else
                return false;
        }

        public int GetHashCode(PersonalReceptor rec)
        {
            //if (Object.ReferenceEquals(rec, null)) return 0;
            return rec.ReceptorId.GetHashCode();
        }
    }
}