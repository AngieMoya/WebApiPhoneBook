using System.ComponentModel.DataAnnotations;

namespace WebApiPhoneBook.Models
{
    public abstract class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required ContactType ContactType { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; } = string.Empty;

        [Required]
        [Phone]
        [MaxLength(20)]
        public required string PhoneNumber { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Comments { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
