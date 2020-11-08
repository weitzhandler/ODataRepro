using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace ODataRepro
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class EntitiesController : Controller
    {
        private readonly AppDbContext _Context;

        public EntitiesController(AppDbContext context)
        {
            _Context = context;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Entity>> Get()
        {
            var result = _Context.Entities.AsNoTracking();

            return new ActionResult<IQueryable<Entity>>(result);
        }
    }
}