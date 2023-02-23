using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using System;
using System.IO;
using System.Threading.Tasks;


[ApiController]
[Route("[controller]")]

public class GetImageForAlbum : ControllerBase
{
    private readonly AlbumDbContext _dbContext;

    public GetImageForAlbum(AlbumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("/albums/{albumId}/images/{imageId}")]
    public async Task<IActionResult> GetImage(Guid albumId, Guid imageId)
    {
        // Get the image from the database
        var image = await _dbContext.Images.FindAsync(imageId);

        if (image == null || image.AlbumId != albumId)
        {
            // Return a 404 Not Found response if the image does not exist or does not belong to the album
            return NotFound();
        }

        // Construct the response object with the image information
        var responseObject = new
        {
            id = imageId,
            //url = $"https://{s3BucketName}.s3.amazonaws.com/{image.FileName}",
            albumId = albumId,
            caption = image.Caption
        };

        // Return the response object as JSON
        return Ok(responseObject);
    }


}

