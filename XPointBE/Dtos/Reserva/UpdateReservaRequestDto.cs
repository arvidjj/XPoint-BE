using System.ComponentModel.DataAnnotations;
using XPointBE.Models;

namespace XPointBE.Dtos.Reserva;

public class UpdateReservaRequestDto
{
    
    [Required]
    public DateTime Fecha { get; set; }
    
    public TimeSpan? HoraInicio { get; set; }
    public TimeSpan? HoraFin { get; set; }
    
    public string? UsuarioId { get; set; }
    
    public decimal Precio { get; set; }
    
    public ReservaEstadoEnum Estado { get; set; }
    
    [StringLength(500)]
    public string? Notas { get; set; }
}