namespace VeterinerKlinigi.Models;

public class Clinic
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<Veterinarian> Veterinarians { get; set; } = new List<Veterinarian>();
    public ICollection<Service> Services { get; set; } = new List<Service>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
