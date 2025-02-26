using System.ComponentModel.DataAnnotations;

namespace GlassLewisAssessment.domain.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Exchange { get; set; }
        [Required]
        public required string Ticker { get; set; }
        [Required]
        public required string Isin { get; set; }
        public string? Website { get; set; }
    }
}