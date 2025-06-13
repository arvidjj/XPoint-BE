using XPointBE.Models;

namespace XPointBE.Helpers;

public class ReservasQueryObject : BaseQuery
{
    public string? Fecha { get; set; } = null;
    public string? UsuarioId { get; set; } = null;
    public string? Estado { get; set; } = null;

    public string? SortBy { get; set; } = null;
    
    public bool isDescending { get; set; } = false;


}