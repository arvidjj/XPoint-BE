using Microsoft.EntityFrameworkCore;
using XPointBE.Interfaces;
using XPointBE.Models;

namespace XPointBE.Repositories;

public class ReservaRepository : IReservaRepository
{
    private readonly ApplicationDBContext _context;

    public ReservaRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<Reserva>> GetAllAsync()
    {
        return await _context.Reservas.ToListAsync();
    }

    public async Task<Reserva?> GetByIdAsync(int id)
    {
        return await _context.Reservas.FindAsync(id);
    }

    public async Task<Reserva> CreateAsync(Reserva reserva)
    {
        if (reserva == null)
            throw new ArgumentNullException(nameof(reserva));

        await _context.Reservas.AddAsync(reserva);
        await _context.SaveChangesAsync();
        return reserva;
    }

    public async Task<Reserva?> UpdateAsync(Reserva reserva)
    {
        if (reserva == null)
            throw new ArgumentNullException(nameof(reserva));

        var existingReserva = await _context.Reservas.FindAsync(reserva.Id);
        if (existingReserva == null) return null;

        existingReserva.Fecha = reserva.Fecha;
        existingReserva.UsuarioId = reserva.UsuarioId;
        existingReserva.ServicioId = reserva.ServicioId;
        existingReserva.Terminada = reserva.Terminada;

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
}
