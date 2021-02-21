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
    public class VoterTalleyController : Controller
    {
        private VoterTally voterTally = null;

        public VoterTalleyController(ICacheOperation cacheOperation)
        {
            voterTally = new VoterTally(cacheOperation);
        }

        [HttpGet]
        [Route("GetVoteSummary")]
        public async Task<IActionResult> GetVoteSummary()
        {
            try
            {
                return Json(await voterTally.GetVoterSummaries());
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Models.VoterTalley>))]
        public async Task<IActionResult> GetVoterTallies()
        {
            try
            {
                await voterTally.GetVoterSummaries();
                return Json(await voterTally.GetVoterTallies());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        [ResponseType(typeof(Models.VoterTalley))]
        public async Task<IActionResult> InsertVoterTalley([FromBody] Models.VoterTalley voter)
        {
            try
            {
                return Json(await voterTally.InsertVoterTally(voter));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }



    }
}
