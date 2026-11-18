using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Data;
using VeterinerKlinigi.Models;

namespace VeterinerKlinigi.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    #region Clinic Management

    public async Task<IActionResult> Clinics()
    {
        var clinics = await _context.Clinics.ToListAsync();
        return View(clinics);
    }

    public IActionResult CreateClinic()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateClinic(Clinic clinic)
    {
        if (ModelState.IsValid)
        {
            _context.Add(clinic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Clinics));
        }
        return View(clinic);
    }

    public async Task<IActionResult> EditClinic(int? id)
    {
        if (id == null)
            return NotFound();

        var clinic = await _context.Clinics.FindAsync(id);
        if (clinic == null)
            return NotFound();

        return View(clinic);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditClinic(int id, Clinic clinic)
    {
        if (id != clinic.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                clinic.UpdatedAt = DateTime.UtcNow;
                _context.Update(clinic);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicExists(clinic.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Clinics));
        }
        return View(clinic);
    }

    public async Task<IActionResult> DeleteClinic(int? id)
    {
        if (id == null)
            return NotFound();

        var clinic = await _context.Clinics.FindAsync(id);
        if (clinic == null)
            return NotFound();

        return View(clinic);
    }

    [HttpPost, ActionName("DeleteClinic")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteClinicConfirmed(int id)
    {
        var clinic = await _context.Clinics.FindAsync(id);
        if (clinic != null)
        {
            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Clinics));
    }

    #endregion

    #region Veterinarian Management

    public async Task<IActionResult> Veterinarians()
    {
        var vets = await _context.Veterinarians
            .Include(v => v.Clinic)
            .ToListAsync();
        return View(vets);
    }

    public async Task<IActionResult> CreateVeterinarian()
    {
        ViewBag.Clinics = await _context.Clinics.ToListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateVeterinarian(Veterinarian vet)
    {
        if (ModelState.IsValid)
        {
            _context.Add(vet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Veterinarians));
        }
        ViewBag.Clinics = await _context.Clinics.ToListAsync();
        return View(vet);
    }

    public async Task<IActionResult> EditVeterinarian(int? id)
    {
        if (id == null)
            return NotFound();

        var vet = await _context.Veterinarians.FindAsync(id);
        if (vet == null)
            return NotFound();

        ViewBag.Clinics = await _context.Clinics.ToListAsync();
        return View(vet);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditVeterinarian(int id, Veterinarian vet)
    {
        if (id != vet.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                vet.UpdatedAt = DateTime.UtcNow;
                _context.Update(vet);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeterinarianExists(vet.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Veterinarians));
        }
        ViewBag.Clinics = await _context.Clinics.ToListAsync();
        return View(vet);
    }

    public async Task<IActionResult> DeleteVeterinarian(int? id)
    {
        if (id == null)
            return NotFound();

        var vet = await _context.Veterinarians
            .Include(v => v.Clinic)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (vet == null)
            return NotFound();

        return View(vet);
    }

    [HttpPost, ActionName("DeleteVeterinarian")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteVeterinarianConfirmed(int id)
    {
        var vet = await _context.Veterinarians.FindAsync(id);
        if (vet != null)
        {
            _context.Veterinarians.Remove(vet);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Veterinarians));
    }

    #endregion

    #region Service Management

    public async Task<IActionResult> Services()
    {
        var services = await _context.Services
            .Include(s => s.Clinic)
            .ToListAsync();
        return View(services);
    }

    public async Task<IActionResult> CreateService()
    {
        ViewBag.Clinics = await _context.Clinics.ToListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateService(Service service)
    {
        if (ModelState.IsValid)
        {
            _context.Add(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Services));
        }
        ViewBag.Clinics = await _context.Clinics.ToListAsync();
        return View(service);
    }

    public async Task<IActionResult> EditService(int? id)
    {
        if (id == null)
            return NotFound();

        var service = await _context.Services.FindAsync(id);
        if (service == null)
            return NotFound();

        ViewBag.Clinics = await _context.Clinics.ToListAsync();
        return View(service);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditService(int id, Service service)
    {
        if (id != service.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                service.UpdatedAt = DateTime.UtcNow;
                _context.Update(service);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(service.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Services));
        }
        ViewBag.Clinics = await _context.Clinics.ToListAsync();
        return View(service);
    }

    public async Task<IActionResult> DeleteService(int? id)
    {
        if (id == null)
            return NotFound();

        var service = await _context.Services
            .Include(s => s.Clinic)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (service == null)
            return NotFound();

        return View(service);
    }

    [HttpPost, ActionName("DeleteService")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteServiceConfirmed(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service != null)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Services));
    }

    #endregion

    #region Helper Methods

    private bool ClinicExists(int id)
    {
        return _context.Clinics.Any(e => e.Id == id);
    }

    private bool VeterinarianExists(int id)
    {
        return _context.Veterinarians.Any(e => e.Id == id);
    }

    private bool ServiceExists(int id)
    {
        return _context.Services.Any(e => e.Id == id);
    }

    #endregion
}
