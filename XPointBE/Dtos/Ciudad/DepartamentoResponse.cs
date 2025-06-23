using XPointBE.Models;

namespace XPointBE.Dtos.Ciudad;

public class DepartamentoResponse
{
    public int limit { get; set; }
    public int skip { get; set; }
    public int total { get; set; }
    public List<Departamento> data { get; set; }
}
