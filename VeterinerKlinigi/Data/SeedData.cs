using Microsoft.AspNetCore.Identity;
using VeterinerKlinigi.Models;

namespace VeterinerKlinigi.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await EnsureRolesAsync(roleManager);
        await EnsureAdminAsync(userManager);
        await EnsureClinicDataAsync(context);
    }

    private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "Veterinarian", "Member" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }

    private static async Task EnsureAdminAsync(UserManager<ApplicationUser> userManager)
    {
        var adminEmail = "ogrencinumarasi@sakarya.edu.tr";
        var admin = await userManager.FindByEmailAsync(adminEmail);

        if (admin == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "sau");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }

    private static async Task EnsureClinicDataAsync(ApplicationDbContext context)
    {
        if (context.Clinics.Any())
            return;

        var clinic = new Clinic
        {
            Name = "Merkez Veteriner Kliniği",
            Address = "İstanbul, Türkiye",
            Phone = "+90 (212) 555-0001",
            Email = "info@merkezklinik.com",
            OpeningTime = TimeSpan.FromHours(9),
            ClosingTime = TimeSpan.FromHours(18)
        };

        context.Clinics.Add(clinic);
        await context.SaveChangesAsync();

        var vets = new List<Veterinarian>
        {
            new Veterinarian
            {
                FirstName = "Ahmet",
                LastName = "Yılmaz",
                LicenseNumber = "VET001",
                Specializations = "Küçük Hayvan, Cerrahi",
                ClinicId = clinic.Id,
                StartTime = TimeSpan.FromHours(9),
                EndTime = TimeSpan.FromHours(17)
            },
            new Veterinarian
            {
                FirstName = "Ayşe",
                LastName = "Demir",
                LicenseNumber = "VET002",
                Specializations = "Dermatoloji, Egzotik Hayvan",
                ClinicId = clinic.Id,
                StartTime = TimeSpan.FromHours(10),
                EndTime = TimeSpan.FromHours(18)
            }
        };

        context.Veterinarians.AddRange(vets);
        await context.SaveChangesAsync();

        var services = new List<Service>
        {
            new Service
            {
                Name = "Muayene",
                Description = "Genel veteriner muayenesi",
                DurationMinutes = 30,
                Price = 250m,
                ClinicId = clinic.Id
            },
            new Service
            {
                Name = "Aşılama",
                Description = "Koruyucu aşılama hizmeti",
                DurationMinutes = 20,
                Price = 350m,
                ClinicId = clinic.Id
            },
            new Service
            {
                Name = "Diş Temizliği",
                Description = "Profesyonel diş temizliği",
                DurationMinutes = 45,
                Price = 500m,
                ClinicId = clinic.Id
            },
            new Service
            {
                Name = "Kısırlaştırma",
                Description = "Cerrahi kısırlaştırma işlemi",
                DurationMinutes = 120,
                Price = 1500m,
                ClinicId = clinic.Id
            }
        };

        context.Services.AddRange(services);
        await context.SaveChangesAsync();

        var vetServices = new List<VeterinarianService>
        {
            new VeterinarianService { VeterinarianId = vets[0].Id, ServiceId = services[0].Id },
            new VeterinarianService { VeterinarianId = vets[0].Id, ServiceId = services[1].Id },
            new VeterinarianService { VeterinarianId = vets[0].Id, ServiceId = services[3].Id },
            new VeterinarianService { VeterinarianId = vets[1].Id, ServiceId = services[0].Id },
            new VeterinarianService { VeterinarianId = vets[1].Id, ServiceId = services[2].Id },
        };

        context.VeterinarianServices.AddRange(vetServices);
        await context.SaveChangesAsync();
    }
}
