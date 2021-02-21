using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoteTrackerAPI.Business;
using VoteTrackerAPI.Database.DBContext;
using VoteTrackerAPI.Services.Cache;

namespace VoteTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteCandidateController : Controller
    {
        private VoteCandidate voteCandidate = null;
        public VoteCandidateController(ICacheOperation cacheOperation) //: base(toafcContextOptions, cacheOperation)
        {
            voteCandidate = new VoteCandidate(cacheOperation);
        }
        //public VoteCandidateController()
        //{
        //    string a = "";
        //    voteCandidate = new VoteCandidate();
        //}

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Models.Database.VoteCandidate>))]
        public async Task<IActionResult> GetCandidates()
        {
            try
            {             
                return Json( await voteCandidate.GetCandidates());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        [ResponseType(typeof(Models.Database.VoteCandidate))]
        public async Task<IActionResult> CreateCandidate([FromBody] Models.Database.VoteCandidate candidate)
        {
            try
            {
                return Json(await voteCandidate.CreateCandidate(candidate));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        [ResponseType(typeof(Models.Database.VoteCandidate))]
        public async Task<IActionResult> UpdateCandidate([FromBody] Models.Database.VoteCandidate candidate)
        {
            try
            {
                return Json(await voteCandidate.UpdateCandidate(candidate));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }


    }
}
