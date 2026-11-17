namespace VeterinerKlinigi.Models;

public class VeterinarianService
{
    public int Id { get; set; }
    public int VeterinarianId { get; set; }
    public int ServiceId { get; set; }

    public Veterinarian Veterinarian { get; set; } = null!;
    public Service Service { get; set; } = null!;
}
