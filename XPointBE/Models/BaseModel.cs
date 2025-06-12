using System.ComponentModel.DataAnnotations;

namespace XPointBE.Models;

public abstract class BaseEntity : IBaseModel
{
    [Key]
    public int Id { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    
    public DateTime? FechaModificacion { get; set; }
    
    // Soft delete 
    public bool Eliminado { get; set; } = false;
    
    public DateTime? FechaEliminacion { get; set; }
}