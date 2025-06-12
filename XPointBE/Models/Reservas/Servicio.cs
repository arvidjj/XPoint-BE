using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPointBE.Models;

public class Servicio : ModelAuditable
{
    
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string Descripcion { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }
    
    public int? DuracionMinutos { get; set; }
    
    public bool Activo { get; set; } = true;
    
    [StringLength(50)]
    public string? Categoria { get; set; }
    
    public ICollection<Reserva> Reservaciones { get; set; }
}