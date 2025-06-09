namespace XPointBE.Dtos.Reserva;

public class ReservaDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public int? UsuarioId { get; set; }
    public decimal Precio { get; set; }
    public Boolean Terminada { get; set; }
}