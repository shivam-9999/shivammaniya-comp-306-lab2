using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;

[ApiController]
[Route("[controller]")]


public class AlbumsController : ControllerBase
{
    private readonly AlbumDbContext _dbContext;

    public AlbumsController(AlbumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAlbum([FromBody] AlbumCreateRequest request)
    {
        // Create new album with the provided name
        var newAlbum = new Album
        {
            Name = request.Name
        };

        // Save the new album to a database or other storage mechanism
        _dbContext.Albums.Add(newAlbum);
        await _dbContext.SaveChangesAsync();

        // Return a standard REST response code for a created resource (201 Created)
        // and include the new album in the response body
        return Created($"/albums/{newAlbum.Id}", newAlbum);
    }
}