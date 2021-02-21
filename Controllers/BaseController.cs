using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Database.DBContext;

namespace VoteTrackerAPI.Controllers
{
    public class BaseController : Controller
    {
        public DbContextOptions<toafcContext> _toafcContext;
        public BaseController(DbContextOptions<toafcContext> toafcContext = null)
        {
            _toafcContext = toafcContext;
        }
    }
}
