using System.ComponentModel.DataAnnotations;

namespace XPointBE.Dtos.Servicio;

public class UpdateServicioRequestDto
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    [MinLength(3, ErrorMessage = "El nombre del servicio debe tener al menos 3 caracteres.")]
    public string Nombre { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string Descripcion { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Precio { get; set; }
    
    public int? DuracionMinutos { get; set; }
    
    [StringLength(50)]
    public string? Categoria { get; set; }
    
    public bool Activo { get; set; }
}