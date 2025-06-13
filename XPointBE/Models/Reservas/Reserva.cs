using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XPointBE.Models.Usuarios;

namespace XPointBE.Models;

public class Reserva : ModelAuditable
{
    
    public DateTime Fecha { get; set; }
    public TimeSpan? HoraInicio { get; set; }
    public TimeSpan? HoraFin { get; set; }
    

    public string? UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual User? Usuario { get; set; }
    
    public int ServicioId { get; set; }
    
    [ForeignKey("ServicioId")]
    public virtual Servicio? Servicio { get; set; }
    
    public decimal Precio { get; set; }
    
    public ReservaEstadoEnum Estado { get; set; } = ReservaEstadoEnum.Pendiente;
    
    public string? Notas { get; set; }
    
    
}