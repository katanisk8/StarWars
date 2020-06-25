using CodePepper.Domain;
using CodePepper.Domain.Business.StarWars;
using CodePepper.Domain.Dtos;
using CodePepper.Domain.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CodePepper.WebUi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StarWarsController : ControllerBase
    {
        private readonly IStarWarsService _starWarsService;

        public StarWarsController(IStarWarsService starWarsService)
        {
            _starWarsService = starWarsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<CharacterDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] Paging paging) =>
            Ok(await _starWarsService.GetAllAsync(paging).ConfigureAwait(false));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([Required] int id)
        {
            CharacterDto result = await _starWarsService.GetAsync(id).ConfigureAwait(false);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound($"Character with id: {id} not exists");
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        public Task<OperationResult> Post(CharacterDto dto) => _starWarsService.AddAsync(dto);

        [HttpPut]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        public Task<OperationResult> Put(CharacterDto dto) => _starWarsService.UpdateAsync(dto);

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        public Task<OperationResult> Delete([Required] int id) => _starWarsService.DeleteAsync(id);
    }
}
