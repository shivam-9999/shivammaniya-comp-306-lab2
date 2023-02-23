
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using System;
using System.IO;
using System.Threading.Tasks;



//[ApiController]
public class DeleteAlbunController : ControllerBase
{
    private readonly AlbumDbContext _dbContext;

    public DeleteAlbunController(AlbumDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpDelete("{albumId}")]
    public async Task<IActionResult> DeleteAlbum(Guid albumId)
    {
        // Get the album from the database including its images
        var album = await _dbContext.Albums.Include(a => a.Images).FirstOrDefaultAsync(a => a.Id == albumId);

        if (album == null)
        {
            // Return a 404 Not Found response if the album does not exist
            return NotFound();
        }

        // Remove all the images from S3
                                        //foreach (var image in album.Images)
                                        //{
                                        //    // TODO: Implement S3 delete logic here
                                        //    // This method should delete the image from the S3 bucket
                                        //}

        // Remove the album and all its images from the database
        _dbContext.Albums.Remove(album);
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


