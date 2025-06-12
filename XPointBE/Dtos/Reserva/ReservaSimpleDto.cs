using XPointBE.Models;

namespace XPointBE.Dtos.Reserva;

public class ReservaSimpleDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan? HoraInicio { get; set; }
    public string? UsuarioNombre { get; set; }
    public string? ServicioNombre { get; set; }
    public decimal Precio { get; set; }
    public ReservaEstadoEnum Estado { get; set; }
}