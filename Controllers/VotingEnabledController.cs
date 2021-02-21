using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoteTrackerAPI.Business;
using VoteTrackerAPI.Services.Cache;

namespace VoteTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotingEnabledController : Controller
    {
        private VotingEnabled votingEnabled = null;
        public VotingEnabledController(ICacheOperation cacheOperation)
        {
            votingEnabled = new VotingEnabled(cacheOperation);
        }

        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                return Json(await votingEnabled.GetStatus());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus([FromBody] Boolean input)
        {
            try
            {

                return Json(await votingEnabled.UpdateFlag(input));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }


    }
}
