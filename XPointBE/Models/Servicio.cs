using System.ComponentModel.DataAnnotations;

namespace XPointBE.Models;

public class Servicio
{
    public int Id { get; set; }
    
    [Required]
    public string Nombre { get; set; } = string.Empty;
    
    public string Descripcion { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
    public decimal Precio { get; set; }
}