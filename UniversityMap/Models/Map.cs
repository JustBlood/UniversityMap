namespace UniversityMap.Models
{
    public class Map
    {
        public int Id { get; set; }
        public char Building { get; set; }
        public int Floor { get; set; }
        public string? SvgMap { get; set; }
        public string? JQueryScript { get; set; }
    }
}
