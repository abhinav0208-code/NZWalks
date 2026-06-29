
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Services
{
    public class RegionServiceClient
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RegionServiceClient(HttpClient _httpClient, IHttpContextAccessor httpContextAccessor)
        {
            httpClient = _httpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<RegionDTO> GetRegionByIdAsync(Guid id)
        {
            var token= httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Replace("Bearer", ""));
                

            var response = await httpClient.GetAsync($"api/Regions/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<RegionDTO>();
        }
    }
}
