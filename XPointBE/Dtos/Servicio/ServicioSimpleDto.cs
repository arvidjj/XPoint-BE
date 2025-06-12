using XPointBE.Dtos.Reserva;

namespace XPointBE.Dtos.Servicio;

public class ServicioSimpleDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int? DuracionMinutos { get; set; }
    public string? Categoria { get; set; }
}