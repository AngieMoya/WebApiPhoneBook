using System.ComponentModel.DataAnnotations;

namespace WebApiPhoneBook.Dtos
{
    public class UpdateContactDto: IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;    
        [Required]
        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? Comments { get; set; }
        // person specific fields
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }
        [MaxLength(50)]
        public string? DocumentNumber { get; set; }
        // public organization specific fields
        [MaxLength(200)]
        public string? PublicOrganizationAddress { get; set; }
        [MaxLength(100)]
        public string? OrganizationName { get; set; }
        // private organization specific fields
        [MaxLength(100)]
        public string? LegalRepresentativeName { get; set; }
        [MaxLength(200)]
        public string? PrivateOrganizationAddress { get; set; }

        // validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
           
            return results;
        }

    }
}
