using Microsoft.EntityFrameworkCore;
using XPointBE.Interfaces;
using XPointBE.Models;

namespace XPointBE.Repositories;

public class ServicioRepository : IServicioRepository
{
    private readonly ApplicationDBContext _context;

    public ServicioRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<Servicio>> GetAllAsync()
    {
        return await _context.Servicios.ToListAsync();
    }

    public async Task<Servicio?> GetByIdAsync(int id)
    {
        return await _context.Servicios.FindAsync(id);
    }

    public async Task<Servicio> CreateAsync(Servicio servicio)
    {
        if (servicio == null)
            throw new ArgumentNullException(nameof(servicio));

        await _context.Servicios.AddAsync(servicio);
        await _context.SaveChangesAsync();
        return servicio;
    }

    public async Task<Servicio?> UpdateAsync(Servicio servicio)
    {
        if (servicio == null)
            throw new ArgumentNullException(nameof(servicio));

        var existingServicio = await _context.Servicios.FindAsync(servicio.Id);
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
}
