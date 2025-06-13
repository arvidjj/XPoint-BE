using Microsoft.EntityFrameworkCore;
using XPointBE.Data;
using XPointBE.Helpers;
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

    /**/
    
    public async Task<List<Reserva>> GetAllQueryAsync(ReservasQueryObject reservasQuery)
    {
        var reservas =  _context.Reservas
            .Include(r => r.Usuario)
            .Include(r => r.Servicio)
            .AsQueryable();
        
        //if fecha is not null (hasvalue doesnt work)
        if (!string.IsNullOrWhiteSpace(reservasQuery.Fecha))
        {
            reservas = reservas.Where(r => r.Fecha.ToString("yyyy-MM-dd") == reservasQuery.Fecha);
        }

        if (!string.IsNullOrWhiteSpace(reservasQuery.UsuarioId))
        {
            reservas = reservas.Where(r => r.UsuarioId == reservasQuery.UsuarioId);
        }
        if (!string.IsNullOrWhiteSpace(reservasQuery.Estado)){
            if (Enum.TryParse<ReservaEstadoEnum>(reservasQuery.Estado, true, out var estado))
            {
                reservas = reservas.Where(r => r.Estado == estado);
            }
            else
            {
                throw new ArgumentException("Estado must be a valid ReservaEstadoEnum value.");
            }
        }
        
        ///order
        if (!string.IsNullOrWhiteSpace(reservasQuery.SortBy))
        {
            if (reservasQuery.SortBy.Equals("fecha", StringComparison.OrdinalIgnoreCase))
            {
                reservas = reservasQuery.isDescending ? reservas.OrderByDescending(s => s.Fecha) : reservas.OrderBy(s => s.Fecha);
            }
        }
        
        //pagination
        var skipNumber = (reservasQuery.Page - 1) * reservasQuery.PageSize;
        
        return await reservas.Skip(skipNumber).Take(reservasQuery.PageSize).ToListAsync();
        
    }
}
