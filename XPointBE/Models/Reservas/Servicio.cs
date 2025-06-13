using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPointBE.Models;

public class Servicio : ModelAuditable
{
    
    public string Nombre { get; set; } = string.Empty;
    
    public string Descripcion { get; set; } = string.Empty;
    
    public decimal Precio { get; set; }
    
    public int? DuracionMinutos { get; set; }
    
    public bool Activo { get; set; } = true;
    
    public string? Categoria { get; set; }
    
    public ICollection<Reserva> Reservaciones { get; set; }
}