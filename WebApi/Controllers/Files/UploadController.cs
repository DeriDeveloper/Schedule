using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebApi.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace WebApi.Controllers.Files
{
    [Route("api/files/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private DBContext _context;
       
        public UploadController(DBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromQuery] string accessToken, [FromForm] IFormFile file)
        {
            try
            {
                if (accessToken is null)
                {
                    return BadRequest(new { message = "Не корректный accessToken!" });
                }

                var userAccessToken = _context.UserAccessTokens.FirstOrDefault(x => x.Token == accessToken);

                if (userAccessToken is null)
                {
                    return BadRequest(new { message = "accessToken не валидный!" });
                }


                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file found in the request.");
                }

                try
                {

                    var user = _context.Users.Find(userAccessToken.UserId);

                    if(user is null)
                    {
                        return NotFound("User not found!");
                    }

                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);

                        var fileMetadatum = _context.FileMetadata.Include(x=>x.File).FirstOrDefault(x => x.Id == user.AvatarProfileFileMetadatumId);

                        bool addingFileMetadatum = false;

                        if (fileMetadatum is null)
                        {
                            fileMetadatum = new Entities.FileMetadatum()
                            {
                                File = new Entities.File()
                            };
                            addingFileMetadatum = true;
                        }

                        var fileNameParts = file.FileName.Split(".");
                        var expansion = fileNameParts.Last();

                        var nameFile = Regex.Replace(Services.HashService.Hash(Guid.NewGuid().ToString()), "[^A-Za-z0-9\\-_]", "") + $".{expansion}";


                        fileMetadatum.File.Data = ms.ToArray();
                        fileMetadatum.MimeType = file.ContentType;
                        fileMetadatum.Name = nameFile;

                        if (addingFileMetadatum)
                        {
                            _context.FileMetadata.Add(fileMetadatum);
                        }

                        _context.SaveChanges();

                        user.AvatarProfileFileMetadatumId = fileMetadatum.Id;

                        _context.SaveChanges();
                    }
                    

                    

                    return NoContent();
                }
                catch (Exception ex)
                {
                    // Обработка ошибки при сохранении файла
                    // ...

                    return StatusCode(500, "An error occurred while saving the file.");
                }

                /*var fileBytes = new List<byte[]>();

                foreach (var file in Request.Form.Files)
                {
                    if (file == null || file.Length == 0)
                    {
                        return BadRequest("No file uploaded.");
                    }
                    using(var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        fileBytes.Add(ms.ToArray());
                    }
                }

                var combinedBytes = fileBytes.SelectMany(bytes => bytes).ToArray();

                System.IO.File.WriteAllBytes(Directory.GetCurrentDirectory() + "/1.png", combinedBytes);

                */
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred during file upload: " + ex.Message);
            }
        }
    }
}
