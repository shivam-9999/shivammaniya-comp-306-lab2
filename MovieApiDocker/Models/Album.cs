using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MovieApi.Models;

namespace MovieApi.Models
{
    public class AlbumCreateRequest
    {
        public string? Name { get; set; }
    }

    public class Album
    {
        [Key]
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Image>? Images { get; set; }
    }


}
