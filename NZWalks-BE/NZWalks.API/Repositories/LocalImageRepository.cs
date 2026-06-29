using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDBContext nZWalksDBContext;

        public IWebHostEnvironment webHostEnvironment { get; }

        public LocalImageRepository(IWebHostEnvironment _webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksDBContext nZWalksDBContext)
        {
            webHostEnvironment = _webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", image.FileName + image.FileExtension);

            //Upload image to local path

            // 'using' automatically calls fileStream.Dispose() when the execution leaves the current scope( when function execution is done).
            using FileStream fileStream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(fileStream);

            string scheme = httpContextAccessor.HttpContext.Request.Scheme;
            string host = httpContextAccessor.HttpContext.Request.Host.ToString();
            string pathBase = httpContextAccessor.HttpContext.Request.PathBase.ToString();




            //https://localhost:1234/Images/Image.jpg
            //creating urlPath
            var urlFilePath = $"{scheme}://{host}{pathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            await nZWalksDBContext.Images.AddAsync(image);
            await nZWalksDBContext.SaveChangesAsync();

            return image;
        }
    }
}
