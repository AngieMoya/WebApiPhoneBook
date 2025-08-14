using System.ComponentModel.DataAnnotations;

namespace WebApiPhoneBook.Models
{
    public class PublicOrganization:Contact
    {
        [Required]
        [MaxLength(200)]
        public string? PublicOrganizationAddress { get; set; }

        [Required]
        [MaxLength(100)]
        public string? OrganizationName { get; set; } 
        public PublicOrganization()
        {
            ContactType = ContactType.PublicOrganization;
        }
    }
}
