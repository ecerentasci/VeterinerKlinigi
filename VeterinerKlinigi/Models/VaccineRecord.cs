namespace VeterinerKlinigi.Models;

public class VaccineRecord
{
    public int Id { get; set; }
    public int PetId { get; set; }
    public string VaccineName { get; set; } = string.Empty;
    public DateTime AdministeredDate { get; set; }
    public DateTime? NextDueDate { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Pet Pet { get; set; } = null!;
}
