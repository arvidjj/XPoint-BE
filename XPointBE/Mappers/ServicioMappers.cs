
using XPointBE.Dtos.Reserva;
using XPointBE.Dtos.Servicio;
using XPointBE.Models;

namespace XPointBE.Mappers;

public static class ServicioMappers
{
    public static ServicioDto ToServicioDto (this Servicio servicio)
    {
        return new ServicioDto
        {
            Id = servicio.Id,
            Nombre = servicio.Nombre,
            Precio = servicio.Precio,
            Descripcion = servicio.Descripcion,
            Reservaciones = servicio.Reservaciones?.Select(r => r.ToReservaSimpleDto()).ToList() ?? new List<ReservaSimpleDto>()
        };
    }
    

    public static Servicio ToServicioFromCreateDTO(this CreateServicioRequestDto servicioDto)
    {
        return new Servicio
        {
            Nombre = servicioDto.Nombre,
            Descripcion = servicioDto.Descripcion,
            Precio = servicioDto.Precio
        };
    }
    
    // why id? // porque al actualizar una reserva, necesitamos el id para buscarla en la base de datos
    public static Servicio ToServicioFromUpdateDTO(this UpdateServicioRequestDto servicioDto, int id)
    {
        return new Servicio
        {
            Id = id,
            Nombre = servicioDto.Nombre,
            Descripcion = servicioDto.Descripcion,
            Precio = servicioDto.Precio
        };
    }
}