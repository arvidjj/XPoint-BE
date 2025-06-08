using System.ComponentModel.DataAnnotations.Schema;

namespace XPointBE.Models;

public class Reserva
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public int? UsuarioId { get; set; }
    public string ServicioId { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }
    
    public Boolean Terminada { get; set; }
}