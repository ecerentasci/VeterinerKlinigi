namespace VeterinerKlinigi.Models;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public decimal Price { get; set; }
    public int ClinicId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Clinic Clinic { get; set; } = null!;
    public ICollection<VeterinarianService> VeterinarianServices { get; set; } = new List<VeterinarianService>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
