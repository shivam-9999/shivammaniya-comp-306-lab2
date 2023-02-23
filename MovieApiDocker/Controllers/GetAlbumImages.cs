using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using System;
using System.IO;
using System.Threading.Tasks;


[ApiController]
[Route("[controller]")]

public class GetAlbumsImage : ControllerBase
{
    private readonly AlbumDbContext _dbContext;

    public GetAlbumsImage(AlbumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("/albums/{albumId}")]
    public async Task<IActionResult> GetAlbumImages(Guid albumId)
    {
        // Get the album from the database
        var album = await _dbContext.Albums.FindAsync(albumId);

        if (album == null)
        {
            // Return a 404 Not Found response if the album does not exist
            return NotFound();
        }

        // Get all the images within the album from the database
        var images = await _dbContext.Images
            .Where(i => i.AlbumId == albumId)
            .Select(i => new
            {
                id = i.Id,
                //url = i.Url,
                caption = i.Caption
            })
            .ToListAsync();

        // Return a standard REST response with the album ID, name, and images
        return Ok(new
        {
            id = album.Id,
            name = album.Name,
            images = images
        });
    }


}

