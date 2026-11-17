using Microsoft.AspNetCore.Identity;

namespace VeterinerKlinigi.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber2 { get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; set; }

    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
