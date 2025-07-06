using XPointBE.Dtos.Reserva;

namespace XPointBE.Dtos.Servicio;

public class ServicioDto : BaseDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int? DuracionMinutos { get; set; }
    public bool Activo { get; set; }
    public List<ReservaSimpleDto> Reservaciones { get; set; }
}