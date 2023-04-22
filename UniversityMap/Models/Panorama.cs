using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UniversityMap.Models
{
    [Index("Tag", IsUnique = true, Name ="Tag_Index")]
    public class Panorama
    {
        public int Id { get; set; }
        public string? Tag { get; set; }
        public byte[]? Image { get; set; }
        // To-do сделать стороны Тегами или ключами к записям.
        public string? Left { get; set; }
        public string? Top { get; set; }
        public string? Right { get; set; }
        public string? Bottom { get; set; }
        public int MapId { get; set; }
        public string? PanoramaOptionsScript { get; set; }
        public Map? Map { get; set; }
    }
}
