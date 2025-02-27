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

        public static bool IsIsinValid(string Isin)
        {
            if (string.IsNullOrEmpty(Isin) || Isin.Length < 2) return false;

            return char.IsLetter(Isin[0]) && char.IsLetter(Isin[1]);
        }
    }
}