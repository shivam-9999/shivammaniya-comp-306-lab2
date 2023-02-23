using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using System;
using System.IO;
using System.Threading.Tasks;


[ApiController]
[Route("[controller]")]


public class GetAlbums : ControllerBase
{
    private readonly AlbumDbContext _dbContext;

    public GetAlbums(AlbumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("albums")]
    public async Task<IActionResult> GetAllAlbums()
    {
        var albums = await _dbContext.Albums.ToListAsync();

        var response = albums.Select(album => new
        {
            albumId = album.Id,
            albumName = album.Name
        });

        return Ok(response);
    }

}