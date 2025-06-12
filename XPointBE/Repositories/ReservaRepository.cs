using Microsoft.EntityFrameworkCore;
using XPointBE.Data;
using XPointBE.Models;
using XPointBE.Repositories.Interfaces;

namespace XPointBE.Repositories;

public class ReservaRepository : IReservaRepository
{
    private readonly ApplicationDbContext _context;

    public ReservaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Reserva>> GetAllAsync()
    {
        return await _context.Reservas
            .Include(r => r.Usuario)
            .Include(r => r.Servicio)
            .ToListAsync();
    }

    public async Task<Reserva?> GetByIdAsync(int id)
    {
        return await _context.Reservas
            .Include(r => r.Usuario)
            .Include(r => r.Servicio)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Reserva> CreateAsync(Reserva reserva)
    {
        if (reserva == null)
            throw new ArgumentNullException(nameof(reserva));

        await _context.Reservas.AddAsync(reserva);
        await _context.SaveChangesAsync();
        return reserva;
    }

    public async Task<Reserva?> UpdateAsync(int id, Reserva reserva)
    {
        if (reserva == null)
            throw new ArgumentNullException(nameof(reserva));

        var existingReserva = await _context.Reservas.FindAsync(id);
        if (existingReserva == null) return null;

        existingReserva.Fecha = reserva.Fecha;
        existingReserva.UsuarioId = reserva.UsuarioId;
        existingReserva.HoraInicio = reserva.HoraInicio;
        existingReserva.Estado = reserva.Estado;
        existingReserva.Precio = reserva.Precio;
        existingReserva.Notas = reserva.Notas;

        await _context.SaveChangesAsync();
        return existingReserva;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null) return false;

        _context.Reservas.Remove(reserva);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Reservas.AnyAsync(r => r.Id == id);
    }
}
