using System.ComponentModel.DataAnnotations;

namespace XPointBE.Dtos.Reserva;

public class CreateReservaRequestDto
{
    [Required]
    public DateTime Fecha { get; set; }
    
    public TimeSpan? HoraInicio { get; set; }
    public TimeSpan? HoraFin { get; set; }
    
    public string? UsuarioId { get; set; }
    
    //[Required]
    //public int ServicioId { get; set; }
    
    public decimal Precio { get; set; }
    
    [StringLength(500)]
    public string? Notas { get; set; }
}