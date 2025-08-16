using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using WebApiPhoneBook.Models;

namespace WebApiPhoneBook.Dtos
{
    public class CreateContactDto: IValidatableObject
    {
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
    
        //validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            switch (ContactType)
            {
                case ContactType.Person:
                    if (string.IsNullOrWhiteSpace(Email))
                        results.Add(new ValidationResult("Email is required for Person contacts.", new[] { nameof(Email) }));
                    if (string.IsNullOrWhiteSpace(DocumentNumber))
                        results.Add(new ValidationResult("DocumentNumber is required for Person contacts.", new[] { nameof(DocumentNumber) }));
                    break;

                case ContactType.PublicOrganization:
                    if (string.IsNullOrWhiteSpace(PublicOrganizationAddress))
                        results.Add(new ValidationResult("PublicOrganizationAddress is required for Public Organization contacts.", new[] { nameof(PublicOrganizationAddress) }));
                    if (string.IsNullOrWhiteSpace(OrganizationName))
                        results.Add(new ValidationResult("OrganizationName is required for Public Organization contacts.", new[] { nameof(OrganizationName) }));
                    break;

                case ContactType.PrivateOrganization:
                    if (string.IsNullOrWhiteSpace(LegalRepresentativeName))
                        results.Add(new ValidationResult("LegalRepresentativeName is required for Private Organization contacts.", new[] { nameof(LegalRepresentativeName) }));
                    if (string.IsNullOrWhiteSpace(PrivateOrganizationAddress))
                        results.Add(new ValidationResult("PrivateCompanyAddress is required for Private Organization contacts.", new[] { nameof(PrivateOrganizationAddress) }));
                    break;
            }

            return results;
        }
    }
}
