using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repository.Interface;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        public readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalkController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var walkList = await _walkRepository.ShowAllWalks();
            return Ok(_mapper.Map<WalkDTO>(walkList));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await _walkRepository.GetWalkById(id);
            return Ok(_mapper.Map<WalkDTO>(walk));
        }

        [HttpPost]
        public async Task<ActionResult> CreateWalk([FromBody] AddWalkRequestDTO walkRequestDTO)
        {
            var walkToCreate = _mapper.Map<Walk>(walkRequestDTO);
            await _walkRepository.CreateWalk(walkToCreate);
            var walkDTO = _mapper.Map<WalkDTO>(walkToCreate);
            return Ok(walkDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<ActionResult> UpdateWask(Guid id, UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var walkToUpdate = _mapper.Map<Walk>(updateRegionRequestDTO);
            await _walkRepository.UpdateWalk(id, walkToUpdate);
            if (walkToUpdate == null)
            {
                NotFound();
            }
            return Ok(_mapper.Map<WalkDTO>(walkToUpdate));
        }
    }
}
