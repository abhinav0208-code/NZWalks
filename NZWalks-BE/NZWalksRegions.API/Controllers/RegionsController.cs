using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalksRegions.API.Models.Domain;
using NZWalksRegions.API.Models.DTO;
using NZWalksRegions.API.Repositories;

namespace NZWalksRegions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        private IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository _regionRepository, IMapper mapper, ILogger<RegionsController> _logger)
        {
            this.regionRepository = _regionRepository;
            this.mapper = mapper;
            logger = _logger;
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
                logger.LogWarning("get method called");
                List<Region> regionEntityList = await regionRepository.GetAllAsync();

                //mapping list of region to list of regionDTO using automapper( configured in AutoMapperProfiles)
                return Ok(mapper.Map<List<RegionDTO>>(regionEntityList));

        }

        // get region by Id
        // GET: https://localhost:7163/api/Regions/{id}  will be executed if this is the request url.
        [HttpGet]
        [Authorize(Roles ="Reader, Writer")]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // get region domain model/entity from database.
            Region? region = await regionRepository.GetById(id);
            if (region == null)
            {
                return NotFound();
            }
            else
            {
                //map region domain model/entity to region dto.
                return Ok(mapper.Map<RegionDTO>(region));
            }
        }

        // POST: https://localhost:7163/api/Region this is the url, requesting which will get this end point executed.
        [HttpPost]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {

            // Map or Convert DTO to Domain model/entity.
            // not providing value for id field since , AddAsync method of EF core provides it for us (if we don't).
            Region region = mapper.Map<Region>(addRegionRequestDTO);

            //Use datacontext to udpate region table using Region domain model/ entity
            await regionRepository.CreateAsync(region); // updating the _nzWalksDbContext instance with new data.

            //create region dto to send it as response
            RegionDTO regionDTO = mapper.Map<RegionDTO>(region);

            return CreatedAtAction(nameof(CreateRegion), regionDTO);

        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {

            Region newRegion = mapper.Map<Region>(updateRegionDTO);
            //assinging value of Id explicitly since mapper is not configured to do so.
            newRegion.Id = id;

            Region region = await regionRepository.UpdateAsync(id, newRegion);

            if (region == null)
            {
                return NotFound();
            }

            RegionDTO responseRegionDTO = mapper.Map<RegionDTO>(region);
            return Ok(responseRegionDTO);


        }


        // DELETE: https://localhost:7361/api/region/{id}
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles ="Writer,Reader")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            Region? regionToBeDeleted = await regionRepository.DeleteAsync(id);
            if (regionToBeDeleted == null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
    }
}
