using System.ComponentModel.DataAnnotations;

namespace WebApiPhoneBook.Models
{
    public enum ContactType
    {
        [Display(Name = "Person")]
        Person = 1,
        [Display(Name = "Public Organization")]
        PublicOrganization = 2,
        [Display(Name = "Private Organization")]
        PrivateOrganization = 3,
    }
}