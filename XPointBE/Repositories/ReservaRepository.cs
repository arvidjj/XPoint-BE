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
        var reservas = await _context.Reservas.ToListAsync();
        return reservas;
    }
}