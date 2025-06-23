using XPointBE.Dtos.Ciudad;
using XPointBE.Models;

namespace XPointBE.Mappers;

public static class CiudadMappers
{
    
    public static Ciudad MapToCiudad(this CiudadDto ciudadDto)
    {
        return new Ciudad
        {
            id = ciudadDto.Id,
            nombre = ciudadDto.Nombre
        };
    }

    public static CiudadDto MapToCiudadDto(this Ciudad ciudad)
    {
        return new CiudadDto
        {
            Id = ciudad.id,
            Nombre = ciudad.nombre
        };
    }
    
    public static Departamento MapToDepartamento(this DepartamentoDto departamentoDto)
    {
        return new Departamento
        {
            id = departamentoDto.Id,
            nombre = departamentoDto.Nombre
        };
    }
    
    public static DepartamentoDto MapToDepartamentoDto(this Departamento departamento)
    {
        return new DepartamentoDto
        {
            Id = departamento.id,
            Nombre = departamento.nombre
        };
    }
}