using System.ComponentModel.DataAnnotations;

namespace WebApiPhoneBook.Models
{
    public class Person:Contact
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; } 

        [Required]
        [MaxLength(50)]
        public string? DocumentNumber { get; set; } 
        public Person()
        {
            ContactType = ContactType.Person;
        }
    }
}
