namespace VeterinerKlinigi.Models;

public enum AppointmentStatus
{
    Pending,
    Approved,
    Rejected,
    Completed,
    Cancelled
}

public class Appointment
{
    public int Id { get; set; }
    public int PetId { get; set; }
    public int VeterinarianId { get; set; }
    public int ServiceId { get; set; }
    public int ClinicId { get; set; }
    public string OwnerId { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public string? ExaminationNotes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Pet Pet { get; set; } = null!;
    public Veterinarian Veterinarian { get; set; } = null!;
    public Service Service { get; set; } = null!;
    public Clinic Clinic { get; set; } = null!;
    public ApplicationUser Owner { get; set; } = null!;
}
