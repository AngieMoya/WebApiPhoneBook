using System.ComponentModel.DataAnnotations;

namespace WebApiPhoneBook.Models
{
    public class PrivateOrganization:Contact
    {
        [Required]
        [MaxLength(100)]
        public string? LegalRepresentativeName { get; set; } 

        [Required]
        [MaxLength(200)]
        public string? PrivateOrganizationAddress { get; set; } 
        public PrivateOrganization()
        {
            ContactType = ContactType.PrivateOrganization;
        }
    }
}
