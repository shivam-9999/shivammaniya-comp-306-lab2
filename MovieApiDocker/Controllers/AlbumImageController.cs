using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using System;
using System.IO;
using System.Threading.Tasks;



[ApiController]
[Route("albums/{albumId}/images")]
public class AlbumImagesController : ControllerBase
{
    private readonly AlbumDbContext _dbContext;

    public AlbumImagesController(AlbumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage([FromRoute] Guid albumId, [FromForm] ImageUploadRequest request)
    {
        // Get the album from the database
        var album = await _dbContext.Albums.FindAsync(albumId);

        if (album == null)
        {
            // Return a 404 Not Found response if the album does not exist
            return NotFound();
        }

        // Generate a new GUID for the image ID
        var imageId = Guid.NewGuid();

        // Upload the image file to the S3 bucket and get the URL
        //var imageUrl = await UploadImageToS3(request.File);

        // Create a new Image object with the album ID, caption, and S3 URL
        var newImage = new Image
        {
            Id = imageId,
            AlbumId = albumId,
            Caption = request.Caption,
            //Url = imageUrl
        };

        // Add the new image to the database
        _dbContext.Images.Add(newImage);
        await _dbContext.SaveChangesAsync();

        // Return a standard REST response code for a created resource (201 Created)
        // and include the new image in the response body
        return Created($"/albums/{albumId}/images/{imageId}", newImage);
    }

    //private async Task<string> UploadImageToS3(Stream imageStream)
    //{
    //    // TODO: Implement S3 upload logic here
    //    // This method should upload the image stream to an S3 bucket and return the URL
    //    // of the uploaded image
    //    return "https://s3.amazonaws.com/mybucket/image.jpg";
    //}
}
