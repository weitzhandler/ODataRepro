using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Entity entity)
        {
            _Context.Entities.Add(entity);
            await _Context.SaveChangesAsync();

            return Created(entity);
        }

        protected CreatedODataResult<Entity> Created(Entity entity) =>
            new CreatedODataResult<Entity>(entity);
    }
}