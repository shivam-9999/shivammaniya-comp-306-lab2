using System;
using System.ComponentModel.DataAnnotations.Schema;



namespace MovieApi.Models
{
    // Image model class
    public class Image
    {
        public Guid Id { get; set; }
        //public string? Url { get; set; }
        [ForeignKey("Album")]
        public Guid AlbumId { get; set; }
        public string? Caption { get; set; }
        public Album? Album { get; set; }

    }


    public class ImageUploadRequest
    {
        public string? Caption { get; set; }
        //public Stream? File { get; set; }
    }
}

