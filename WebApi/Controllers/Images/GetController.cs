using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

namespace WebApi.Controllers.Images
{
    [Route("api/images/[controller]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        private DBContext _context;

        public GetController(DBContext context)
        {
            _context = context;
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> Get(string fileName)
        {
            var fileMetadata = _context.FileMetadata.FirstOrDefault(x => x.Name == fileName);

            if (fileMetadata is null)
            {
                return NotFound();
            }

            fileMetadata.File = _context.Files.Find(fileMetadata.FileId);

            if (fileMetadata.MimeType == "image/jpeg" || fileMetadata.MimeType == "image/png")
            {
                if (fileMetadata.File is not null)
                {
                    var stream = new MemoryStream(fileMetadata.File.Data);

                    var contentType = fileMetadata.MimeType;

                    return File(stream, contentType);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

    }
}
