using XPointBE.Dtos.Reserva;
using XPointBE.Models;

namespace XPointBE.Mappers;

public static class ReservaMappers
{
    public static ReservaDto ToReservaDto (this Reserva reserva)
    {
        return new ReservaDto
        {
            Id = reserva.Id,
            Fecha = reserva.Fecha,
            UsuarioId = reserva.UsuarioId,
            Precio = reserva.Precio,
            Terminada = reserva.Terminada
        };
    }

    public static Reserva ToReservaFromCreateDTO(this CreateReservaRequestDto reservaDto)
    {
        return new Reserva
        {
            Fecha = reservaDto.Fecha,
            UsuarioId = reservaDto.UsuarioId,
            Precio = 0, // El precio se decide por el servicio, no se asigna aquí
            ServicioId = reservaDto.ServicioId,
            Terminada = false // Por defecto, una reserva no está terminada
        };
    }
}