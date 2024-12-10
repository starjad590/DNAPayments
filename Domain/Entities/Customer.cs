using Domain.Abstractions;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public sealed class Customer : Entity<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation property
    public ICollection<Account>? Accounts { get; set; }

    private Customer()
    {
            
    }

    private Customer(string firstName, string lastName, string email, string phoneNumber, string address)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        CreatedAt = DateTime.Now;
    }

    public static Customer Create(string firstName, string lastName, string email, string phoneNumber, string address)
        => new Customer(firstName, lastName, email, phoneNumber, address);
}
