using XPointBE.Dtos.Reserva;
using XPointBE.Dtos.Servicio;
using XPointBE.Dtos.User;
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
            HoraInicio = reserva.HoraInicio,
            HoraFin = reserva.HoraFin,
            UsuarioId = reserva.UsuarioId,
            ServicioId = reserva.ServicioId,
            Precio = reserva.Precio,
            Estado = reserva.Estado,
            Notas = reserva.Notas,
            Usuario = reserva.Usuario != null ? new UserSimpleDto
            {
                Id = reserva.Usuario.Id,
                Nombre = reserva.Usuario.Nombre
            } : null,
            Servicio = reserva.Servicio != null ? new ServicioSimpleDto
            {
                Id = reserva.Servicio.Id,
                Nombre = reserva.Servicio.Nombre
            } : null
        };
    }
    
    
    
    public static ReservaSimpleDto ToReservaSimpleDto(this Reserva reserva)
    {
        return new ReservaSimpleDto
        {
            Id = reserva.Id,
            Fecha = reserva.Fecha,
            HoraInicio = reserva.HoraInicio,
            Precio = reserva.Precio,
            Estado = reserva.Estado,
            UsuarioNombre = reserva.Usuario?.Nombre ?? "Usuario no encontrado", //TODO: no funciona, revisar
            ServicioNombre = reserva.Servicio?.Nombre ?? "Servicio no encontrado"
        };
    }

    public static Reserva ToReservaFromCreateDto(this CreateReservaRequestDto reservaDto, int servicioId)
    {
        return new Reserva
        {
            Fecha = reservaDto.Fecha,
            HoraInicio = reservaDto.HoraInicio,
            HoraFin = reservaDto.HoraFin,
            UsuarioId = reservaDto.UsuarioId,
            Precio = 0, // El precio se decide por el servicio, no se asigna aquí
            ServicioId = servicioId,
            Notas = reservaDto.Notas
        };
    }
    
    public static Reserva ToReservaFromUpdateDto(this UpdateReservaRequestDto reservaDto)
    {
        return new Reserva
        {
            Fecha = reservaDto.Fecha,
            HoraInicio = reservaDto.HoraInicio,
            HoraFin = reservaDto.HoraFin,
            UsuarioId = reservaDto.UsuarioId,
            Precio = reservaDto.Precio,
            Estado = reservaDto.Estado,
            Notas = reservaDto.Notas
        };
    }
}