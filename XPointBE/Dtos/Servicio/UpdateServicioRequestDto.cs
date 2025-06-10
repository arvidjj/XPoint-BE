namespace XPointBE.Dtos.Servicio;

public class UpdateServicioRequestDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}