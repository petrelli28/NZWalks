using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("walkDifficulty")]
    public class WalkDifficultyController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalkDifficultyController(IMapper mapper, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.mapper = mapper;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDificultyAsync()
        {
            var walkDifficulty = await walkDifficultyRepository.GetAllAsync();

            var walkDifficultyDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficulty);

            return Ok(walkDifficulty);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDificultyAsync")]
        public async Task<IActionResult> GetWalkDificultyAsync(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDificultyAsync(AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code
            };

            walkDifficulty = await walkDifficultyRepository.AddAsync(walkDifficulty);

            var walkDifficultyDTO = new Models.Domain.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };

            return CreatedAtAction(nameof(GetWalkDificultyAsync), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDificultyAsync([FromRoute] Guid id, [FromBody] UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code
            };

            walkDifficulty = await walkDifficultyRepository.UpdateAsync(id, walkDifficulty);

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = new Models.Domain.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };

            return Ok(walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDificultyAsync(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.DeleteAsync(id);

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            return Ok(walkDifficulty);
        }
    }
}
