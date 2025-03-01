using _2_Service.Service;
using BusinessObject.Model;
using Microsoft.AspNetCore.Mvc;

namespace _1_SPR25_SWD392_ClothingCustomization.Controllers
{
    [Route("api/designelements")]
    [ApiController]
    public class DesignElementController : Controller
    {
        private readonly IDesignElementService _designElementService;
        public DesignElementController(IDesignElementService designElementService)
        {
            _designElementService = designElementService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DesignElement>>> GetAll()
        {
            return Ok(await _designElementService.GetAllDesignElements());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DesignElement>> GetById(int id)
        {
            var designElement = await _designElementService.GetDesignElementById(id);
            if (designElement == null)
                return NotFound();
            return Ok(designElement);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] DesignElement designElement)
        {
            await _designElementService.AddDesignElement(designElement);
            return CreatedAtAction(nameof(GetById), new { id = designElement.DesignElementId }, designElement);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] DesignElement designElement)
        {
            if (id != designElement.DesignElementId)
                return BadRequest();
            await _designElementService.UpdateDesignElement(designElement);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _designElementService.DeleteDesignElement(id);
            return NoContent();
        }
    }

}
