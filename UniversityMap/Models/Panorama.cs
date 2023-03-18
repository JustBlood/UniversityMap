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
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
        public int MapId { get; set; }
        public Map? Map { get; set; }
    }
}
