using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoteTrackerAPI.Business;
using VoteTrackerAPI.Services.Cache;

namespace VoteTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotePeriodController : Controller
    {

        private VotePeriod votePeriod = null;
        public VotePeriodController(ICacheOperation cacheOperation) //: base(toafcContextOptions, cacheOperation)
        {
            votePeriod = new VotePeriod(cacheOperation);
        }
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Models.Database.VotePeriod>))]
        public async Task<IActionResult> GetVotePeriod()
        {
            try
            {
                return Json(await votePeriod.GetCurrentVotePeriod());
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

    }
}
