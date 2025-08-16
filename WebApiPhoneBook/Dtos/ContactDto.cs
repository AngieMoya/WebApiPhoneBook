using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiPhoneBook.Models;

namespace WebApiPhoneBook.Dtos
{
    public class ContactDto
    {
        public int Id { get; set; }

        [Required]
        //[JsonConverter(typeof(JsonStringEnumConverter))]
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

        // person specific fields

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string? DocumentNumber { get; set; }

        // public organization specific fields 

        [Required]
        [MaxLength(200)]
        public string? PublicOrganizationAddress { get; set; }

        [Required]
        [MaxLength(100)]
        public string? OrganizationName { get; set; }

        // private organization specific fields

        [Required]
        [MaxLength(100)]
        public string? LegalRepresentativeName { get; set; }

        [Required]
        [MaxLength(200)]
        public string? PrivateOrganizationAddress { get; set; }

    }
}
