using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Database.DBContext;
using VoteTrackerAPI.Services.Cache;

namespace VoteTrackerAPI.Controllers
{
    public class BaseController : Controller
    {
        public DbContextOptions<toafcContext> _toafcContext;
        public ICacheOperation _cacheOperation;
        public BaseController(DbContextOptions<toafcContext> toafcContext = null, ICacheOperation cacheOperation = null)
        {
            _toafcContext = toafcContext;
            _cacheOperation = cacheOperation;
        }
    }
}
