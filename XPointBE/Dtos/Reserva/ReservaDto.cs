using XPointBE.Dtos.Servicio;
using XPointBE.Dtos.User;
using XPointBE.Models;

namespace XPointBE.Dtos.Reserva;

public class ReservaDto : BaseDto
{
    public DateTime Fecha { get; set; }
    public TimeSpan? HoraInicio { get; set; }
    public TimeSpan? HoraFin { get; set; }
    public int? UsuarioId { get; set; }
    public int ServicioId { get; set; }
    public decimal Precio { get; set; }
    public ReservaEstadoEnum Estado { get; set; }
    public string? Notas { get; set; }
    
    // Navigation properties as nested DTOs
    public UserSimpleDto? Usuario { get; set; }
    public ServicioSimpleDto? Servicio { get; set; }
    
}