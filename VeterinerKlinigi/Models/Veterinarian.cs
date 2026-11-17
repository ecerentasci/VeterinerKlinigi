namespace VeterinerKlinigi.Models;

public class Veterinarian
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string Specializations { get; set; } = string.Empty;
    public int ClinicId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Clinic Clinic { get; set; } = null!;
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<VeterinarianService> VeterinarianServices { get; set; } = new List<VeterinarianService>();
}
