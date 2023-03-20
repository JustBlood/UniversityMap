namespace UniversityMap.Models
{
    public class FileUpload
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
        public string? Tag { get; set; }
        public string? Left { get; set; }
        public string? Top { get; set; }
        public string? Right { get; set; }
        public string? Bottom { get; set; }
        public int MapId { get; set; }

        public Panorama CreatePanorama()
        {
            if (Image == null)
                return null;
            var binaryReader = new BinaryReader(Image.OpenReadStream());
            var imageData = binaryReader.ReadBytes((int)Image.Length);
            return new Panorama
            {
                Id = Id,
                Tag = Tag,
                Left = Left,
                Top = Top,
                Right = Right,
                Bottom = Bottom,
                MapId = MapId,
                Image = imageData
            };
        }
    }
}
