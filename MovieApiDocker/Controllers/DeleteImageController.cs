
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using System;
using System.IO;
using System.Threading.Tasks;



//[ApiController]
public class DeleteImageController : ControllerBase
{
    private readonly AlbumDbContext _dbContext;

    public DeleteImageController(AlbumDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpDelete("{albumId}/images/{imageId}")]
    public async Task<IActionResult> DeleteImage(Guid albumId, Guid imageId)
    {
        // Get the image from the database
        var image = await _dbContext.Images.FindAsync(imageId);

        if (image == null || image.AlbumId != albumId)
        {
            // Return a 404 Not Found response if the image does not exist or does not belong to the album
            return NotFound();
        }

        // TODO: Implement S3 delete logic here
        // This method should delete the image from the S3 bucket

        // Remove the image from the database
        _dbContext.Images.Remove(image);
        await _dbContext.SaveChangesAsync();

        // Return a standard REST response code for a deleted resource (204 No Content)
        return NoContent();
    }



    //private async Task<string> UploadImageToS3(Stream imageStream)
    //{
    //    // TODO: Implement S3 upload logic here
    //    // This method should upload the image stream to an S3 bucket and return the URL
    //    // of the uploaded image
    //    return "https://s3.amazonaws.com/mybucket/image.jpg";
    //}
}



