namespace XPointBE.Dtos.Reserva;

public class CreateReservaRequestDto
{
    public DateTime Fecha { get; set; }
    public int? UsuarioId { get; set; }
   // public decimal Precio { get; set; } //precio se decide por el servicio
    public string ServicioId { get; set; } = string.Empty;
}