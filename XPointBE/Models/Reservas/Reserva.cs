using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPointBE.Models;

public class Reserva : ModelAuditable
{
    
    [Required]
    public DateTime Fecha { get; set; }
    public TimeSpan? HoraInicio { get; set; }
    public TimeSpan? HoraFin { get; set; }
    

    public int? UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual User? Usuario { get; set; }
    
    public int ServicioId { get; set; }
    
    [ForeignKey("ServicioId")]
    public virtual Servicio? Servicio { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }
    
    public ReservaEstadoEnum Estado { get; set; } = ReservaEstadoEnum.Pendiente;
    
    [StringLength(500)]
    public string? Notas { get; set; }
    
    
}