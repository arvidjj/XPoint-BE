using Microsoft.EntityFrameworkCore;
using XPointBE.Data;
using XPointBE.Models;
using XPointBE.Repositories.Interfaces;

namespace XPointBE.Repositories;

public class ServicioRepository : IServicioRepository
{
    private readonly ApplicationDbContext _context;

    public ServicioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Servicio>> GetAllAsync()
    {
        return await _context.Servicios.Include(s => s.Reservaciones)
                                       .ToListAsync();
    }

    public async Task<Servicio?> GetByIdAsync(int id)
    {
        return await _context.Servicios
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Servicio> CreateAsync(Servicio servicio)
    {
        if (servicio == null)
            throw new ArgumentNullException(nameof(servicio));

        await _context.Servicios.AddAsync(servicio);
        await _context.SaveChangesAsync();
        return servicio;
    }

    public async Task<Servicio?> UpdateAsync(int id, Servicio servicio)
    {
        if (servicio == null)
            throw new ArgumentNullException(nameof(servicio));

        var existingServicio = await _context.Servicios.FindAsync(id);
        if (existingServicio == null) return null;

        existingServicio.Nombre = servicio.Nombre;
        existingServicio.Descripcion = servicio.Descripcion;
        existingServicio.Precio = servicio.Precio;

        await _context.SaveChangesAsync();
        return existingServicio;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var servicio = await _context.Servicios.FindAsync(id);
        if (servicio == null) return false;

        _context.Servicios.Remove(servicio);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Servicios.AnyAsync(s => s.Id == id);
    }

    public async Task<bool> AtLeastOneExistsAsync()
    {
        return await _context.Servicios.AnyAsync();
    }
}
