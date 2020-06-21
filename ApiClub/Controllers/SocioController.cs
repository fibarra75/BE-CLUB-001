using ApiClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiClub.Controllers
{
    public class SocioController : ApiController
    {
        static readonly SocioRepository repo = new SocioRepository();
        // GET: api/Socio
        public IEnumerable<Socio> Get()
        {
            //return new string[] { "value1", "value2" };
            return repo.ListaSocios1();
        }

        // GET: api/Socio/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Socio
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Socio/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Socio/5
        public void Delete(int id)
        {
        }
    }
}
