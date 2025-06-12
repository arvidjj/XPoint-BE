using System.ComponentModel.DataAnnotations;

namespace XPointBE.Models;

public abstract class ModelAuditable : BaseEntity
{
    [StringLength(100)]
    public string? CreadoPor { get; set; }
    
    [StringLength(100)]
    public string? ModificadoPor { get; set; }
    
    [StringLength(100)]
    public string? EliminadoPor { get; set; }
}