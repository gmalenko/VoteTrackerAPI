using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Mvc;
using VoteTrackerAPI.Business;
using VoteTrackerAPI.Services.Cache;

namespace VoteTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelfRegistrationController : Controller
    {
        private SelfRegistration voterRegistration = null;
        public SelfRegistrationController(ICacheOperation cacheOperation)
        {
            voterRegistration = new SelfRegistration(cacheOperation);
        }


        [HttpGet]
        [ResponseType(typeof(IEnumerable<Models.Database.VoteSelfRegistration>))]
        public async Task<IActionResult> GetRegisteredVoters()
        {
            try
            {
                return Json(await voterRegistration.GetVoterRegistrations());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        [ResponseType(typeof(Models.Database.VoteSelfRegistration))]
        public async Task<IActionResult> GetVoterRegistration([FromBody] Models.Database.VoteSelfRegistration voteRegistration)
        {
            try
            {
                return Json(await voterRegistration.CreateVoterRegistration(voteRegistration));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        [ResponseType(typeof(Models.Database.VoteSelfRegistration))]
        public async Task<IActionResult> UpdateVoterRegistration([FromBody] Models.Database.VoteSelfRegistration voteRegistration)
        {
            try
            {
                return Json(await voterRegistration.UpdateVoterRegistration(voteRegistration));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
