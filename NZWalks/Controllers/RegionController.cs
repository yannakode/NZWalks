using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.CustomActionFilters;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.Domain.DTO;
using NZWalks.Models.DTO;
using NZWalks.Repository;
using NZWalks.Repository.Interface;
using System.ComponentModel;
using System.Text.Json;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : Controller
    {
        public readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionController> logger;

        public RegionController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionController> logger)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<ActionResult> GetAllRegions()
        {
             var allRegions = await _regionRepository.ShowAllRegions();

             logger.LogInformation($"Finished GetAllRegions request with data {JsonSerializer.Serialize(allRegions)}");

             return Ok(_mapper.Map<IEnumerable<RegionDTO>>(allRegions));           
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            var region = await _regionRepository.GetRegionById(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RegionDTO>(region));
        }

        [HttpPost]
        [ValidationModel]
        public async Task<ActionResult> CreateRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
                var regionToCreate = _mapper.Map<Region>(addRegionRequestDTO);

                regionToCreate = await _regionRepository.CreateRegion(regionToCreate);

                var regionDto = _mapper.Map<RegionDTO>(regionToCreate);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidationModel]
        public async Task<ActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
                var regionToUpdate = _mapper.Map<Region>(updateRegionRequestDTO);
                regionToUpdate = await _regionRepository.UpdateRegion(id, regionToUpdate);
                if (regionToUpdate == null)
                {
                    NotFound();
                }
                return Ok(_mapper.Map<RegionDTO>(regionToUpdate));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult> DeleteRegion([FromRoute] Guid id)
        {
            if(await _regionRepository.GetRegionById(id) == null)
            {
                ModelState.AddModelError("", "The region doesn't exits");
                return BadRequest(ModelState);
            }
            await _regionRepository.DeleteRegion(id);
            return Ok(true);
        }
    }
}
