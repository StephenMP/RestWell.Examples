using Microsoft.AspNetCore.Mvc;
using RestWell.Examples.Resource.Api.Response;

namespace RestWell.Examples.Resource.Api.Controllers
{
    [Route("api/[controller]")]
    public class ExampleController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromQuery]int? appendId)
        {
            if (appendId.HasValue)
            {
                return Ok(new string[] { $"value1-{id}-{appendId.Value}", $"value2-{id}-{appendId.Value}" });
            }

            return Ok(new string[] { $"value1-{id}", $"value2-{id}" });
        }

        [HttpPost]
        public IActionResult Post([FromBody]ExampleApiResponse messageRequestDto)
        {
            if (ModelState.IsValid && messageRequestDto != null)
            {
                var messageResponseDto = new ExampleApiResponse();
                return Ok(messageResponseDto);
            }

            return BadRequest();
        }
    }
}
