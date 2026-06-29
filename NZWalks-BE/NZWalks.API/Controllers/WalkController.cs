using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Services;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private IWalkRepository walkRepository;
        private IMapper mapper;
        private readonly RegionServiceClient regionServiceClient;

        public WalkController(IWalkRepository _walkRepository, IMapper _mapper, RegionServiceClient regionServiceClient)
        {
            walkRepository = _walkRepository;
            mapper = _mapper;
            this.regionServiceClient = regionServiceClient;
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            Walk walk = mapper.Map<Walk>(addWalkRequestDto);


            return Ok(mapper.Map<WalkDto>(await walkRepository.CreateAsync(walk)));
        }
        

        // base url/api/Walk?filterOn=columnName&filterQuery=value&sortBy=columnName&isAscending=boolean&pageNumber=number&pageSize=number
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterON, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNubmer=1, [FromQuery] int pageSize=1000)
        {
            List<Walk> walks = await walkRepository.GetAllAsync(filterON,filterQuery,sortBy,isAscending??true, pageNubmer, pageSize);

            return Ok(mapper.Map<List<WalkDto>>(walks));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await walkRepository.DeleteAsync(id));
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkDto)
        {

            Walk walk = mapper.Map<Walk>(updateWalkDto);
            walk.Id = id;

            return Ok(mapper.Map<WalkDto>(await walkRepository.UpdateAsync(id, walk)));

        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Walk? walk = await walkRepository.GetByIdAsync(id);
            RegionDTO region = await regionServiceClient.GetRegionByIdAsync(walk.RegionId);

            if (walk != null)
            {
                WalkDto walkDto = mapper.Map<WalkDto>(walk);
                walkDto.RegionName = region.Name;
                walkDto.RegionCode = region.Code;
                return Ok(walkDto);
            }
            else
            {
                return NotFound();
            }
        }
    }

}
