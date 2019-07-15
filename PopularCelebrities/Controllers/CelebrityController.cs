using PopularCelebrities.BL;
using PopularCelebrities.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace PopularCelebrities.Controllers
{
    public class CelebrityController : ApiController
    {
        ICelebsBl celebsBl;
        public CelebrityController(ICelebsBl celebsBl)
        {
            this.celebsBl = celebsBl;
        }

        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Celebrity>))]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                IEnumerable<Celebrity> celebsData = await Task.Run(() => celebsBl.GetData());
                return Ok(celebsData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }
        }

        [Route("reset")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Celebrity>))]
        public async Task<IHttpActionResult> Reset()
        {
            try
            {
                IEnumerable<Celebrity> newData = await Task.Run(() => celebsBl.InitData());
                return Ok(newData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }
        }

        [Route("DeleteCelebrity")]
        [HttpPost]
        public HttpResponseMessage DeleteCelebrity([FromBody]string name)
        {

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, celebsBl.RemoveCelebrity(name));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("AddCelebrity")]
        [HttpPost]
        public HttpResponseMessage AddCelebrity([FromBody]Celebrity celebrity)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, celebsBl.AddCelebrityToJsonFile(celebrity));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("UpdateCelebrity")]
        [HttpPost]
        public HttpResponseMessage UpdateCelebrity([FromBody]Celebrity celebrity, string name)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    celebsBl.UpdateCelebrity(name,
                    celebrity));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}