using System.ComponentModel.DataAnnotations;

namespace CryptoInfoApi.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Code { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string ChineseName { get; set; } = string.Empty;
    }
}
