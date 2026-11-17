namespace VeterinerKlinigi.Models;

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string? Breed { get; set; }
    public DateTime DateOfBirth { get; set; }
    public decimal? Weight { get; set; }
    public string? PhotoUrl { get; set; }
    public string OwnerId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ApplicationUser Owner { get; set; } = null!;
    public ICollection<VaccineRecord> VaccineRecords { get; set; } = new List<VaccineRecord>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
