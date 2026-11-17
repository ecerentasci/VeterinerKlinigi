using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Models;

namespace VeterinerKlinigi.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Clinic> Clinics { get; set; }
    public DbSet<Veterinarian> Veterinarians { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<VeterinarianService> VeterinarianServices { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<VaccineRecord> VaccineRecords { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<VeterinarianService>()
            .HasKey(vs => vs.Id);

        builder.Entity<VeterinarianService>()
            .HasOne(vs => vs.Veterinarian)
            .WithMany(v => v.VeterinarianServices)
            .HasForeignKey(vs => vs.VeterinarianId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<VeterinarianService>()
            .HasOne(vs => vs.Service)
            .WithMany(s => s.VeterinarianServices)
            .HasForeignKey(vs => vs.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Pet>()
            .HasOne(p => p.Owner)
            .WithMany(u => u.Pets)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<VaccineRecord>()
            .HasOne(vr => vr.Pet)
            .WithMany(p => p.VaccineRecords)
            .HasForeignKey(vr => vr.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Appointment>()
            .HasOne(a => a.Pet)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Appointment>()
            .HasOne(a => a.Owner)
            .WithMany(u => u.Appointments)
            .HasForeignKey(a => a.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Veterinarian>()
            .HasOne(v => v.Clinic)
            .WithMany(c => c.Veterinarians)
            .HasForeignKey(v => v.ClinicId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Service>()
            .HasOne(s => s.Clinic)
            .WithMany(c => c.Services)
            .HasForeignKey(s => s.ClinicId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Appointment>()
            .HasOne(a => a.Clinic)
            .WithMany(c => c.Appointments)
            .HasForeignKey(a => a.ClinicId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
