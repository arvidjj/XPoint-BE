using System.ComponentModel.DataAnnotations;

namespace XPointBE.Dtos.Servicio;

public class CreateServicioRequestDto
{
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string Descripcion { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
    public decimal Precio { get; set; }
    
    public int? DuracionMinutos { get; set; }
    
    [StringLength(50)]
    public string? Categoria { get; set; }
}