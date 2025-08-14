using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPhoneBook.Context;
using WebApiPhoneBook.Dtos;
using WebApiPhoneBook.Models;

namespace WebApiPhoneBook.Controllers
{
    [Route("api/contacts")] //[controller]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContacts()
        {
            var contacts = await _context.Contacts.ToListAsync();
            var contactDtos = contacts.Select(MapToDto).ToList();
            return Ok(contactDtos);
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(MapToDto(contact));
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ContactDto>> UpdateContact(int id, UpdateContactDto updateDto)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            // Update common fields
            contact.Name = updateDto.Name;
            contact.PhoneNumber = updateDto.PhoneNumber;
            contact.Comments = updateDto.Comments;

            // Update type-specific fields
            switch (contact)
            {
                case Person personContact:
                    personContact.Email = updateDto.Email ?? string.Empty;
                    personContact.DocumentNumber = updateDto.DocumentNumber ?? string.Empty;
                    break;

                case PublicOrganization publicOrgContact:
                    publicOrgContact.PublicOrganizationAddress = updateDto.PublicOrganizationAddress ?? string.Empty;
                    publicOrgContact.OrganizationName = updateDto.OrganizationName ?? string.Empty;
                    break;

                case PrivateOrganization privateOrgContact:
                    privateOrgContact.LegalRepresentativeName = updateDto.LegalRepresentativeName ?? string.Empty;
                    privateOrgContact.PrivateOrganizationAddress = updateDto.PrivateOrganizationAddress ?? string.Empty;
                    break;
            }

            await _context.SaveChangesAsync();

            return Ok(MapToDto(contact));
        }

        [HttpPost]
        public async Task<ActionResult<ContactDto>> PostContact(CreateContactDto createDto)
        {
            Contact newContact = createDto.ContactType switch
            {
                ContactType.Person => new Person
                {
                    ContactType = ContactType.Person,
                    Name = createDto.Name,
                    PhoneNumber = createDto.PhoneNumber,
                    Comments = createDto.Comments,
                    Email = createDto.Email ?? string.Empty,
                    DocumentNumber = createDto.DocumentNumber ?? string.Empty
                },
                ContactType.PublicOrganization => new PublicOrganization
                {
                    ContactType = ContactType.PublicOrganization,
                    Name = createDto.Name,
                    PhoneNumber = createDto.PhoneNumber,
                    Comments = createDto.Comments,
                    PublicOrganizationAddress = createDto.PublicOrganizationAddress ?? string.Empty,
                    OrganizationName = createDto.OrganizationName ?? string.Empty
                },
                ContactType.PrivateOrganization => new PrivateOrganization
                {
                    ContactType = ContactType.PrivateOrganization,
                    Name = createDto.Name,
                    PhoneNumber = createDto.PhoneNumber,
                    Comments = createDto.Comments,
                    LegalRepresentativeName = createDto.LegalRepresentativeName ?? string.Empty,
                    PrivateOrganizationAddress = createDto.PrivateOrganizationAddress ?? string.Empty
                },
                _ => throw new ArgumentException("Invalid contact type")
            };

            _context.Contacts.Add(newContact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContact), new { id = newContact.Id }, MapToDto(newContact));
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
        // Helper method to map entity to DTO
        private ContactDto MapToDto(Contact contact)
        {
            var dto = new ContactDto
            {
                Id = contact.Id,
                ContactType = contact.ContactType,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Comments = contact.Comments,
                CreatedAt = contact.CreatedAt,
                UpdatedAt = contact.UpdatedAt
            };

            switch (contact)
            {
                case Person personContact:
                    dto.Email = personContact.Email;
                    dto.DocumentNumber = personContact.DocumentNumber;
                    break;

                case PublicOrganization publicOrgContact:
                    dto.PublicOrganizationAddress = publicOrgContact.PublicOrganizationAddress;
                    dto.OrganizationName = publicOrgContact.OrganizationName;
                    break;

                case PrivateOrganization privateOrgContact:
                    dto.LegalRepresentativeName = privateOrgContact.LegalRepresentativeName;
                    dto.PrivateOrganizationAddress = privateOrgContact.PrivateOrganizationAddress;
                    break;
            }

            return dto;
        }
    }
}

