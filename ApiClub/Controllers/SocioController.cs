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
            return repo.ListaSocios();
        }

        // GET: api/Socio/5
        [HttpGet]
        [Route("api/socio/{rut}")]
        public Socio Get(int rut)
        {
            return repo.ObtenerSocio(rut);
        }

        // POST: api/Socio
        public void Post([FromBody] Socio socio)
        {

            repo.CrearSocio(socio);
        }

        // PUT: api/Socio/5
        [HttpPut]
        [Route("api/socio/{rut}")]
        public void Put(int rut, [FromBody]Socio socio)
        {
            //El verbo HTTP PUT no es sportado naturalmente por el Web API

            repo.ModificarSocio(rut, socio);
        }

        // DELETE: api/Socio/5
        [HttpDelete]
        [Route("api/socio/{rut}")]
        public void Delete(int rut)
        {
            repo.EliminarSocio(rut);
        }
    }
}
