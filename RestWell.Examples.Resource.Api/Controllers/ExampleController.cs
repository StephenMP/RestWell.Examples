using Microsoft.AspNetCore.Mvc;
using RestWell.Examples.Resource.Api.Dtos;

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

        [HttpGet("body")]
        public IActionResult GetBody([FromBody]ExampleApiRequestDto exapmleApiRequestDto)
        {
            var exampleResponseDto = new ExampleApiResponseDto { Message = $"The Request Body Message Was -> {exapmleApiRequestDto.Message}" };
            return Ok(exampleResponseDto);
        }
    }
}
